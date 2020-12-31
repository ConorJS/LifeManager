using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeManager.Server.Model.Entity {
    [Table("ToDoTask")]
    public class ToDoTaskEntity : ITaskEntity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long OwnedByUserId { get; set; }
        
        public bool Active { get; set; }

        public string Name { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeLastModified { get; set; }

        public int RelativeSize { get; set; }
    }
}