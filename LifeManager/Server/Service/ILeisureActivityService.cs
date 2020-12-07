using LifeManager.Server.Domain;

namespace LifeManager.Server.Service {
    public interface ILeisureActivityService {
        public LeisureActivity GetById(long id);

        public void Create(LeisureActivity domain);
    }
}