namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Administration")]
    public partial class Administration
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string GasCaseNo { get; set; }

        [StringLength(50)]
        public string IllegalCaseNo { get; set; }

        public DateTime? ReceiveDate { get; set; }

        [StringLength(50)]
        public string Undertaker { get; set; }

        [StringLength(10)]
        public string Illegaler { get; set; }

        [StringLength(10)]
        public string IdNo { get; set; }

        [StringLength(5)]
        public string City { get; set; }

        [StringLength(50)]
        public string GasName { get; set; }

        [StringLength(3)]
        public string GasZip { get; set; }

        [StringLength(100)]
        public string GasAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string BusinessOrganization { get; set; }

        [StringLength(50)]
        public string OtherBusinessOrganization { get; set; }

        [StringLength(50)]
        public string LicenseNo { get; set; }

        [StringLength(5)]
        public string LicenseNoA { get; set; }

        [StringLength(5)]
        public string LicenseNoB { get; set; }

        public DateTime? PleadStartDate { get; set; }

        public DateTime? PleadEndDate { get; set; }

        [StringLength(10)]
        public string PleadMain { get; set; }

        public DateTime? LitigationStartDate { get; set; }

        public DateTime? LitigationEndDate { get; set; }

        [StringLength(30)]
        public string LitigationMain { get; set; }

        public int? Change { get; set; }

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
