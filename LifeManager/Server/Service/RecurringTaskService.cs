using System;
using System.Collections.Generic;
using System.Linq;
using LifeManager.Server.Database;
using LifeManager.Server.Database.Entities;
using LifeManager.Server.Domain;
using LifeManager.Server.Domain.Mapper;

namespace LifeManager.Server.Service {
    public class RecurringTaskService : IRecurringTaskService {
        private readonly ILifeManagerRepository _lifeManagerRepository;
        
        private readonly RecurringTaskMapper _recurringTaskMapper = new RecurringTaskMapper();
        
        public RecurringTaskService(ILifeManagerRepository lifeManagerRepository) {
            _lifeManagerRepository = lifeManagerRepository;
        }

        // TODO: Should be GetAllForUser(userId)
        public IEnumerable<RecurringTask> GetAll() {
            List<RecurringTaskEntity> entities = _lifeManagerRepository.LoadRecurringTasks();

            return entities.Select(entity => _recurringTaskMapper.ToDomain(entity));
        }

        public RecurringTask GetById(long id) {
            RecurringTaskEntity entity = _lifeManagerRepository.LoadRecurringTask(id);

            if (entity == null) {
                return null;
            }

            return _recurringTaskMapper.ToDomain(entity);
        }

        public void Create(RecurringTask domain) {
            domain.DateTimeCreated = DateTime.Now;
            domain.DateTimeLastModified = DateTime.Now;

            _lifeManagerRepository.SaveRecurringTask(new RecurringTaskMapper().ToEntity(domain));
        }

        public void Update(RecurringTask domain) {
            if (_lifeManagerRepository.LoadRecurringTask(domain.Id) == null) {
                throw new InvalidOperationException(
                    $"Tried to update a RecurringTaskEntity (id = {domain.Id}), but the task doesn't exist. " +
                    $"This could indicate a misuse of save resources/service endpoints.");
            }

            domain.DateTimeLastModified = DateTime.Now;
            _lifeManagerRepository.SaveRecurringTask(new RecurringTaskMapper().ToEntity(domain));
        }

        public void Remove(long id) {
            RecurringTaskEntity recurringTaskEntity = _lifeManagerRepository.LoadRecurringTask(id);
            if (recurringTaskEntity == null) {
                throw new InvalidOperationException(
                    $"Tried to remove a RecurringTaskEntity (id = {id}), but the task doesn't exist. " +
                    $"This could indicate a misuse of save resources/service endpoints.");
            }

            _lifeManagerRepository.RemoveRecurringTask(recurringTaskEntity);
        }
    }
}