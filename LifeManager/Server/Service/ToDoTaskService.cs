using LifeManager.Server.Database;
using LifeManager.Server.Database.Entities;
using LifeManager.Server.Domain;
using LifeManager.Server.Domain.Mapper;

namespace LifeManager.Server.Service {
    public class ToDoTaskService : IToDoTaskService {
        private readonly ILifeManagerRepository _lifeManagerRepository;
        
        public ToDoTaskService(ILifeManagerRepository lifeManagerRepository) {
            _lifeManagerRepository = lifeManagerRepository;
        }

        public ToDoTask GetById(long id) {
            ToDoTaskEntity entity = _lifeManagerRepository.LoadToDoTask(id);

            if (entity == null) {
                return null;
            }

            return new ToDoTaskMapper().ToDomain(entity);
        }

        public void Create(ToDoTask domain) {
            _lifeManagerRepository.SaveToDoTask(new ToDoTaskMapper().ToEntity(domain));
        }
    }
}