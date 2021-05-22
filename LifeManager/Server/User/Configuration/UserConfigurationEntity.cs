using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeManager.Server.User.Configuration {
    [Table("UserConfiguration")]
    public class UserConfigurationEntity {
        //== attributes: base =======================================================================================================================
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeLastModified { get; set; }
        
        [ForeignKey("UserId")]
        public virtual UserEntity UserEntity { get; set; }
        
        // TODO: Switch this to a list
        [InverseProperty("Configuration")]
        public virtual ICollection<ColumnSortOrderEntity> SortedColumns { get; set; }
        
        //== attributes: to do tasks ================================================================================================================
        
        public bool ToDoTaskHideCompletedAndCancelled { get; set; }
    }
}