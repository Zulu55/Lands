namespace Lands.Backend.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using System.Linq;
    using Models;
    using Domain;

    [Authorize(Roles = "Admin")]
    public class BoardsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        public async Task<ActionResult> Index()
        {
            var boards = db.Boards.Where(b => b.BoardStatusId == 1).Include(b => b.BoardStatus).Include(b => b.User);
            return View(await boards.ToListAsync());
        }

        public async Task<ActionResult> Index2()
        {
            var boards = db.Boards.Include(b => b.BoardStatus).Include(b => b.User);
            return View(await boards.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Board board = await db.Boards.FindAsync(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            return View(board);
        }

        public ActionResult Create()
        {
            ViewBag.BoardStatusId = new SelectList(db.BoardStatus, "BoardStatusId", "Name");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Board board)
        {
            if (ModelState.IsValid)
            {
                db.Boards.Add(board);
                await db.SaveChangesAsync();
                return RedirectToAction("Index2");
            }

            ViewBag.BoardStatusId = new SelectList(db.BoardStatus, "BoardStatusId", "Name", board.BoardStatusId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", board.UserId);
            return View(board);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var board = await db.Boards.FindAsync(id);

            if (board == null)
            {
                return HttpNotFound();
            }

            ViewBag.BoardStatusId = new SelectList(db.BoardStatus, "BoardStatusId", "Name", board.BoardStatusId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", board.UserId);

            return View(board);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Board board)
        {
            if (ModelState.IsValid)
            {
                db.Entry(board).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index2");
            }

            ViewBag.BoardStatusId = new SelectList(db.BoardStatus, "BoardStatusId", "Name", board.BoardStatusId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", board.UserId);
            return View(board);
        }

        // GET: Boards/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Board board = await db.Boards.FindAsync(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            return View(board);
        }

        // POST: Boards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Board board = await db.Boards.FindAsync(id);
            db.Boards.Remove(board);
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
