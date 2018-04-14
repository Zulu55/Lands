using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lands.Domain
{
    public class Match
    {
        [Key]
        public int MatchId { get; set; }

        public int GroupId { get; set; }

        [Display(Name = "Local")]
        public int LocalId { get; set; }

        [Display(Name = "Visitor")]
        public int VisitorId { get; set; }

        public int StatusMatchId { get; set; }

        [Display(Name = "Local goals")]
        public int? LocalGoals { get; set; }

        [Display(Name = "Visitor goals")]
        public int? VisitorGoals { get; set; }

        [Display(Name = "Date time")]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        [JsonIgnore]
        public virtual Group Group { get; set; }

        [JsonIgnore]
        public virtual Team Local { get; set; }

        [JsonIgnore]
        public virtual Team Visitor { get; set; }

        [JsonIgnore]
        public virtual StatusMatch StatusMatch { get; set; }

        [JsonIgnore]
        public virtual ICollection<Prediction> Predictions { get; set; }
    }
}
