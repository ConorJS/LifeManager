using System;

namespace LifeManager.Server.User {
    public class UserEntity {
        public long Id { get; set; }

        public string DisplayName { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeLastModified { get; set; }
    }
}