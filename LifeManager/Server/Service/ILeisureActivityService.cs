using System.Collections.Generic;
using LifeManager.Server.Domain;

namespace LifeManager.Server.Service {
    public interface ILeisureActivityService {
        public IEnumerable<LeisureActivity> GetAll();
        
        public LeisureActivity GetById(long id);

        public void Create(LeisureActivity domain);
        
        public void Update(LeisureActivity domain);
        
        public void Remove(long id);
    }
}