using LifeManager.Server.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace LifeManager.Server.Database.Implementation {
    public class LifeManagerDatabaseContext : DbContext {
        //== configuration ==========================================================================================================================

        public LifeManagerDatabaseContext(DbContextOptions<LifeManagerDatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema("lifemanager");
        }

        //== entity definitions =====================================================================================================================

        public DbSet<DummyDataEntity> Dummy { get; set; }

        public DbSet<AppointmentEntity> Appointment { get; set; }

        public DbSet<ChoreEntity> Chore { get; set; }

        public DbSet<LeisureActivityEntity> LeisureActivity { get; set; }

        public DbSet<RecurringTaskEntity> RecurringTask { get; set; }

        public DbSet<PrincipleEntity> Principle { get; set; }

        public DbSet<ToDoTaskEntity> ToDoTask { get; set; }
    }
}