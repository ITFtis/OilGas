namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PermissionSetting")]
    public partial class PermissionSetting
    {
        [Key]
        [StringLength(2)]
        public string Role { get; set; }

        [Column(TypeName = "text")]
        public string Power { get; set; }
    }
}
