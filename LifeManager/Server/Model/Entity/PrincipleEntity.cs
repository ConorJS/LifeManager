using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeManager.Server.Model.Entity {
    [Table("Principle")]
    public class PrincipleEntity : IItemEntity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long OwnedByUserId { get; set; }
        
        public bool Active { get; set; }

        public string Name { get; set; }
        
        public string Comments { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeLastModified { get; set; }
    }
}