using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeManager.Server.User.Configuration {
    public class UserConfigurationMapper {
        private static readonly string TO_DO_TASK_TABLE_NAME = UserConfiguration.ToDoTaskTableViewConfiguration.TABLE_NAME;
        
        public UserConfiguration ToDomain(UserConfigurationEntity entity) {
            UserConfiguration.ToDoTaskTableViewConfiguration toDoTaskConfig = new UserConfiguration.ToDoTaskTableViewConfiguration {
                UserConfigurationId = entity.Id,
                ColumnSortOrderConfig = new List<ColumnSortOrder>(ToDomain(entity.SortedColumns, TO_DO_TASK_TABLE_NAME)),
                HideCompletedAndCancelled = entity.ToDoTaskHideCompletedAndCancelled
            };

            return new UserConfiguration {
                Id = entity.Id,
                DateTimeCreated = entity.DateTimeCreated,
                DateTimeLastModified = entity.DateTimeLastModified,
                ToDoTaskConfig = toDoTaskConfig
            };
        }

        public UserConfigurationEntity ToEntity(UserConfiguration domain) {
            ICollection<ColumnSortOrderEntity> mappedColumnSortOrderEntities = new List<ColumnSortOrderEntity>();

            foreach (var columnSortOrderEntity in ToEntity(domain.ToDoTaskConfig.ColumnSortOrderConfig, TO_DO_TASK_TABLE_NAME)) {
                mappedColumnSortOrderEntities.Add(columnSortOrderEntity);
            }

            return new UserConfigurationEntity {
                Id = domain.Id,
                SortedColumns = mappedColumnSortOrderEntities,

                // When mapping down to the entity model for the first time, populate these values.
                // These could get overridden during the save; this is mostly to fulfil the non-null constraint on the entity.
                DateTimeCreated = domain.DateTimeCreated ?? DateTime.Now,
                DateTimeLastModified = domain.DateTimeLastModified ?? DateTime.Now,
                ToDoTaskHideCompletedAndCancelled = domain.ToDoTaskConfig.HideCompletedAndCancelled
            };
        }

        private ICollection<ColumnSortOrder> ToDomain(ICollection<ColumnSortOrderEntity> entities, string tableName) {
            return entities
                .Where(entity => tableName.Equals(entity.TableName))
                .Select(entity => new ColumnSortOrder {
                    UserConfigurationId = entity.UserConfigurationId,
                    ColumnName = entity.ColumnName,
                    IsSortedAscending = entity.IsSortedAscending,
                    Precedence = entity.Precedence 
                })
                .ToList();
        }

        private ICollection<ColumnSortOrderEntity> ToEntity(ICollection<ColumnSortOrder> domainItems, string tableName) {
            return domainItems
                .Select(domain => new ColumnSortOrderEntity {
                    UserConfigurationId = domain.UserConfigurationId,
                    TableName = tableName,
                    ColumnName = domain.ColumnName,
                    IsSortedAscending = domain.IsSortedAscending,
                    Precedence = domain.Precedence
                })
                .ToList();
        }
    }
}