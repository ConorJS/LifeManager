using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeManager.Server.User.Configuration {
    // TODO: Remove Entity from name
    [Table("UserConfigurationEntity")]
    public class UserConfigurationEntity {
        //== attributes: base =======================================================================================================================
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserConfigurationEntityId { get; set; }
        
        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeLastModified { get; set; }
        
        [ForeignKey("UserId")]
        public virtual UserEntity UserEntity { get; set; }
        
        // TODO: Sorted columns
        //public virtual ICollection<ColumnSortOrderEntity> SortedColumns { get; set; }
        //[InverseProperty()]
        public ICollection<ColumnSortOrderEntity> SortedColumns = new List<ColumnSortOrderEntity>();
        
        //== attributes: to do tasks ================================================================================================================
        
        public bool ToDoTaskHideCompletedAndCancelled { get; set; }
    }
}