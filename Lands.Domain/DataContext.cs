namespace Lands.Domain
{
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        #region Properties
        public DbSet<User> Users { get; set; }

        public DbSet<UserType> UserTypes { get; set; }
        #endregion

        #region Constructors
        public DataContext() : base("DefaultConnection")
        {
        }

        #endregion
    }
}