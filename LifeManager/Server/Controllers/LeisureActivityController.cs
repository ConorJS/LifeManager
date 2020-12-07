using LifeManager.Server.Domain;
using LifeManager.Server.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LifeManager.Server.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class LeisureActivityController : ControllerBase {
        private readonly ILogger<LeisureActivityController> _logger;

        private readonly ILeisureActivityService _leisureActivityService;

        public LeisureActivityController(ILogger<LeisureActivityController> logger, ILeisureActivityService leisureActivityService) {
            _logger = logger;
            _leisureActivityService = leisureActivityService;
        }

        [HttpGet]
        public LeisureActivity Get() {
            return _leisureActivityService.GetById(1L);
        }

        [HttpPost]
        public Response Post(LeisureActivity leisureActivity) {
            _leisureActivityService.Create(leisureActivity);

            return new Response {Body = "success"};
        }
    }
}