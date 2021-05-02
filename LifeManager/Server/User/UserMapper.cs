using System;

namespace LifeManager.Server.User {
    public class UserMapper {
        public User ToDomain(UserEntity entity) {
            return new User {
                Id = entity.Id,
                DisplayName = entity.DisplayName,
                DateTimeCreated = entity.DateTimeCreated,
                DateTimeLastModified = entity.DateTimeLastModified
            };
        }
        
        public UserEntity ToEntity(User domain) {
            return new UserEntity {
                Id = domain.Id,
                DisplayName = domain.DisplayName,
                DateTimeCreated = domain.DateTimeCreated ?? DateTime.Now,
                DateTimeLastModified = domain.DateTimeLastModified ?? DateTime.Now
            };
        }
    }
}