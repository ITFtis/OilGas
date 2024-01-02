namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using static OilGas.Controllers.basicController;

    public partial class CarFuel_BasicData : UsageStatebasic
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public long ID { get; set; }

        [Key]
        [Column(Order = 2)]
        [ColumnDef(Filter = true)]
        [Display(Name = "案件編號", Order = 1)]
        [StringLength(10)]
        public string CaseNo { get; set; }





        [Display(Name = "縣市", Order = 1)]
        [ColumnDef(Filter = true,Visible =false,VisibleEdit =false, EditType = EditType.Select,
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




        [StringLength(70)]
        [ColumnDef(Filter = true)]
        [Display(Name = "加油站名稱", Order = 2)]
        public string Gas_Name { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "核發證號日期", Order = 11)]
        public DateTime? Report_date { get; set; }

        [Display(Name = "收件日期", Order = 6)]
        [ColumnDef(Filter = true,FilterAssign = FilterAssignType.Between)]
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
        [Display(Name = "之", Order =15)]//證號3
        public string LicenseNo3 { get; set; }

        [StringLength(20)]
        [ColumnDef(Filter = true,Sortable = true, EditType = EditType.Select,
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
        [ColumnDef(Visible = false,Sortable = true, EditType = EditType.Select,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_SaleSoilClass, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
        [Display(Name = "油品供應商", Order =16)]
        public string SoilServerData { get; set; }

        [StringLength(20)]
        [ColumnDef(Visible = false,VisibleEdit =false)]
        [Display(Name = "其他油品供應商", Order = 17)]
        public string otherSoilServerData { get; set; }

        [StringLength(10)]
        [ColumnDef(Filter =true,VisibleEdit = false,Visible =true, EditType = EditType.Select,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.UsageStateCode, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "ShortName")]
        [Display(Name = "營運狀況", Order = 7)]
        public string UsageState { get; set; }

        //[StringLength(10)]
        //[Required]
        //[ColumnDef(Visible = false, Sortable = true, EditType = EditType.Select,
        // SelectGearingWith = "UsageState2,Parent,true",
        // SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
        //  SelectSourceModelNamespace = "OilGas.Models.UsageStateCode1, OilGas",
        //  SelectSourceModelValueField = "Value",
        //  SelectSourceModelDisplayField = "Name")]
        //[Display(Name = "營運狀況(選單1)", Order = 18)]
        //public string UsageState1 { get; set; }

        //[StringLength(10)]
        //[ColumnDef(Visible = false, Sortable = true, EditType = EditType.Select, SelectGearingWith = "UsageState3,Parent,true", 
        // SelectItemsClassNamespace = UsageState2SelectItemsClassImp.AssemblyQualifiedName)]
        //[Display(Name = "營運狀況(選單2)", Order = 19)]
        //public string UsageState2 { get; set; }

        //[StringLength(10)]
        //[ColumnDef(Visible = false, Sortable = true, EditType = EditType.Select, SelectGearingWith = "UsageState4,Parent,true", 
        //  SelectItemsClassNamespace = UsageState3SelectItemsClassImp.AssemblyQualifiedName)]
        //[Display(Name = "營運狀況(選單3)", Order = 20)]
        //public string UsageState3 { get; set; }

        //[StringLength(10)]
        //[ColumnDef(Visible = false, Sortable = true, EditType = EditType.Select, SelectGearingWith = "UsageState5,Parent,true",
        // SelectItemsClassNamespace = UsageState4SelectItemsClassImp.AssemblyQualifiedName)]
        //[Display(Name = "營運狀況(選單4)", Order = 21)]
        //public string UsageState4 { get; set; }

        //[StringLength(10)]
        //[ColumnDef(Visible = false, Sortable = true, EditType = EditType.Select, SelectGearingWith = "UsageState6,Parent,true",
        // SelectItemsClassNamespace = UsageState5SelectItemsClassImp.AssemblyQualifiedName)]
        //[Display(Name = "營運狀況(選單5)", Order = 22)]
        //public string UsageState5 { get; set; }

        //[StringLength(10)]
        //[ColumnDef(Visible = false, Sortable = true, EditType = EditType.Select, SelectGearingWith = "UsageState7,Parent,true", 
        // SelectItemsClassNamespace = UsageState6SelectItemsClassImp.AssemblyQualifiedName)]
        //[Display(Name = "營運狀況(選單6)", Order = 23)]
        //public string UsageState6 { get; set; }

        //[StringLength(10)]
        //[ColumnDef(Visible = false, Sortable = true, EditType = EditType.Select, 
        // SelectItemsClassNamespace = UsageState7SelectItemsClassImp.AssemblyQualifiedName)]
        //[Display(Name = "營運狀況(選單7)", Order = 24)]
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
        [ColumnDef(Visible = false,VisibleEdit =false)]       
        public string Officials { get; set; }

        [StringLength(20)]
        [ColumnDef(Visible = false)]
        [Display(Name = "加油站電話", Order = 48)]
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
        [Display(Name = "加油站地號", Order = 51)]
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

        [StringLength(50)]
        [ColumnDef(Visible = false,  EditType = EditType.Radio,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.AncillaryFacility, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
        [Display(Name = "附屬設施", Order = 58)]
        public string AncillaryFacility { get; set; }

        [StringLength(100)]
        [ColumnDef(Visible = false, EditType = EditType.Radio,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_Facility, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
        [Display(Name = "兼營設施", Order = 59)]
        public string Facility { get; set; }

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
        [ColumnDef(Visible = false,VisibleEdit =false)]
        [Display(Name = "用地類別", Order = 66)]
        public string LandClass { get; set; }

        [StringLength(50)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        [Display(Name = "其他用地類別", Order = 67)]
        public string OtherLandClass { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false)]
        [Display(Name = "平面配置圖", Order = 90)]
        public string File_name { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "油槽總數", Order = 68)]
        public int? Tank { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加油泵島數", Order = 67)]
        public int? Island { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加油機數:單槍", Order = 69)]
        public int? one_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加油機數:雙槍", Order = 70)]
        public int? two_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加油機數: 四槍", Order = 71)]
        public int? four_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加油機數:六槍", Order = 72)]
        public int? six_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加油機數: 八槍", Order = 73)]
        public int? eight_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加油機數: 其他", Order = 74)]
        public int? other_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加油機數:共計", Order =75)]
        public int? total_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "自助加油機數:單槍", Order =76)]
        public int? Self_one_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "自助加油機數:雙槍", Order = 77)]
        public int? Self_two_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "自助加油機數: 四槍", Order =78)]
        public int? Self_four_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "自助加油機數:六槍", Order = 79)]
        public int? Self_six_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "自助加油機數: 八槍", Order = 80)]
        public int? Self_eight_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "自助加油機數: 其他", Order = 81)]
        public int? Self_other_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "自助加油機數:共計", Order = 82)]
        public int? Self_total_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加油機及自助加油機數:單槍", Order = 83)]
        public int? total_one_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加油機及自助加油機數:雙槍", Order = 84)]
        public int? total_two_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加油機及自助加油機數: 四槍", Order = 85)]
        public int? total_four_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加油機及自助加油機數:六槍", Order = 86)]
        public int? total_six_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加油機及自助加油機數: 八槍", Order = 87)]
        public int? total_eight_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加油機及自助加油機數: 其他", Order = 88)]
        public int? total_other_gun { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "加油機及自助加油機數:共計", Order = 89)]
        public int? total_total_gun { get; set; }

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
        [ColumnDef(Visible = false,VisibleEdit =false)]
        [Display(Name = "修改人帳號", Order = 95)]
        public string MemberID { get; set; }

        [ColumnDef(Visible = false,VisibleEdit =false)]
        public int? Change { get; set; }

        [StringLength(80)]
        [ColumnDef(Visible = false)]
        [Display(Name = "其他兼營設施", Order = 60)]
        public string otherFacility { get; set; }

        [StringLength(50)]
        [Display(Name = "營業中變更", Order = 47)]
        [ColumnDef(Visible = false,  EditType = EditType.Select, SelectItems = "{\"1\":\"經營許可執照\",\"2\":\"加油站平面配置\",\"3\":\"營運設施\",\"4\":\"兼營事項\",\"5\":\"附屬設施\"}")]
        public string UsageStateSub { get; set; }


        [ColumnDef(Visible = false)]
        [Display(Name = "換發證號日期", Order = 12)]
        public DateTime? ChangeReport_date { get; set; }

        [StringLength(20)]
        [ColumnDef(Visible = false,VisibleEdit =false)]
        [Display(Name = "X", Order = 96)]
        public string Longitude_E { get; set; }

        [StringLength(20)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        [Display(Name = "Y", Order = 97)]
        public string Longitude_N { get; set; }





















        public virtual IEnumerable<CarFuel_Insurance> CarFuel_Insurance
        {
            get
            {
                var detail = OilGas.Models.CarFuel_Insurance.GetAllCarFuel_Insurance().Where(a => a.CaseNo == this.CaseNo);
                return detail;
            }
        }




    }

    public class UsageState2SelectItemsClassImp : Dou.Misc.Attr.SelectItemsClass
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();
        public const string AssemblyQualifiedName = "OilGas.Models.UsageState2SelectItemsClassImp,OilGas";

     
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            //var UsageStateCode2s = GetAllUsageStateCode2().ToArray();
             var UsageStateCode2s = db.UsageStateCode2.ToArray();
            return UsageStateCode2s.Select(s => new KeyValuePair<string, object>(s.Value, "{\"v\":\"" + s.Name + "\",\"Parent\":\"" + s.Parent + "\"}"));
        }




       // static object lockGetAllCarFuel_Insurance = new object();
        //public static IEnumerable<UsageStateCode2> GetAllUsageStateCode2(int cachetimer = 0)
        //{
        //    if (cachetimer == 0) cachetimer = Constant.cacheTime;

        //    string key = "OilGas.Models.UsageStateCode2";

        //    var UsageStateCode2 = DouHelper.Misc.GetCache<IEnumerable<UsageStateCode2>>(cachetimer, key);

        //    lock (lockGetAllCarFuel_Insurance)
        //    {
        //        if (UsageStateCode2 == null)
        //        {
                   
        //            UsageStateCode2 = db.UsageStateCode2.ToList();

        //            DouHelper.Misc.AddCache(UsageStateCode2, key);
        //        }
        //    }

        //    return UsageStateCode2;
        //}
    }
    public class UsageState3SelectItemsClassImp : UsageState2SelectItemsClassImp
    {        
        public new const string AssemblyQualifiedName = "OilGas.Models.UsageState3SelectItemsClassImp,OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {            
            var UsageStateCodes = db.UsageStateCode3.ToArray();
            return UsageStateCodes.Select(s => new KeyValuePair<string, object>(s.Value, "{\"v\":\"" + s.Name + "\",\"Parent\":\"" + s.Parent + "\"}"));
        }
    }

    public class UsageState4SelectItemsClassImp : UsageState2SelectItemsClassImp
    {
        public new const string AssemblyQualifiedName = "OilGas.Models.UsageState4SelectItemsClassImp,OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var UsageStateCodes = db.UsageStateCode4.ToArray();
            return UsageStateCodes.Select(s => new KeyValuePair<string, object>(s.Value, "{\"v\":\"" + s.Name + "\",\"Parent\":\"" + s.Parent + "\"}"));
        }
    }
    public class UsageState5SelectItemsClassImp : UsageState2SelectItemsClassImp
    {
        public new const string AssemblyQualifiedName = "OilGas.Models.UsageState5SelectItemsClassImp,OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var UsageStateCodes = db.UsageStateCode5.ToArray();
            return UsageStateCodes.Select(s => new KeyValuePair<string, object>(s.Value, "{\"v\":\"" + s.Name + "\",\"Parent\":\"" + s.Parent + "\"}"));
        }
    }
    public class UsageState6SelectItemsClassImp : UsageState2SelectItemsClassImp
    {
        public new const string AssemblyQualifiedName = "OilGas.Models.UsageState6SelectItemsClassImp,OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var UsageStateCodes = db.UsageStateCode6.ToArray();
            return UsageStateCodes.Select(s => new KeyValuePair<string, object>(s.Value, "{\"v\":\"" + s.Name + "\",\"Parent\":\"" + s.Parent + "\"}"));
        }
    }
    public class UsageState7SelectItemsClassImp : UsageState2SelectItemsClassImp
    {
        public new const string AssemblyQualifiedName = "OilGas.Models.UsageState7SelectItemsClassImp,OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var UsageStateCodes = db.UsageStateCode7.ToArray();
            return UsageStateCodes.Select(s => new KeyValuePair<string, object>(s.Value, "{\"v\":\"" + s.Name + "\",\"Parent\":\"" + s.Parent + "\"}"));
        }
    }

    public class CarFuel_BasicDataCaseNoSelectItems : Dou.Misc.Attr.SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Models.CarFuel_BasicDataCaseNoSelectItems, OilGas";

        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            Dou.Models.DB.IModelEntity<CarFuel_BasicData> carFuel_BasicData = new Dou.Models.DB.ModelEntity<CarFuel_BasicData>(new OilGasModelContextExt());
            var v = carFuel_BasicData.GetAll().Select(a => new { CaseNo = a.CaseNo.Trim(), Gas_Name = a.Gas_Name.Trim() })
                        .Distinct().ToList();

            return v.Select(s => new KeyValuePair<string, object>(s.CaseNo, JsonConvert.SerializeObject(new { v = s.CaseNo, s = s.CaseNo, Gas_Name = s.Gas_Name })));
        }
    }

    public class CarFuel_BasicDataGas_NameSelectItems : Dou.Misc.Attr.SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Models.CarFuel_BasicDataGas_NameSelectItems, OilGas";

        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            Dou.Models.DB.IModelEntity<CarFuel_BasicData> carFuel_BasicData = new Dou.Models.DB.ModelEntity<CarFuel_BasicData>(new OilGasModelContextExt());
            var v = carFuel_BasicData.GetAll().Select(a => new { CaseNo = a.CaseNo.Trim(), Gas_Name = a.Gas_Name.Trim() })                        
                        .Distinct().ToList();

            return v.Select(s => new KeyValuePair<string, object>(s.CaseNo, JsonConvert.SerializeObject(new { v = s.Gas_Name, s = s.Gas_Name, CaseNo = s.CaseNo })));
        }
    }
}
