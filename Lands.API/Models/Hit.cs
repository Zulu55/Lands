namespace Lands.API.Models
{
    using Lands.Domain;

    public class Hit
    {
        public int PredictionId { get; set; }

        public int BoardId { get; set; }

        public int MatchId { get; set; }

        public int LocalGoals { get; set; }

        public int VisitorGoals { get; set; }

        public int UserId { get; set; }

        public int Points { get; set; }

        public MatchResponse Match { get; set; }
    }
}