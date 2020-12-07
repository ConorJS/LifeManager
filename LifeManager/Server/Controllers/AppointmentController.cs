using LifeManager.Server.Domain;
using LifeManager.Server.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LifeManager.Server.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase {
        private readonly ILogger<AppointmentController> _logger;

        private readonly IAppointmentService _AppointmentService;

        public AppointmentController(ILogger<AppointmentController> logger, IAppointmentService AppointmentService) {
            _logger = logger;
            _AppointmentService = AppointmentService;
        }

        [HttpGet]
        public Appointment Get() {
            return _AppointmentService.GetById(1L);
        }

        [HttpPost]
        public Response Post(Appointment appointment) {
            _AppointmentService.Create(appointment);

            return new Response {Body = "success"};
        }
    }
}