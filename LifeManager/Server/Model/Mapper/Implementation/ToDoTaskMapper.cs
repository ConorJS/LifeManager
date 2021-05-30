using System.Collections.Generic;
using System.Linq;
using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper.Implementation {
    public class ToDoTaskMapper : IToDoTaskMapper {
        private readonly ITaskMapper _taskMapper;

        public ToDoTaskMapper(ITaskMapper taskMapper) {
            _taskMapper = taskMapper;
        }

        public ToDoTask ToDomain(ToDoTaskEntity entity) {
            ToDoTask domain = new ToDoTask {
                Status = entity.Status,
                Priority = entity.Priority,
                Dependencies = entity.Dependencies == null
                    ? new List<long>()
                    : entity.Dependencies.Select(dependencyEntity => dependencyEntity.ToDoTaskDependencyId).ToList()
            };

            _taskMapper.ToDomain(entity, domain);

            return domain;
        }

        public ToDoTaskEntity ToEntity(ToDoTask domain) {
            ToDoTaskEntity entity = new ToDoTaskEntity {
                Status = domain.Status,
                Priority = domain.Priority,
                Dependencies = domain.Dependencies.Select(dependencyId => new ToDoTaskDependencyEntity {
                    ToDoTaskEntityId = domain.Id,
                    ToDoTaskDependencyId = dependencyId
                }).ToList()
            };

            _taskMapper.ToEntity(domain, entity);

            return entity;
        }
    }
}