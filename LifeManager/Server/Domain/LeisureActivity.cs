using System;
using LifeManager.Server.Database.Entities;

namespace LifeManager.Server.Domain {
    public class LeisureActivity : IItem {
        public long Id { get; set; }

        public string Name { get; set; }
        
        public DateTime? DateTimeCreated { get; set; }
        
        public DateTime? DateTimeLastModified { get; set; }
    }
}