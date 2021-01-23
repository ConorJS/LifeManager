using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper.Implementation {
    public class RecurringTaskMapper : IRecurringTaskMapper {
        private readonly IItemMapper _itemMapper;

        public RecurringTaskMapper(IItemMapper itemMapper) {
            _itemMapper = itemMapper;
        }
        
        public RecurringTask ToDomain(RecurringTaskEntity entity) {
            RecurringTask domain = new RecurringTask { };

            _itemMapper.ToDomain(entity, domain);

            return domain;
        }

        public RecurringTaskEntity ToEntity(RecurringTask domain) {
            RecurringTaskEntity entity = new RecurringTaskEntity { };

            _itemMapper.ToEntity(domain, entity);

            return entity;
        }
    }
}