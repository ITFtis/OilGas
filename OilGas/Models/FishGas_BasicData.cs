namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using static OilGas.Controllers.basicController;

    public partial class FishGas_BasicData : UsageStatebasic
    {
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public long ID { get; set; }

        [StringLength(10)]
        [ColumnDef(Filter = true)]
        [Display(Name = "案件編號", Order = 1)]
        [Required]
        public  string CaseNo { get; set; }

        [Display(Name = "縣市", Order = 1)]
        [ColumnDef(Filter = true, Visible = false, VisibleEdit = false, EditType = EditType.Select,
    SelectItemsClassNamespace = UsercitySelectItemsClassImp.AssemblyQualifiedName)]
        [StringLength(10)]
        [NotMapped]
        public string CITY
        {
            get
            {
                if (CaseNo != null && CaseNo.Length > 6)
                {
                    return CaseNo.Substring(4, 2);
                }
                else
                {
                    return CaseNo;
                }
            }
            set
            {
            }
        }

        [Display(Name = "油氣設施類型")]
     //   [ColumnDef( Visible = false, VisibleEdit = false, EditType = EditType.Select,
     //SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
     // SelectSourceModelNamespace = "OilGas.Models.FacilityType, OilGas",
     // SelectSourceModelValueField = "Value",
     // SelectSourceModelDisplayField = "Name")]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(20)]
        public string Facility_Type { get; set; }

        [StringLength(70)]
        [ColumnDef(Filter = true)]
        [Display(Name = "加氣站名稱", Order = 2)]
        public string Gas_Name { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "核發證號日期", Order = 11)]
        public DateTime? Report_date { get; set; }

        [Display(Name = "收件日期", Order = 6)]
        [ColumnDef(Filter = true, FilterAssign = FilterAssignType.Between)]
        public DateTime? Recipient_date { get; set; }

        [StringLength(20)]
        [ColumnDef(Visible = false)]
        [Display(Name = "證號", Order = 13)]//證號1
        public string LicenseNo1 { get; set; }

        [StringLength(20)]
        [ColumnDef(Visible = false)]
        [Display(Name = "字第", Order = 14)]//證號2
        public string LicenseNo2 { get; set; }

        [StringLength(20)]
        [ColumnDef(Visible = false)]
        [Display(Name = "之", Order = 15)]//證號3
        public string LicenseNo3 { get; set; }

        [StringLength(20)]
        [ColumnDef(Filter = true, Sortable = true, EditType = EditType.Select,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_BusinessOrganization, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
        [Display(Name = "營業主體", Order = 3)]
        public string Business_theme { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false)]
        [Display(Name = "其他營業主體", Order = 4)]
        public string otherBusiness_theme { get; set; }

        [StringLength(20)]
        [ColumnDef(Visible = false, Sortable = true, EditType = EditType.Select,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_SaleSoilClass, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
        [Display(Name = "油品供應商", Order = 16)]
        public string SoilServerData { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        [Display(Name = "其他油品供應商", Order = 17)]
        public string otherSoilServerData { get; set; }

        [StringLength(10)]
        [ColumnDef(Filter = true, VisibleEdit = false, Visible = true, EditType = EditType.Select,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.UsageStateCode, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "ShortName")]
        [Display(Name = "營運狀況", Order = 7)]
        public string UsageState { get; set; }

        //[StringLength(10)]
        //public string UsageState1 { get; set; }

        //[StringLength(10)]
        //public string UsageState2 { get; set; }

        //[StringLength(10)]
        //public string UsageState3 { get; set; }

        //[StringLength(10)]
        //public string UsageState4 { get; set; }

        //[StringLength(10)]
        //public string UsageState5 { get; set; }

        //[StringLength(10)]
        //public string UsageState6 { get; set; }

        //[StringLength(10)]
        //public string UsageState7 { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "同意認定期限", Order = 25)]
        public DateTime? AgreeDate { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "同意籌建期限", Order = 26)]
        public DateTime? Build_Deadline { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "同意籌建展延次數", Order = 27)]
        public int? ExtensionCount1 { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "同意籌建開始日期", Order = 28)]
        public DateTime? ExtensionDateStart1 { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "同意籌建結束日期", Order = 29)]
        public DateTime? ExtensionDateEnd1 { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "申請開業展延次數", Order = 30)]
        public int? ExtensionCount2 { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "申請開業展延開始日期", Order = 31)]
        public DateTime? ExtensionDateStart2 { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "申請開業展延結束日期", Order = 32)]
        public DateTime? ExtensionDateEnd2 { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "核發經營許可執照展延次數", Order = 33)]
        public int? ExtensionCount3 { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "核發經營許可執照展延延開始日期", Order = 34)]
        public DateTime? ExtensionDateStart3 { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "核發經營許可執照展延延結束日期", Order = 35)]
        public DateTime? ExtensionDateEnd3 { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "申請暫停營業展延次數", Order = 36)]
        public int? ExtensionCount4 { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "申請暫停營業展延次數開始日期", Order = 37)]
        public DateTime? ExtensionDateStart4 { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "申請暫停營業展延次數結束日期", Order = 38)]
        public DateTime? ExtensionDateEnd4 { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "申請開業展延次數", Order = 39)]
        public int? ExtensionCount5 { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "申請復業展延開始日期", Order = 40)]
        public DateTime? ExtensionDateStart5 { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "申請復業展延結束日期", Order = 41)]
        public DateTime? ExtensionDateEnd5 { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "籌建完成日期", Order = 42)]
        public DateTime? BuildDate { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "營運日期", Order = 43)]
        public DateTime? OperationDate { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "進入法拍作業程序日期", Order = 44)]
        public DateTime? ForeclosureDate { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "同意暫停歇業日期", Order = 45)]
        public DateTime? ClosedDate { get; set; }


        [ColumnDef(Visible = false)]
        [Display(Name = "同意歇業日期", Order = 46)]
        public DateTime? StopDate { get; set; }

        [StringLength(25)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Officials { get; set; }

        [StringLength(20)]
        [ColumnDef(Visible = false)]
        [Display(Name = "加氣站電話", Order = 48)]
        public string TelNo { get; set; }

        [StringLength(5)]
        [ColumnDef(Visible = false)]
        [Display(Name = "郵遞區號", Order = 49)]
        public string ZipCode { get; set; }

        [StringLength(100)]
        [ColumnDef(Filter = true, FilterAssign = FilterAssignType.Contains)]
        [Display(Name = "地址", Order = 50)]
        public string Address { get; set; }

        [StringLength(200)]
        [ColumnDef(Visible = false)]
        [Display(Name = "加氣站地號", Order = 51)]
        public string AddressNo { get; set; }

        [StringLength(25)]
        [ColumnDef(Visible = false)]
        [Display(Name = "負責人姓名", Order = 52)]
        public string Boss { get; set; }

        [StringLength(10)]
        [ColumnDef(Visible = false)]
        [Display(Name = "負責人身份證字號", Order = 53)]
        public string ID_No { get; set; }

        [StringLength(5)]
        [ColumnDef(Visible = false)]
        [Display(Name = "負責人郵遞區號", Order = 54)]
        public string ZipCode2 { get; set; }

        [StringLength(100)]
        [ColumnDef(Visible = false)]
        [Display(Name = "負責人聯絡地址", Order = 55)]
        public string Address2 { get; set; }

        [StringLength(20)]
        [ColumnDef(Visible = false)]
        [Display(Name = "負責人聯絡電話", Order = 56)]
        public string Boss_Tel { get; set; }

        [StringLength(50)]
        [ColumnDef(Visible = false, EditType = EditType.Email)]
        [Display(Name = "電子郵件信箱", Order = 57)]
        public string Boss_Email { get; set; }


        [StringLength(10)]
        [ColumnDef(Visible = false, Sortable = true, EditType = EditType.Select,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_LandPriority, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
        [Display(Name = "土地權屬", Order = 61)]
        public string LandPriority { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "用地總面積", Order = 62)]
        public double? Land_acreage { get; set; }

        [StringLength(10)]
        [ColumnDef(Visible = false, Sortable = true, EditType = EditType.Select,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.LandClassCode, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
        [Display(Name = "土地使用分區", Order = 63)]
        public string LandType { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false, Sortable = true, EditType = EditType.Select,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.LandUsageZoneCode, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
        [Display(Name = "土地使用分區(選單2)", Order = 64)]
        public string LandUsageZone { get; set; }

        [StringLength(50)]
        [ColumnDef(Visible = false, Sortable = true, EditType = EditType.Select,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.LandClassCode, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
        [Display(Name = "土地使用分區(選單3)", Order = 65)]
        public string OtherLandUsageZone { get; set; }

        [StringLength(50)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        [Display(Name = "用地類別", Order = 66)]
        public string LandClass { get; set; }

        [StringLength(50)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        [Display(Name = "其他用地類別", Order = 67)]
        public string OtherLandClass { get; set; }

        [ColumnDef(Visible = true, Sortable = true, EditType = EditType.Select,
       SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
        SelectSourceModelNamespace = "OilGas.Models.SeaportZone, OilGas",
        SelectSourceModelValueField = "Value",
        SelectSourceModelDisplayField = "Name")]
        [Display(Name = "設站港區", Order = 67)]
        [StringLength(30)]
        public string SeaportZone { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false)]
        [Display(Name = "平面配置圖", Order = 90)]
        public string File_name { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "油槽總數", Order = 68)]
        public int? Tank { get; set; }
        [ColumnDef(Visible = false)]
        [Display(Name = "流量計", Order = 68)]
        public int? Flowmeter { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加氣機數:單槍", Order = 69)]
        public int? one_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加氣機數:雙槍", Order = 70)]
        public int? two_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加氣機數: 四槍", Order = 71)]
        public int? four_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加氣機數:六槍", Order = 72)]
        public int? six_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加氣機數: 八槍", Order = 73)]
        public int? eight_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加氣機數: 其他", Order = 74)]
        public int? other_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加氣機數:共計", Order = 75)]
        public int? total_gun { get; set; }

        [Display(Name = "油駁船加油", Order = 90)]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItems = "{\"是\":\"是\",\"否\":\"否\"}")]
        [StringLength(10)]
        public string Oil_barge { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "消防安全措施", Order = 90)]
        [StringLength(50)]
        public string Fire_Safety { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "污染防治措施", Order = 90)]
        [StringLength(50)]
        public string Pollution_Prevention { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "資料建立時間", Order = 90)]
        public DateTime? Create_date { get; set; }

        [StringLength(25)]
        [ColumnDef(Visible = false)]
        [Display(Name = "資料建立者", Order = 91)]
        public string Create_name { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "資料修改時間", Order = 92)]
        public DateTime? Mod_date { get; set; }

        [StringLength(25)]
        [ColumnDef(Visible = false)]
        [Display(Name = "最後修改者", Order = 93)]
        public string Mod_name { get; set; }

        [StringLength(200)]
        [ColumnDef(Visible = false)]
        [Display(Name = "備註", Order = 94)]
        public string Note2 { get; set; }

        [StringLength(52)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        [Display(Name = "修改人帳號", Order = 95)]
        public string MemberID { get; set; }


        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }

        [StringLength(50)]
        [Display(Name = "營業中變更", Order = 47)]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectItems = "{\"1\":\"經營許可執照\",\"2\":\"加氣站平面配置\",\"3\":\"營運設施\",\"4\":\"兼營事項\",\"5\":\"附屬設施\"}")]
        public string UsageStateSub { get; set; }


        [ColumnDef(Visible = false)]
        [Display(Name = "換發證號日期", Order = 12)]
        public DateTime? ChangeReport_date { get; set; }

        [StringLength(20)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        [Display(Name = "X", Order = 96)]
        public string Longitude_E { get; set; }

        [StringLength(20)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        [Display(Name = "Y", Order = 97)]
        public string Longitude_N { get; set; }
    }
}
