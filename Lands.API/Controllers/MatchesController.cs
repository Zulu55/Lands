namespace Lands.API.Controllers
{
    using Domain;
    using Models;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    [RoutePrefix("api/Matches")]
    public class MatchesController : ApiController
    {
        private DataContext db = new DataContext();

        [Route("GetClosedMatches")]
        public async Task<IHttpActionResult> GetClosedMatches()
        {
            var responses = new List<MatchResponse>();
            var matches = await db.Matches.Where(m => m.StatusMatchId != 1).ToListAsync();
            foreach (var match in matches)
            {
                var predictionResponses = new List<PredictionResponse>();

                foreach (var prediction in match.Predictions)
                {
                    predictionResponses.Add(new PredictionResponse
                    {
                        BoardId = prediction.BoardId,
                        LocalGoals = prediction.LocalGoals,
                        MatchId = prediction.MatchId,
                        Points = prediction.Points,
                        PredictionId = prediction.PredictionId,
                        User = prediction.User,
                        UserId = prediction.UserId,
                        VisitorGoals = prediction.VisitorGoals,
                    });
                }

                responses.Add(new MatchResponse
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
                    Predictions = predictionResponses,
                });
            }

            return Ok(responses);
        }

        private MatchResponse ToMatchResponse(Match match)
        {
            return new MatchResponse
            {
                // TODO: Here go
            };
        }

        [Authorize]
        public async Task<IHttpActionResult> GetMatches()
        {
            var resonse = new List<MatchResponse>();
            var marches = await db.Matches.ToListAsync();
            foreach (var match in marches.OrderBy(m => m.StatusMatchId).ThenBy(m => m.DateTime))
            {
                resonse.Add(new MatchResponse
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
                });
            }
            return Ok(resonse);
        }

        //[Authorize]
        //[ResponseType(typeof(Match))]
        //public async Task<IHttpActionResult> GetMatch(int id)
        //{
        //    Match match = await db.Matches.FindAsync(id);
        //    if (match == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(match);
        //}

        [Authorize]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMatch(int id, Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != match.MatchId)
            {
                return BadRequest();
            }

            db.Entry(match).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
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

        [Authorize]
        [ResponseType(typeof(Match))]
        public async Task<IHttpActionResult> PostMatch(Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Matches.Add(match);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = match.MatchId }, match);
        }

        //[ResponseType(typeof(Match))]
        //public async Task<IHttpActionResult> DeleteMatch(int id)
        //{
        //    Match match = await db.Matches.FindAsync(id);
        //    if (match == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Matches.Remove(match);
        //    await db.SaveChangesAsync();

        //    return Ok(match);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MatchExists(int id)
        {
            return db.Matches.Count(e => e.MatchId == id) > 0;
        }
    }
}