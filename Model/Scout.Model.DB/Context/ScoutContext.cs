using Microsoft.EntityFrameworkCore;

namespace Scout.Model.DB
{
    public class ScoutContext : DbContext
    {
        public ScoutContext(DbContextOptions<ScoutContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>();
            modelBuilder.Entity<Team>();
            modelBuilder.Entity<PlayerBattingStatistics>();
            modelBuilder.Entity<PlayerPitchingStatistics>();
            modelBuilder.Entity<League>();
            modelBuilder.Entity<Franchise>();
        }

        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<PlayerPitchingStatistics> PlayerPitchingStatisticsSet { get; set; }
        public virtual DbSet<PlayerBattingStatistics> PlayerBattingStatisticsSet { get; set; }
        public virtual DbSet<League> Leagues { get; set; }
        public virtual DbSet<Franchise> Franchises { get; set; }
    }
}
