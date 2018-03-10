namespace Lands.Backend.Models
{
    using Domain;

    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Lands.Domain.User> Users { get; set; }

        public System.Data.Entity.DbSet<Lands.Domain.UserType> UserTypes { get; set; }
    }
}