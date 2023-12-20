namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PortGas_BasicData_Temp
    {
        [Key]
        public long TID { get; set; }

        [StringLength(20)]
        public string CaseNo { get; set; }

        [StringLength(50)]
        public string ApparatusOwner { get; set; }

        [StringLength(25)]
        public string LocationType { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        [StringLength(200)]
        public string Gas_Name { get; set; }

        [StringLength(200)]
        public string Gas_Location { get; set; }

        public bool? BusinessType1 { get; set; }

        public bool? BusinessType2 { get; set; }

        public bool? BusinessType3 { get; set; }

        public bool? BusinessType4 { get; set; }

        [StringLength(20)]
        public string Report_date { get; set; }

        [StringLength(20)]
        public string Recipient_date { get; set; }

        [StringLength(20)]
        public string StopReport_date { get; set; }

        [StringLength(10)]
        public string LicenseNo1 { get; set; }

        [StringLength(50)]
        public string LicenseNo2 { get; set; }

        [StringLength(10)]
        public string LicenseNo3 { get; set; }

        [StringLength(255)]
        public string LicenseNote1 { get; set; }

        [StringLength(255)]
        public string LicenseNote2 { get; set; }

        [StringLength(255)]
        public string LicenseNote3 { get; set; }

        [StringLength(10)]
        public string UsageState { get; set; }

        [StringLength(10)]
        public string UsageState1 { get; set; }

        [StringLength(10)]
        public string UsageState2 { get; set; }

        [StringLength(10)]
        public string UsageState3 { get; set; }

        [StringLength(10)]
        public string UsageState4 { get; set; }

        [StringLength(10)]
        public string UsageState5 { get; set; }

        [StringLength(25)]
        public string Boss { get; set; }

        [StringLength(10)]
        public string Boss_ID { get; set; }

        [StringLength(20)]
        public string Boss_Tel { get; set; }

        [StringLength(50)]
        public string Boss_Email { get; set; }

        [StringLength(5)]
        public string Boss_AreaNo { get; set; }

        [StringLength(100)]
        public string Boss_Address { get; set; }

        [StringLength(100)]
        public string Apply_Name { get; set; }

        [StringLength(20)]
        public string Apply_Tel { get; set; }

        [StringLength(5)]
        public string Apply_AreaNo { get; set; }

        [StringLength(100)]
        public string Apply_Address { get; set; }

        [StringLength(500)]
        public string BasicFacilities { get; set; }

        [StringLength(500)]
        public string OtherFacilities { get; set; }

        [StringLength(500)]
        public string AllFacilities { get; set; }

        [StringLength(200)]
        public string SupplyTarget { get; set; }

        [StringLength(30)]
        public string File_name { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Create_date { get; set; }

        [StringLength(25)]
        public string Create_name { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Mod_date { get; set; }

        [StringLength(25)]
        public string Mod_name { get; set; }

        [StringLength(200)]
        public string Note1 { get; set; }

        [StringLength(200)]
        public string Note2 { get; set; }

        [StringLength(200)]
        public string Note3 { get; set; }

        [StringLength(52)]
        public string MemberID { get; set; }

        public int? Change { get; set; }

        [StringLength(50)]
        public string UsageStateSub { get; set; }

        [StringLength(80)]
        public string otherFacility { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ChangeReport_date { get; set; }
    }
}
