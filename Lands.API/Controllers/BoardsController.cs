namespace Lands.API.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Domain;
    using Lands.API.Helpers;

    [Authorize]
    public class BoardsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Boards
        public IQueryable<Board> GetBoards()
        {
            return db.Boards;
        }

        // GET: api/Boards/5
        [ResponseType(typeof(Board))]
        public async Task<IHttpActionResult> GetBoard(int id)
        {
            var boards = await db.Boards.Where(b => b.UserId == id).ToListAsync();
            return Ok(boards);
        }

        // PUT: api/Boards/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBoard(int id, Board board)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != board.BoardId)
            {
                return BadRequest();
            }

            db.Entry(board).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Boards
        [ResponseType(typeof(Board))]
        public async Task<IHttpActionResult> PostBoard(Board board)
        {
            if (board.ImageArray != null && board.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(board.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = string.Format("{0}.jpg", guid);
                var folder = "~/Content/Boards";
                var fullPath = string.Format("{0}/{1}", folder, file);
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    board.ImagePath = fullPath;
                }
            }

            db.Boards.Add(board);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = board.BoardId }, board);
        }

        // DELETE: api/Boards/5
        [ResponseType(typeof(Board))]
        public async Task<IHttpActionResult> DeleteBoard(int id)
        {
            Board board = await db.Boards.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }

            db.Boards.Remove(board);
            await db.SaveChangesAsync();

            return Ok(board);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BoardExists(int id)
        {
            return db.Boards.Count(e => e.BoardId == id) > 0;
        }
    }
}