namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SelfGas_Ban
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string CaseNo { get; set; }

        [Required]
        [StringLength(10)]
        public string BanCaseNo { get; set; }

        [StringLength(10)]
        public string AcceptPenaltor { get; set; }

        [StringLength(10)]
        public string IdNo { get; set; }

        [StringLength(5)]
        public string AddressValue { get; set; }

        [StringLength(3)]
        public string AreaNo { get; set; }

        [StringLength(70)]
        public string Address { get; set; }

        public DateTime? IllegalDate { get; set; }

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

        public int? PenaltyMoney { get; set; }

        [StringLength(50)]
        public string DocumentNo { get; set; }

        public DateTime? SendDate { get; set; }

        public DateTime? ExecutiveDate { get; set; }

        [StringLength(5)]
        public string PayValue { get; set; }

        public int? OncePayMoney { get; set; }

        public int? ManyPayMoney { get; set; }

        public int? TotalMoney { get; set; }

        public DateTime? PayDeadLine { get; set; }

        public DateTime? PleadStartDate { get; set; }

        public DateTime? PleadEndDate { get; set; }

        [StringLength(20)]
        public string PleadNo { get; set; }

        [StringLength(5)]
        public string PleadMain { get; set; }

        public DateTime? LitigationStartDate { get; set; }

        public DateTime? LitigationEndDate { get; set; }

        [StringLength(20)]
        public string LitigationNo { get; set; }

        [StringLength(5)]
        public string LitigationMain { get; set; }

        public bool? IsConfirm { get; set; }

        [StringLength(10)]
        public string CreateUserTemp { get; set; }

        public DateTime? CreateTime { get; set; }

        [StringLength(10)]
        public string CreateUser { get; set; }

        public DateTime? ModifyTime { get; set; }

        [StringLength(10)]
        public string LastModifyUser { get; set; }

        [StringLength(50)]
        public string FileUploadId { get; set; }

        public int? OweMoney { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        public DateTime? OncePayMoneyDate { get; set; }
    }
}
