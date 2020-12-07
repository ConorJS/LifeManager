using System;

namespace LifeManager.Server.Database.Entities {
    public interface IItemEntity : IPersistableEntity {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeLastModified { get; set; }
    }
}