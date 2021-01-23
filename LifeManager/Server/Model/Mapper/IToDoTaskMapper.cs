using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper {
    public interface IToDoTaskMapper {
        public ToDoTask ToDomain(ToDoTaskEntity entity);

        public ToDoTaskEntity ToEntity(ToDoTask domain);
    }
}