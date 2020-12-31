using LifeManager.Server.Database;

namespace LifeManager.Server.Security {
    public class UserService : IUserService {
        private readonly ILifeManagerRepository _lifeManagerRepository;
        
        private readonly UserMapper _userMapper = new UserMapper();

        public UserService(ILifeManagerRepository lifeManagerRepository) {
            _lifeManagerRepository = lifeManagerRepository;
        }

        public User GetUser(long id) {
            return _userMapper.ToDomain(_lifeManagerRepository.LoadUser(id));
        }

        public User GetLoggedInUser() {
            // TODO: Implement a UserController (REST endpoint), user creation, and a proper user system.
            return GetUser(1);
        }
    }
}