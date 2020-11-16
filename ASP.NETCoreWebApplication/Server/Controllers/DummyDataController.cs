using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASP.NETCoreWebApplication.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class DummyDataController : ControllerBase {
        private readonly ILogger<DummyDataController> _logger;

        private readonly IDummyDataService _dummyDataService;

        public DummyDataController(ILogger<DummyDataController> logger, IDummyDataService dummyDataService) {
            _logger = logger;
            _dummyDataService = dummyDataService;
        }

        [HttpGet]
        public DummyData Get() {
            return _dummyDataService.GetDummyDataById(1L);
        }

        [HttpPost]
        public Response Post(DummyData dummyData) {
            _dummyDataService.CreateDummyData(dummyData);

            return new Response {Body = "success"};
        }
    }

    public class Response {
        public string Body { get; set; }
    }
}