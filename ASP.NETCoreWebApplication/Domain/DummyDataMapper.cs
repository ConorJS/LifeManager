using ASP.NETCoreWebApplication.Entities;

namespace ASP.NETCoreWebApplication {
    public class DummyDataMapper {
        public DummyData ToDomain(DummyDataEntity entity) {
            return new DummyData {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}