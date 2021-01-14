using System;

namespace LifeManager.Server.Model {
    public interface IItemEntity : IPersistableEntity {
        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeLastModified { get; set; }
        
        public long OwnedByUserId { get; set; }
        
        public bool Active { get; set; }

        public string Name { get; set; }
        
        public string Comments { get; set; }
    }
}