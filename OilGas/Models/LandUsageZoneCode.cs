namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LandUsageZoneCode")]
    public partial class LandUsageZoneCode
    {
        [StringLength(30)]
        public string Name { get; set; }
        
        [Key]
        [Column(Order = 2)]
        [StringLength(30)]
        public string Value { get; set; }

        public int? Rank { get; set; }
        
        [Key]
        [Column(Order = 1)]
        public int? LandType { get; set; }
    }
}
