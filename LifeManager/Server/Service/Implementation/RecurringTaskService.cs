using System.Collections.Generic;
using System.Linq;
using LifeManager.Server.Database;
using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;
using LifeManager.Server.Model.Mapper;
using LifeManager.Server.Service.Implementation.Tool;

namespace LifeManager.Server.Service.Implementation {
    public class RecurringTaskService : IRecurringTaskService {
        //== attributes =============================================================================================================================
        
        private readonly ILifeManagerRepository _lifeManagerRepository;

        private readonly IModelServiceTools _modelServiceTools;
        
        private readonly IRecurringTaskMapper _recurringTaskMapper;
        
        //== init ===================================================================================================================================
        
        public RecurringTaskService(ILifeManagerRepository lifeManagerRepository, IModelServiceTools modelServiceTools, 
            IRecurringTaskMapper recurringTaskMapper) {
            
            _lifeManagerRepository = lifeManagerRepository;
            _modelServiceTools = modelServiceTools;
            _recurringTaskMapper = recurringTaskMapper;
        }
        
        //== methods ================================================================================================================================

        public IEnumerable<RecurringTask> GetAll() {
            return _modelServiceTools.AllEntitiesForLoggedInUser<RecurringTaskEntity>().Select(entity => _recurringTaskMapper.ToDomain(entity));
        }

        public RecurringTask GetById(long id) {
            RecurringTaskEntity entity = _lifeManagerRepository.LoadEntity<RecurringTaskEntity>(id);

            if (entity == null) {
                return null;
            }

            return _recurringTaskMapper.ToDomain(entity);
        }

        public void Create(RecurringTask domain) {
            _modelServiceTools.InitialiseNewItem(domain);

            _lifeManagerRepository.SaveEntity(_recurringTaskMapper.ToEntity(domain));
        }

        public void Update(RecurringTask domain) {
            _modelServiceTools.UpdateProcessing<RecurringTaskEntity>(domain);
            
            _lifeManagerRepository.SaveEntity(_recurringTaskMapper.ToEntity(domain));
        }

        public void Remove(long id) {
            _modelServiceTools.RemoveEntity<RecurringTaskEntity>(id);
        }
    }
}