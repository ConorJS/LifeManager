using LifeManager.Server.Domain;
using LifeManager.Server.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LifeManager.Server.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class PrincipleController : ControllerBase {
        private readonly ILogger<PrincipleController> _logger;

        private readonly IPrincipleService _principleService;

        public PrincipleController(ILogger<PrincipleController> logger, IPrincipleService principleService) {
            _logger = logger;
            _principleService = principleService;
        }

        [HttpGet]
        public Principle Get() {
            return _principleService.GetById(1L);
        }

        [HttpPost]
        public Response Post(Principle principle) {
            _principleService.Create(principle);

            return new Response {Body = "success"};
        }
    }
}