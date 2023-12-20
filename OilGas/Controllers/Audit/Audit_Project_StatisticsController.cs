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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_Project_Statistics", Name = "石油設施各檢查項目缺失數", MenuPath = "查核輔導專區/G統計報表專區", Action = "Index", Index = 6, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_Project_StatisticsController : AGenericModelController<vw_Audit_Project_Statistics>
    {
        // GET: Audit_Project_Statistics
        public ActionResult Index()
        {
            return View();
        }

        protected override Dou.Models.DB.IModelEntity<vw_Audit_Project_Statistics> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<vw_Audit_Project_Statistics>(new OilGasModelContextExt());
        }

        protected override IEnumerable<vw_Audit_Project_Statistics> GetDataDBObject(IModelEntity<vw_Audit_Project_Statistics> dbEntity, params KeyValueParams[] paras)
        {
            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return new List<vw_Audit_Project_Statistics>().AsQueryable();
            }

            List<string> titles = new List<string>();
            var result = GetOutputData(ref titles, paras);

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
                        result = result.OrderBy(a => a.CheckItemTitel);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.CheckItemTitel);
                    }
                }
                else if (ksort.value.ToString() == "CheckItemDesc")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.CheckItemDesc);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.CheckItemDesc);
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
                else if (ksort.value.ToString() == "CheckItemTotalCount")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.CheckItemTotalCount);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.CheckItemTotalCount);
                    }
                }
                else if (ksort.value.ToString() == "CheckItemPercentage")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.CheckItemPercentage);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.CheckItemPercentage);
                    }
                }                
            }
            else
            {
                //預設排序              
                result = result.OrderBy(a => a.CheckItemTitelNo).ThenBy(a => a.CheckItemDescNo);
            }

            return result;
        }

        public ActionResult ExportAudit_Project_Statistics(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G統計報表專區_石油設施各檢查項目缺失數);
            string fileTitle = "查核輔導專區_石油設施各檢查項目缺失數";

            List<string> titles = new List<string>() { "查核輔導專區_石油設施各檢查項目缺失數，查詢條件:" }; ;

            var result = GetOutputData(ref titles, paras);
            //預設排序            
            result = result.OrderBy(a => a.CheckItemTitelNo).ThenBy(a => a.CheckItemDescNo);

            //產出Dynamic資料 (給Excel)
            List<dynamic> list = new List<dynamic>();
            var output = result.ToList();

            foreach (var data in output)
            {
                dynamic f = new ExpandoObject();
                f.設備名稱 = data.CheckItemTitel;
                f.檢查項目 = data.CheckItemDesc;
                f.缺失家數_家 = data.CheckItemErrCount;
                f.缺失百分比 = data.CheckItemPercentage.ToString() + "%";

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

        private IEnumerable<vw_Audit_Project_Statistics> GetOutputData(ref List<string> titles, params KeyValueParams[] paras)
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();

            Dou.Models.DB.IModelEntity<Check_Basic> check_Basic = new Dou.Models.DB.ModelEntity<Check_Basic>(dbContext);
            Dou.Models.DB.IModelEntity<vw_UNPIVOT_Check_Item> vw_Checks = new Dou.Models.DB.ModelEntity<vw_UNPIVOT_Check_Item>(dbContext);
            Dou.Models.DB.IModelEntity<vw_UNPIVOT_Check_Item_Other> vw_Checks_Others = new Dou.Models.DB.ModelEntity<vw_UNPIVOT_Check_Item_Other>(dbContext);

            var CaseType = KeyValue.GetFilterParaValue(paras, "CaseType");
            string strWorkYear = KeyValue.GetFilterParaValue(paras, "WorkYear");            
            string CheckItemTableType = KeyValue.GetFilterParaValue(paras, "CheckItemTableType");

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
                        .Where(a => a.CheckItemTable == workTable);

            //條件
            if (!string.IsNullOrEmpty(CheckItemTableType))
            {
                List<string> sels = CheckItemTableType.Split(',').ToList();
                iquery = iquery.Where(a => sels.Any(b => (a.CheckItemTitelNo.ToString() == b)));
                
                var tts = checkItemList.GetAll().Where(a => a.CheckItemTable == workTable)
                    .Where(a => sels.Any(b => a.CheckItemTitelNo == b))
                    .Select(a => a.CheckItemTitel).Distinct();
                titles.Add("檢查設備:" + string.Join(",", tts));

            }

            var result = iquery
                        .GroupBy(a => new { a.CheckItemTitel, a.CheckItemTitelNo, a.CheckItemDesc, a.CheckItemDescNo })
                        .Select(a => new vw_Audit_Project_Statistics
                        {
                            CheckItemTitel = a.Key.CheckItemTitel,
                            CheckItemTitelNo = a.Key.CheckItemTitelNo,
                            CheckItemDesc = a.Key.CheckItemDesc,
                            CheckItemDescNo = a.Key.CheckItemDescNo,
                            CheckItemCount = a.Count(),
                            CheckItemErrCount = 0,
                            CheckItemTotalCount = 0                            
                        }).AsEnumerable();

            //統計
            IQueryable<AmountClass> jAmount;

            //(a)缺失 value == 2
            if (workTable == "Check_Item")
            {
                //資料多
                jAmount = vw_Checks.GetAll()
                            .Join(check_Basic.GetAll(), a => a.CheckNo, b => b.CheckNo, (o, c) => new { o.CheckNo, o.name, o.value, c.CheckDate })
                            .Where(a => a.CheckDate != null && ((DateTime)a.CheckDate).Year - 1911 == WorkYear)
                            .GroupBy(a => a.name).Select(a => new AmountClass
                            {
                                name = a.Key,
                                amount = a.Where(p => p.value == "2").Count(),
                                TotalAmount = a.Count()
                            });
            }
            else
            {
                //資料少
                jAmount = vw_Checks_Others.GetAll().Where(a => a.workTable == workTable)
                            .GroupJoin(check_Basic.GetAll(), a => a.CheckNo, b => b.CheckNo, (o, c) => new { o.CheckNo, o.name, o.value, c.FirstOrDefault().CheckDate, c })
                            .Where(a => a.CheckDate != null && ((DateTime)a.CheckDate).Year - 1911 == WorkYear)
                            .GroupBy(a => a.name).Select(a => new AmountClass
                            {
                                name = a.Key,
                                amount = a.Where(p => p.value == "2").Count(),
                                TotalAmount = a.Count()
                            });
            }

            //////配合舊系統, null算1筆
            //var aaa = jAmount.ToList();

            //結果
            result = result.GroupJoin(jAmount, a => a.CheckItemDescNo, b => b.name, (o, c) => new
            {
                CheckItemTitel = o.CheckItemTitel,
                CheckItemTitelNo = o.CheckItemTitelNo,
                CheckItemDesc = o.CheckItemDesc,
                CheckItemDescNo = o.CheckItemDescNo,
                CheckItemCount = o.CheckItemCount,
                CheckItemErrCount = c.FirstOrDefault() == null ? 0 : c.FirstOrDefault().amount,
                CheckItemTotalCount = c.FirstOrDefault() == null ? 0 : c.FirstOrDefault().TotalAmount,                
            })
            .Select(o => new vw_Audit_Project_Statistics
            {
                CheckItemTitel = o.CheckItemTitel,
                CheckItemTitelNo = o.CheckItemTitelNo,
                CheckItemDesc = o.CheckItemDesc,
                CheckItemDescNo = o.CheckItemDescNo,
                CheckItemCount = o.CheckItemCount,
                CheckItemErrCount = o.CheckItemErrCount,
                CheckItemTotalCount = o.CheckItemTotalCount,
                CheckItemPercentage = o.CheckItemTotalCount == 0 ? 0 : Math.Round((double)o.CheckItemErrCount / o.CheckItemTotalCount * 100, 2)
            });
                       
            return result;
        }

        public ActionResult GetDDLCheckItemTableType(string CaseType, int WorkYear)
        {
            string workTable = Code.GetWorkTable(CaseType, WorkYear);

            Dou.Models.DB.IModelEntity<CheckItemList> model = new Dou.Models.DB.ModelEntity<CheckItemList>(new OilGasModelContextExt());
            var types = model.GetAll()
                        .Where(a => a.CheckItemTable == workTable)                        
                        .Select(a => new
                        {
                            v = a.CheckItemTitelNo,
                            s = a.CheckItemTitel
                        }).Distinct()
                        .OrderBy(a => a.v)
                        .ToList();

            return Json(new { result = true, types = types }, JsonRequestBehavior.AllowGet);
        }
    }

    public class vw_Audit_Project_Statistics
    {
        [Key]
        [Display(Name = "設備名稱")]        
        [Column(Order = 1)]
        public string CheckItemTitel { get; set; }

        [Key]
        [Display(Name = "設備名稱代碼")]
        [ColumnDef(Visible = false)]
        [Column(Order = 2)]
        public string CheckItemTitelNo { get; set; }

        [Key]
        [Display(Name = "檢查項目")]        
        [Column(Order = 3)]
        public string CheckItemDesc{ get; set; }

        [Key]
        [Display(Name = "檢查項目代碼")]
        [ColumnDef(Visible = false)]
        [Column(Order = 4)]
        public string CheckItemDescNo { get; set; }

        [Display(Name = "設備名稱數量")]
        [ColumnDef(Visible = false)]
        public int CheckItemCount { get; set; }

        [Display(Name = "缺失家數(家)")]        
        public int CheckItemErrCount { get; set; }
        
        [Display(Name = "檢查家數(家)")]
        [ColumnDef(Visible = false)]
        public int CheckItemTotalCount { get; set; }

        [Display(Name = "缺失百分比(%)")]        
        public double CheckItemPercentage { get; set; }

        [Display(Name = "石油設施類型", Order = 1)]
        [ColumnDef(Visible = false, Filter = true, EditType = EditType.Select,            
            SelectItemsClassNamespace = OrganizationCaseTypeSelectItemsClassImp.AssemblyQualifiedName)]
        public string CaseType { get; }

        [Display(Name = "查核年度", Order = 2)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
            Filter = true, SelectItemsClassNamespace = OilGas.Controllers.Audit.Audit_StatisticReportEquipViewYearSelectItems.AssemblyQualifiedName)]
        public DateTime WorkYear { get; }
        
        [Display(Name = "檢查設備", Order = 2)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
            Filter = true, SelectItems = "{}")]
        public DateTime CheckItemTableType { get; }
    }
}