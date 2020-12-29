using System.Collections.Generic;
using LifeManager.Server.Model.Domain;
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

        // TODO: Should only select all tasks for a given user
        [HttpGet("GetAll")]
        public IEnumerable<Principle> GetAll() {
            return _principleService.GetAll();
        }

        [Route("GetOne/{id}")]
        public Principle GetOne(long id) {
            return _principleService.GetById(id);
        }
        
        [HttpPost("Create")]
        public Response Post(Principle principle) {
            _principleService.Create(principle);

            return new Response {Body = "success"};
        }
        
        [HttpPost("Update")]
        public Response Update(Principle principle) {
            _principleService.Update(principle);

            return new Response {Body = "success"};
        }
        
        [HttpGet("Remove/{id}")]
        public Response Remove(long id) {
            _principleService.Remove(id);

            return new Response {Body = "success"};
        }
    }
}