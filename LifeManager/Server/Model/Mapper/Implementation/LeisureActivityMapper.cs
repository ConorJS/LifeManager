using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper.Implementation {
    public class LeisureActivityMapper : ILeisureActivityMapper {
        private readonly IItemMapper _itemMapper;

        public LeisureActivityMapper(IItemMapper itemMapper) {
            _itemMapper = itemMapper;
        }

        public LeisureActivity ToDomain(LeisureActivityEntity entity) {
            LeisureActivity domain = new LeisureActivity { };

            _itemMapper.ToDomain(entity, domain);

            return domain;
        }

        public LeisureActivityEntity ToEntity(LeisureActivity domain) {
            LeisureActivityEntity entity = new LeisureActivityEntity { };

            _itemMapper.ToEntity(domain, entity);

            return entity;
        }
    }
}