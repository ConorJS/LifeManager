using System;

namespace LifeManager.Server.Model.Domain {
    public class Principle : ITask {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime? DateTimeCreated { get; set; }

        public DateTime? DateTimeLastModified { get; set; }

        public int RelativeSize { get; set; }
    }
}