namespace Lands.Backend.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using Models;
    using Domain;

    [Authorize(Roles = "Admin")]
    public class StatusMatchesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: StatusMatches
        public async Task<ActionResult> Index()
        {
            return View(await db.StatusMatches.ToListAsync());
        }

        // GET: StatusMatches/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusMatch statusMatch = await db.StatusMatches.FindAsync(id);
            if (statusMatch == null)
            {
                return HttpNotFound();
            }
            return View(statusMatch);
        }

        // GET: StatusMatches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusMatches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StatusMatchId,Name")] StatusMatch statusMatch)
        {
            if (ModelState.IsValid)
            {
                db.StatusMatches.Add(statusMatch);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(statusMatch);
        }

        // GET: StatusMatches/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusMatch statusMatch = await db.StatusMatches.FindAsync(id);
            if (statusMatch == null)
            {
                return HttpNotFound();
            }
            return View(statusMatch);
        }

        // POST: StatusMatches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StatusMatchId,Name")] StatusMatch statusMatch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusMatch).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(statusMatch);
        }

        // GET: StatusMatches/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusMatch statusMatch = await db.StatusMatches.FindAsync(id);
            if (statusMatch == null)
            {
                return HttpNotFound();
            }
            return View(statusMatch);
        }

        // POST: StatusMatches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StatusMatch statusMatch = await db.StatusMatches.FindAsync(id);
            db.StatusMatches.Remove(statusMatch);
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
