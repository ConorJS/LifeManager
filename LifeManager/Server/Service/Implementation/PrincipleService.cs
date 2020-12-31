using System;
using System.Collections.Generic;
using System.Linq;
using LifeManager.Server.Database;
using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;
using LifeManager.Server.Model.Mapper;
using LifeManager.Server.Security;
using LifeManager.Server.Service.Implementation.Tool;

namespace LifeManager.Server.Service.Implementation {
    public class PrincipleService : IPrincipleService {
        //== attributes =============================================================================================================================
        
        private readonly ILifeManagerRepository _lifeManagerRepository;

        private readonly IModelServiceTools _modelServiceTools;
        
        private readonly PrincipleMapper _principleMapper = new PrincipleMapper();
        
        //== init ===================================================================================================================================
        
        public PrincipleService(ILifeManagerRepository lifeManagerRepository, IModelServiceTools modelServiceTools) {
            _lifeManagerRepository = lifeManagerRepository;
            _modelServiceTools = modelServiceTools;
        }
        
        //== methods ================================================================================================================================

        public IEnumerable<Principle> GetAll() {
            return _modelServiceTools.AllEntitiesForLoggedInUser<PrincipleEntity>().Select(entity => _principleMapper.ToDomain(entity));
        }

        public Principle GetById(long id) {
            PrincipleEntity entity = _lifeManagerRepository.LoadEntity<PrincipleEntity>(id);

            if (entity == null) {
                return null;
            }

            return _principleMapper.ToDomain(entity);
        }

        public void Create(Principle domain) {
            _modelServiceTools.InitialiseNewItem(domain);

            _lifeManagerRepository.SaveEntity(_principleMapper.ToEntity(domain));
        }

        public void Update(Principle domain) {
            _modelServiceTools.UpdateProcessing<PrincipleEntity>(domain);
            
            _lifeManagerRepository.SaveEntity(_principleMapper.ToEntity(domain));
        }

        public void Remove(long id) {
            _modelServiceTools.RemoveEntity<PrincipleEntity>(id);
        }
    }
}