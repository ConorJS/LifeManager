using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper {
    public interface IPrincipleMapper {
        public Principle ToDomain(PrincipleEntity entity);

        public PrincipleEntity ToEntity(Principle domain);
    }
}