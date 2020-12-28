using System.Collections.Generic;
using LifeManager.Server.Domain;

namespace LifeManager.Server.Service {
    public interface IRecurringTaskService {
        public IEnumerable<RecurringTask> GetAll();
        
        public RecurringTask GetById(long id);

        public void Create(RecurringTask domain);
        
        public void Update(RecurringTask domain);
        
        public void Remove(long id);
    }
}