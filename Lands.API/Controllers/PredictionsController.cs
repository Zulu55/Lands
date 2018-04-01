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

    //[Authorize]
    [RoutePrefix("api/Predictions")]
    public class PredictionsController : ApiController
    {
        private DataContext db = new DataContext();

        public IQueryable<Prediction> GetPredictions()
        {
            return db.Predictions;
        }

        [Route("GetHits/{userId}")]
        public async Task<IHttpActionResult> GetHits(int userId)
        {
            var hits = new List<Hit>();
            var predictions = await db.Predictions.
                Where(p => p.UserId == userId && p.Points != null).
                ToListAsync();
            foreach (var prediction in predictions)
            {
                hits.Add(new Hit
                {
                    BoardId = prediction.BoardId,
                    LocalGoals = prediction.LocalGoals,
                    Match =  this.ToMatchResponse(prediction.Match),
                    MatchId = prediction.MatchId,
                    Points = prediction.Points.Value,
                    PredictionId = prediction.PredictionId,
                    UserId = prediction.UserId,
                    VisitorGoals = prediction.VisitorGoals,
                });
            }

            return Ok(hits);
        }

        [Route("GetRanking")]
        public async Task<IHttpActionResult> GetRanking()
        {
            var responses = new List<Ranking>();
            var predictions = await db.Predictions.Where(p => p.Points != null).ToListAsync();

            var preRankings =  predictions.Select(p => new PreRanking
            {
                BoardId = p.BoardId,
                UserId = p.UserId,
            }).ToList();

            preRankings = preRankings.OrderBy(p => p.UserId).ThenBy(p => p.BoardId).ToList();

            int i = 0;
            while (i < preRankings.Count)
            {
                var preUserId = preRankings[i].UserId;
                var preBoard = preRankings[i].BoardId;
                while (i < preRankings.Count &&
                       preUserId == preRankings[i].UserId && 
                       preBoard == preRankings[i].BoardId)
                {
                    i++;
                }

                var user = await db.Users.FindAsync(preUserId);
                var board = await db.Boards.FindAsync(preBoard);
                var points = predictions.
                    Where(p => p.UserId == preUserId && p.BoardId == preBoard).
                    Sum(p => p.Points);
                responses.Add(new Ranking
                {
                    Board = board,
                    Points = points.Value,
                    User = user,
                });

            }

            i = 1;
            foreach (var response in responses.OrderByDescending(p => p.Points))
            {
                response.RankingId = i;
                i++;
            }

            return Ok(responses.OrderBy(r => r.RankingId));
        }

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

        [ResponseType(typeof(Prediction))]
        public async Task<IHttpActionResult> PostPrediction(Prediction prediction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var match = await db.Matches.FindAsync(prediction.MatchId);
            if (match == null)
            {
                return BadRequest("Match no found.");
            }

            if (match.StatusMatchId != 1)
            {
                return BadRequest("No prediction available for this match.");
            }

            if (match.DateTime <= DateTime.Now)
            {
                return BadRequest("It's too late to make a prediction for this match.");
            }

            var oldPrediction = await db.Predictions.FindAsync(prediction.PredictionId);
            if (oldPrediction == null)
            {
                db.Predictions.Add(prediction);
            }
            else
            {
                oldPrediction.LocalGoals = prediction.LocalGoals;
                oldPrediction.VisitorGoals = prediction.VisitorGoals;
                db.Entry(oldPrediction).State = EntityState.Modified;
            }

            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = prediction.PredictionId }, prediction);
        }

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