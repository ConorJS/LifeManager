using System;
using LifeManager.Server.Database;
using LifeManager.Server.Database.Entities;
using LifeManager.Server.Domain;
using LifeManager.Server.Domain.Mapper;

namespace LifeManager.Server.Service {
    public class PrincipleService : IPrincipleService {
        private readonly ILifeManagerRepository _lifeManagerRepository;
        
        public PrincipleService(ILifeManagerRepository lifeManagerRepository) {
            _lifeManagerRepository = lifeManagerRepository;
        }

        public Principle GetById(long id) {
            PrincipleEntity entity = _lifeManagerRepository.LoadPrinciple(id);

            if (entity == null) {
                return null;
            }

            return new PrincipleMapper().ToDomain(entity);
        }

        public void Create(Principle domain) {
            domain.DateTimeCreated = DateTime.Now;
            domain.DateTimeLastModified = DateTime.Now;
            
            _lifeManagerRepository.SavePrinciple(new PrincipleMapper().ToEntity(domain));
        }
    }
}