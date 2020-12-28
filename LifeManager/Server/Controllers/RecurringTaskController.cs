using System.Collections.Generic;
using LifeManager.Server.Domain;
using LifeManager.Server.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LifeManager.Server.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class RecurringTaskController : ControllerBase {
        private readonly ILogger<RecurringTaskController> _logger;

        private readonly IRecurringTaskService _recurringTaskService;

        public RecurringTaskController(ILogger<RecurringTaskController> logger, IRecurringTaskService recurringTaskService) {
            _logger = logger;
            _recurringTaskService = recurringTaskService;
        }

        // TODO: Should only select all tasks for a given user
        [HttpGet("GetAll")]
        public IEnumerable<RecurringTask> GetAll() {
            return _recurringTaskService.GetAll();
        }

        [Route("GetOne/{id}")]
        public RecurringTask GetOne(long id) {
            return _recurringTaskService.GetById(id);
        }
        
        [HttpPost("Create")]
        public Response Post(RecurringTask recurringTask) {
            _recurringTaskService.Create(recurringTask);

            return new Response {Body = "success"};
        }
        
        [HttpPost("Update")]
        public Response Update(RecurringTask recurringTask) {
            _recurringTaskService.Update(recurringTask);

            return new Response {Body = "success"};
        }
        
        [HttpGet("Remove/{id}")]
        public Response Remove(long id) {
            _recurringTaskService.Remove(id);

            return new Response {Body = "success"};
        }
    }
}