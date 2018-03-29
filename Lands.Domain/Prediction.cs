namespace Lands.Domain
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    public class Prediction
    {
        [Key]
        public int PredictionId { get; set; }

        public int BoardId { get; set; }

        public int MatchId { get; set; }

        [Display(Name = "Local goals")]
        public int LocalGoals { get; set; }

        [Display(Name = "Visitor goals")]
        public int VisitorGoals { get; set; }

        public int UserId { get; set; }

        public int? Points { get; set; }

        [JsonIgnore]
        public virtual Board Board { get; set; }

        [JsonIgnore]
        public virtual Match Match { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
