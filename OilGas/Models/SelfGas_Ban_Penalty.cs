namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SelfGas_Ban_Penalty
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CaseNo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string BanCaseNo { get; set; }

        [StringLength(5)]
        public string OilRuleA { get; set; }

        [StringLength(5)]
        public string OilRuleB { get; set; }

        [StringLength(5)]
        public string OilRuleC { get; set; }

        [StringLength(5)]
        public string SelfFuelRuleA { get; set; }

        [StringLength(5)]
        public string SelfFuelRuleB { get; set; }

        [StringLength(5)]
        public string SelfFuelRuleC { get; set; }

        public bool? IsConfirm { get; set; }
    }
}
