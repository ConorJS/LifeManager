using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeManager.Server.User.Configuration {
    [Table("UserConfiguration_ColumnSortOrder")]
    public class ColumnSortOrderEntity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        [ForeignKey("UserConfigurationId")]
        public virtual UserConfigurationEntity Configuration { get; set; }
        
        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public int Precedence { get; set; }
    }
}