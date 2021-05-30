using System.Collections.Generic;
using System.Linq;
using LifeManager.Server.Database;
using LifeManager.Server.Model;
using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;
using LifeManager.Server.Model.Mapper;
using LifeManager.Server.Service.Implementation.Tool;
using LifeManager.Server.User;

namespace LifeManager.Server.Service.Implementation {
    public class ToDoTaskService : IToDoTaskService {
        //== attributes =============================================================================================================================
        
        private readonly ILifeManagerRepository _lifeManagerRepository;

        private readonly IModelServiceTools _modelServiceTools;

        private readonly IToDoTaskMapper _toDoTaskMapper;

        private readonly IUserService _userService;
        
        //== init ===================================================================================================================================

        public ToDoTaskService(ILifeManagerRepository lifeManagerRepository, IModelServiceTools modelServiceTools, IToDoTaskMapper toDoTaskMapper, 
            IUserService userService) {
            _lifeManagerRepository = lifeManagerRepository;
            _modelServiceTools = modelServiceTools;
            _toDoTaskMapper = toDoTaskMapper;
            _userService = userService;
        }
        
        //== methods ================================================================================================================================

        public IEnumerable<ToDoTask> GetAll() {
            return _lifeManagerRepository.LoadAllToDoTasksForUser(_userService.GetLoggedInUser().Id)
                .Select(e => _toDoTaskMapper.ToDomain(e));
        }

        public ToDoTask GetById(long id) {
            ToDoTaskEntity entity = _lifeManagerRepository.LoadToDoTask(id);

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