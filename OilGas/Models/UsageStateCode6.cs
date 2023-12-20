namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UsageStateCode6
    {
        [StringLength(50)]
        public string Name { get; set; }

        [Key]
        [StringLength(10)]
        public string Value { get; set; }

        public byte? Rank { get; set; }

        [StringLength(50)]
        public string Parent { get; set; }

        [StringLength(50)]
        public string child { get; set; }

        [StringLength(5)]
        public string sys { get; set; }
    }
}
