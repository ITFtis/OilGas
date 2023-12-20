namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FishGas_BasicData_Temp
    {
        [Key]
        public long TID { get; set; }

        [StringLength(10)]
        public string CaseNo { get; set; }

        [StringLength(70)]
        public string Gas_Name { get; set; }

        public DateTime? Report_date { get; set; }

        public DateTime? Recipient_date { get; set; }

        [StringLength(20)]
        public string LicenseNo1 { get; set; }

        [StringLength(20)]
        public string LicenseNo2 { get; set; }

        [StringLength(20)]
        public string LicenseNo3 { get; set; }

        [StringLength(20)]
        public string Business_theme { get; set; }

        [StringLength(30)]
        public string otherBusiness_theme { get; set; }

        [StringLength(20)]
        public string SoilServerData { get; set; }

        [StringLength(20)]
        public string otherSoilServerData { get; set; }

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

        [StringLength(10)]
        public string UsageState6 { get; set; }

        [StringLength(10)]
        public string UsageState7 { get; set; }

        public DateTime? AgreeDate { get; set; }

        public DateTime? Build_Deadline { get; set; }

        public int? ExtensionCount1 { get; set; }

        public DateTime? ExtensionDateStart1 { get; set; }

        public DateTime? ExtensionDateEnd1 { get; set; }

        public int? ExtensionCount2 { get; set; }

        public DateTime? ExtensionDateStart2 { get; set; }

        public DateTime? ExtensionDateEnd2 { get; set; }

        public int? ExtensionCount3 { get; set; }

        public DateTime? ExtensionDateStart3 { get; set; }

        public DateTime? ExtensionDateEnd3 { get; set; }

        public int? ExtensionCount4 { get; set; }

        public DateTime? ExtensionDateStart4 { get; set; }

        public DateTime? ExtensionDateEnd4 { get; set; }

        public int? ExtensionCount5 { get; set; }

        public DateTime? ExtensionDateStart5 { get; set; }

        public DateTime? ExtensionDateEnd5 { get; set; }

        public DateTime? BuildDate { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? ForeclosureDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        public DateTime? StopDate { get; set; }

        [StringLength(25)]
        public string Officials { get; set; }

        [StringLength(20)]
        public string TelNo { get; set; }

        [StringLength(5)]
        public string ZipCode { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(200)]
        public string AddressNo { get; set; }

        [StringLength(25)]
        public string Boss { get; set; }

        [StringLength(10)]
        public string ID_No { get; set; }

        [StringLength(5)]
        public string ZipCode2 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(20)]
        public string Boss_Tel { get; set; }

        [StringLength(50)]
        public string Boss_Email { get; set; }

        [StringLength(10)]
        public string LandPriority { get; set; }

        public double? Land_acreage { get; set; }

        [StringLength(10)]
        public string LandType { get; set; }

        [StringLength(30)]
        public string LandUsageZone { get; set; }

        [StringLength(50)]
        public string OtherLandUsageZone { get; set; }

        [StringLength(50)]
        public string LandClass { get; set; }

        [StringLength(50)]
        public string OtherLandClass { get; set; }

        [StringLength(30)]
        public string SeaportZone { get; set; }

        [StringLength(30)]
        public string File_name { get; set; }

        public int? Tank { get; set; }

        public int? Flowmeter { get; set; }

        public int? one_gun { get; set; }

        public int? two_gun { get; set; }

        public int? four_gun { get; set; }

        public int? six_gun { get; set; }

        public int? eight_gun { get; set; }

        public int? other_gun { get; set; }

        public int? total_gun { get; set; }

        [StringLength(10)]
        public string Oil_barge { get; set; }

        [StringLength(50)]
        public string Fire_Safety { get; set; }

        [StringLength(50)]
        public string Pollution_Prevention { get; set; }

        public DateTime? Create_date { get; set; }

        [StringLength(25)]
        public string Create_name { get; set; }

        public DateTime? Mod_date { get; set; }

        [StringLength(25)]
        public string Mod_name { get; set; }

        [StringLength(200)]
        public string Note2 { get; set; }

        [StringLength(52)]
        public string MemberID { get; set; }

        public int? Change { get; set; }

        [StringLength(50)]
        public string UsageStateSub { get; set; }

        public DateTime? ChangeReport_date { get; set; }

        [StringLength(20)]
        public string Longitude_E { get; set; }

        [StringLength(20)]
        public string Longitude_N { get; set; }
    }
}
