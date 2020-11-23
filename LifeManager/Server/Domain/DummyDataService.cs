using LifeManager.Server.Database;
using LifeManager.Server.Database.Entities;

namespace LifeManager.Server.Domain {
    public class DummyDataService : IDummyDataService {
        private readonly ILifeManagerRepository _lifeManagerRepository;

        public DummyDataService(ILifeManagerRepository lifeManagerRepository) {
            _lifeManagerRepository = lifeManagerRepository;
        }

        public DummyData GetDummyDataById(long id) {
            DummyDataEntity entity = _lifeManagerRepository.LoadDummyData(id);

            if (entity == null) {
                return null;
            }

            return new DummyDataMapper().ToDomain(entity);
        }

        public void CreateDummyData(DummyData dummyData) {
            _lifeManagerRepository.SaveDummyData(new DummyDataMapper().ToEntity(dummyData));
        }
    }
}