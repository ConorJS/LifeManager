using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper {
    public class ToDoTaskMapper {
        public ToDoTask ToDomain(ToDoTaskEntity entity) {
            ToDoTask domain = new ToDoTask { };

            new TaskMapper().ToDomain(entity, domain);

            return domain;
        }

        public ToDoTaskEntity ToEntity(ToDoTask domain) {
            ToDoTaskEntity entity = new ToDoTaskEntity { };

            new TaskMapper().ToEntity(domain, entity);

            return entity;
        }
    }
}