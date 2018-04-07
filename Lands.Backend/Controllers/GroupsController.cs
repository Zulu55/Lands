namespace Lands.Backend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Domain;
    using Microsoft.Azure.NotificationHubs;
    using Models;

    [Authorize(Roles = "Admin")]
    public class GroupsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();
        private NotificationHubClient hub;

        public GroupsController()
        {
            var listenConnectionString = "Endpoint=" +
                "sb://russiahub.servicebus.windows.net/;SharedAccessKeyName=" +
                "DefaultFullSharedAccessSignature;" +
                "SharedAccessKey=XXLIqplD13sRQ0N8Ex5ZBSsuGGlnFDJy+r7ai+MOQfs=";
            var notificationHubName = "russia";
            this.hub = NotificationHubClient.CreateClientFromConnectionString(
                listenConnectionString, 
                notificationHubName);
        }

        public async Task<ActionResult> NotificateUsers(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var match = await db.Matches.FindAsync(id);

            if (match == null)
            {
                return HttpNotFound();
            }

            var tags = new List<string>();
            var boards = await db.Boards.ToListAsync();
            foreach (var board in boards)
            {
                var prediction = await db.Predictions.Where(p => p.BoardId == board.BoardId && p.MatchId == match.MatchId).FirstOrDefaultAsync();
                if (prediction == null)
                {
                    tags.Add(string.Format("userId:{0}", board.UserId));
                }
            }

            if (tags.Count > 0)
            {
                await this.SendNotificationNoPoints(tags.Distinct().ToList(), match, "alert");
            }

            return RedirectToAction(string.Format("Details/{0}", match.GroupId));
        }

        private async Task SendNotificationNoPoints(List<string> tags, Match match, string type)
        {
            var message = string.Format(
                "{0} Vs. {1}",
                match.Local.Name, 
                match.Visitor.Name);
            await this.SendNotification(tags, message, type);
        }

        private async Task SendNotification(List<string> tags, string message, string type)
        {
            try
            {
                do
                {
                    if (tags.Count <= 20)
                    {
                        await hub.SendGcmNativeNotificationAsync("{ \"data\" : " +
                            "{\"Message\":\"" + message + "\", " +
                            "\"Type\":\"" + type + "\"}}", tags);
                        tags.Clear();
                    }
                    else
                    {
                        var tags20 = new List<string>();
                        for (int i = 0; i < 20; i++)
                        {
                            tags20.Add(tags[i]);
                        }

                        tags.RemoveRange(0, 20);
                        await hub.SendGcmNativeNotificationAsync("{ \"data\" : " +
                            "{\"Message\":\"" + message + "\", " +
                            "\"Type\":\"" + type + "\"}}", tags20);
                    }
                } while (tags.Count > 0);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        [HttpPost]
        public async Task<ActionResult> CloseMatch(Match match)
        {
            using (var transacction = db.Database.BeginTransaction())
            {
                try
                {
                    // Update match
                    var oldMatch = await db.Matches.FindAsync(match.MatchId);
                    oldMatch.LocalGoals = match.LocalGoals;
                    oldMatch.VisitorGoals = match.VisitorGoals;
                    oldMatch.StatusMatchId = 2; // Closed
                    db.Entry(oldMatch).State = EntityState.Modified;

                    var statusMatch = this.GetStatus(oldMatch.LocalGoals.Value, oldMatch.VisitorGoals.Value);

                    // Update predictions
                    var predictions = await db.Predictions
                        .Where(p => p.MatchId == match.MatchId)
                        .ToListAsync();
                    foreach (var prediction in predictions)
                    {
                        var points = 0;
                        if (prediction.LocalGoals == oldMatch.LocalGoals &&
                            prediction.VisitorGoals == oldMatch.VisitorGoals)
                        {
                            points = 3;
                        }
                        else
                        {
                            var statusPrediction = this.GetStatus(prediction.LocalGoals, prediction.VisitorGoals);
                            if (statusMatch == statusPrediction)
                            {
                                points = 1;
                            }
                        }

                        prediction.Points = points;
                        db.Entry(prediction).State = EntityState.Modified;
                    }

                    await db.SaveChangesAsync();
                    transacction.Commit();

                    return RedirectToAction(string.Format("Details/{0}", oldMatch.GroupId));
                }
                catch (Exception ex)
                {
                    transacction.Rollback();
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(match);
                }
            }
        }

        private int GetStatus(int localGoals, int visitorGoals)
        {
            if (localGoals > visitorGoals)
            {
                return 1; // Local win
            }

            if (visitorGoals > localGoals)
            {
                return 2; // Visitor win
            }

            return 3; // Draw
        }

        public async Task<ActionResult> CloseMatch(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var match = await db.Matches.FindAsync(id);

            if (match == null)
            {
                return HttpNotFound();
            }

            match.DateTime = match.DateTime.ToLocalTime();
            return View(match);
        }

        [HttpPost]
        public async Task<ActionResult> EditMatch(Match match)
        {
            if (ModelState.IsValid)
            {
                match.DateTime = match.DateTime.ToUniversalTime();
                db.Entry(match).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction(string.Format("Details/{0}", match.GroupId));
            }

            var teams = await this.GetTeamsGroup(match.Group);
            ViewBag.LocalId = new SelectList(teams.OrderBy(t => t.Name), "TeamId", "Name", match.LocalId);
            ViewBag.VisitorId = new SelectList(teams.OrderBy(t => t.Name), "TeamId", "Name", match.VisitorId);
            return View(match);
        }

        public async Task<ActionResult> EditMatch(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var match = await db.Matches.FindAsync(id);

            if (match == null)
            {
                return HttpNotFound();
            }

            match.DateTime = match.DateTime.ToLocalTime();
            var teams = await this.GetTeamsGroup(match.Group);
            ViewBag.LocalId = new SelectList(teams.OrderBy(t => t.Name), "TeamId", "Name", match.LocalId);
            ViewBag.VisitorId = new SelectList(teams.OrderBy(t => t.Name), "TeamId", "Name", match.VisitorId);
            return View(match);
        }

        public async Task<ActionResult> DeleteMatch(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var match = await db.Matches.FindAsync(id);

            if (match == null)
            {
                return HttpNotFound();
            }

            db.Matches.Remove(match);
            await db.SaveChangesAsync();
            return RedirectToAction(string.Format("Details/{0}", match.GroupId));
        }

        [HttpPost]
        public async Task<ActionResult> AddMatch(Match match)
        {
            if (ModelState.IsValid)
            {
                match.StatusMatchId = 1;
                db.Matches.Add(match);
                await db.SaveChangesAsync();
                return RedirectToAction(string.Format("Details/{0}", match.GroupId));
            }

            var group = await db.Groups.FindAsync(match.GroupId);
            var teams = await this.GetTeamsGroup(group);
            ViewBag.LocalId = new SelectList(teams.OrderBy(t => t.Name), "TeamId", "Name");
            ViewBag.VisitorId = new SelectList(teams.OrderBy(t => t.Name), "TeamId", "Name");
            return View(match);
        }

        public async Task<ActionResult> AddMatch(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var group = await db.Groups.FindAsync(id);

            if (group == null)
            {
                return HttpNotFound();
            }

            var match = new Match
            {
                DateTime = DateTime.Today,
                GroupId = group.GroupId,
            };

            var teams = await this.GetTeamsGroup(group);
            ViewBag.LocalId = new SelectList(teams.OrderBy(t => t.Name), "TeamId", "Name");
            ViewBag.VisitorId = new SelectList(teams.OrderBy(t => t.Name), "TeamId", "Name");
            return View(match);
        }

        public async Task<List<Team>> GetTeamsGroup(Group group)
        {
            var teams = new List<Team>();
            foreach (var item in group.GroupTeams)
            {
                var team = await db.Teams.FindAsync(item.TeamId);
                if (team != null)
                {
                    teams.Add(team);
                }
            }

            return teams;
        }

        public async Task<ActionResult> DeleteTeam(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var groupTeam = await db.GroupTeams.FindAsync(id);

            if (groupTeam == null)
            {
                return HttpNotFound();
            }

            db.GroupTeams.Remove(groupTeam);
            await db.SaveChangesAsync();
            return RedirectToAction(string.Format("Details/{0}", groupTeam.GroupId));
        }

        [HttpPost]
        public async Task<ActionResult> AddTeam(GroupTeam groupTeam)
        {
            if (ModelState.IsValid)
            {
                db.GroupTeams.Add(groupTeam);
                await db.SaveChangesAsync();
                return RedirectToAction(string.Format("Details/{0}", groupTeam.GroupId));
            }

            ViewBag.TeamId = new SelectList(db.Teams.OrderBy(t => t.Name), "TeamId", "Name");
            return View(groupTeam);
        }

        public async Task<ActionResult> AddTeam(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var group = await db.Groups.FindAsync(id);

            if (group == null)
            {
                return HttpNotFound();
            }

            var groupTeam = new GroupTeam
            {
                GroupId = group.GroupId,
            };

            ViewBag.TeamId = new SelectList(db.Teams.OrderBy(t => t.Name), "TeamId", "Name");
            return View(groupTeam);
        }

        // GET: Groups
        public async Task<ActionResult> Index()
        {
            return View(await db.Groups.ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var group = await db.Groups.FindAsync(id);

            if (group == null)
            {
                return HttpNotFound();
            }

            foreach (var match in group.Matches)
            {
                match.DateTime = match.DateTime.ToLocalTime();
            }

            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Group group)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(group);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: Groups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var group = await db.Groups.FindAsync(id);

            if (group == null)
            {
                return HttpNotFound();
            }

            return View(group);
        }

        // POST: Groups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: Groups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var group = await db.Groups.FindAsync(id);

            if (group == null)
            {
                return HttpNotFound();
            }

            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var group = await db.Groups.FindAsync(id);
            db.Groups.Remove(group);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}