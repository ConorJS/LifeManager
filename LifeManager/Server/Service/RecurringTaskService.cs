using System;
using LifeManager.Server.Database;
using LifeManager.Server.Database.Entities;
using LifeManager.Server.Domain;
using LifeManager.Server.Domain.Mapper;

namespace LifeManager.Server.Service {
    public class RecurringTaskService : IRecurringTaskService {
        private readonly ILifeManagerRepository _lifeManagerRepository;
        
        public RecurringTaskService(ILifeManagerRepository lifeManagerRepository) {
            _lifeManagerRepository = lifeManagerRepository;
        }

        public RecurringTask GetById(long id) {
            RecurringTaskEntity entity = _lifeManagerRepository.LoadRecurringTask(id);

            if (entity == null) {
                return null;
            }

            return new RecurringTaskMapper().ToDomain(entity);
        }

        public void Create(RecurringTask domain) {
            domain.DateTimeCreated = DateTime.Now;
            domain.DateTimeLastModified = DateTime.Now;
            
            _lifeManagerRepository.SaveRecurringTask(new RecurringTaskMapper().ToEntity(domain));
        }
    }
}