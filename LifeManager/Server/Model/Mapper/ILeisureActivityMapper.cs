using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper {
    public interface ILeisureActivityMapper {
        public LeisureActivity ToDomain(LeisureActivityEntity entity);

        public LeisureActivityEntity ToEntity(LeisureActivity domain);
    }
}