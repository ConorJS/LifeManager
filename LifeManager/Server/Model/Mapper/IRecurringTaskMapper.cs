using LifeManager.Server.Model.Domain;
using LifeManager.Server.Model.Entity;

namespace LifeManager.Server.Model.Mapper {
    public interface IRecurringTaskMapper {
        public RecurringTask ToDomain(RecurringTaskEntity entity);

        public RecurringTaskEntity ToEntity(RecurringTask domain);
    }
}