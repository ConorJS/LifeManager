using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper {
    public class LeisureActivityMapper {
        public LeisureActivity ToDomain(LeisureActivityEntity entity) {
            LeisureActivity domain = new LeisureActivity { };

            new ItemMapper().ToDomain(entity, domain);

            return domain;
        }

        public LeisureActivityEntity ToEntity(LeisureActivity domain) {
            LeisureActivityEntity entity = new LeisureActivityEntity { };

            new ItemMapper().ToEntity(domain, entity);

            return entity;
        }
    }
}