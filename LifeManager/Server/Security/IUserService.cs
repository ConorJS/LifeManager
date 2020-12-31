namespace LifeManager.Server.Security {
    public interface IUserService {
        public User GetUser(long id);
        
        public User GetLoggedInUser();
    }
}