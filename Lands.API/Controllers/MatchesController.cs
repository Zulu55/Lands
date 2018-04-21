namespace Lands.API.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Domain;
    using Models;
    using System.Collections.Generic;

    [Authorize]
    public class MatchesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Matches
        public async Task<IHttpActionResult> GetMatches()
        {
            var responses = new List<MatchResponse>();
            var matches = await db.Matches.ToListAsync();

            foreach (var match in matches)
            {
                responses.Add(new MatchResponse
                {
                    DateTime = match.DateTime,
                    Group = match.Group,
                    GroupId = match.GroupId,
                    Local = match.Local,
                    LocalGoals = match.LocalGoals,
                    LocalId = match.LocalId,
                    MatchId = match.MatchId,
                    Predictions = match.Predictions.ToList(),
                    StatusMatch = match.StatusMatch,
                    StatusMatchId = match.StatusMatchId,
                    Visitor = match.Visitor,
                    VisitorGoals = match.VisitorGoals,
                    VisitorId = match.VisitorId,
                });
            }

            return Ok(responses);
        }

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