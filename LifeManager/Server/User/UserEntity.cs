using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LifeManager.Server.User.Configuration;

namespace LifeManager.Server.User {
    [Table("User")]
    public class UserEntity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string DisplayName { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeLastModified { get; set; }
        
        // TODO: Rename field to 'Configuration'
        [InverseProperty("UserEntity")]
        public virtual IList<UserConfigurationEntity> UserConfigurationEntity { get; set; }
    }
}