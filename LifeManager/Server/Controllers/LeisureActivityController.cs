using System.Collections.Generic;
using LifeManager.Server.Model.Domain;
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

        // TODO: Should only select all tasks for a given user
        [HttpGet("GetAll")]
        public IEnumerable<LeisureActivity> GetAll() {
            return _leisureActivityService.GetAll();
        }

        [Route("GetOne/{id}")]
        public LeisureActivity GetOne(long id) {
            return _leisureActivityService.GetById(id);
        }
        
        [HttpPost("Create")]
        public Response Post(LeisureActivity leisureActivity) {
            _leisureActivityService.Create(leisureActivity);

            return new Response {Body = "success"};
        }
        
        [HttpPost("Update")]
        public Response Update(LeisureActivity leisureActivity) {
            _leisureActivityService.Update(leisureActivity);

            return new Response {Body = "success"};
        }
        
        [HttpGet("Remove/{id}")]
        public Response Remove(long id) {
            _leisureActivityService.Remove(id);

            return new Response {Body = "success"};
        }
    }
}