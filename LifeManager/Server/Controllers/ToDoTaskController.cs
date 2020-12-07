using LifeManager.Server.Domain;
using LifeManager.Server.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LifeManager.Server.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ToDoTaskController : ControllerBase {
        private readonly ILogger<ToDoTaskController> _logger;

        private readonly IToDoTaskService _toDoTaskService;

        public ToDoTaskController(ILogger<ToDoTaskController> logger, IToDoTaskService toDoTaskService) {
            _logger = logger;
            _toDoTaskService = toDoTaskService;
        }

        [HttpGet]
        public ToDoTask Get() {
            return _toDoTaskService.GetById(1L);
        }

        [HttpPost]
        public Response Post(ToDoTask toDoTask) {
            _toDoTaskService.Create(toDoTask);

            return new Response {Body = "success"};
        }
    }
}