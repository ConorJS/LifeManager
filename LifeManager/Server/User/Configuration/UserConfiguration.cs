using System;
using System.Collections.Generic;

namespace LifeManager.Server.User.Configuration {
    public class UserConfiguration {
        public long Id { get; set; }

        public User User { get; set; }

        public DateTime? DateTimeCreated { get; set; }

        public DateTime? DateTimeLastModified { get; set; }

        public ToDoTaskTableViewConfiguration ToDoTaskConfig { get; set; }

        public interface ITableViewConfiguration {
            public long UserConfigurationId { get; set; }

            public IList<ColumnSortOrder> ColumnSortOrderConfig { get; set; }

            public string GetTableName();
        }

        public class ToDoTaskTableViewConfiguration : ITableViewConfiguration {
            public static readonly string TABLE_NAME = "ToDoTask";

            public string GetTableName() {
                return TABLE_NAME;
            }

            public long UserConfigurationId { get; set; }

            public IList<ColumnSortOrder> ColumnSortOrderConfig { get; set; }

            public bool HideCompletedAndCancelled { get; set; }
        }
    }
}