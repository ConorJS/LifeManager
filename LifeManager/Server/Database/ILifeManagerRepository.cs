using System;
using LifeManager.Entities;

namespace LifeManager.Store {
    public interface ILifeManagerRepository : IDisposable {
        public DummyDataEntity LoadDummyData(long id);

        public void SaveDummyData(DummyDataEntity dummyDataEntity);
    }
}