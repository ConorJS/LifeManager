using LifeManager.Server.Domain;

namespace LifeManager.Server.Service {
    public interface IChoreService {
        public Chore GetById(long id);

        public void Create(Chore domain);
    }
}