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

            // Guard against over-sized name/field comments (indicates problem with UI or user tampering with DOM)
            // TODO: Log a warning here
            entity.Name = domain.Name.Length > 255 ? domain.Name.Substring(0, 255) : domain.Name;
            entity.Comments = domain.Comments.Length > 65_535 ? domain.Comments.Substring(0, 65_535) : domain.Comments;
        }
    }
}