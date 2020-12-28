using System.Collections.Generic;
using LifeManager.Server.Domain;

namespace LifeManager.Server.Service {
    public interface IChoreService {
        public IEnumerable<Chore> GetAll();
        
        public Chore GetById(long id);

        public void Create(Chore domain);
        
        public void Update(Chore domain);
        
        public void Remove(long id);
    }
}