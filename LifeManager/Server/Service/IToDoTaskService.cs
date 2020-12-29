using System.Collections.Generic;
using LifeManager.Server.Model.Domain;

namespace LifeManager.Server.Service {
    public interface IToDoTaskService {
        public IEnumerable<ToDoTask> GetAll();
        
        public ToDoTask GetById(long id);

        public void Create(ToDoTask domain);
        
        public void Update(ToDoTask domain);
        
        public void Remove(long id);
    }
}