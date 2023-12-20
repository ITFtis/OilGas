namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using static OilGas.Controllers.basicController;

    public partial class PortGas_BasicData
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public long ID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        [ColumnDef(Filter = true)]
        [Display(Name = "案件編號", Order = 1)]
        public string CaseNo { get; set; }




        [StringLength(50)]
        [Display(Name = "設備使用主體", Order = 7)]
        public string ApparatusOwner { get; set; }


        [StringLength(25)]
        [Display(Name = "設施地點類型", Order = 3)]
        [ColumnDef(Filter = true,EditType = EditType.Select, SelectGearingWith = "Location,Parent,true", SelectItems = "{\"航空站\":\"航空站\",\"商港\":\"商港\",\"工業專用港\":\"工業專用港\"}")]
        public string LocationType { get; set; }


        [StringLength(50)]
        [Display(Name = "設施地點", Order = 4)]
        [ColumnDef(Filter = true,EditType = EditType.Select,
          SelectItemsClassNamespace = LocationSelectItemsClassImp.AssemblyQualifiedName)]
        public string Location { get; set; }


        [StringLength(200)]
        [ColumnDef(Filter = true)]
        [Display(Name = "設施名稱", Order = 5)]
        public string Gas_Name { get; set; }


        [StringLength(200)]
        [Display(Name = "設施所在地", Order = 6)]
        public string Gas_Location { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public bool? BusinessType1 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public bool? BusinessType2 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public bool? BusinessType3 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public bool? BusinessType4 { get; set; }

        [ColumnDef(Visible = false,EditType =EditType.Date)]
        [StringLength(20)]
        [Display(Name = "核准使用日期", Order = 18)]
        public string Report_date { get; set; }

        [ColumnDef(Visible = false, EditType = EditType.Date, Filter = true, FilterAssign = FilterAssignType.Between)]
        [StringLength(20)]
        [Display(Name = "收件日期", Order = 2)]
        public string Recipient_date { get; set; }

        [ColumnDef(Visible = false, EditType = EditType.Date)]
        [StringLength(20)]
        [Display(Name = "結束使用日期", Order = 19)]
        public string StopReport_date { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(10)]
        [Display(Name = "核准設置文號", Order = 20)]//核准設置文號(字)
        public string LicenseNo1 { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(50)]
        [Display(Name = "字 第", Order = 21)]//核准設置文號(號)
        public string LicenseNo2 { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(10)]
        [Display(Name = "使用狀況(選單6)", Order = 16)]
        public string LicenseNo3 { get; set; }


        [StringLength(255)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string LicenseNote1 { get; set; }


        [StringLength(255)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string LicenseNote2 { get; set; }




        [StringLength(10)]
        [Display(Name = "使用狀況", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string UsageState { get; set; }

       
        [StringLength(10)]
        [Display(Name = "使用狀況(選單1)", Order = 12)]
        [ColumnDef(Visible = false,EditType = EditType.Select, SelectGearingWith = "UsageState2,Parent,true",
          SelectItemsClassNamespace = UsageState1ItemsClassImp.AssemblyQualifiedName)]
        public string UsageState1 { get; set; }

        [ColumnDef(Visible = false, EditType = EditType.Select, SelectGearingWith = "UsageState3,Parent,true",
                SelectItemsClassNamespace = UsageState2ItemsClassImp.AssemblyQualifiedName)]
        [StringLength(10)]
        [Display(Name = "使用狀況(選單2)", Order = 13)]
        public string UsageState2 { get; set; }

        [ColumnDef(Visible = false, EditType = EditType.Select, SelectGearingWith = "UsageState4,Parent,true",
           SelectItemsClassNamespace = UsageState3ItemsClassImp.AssemblyQualifiedName)]
        [StringLength(10)]
        [Display(Name = "使用狀況(選單3)", Order = 14)]
        public string UsageState3 { get; set; }

        [ColumnDef(Visible = false, EditType = EditType.Select, SelectGearingWith = "UsageState5,Parent,true",
                SelectItemsClassNamespace = UsageState4ItemsClassImp.AssemblyQualifiedName)]
        [StringLength(10)]
        [Display(Name = "使用狀況(選單4)", Order = 15)]
        public string UsageState4 { get; set; }

        [ColumnDef(Visible = false, EditType = EditType.Select,
            SelectItemsClassNamespace = UsageState5ItemsClassImp.AssemblyQualifiedName)]
        [StringLength(10)]
        [Display(Name = "使用狀況(選單5)", Order = 16)]
        public string UsageState5 { get; set; }


        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(255)]
        public string LicenseNote3 { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(25)]
        [Display(Name = "負責人姓名", Order = 22)]
        public string Boss { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(10)]
        [Display(Name = "負責人身份證字號", Order = 23)]
        public string Boss_ID { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(20)]
        [Display(Name = "負責人電話", Order = 24)]
        public string Boss_Tel { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(50)]
        [Display(Name = "電子郵件信箱", Order = 27)]
        public string Boss_Email { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(5)]
        [Display(Name = "負責人郵遞區號", Order = 25)]
        public string Boss_AreaNo { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(100)]
        [Display(Name = "負責人地址", Order = 26)]
        public string Boss_Address { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(100)]
        [Display(Name = "申請單位", Order = 8)]
        public string Apply_Name { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(20)]
        [Display(Name = "單位電話", Order = 9)]
        public string Apply_Tel { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(5)]
        [Display(Name = "單位郵遞區號", Order = 10)]
        public string Apply_AreaNo { get; set; }

        [ColumnDef(Visible = false, Filter = true)]
        [StringLength(100)]
        [Display(Name = "單位地址", Order = 11)]
        public string Apply_Address { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(500)]
        [Display(Name = "設施狀況：基本設施", Order = 28)]
        public string BasicFacilities { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(500)]
        [Display(Name = "設施狀況：其他設施", Order = 29)]
        public string OtherFacilities { get; set; }


        [StringLength(500)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string AllFacilities { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(200)]
        [Display(Name = "設施狀況：供油對象", Order = 30)]
        public string SupplyTarget { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string File_name { get; set; }

        [ColumnDef(Visible = false)]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "資料建立時間", Order = 31)]
        public DateTime? Create_date { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(25)]
        [Display(Name = "資料建立者", Order = 32)]
        public string Create_name { get; set; }

        [ColumnDef(Visible = false)]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "資料修改時間", Order = 33)]
        public DateTime? Mod_date { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(25)]
        [Display(Name = "最後修改者", Order = 34)]
        public string Mod_name { get; set; }

        [StringLength(200)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Note1 { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(200)]
        [Display(Name = "備註", Order = 35)]
        public string Note2 { get; set; }

        [StringLength(1000)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Note3 { get; set; }

        [StringLength(52)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string MemberID { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }

        [StringLength(50)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string UsageStateSub { get; set; }

        [StringLength(80)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string otherFacility { get; set; }

        [Column(TypeName = "smalldatetime")]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? ChangeReport_date { get; set; }

    }



    public class LocationSelectItemsClassImp : Dou.Misc.Attr.SelectItemsClass
    {

        public const string AssemblyQualifiedName = "OilGas.Models.LocationSelectItemsClassImp,OilGas";


        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {

            List<SelectITEM> Locations = new List<SelectITEM>
            {
    new SelectITEM { Value = "臺北國際機場", Name = "臺北國際機場", Parent = "航空站" },
    new SelectITEM { Value = "桃園國際機場", Name = "桃園國際機場", Parent = "航空站" },
    new SelectITEM { Value = "臺中機場", Name = "臺中機場", Parent = "航空站" },
    new SelectITEM { Value = "嘉義機場", Name = "嘉義機場", Parent = "航空站" },
    new SelectITEM { Value = "臺南機場", Name = "臺南機場", Parent = "航空站" },
    new SelectITEM { Value = "高雄國際機場", Name = "高雄國際機場", Parent = "航空站" },
    new SelectITEM { Value = "恆春機場", Name = "恆春機場", Parent = "航空站" },
    new SelectITEM { Value = "花蓮機場", Name = "花蓮機場", Parent = "航空站" },
    new SelectITEM { Value = "臺東機場", Name = "臺東機場", Parent = "航空站" },
    new SelectITEM { Value = "綠島機場", Name = "綠島機場", Parent = "航空站" },
    new SelectITEM { Value = "蘭嶼機場", Name = "蘭嶼機場", Parent = "航空站" },
    new SelectITEM { Value = "馬祖北竿幾場", Name = "馬祖北竿幾場", Parent = "航空站" },
    new SelectITEM { Value = "馬祖南竿機場", Name = "馬祖南竿機場", Parent = "航空站" },
    new SelectITEM { Value = "金門機場", Name = "金門機場", Parent = "航空站" },
    new SelectITEM { Value = "馬公機場", Name = "馬公機場", Parent = "航空站" },
    new SelectITEM { Value = "望安機場", Name = "望安機場", Parent = "航空站" },
    new SelectITEM { Value = "七美機場", Name = "七美機場", Parent = "航空站" },


    new SelectITEM { Value = "基隆港", Name = "基隆港", Parent = "商港" },
    new SelectITEM { Value = "臺北港", Name = "臺北港", Parent = "商港" },
    new SelectITEM { Value = "臺中港", Name = "臺中港", Parent = "商港" },
    new SelectITEM { Value = "布袋港", Name = "布袋港", Parent = "商港" },
    new SelectITEM { Value = "安平港", Name = "安平港", Parent = "商港" },
    new SelectITEM { Value = "興達港", Name = "興達港", Parent = "商港" },
    new SelectITEM { Value = "永安港", Name = "永安港", Parent = "商港" },
    new SelectITEM { Value = "高雄港", Name = "高雄港", Parent = "商港" },
    new SelectITEM { Value = "深澳港", Name = "深澳港", Parent = "商港" },
    new SelectITEM { Value = "蘇澳港", Name = "蘇澳港", Parent = "商港" },
    new SelectITEM { Value = "花蓮港", Name = "花蓮港", Parent = "商港" },
    new SelectITEM { Value = "澎湖港", Name = "澎湖港", Parent = "商港" },
    new SelectITEM { Value = "金門港", Name = "金門港", Parent = "商港" },


    new SelectITEM { Value = "觀塘港", Name = "觀塘港", Parent = "工業專用港" },
    new SelectITEM { Value = "麥寮港", Name = "麥寮港", Parent = "工業專用港" },
    new SelectITEM { Value = "和平港", Name = "和平港", Parent = "工業專用港" }
            };


            return Locations.Select(s => new KeyValuePair<string, object>(s.Value, "{\"v\":\"" + s.Name + "\",\"Parent\":\"" + s.Parent + "\"}"));
        }

        public class SelectITEM
        {
            public string Value { get; set; }
            public string Name { get; set; }
            public string Parent { get; set; }
        }


    }


    public class UsageState1ItemsClassImp : Dou.Misc.Attr.SelectItemsClass
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();
        public const string AssemblyQualifiedName = "OilGas.Models.UsageState1ItemsClassImp,OilGas";


        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
           

            var UsageStatedata= db.PortGas_UsageState.Where(x=>x.UsageState_Type_ParentID == "00").ToArray();


            return UsageStatedata.Select(s => new KeyValuePair<string, object>(s.UsageState_TypeID, "{\"v\":\"" + s.UsageState_TypeName + "\",\"Parent\":\"" + getselect(s.UsageState_Type_ParentType) + "\"}"));
        }

      public string getselect(string UsageState_Type_ParentType)
        {
            var value = "";
            switch (UsageState_Type_ParentType)
            {

                case "主要狀態":
                    value = "";
                    break;

                case "申請中":
                    value = "01";
                    break;

                case "同意設置":
                    value = "11";
                    break;

                case "申請使用":
                    value = "21";
                    break;
                case "同意使用":
                    value = "31";
                    break;

            }

            return value;
      }


    }
    public class UsageState2ItemsClassImp : UsageState1ItemsClassImp
    {      
        public new const string AssemblyQualifiedName = "OilGas.Models.UsageState2ItemsClassImp,OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var UsageStatedata = db.PortGas_UsageState.Where(x => x.UsageState_Type_ParentID == "01").ToArray();
            return UsageStatedata.Select(s => new KeyValuePair<string, object>(s.UsageState_TypeID, "{\"v\":\"" + s.UsageState_TypeName + "\",\"Parent\":\"" + getselect(s.UsageState_Type_ParentType) + "\"}"));
        }
    }

    public class UsageState3ItemsClassImp : UsageState1ItemsClassImp
    {
        public new const string AssemblyQualifiedName = "OilGas.Models.UsageState3ItemsClassImp,OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var UsageStatedata = db.PortGas_UsageState.Where(x => x.UsageState_Type_ParentID == "02").ToArray();
            return UsageStatedata.Select(s => new KeyValuePair<string, object>(s.UsageState_TypeID, "{\"v\":\"" + s.UsageState_TypeName + "\",\"Parent\":\"" + getselect(s.UsageState_Type_ParentType) + "\"}"));
        }
    }
    public class UsageState4ItemsClassImp : UsageState1ItemsClassImp
    {
        public new const string AssemblyQualifiedName = "OilGas.Models.UsageState4ItemsClassImp,OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var UsageStatedata = db.PortGas_UsageState.Where(x => x.UsageState_Type_ParentID == "03").ToArray();
            return UsageStatedata.Select(s => new KeyValuePair<string, object>(s.UsageState_TypeID, "{\"v\":\"" + s.UsageState_TypeName + "\",\"Parent\":\"" + getselect(s.UsageState_Type_ParentType) + "\"}"));
        }
    }
    public class UsageState5ItemsClassImp : UsageState1ItemsClassImp
    {
        public new const string AssemblyQualifiedName = "OilGas.Models.UsageState5ItemsClassImp,OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var UsageStatedata = db.PortGas_UsageState.Where(x => x.UsageState_Type_ParentID == "04").ToArray();
            return UsageStatedata.Select(s => new KeyValuePair<string, object>(s.UsageState_TypeID, "{\"v\":\"" + s.UsageState_TypeName + "\",\"Parent\":\"" + getselect(s.UsageState_Type_ParentType) + "\"}"));
        }
    }
}
