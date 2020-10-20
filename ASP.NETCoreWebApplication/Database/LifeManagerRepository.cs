using ASP.NETCoreWebApplication.Entities;

namespace ASP.NETCoreWebApplication.Store {
    public class LifeManagerRepository : ILifeManagerRepository {
        private readonly LifeManagerDatabaseContext _dbContext;

        public LifeManagerRepository(LifeManagerDatabaseContext lifeManagerDatabaseContext) {
            _dbContext = lifeManagerDatabaseContext;
        }

        public DummyDataEntity LoadDummyData(long id) {
            return _dbContext.dummy.Find(id);
        }

        public void Dispose() { }
    }
}