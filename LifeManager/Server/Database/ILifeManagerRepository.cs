using System;
using LifeManager.Server.Database.Entities;

namespace LifeManager.Server.Database {
    public interface ILifeManagerRepository : IDisposable {
        public DummyDataEntity LoadDummyData(long id);

        public void SaveDummyData(DummyDataEntity dummyDataEntity);
    }
}