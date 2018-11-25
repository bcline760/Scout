using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Scout.Model.DB.Context
{
    internal class ScoutContext : DbContext, IScoutContext
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
            modelBuilder.Entity<PlayerModel>();
            modelBuilder.Entity<TeamModel>();
            modelBuilder.Entity<PlayerBattingStatisticsModel>();
            modelBuilder.Entity<PlayerPitchingStatisticsModel>();
            modelBuilder.Entity<LeagueModel>();
            modelBuilder.Entity<FranchiseModel>();
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

        public new async Task AddAsync(object entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await base.AddAsync(entity, cancellationToken);
        }

        public new async Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
            where TEntity : class
        {
            EntityEntry<TEntity> entityEntry = await base.AddAsync<TEntity>(entity, cancellationToken);

            return entityEntry.Entity;
        }

        public virtual DbSet<PlayerModel> Player { get; set; }
        public virtual DbSet<TeamModel> Team { get; set; }
        public virtual DbSet<PlayerPitchingStatisticsModel> PlayerPitchingStatistics { get; set; }
        public virtual DbSet<PlayerBattingStatisticsModel> PlayerBattingStatistics { get; set; }
        public virtual DbSet<PlayerFieldingStatisticsModel> PlayerFieldingStatistics { get; set; }
        public virtual DbSet<LeagueModel> League { get; set; }
        public virtual DbSet<FranchiseModel> Franchise { get; set; }
        public DbContext DbContext => this;
    }
}