using System;

namespace LifeManager.Server.Security {
    public class User {
        public long Id { get; set; }

        public string DisplayName { get; set; }

        public DateTime? DateTimeCreated { get; set; }

        public DateTime? DateTimeLastModified { get; set; }
    }
}