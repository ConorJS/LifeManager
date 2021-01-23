using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper.Implementation {
    public class ToDoTaskMapper : IToDoTaskMapper {
        private readonly ITaskMapper _taskMapper;

        public ToDoTaskMapper(ITaskMapper taskMapper) {
            _taskMapper = taskMapper;
        }

        public ToDoTask ToDomain(ToDoTaskEntity entity) {
            ToDoTask domain = new ToDoTask {
                Status = entity.Status,
                Priority = entity.Priority
            };

            _taskMapper.ToDomain(entity, domain);

            return domain;
        }

        public ToDoTaskEntity ToEntity(ToDoTask domain) {
            ToDoTaskEntity entity = new ToDoTaskEntity {
                Status = domain.Status,
                Priority = domain.Priority
            };

            _taskMapper.ToEntity(domain, entity);

            return entity;
        }
    }
}