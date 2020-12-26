using System;
using System.Collections.Generic;
using System.Linq;
using LifeManager.Server.Database;
using LifeManager.Server.Database.Entities;
using LifeManager.Server.Domain;
using LifeManager.Server.Domain.Mapper;

namespace LifeManager.Server.Service {
    public class ToDoTaskService : IToDoTaskService {
        private readonly ILifeManagerRepository _lifeManagerRepository;

        private readonly ToDoTaskMapper _toDoTaskMapper = new ToDoTaskMapper();

        public ToDoTaskService(ILifeManagerRepository lifeManagerRepository) {
            _lifeManagerRepository = lifeManagerRepository;
        }

        // TODO: Should be GetAllForUser(userId)
        public IEnumerable<ToDoTask> GetAll() {
            List<ToDoTaskEntity> taskEntities = _lifeManagerRepository.LoadToDoTasks();

            return taskEntities.Select(taskEntity => _toDoTaskMapper.ToDomain(taskEntity));
        }

        public ToDoTask GetById(long id) {
            ToDoTaskEntity entity = _lifeManagerRepository.LoadToDoTask(id);

            if (entity == null) {
                return null;
            }

            return _toDoTaskMapper.ToDomain(entity);
        }

        public void Create(ToDoTask domain) {
            _lifeManagerRepository.SaveToDoTask(new ToDoTaskMapper().ToEntity(domain));
        }

        public void Update(ToDoTask domain) {
            if (_lifeManagerRepository.LoadToDoTask(domain.Id) == null) {
                throw new InvalidOperationException(
                    "Tried to update a ToDoTaskEntity, but the task doesn't exist. This could indicate a misuse of save resources/service endpoints.");
            }

            _lifeManagerRepository.SaveToDoTask(new ToDoTaskMapper().ToEntity(domain));
        }
    }
}