namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PleadMain")]
    public partial class PleadMain
    {
        [Key]
        [StringLength(10)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Value { get; set; }

        public int? Rank { get; set; }
    }
}
