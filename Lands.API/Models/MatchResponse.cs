namespace Lands.API.Models
{
    using System;
    using System.Collections.Generic;
    using Domain;

    public class MatchResponse
    {
        public int MatchId { get; set; }

        public int GroupId { get; set; }

        public int LocalId { get; set; }

        public int VisitorId { get; set; }

        public int StatusMatchId { get; set; }

        public int? LocalGoals { get; set; }

        public int? VisitorGoals { get; set; }

        public DateTime DateTime { get; set; }

        public Group Group { get; set; }

        public Team Local { get; set; }

        public Team Visitor { get; set; }

        public StatusMatch StatusMatch { get; set; }

        public List<Prediction> Predictions { get; set; }
    }
}