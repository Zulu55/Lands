namespace Lands.Domain
{
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        #region Properties
        public DbSet<User> Users { get; set; }

        public DbSet<UserType> UserTypes { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<GroupTeam> GroupTeams { get; set; }
        #endregion

        #region Constructors
        public DataContext() : base("DefaultConnection")
        {
        }
        #endregion
    }
}