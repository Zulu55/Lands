namespace Lands.Backend.Models
{
    using Domain;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class TeamView : Team
    {
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}