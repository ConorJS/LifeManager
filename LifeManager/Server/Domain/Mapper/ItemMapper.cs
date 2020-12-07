using LifeManager.Server.Database.Entities;

namespace LifeManager.Server.Domain.Mapper {
    public class ItemMapper {
        public void ToDomain(IItemEntity entity, IItem domain) {
            domain.Id = entity.Id;
            domain.Name = entity.Name;
            domain.DateTimeCreated = entity.DateTimeCreated;
            domain.DateTimeLastModified = entity.DateTimeLastModified;
        }

        public void ToEntity(IItem domain, IItemEntity entity) {
            entity.Id = domain.Id;
            entity.Name = domain.Name;
            entity.DateTimeCreated = domain.DateTimeCreated;
            entity.DateTimeLastModified = domain.DateTimeLastModified;
        }
    }
}