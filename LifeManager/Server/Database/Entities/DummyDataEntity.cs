using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeManager.Server.Database.Entities {
    [Table("Dummy")]
    public class DummyDataEntity {
        
        [Key]
        public long Id { get; set; }
        
        public string Name { get; set; }
    }
}