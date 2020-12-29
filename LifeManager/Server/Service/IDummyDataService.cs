using LifeManager.Server.Model.Domain;

namespace LifeManager.Server.Service {
    public interface IDummyDataService {
        DummyData GetDummyDataById(long id);

        void CreateDummyData(DummyData dummyData);
    }
}