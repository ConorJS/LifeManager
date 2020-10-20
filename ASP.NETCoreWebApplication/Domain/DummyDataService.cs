using ASP.NETCoreWebApplication.Entities;
using ASP.NETCoreWebApplication.Store;

namespace ASP.NETCoreWebApplication.Domain {
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
    }
}