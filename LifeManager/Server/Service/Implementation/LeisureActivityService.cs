using System.Collections.Generic;
using System.Linq;
using LifeManager.Server.Database;
using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;
using LifeManager.Server.Model.Mapper;
using LifeManager.Server.Service.Implementation.Tool;

namespace LifeManager.Server.Service.Implementation {
    public class LeisureActivityService : ILeisureActivityService {
        //== attributes =============================================================================================================================
        
        private readonly ILifeManagerRepository _lifeManagerRepository;

        private readonly IModelServiceTools _modelServiceTools;
        
        private readonly ILeisureActivityMapper _leisureActivityMapper;
        
        //== init ===================================================================================================================================
        
        public LeisureActivityService(ILifeManagerRepository lifeManagerRepository, IModelServiceTools modelServiceTools, 
            ILeisureActivityMapper leisureActivityMapper) {
            
            _lifeManagerRepository = lifeManagerRepository;
            _modelServiceTools = modelServiceTools;
            _leisureActivityMapper = leisureActivityMapper;
        }
        
        //== methods ================================================================================================================================

        public IEnumerable<LeisureActivity> GetAll() {
            return _modelServiceTools.AllEntitiesForLoggedInUser<LeisureActivityEntity>().Select(entity => _leisureActivityMapper.ToDomain(entity));
        }

        public LeisureActivity GetById(long id) {
            LeisureActivityEntity entity = _lifeManagerRepository.LoadEntity<LeisureActivityEntity>(id);

            if (entity == null) {
                return null;
            }

            return _leisureActivityMapper.ToDomain(entity);
        }

        public void Create(LeisureActivity domain) {
            _modelServiceTools.InitialiseNewItem(domain);

            _lifeManagerRepository.SaveEntity(_leisureActivityMapper.ToEntity(domain));
        }

        public void Update(LeisureActivity domain) {
            _modelServiceTools.UpdateProcessing<LeisureActivityEntity>(domain);
            
            _lifeManagerRepository.SaveEntity(_leisureActivityMapper.ToEntity(domain));
        }

        public void Remove(long id) {
            _modelServiceTools.RemoveEntity<LeisureActivityEntity>(id);
        }
    }
}