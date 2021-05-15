using System;
using System.Collections.Generic;
using LifeManager.Server.Model;
using LifeManager.Server.User;
using LifeManager.Server.User.Configuration;

namespace LifeManager.Server.Database {
    public interface ILifeManagerRepository : IDisposable {
        //== user ===================================================================================================================================

        public UserEntity LoadUser(long id);

        public UserConfigurationEntity LoadUserConfiguration(long id);

        public void SaveUserConfiguration(UserConfigurationEntity entity);

        //== general item queries ===================================================================================================================

        public List<T> LoadEntities<T>(long ownedByUserId) where T : class, IItemEntity;

        public T LoadEntity<T>(long id) where T : class, IItemEntity;

        public void SaveEntity<T>(T entity) where T : class, IItemEntity;
    }
}