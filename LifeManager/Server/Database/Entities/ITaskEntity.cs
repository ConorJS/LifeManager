using System.ComponentModel.DataAnnotations.Schema;

namespace LifeManager.Server.Database.Entities {
    public interface ITaskEntity : IItemEntity {
        public int RelativeSize { get; set; }
    }
}