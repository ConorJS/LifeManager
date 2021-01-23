using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper.Implementation {
    public class RecurringTaskMapper : IRecurringTaskMapper {
        private readonly ITaskMapper _taskMapper;

        public RecurringTaskMapper(ITaskMapper taskMapper) {
            _taskMapper = taskMapper;
        }
        
        public RecurringTask ToDomain(RecurringTaskEntity entity) {
            RecurringTask domain = new RecurringTask { };

            _taskMapper.ToDomain(entity, domain);

            return domain;
        }

        public RecurringTaskEntity ToEntity(RecurringTask domain) {
            RecurringTaskEntity entity = new RecurringTaskEntity { };

            _taskMapper.ToEntity(domain, entity);

            return entity;
        }
    }
}