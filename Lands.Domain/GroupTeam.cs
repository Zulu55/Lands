namespace Lands.Domain
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    public class GroupTeam
    {
        [Key]
        public int GroupTeamId { get; set; }

        public int GroupId { get; set; }
        
        public int TeamId { get; set; }

        [JsonIgnore]
        public virtual Group Group { get; set; }

        [JsonIgnore]
        public virtual Team Team { get; set; }
    }
}
