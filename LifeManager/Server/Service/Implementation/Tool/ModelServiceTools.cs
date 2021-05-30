using System;
using System.Collections.Generic;
using LifeManager.Server.Database;
using LifeManager.Server.Model;
using LifeManager.Server.User;

namespace LifeManager.Server.Service.Implementation.Tool {
    public class ModelServiceTools : IModelServiceTools {
        //== attributes =============================================================================================================================

        private readonly ILifeManagerRepository _lifeManagerRepository;

        private readonly IUserService _userService;

        //== init ===================================================================================================================================

        public ModelServiceTools(ILifeManagerRepository lifeManagerRepository, IUserService userService) {
            _lifeManagerRepository = lifeManagerRepository;
            _userService = userService;
        }

        //== methods ================================================================================================================================

        [Obsolete("Don't try to generalise entity retrieval in any way")]
        public List<T> AllEntitiesForLoggedInUser<T>() where T : class, IItemEntity {
            return _lifeManagerRepository.LoadEntities<T>(_userService.GetLoggedInUser().Id);
        }

        public void InitialiseNewItem(IItem domain) {
            domain.DateTimeCreated = DateTime.Now;
            domain.DateTimeLastModified = DateTime.Now;

            domain.OwnedByUserId = _userService.GetLoggedInUser().Id;

            domain.Active = true;
        }

        public void UpdateProcessing<T>(IItem domain) where T : class, IItemEntity {
            if (_lifeManagerRepository.LoadEntity<T>(domain.Id) == null) {
                FailReferencingVerb("update", domain.Id, typeof(T));
            }

            // TODO: Check if the object differs from the existing object.
            // TODO: It may be better to move UpdateProcessing calls within the SaveEntity call, so that we can compare the two entity models.
            domain.DateTimeLastModified = DateTime.Now;
        }

        public void RemoveEntity<T>(long id) where T : class, IItemEntity {
            T entity = _lifeManagerRepository.LoadEntity<T>(id);
            
            if (entity == null) {
                FailReferencingVerb("remove", id, typeof(T));
                
            } else {
                entity.Active = false;
                _lifeManagerRepository.SaveEntity(entity);
            }
        }

        //== helpers ================================================================================================================================

        private static void FailReferencingVerb(string verb, long entityId, Type entityType) {
            throw new InvalidOperationException(
                $"Tried to ${verb} a ${entityType} (id = {entityId}), but the item doesn't exist. " +
                $"This could indicate a misuse of save resource/service methods, or a malformed query to a REST endpoint.");
        }
    }
}