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
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_Missing_statistics_origin", Name = "石油設施各項設備檢查缺失數", MenuPath = "查核輔導專區/G統計報表專區", Action = "Index", Index = 5, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_Missing_statistics_originController : AGenericModelController<vw_Audit_Missing_statistics_origin>
    {
        // GET: Audit_Missing_statistics_origin
        public ActionResult Index()
        {
            return View();
        }

        protected override Dou.Models.DB.IModelEntity<vw_Audit_Missing_statistics_origin> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<vw_Audit_Missing_statistics_origin>(new OilGasModelContextExt());
        }

        protected override IEnumerable<vw_Audit_Missing_statistics_origin> GetDataDBObject(IModelEntity<vw_Audit_Missing_statistics_origin> dbEntity, params KeyValueParams[] paras)
        {
            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return new List<vw_Audit_Missing_statistics_origin>().AsQueryable();
            }

            List<string> titles = new List<string>();
            var result = GetOutputData(ref titles, paras);

            //查詢錯誤，顯示0筆
            if (result == null)
            {
                return new List<vw_Audit_Missing_statistics_origin>().AsQueryable();                
            }

            KeyValueParams ksort = paras.FirstOrDefault((KeyValueParams s) => s.key == "sort");
            KeyValueParams korder = paras.FirstOrDefault((KeyValueParams s) => s.key == "order");
            //分頁排序
            if (ksort.value != null && korder.value != null)
            {
                string sort = ksort.value.ToString();
                string order = korder.value.ToString();

                if (ksort.value.ToString() == "CheckItemTitel")
                {
                    if (order == "asc")
                    {
                        //result = result.OrderBy(a => a.CheckItemTitel);
                        result = result.OrderBy(a => a.CheckItemTitelNo).ThenBy(a => a.CheckItemTitel);
                    }
                    else if (order == "desc")
                    {
                        //result = result.OrderByDescending(a => a.CheckItemTitel);
                        result = result.OrderByDescending(a => a.CheckItemTitelNo).ThenByDescending(a => a.CheckItemTitel);
                    }
                }
                else if (ksort.value.ToString() == "CheckItemCount")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.CheckItemCount);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.CheckItemCount);
                    }
                }
                else if (ksort.value.ToString() == "CheckItemErrCount")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.CheckItemErrCount);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.CheckItemErrCount);
                    }
                }                
            }
            else
            {
                //預設排序                
                result = result.OrderBy(a => a.CheckItemTitelNo).ThenBy(a => a.CheckItemTitel);
            }

            return result;            
        }

        public ActionResult ExportAudit_Missing_statistics_origin(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G統計報表專區_石油設施各項設備檢查缺失數);
            string fileTitle = "查核輔導專區_石油設施各項設備檢查缺失數";

            List<string> titles = new List<string>() { "查核輔導專區_石油設施各項設備檢查缺失數，查詢條件:" }; ;

            var result = GetOutputData(ref titles, paras);
            //預設排序            
            result = result.OrderBy(a => a.CheckItemTitelNo).ThenBy(a => a.CheckItemTitel);

            //產出Dynamic資料 (給Excel)
            List<dynamic> list = new List<dynamic>();
            var output = result.ToList();

            var Business_theme = KeyValue.GetFilterParaValue(paras, "Business_theme");
            string Business_theme_Name = CarVehicleGas_BusinessOrganizationVSelectItemsClassImp.BUSS.FirstOrDefault().Name;
            foreach (var data in output)
            {
                dynamic f = new ExpandoObject();
                f.檢查設備名稱 = data.CheckItemTitel;
                f.檢查項目 = data.CheckItemCount;
                f.缺失數_項次 = data.CheckItemErrCount;

                f.SheetName = fileTitle;//sheep.名稱;
                list.Add(f);
            }

            //合計
            dynamic lastd = new ExpandoObject();
            lastd.檢查設備名稱 = "合計";
            lastd.檢查項目 = list.Sum(a => a.檢查項目);
            lastd.缺失數_項次 = list.Sum(a => a.缺失數_項次);

            lastd.SheetName = fileTitle;//sheep.名稱;
            list.Add(lastd);

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

        private IEnumerable<vw_Audit_Missing_statistics_origin> GetOutputData(ref List<string> titles, params KeyValueParams[] paras)
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();            
            Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> carVehicleGas_BusinessOrganization = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);

            var CaseType = KeyValue.GetFilterParaValue(paras, "CaseType");            
            string strWorkYear = KeyValue.GetFilterParaValue(paras, "WorkYear");
            int WorkYear = 0;
            if (string.IsNullOrEmpty(strWorkYear))
            {
                return null;
            }
            else
            {
                WorkYear = int.Parse(strWorkYear);
            }

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
            
            Dou.Models.DB.IModelEntity<CheckItemList> checkItemList = new Dou.Models.DB.ModelEntity<CheckItemList>(dbContext);
            var iquery = checkItemList.GetAll()
                        .Where(a => a.CheckItemTable == workTable)
                        .GroupBy(a => new { a.CheckItemTitel, a.CheckItemTitelNo, a.CheckItemTitelSum })
                        .Select(a => new vw_Audit_Missing_statistics_origin
                        {
                            CheckItemTitel = a.Key.CheckItemTitel,
                            CheckItemTitelNo = a.Key.CheckItemTitelNo,
                            CheckItemTitelSum = a.Key.CheckItemTitelSum,
                            CheckItemCount = a.Count(),
                            CheckItemErrCount = 0
                        });

            //主表變動(workTable)
            Dou.Models.DB.IModelEntity<Check_Basic> check_Basic = new Dou.Models.DB.ModelEntity<Check_Basic>(dbContext);
            Dou.Models.DB.IModelEntity<vw_UNPIVOT_Check_Item_For_Doesmeet> checks = new Dou.Models.DB.ModelEntity<vw_UNPIVOT_Check_Item_For_Doesmeet>(dbContext);

            var main = checks.GetAll().Where(a => a.workTable == workTable)
                                .Where(a => a.CheckDate != null && a.CheckDate.Value.Year - 1911 == WorkYear)
                        .Join(check_Basic.GetAll(), a => a.CheckNo, b => b.CheckNo, (o, c) => new
                        {
                            o.name,
                            value = c == null ? 0 : o.value
                        })
                        .GroupBy(a => a.name).Select(a => new
                        {
                            name = a.Key,
                            amount = a.Sum(b => b.value)
                        });

            iquery = iquery.GroupJoin(main, a => a.CheckItemTitelSum, b => b.name, (o, c) => new vw_Audit_Missing_statistics_origin
            {
                CheckItemTitel = o.CheckItemTitel,
                CheckItemTitelNo = o.CheckItemTitelNo,
                CheckItemTitelSum = o.CheckItemTitelSum,
                CheckItemCount = o.CheckItemCount,
                CheckItemErrCount = c.FirstOrDefault() == null ? 0 : c.FirstOrDefault().amount
            });

            return iquery;
        }
    }

    public class vw_Audit_Missing_statistics_origin
    {
        [Key]
        [Display(Name = "檢查設備名稱")]
        [ColumnDef(Sortable = true)]
        [Column(Order = 1)]
        public string CheckItemTitel { get; set; }

        [Key]
        [Display(Name = "檢查設備代碼")]
        [ColumnDef(Visible = false)]
        [Column(Order = 2)]
        public string CheckItemTitelNo { get; set; }

        [Key]
        [Display(Name = "檢查設備合計字串")]
        [ColumnDef(Visible = false)]
        [Column(Order = 3)]
        public string CheckItemTitelSum { get; set; }

        [Display(Name = "工作報表")]
        [ColumnDef(Visible = false)]
        public string CheckItemTable { get; set; }
        
        [Display(Name = "檢查項目")]
        [ColumnDef(Sortable = true)]
        public int CheckItemCount { get; set; }

        [Display(Name = "缺失數(項次)")]
        [ColumnDef(Sortable = true)]
        public int CheckItemErrCount { get; set; }

        [Display(Name = "石油設施類型", Order = 1)]
        [ColumnDef(Visible = false, Filter = true, EditType = EditType.Select,
            SelectGearingWith = "Business_theme,CaseType,true",
            SelectItemsClassNamespace = OrganizationCaseTypeSelectItemsClassImp.AssemblyQualifiedName)]
        public string CaseType { get; }

        [Display(Name = "查核年度", Order = 2)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
            Filter = true, SelectItemsClassNamespace = OilGas.Controllers.Audit.Audit_StatisticReportEquipViewYearSelectItems.AssemblyQualifiedName)]
        public DateTime WorkYear { get; }


    }
}