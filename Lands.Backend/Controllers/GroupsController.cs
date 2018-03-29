namespace Lands.Backend.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Domain;
    using Models;

    [Authorize(Roles = "Admin")]
    public class GroupsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

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

            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "GroupId,Name")] Group group)
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
            Group group = await db.Groups.FindAsync(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "GroupId,Name")] Group group)
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
            Group group = await db.Groups.FindAsync(id);
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
            Group group = await db.Groups.FindAsync(id);
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
