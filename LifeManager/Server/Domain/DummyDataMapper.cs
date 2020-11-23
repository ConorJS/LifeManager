using LifeManager.Server.Database.Entities;

namespace LifeManager.Server.Domain {
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