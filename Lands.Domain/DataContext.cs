namespace Lands.Domain
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class DataContext : DbContext
    {
        #region Properties
        public DbSet<User> Users { get; set; }

        public DbSet<UserType> UserTypes { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<GroupTeam> GroupTeams { get; set; }

        public DbSet<StatusMatch> StatusMatches { get; set; }

        public DbSet<Match> Matches { get; set; }
        #endregion

        #region Constructors
        public DataContext() : base("DefaultConnection")
        {
        }
        #endregion

        #region Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.Add(new MatchesMap());
        }
        #endregion
    }
}