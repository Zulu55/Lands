namespace Lands.Domain
{
    using System.ComponentModel.DataAnnotations.Schema;

    [NotMapped]
    public class UserView : User
    {
        public byte[] ImageArray { get; set; }

        public string Password { get; set; }
    }
}
