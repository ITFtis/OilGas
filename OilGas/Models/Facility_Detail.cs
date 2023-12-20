namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Facility_Detail")]
    public partial class Facility_Detail
    {
        [StringLength(10)]
        public string Name { get; set; }

        [Key]
        [StringLength(10)]
        public string Value { get; set; }

        public byte? Rank { get; set; }

        [StringLength(5)]
        public string Group { get; set; }


    }
}
