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

        [HttpGet]
        public RecurringTask Get() {
            return _recurringTaskService.GetById(1L);
        }

        [HttpPost]
        public Response Post(RecurringTask recurringTask) {
            _recurringTaskService.Create(recurringTask);

            return new Response {Body = "success"};
        }
    }
}