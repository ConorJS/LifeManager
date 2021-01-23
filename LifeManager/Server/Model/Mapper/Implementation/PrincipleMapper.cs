using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper.Implementation {
    public class PrincipleMapper : IPrincipleMapper {
        private readonly IItemMapper _itemMapper;

        public PrincipleMapper(IItemMapper itemMapper) {
            _itemMapper = itemMapper;
        }

        public Principle ToDomain(PrincipleEntity entity) {
            Principle domain = new Principle { };

            _itemMapper.ToDomain(entity, domain);

            return domain;
        }

        public PrincipleEntity ToEntity(Principle domain) {
            PrincipleEntity entity = new PrincipleEntity { };

            _itemMapper.ToEntity(domain, entity);

            return entity;
        }
    }
}