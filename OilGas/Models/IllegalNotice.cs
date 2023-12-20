namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IllegalNotice")]
    public partial class IllegalNotice
    {
        [Key]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Value { get; set; }

        public int? Rank { get; set; }
    }
}
