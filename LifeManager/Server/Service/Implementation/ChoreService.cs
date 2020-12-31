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
    public class ChoreService : IChoreService {
        //== attributes =============================================================================================================================
        
        private readonly ILifeManagerRepository _lifeManagerRepository;

        private readonly IModelServiceTools _modelServiceTools;
        
        private readonly ChoreMapper _choreMapper = new ChoreMapper();
        
        //== init ===================================================================================================================================
        
        public ChoreService(ILifeManagerRepository lifeManagerRepository, IModelServiceTools modelServiceTools) {
            _lifeManagerRepository = lifeManagerRepository;
            _modelServiceTools = modelServiceTools;
        }
        
        //== methods ================================================================================================================================

        public IEnumerable<Chore> GetAll() {
            return _modelServiceTools.AllEntitiesForLoggedInUser<ChoreEntity>().Select(entity => _choreMapper.ToDomain(entity));
        }

        public Chore GetById(long id) {
            ChoreEntity entity = _lifeManagerRepository.LoadEntity<ChoreEntity>(id);

            if (entity == null) {
                return null;
            }

            return _choreMapper.ToDomain(entity);
        }

        public void Create(Chore domain) {
            _modelServiceTools.InitialiseNewItem(domain);

            _lifeManagerRepository.SaveEntity(_choreMapper.ToEntity(domain));
        }

        public void Update(Chore domain) {
            _modelServiceTools.UpdateProcessing<ChoreEntity>(domain);
            
            _lifeManagerRepository.SaveEntity(_choreMapper.ToEntity(domain));
        }

        public void Remove(long id) {
            _modelServiceTools.RemoveEntity<ChoreEntity>(id);
        }
    }
}