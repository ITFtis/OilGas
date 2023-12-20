namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Counseling_Rate_City
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(4)]
        public string workYear { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string workItem { get; set; }

        [StringLength(50)]
        public string CityCode { get; set; }

        public int? Rank { get; set; }

        [StringLength(10)]
        public string GSLCode { get; set; }

        public int? AttendPCount { get; set; }

        public int? AttendSCount { get; set; }

        public int? DenominatorCount { get; set; }

        public decimal? Average1 { get; set; }

        public decimal? Average2 { get; set; }
    }
}
