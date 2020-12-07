using LifeManager.Server.Database.Entities;

namespace LifeManager.Server.Domain.Mapper {
    public class RecurringTaskMapper {
        public RecurringTask ToDomain(RecurringTaskEntity entity) {
            RecurringTask domain = new RecurringTask { };

            new TaskMapper().ToDomain(entity, domain);

            return domain;
        }

        public RecurringTaskEntity ToEntity(RecurringTask domain) {
            RecurringTaskEntity entity = new RecurringTaskEntity { };

            new TaskMapper().ToEntity(domain, entity);

            return entity;
        }
    }
}