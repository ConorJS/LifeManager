using System;

namespace LifeManager.Server.Model {
    public interface IItem : IPersistable {
        public string Name { get; set; }
        
        public long OwnedByUserId { get; set; }

        public DateTime? DateTimeCreated { get; set; }
        
        public DateTime? DateTimeLastModified { get; set; }
    }
}