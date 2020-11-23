using LifeManager.Entities;

namespace LifeManager {
    public class DummyDataMapper {
        public DummyData ToDomain(DummyDataEntity entity) {
            return new DummyData {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public DummyDataEntity ToEntity(DummyData domain) {
            return new DummyDataEntity {
                Id = domain.Id,
                Name = domain.Name
            };
        }
    }
}