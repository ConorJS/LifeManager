using System;

namespace LifeManager.Server.Domain {
    public class Appointment : ITask {
        public long Id { get; set; }

        public string Name { get; set; }
        
        public DateTime DateTimeCreated { get; set; }
        
        public DateTime DateTimeLastModified { get; set; }
        
        public int RelativeSize { get; set; }
    }
}