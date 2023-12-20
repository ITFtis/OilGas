namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SelfFuel_Basic_Log
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CaseNo { get; set; }

        [StringLength(70)]
        public string FuelName { get; set; }

        [StringLength(30)]
        public string BusiOrg { get; set; }

        [StringLength(2)]
        public string UsageState { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(2)]
        public string Facility { get; set; }

        [StringLength(2)]
        public string FacilityDetail { get; set; }

        [StringLength(20)]
        public string FacilityOther { get; set; }

        [StringLength(20)]
        public string FacilityBase { get; set; }

        [StringLength(25)]
        public string Responsor { get; set; }

        [StringLength(20)]
        public string FacilityPhone { get; set; }

        [StringLength(20)]
        public string Email { get; set; }

        [StringLength(10)]
        public string IdNo { get; set; }

        [StringLength(3)]
        public string AreaNo { get; set; }

        [StringLength(70)]
        public string Address { get; set; }

        [StringLength(300)]
        public string AddressNo { get; set; }

        public DateTime? CreateTime { get; set; }

        [StringLength(10)]
        public string CreateUser { get; set; }

        [StringLength(10)]
        public string CreateUserTemp { get; set; }

        public DateTime? ModifyTime { get; set; }

        [StringLength(10)]
        public string ModifyUser { get; set; }

        public DateTime? DeleteTime { get; set; }

        [StringLength(10)]
        public string DeleteUser { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        public bool? IsConfirm { get; set; }

        [StringLength(2)]
        public string UsageState_Second { get; set; }

        [StringLength(2)]
        public string UsageState_Third { get; set; }

        public DateTime? AuthorizedDate { get; set; }

        public DateTime? ExpiredDate { get; set; }

        [StringLength(60)]
        public string Ip { get; set; }

        [StringLength(10)]
        public string Action { get; set; }

        [StringLength(2)]
        public string UsageState_Fourth { get; set; }

        [StringLength(30)]
        public string LicenseNo { get; set; }

        public int? Change { get; set; }

        [StringLength(20)]
        public string Longitude_E { get; set; }

        [StringLength(20)]
        public string Longitude_N { get; set; }
    }
}
