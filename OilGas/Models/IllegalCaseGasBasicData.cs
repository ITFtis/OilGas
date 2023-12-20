namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IllegalCaseGasBasicData")]
    public partial class IllegalCaseGasBasicData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string IllegalCaseNo { get; set; }

        public DateTime? ReceiveDate { get; set; }

        [StringLength(50)]
        public string Undertaker { get; set; }

        [StringLength(10)]
        public string CaseState { get; set; }

        [StringLength(50)]
        public string GasName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string GasCaseNo { get; set; }

        public int? Change { get; set; }

        [StringLength(3)]
        public string GasZip { get; set; }

        [StringLength(100)]
        public string GasAddress { get; set; }

        [StringLength(50)]
        public string BusinessOrganization { get; set; }

        [StringLength(50)]
        public string OtherBusinessOrganization { get; set; }

        [StringLength(50)]
        public string LicenseNo { get; set; }

        public DateTime? DispatchDate { get; set; }

        [StringLength(50)]
        public string DisciplineNo { get; set; }

        public int? DisciplineMoney { get; set; }

        public DateTime? PayDeadLine { get; set; }

        public bool? IsPayStage { get; set; }

        [StringLength(100)]
        public string IllegalLaw { get; set; }

        [StringLength(200)]
        public string Reason { get; set; }

        [StringLength(50)]
        public string BusinessName { get; set; }

        [StringLength(50)]
        public string BusinessID { get; set; }

        public DateTime? BusinessBirthDate { get; set; }

        [StringLength(3)]
        public string BusinessZip { get; set; }

        [StringLength(100)]
        public string BusinessAddress { get; set; }

        [StringLength(50)]
        public string LegalAgentName { get; set; }

        [StringLength(50)]
        public string LegalAgentID { get; set; }

        public DateTime? LegalAgentBirthDate { get; set; }

        [StringLength(3)]
        public string LegalAgentZip { get; set; }

        [StringLength(100)]
        public string LegalAgentAddress { get; set; }

        [StringLength(50)]
        public string NoticeNo { get; set; }

        public DateTime? NoticeDate { get; set; }

        public DateTime? NoticeDeadLine { get; set; }

        [StringLength(20)]
        public string NoticeType { get; set; }

        [StringLength(50)]
        public string NoticeOtherType { get; set; }

        [StringLength(200)]
        public string NoticeNote { get; set; }

        [StringLength(50)]
        public string NoticeAlterUnderTaker { get; set; }

        public DateTime? NoticeAlterDate { get; set; }

        [StringLength(50)]
        public string EnforceNo { get; set; }

        public DateTime? EnforceDate { get; set; }

        [StringLength(200)]
        public string EnforceNote { get; set; }

        [StringLength(50)]
        public string EnforceAlterUnderTaker { get; set; }

        public DateTime? EnforceAlterDate { get; set; }

        public DateTime? CancelDate { get; set; }

        [StringLength(50)]
        public string CancelNo { get; set; }

        [StringLength(200)]
        public string CancelReason { get; set; }

        [StringLength(50)]
        public string CancelAlterUnderTaker { get; set; }

        public DateTime? CancelAlterDate { get; set; }

        [StringLength(20)]
        public string State { get; set; }

        [StringLength(5)]
        public string CreateUser { get; set; }

        public DateTime? CreateTime { get; set; }

        [StringLength(5)]
        public string ModifyUser { get; set; }

        public DateTime? ModifyTime { get; set; }
    }
}
