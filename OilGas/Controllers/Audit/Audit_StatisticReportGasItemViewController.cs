using Dou.Controllers;
using Dou.Misc.Attr;
using Dou.Models.DB;
using NPOI.SS.Formula.Functions;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_StatisticReportGasItemView", Name = "石油設施缺失統計表", MenuPath = "查核輔導專區/G統計報表專區", Action = "Index", Index = 4, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_StatisticReportGasItemViewController : AGenericModelController<vw_Audit_StatisticReportGasItemView>
    {
        // GET: Audit_StatisticReportGasItemView
        public ActionResult Index()
        {
            return View();
        }
        protected override Dou.Models.DB.IModelEntity<vw_Audit_StatisticReportGasItemView> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<vw_Audit_StatisticReportGasItemView>(new OilGasModelContextExt());
        }

        protected override IEnumerable<vw_Audit_StatisticReportGasItemView> GetDataDBObject(IModelEntity<vw_Audit_StatisticReportGasItemView> dbEntity, params KeyValueParams[] paras)
        {
            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {                
                return new List<vw_Audit_StatisticReportGasItemView>().AsQueryable();
            }

            //var result = iquery.AsEnumerable();
            List<string> titles = new List<string>();
            var result = GetOutputData(ref titles, paras);

            KeyValueParams ksort = paras.FirstOrDefault((KeyValueParams s) => s.key == "sort");
            KeyValueParams korder = paras.FirstOrDefault((KeyValueParams s) => s.key == "order");
            //分頁排序
            if (ksort.value != null && korder.value != null)
            {
                string sort = ksort.value.ToString();
                string order = korder.value.ToString();

                if (ksort.value.ToString() == "AllDoesmeet")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.AllDoesmeet);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.AllDoesmeet);
                    }
                }
                else if (ksort.value.ToString() == "CheckNo")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.CheckNo);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.CheckNo);
                    }
                }
                else if (ksort.value.ToString() == "CheckDate")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.CheckDate);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.CheckDate);
                    }
                }
                else if (ksort.value.ToString() == "Business_theme_Name")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.Business_theme_Name);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.Business_theme_Name);
                    }
                }
                else if (ksort.value.ToString() == "Gas_Name")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.Gas_Name);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.Gas_Name);
                    }
                }
                else if (ksort.value.ToString() == "Addr")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.Addr);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.Addr);
                    }
                }
            }
            else
            {
                //預設排序                
                result = result.OrderBy(a => a.CheckNo);
            }

            return result;
        }

        public ActionResult ExportAudit_StatisticReportGasItemView(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G統計報表專區_石油設施缺失統計表);
            string fileTitle = "查核輔導專區_石油設施缺失統計表";

            List<string> titles = new List<string>() { "查核輔導專區_石油設施缺失統計表，查詢條件:" }; ;

            var result = GetOutputData(ref titles, paras);
            //預設排序            
            result = result.OrderBy(a => a.CheckNo);

            //產出Dynamic資料 (給Excel)
            List<dynamic> list = new List<dynamic>();
            var output = result.ToList();

            var Business_theme = KeyValue.GetFilterParaValue(paras, "Business_theme");
            string Business_theme_Name = CarVehicleGas_BusinessOrganizationVSelectItemsClassImp.BUSS.FirstOrDefault().Name;
            foreach (var data in output)
            {
                dynamic f = new ExpandoObject();
                f.缺失數量 = data.AllDoesmeet;
                f.查核編號 = data.CheckNo;
                f.查核日期 = data.CheckDate;
                f.營業主體 = data.Business_theme_Name;
                f.石油設施名稱 = data.Gas_Name;
                f.石油設施地址 = data.Addr;                

                f.SheetName = fileTitle;//sheep.名稱;
                list.Add(f);
            }

            //查無符合資料表數
            if (list.Count == 0)
            {
                return Json(new { result = false, errorMessage = "查無符合資料表數" }, JsonRequestBehavior.AllowGet);
            }

            string fileName = OilGas.ExcelSpecHelper.GenerateExcelByLinqF1(fileTitle, titles, list, folder, "N");
            string path = folder + fileName;
            url = OilGas.Cm.PhysicalToUrl(path);

            if (url == "")
            {
                return Json(new { result = false, errorMessage = error }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = true, url = url }, JsonRequestBehavior.AllowGet);
            }
        }

        private IEnumerable<vw_Audit_StatisticReportGasItemView> GetOutputData(ref List<string> titles, params KeyValueParams[] paras)
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
            
            Dou.Models.DB.IModelEntity<Check_Basic> check_Basic = new Dou.Models.DB.ModelEntity<Check_Basic>(dbContext);
            Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> carVehicleGas_BusinessOrganization = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);
            
            var CaseType = KeyValue.GetFilterParaValue(paras, "CaseType");
            var WorkYear = int.Parse(KeyValue.GetFilterParaValue(paras, "WorkYear"));
            var CityCode1 = KeyValue.GetFilterParaValue(paras, "CityCode1");
            var HiatusMin = KeyValue.GetFilterParaValue(paras, "HiatusMin");
            var HiatusMax = KeyValue.GetFilterParaValue(paras, "HiatusMax");

            if (CaseType == "Check_Item")
            {
                titles.Add("營業主體:" + "汽/機車加油站");
            }
            else if (CaseType == "Check_Item_Fish")
            {
                titles.Add("營業主體:" + "漁船加油站");
            }
            else if (CaseType == "Check_Item_SelfUP")
            {
                titles.Add("營業主體:" + "自用加儲油設施(地上)");
            }
            else if (CaseType == "Check_Item_SelfDown")
            {
                titles.Add("營業主體:" + "自用加儲油設施(地下)");
            }
            titles.Add("查核年度(起):" + WorkYear.ToString());
            string workTable = Code.GetWorkTable(CaseType, WorkYear);

            IEnumerable<vw_Audit_StatisticReportGasItemView> iquery;

            var s1 = check_Basic.GetAll().GroupJoin(carVehicleGas_BusinessOrganization.GetAll(), a => a.Business_theme, b => b.Value, (o, c) => new
            {
                o.Tank_Well, o.CheckNo, o.CheckDate, o.Business_theme, o.Gas_Name, o.Addr,
                Business_theme_Name = (c.FirstOrDefault() == null || c.FirstOrDefault().Name == "其他") ? o.Business_theme_Name : c.FirstOrDefault().Name
            });

            //主表變動(workTable)
            switch (workTable)
            {
                case "Check_Item_97":
                    Dou.Models.DB.IModelEntity<Check_Item_97> check_Item_97 = new Dou.Models.DB.ModelEntity<Check_Item_97>(dbContext);
                    var main = check_Item_97.GetAll()
                        .Join(s1, a => a.CheckNo, b => b.CheckNo, (o, c) => new
                        {
                            o.AllDoesmeet, c.Tank_Well, c.CheckNo, c.CheckDate, c.Business_theme, c.Gas_Name, c.Addr, c.Business_theme_Name
                        })
                        .Where(a => ((DateTime)a.CheckDate).Year - 1911 == WorkYear);

                    iquery = main.ToArray()
                        .Select(o => new vw_Audit_StatisticReportGasItemView
                        {
                            AllDoesmeet = o.AllDoesmeet,
                            CheckNo = o.CheckNo,
                            CheckDate = o.CheckDate,
                            Business_theme_Name = o.Business_theme_Name,
                            Gas_Name = o.Gas_Name,
                            Addr = o.Addr
                        });
                    break;
                case "Check_Item":
                    Dou.Models.DB.IModelEntity<Check_Item> check_Item = new Dou.Models.DB.ModelEntity<Check_Item>(dbContext);
                    var main2 = check_Item.GetAll()
                        .Join(s1, a => a.CheckNo, b => b.CheckNo, (o, c) => new
                        {
                            o.AllDoesmeet,
                            c.Tank_Well,
                            c.CheckNo,
                            c.CheckDate,
                            c.Business_theme,
                            c.Gas_Name,
                            c.Addr,
                            c.Business_theme_Name
                        })
                        .Where(a => ((DateTime)a.CheckDate).Year - 1911 == WorkYear);

                    iquery = main2.ToArray()
                        .Select(o => new vw_Audit_StatisticReportGasItemView
                        {
                            AllDoesmeet = o.AllDoesmeet,
                            CheckNo = o.CheckNo,
                            CheckDate = o.CheckDate,
                            Business_theme_Name = o.Business_theme_Name,
                            Gas_Name = o.Gas_Name,
                            Addr = o.Addr
                        });
                    break;
                case "Check_Item_Fish103":
                    Dou.Models.DB.IModelEntity<Check_Item_Fish103> check_Item_Fish103 = new Dou.Models.DB.ModelEntity<Check_Item_Fish103>(dbContext);
                    var main3 = check_Item_Fish103.GetAll()
                        .Join(s1, a => a.CheckNo, b => b.CheckNo, (o, c) => new
                        {
                            o.AllDoesmeet,
                            c.Tank_Well,
                            c.CheckNo,
                            c.CheckDate,
                            c.Business_theme,
                            c.Gas_Name,
                            c.Addr,
                            c.Business_theme_Name
                        })
                        .Where(a => ((DateTime)a.CheckDate).Year - 1911 == WorkYear);

                    iquery = main3.ToArray()
                        .Select(o => new vw_Audit_StatisticReportGasItemView
                        {
                            AllDoesmeet = o.AllDoesmeet,
                            CheckNo = o.CheckNo,
                            CheckDate = o.CheckDate,
                            Business_theme_Name = o.Business_theme_Name,
                            Gas_Name = o.Gas_Name,
                            Addr = o.Addr
                        });
                    break;
                case "Check_Item_Fish":
                    Dou.Models.DB.IModelEntity<Check_Item_Fish> check_Item_Fish = new Dou.Models.DB.ModelEntity<Check_Item_Fish>(dbContext);
                    var main4 = check_Item_Fish.GetAll()
                        .Join(s1, a => a.CheckNo, b => b.CheckNo, (o, c) => new
                        {
                            o.AllDoesmeet,
                            c.Tank_Well,
                            c.CheckNo,
                            c.CheckDate,
                            c.Business_theme,
                            c.Gas_Name,
                            c.Addr,
                            c.Business_theme_Name
                        })
                        .Where(a => ((DateTime)a.CheckDate).Year - 1911 == WorkYear);

                    iquery = main4.ToArray()
                        .Select(o => new vw_Audit_StatisticReportGasItemView
                        {
                            AllDoesmeet = o.AllDoesmeet,
                            CheckNo = o.CheckNo,
                            CheckDate = o.CheckDate,
                            Business_theme_Name = o.Business_theme_Name,
                            Gas_Name = o.Gas_Name,
                            Addr = o.Addr
                        });
                    break;
                case "Check_Item_SelfUP":
                    Dou.Models.DB.IModelEntity<Check_Item_SelfUP> check_Item_SelfUP = new Dou.Models.DB.ModelEntity<Check_Item_SelfUP>(dbContext);
                    var main5 = check_Item_SelfUP.GetAll()
                        .Join(s1, a => a.CheckNo, b => b.CheckNo, (o, c) => new
                        {
                            o.AllDoesmeet,
                            c.Tank_Well,
                            c.CheckNo,
                            c.CheckDate,
                            c.Business_theme,
                            c.Gas_Name,
                            c.Addr,
                            c.Business_theme_Name
                        })
                        .Where(a => ((DateTime)a.CheckDate).Year - 1911 == WorkYear);
                    main5 = main5.Where(a => a.Tank_Well == "0");

                    iquery = main5.ToArray()
                        .Select(o => new vw_Audit_StatisticReportGasItemView
                        {
                            AllDoesmeet = o.AllDoesmeet,
                            CheckNo = o.CheckNo,
                            CheckDate = o.CheckDate,
                            Business_theme_Name = o.Business_theme_Name,
                            Gas_Name = o.Gas_Name,
                            Addr = o.Addr
                        });
                    break;
                case "Check_Item_SelfDown":
                    Dou.Models.DB.IModelEntity<Check_Item_SelfDown> check_Item_SelfDown = new Dou.Models.DB.ModelEntity<Check_Item_SelfDown>(dbContext);
                    var main6 = check_Item_SelfDown.GetAll()
                        .Join(s1, a => a.CheckNo, b => b.CheckNo, (o, c) => new
                        {
                            o.AllDoesmeet,
                            c.Tank_Well,
                            c.CheckNo,
                            c.CheckDate,
                            c.Business_theme,
                            c.Gas_Name,
                            c.Addr,
                            c.Business_theme_Name
                        })
                        .Where(a => ((DateTime)a.CheckDate).Year - 1911 == WorkYear);
                    main6 = main6.Where(a => a.Tank_Well == "1");

                    iquery = main6.ToArray()
                        .Select(o => new vw_Audit_StatisticReportGasItemView
                        {
                            AllDoesmeet = o.AllDoesmeet,
                            CheckNo = o.CheckNo,
                            CheckDate = o.CheckDate,
                            Business_theme_Name = o.Business_theme_Name,
                            Gas_Name = o.Gas_Name,
                            Addr = o.Addr
                        });
                    break;
                default:
                    return null;                    
            }

            //權限查詢
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysCodes();
            iquery = iquery.Where(a => a.CheckNo != ""
                    && pCitys.Any(b => a.CheckNo.Substring(0, 1) == b));

            //條件            
            if (!string.IsNullOrEmpty(CityCode1))
            {
                var codes = CityCode1.Split(',').ToList();
                codes = Code.ConvertTwCity(codes);

                iquery = iquery.Where(a => a.CheckNo != ""
                    && codes.Any(b => a.CheckNo.Substring(0, 1) == b));

                //代碼-縣市別                
                Dou.Models.DB.IModelEntity<CityCode> cityCode = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
                var citys = cityCode.GetAll().Where(a => codes.Any(b => b == a.CityCode1)).OrderBy(a => a.Rank);
                titles.Add("縣市:" + string.Join(",", citys.Select(a => a.CityName).ToList()));
            }

            if (!string.IsNullOrEmpty(HiatusMin))
            {
                int num = int.Parse(HiatusMin);
                iquery = iquery.Where(a => a.AllDoesmeet >= num);
                titles.Add("缺失數量(起):" + num.ToString());
            }

            if (!string.IsNullOrEmpty(HiatusMax))
            {
                int num = int.Parse(HiatusMax);
                iquery = iquery.Where(a => a.AllDoesmeet <= num);
                titles.Add("缺失數量(迄):" + num.ToString());
            }

            return iquery;
        }

    }

    public class vw_Audit_StatisticReportGasItemView
    {
        [Display(Name = "缺失數量")]
        [ColumnDef(Sortable = true)]
        public int? AllDoesmeet { get; set; }


        [Display(Name = "儲油槽", Order = 1)]
        [ColumnDef(Visible = false)]        
        [StringLength(2)]
        public string Tank_Well { get; set; }

        [Key]
        [Display(Name = "查核編號")]
        [ColumnDef(Sortable = true)]
        public string CheckNo { get; set; }
        
        [Display(Name = "查核日期")]
        [ColumnDef(Sortable = true, EditType = EditType.Date)]
        public DateTime? CheckDate { get; set; }
        
        [Display(Name = "營業主體")]
        [ColumnDef(Sortable = true)]
        public string Business_theme_Name { get; set; }
        
        [Display(Name = "石油設施名稱")]
        [ColumnDef(Sortable = true)]
        public string Gas_Name { get; set; }
        
        [Display(Name = "石油設施地址")]
        [ColumnDef(Sortable = true)]
        public string Addr { get; set; }

        //[Display(Name = "???")]
        //[ColumnDef(Sortable = true)]
        //public bool CheckAction { get; set; }

        [Display(Name = "石油設施類型", Order = 1)]
        [ColumnDef(Visible = false, Filter = true, EditType = EditType.Select,
            SelectGearingWith = "Business_theme,CaseType,true",
            SelectItemsClassNamespace = OrganizationCaseTypeSelectItemsClassImp.AssemblyQualifiedName)]
        public string CaseType { get; }

        [Display(Name = "查核年度", Order = 2)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
            Filter = true, SelectItemsClassNamespace = OilGas.Controllers.Audit.Audit_StatisticReportEquipViewYearSelectItems.AssemblyQualifiedName)]
        public DateTime WorkYear { get; }

        [Display(Name = "縣市別", Order = 3)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
                Filter = true, SelectItemsClassNamespace = UsercityCodeSelectItems.AssemblyQualifiedName)]
        public string CityCode1 { get; set; }

        [Display(Name = "缺失數量(起)", Order = 4)]
        [ColumnDef(Visible = false, Filter = true)]
        public int HiatusMin { get; }

        [Display(Name = "缺失數量(迄)", Order = 5)]
        [ColumnDef(Visible = false, Filter = true)]
        public int HiatusMax { get; }
    }
}