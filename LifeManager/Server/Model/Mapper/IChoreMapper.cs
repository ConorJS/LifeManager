using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper {
    public interface IChoreMapper {
        public Chore ToDomain(ChoreEntity entity);
        
        public ChoreEntity ToEntity(Chore domain);
    }
}