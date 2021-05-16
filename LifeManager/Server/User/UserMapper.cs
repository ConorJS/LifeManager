using System;
using System.Collections.Generic;
using Castle.Core.Internal;
using LifeManager.Server.User.Configuration;

namespace LifeManager.Server.User {
    public class UserMapper {
        public User ToDomain(UserEntity entity) {
            // TODO: OneToMany User -> Configuration
            UserConfigurationEntity configEntity = entity.UserConfigurationEntity.IsNullOrEmpty() ? null : entity.UserConfigurationEntity[0];

            return new User {
                Id = entity.Id,
                DisplayName = entity.DisplayName,
                DateTimeCreated = entity.DateTimeCreated,
                DateTimeLastModified = entity.DateTimeLastModified,
                Configuration = configEntity == null ? null : new UserConfigurationMapper().ToDomain(configEntity)
            };
        }

        public UserEntity ToEntity(User domain) {
            // TODO: OneToMany User -> Configuration
            List<UserConfigurationEntity> configEntities = new List<UserConfigurationEntity>();
            configEntities.Add(new UserConfigurationMapper().ToEntity(domain.Configuration));

            return new UserEntity {
                Id = domain.Id,
                DisplayName = domain.DisplayName,
                DateTimeCreated = domain.DateTimeCreated ?? DateTime.Now,
                DateTimeLastModified = domain.DateTimeLastModified ?? DateTime.Now,
                UserConfigurationEntity = configEntities
            };
        }
    }
}