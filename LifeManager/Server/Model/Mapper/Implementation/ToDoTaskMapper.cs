using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper.Implementation {
    public class ToDoTaskMapper : IToDoTaskMapper {
        private readonly IItemMapper _itemMapper;

        public ToDoTaskMapper(IItemMapper itemMapper) {
            _itemMapper = itemMapper;
        }

        public ToDoTask ToDomain(ToDoTaskEntity entity) {
            ToDoTask domain = new ToDoTask {
                Status = entity.Status,
                Priority = entity.Priority
            };

            _itemMapper.ToDomain(entity, domain);

            return domain;
        }

        public ToDoTaskEntity ToEntity(ToDoTask domain) {
            ToDoTaskEntity entity = new ToDoTaskEntity {
                Status = domain.Status,
                Priority = domain.Priority
            };

            _itemMapper.ToEntity(domain, entity);

            return entity;
        }
    }
}