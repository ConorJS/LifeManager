using System.Collections.Generic;
using LifeManager.Server.Domain;

namespace LifeManager.Server.Service {
    public interface IPrincipleService {
        public IEnumerable<Principle> GetAll();
        
        public Principle GetById(long id);

        public void Create(Principle domain);
        
        public void Update(Principle domain);
        
        public void Remove(long id);
    }
}