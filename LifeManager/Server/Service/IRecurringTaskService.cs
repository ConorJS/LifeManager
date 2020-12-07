using LifeManager.Server.Domain;

namespace LifeManager.Server.Service {
    public interface IRecurringTaskService {
        
        public RecurringTask GetById(long id);

        public void Create(RecurringTask domain);
    }
}