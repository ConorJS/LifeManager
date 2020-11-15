using System;
using ASP.NETCoreWebApplication.Entities;

namespace ASP.NETCoreWebApplication.Store {
    public interface ILifeManagerRepository : IDisposable {
        public DummyDataEntity LoadDummyData(long id);

        public void SaveDummyData(DummyDataEntity dummyDataEntity);
    }
}