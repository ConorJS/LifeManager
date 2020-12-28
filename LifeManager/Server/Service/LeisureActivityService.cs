using System;
using System.Collections.Generic;
using System.Linq;
using LifeManager.Server.Database;
using LifeManager.Server.Database.Entities;
using LifeManager.Server.Domain;
using LifeManager.Server.Domain.Mapper;

namespace LifeManager.Server.Service {
    public class LeisureActivityService : ILeisureActivityService {
        private readonly ILifeManagerRepository _lifeManagerRepository;
        
        private readonly LeisureActivityMapper _leisureActivityMapper = new LeisureActivityMapper();
        
        public LeisureActivityService(ILifeManagerRepository lifeManagerRepository) {
            _lifeManagerRepository = lifeManagerRepository;
        }

        // TODO: Should be GetAllForUser(userId)
        public IEnumerable<LeisureActivity> GetAll() {
            List<LeisureActivityEntity> entities = _lifeManagerRepository.LoadLeisureActivities();

            return entities.Select(entity => _leisureActivityMapper.ToDomain(entity));
        }

        public LeisureActivity GetById(long id) {
            LeisureActivityEntity entity = _lifeManagerRepository.LoadLeisureActivity(id);

            if (entity == null) {
                return null;
            }

            return _leisureActivityMapper.ToDomain(entity);
        }

        public void Create(LeisureActivity domain) {
            domain.DateTimeCreated = DateTime.Now;
            domain.DateTimeLastModified = DateTime.Now;

            _lifeManagerRepository.SaveLeisureActivity(_leisureActivityMapper.ToEntity(domain));
        }

        public void Update(LeisureActivity domain) {
            if (_lifeManagerRepository.LoadLeisureActivity(domain.Id) == null) {
                throw new InvalidOperationException(
                    $"Tried to update a LeisureActivityEntity (id = {domain.Id}), but the task doesn't exist. " +
                    $"This could indicate a misuse of save resources/service endpoints.");
            }

            domain.DateTimeLastModified = DateTime.Now;
            _lifeManagerRepository.SaveLeisureActivity(_leisureActivityMapper.ToEntity(domain));
        }

        public void Remove(long id) {
            LeisureActivityEntity leisureActivityEntity = _lifeManagerRepository.LoadLeisureActivity(id);
            if (leisureActivityEntity == null) {
                throw new InvalidOperationException(
                    $"Tried to remove a LeisureActivityEntity (id = {id}), but the task doesn't exist. " +
                    $"This could indicate a misuse of save resources/service endpoints.");
            }

            _lifeManagerRepository.RemoveLeisureActivity(leisureActivityEntity);
        }
    }
}