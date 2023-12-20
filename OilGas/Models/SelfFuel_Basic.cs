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

    public partial class SelfFuel_Basic
    {
        [Key]
        [Column(Order = 0)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        [ColumnDef(Filter = true)]
        [Display(Name = "案件編號", Order = 1)]
        public string CaseNo { get; set; }





        [Display(Name = "縣市", Order = 1)]
        [ColumnDef(Filter = true, Visible = false, VisibleEdit = false, EditType = EditType.Select,
       SelectItemsClassNamespace = UsercitySelectItemsClassImp.AssemblyQualifiedName)]
        [StringLength(10)]
        [NotMapped]
        public string CITY
        {
            get
            {
                if (CaseNo.Length > 6)
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








        [Display(Name = "設施名稱", Order = 4)]
        [ColumnDef(Filter = true)]
        [StringLength(70)]
        public string FuelName { get; set; }



        [StringLength(30)]
        [Display(Name = "營業主體", Order = 5)]
        [ColumnDef(Filter = true)]
        public string BusiOrg { get; set; }

        

        [StringLength(2)]
        [ColumnDef(Visible = false,EditType = EditType.Select, SelectGearingWith = "UsageState_Second,Parent,true",
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.UsageState, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
        [Display(Name = "使用狀況(選單1)", Order = 6)]
        public string UsageState { get; set; }

        [StringLength(2)]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectGearingWith = "UsageState_Third,Parent,true",
               SelectItemsClassNamespace = UsageState_SecondSelectItemsClassImp.AssemblyQualifiedName)]
        [Display(Name = "使用狀況(選單2)", Order = 7)]
        public string UsageState_Second { get; set; }

        [StringLength(2)]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectGearingWith = "UsageState_Fourth,Parent,true",
          SelectItemsClassNamespace = UsageState_ThirdSelectItemsClassImp.AssemblyQualifiedName)]
        [Display(Name = "使用狀況(選單3)", Order = 8)]
        public string UsageState_Third { get; set; }

        [StringLength(2)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
   SelectItemsClassNamespace = UsageState_FourthSelectItemsClassImp.AssemblyQualifiedName)]
        [Display(Name = "使用狀況(選單4)", Order = 9)]
        public string UsageState_Fourth { get; set; }




        //讓選向變成1:1代表UsageState的選項1，2:1代表UsageState_Second的選項1
        [NotMapped]
        [ColumnDef(VisibleEdit =false, EditType = EditType.Select,
SelectItemsClassNamespace = UsageStateMENUSelectItemsClassImp.AssemblyQualifiedName)]
        [Display(Name = "使用狀況", Order = 9)]
        public string UsageStatemenu
        {
            get
            {
                //先確定不會是null
                UsageState_Fourth = UsageState_Fourth is null ? "" : UsageState_Fourth;
                UsageState_Third = UsageState_Third is null ? "" : UsageState_Third;
                UsageState_Second = UsageState_Second is null ? "" : UsageState_Second;
                UsageState = UsageState is null ? "" : UsageState;

                if (UsageState_Fourth.Length > 0)
                {
                    return "4:" + UsageState_Fourth;
                }
                else if (UsageState_Third.Length > 0)
                {
                    return "3:" + UsageState_Third;
                }
                else if (UsageState_Second.Length > 0)
                {
                    return "2:" + UsageState_Second;
                }
                else if (UsageState.Length > 0)
                {
                    return "1:" + UsageState;
                }
                else
                {
                    return UsageState;
                }
            }
            set
            {
            }
        }





























        [ColumnDef(Visible = false)]
        [Display(Name = "核准使用日期", Order = 10)]
        public DateTime? StartDate { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "結束使用日期", Order = 11)]
        public DateTime? EndDate { get; set; }


        [StringLength(2)]
        [Display(Name = "設置場所", Order = 11)]
        [ColumnDef(Visible = false, EditType = EditType.Select, SelectGearingWith = "FacilityDetail,Parent,true",
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.Facility, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
        public string Facility { get; set; }


        [StringLength(2)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
           SelectItemsClassNamespace = FacilityDetailSelectItemsClassImp.AssemblyQualifiedName)]
        [Display(Name = "詳細設置場所", Order = 12)]
        public string FacilityDetail { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(20)]
        [Display(Name = "其他設置場所", Order = 13)]
        public string FacilityOther { get; set; }


        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(20)]
        [Display(Name = "設置場所-基地", Order = 99)]
        public string FacilityBase { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(25)]
        [Display(Name = "負責人姓名", Order = 19)]
        public string Responsor { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(20)]
        [Display(Name = "設施電話", Order = 15)]
        public string FacilityPhone { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(20)]
        [Display(Name = "電子郵件信箱", Order = 21)]
        public string Email { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(10)]
        [Display(Name = "負責人身份證字號", Order = 20)]
        public string IdNo { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(3)]
        [Display(Name = "郵遞區號", Order = 16)]
        public string AreaNo { get; set; }

    
        [StringLength(70)]
        [Display(Name = "設施地址", Order =17)]
        [ColumnDef(Filter = true, FilterAssign = FilterAssignType.Contains)]
        public string Address { get; set; }

        [ColumnDef(Visible = false, Filter = true)]
        [StringLength(300)]
        [Display(Name = "加油站地號", Order = 18)]
        public string AddressNo { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "資料建立時間", Order = 30)]
        public DateTime? CreateTime { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(10)]
        [Display(Name = "資料建立者", Order = 31)]
        public string CreateUser { get; set; }

        [ColumnDef(Visible = false,VisibleEdit =false)]
        [StringLength(10)]
        [Display(Name = "資料建立者(暫存)", Order = 32)]
        public string CreateUserTemp { get; set; }

        [ColumnDef(Visible = false)]
        [Display(Name = "資料修改時間", Order = 33)]
        public DateTime? ModifyTime { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(10)]
        [Display(Name = "最後修改者", Order = 34)]
        public string LastModifyUser { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(500)]
        [Display(Name = "備註", Order = 35)]
        public string Note { get; set; }


        [ColumnDef(Visible = false, VisibleEdit = false)]
        [Display(Name = "是否確認(暫存)", Order = 99)]
        public bool? IsConfirm { get; set; }



        [ColumnDef(Visible = false, VisibleEdit = false)]
        [Display(Name = "核准日期", Order = 99)]
        public DateTime? AuthorizedDate { get; set; }

      
        [Display(Name = "收件日期", Order = 2)]
        [ColumnDef(Filter = true)]
        public DateTime? ExpiredDate { get; set; }


        [ColumnDef(Visible = false)]
        [StringLength(30)]
        [Display(Name = "特許執照號碼", Order = 3)]
        public string LicenseNo { get; set; }


        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }


        [StringLength(20)]
        [Display(Name = "X", Order = 99)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Longitude_E { get; set; }


        [StringLength(20)]
        [Display(Name = "Y", Order = 99)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Longitude_N { get; set; }

    }




    public class UsageState_SecondSelectItemsClassImp : Dou.Misc.Attr.SelectItemsClass
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();
        public const string AssemblyQualifiedName = "OilGas.Models.UsageState_SecondSelectItemsClassImp,OilGas";


        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            //var UsageStateCode2s = GetAllUsageStateCode2().ToArray();
            var UsageState_Seconds = db.UsageState_Second.ToArray();
            return UsageState_Seconds.Select(s => new KeyValuePair<string, object>(s.Value, "{\"v\":\"" + s.Name + "\",\"Parent\":\"" + s.Father + "\"}"));
        }


    }



    public class UsageState_ThirdSelectItemsClassImp : UsageState_SecondSelectItemsClassImp
    {
        public new const string AssemblyQualifiedName = "OilGas.Models.UsageState_ThirdSelectItemsClassImp,OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var UsageStateCodes = db.UsageState_Third.ToArray();
            return UsageStateCodes.Select(s => new KeyValuePair<string, object>(s.Value, "{\"v\":\"" + s.Name + "\",\"Parent\":\"" + s.Father + "\"}"));
        }

    }


    public class UsageState_FourthSelectItemsClassImp : UsageState_SecondSelectItemsClassImp
    {
        public new const string AssemblyQualifiedName = "OilGas.Models.UsageState_FourthSelectItemsClassImp,OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var UsageStateCodes = db.UsageState_Fourth.ToArray();
            return UsageStateCodes.Select(s => new KeyValuePair<string, object>(s.Value, "{\"v\":\"" + s.Name + "\",\"Parent\":\"" + s.Father + "\"}"));
        }

    }



    //讓選向變成1:1代表UsageState的選項1，2:1代表UsageState_Second的選項1
    public class UsageStateMENUSelectItemsClassImp : UsageState_SecondSelectItemsClassImp
    {
        public new const string AssemblyQualifiedName = "OilGas.Models.UsageStateMENUSelectItemsClassImp,OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            List<UsageStateMENU> UsageStateCodes = new List<UsageStateMENU>();
           
            List<UsageStateMENU> UsageState = db.UsageState.Select(u => new UsageStateMENU
            {
                Name = u.Name,
                Value = u.Value,
                MENU = 1 
            }).ToList();

            List<UsageStateMENU> UsageState_Second = db.UsageState_Second.Select(u => new UsageStateMENU
            {
                Name = u.Name,
                Value = u.Value,
                MENU = 2
            }).ToList();
            List<UsageStateMENU> UsageState_Third = db.UsageState_Third.Select(u => new UsageStateMENU
            {
                Name = u.Name,
                Value = u.Value,
                MENU = 3
            }).ToList();
            List<UsageStateMENU> UsageState_Fourth = db.UsageState_Fourth.Select(u => new UsageStateMENU
            {
                Name = u.Name,
                Value = u.Value,
                MENU = 4
            }).ToList();

            UsageStateCodes.AddRange(UsageState);
            UsageStateCodes.AddRange(UsageState_Second);
            UsageStateCodes.AddRange(UsageState_Third);
            UsageStateCodes.AddRange(UsageState_Fourth);


            return UsageStateCodes.Select(s => new KeyValuePair<string, object>(s.MENU+":"+s.Value, s.Name));
        }




        public partial class UsageStateMENU
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public int MENU { get; set; }

        }
    }









    public class FacilityDetailSelectItemsClassImp : UsageState_SecondSelectItemsClassImp
    {
        public new const string AssemblyQualifiedName = "OilGas.Models.FacilityDetailSelectItemsClassImp,OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var UsageStateCodes = db.Facility_Detail.ToArray();
            return UsageStateCodes.Select(s => new KeyValuePair<string, object>(s.Value, "{\"v\":\"" + s.Name + "\",\"Parent\":\"" + s.Group + "\"}"));
        }

    }


}
