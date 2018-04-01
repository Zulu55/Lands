namespace Lands.Domain
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [Required(ErrorMessage = "The field {0} is requiered.")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contains a maximum of {1} characters lenght.")]
        [Index("Team_Name_Index", IsUnique = true)]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        [Display(Name = "Image")]
        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                {
                    return "noimage_team";
                }

                return string.Format(
                    "https://russiabackend.azurewebsites.net{0}",
                    ImagePath.Substring(1));
            }
        }

        [JsonIgnore]
        public virtual ICollection<GroupTeam> GroupTeams { get; set; }

        [JsonIgnore]
        public virtual ICollection<Match> Locals { get; set; }

        [JsonIgnore]
        public virtual ICollection<Match> Visitors { get; set; }
    }
}
