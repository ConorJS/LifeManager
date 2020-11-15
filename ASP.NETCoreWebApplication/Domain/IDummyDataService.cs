namespace ASP.NETCoreWebApplication {
    public interface IDummyDataService {
        DummyData GetDummyDataById(long id);

        void CreateDummyData(DummyData dummyData);
    }
}