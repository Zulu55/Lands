namespace Lands.API.Models
{
    public class PredictionResponse
    {
        public int PredictionId { get; set; }

        public int BoardId { get; set; }

        public int MatchId { get; set; }

        public int? LocalGoals { get; set; }

        public int? VisitorGoals { get; set; }

        public int UserId { get; set; }

        public MatchResponse Match { get; set; }
    }
}