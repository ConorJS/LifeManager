using System;
using System.Collections.Generic;
using LifeManager.Server.Model;

namespace LifeManager.Server.Service.Implementation.Tool {
    public interface IModelServiceTools {
        [Obsolete("Don't try to generalise entity retrieval in any way")]
        public List<T> AllEntitiesForLoggedInUser<T>() where T : class, IItemEntity;

        public void InitialiseNewItem(IItem domain);

        public void UpdateProcessing<T>(IItem domain) where T : class, IItemEntity;

        public void RemoveEntity<T>(long id) where T : class, IItemEntity;
    }
}