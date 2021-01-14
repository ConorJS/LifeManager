using System.Collections.Generic;
using System.Linq;
using LifeManager.Server.Database;
using LifeManager.Server.Model;
using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;
using LifeManager.Server.Model.Mapper;
using LifeManager.Server.Service.Implementation.Tool;

namespace LifeManager.Server.Service.Implementation {
    public class ToDoTaskService : IToDoTaskService {
        //== attributes =============================================================================================================================
        
        private readonly ILifeManagerRepository _lifeManagerRepository;

        private readonly IModelServiceTools _modelServiceTools;

        private readonly ToDoTaskMapper _toDoTaskMapper = new ToDoTaskMapper();
        
        //== init ===================================================================================================================================

        public ToDoTaskService(ILifeManagerRepository lifeManagerRepository, IModelServiceTools modelServiceTools) {
            _lifeManagerRepository = lifeManagerRepository;
            _modelServiceTools = modelServiceTools;
        }
        
        //== methods ================================================================================================================================

        public IEnumerable<ToDoTask> GetAll() {
            return _modelServiceTools.AllEntitiesForLoggedInUser<ToDoTaskEntity>().Select(entity => _toDoTaskMapper.ToDomain(entity));
        }

        public ToDoTask GetById(long id) {
            ToDoTaskEntity entity = _lifeManagerRepository.LoadEntity<ToDoTaskEntity>(id);

            if (entity == null) {
                return null;
            }

            return _toDoTaskMapper.ToDomain(entity);
        }

        public void Create(ToDoTask domain) {
            _modelServiceTools.InitialiseNewItem(domain);
            
            domain.Status = ToDoTaskStatus.Ready;

            _lifeManagerRepository.SaveEntity(_toDoTaskMapper.ToEntity(domain));
        }

        public void Update(ToDoTask domain) {
            _modelServiceTools.UpdateProcessing<ToDoTaskEntity>(domain);
            
            _lifeManagerRepository.SaveEntity(_toDoTaskMapper.ToEntity(domain));
        }

        public void Remove(long id) {
            _modelServiceTools.RemoveEntity<ToDoTaskEntity>(id);
        }
    }
}