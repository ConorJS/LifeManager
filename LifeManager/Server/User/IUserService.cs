using LifeManager.Server.User.Configuration;

namespace LifeManager.Server.User {
    public interface IUserService {
        public User GetUser(long id);
        
        public User GetLoggedInUser();

        public void SaveTableViewConfiguration(UserConfiguration.ITableViewConfiguration tableViewConfiguration);
    }
}