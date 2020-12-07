using LifeManager.Server.Database.Entities;

namespace LifeManager.Server.Domain {
    public interface ITask : IItem {
        public int RelativeSize { get; set; }
    }
}