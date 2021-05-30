using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using LifeManager.Server.Model;
using LifeManager.Server.Model.Entity;
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
            UserEntity userEntity = _dbContext.User
                .Include(user => user.UserConfigurationEntity)
                .ThenInclude(config => config.SortedColumns)
                .FirstOrDefault(entity => entity.Id == id);

            return DetachedEntityOrNull(userEntity);
        }

        public UserConfigurationEntity LoadUserConfiguration(long userConfigurationId) {
            UserConfigurationEntity userConfigurationEntity = _dbContext.UserConfiguration
                .Include(userConfiguration => userConfiguration.SortedColumns)
                .FirstOrDefault(userConfiguration => userConfiguration.Id == userConfigurationId);

            return DetachedEntityOrNull(userConfigurationEntity);
        }

        public UserConfigurationEntity LoadUserConfigurationAttached(long userConfigurationId) {
            return _dbContext.UserConfiguration
                .Include(userConfiguration => userConfiguration.SortedColumns)
                .FirstOrDefault(userConfiguration => userConfiguration.Id == userConfigurationId);
        }

        public void SaveUserConfiguration(UserConfigurationEntity entity) {
            _dbContext.Set<UserConfigurationEntity>().Update(entity);
            _dbContext.SaveChanges();

            DetachedEntityOrNull(entity);
        }

        //== general item queries ===================================================================================================================

        public List<T> LoadEntities<T>(long ownedByUserId) where T : class, IItemEntity {
            List<T> entities = _dbContext.Set<T>()
                .Where(t => t.OwnedByUserId == ownedByUserId)
                .Where(t => t.Active)
                .ToList();
            entities.ForEach(Detach);

            return entities;
        }

        [Obsolete("Don't try to generalise entity retrieval in any way")]
        public T LoadEntity<T>(long id) where T : class, IItemEntity {
            var entity = DetachedEntityOrNull(_dbContext.Set<T>().Find(id));

            if (!entity.Active) {
                throw new InvalidOperationException($"Tried to load a ${typeof(T)} entity (id = ${id}), but it has already been removed.");
            }

            return entity;
        }

        public void SaveEntity<T>(T entity) where T : class, IItemEntity {
            switch (entity) {
                case ToDoTaskEntity toDoTaskEntity:
                    if (toDoTaskEntity.Id == 0) {
                        break;
                    }
                    
                    ToDoTaskEntity existing = LoadToDoTask(entity.Id);
                    foreach (ToDoTaskDependencyEntity existingDependency in existing.Dependencies) {
                        if (toDoTaskEntity.Dependencies.All(e => e.ToDoTaskDependencyId != existingDependency.ToDoTaskDependencyId)) {
                            _dbContext.Entry(existingDependency).State = EntityState.Deleted;
                        }
                    }

                    _dbContext.SaveChanges();
                    DetachedEntityOrNull(existing);

                    foreach (ToDoTaskDependencyEntity dependency in toDoTaskEntity.Dependencies) {
                        _dbContext.Entry(dependency).State = existing.Dependencies
                            .Any(e => dependency.ToDoTaskDependencyId == e.ToDoTaskDependencyId)
                            ? EntityState.Modified
                            : EntityState.Added;
                    }

                    break;
            }

            _dbContext.Set<T>().Update(entity);
            _dbContext.SaveChanges();
        }

        //== specific item queries ==================================================================================================================

        public ToDoTaskEntity LoadToDoTask(long id) {
            IList<ToDoTaskEntity> entities = _dbContext.ToDoTask
                .Include(e => e.Dependencies)
                .Where(e => e.Id == id)
                .ToList();

            if (entities.IsNullOrEmpty()) {
                throw new InvalidOperationException($"Tried to load a ToDoTask entity (id = ${id}), but it does not exist.");
            }

            return entities[0];
        }

        public IEnumerable<ToDoTaskEntity> LoadAllToDoTasksForUser(long userId) {
            return _dbContext.ToDoTask
                .Include(e => e.Dependencies)
                .Where(e => e.OwnedByUserId == userId)
                .Where(e => e.Active);
        }

        //== helpers ================================================================================================================================

        /**
         * Detaches an entity, and returns the detached entity.
         *
         * This aims to propagate through relationships (in the owner -> child direction), thus detaching child entities, too.
         * These are implemented manually. In entity graphs more than 1 relationship deep, this will be used recursively.
         */
        private T DetachedEntityOrNull<T>(T entity) where T : class {
            if (entity == null) {
                return null;
            }

            switch (entity) {
                case ToDoTaskEntity toDoTaskEntity:
                    foreach (var toDoTaskDependencyEntity in toDoTaskEntity.Dependencies) {
                        Detach(toDoTaskDependencyEntity);
                    }
                    break;
                
                case UserEntity userEntity:
                    foreach (var userConfigurationEntity in userEntity.UserConfigurationEntity) {
                        DetachedEntityOrNull(userConfigurationEntity);
                    }
                    break;
                
                case UserConfigurationEntity userConfigurationEntity:
                    foreach (ColumnSortOrderEntity columnSortOrderEntity in userConfigurationEntity.SortedColumns) {
                        Detach(columnSortOrderEntity);
                    }
                    break;
            }

            Detach(entity);
            return entity;
        }
    }
}