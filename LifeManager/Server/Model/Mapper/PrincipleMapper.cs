using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper {
    public class PrincipleMapper {
        public Principle ToDomain(PrincipleEntity entity) {
            Principle domain = new Principle { };

            new ItemMapper().ToDomain(entity, domain);

            return domain;
        }

        public PrincipleEntity ToEntity(Principle domain) {
            PrincipleEntity entity = new PrincipleEntity { };

            new ItemMapper().ToEntity(domain, entity);

            return entity;
        }
    }
}