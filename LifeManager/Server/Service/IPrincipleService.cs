using LifeManager.Server.Domain;

namespace LifeManager.Server.Service {
    public interface IPrincipleService {
        public Principle GetById(long id);

        public void Create(Principle domain);
    }
}