namespace Lands.Domain
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class GroupTeam
    {
        [Key]
        public int GroupTeamId { get; set; }

        [Index("GroupTeam_GroupId_TeamId_Index", IsUnique = true, Order = 1)]
        public int GroupId { get; set; }

        [Index("GroupTeam_GroupId_TeamId_Index", IsUnique = true, Order = 2)]
        public int TeamId { get; set; }

        [JsonIgnore]
        public virtual Group Group { get; set; }

        [JsonIgnore]
        public virtual Team Team { get; set; }
    }
}