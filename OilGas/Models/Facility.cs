namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Facility")]
    public partial class Facility
    {
        [StringLength(10)]
        public string Name { get; set; }

        [Key]
        [StringLength(10)]
        public string Value { get; set; }

        public byte? Rank { get; set; }

     
    }
}
