using System;
using System.Collections.Generic;

namespace LifeManager.Server.User.Configuration {
    public class UserConfiguration {
        public long Id { get; set; }
        
        public User User { get; set; }

        public DateTime? DateTimeCreated { get; set; }

        public DateTime? DateTimeLastModified { get; set; }

        public ToDoTaskScreenConfiguration ToDoTaskConfig { get; set; }

        public class ToDoTaskScreenConfiguration {
            public ICollection<ColumnSortOrder> ColumnSortOrderConfig;
            
            public bool HideCompletedAndCancelled { get; set; }
        } 
    }
}