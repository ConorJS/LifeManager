using System.Collections.Generic;
using LifeManager.Server.Model.Domain;
using LifeManager.Server.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LifeManager.Server.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase {
        private readonly ILogger<AppointmentController> _logger;

        private readonly IAppointmentService _appointmentService;

        public AppointmentController(ILogger<AppointmentController> logger, IAppointmentService AppointmentService) {
            _logger = logger;
            _appointmentService = AppointmentService;
        }

        // TODO: Should only select all tasks for a given user
        [HttpGet("GetAll")]
        public IEnumerable<Appointment> GetAll() {
            return _appointmentService.GetAll();
        }

        [Route("GetOne/{id}")]
        public Appointment GetOne(long id) {
            return _appointmentService.GetById(id);
        }
        
        [HttpPost("Create")]
        public Response Post(Appointment appointment) {
            _appointmentService.Create(appointment);

            return new Response {Body = "success"};
        }
        
        [HttpPost("Update")]
        public Response Update(Appointment appointment) {
            _appointmentService.Update(appointment);

            return new Response {Body = "success"};
        }
        
        [HttpGet("Remove/{id}")]
        public Response Remove(long id) {
            _appointmentService.Remove(id);

            return new Response {Body = "success"};
        }
    }
}