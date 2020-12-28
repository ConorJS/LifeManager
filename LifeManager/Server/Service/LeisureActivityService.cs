using System;
using LifeManager.Server.Database;
using LifeManager.Server.Database.Entities;
using LifeManager.Server.Domain;
using LifeManager.Server.Domain.Mapper;

namespace LifeManager.Server.Service {
    public class LeisureActivityService : ILeisureActivityService {
        private readonly ILifeManagerRepository _lifeManagerRepository;
        
        public LeisureActivityService(ILifeManagerRepository lifeManagerRepository) {
            _lifeManagerRepository = lifeManagerRepository;
        }

        public LeisureActivity GetById(long id) {
            LeisureActivityEntity entity = _lifeManagerRepository.LoadLeisureActivity(id);

            if (entity == null) {
                return null;
            }

            return new LeisureActivityMapper().ToDomain(entity);
        }

        public void Create(LeisureActivity domain) {
            domain.DateTimeCreated = DateTime.Now;
            domain.DateTimeLastModified = DateTime.Now;
            
            _lifeManagerRepository.SaveLeisureActivity(new LeisureActivityMapper().ToEntity(domain));
        }
    }
}