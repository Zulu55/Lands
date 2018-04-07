using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lands.Backend.Models;
using Lands.Domain;

namespace Lands.Backend.Controllers
{
    public class GroupTeamsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: GroupTeams
        public async Task<ActionResult> Index()
        {
            var groupTeams = db.GroupTeams.Include(g => g.Group).Include(g => g.Team);
            return View(await groupTeams.ToListAsync());
        }

        // GET: GroupTeams/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupTeam groupTeam = await db.GroupTeams.FindAsync(id);
            if (groupTeam == null)
            {
                return HttpNotFound();
            }
            return View(groupTeam);
        }

        // GET: GroupTeams/Create
        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(db.Groups, "GroupId", "Name");
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "Name");
            return View();
        }

        // POST: GroupTeams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "GroupTeamId,GroupId,TeamId")] GroupTeam groupTeam)
        {
            if (ModelState.IsValid)
            {
                db.GroupTeams.Add(groupTeam);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.Groups, "GroupId", "Name", groupTeam.GroupId);
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "Name", groupTeam.TeamId);
            return View(groupTeam);
        }

        // GET: GroupTeams/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupTeam groupTeam = await db.GroupTeams.FindAsync(id);
            if (groupTeam == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.Groups, "GroupId", "Name", groupTeam.GroupId);
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "Name", groupTeam.TeamId);
            return View(groupTeam);
        }

        // POST: GroupTeams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "GroupTeamId,GroupId,TeamId")] GroupTeam groupTeam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupTeam).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.Groups, "GroupId", "Name", groupTeam.GroupId);
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "Name", groupTeam.TeamId);
            return View(groupTeam);
        }

        // GET: GroupTeams/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupTeam groupTeam = await db.GroupTeams.FindAsync(id);
            if (groupTeam == null)
            {
                return HttpNotFound();
            }
            return View(groupTeam);
        }

        // POST: GroupTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            GroupTeam groupTeam = await db.GroupTeams.FindAsync(id);
            db.GroupTeams.Remove(groupTeam);
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
