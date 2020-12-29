using System.Collections.Generic;
using LifeManager.Server.Model.Domain;
using LifeManager.Server.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LifeManager.Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoTaskController : ControllerBase {
        private readonly ILogger<ToDoTaskController> _logger;

        private readonly IToDoTaskService _toDoTaskService;

        public ToDoTaskController(ILogger<ToDoTaskController> logger, IToDoTaskService toDoTaskService) {
            _logger = logger;
            _toDoTaskService = toDoTaskService;
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
    }
}