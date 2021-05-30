using System;
using System.Collections.Generic;
using LifeManager.Server.Model;
using LifeManager.Server.Model.Entity;
using LifeManager.Server.User;
using LifeManager.Server.User.Configuration;

namespace LifeManager.Server.Database {
    public interface ILifeManagerRepository : IDisposable {
        //== user ===================================================================================================================================

        public UserEntity LoadUser(long id);

        public UserConfigurationEntity LoadUserConfiguration(long id);

        public UserConfigurationEntity LoadUserConfigurationAttached(long userConfigurationId);

        public void SaveUserConfiguration(UserConfigurationEntity entity);

        //== general item queries ===================================================================================================================

        public List<T> LoadEntities<T>(long ownedByUserId) where T : class, IItemEntity;
        
        [Obsolete("Don't try to generalise entity retrieval in any way")] 
        public T LoadEntity<T>(long id) where T : class, IItemEntity;

        public void SaveEntity<T>(T entity) where T : class, IItemEntity;

        //== specific item queries ==================================================================================================================

        public ToDoTaskEntity LoadToDoTask(long id);

        public IEnumerable<ToDoTaskEntity> LoadAllToDoTasksForUser(long userId);
    }
}