using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Scout.Core;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Scout.Model.DB
{
    public class ScoutContext : DbContext, IScoutContext
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

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public new void Add<TEntity>(TEntity entity)
            where TEntity : class
        {
            base.Add<TEntity>(entity);
        }

        public new async Task AddAsync(object entity, CancellationToken cancellationToken=default(CancellationToken))
        {
            await base.AddAsync(entity, cancellationToken);
        }

        public new async Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
            where TEntity : class
        {
            EntityEntry<TEntity> entityEntry = await base.AddAsync<TEntity>(entity, cancellationToken);

            return entityEntry.Entity;
        }

        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<PlayerPitchingStatistics> PlayerPitchingStatisticsSet { get; set; }
        public virtual DbSet<PlayerBattingStatistics> PlayerBattingStatisticsSet { get; set; }
        public virtual DbSet<League> League { get; set; }
        public virtual DbSet<Franchise> Franchise { get; set; }
    }
}
