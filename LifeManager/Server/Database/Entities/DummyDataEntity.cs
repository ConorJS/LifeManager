using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeManager.Server.Database.Entities {
    [Table("dummy")]
    public class DummyDataEntity {
        
        [Key]
        [Column("id")]
        public long Id { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
    }
}