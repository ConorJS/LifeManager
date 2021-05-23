using System;
using System.Collections.Generic;
using System.Linq;
using LifeManager.Server.Database;
using LifeManager.Server.User.Configuration;

namespace LifeManager.Server.User {
    public class UserService : IUserService {
        //== attributes =============================================================================================================================
        
        private readonly ILifeManagerRepository _lifeManagerRepository;
        
        private readonly UserMapper _userMapper = new UserMapper();
        
        //== init ===================================================================================================================================

        public UserService(ILifeManagerRepository lifeManagerRepository) {
            _lifeManagerRepository = lifeManagerRepository;
        }
        
        //== methods ================================================================================================================================

        public User GetUser(long id) {
            return _userMapper.ToDomain(_lifeManagerRepository.LoadUser(id));
        }

        public User GetLoggedInUser() {
            // TODO: Implement a UserController (REST endpoint), user creation, and a proper user system.
            return GetUser(1);
        }

        // TODO: This probably won't be necessary
        public UserConfiguration GetUserConfiguration(long id) {
            // TODO: Implement a UserController (REST endpoint), user creation, and a proper user system.
            //UserConfigurationEntity entity = _lifeManagerRepository.LoadUserConfiguration(id);
            UserConfigurationEntity entity = _lifeManagerRepository.LoadUserConfiguration(1);

            return new UserConfigurationMapper().ToDomain(entity);
        }

        public void SaveUserConfiguration(UserConfiguration userConfiguration) {
            UserConfigurationEntity userConfigurationEntity = new UserConfigurationMapper().ToEntity(userConfiguration);
            
            _lifeManagerRepository.SaveUserConfiguration(userConfigurationEntity);
        }

        public void SaveTableViewConfiguration(UserConfiguration.ITableViewConfiguration tableViewConfiguration) {
            UserConfigurationEntity userConfigurationEntity = 
                _lifeManagerRepository.LoadUserConfigurationAttached(tableViewConfiguration.UserConfigurationId);

            // Remove existing column configuration for this table.
            List<ColumnSortOrderEntity> otherTableColumns = userConfigurationEntity.SortedColumns
                .Where(sortedColumn => !tableViewConfiguration.GetTableName().Equals(sortedColumn.TableName))
                .ToList();
            userConfigurationEntity.SortedColumns.Clear();
            foreach (var columnSortOrderEntity in otherTableColumns) {
                userConfigurationEntity.SortedColumns.Add(columnSortOrderEntity);
            }

            // TODO: SortedColumns should be a List, so AddRange should be used.
            // Add the new/updated column configuration for this table.
            tableViewConfiguration.ColumnSortOrderConfig
                .Select(columnSortOrderConfig => new ColumnSortOrderEntity {
                    UserConfigurationId = columnSortOrderConfig.UserConfigurationId,
                    TableName = tableViewConfiguration.GetTableName(),
                    ColumnName = columnSortOrderConfig.ColumnName,
                    IsSortedAscending = columnSortOrderConfig.IsSortedAscending,
                    Precedence = columnSortOrderConfig.Precedence
                })
                .ToList().ForEach(entity => userConfigurationEntity.SortedColumns.Add(entity));
            
            switch (tableViewConfiguration) {
                case UserConfiguration.ToDoTaskTableViewConfiguration config :
                    userConfigurationEntity.ToDoTaskHideCompletedAndCancelled = config.HideCompletedAndCancelled;
                    break;
                
                default :
                    throw new InvalidOperationException($"Can't save table configuration for type {tableViewConfiguration.GetType()}");
            }
            
            _lifeManagerRepository.SaveUserConfiguration(userConfigurationEntity);
        }
    }
}