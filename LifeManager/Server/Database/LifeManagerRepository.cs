using LifeManager.Server.Database.Entities;

namespace LifeManager.Server.Database {
    public class LifeManagerRepository : ILifeManagerRepository {
        private readonly LifeManagerDatabaseContext _dbContext;

        public LifeManagerRepository(LifeManagerDatabaseContext lifeManagerDatabaseContext) {
            _dbContext = lifeManagerDatabaseContext;
        }

        public DummyDataEntity LoadDummyData(long id) {
            return _dbContext.dummy.Find(id);
        }

        public void SaveDummyData(DummyDataEntity dummyDataEntity) {
            _dbContext.dummy.Add(dummyDataEntity);
            _dbContext.SaveChanges();
        }

        public void Dispose() { }
    }
}