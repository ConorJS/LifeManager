using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper.Implementation {
    public class ChoreMapper : IChoreMapper {
        private readonly IItemMapper _itemMapper;

        public ChoreMapper(IItemMapper itemMapper) {
            _itemMapper = itemMapper;
        }

        public Chore ToDomain(ChoreEntity entity) {
            Chore domain = new Chore { };

            _itemMapper.ToDomain(entity, domain);

            return domain;
        }

        public ChoreEntity ToEntity(Chore domain) {
            ChoreEntity entity = new ChoreEntity { };

            _itemMapper.ToEntity(domain, entity);

            return entity;
        }
    }
}