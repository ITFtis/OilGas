namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UsageState_Third
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(15)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string Value { get; set; }

        public byte? Rank { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string Father { get; set; }
    }
}
