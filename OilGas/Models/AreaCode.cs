namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AreaCode")]
    public partial class AreaCode
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public string CityCode { get; set; }

        [Key]
        [Column("AreaCode", Order = 1)]
        [StringLength(10)]
        public string AreaCode1 { get; set; }

        [StringLength(5)]
        public string AreaName { get; set; }

        public int? Rank { get; set; }

        [StringLength(10)]
        public string GSLCode { get; set; }
    }
}
