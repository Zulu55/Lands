namespace Lands.Backend.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using Domain;
    using Models;

    [Authorize(Roles = "Admin")]
    public class BoardStatusController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: BoardStatus
        public async Task<ActionResult> Index()
        {
            return View(await db.BoardStatus.ToListAsync());
        }

        // GET: BoardStatus/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardStatus boardStatus = await db.BoardStatus.FindAsync(id);
            if (boardStatus == null)
            {
                return HttpNotFound();
            }
            return View(boardStatus);
        }

        // GET: BoardStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BoardStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BoardStatusId,Name")] BoardStatus boardStatus)
        {
            if (ModelState.IsValid)
            {
                db.BoardStatus.Add(boardStatus);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(boardStatus);
        }

        // GET: BoardStatus/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardStatus boardStatus = await db.BoardStatus.FindAsync(id);
            if (boardStatus == null)
            {
                return HttpNotFound();
            }
            return View(boardStatus);
        }

        // POST: BoardStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BoardStatusId,Name")] BoardStatus boardStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boardStatus).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(boardStatus);
        }

        // GET: BoardStatus/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardStatus boardStatus = await db.BoardStatus.FindAsync(id);
            if (boardStatus == null)
            {
                return HttpNotFound();
            }
            return View(boardStatus);
        }

        // POST: BoardStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BoardStatus boardStatus = await db.BoardStatus.FindAsync(id);
            db.BoardStatus.Remove(boardStatus);
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
