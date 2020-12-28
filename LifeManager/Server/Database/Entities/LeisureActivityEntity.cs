using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeManager.Server.Database.Entities {
    [Table("LeisureActivity")]
    public class LeisureActivityEntity : IItemEntity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeLastModified { get; set; }
    }
}