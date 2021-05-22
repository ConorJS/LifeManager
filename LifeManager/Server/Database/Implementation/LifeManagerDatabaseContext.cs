using LifeManager.Server.Model.Entity;
using LifeManager.Server.User;
using LifeManager.Server.User.Configuration;
using Microsoft.EntityFrameworkCore;

namespace LifeManager.Server.Database.Implementation {
    public class LifeManagerDatabaseContext : DbContext {
        //== configuration ==========================================================================================================================

        public LifeManagerDatabaseContext(DbContextOptions<LifeManagerDatabaseContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            //optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema("lifemanager");

            modelBuilder
                .Entity<ToDoTaskEntity>()
                .Property(e => e.Status)
                .HasConversion<string>();

            modelBuilder
                .Entity<ColumnSortOrderEntity>()
                .HasKey(e => new {e.UserConfigurationId, e.TableName, e.ColumnName});
        }

        //== entity definitions =====================================================================================================================

        public DbSet<UserEntity> User { get; set; }

        public DbSet<UserConfigurationEntity> UserConfiguration { get; set; }

        public DbSet<AppointmentEntity> Appointment { get; set; }

        public DbSet<ChoreEntity> Chore { get; set; }

        public DbSet<LeisureActivityEntity> LeisureActivity { get; set; }

        public DbSet<RecurringTaskEntity> RecurringTask { get; set; }

        public DbSet<PrincipleEntity> Principle { get; set; }

        public DbSet<ToDoTaskEntity> ToDoTask { get; set; }
    }
}