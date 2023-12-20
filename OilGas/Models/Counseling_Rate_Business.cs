namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Counseling_Rate_Business
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(4)]
        public string workYear { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string workItem { get; set; }

        [StringLength(20)]
        public string workCode { get; set; }

        public int? Rank { get; set; }

        [StringLength(4)]
        public string ShortName { get; set; }

        [StringLength(2)]
        public string OldCode { get; set; }

        public int? AttendPCount { get; set; }

        public int? AttendSCount { get; set; }

        public int? DenominatorCount { get; set; }

        public decimal? Average1 { get; set; }

        public decimal? Average2 { get; set; }
    }
}
