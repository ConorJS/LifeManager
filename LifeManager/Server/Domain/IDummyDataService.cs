namespace LifeManager.Server.Domain {
    public interface IDummyDataService {
        DummyData GetDummyDataById(long id);

        void CreateDummyData(DummyData dummyData);
    }
}