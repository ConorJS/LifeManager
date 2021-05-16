using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using LifeManager.Server.Model;
using LifeManager.Server.User;
using LifeManager.Server.User.Configuration;
using Microsoft.EntityFrameworkCore;

namespace LifeManager.Server.Database.Implementation {
    public class LifeManagerRepository : ILifeManagerRepository {
        //== general ================================================================================================================================

        private readonly LifeManagerDatabaseContext _dbContext;

        public LifeManagerRepository(LifeManagerDatabaseContext lifeManagerDatabaseContext) {
            _dbContext = lifeManagerDatabaseContext;
        }

        public void Dispose() { }

        private void Detach<T>(T entity) where T : class {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }

        //== user ===================================================================================================================================

        public UserEntity LoadUser(long id) {
            /*
             * TODO - Fix this
             * 
             * Compiling a query which loads related collections for more than one collection navigation either via 'Include' or through projection
             * but no 'QuerySplittingBehavior' has been configured. By default Entity Framework will use 'QuerySplittingBehavior.SingleQuery' which
             * can potentially result in slow query performance. See https://go.microsoft.com/fwlink/?linkid=2134277 for more information.
             * To identify the query that's triggering this warning call
             * 'ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning))'
             */
            return DetachedEntityOrNull(_dbContext.User
                .Include(user => user.UserConfigurationEntity)
                .ThenInclude(config => config.SortedColumns)
                .FirstOrDefault(entity => entity.Id == id));
        }

        public UserConfigurationEntity LoadUserConfiguration(long userId) {
            IList<UserConfigurationEntity> configItems = _dbContext.User.Find(userId).UserConfigurationEntity;
            return DetachedEntityOrNull(configItems.IsNullOrEmpty() ? null : configItems[0]);
        }

        public void SaveUserConfiguration(UserConfigurationEntity entity) {
            _dbContext.Set<UserConfigurationEntity>().Update(entity);
            _dbContext.SaveChanges();
        }

        //== queries ================================================================================================================================

        public List<T> LoadEntities<T>(long ownedByUserId) where T : class, IItemEntity {
            List<T> entities = _dbContext.Set<T>()
                .Where(t => t.OwnedByUserId == ownedByUserId)
                .Where(t => t.Active)
                .ToList();
            entities.ForEach(Detach);

            return entities;
        }

        public T LoadEntity<T>(long id) where T : class, IItemEntity {
            var entity = DetachedEntityOrNull(_dbContext.Set<T>().Find(id));

            if (!entity.Active) {
                throw new InvalidOperationException($"Tried to load a ${typeof(T)} entity (id = ${id}), but it has already been removed.");
            }

            return entity;
        }

        public void SaveEntity<T>(T entity) where T : class, IItemEntity {
            _dbContext.Set<T>().Update(entity);
            _dbContext.SaveChanges();
        }

        //== helpers ================================================================================================================================

        private T DetachedEntityOrNull<T>(T entity) where T : class {
            if (entity == null) {
                return null;
            }

            Detach(entity);
            return entity;
        }
    }
}