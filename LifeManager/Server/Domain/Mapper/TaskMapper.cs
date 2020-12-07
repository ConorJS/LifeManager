using LifeManager.Server.Database.Entities;

namespace LifeManager.Server.Domain.Mapper {
    public class TaskMapper {
        public void ToDomain(ITaskEntity entity, ITask domain) {
            new ItemMapper().ToDomain(entity, domain);

            domain.RelativeSize = entity.RelativeSize;
        }

        public void ToEntity(ITask domain, ITaskEntity entity) {
            new ItemMapper().ToEntity(domain, entity);

            entity.RelativeSize = domain.RelativeSize;
        }
    }
}