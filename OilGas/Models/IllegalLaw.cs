namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IllegalLaw")]
    public partial class IllegalLaw
    {
        [Key]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Value { get; set; }

        public int? Rank { get; set; }
    }
}
