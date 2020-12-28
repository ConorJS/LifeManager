using System;
using System.Collections.Generic;
using System.Linq;
using LifeManager.Server.Database;
using LifeManager.Server.Database.Entities;
using LifeManager.Server.Domain;
using LifeManager.Server.Domain.Mapper;

namespace LifeManager.Server.Service {
    public class PrincipleService : IPrincipleService {
        private readonly ILifeManagerRepository _lifeManagerRepository;
        
        private readonly PrincipleMapper _principleMapper = new PrincipleMapper();
        
        public PrincipleService(ILifeManagerRepository lifeManagerRepository) {
            _lifeManagerRepository = lifeManagerRepository;
        }

        // TODO: Should be GetAllForUser(userId)
        public IEnumerable<Principle> GetAll() {
            List<PrincipleEntity> entities = _lifeManagerRepository.LoadPrinciples();

            return entities.Select(entity => _principleMapper.ToDomain(entity));
        }

        public Principle GetById(long id) {
            PrincipleEntity entity = _lifeManagerRepository.LoadPrinciple(id);

            if (entity == null) {
                return null;
            }

            return _principleMapper.ToDomain(entity);
        }

        public void Create(Principle domain) {
            domain.DateTimeCreated = DateTime.Now;
            domain.DateTimeLastModified = DateTime.Now;

            _lifeManagerRepository.SavePrinciple(_principleMapper.ToEntity(domain));
        }

        public void Update(Principle domain) {
            if (_lifeManagerRepository.LoadPrinciple(domain.Id) == null) {
                throw new InvalidOperationException(
                    $"Tried to update a PrincipleEntity (id = {domain.Id}), but the task doesn't exist. " +
                    $"This could indicate a misuse of save resources/service endpoints.");
            }

            domain.DateTimeLastModified = DateTime.Now;
            _lifeManagerRepository.SavePrinciple(_principleMapper.ToEntity(domain));
        }

        public void Remove(long id) {
            PrincipleEntity principleEntity = _lifeManagerRepository.LoadPrinciple(id);
            if (principleEntity == null) {
                throw new InvalidOperationException(
                    $"Tried to remove a PrincipleEntity (id = {id}), but the task doesn't exist. " +
                    $"This could indicate a misuse of save resources/service endpoints.");
            }

            _lifeManagerRepository.RemovePrinciple(principleEntity);
        }
    }
}