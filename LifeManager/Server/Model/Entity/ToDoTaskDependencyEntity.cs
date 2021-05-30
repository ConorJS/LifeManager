using System.ComponentModel.DataAnnotations.Schema;

namespace LifeManager.Server.Model.Entity {
    [Table("ToDoTask_Dependency")]
    public class ToDoTaskDependencyEntity {
        [ForeignKey("ToDoTaskEntityId")]
        public virtual ToDoTaskEntity ToDoTask { get; set; }
        public long ToDoTaskEntityId { get; set; }

        public virtual ToDoTaskEntity ToDoTaskDependency { get; set; }
        public long ToDoTaskDependencyId { get; set; }
    }
}