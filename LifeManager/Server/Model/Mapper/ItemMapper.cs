using System;

namespace LifeManager.Server.Model.Mapper {
    public class ItemMapper {
        public void ToDomain(IItemEntity entity, IItem domain) {
            domain.Id = entity.Id;
            domain.DateTimeCreated = entity.DateTimeCreated;
            domain.DateTimeLastModified = entity.DateTimeLastModified;
            domain.OwnedByUserId = entity.OwnedByUserId;
            domain.Active = entity.Active;
            domain.Name = entity.Name;
            domain.Comments = entity.Comments;
        }

        public void ToEntity(IItem domain, IItemEntity entity) {
            entity.Id = domain.Id;

            // When mapping down to the entity model for the first time, populate these values.
            // These could get overridden during the save; this is mostly to fulfil the non-null constraint on the entity.
            entity.DateTimeCreated = domain.DateTimeCreated ?? DateTime.Now;
            entity.DateTimeLastModified = domain.DateTimeLastModified ?? DateTime.Now;
            
            entity.OwnedByUserId = domain.OwnedByUserId;
            entity.Active = domain.Active;
            entity.Name = domain.Name;
            entity.Comments = domain.Comments;
        }
    }
}