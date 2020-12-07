using LifeManager.Server.Domain;
using LifeManager.Server.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LifeManager.Server.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ChoreController : ControllerBase {
        private readonly ILogger<ChoreController> _logger;

        private readonly IChoreService _choreService;

        public ChoreController(ILogger<ChoreController> logger, IChoreService choreService) {
            _logger = logger;
            _choreService = choreService;
        }

        [HttpGet]
        public Chore Get() {
            return _choreService.GetById(1L);
        }

        [HttpPost]
        public Response Post(Chore Chore) {
            _choreService.Create(Chore);

            return new Response {Body = "success"};
        }
    }
}