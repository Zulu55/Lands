namespace Lands.Domain
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Board
    {
        [Key]
        public int BoardId { get; set; }

        public int BoardStatusId { get; set; }

        public int UserId { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        [NotMapped]
        public byte[] ImageArray { get; set; }

        [Display(Name = "Image")]
        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                {
                    return "noimage_board";
                }

                return string.Format(
                    "http://landsapi1.azurewebsites.net{0}",
                    ImagePath.Substring(1));
            }
        }

        [JsonIgnore]
        public virtual BoardStatus BoardStatus { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual ICollection<Prediction> Predictions { get; set; }
    }
}
