using System;
using LifeManager.Server.Database.Entities;

namespace LifeManager.Server.Domain {
    public interface IItem : IPersistable {
        public string Name { get; set; }
        
        public DateTime? DateTimeCreated { get; set; }
        
        public DateTime? DateTimeLastModified { get; set; }
    }
}