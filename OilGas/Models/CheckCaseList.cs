namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CheckCaseList")]
    public partial class CheckCaseList
    {
        public int id { get; set; }

        [StringLength(20)]
        public string CaseType { get; set; }

        [StringLength(20)]
        public string CaseNo { get; set; }

        [StringLength(50)]
        public string Gas_Name { get; set; }

        [StringLength(50)]
        public string Business_theme { get; set; }

        [StringLength(5)]
        public string ZipCode { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(10)]
        public string LastCheckNo { get; set; }

        [StringLength(4)]
        public string LastCheckYaer { get; set; }

        [StringLength(10)]
        public string LastCheckNo2 { get; set; }

        [StringLength(4)]
        public string LastCheckYaer2 { get; set; }

        [StringLength(2)]
        public string Case_Lev { get; set; }

        public int? Case_CheckErrCount { get; set; }

        [StringLength(4)]
        public string End_CheckYaer { get; set; }

        public int? CheckType1 { get; set; }

        public int? CheckType2 { get; set; }

        public int? CheckType3 { get; set; }

        public int? CheckType4 { get; set; }

        public int? CheckType5 { get; set; }

        public int? CheckType6 { get; set; }

        public int? CheckType7 { get; set; }

        public int? CheckType8 { get; set; }

        public int? CheckTypeN1 { get; set; }

        public int? CheckTypeN2 { get; set; }

        public int? CheckTypeN3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Well_LEL { get; set; }

        [StringLength(10)]
        public string uType { get; set; }

        [StringLength(50)]
        public string UsageState_Name { get; set; }

        public bool? IsCheck { get; set; }
    }
}
