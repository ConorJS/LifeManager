using ASP.NETCoreWebApplication.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreWebApplication.Store {
    public class LifeManagerDatabaseContext : DbContext {
        public LifeManagerDatabaseContext(DbContextOptions<LifeManagerDatabaseContext> options) : base(options) { }

        public DbSet<DummyDataEntity> dummy { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema("lifemanager");
        }
    }
}