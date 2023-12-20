using Dou.Controllers;
using Dou.Misc;
using Dou.Misc.Attr;
using Dou.Models.DB;
using Newtonsoft.Json;
using NPOI.OpenXmlFormats.Dml.Diagram;
using NPOI.SS.Formula.Functions;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_StatisticReportEquipView", Name = "設備檢查缺失", MenuPath = "查核輔導專區/G統計報表專區", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_StatisticReportEquipViewController : APaginationModelController<vw_Audit_StatisticReportEquipView>
    {
        //AGenericModelController,APaginationModelController
        // GET: Audit_StatisticReportEquipView
        public ActionResult Index()
        {
            return View();
        }
        protected override Dou.Models.DB.IModelEntity<vw_Audit_StatisticReportEquipView> GetModelEntity()
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
            return new Dou.Models.DB.ModelEntity<vw_Audit_StatisticReportEquipView>(dbContext);

            //return null;
        }

        protected override IQueryable<vw_Audit_StatisticReportEquipView> BeforeIQueryToPagedList(IQueryable<vw_Audit_StatisticReportEquipView> iquery,
                                params KeyValueParams[] paras)
        {
            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return null;
            }

            //解決資料查詢錯誤，但查詢數量為全部(非分頁數量)
            //不使用dou filter過濾資料(iquery)            
            //var result = iquery.AsEnumerable();
            List<string> titles = new List<string>();
            var result = GetOutputData(ref titles,paras);

            KeyValueParams ksort = paras.FirstOrDefault((KeyValueParams s) => s.key == "sort");
            KeyValueParams korder = paras.FirstOrDefault((KeyValueParams s) => s.key == "order");
            //分頁排序
            if (ksort.value != null && korder.value != null)
            {
                string sort = ksort.value.ToString();
                string order = korder.value.ToString();

                if (ksort.value.ToString() == "MapName")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.MapName);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.MapName);
                    }
                }
                else if (ksort.value.ToString() == "CheckYear")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.CheckYear);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.CheckYear);
                    }
                }
                else if (ksort.value.ToString() == "CheckCount")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.CheckCount);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.CheckCount);
                    }
                }
                else if (ksort.value.ToString() == "CheckAllDoesmeet")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.CheckAllDoesmeet);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.CheckAllDoesmeet);
                    }
                }
                else if (ksort.value.ToString() == "CheckNoHiatusCount")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.CheckNoHiatusCount);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.CheckNoHiatusCount);
                    }
                }
                else if (ksort.value.ToString() == "Average")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.double_Average);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.double_Average);
                    }
                }
            }
            else
            {
                //預設排序
                result = result.OrderBy(a => a.MapName).ThenBy(a => a.CheckYear);
            }

            return base.BeforeIQueryToPagedList(result.AsQueryable(), paras);
        }

        public ActionResult ExportAudit_StatisticReportEquipView(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G統計報表專區_設備檢查缺失);
            string fileTitle = "查核輔導專區_設備檢查缺失";

            List<string> titles = new List<string>();

            //報表類型
            var RKind = KeyValue.GetFilterParaValue(paras, "RKind");
            if (RKind == "1")
            {                
                titles.Add("各縣市設備檢查缺失彙整表，查詢條件:");
            }
            else if (RKind == "2")
            {                
                titles.Add("各集團站設備檢查缺失比較表，查詢條件:");
            }

            var result = GetOutputData(ref titles, paras);
            //預設排序
            result = result.OrderBy(a => a.MapName).ThenBy(a => a.CheckYear);

            //產出Dynamic資料 (給Excel)
            List<dynamic> list = new List<dynamic>();
            var output = result.ToList();
            foreach (var data in output)
            {
                dynamic f = new ExpandoObject();
                if (RKind == "1")
                {
                    f.縣市別 = data.MapName;                    
                }
                else if (RKind == "2")
                {
                    f.營業主體 = data.MapName;                    
                }
                f.年度 = data.CheckYear;
                f.檢查家數_家 = data.CheckCount;
                f.缺失數_項次 = data.CheckAllDoesmeet;
                f.零缺失家數_家 = data.CheckNoHiatusCount;
                f.平均缺失數_項家 = data.Average;                

                f.SheetName = fileTitle;//sheep.名稱;
                list.Add(f);
            }

            //查無符合資料表數
            if (list.Count == 0)
            {
                return Json(new { result = false, errorMessage = "查無符合資料表數" }, JsonRequestBehavior.AllowGet);
            }

            //產出excel
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

        public ActionResult CountAudit_StatisticReportEquipView(params KeyValueParams[] paras)
        {
            //進入頁面不顯示清單(未使用查詢)            
            if (paras == null)
            {
                return null;
            }

            List<string> titles = new List<string>();
            var datas = GetOutputData(ref titles, paras);            
            datas = datas.GroupBy(a => a.CheckYear)
                .Select(a => new
                {
                    CheckYear = a.Key,
                    CheckCount = a.Sum(p => p.CheckCount),
                    CheckAllDoesmeet = a.Sum(p => p.CheckAllDoesmeet),
                    CheckNoHiatusCount = a.Sum(p => p.CheckNoHiatusCount)                    
                }).Select(a => new vw_Audit_StatisticReportEquipView
                {                    
                    CheckYear = a.CheckYear,
                    CheckCount = a.CheckCount,
                    CheckAllDoesmeet = a.CheckAllDoesmeet,
                    CheckNoHiatusCount = a.CheckNoHiatusCount,
                    Average = String.Format("{0:N2}", Math.Round((double)a.CheckAllDoesmeet / a.CheckCount, 2))
                });

            //預設排序
            datas = datas.OrderBy(a => a.MapName).ThenBy(a => a.CheckYear);

            var opts = Dou.Misc.DataManagerScriptHelper.GetDataManagerOptions<vw_Audit_StatisticReportEquipView>();
            opts.addable = false;
            opts.editable = false;
            opts.deleteable = false;

            //全部欄位排序
            foreach (var field in opts.fields)
            {
                field.sortable = false;
                field.filter = false;                
            }

            opts.GetFiled("MapName").visible = false;
            opts.datas = datas;

            var jstr = JsonConvert.SerializeObject(opts, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            jstr = jstr.Replace(DataManagerScriptHelper.JavaScriptFunctionStringStart, "(").Replace(DataManagerScriptHelper.JavaScriptFunctionStringEnd, ")");
            return Content(jstr, "application/json");
        }

        public ActionResult ExportCountAudit_StatisticReportEquipView(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G統計報表專區_設備檢查缺失_合計);
            string fileTitle = "查核輔導專區_設備檢查缺失_合計";

            List<string> titles = new List<string>();

            //報表類型
            var RKind = KeyValue.GetFilterParaValue(paras, "RKind");
            if (RKind == null)
                RKind = "1";

            if (RKind == "1")
            {
                titles.Add("各縣市設備檢查缺失彙整表_合計，查詢條件:");
            }
            else if (RKind == "2")
            {
                titles.Add("各集團站設備檢查缺失比較表_合計，查詢條件:");
            }

            var datas = GetOutputData(ref titles, paras);
            datas = datas.GroupBy(a => a.CheckYear)
                .Select(a => new
                {
                    CheckYear = a.Key,
                    CheckCount = a.Sum(p => p.CheckCount),
                    CheckAllDoesmeet = a.Sum(p => p.CheckAllDoesmeet),
                    CheckNoHiatusCount = a.Sum(p => p.CheckNoHiatusCount)
                }).Select(a => new vw_Audit_StatisticReportEquipView
                {
                    CheckYear = a.CheckYear,
                    CheckCount = a.CheckCount,
                    CheckAllDoesmeet = a.CheckAllDoesmeet,
                    CheckNoHiatusCount = a.CheckNoHiatusCount,
                    Average = String.Format("{0:N2}", Math.Round((double)a.CheckAllDoesmeet / a.CheckCount, 2))
                });

            //產出Dynamic資料 (給Excel)
            List<dynamic> list = new List<dynamic>();
            var output = datas.ToList();
            foreach (var data in output)
            {
                dynamic f = new ExpandoObject();
                f.年度 = data.CheckYear;
                f.檢查家數_家 = data.CheckCount;
                f.缺失數_項次 = data.CheckAllDoesmeet;
                f.零缺失家數_家 = data.CheckNoHiatusCount;
                f.平均缺失數_項家 = data.Average;

                f.SheetName = fileTitle;//sheep.名稱;
                list.Add(f);
            }

            //查無符合資料表數
            if (list.Count == 0)
            {
                return Json(new { result = false, errorMessage = "查無符合資料表數" }, JsonRequestBehavior.AllowGet);
            }

            //產出excel
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

        private IEnumerable<vw_Audit_StatisticReportEquipView> GetOutputData(ref List<string> titles, params KeyValueParams[] paras)
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
            Dou.Models.DB.IModelEntity<Check_Basic_View> check_Basic_View = new Dou.Models.DB.ModelEntity<Check_Basic_View>(dbContext);
            Dou.Models.DB.IModelEntity<CityCode> cityCode = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);

            var iquery = check_Basic_View.GetAll()
                .Where(a => a.Check_Style != null);//and Check_Style is not null
                                                   //.Where(a => (a.CityName == "宜蘭縣" || a.CityName == "花蓮縣" || a.CityName == "臺東縣") && a.CheckYear == 95);
                                                   //.Where(a => a.CityName == "宜蘭縣" && a.CheckYear == 103);

            //權限查詢
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysCodes();
            iquery = iquery.Where(a => pCitys.Any(b => a.AreaCode == b));

            //條件
            //預設-報表類型
            var RKind = KeyValue.GetFilterParaValue(paras, "RKind");
            if (string.IsNullOrEmpty(RKind))
                RKind = "1";

            //預設-石油設施類型
            var CaseType = KeyValue.GetFilterParaValue(paras, "CaseType");            
            if (string.IsNullOrEmpty(CaseType))
                CaseType = "CarFuel_BasicData";

            var CityCode1 = KeyValue.GetFilterParaValue(paras, "CityCode1");
            var StrYear = KeyValue.GetFilterParaValue(paras, "StrYear");
            var Business_theme = KeyValue.GetFilterParaValue(paras, "Business_theme");

            //石油設施類型
            if (!string.IsNullOrEmpty(CaseType))
            {
                iquery = iquery.Where(a => a.CaseType == CaseType);
                titles.Add("石油設施類型:" + Code.GetCaseType().Where(a => a.Key == CaseType).FirstOrDefault().Value);
            }

            //年度
            if (!string.IsNullOrEmpty(StrYear))
            {
                List<string> sels = StrYear.Split(',').ToList();
                iquery = iquery.Where(a => sels.Any(b => (a.CheckDate.Year - 1911).ToString() == b));
                titles.Add("查核年度:" + string.Join(",", sels));
            }

            //代碼-縣市別
            if (!string.IsNullOrEmpty(CityCode1))
            {
                List<string> sels = CityCode1.Split(',').ToList();
                iquery = iquery.Where(a => sels.Any(b => a.AreaCode == b));
                
                //代碼-縣市別
                var citys = cityCode.GetAll().Where(a => sels.Any(b => b == a.CityCode1)).OrderBy(a => a.Rank);
                titles.Add("縣市別:" + string.Join(",", citys.Select(a => a.CityName).ToList()));
            }

            //代碼-營業主體
            if (!string.IsNullOrEmpty(Business_theme))
            {
                List<string> sels = Business_theme.Split(',').ToList();
                iquery = iquery.Where(a => sels.Any(b => a.Business_theme == b));

                //titles add
                //代碼-營業主體
                Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> dal = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);
                var text = dal.GetAll().Where(a => sels.Any(b => b == a.Value)).Select(a => a.Name).ToList();
                titles.Add("營業主體:" + string.Join(",", text));
            }

            //統計:Group By
            IEnumerable<vw_Audit_StatisticReportEquipView> result;

            if (RKind == "1")
            {
                //各縣市設備檢查缺失彙整表(1)
                var tmp = iquery
                    .GroupBy(a => new { a.AreaCode, CheckDate = a.CheckDate.Year - 1911 })
                    .Select(a => new
                    {
                        AreaCode = a.Key.AreaCode,
                        CheckYear = a.Key.CheckDate,
                        CheckCount = a.Count(),
                        CheckAllDoesmeet = a.Sum(p => p.AllDoesmeet ?? 0),
                        CheckNoHiatusCount = a.Sum(p => p.AllDoesmeet == null || p.AllDoesmeet == 0 ? 1 : 0),
                    })
                    .Select(a => new
                    {
                        AreaCode = a.AreaCode,
                        CheckYear = a.CheckYear,
                        CheckCount = a.CheckCount,
                        CheckAllDoesmeet = a.CheckAllDoesmeet,
                        CheckNoHiatusCount = a.CheckNoHiatusCount,
                        Average = Math.Round((double)a.CheckAllDoesmeet / a.CheckCount, 2)
                    }).Distinct();

                //結果:(vw)物件回傳
                Dou.Models.DB.IModelEntity<vw_Audit_StatisticReportEquipView> vw = new Dou.Models.DB.ModelEntity<vw_Audit_StatisticReportEquipView>(dbContext);
                //報表呈現(1or2)
                result = vw.GetAll().AsEnumerable().Where(a => a.RKind.ToString() == RKind);
                result = result.Join(tmp,
                    a => new { a.MapCode, a.CheckYear },
                    b => new { MapCode = b.AreaCode, b.CheckYear },
                    (o, c) => new vw_Audit_StatisticReportEquipView
                    {
                        MapName = o.MapName,
                        CheckYear = o.CheckYear,
                        CheckCount = c.CheckCount,
                        CheckAllDoesmeet = c.CheckAllDoesmeet,
                        CheckNoHiatusCount = c.CheckNoHiatusCount,
                        Average = String.Format("{0:N2}", c.Average)
                    });
            }
            else
            {
                //各集團站設備檢查缺失比較表(2)
                var tmp = iquery
                    .GroupBy(a => new { a.Business_theme, CheckDate = a.CheckDate.Year - 1911 })
                    .Select(a => new
                    {
                        Business_theme = a.Key.Business_theme,
                        CheckYear = a.Key.CheckDate,
                        CheckCount = a.Count(),
                        CheckAllDoesmeet = a.Sum(p => p.AllDoesmeet ?? 0),
                        CheckNoHiatusCount = a.Sum(p => p.AllDoesmeet == null || p.AllDoesmeet == 0 ? 1 : 0),
                    })
                    .Select(a => new
                    {
                        Business_theme = a.Business_theme,
                        CheckYear = a.CheckYear,
                        CheckCount = a.CheckCount,
                        CheckAllDoesmeet = a.CheckAllDoesmeet,
                        CheckNoHiatusCount = a.CheckNoHiatusCount,
                        Average = Math.Round((double)a.CheckAllDoesmeet / a.CheckCount, 2)
                    }).Distinct();

                //結果:(vw)物件回傳
                Dou.Models.DB.IModelEntity<vw_Audit_StatisticReportEquipView> vw = new Dou.Models.DB.ModelEntity<vw_Audit_StatisticReportEquipView>(dbContext);
                //報表呈現(1or2)
                result = vw.GetAll().AsEnumerable().Where(a => a.RKind.ToString() == RKind);
                result = result.Join(tmp,
                    a => new { a.MapCode, a.CheckYear },
                    b => new { MapCode = b.Business_theme, b.CheckYear },
                    (o, c) => new vw_Audit_StatisticReportEquipView
                    {
                        MapName = o.MapName,
                        CheckYear = o.CheckYear,
                        CheckCount = c.CheckCount,
                        CheckAllDoesmeet = c.CheckAllDoesmeet,
                        CheckNoHiatusCount = c.CheckNoHiatusCount,
                        Average = String.Format("{0:N2}", c.Average)
                    });                
            }

            return result;
        }
    }

    public class vw_Audit_StatisticReportEquipView
    {
        [Key]
        [Display(Name = "縣市別")]
        [ColumnDef(Sortable = true)]
        [Column(Order = 1)]
        public string MapName { get; set; }

        [Key]
        [Display(Name = "年度")]
        [ColumnDef(Sortable = true)]
        [Column(Order = 2)]
        public int CheckYear { get; set; }

        [Display(Name = "縣市別")]
        [ColumnDef(Visible = false)]
        public string MapCode { get; set; }        

        [Display(Name = "檢查家數(家)")]
        [ColumnDef(Sortable = true)]
        public int CheckCount { get; set; }

        [ColumnDef(Sortable = true)]
        [Display(Name = "缺失數(項次)")]
        public int CheckAllDoesmeet { get; set; }

        [ColumnDef(Sortable = true)]
        [Display(Name = "零缺失家數(家)")]
        public int CheckNoHiatusCount { get; set; }

        [ColumnDef(Sortable = true)]
        [Display(Name = "平均缺失數(項/家)")]
        public string Average { get; set; }


        //排序用
        [ColumnDef(Visible = false)]
        public double double_Average
        {
            get
            {
                double d = 0;
                double.TryParse(this.Average, out d);
                return d;
            }
        }

        [Display(Name = "報表類型", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
                Filter = true, SelectItems = "{\"1\":\"各縣市設備檢查缺失彙整表\",\"2\":\"各集團站設備檢查缺失比較表\"}")]
        public int RKind { get; set; }
        
        [Display(Name = "石油設施類型", Order = 2)]        
        [ColumnDef(Visible = false, Filter = true, EditType = EditType.Select,
            SelectItems = "{\"CarFuel_BasicData\":\"汽/機車加油站\",\"FishGas_BasicData\":\"漁船加油站\",\"SelfFuel_Basic\":\"自用加儲油\"}")]
        public string CaseType { get; }

        [Display(Name = "年度", Order = 3)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
                Filter = true, SelectItemsClassNamespace = OilGas.Controllers.Audit.Audit_StatisticReportEquipViewYearSelectItems.AssemblyQualifiedName)]
        public string StrYear { get; }

        [Display(Name = "縣市別", Order = 4)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
                Filter = true, SelectItemsClassNamespace = UsercityCodeSelectItems.AssemblyQualifiedName)]
        public string CityCode1 { get; }

        [Display(Name = "營業主體", Order = 5)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
            Filter = true,
            SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
            SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_BusinessOrganization, OilGas",
            SelectSourceModelValueField = "Value",
            SelectSourceModelDisplayField = "Name")]
        [StringLength(70)]
        public string Business_theme { get; }
    }

    public class Audit_StatisticReportEquipViewYearSelectItems : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Controllers.Audit.Audit_StatisticReportEquipViewYearSelectItems, OilGas";

        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return Code.GetYear();
        }
    }
}