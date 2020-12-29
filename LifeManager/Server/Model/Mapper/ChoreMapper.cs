using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper {
    public class ChoreMapper {
        public Chore ToDomain(ChoreEntity entity) {
            Chore domain = new Chore { };

            new TaskMapper().ToDomain(entity, domain);

            return domain;
        }

        public ChoreEntity ToEntity(Chore domain) {
            ChoreEntity entity = new ChoreEntity { };

            new TaskMapper().ToEntity(domain, entity);

            return entity;
        }
    }
}