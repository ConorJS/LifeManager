using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LifeManager.Server.User {
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase {
        private readonly ILogger<UserController> _logger;

        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService) {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("GetLoggedInUser")]
        public User GetLoggedInUser() {
            // TODO: Implement a UserController (REST endpoint), user creation, and a proper user system.
            return Get(1);
        }

        public User Get(long id) {
            return _userService.GetUser(id);
        }
    }
}