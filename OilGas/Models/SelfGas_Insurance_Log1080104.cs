namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SelfGas_Insurance_Log1080104
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
        [StringLength(30)]
        public string InsuranceNo { get; set; }

        [StringLength(2)]
        public string InsuranceCompanyName { get; set; }

        [StringLength(20)]
        public string OtherInsuranceCompanyName { get; set; }

        public DateTime? InsuranceValidateStartDate { get; set; }

        public DateTime? InsuranceValidateEndDate { get; set; }

        [StringLength(10)]
        public string InsuranceType { get; set; }

        [StringLength(10)]
        public string CreateUserTemp { get; set; }

        public bool? IsConfirm { get; set; }

        public DateTime? ModifyTime { get; set; }

        [StringLength(10)]
        public string ModifyUser { get; set; }

        public DateTime? DeleteTime { get; set; }

        [StringLength(10)]
        public string DeleteUser { get; set; }

        [StringLength(20)]
        public string Ip { get; set; }

        [StringLength(10)]
        public string Action { get; set; }

        public int? Change { get; set; }
    }
}
