namespace Lands.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Domain;
    using Models;

    [Authorize]
    [RoutePrefix("api/Predictions")]
    public class PredictionsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Predictions
        public IQueryable<Prediction> GetPredictions()
        {
            return db.Predictions;
        }

        // GET: api/Predictions/5
        [Route("GetPrediction/{userId}/{boardId}")]
        public async Task<IHttpActionResult> GetPrediction(int userId, int boardId)
        {
            var predictions = await db.Predictions.
                Where(p => p.UserId == userId && p.BoardId == boardId).
                ToListAsync();
            var matches = await db.Matches.
                Where(m => m.StatusMatchId == 1 && m.DateTime > DateTime.Now).
                ToListAsync();
            var responses = new List<PredictionResponse>();

            foreach (var match in matches)
            {
                var response = new PredictionResponse
                {
                    BoardId = boardId,
                    MatchId = match.MatchId,
                    Match = this.ToMatchResponse(match),
                    UserId = userId,
                };

                var prediction = predictions.Where(p => p.MatchId == match.MatchId).FirstOrDefault();
                if (prediction != null)
                {
                    response.LocalGoals = prediction.LocalGoals;
                    response.PredictionId = prediction.PredictionId;
                    response.VisitorGoals = prediction.VisitorGoals;
                }

                responses.Add(response);
            }

            return Ok(responses);
        }

        private MatchResponse ToMatchResponse(Match match)
        {
            return new MatchResponse
            {
                DateTime = match.DateTime,
                Group = match.Group,
                GroupId = match.GroupId,
                Local = match.Local,
                LocalGoals = match.LocalGoals,
                LocalId = match.LocalId,
                MatchId = match.MatchId,
                StatusMatch = match.StatusMatch,
                StatusMatchId = match.StatusMatchId,
                Visitor = match.Visitor,
                VisitorGoals = match.VisitorGoals,
                VisitorId = match.VisitorId,
            };
        }

        // PUT: api/Predictions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPrediction(int id, Prediction prediction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prediction.PredictionId)
            {
                return BadRequest();
            }

            db.Entry(prediction).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PredictionExists(id))
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

        // POST: api/Predictions
        [ResponseType(typeof(Prediction))]
        public async Task<IHttpActionResult> PostPrediction(Prediction prediction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Predictions.Add(prediction);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = prediction.PredictionId }, prediction);
        }

        // DELETE: api/Predictions/5
        [ResponseType(typeof(Prediction))]
        public async Task<IHttpActionResult> DeletePrediction(int id)
        {
            Prediction prediction = await db.Predictions.FindAsync(id);
            if (prediction == null)
            {
                return NotFound();
            }

            db.Predictions.Remove(prediction);
            await db.SaveChangesAsync();

            return Ok(prediction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PredictionExists(int id)
        {
            return db.Predictions.Count(e => e.PredictionId == id) > 0;
        }
    }
}