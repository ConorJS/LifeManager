using System;
using Microsoft.Extensions.Logging;

namespace LifeManager.Server.Model.Mapper.Implementation {
    public class ItemMapper : IItemMapper {
        private readonly ILogger<ItemMapper> _logger;

        public ItemMapper(ILogger<ItemMapper> logger) {
            _logger = logger;
        }

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
            entity.Name = TruncateIfBeyond(domain.Name, 255, $"a name for a {domain.GetType()} (id {domain.Id}) ");
            entity.Comments = TruncateIfBeyond(domain.Comments, 65_535, $"comments for a {domain.GetType()} (id {domain.Id}) ");
        }

        private string TruncateIfBeyond(string value, int limit, string referenceForLog) {
            if (value.Length > limit) {
                _logger.LogWarning($"Tried to save {referenceForLog} longer than {limit} characters ({value.Length}): {value}");
                return value.Substring(0, limit);
            }

            return value;
        }
    }
}