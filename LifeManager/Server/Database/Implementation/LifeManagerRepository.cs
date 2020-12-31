using System.Collections.Generic;
using System.Linq;
using LifeManager.Server.Model;
using LifeManager.Server.Model.Entity;
using LifeManager.Server.Security;
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
            return DetachedEntityOrNull(_dbContext.User.Find(id));
        }

        //== dummy ==================================================================================================================================

        public DummyDataEntity LoadDummyData(long id) {
            return _dbContext.Dummy.Find(id);
        }

        public void SaveDummyData(DummyDataEntity dummyDataEntity) {
            _dbContext.Dummy.Update(dummyDataEntity);
            _dbContext.SaveChanges();
        }
        
        //== queries ================================================================================================================================

        public List<T> LoadEntities<T>(long ownedByUserId) where T : class, IItemEntity {
            List<T> entities = _dbContext.Set<T>()
                .Where(t => t.OwnedByUserId == ownedByUserId)
                .ToList();
            entities.ForEach(Detach);

            return entities;
        }
        
        public T LoadEntity<T>(long id) where T : class, IItemEntity {
            return DetachedEntityOrNull(_dbContext.Set<T>().Find(id));
        }

        public void SaveEntity<T>(T entity) where T : class, IItemEntity {
            _dbContext.Set<T>().Update(entity);
            _dbContext.SaveChanges();
        }

        public void RemoveEntity<T>(T entity) where T : class, IItemEntity {
            _dbContext.Set<T>().Remove(entity);
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