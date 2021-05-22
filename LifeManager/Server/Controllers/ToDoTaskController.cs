using System.Collections.Generic;
using LifeManager.Server.Model.Domain;
using LifeManager.Server.Service;
using LifeManager.Server.User;
using LifeManager.Server.User.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LifeManager.Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoTaskController : ControllerBase {
        private readonly ILogger<ToDoTaskController> _logger;

        private readonly IToDoTaskService _toDoTaskService;
        
        private readonly IUserService _userService;

        public ToDoTaskController(ILogger<ToDoTaskController> logger, IToDoTaskService toDoTaskService, IUserService userService) {
            _logger = logger;
            _toDoTaskService = toDoTaskService;
            _userService = userService;
        }

        // TODO: Should only select all tasks for a given user
        [HttpGet("GetAll")]
        public IEnumerable<ToDoTask> GetAll() {
            return _toDoTaskService.GetAll();
        }

        [Route("GetOne/{id}")]
        public ToDoTask GetOne(long id) {
            return _toDoTaskService.GetById(id);
        }
        
        [HttpPost("Create")]
        public Response Post(ToDoTask toDoTask) {
            _toDoTaskService.Create(toDoTask);

            return new Response {Body = "success"};
        }
        
        [HttpPost("Update")]
        public Response Update(ToDoTask toDoTask) {
            _toDoTaskService.Update(toDoTask);

            return new Response {Body = "success"};
        }
        
        [HttpGet("Remove/{id}")]
        public Response Remove(long id) {
            _toDoTaskService.Remove(id);

            return new Response {Body = "success"};
        }

        [HttpPost("UpdateUserConfig")]
        public Response UpdateUserConfig(UserConfiguration.ToDoTaskTableViewConfiguration toDoTaskTableViewConfiguration) {
            _userService.SaveTableViewConfiguration(toDoTaskTableViewConfiguration);
            
            return new Response {Body = "success"};
        }
    }
}