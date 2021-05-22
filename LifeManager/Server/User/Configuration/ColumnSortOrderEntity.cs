using System.ComponentModel.DataAnnotations.Schema;

namespace LifeManager.Server.User.Configuration {
    [Table("UserConfiguration_ColumnSortOrder")]
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global - EF Core Entity cannot be sealed.
    public class ColumnSortOrderEntity {
        public long UserConfigurationId { get; set; }
        
        [ForeignKey("UserConfigurationId")]
        public virtual UserConfigurationEntity Configuration { get; set; }
        
        public string TableName { get; set; }

        public string ColumnName { get; set; }
        
        public bool IsSortedAscending { get; set; }

        public int Precedence { get; set; }
    }
}