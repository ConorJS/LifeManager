using LifeManager.Server.Database;
using LifeManager.Server.User.Configuration;

namespace LifeManager.Server.User {
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

        // TODO: This probably won't be necessary
        public UserConfiguration GetUserConfiguration(long id) {
            // TODO: Implement a UserController (REST endpoint), user creation, and a proper user system.
            //UserConfigurationEntity entity = _lifeManagerRepository.LoadUserConfiguration(id);
            UserConfigurationEntity entity = _lifeManagerRepository.LoadUserConfiguration(1);

            return new UserConfigurationMapper().ToDomain(entity);
        }

        public void SaveUserConfiguration(UserConfiguration userConfiguration) {
            UserConfigurationEntity userConfigurationEntity = new UserConfigurationMapper().ToEntity(userConfiguration);
            
            _lifeManagerRepository.SaveUserConfiguration(userConfigurationEntity);
        }
    }
}