using LifeManager.Server.Domain;

namespace LifeManager.Server.Service {
    public interface IToDoTaskService {
        public ToDoTask GetById(long id);

        public void Create(ToDoTask domain);
    }
}