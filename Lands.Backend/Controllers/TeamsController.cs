namespace Lands.Backend.Controllers
{
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Domain;
    using Helpers;
    using Models;

    [Authorize(Roles = "Admin")]
    public class TeamsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Teams
        public async Task<ActionResult> Index()
        {
            return View(await db.Teams.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = await db.Teams.FindAsync(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }   

        // GET: Teams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TeamView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Teams";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var team = this.ToTeam(view);
                team.ImagePath = pic;
                db.Teams.Add(team);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        private Team ToTeam(TeamView view)
        {
            return new Team
            {
                ImagePath = view.ImagePath,
                Name = view.Name,
                TeamId = view.TeamId,
            };
        }

        // GET: Teams/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var team = await db.Teams.FindAsync(id);

            if (team == null)
            {
                return HttpNotFound();
            }

            var view = this.ToView(team);
            return View(view);
        }

        private TeamView ToView(Team team)
        {
            return new TeamView
            {
                ImagePath = team.ImagePath,
                Name = team.Name,
                TeamId = team.TeamId,
            };
        }

        // POST: Teams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TeamView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.ImagePath;
                var folder = "~/Content/Teams";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var team = this.ToTeam(view);
                team.ImagePath = pic;
                db.Entry(team).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        // GET: Teams/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = await db.Teams.FindAsync(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Team team = await db.Teams.FindAsync(id);
            db.Teams.Remove(team);
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
