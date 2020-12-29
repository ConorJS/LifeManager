using System.Collections.Generic;
using LifeManager.Server.Model.Domain;
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

        // TODO: Should only select all tasks for a given user
        [HttpGet("GetAll")]
        public IEnumerable<Chore> GetAll() {
            return _choreService.GetAll();
        }

        [Route("GetOne/{id}")]
        public Chore GetOne(long id) {
            return _choreService.GetById(id);
        }
        
        [HttpPost("Create")]
        public Response Post(Chore chore) {
            _choreService.Create(chore);

            return new Response {Body = "success"};
        }
        
        [HttpPost("Update")]
        public Response Update(Chore chore) {
            _choreService.Update(chore);

            return new Response {Body = "success"};
        }
        
        [HttpGet("Remove/{id}")]
        public Response Remove(long id) {
            _choreService.Remove(id);

            return new Response {Body = "success"};
        }
    }
}