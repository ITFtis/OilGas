namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SeaportZone")]
    public partial class SeaportZone
    {
        [Key]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(30)]
        public string Value { get; set; }

        public int? Rank { get; set; }
    }
}
