using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper.Implementation {
    public class ChoreMapper : IChoreMapper {
        private readonly ITaskMapper _taskMapper;

        public ChoreMapper(ITaskMapper taskMapper) {
            _taskMapper = taskMapper;
        }

        public Chore ToDomain(ChoreEntity entity) {
            Chore domain = new Chore { };

            _taskMapper.ToDomain(entity, domain);

            return domain;
        }

        public ChoreEntity ToEntity(Chore domain) {
            ChoreEntity entity = new ChoreEntity { };

            _taskMapper.ToEntity(domain, entity);

            return entity;
        }
    }
}