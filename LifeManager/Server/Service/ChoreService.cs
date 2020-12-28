using System;
using System.Collections.Generic;
using System.Linq;
using LifeManager.Server.Database;
using LifeManager.Server.Database.Entities;
using LifeManager.Server.Domain;
using LifeManager.Server.Domain.Mapper;

namespace LifeManager.Server.Service {
    public class ChoreService : IChoreService {
        private readonly ILifeManagerRepository _lifeManagerRepository;
        
        private readonly ChoreMapper _choreMapper = new ChoreMapper();
        
        public ChoreService(ILifeManagerRepository lifeManagerRepository) {
            _lifeManagerRepository = lifeManagerRepository;
        }

        // TODO: Should be GetAllForUser(userId)
        public IEnumerable<Chore> GetAll() {
            List<ChoreEntity> entities = _lifeManagerRepository.LoadChores();

            return entities.Select(entity => _choreMapper.ToDomain(entity));
        }

        public Chore GetById(long id) {
            ChoreEntity entity = _lifeManagerRepository.LoadChore(id);

            if (entity == null) {
                return null;
            }

            return _choreMapper.ToDomain(entity);
        }

        public void Create(Chore domain) {
            domain.DateTimeCreated = DateTime.Now;
            domain.DateTimeLastModified = DateTime.Now;

            _lifeManagerRepository.SaveChore(_choreMapper.ToEntity(domain));
        }

        public void Update(Chore domain) {
            if (_lifeManagerRepository.LoadChore(domain.Id) == null) {
                throw new InvalidOperationException(
                    $"Tried to update a ChoreEntity (id = {domain.Id}), but the task doesn't exist. " +
                    $"This could indicate a misuse of save resources/service endpoints.");
            }

            domain.DateTimeLastModified = DateTime.Now;
            _lifeManagerRepository.SaveChore(_choreMapper.ToEntity(domain));
        }

        public void Remove(long id) {
            ChoreEntity choreEntity = _lifeManagerRepository.LoadChore(id);
            if (choreEntity == null) {
                throw new InvalidOperationException(
                    $"Tried to remove a ChoreEntity (id = {id}), but the task doesn't exist. " +
                    $"This could indicate a misuse of save resources/service endpoints.");
            }

            _lifeManagerRepository.RemoveChore(choreEntity);
        }
    }
}