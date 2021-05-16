using System;
using LifeManager.Server.User.Configuration;

namespace LifeManager.Server.User {
    public class User {
        public long Id { get; set; }

        public string DisplayName { get; set; }

        public DateTime? DateTimeCreated { get; set; }

        public DateTime? DateTimeLastModified { get; set; }

        public UserConfiguration Configuration { get; set; }
    }
}