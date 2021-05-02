namespace LifeManager.Server.User {
    public interface IUserService {
        public User GetUser(long id);
        
        public User GetLoggedInUser();
    }
}