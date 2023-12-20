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
    [Dou.Misc.Attr.MenuDef(Id = "Audit_StatisticReportItemView", Name = "石油設施缺失項目彙整表", MenuPath = "查核輔導專區/G統計報表專區", Action = "Index", Index = 3, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_StatisticReportItemViewController : APaginationModelController<vw_Audit_StatisticReportItemView>
    {
        // GET: Audit_StatisticReportItemView
        public ActionResult Index()
        {
            return View();
        }

        protected override Dou.Models.DB.IModelEntity<vw_Audit_StatisticReportItemView> GetModelEntity()
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
            return new Dou.Models.DB.ModelEntity<vw_Audit_StatisticReportItemView>(dbContext);

            //return null;
        }

        protected override IQueryable<vw_Audit_StatisticReportItemView> BeforeIQueryToPagedList(IQueryable<vw_Audit_StatisticReportItemView> iquery, params KeyValueParams[] paras)
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
            var result = GetOutputData(ref titles, paras);

            KeyValueParams ksort = paras.FirstOrDefault((KeyValueParams s) => s.key == "sort");
            KeyValueParams korder = paras.FirstOrDefault((KeyValueParams s) => s.key == "order");
            //分頁排序
            if (ksort.value != null && korder.value != null)
            {
                string sort = ksort.value.ToString();
                string order = korder.value.ToString();

                if (ksort.value.ToString() == "ItemName")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.ItemName);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.ItemName);
                    }
                }
                else if (ksort.value.ToString() == "ItemHiatusCountByBusi")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.ItemHiatusCountByBusi);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.ItemHiatusCountByBusi);
                    }
                }
                else if (ksort.value.ToString() == "ItemHiatusCountByYear")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.ItemHiatusCountByYear);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.ItemHiatusCountByYear);
                    }
                }
                else if (ksort.value.ToString() == "ItemHiatusCheckByBusi")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.ItemHiatusCheckByBusi);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.ItemHiatusCheckByBusi);
                    }
                }
                else if (ksort.value.ToString() == "ItemHiatusCheckByYear")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.ItemHiatusCheckByYear);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.ItemHiatusCheckByYear);
                    }
                }
                else if (ksort.value.ToString() == "ItemHiatusPercentByBusi")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.ItemHiatusPercentByBusi);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.ItemHiatusPercentByBusi);
                    }
                }
                else if (ksort.value.ToString() == "ItemHiatusPercentByYear")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.ItemHiatusPercentByYear);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.ItemHiatusPercentByYear);
                    }
                }
            }
            else
            {
                //預設排序                
                result = result.OrderBy(a => a.CheckItemTitelNo).ThenBy(a => a.CheckItemDescNo);
            }

            return base.BeforeIQueryToPagedList(result.AsQueryable(), paras);
        }

        public ActionResult ExportAudit_StatisticReportItemView(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G統計報表專區_石油設施缺失項目彙整表);
            string fileTitle = "查核輔導專區_石油設施缺失項目彙整表";

            List<string> titles = new List<string>() { "查核輔導專區_石油設施缺失項目彙整表，查詢條件:" }; ;

            var result = GetOutputData(ref titles, paras);
            //預設排序            
            result = result.OrderBy(a => a.CheckItemTitelNo).ThenBy(a => a.CheckItemDescNo);            

            //產出Dynamic資料 (給Excel)
            List<dynamic> list = new List<dynamic>();
            var output = result.ToList();

            var Business_theme = KeyValue.GetFilterParaValue(paras, "Business_theme");
            string Business_theme_Name = CarVehicleGas_BusinessOrganizationVSelectItemsClassImp.BUSS.FirstOrDefault().Name;
            foreach (var data in output)
            {
                dynamic f = new ExpandoObject();
                f.缺失項目名稱 = data.ItemName;
                //f.缺失家_公司
                ((IDictionary<string, object>)f).Add(new KeyValuePair<string, object>(Business_theme_Name, data.ItemHiatusCountByBusi));
                f.全國缺失家數 = data.ItemHiatusCountByYear;
                //f.缺失百分比_公司                
                ((IDictionary<string, object>)f).Add(new KeyValuePair<string, object>("_" + Business_theme_Name + " ", data.ItemHiatusPercentByBusi + "%"));
                f._全國缺失百分比 = data.ItemHiatusPercentByYear + "%";

                f.SheetName = fileTitle;//sheep.名稱;
                list.Add(f);
            }

            //查無符合資料表數
            if (list.Count == 0)
            {
                return Json(new { result = false, errorMessage = "查無符合資料表數" }, JsonRequestBehavior.AllowGet);
            }

            //產出excel
            Dictionary<string, int> dicsHeaderMerge = new Dictionary<string, int>()
            {
                { "缺失家數(家)", 2 }, { "缺失百分比(%)", 2 },
            };


            string fileName = OilGas.ExcelSpecHelper.GenerateExcelByLinqF2(fileTitle, titles, dicsHeaderMerge, list, folder, "N");
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

        private IEnumerable<vw_Audit_StatisticReportItemView> GetOutputData(ref List<string> titles, params KeyValueParams[] paras)
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
                                                     
            Dou.Models.DB.IModelEntity<Check_Basic> check_Basic = new Dou.Models.DB.ModelEntity<Check_Basic>(dbContext);            
            Dou.Models.DB.IModelEntity<vw_UNPIVOT_Check_Item> vw_Checks = new Dou.Models.DB.ModelEntity<vw_UNPIVOT_Check_Item>(dbContext);
            Dou.Models.DB.IModelEntity<vw_UNPIVOT_Check_Item_Other> vw_Checks_Others = new Dou.Models.DB.ModelEntity<vw_UNPIVOT_Check_Item_Other>(dbContext);

            //全國
            var iqueryAll = check_Basic.GetAll();

            //條件
            var CaseType = KeyValue.GetFilterParaValue(paras, "CaseType");
            var Business_theme = KeyValue.GetFilterParaValue(paras, "Business_theme");
            var CheckDate_Start_Between_ = KeyValue.GetFilterParaValue(paras, "CheckDate-Start-Between_");
            var CheckDate_End_Between_ = KeyValue.GetFilterParaValue(paras, "CheckDate-End-Between_");

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

            if (!string.IsNullOrEmpty(CheckDate_Start_Between_))
            {
                DateTime date = DateTime.Parse(CheckDate_Start_Between_);
                iqueryAll = iqueryAll.Where(a => a.CheckDate >= date);                                
                titles.Add("查核日期(起):" + CheckDate_Start_Between_);
            }

            if (!string.IsNullOrEmpty(CheckDate_End_Between_))
            {
                DateTime date = DateTime.Parse(CheckDate_End_Between_);
                iqueryAll = iqueryAll.Where(a => a.CheckDate <= date);                
                titles.Add("查核日期(迄):" + CheckDate_End_Between_);
            }

            //(特別處理)workTable設定
            int workYear = DateTime.Now.Year;
            if (!string.IsNullOrEmpty(CheckDate_Start_Between_))
            {
                DateTime date = DateTime.Parse(CheckDate_Start_Between_);
                workYear = date.Year - 1911;
            }
            string workTable = Code.GetWorkTable(CaseType, workYear);

            //統計
            //單一公司
            var iqueryBusi = iqueryAll.Where(a => a.Business_theme == Business_theme);            
            string Business_theme_Name = CarVehicleGas_BusinessOrganizationVSelectItemsClassImp.BUSS.FirstOrDefault().Name;
            titles.Add("營業主體:" + Business_theme_Name);

            IQueryable<AmountClass> jHiatusCountByBusi;
            IQueryable<AmountClass> jHiatusCountByYear;
            IQueryable<AmountClass> jHiatusCheckByBusi;
            IQueryable<AmountClass> jHiatusCheckByYear;

            //(a)缺失 value == 2
            if (workTable == "Check_Item")
            {
                //資料多
                jHiatusCountByBusi = iqueryBusi.Join(vw_Checks.GetAll(), a => a.CheckNo, b => b.CheckNo, (o, c) => new { o.CheckNo, c.name, c.value })
                    .Where(a => a.value == "2")
                    .GroupBy(a => a.name).Select(a => new AmountClass { name = a.Key, amount = a.Count() });

                jHiatusCountByYear = iqueryAll.Join(vw_Checks.GetAll(), a => a.CheckNo, b => b.CheckNo, (o, c) => new { o.CheckNo, c.name, c.value })
                    .Where(a => a.value == "2")                    
                    .GroupBy(a => a.name).Select(a => new AmountClass { name = a.Key, amount = a.Count() });

                //(b)查核
                jHiatusCheckByBusi = iqueryBusi.Join(vw_Checks.GetAll(), a => a.CheckNo, b => b.CheckNo, (o, c) => new { o.CheckNo, c.name, c.value })
                    .GroupBy(a => a.name).Select(a => new AmountClass { name = a.Key, amount = a.Count() });

                jHiatusCheckByYear = iqueryAll.Join(vw_Checks.GetAll(), a => a.CheckNo, b => b.CheckNo, (o, c) => new { o.CheckNo, c.name, c.value })
                    .GroupBy(a => a.name).Select(a => new AmountClass { name = a.Key, amount = a.Count() });
            }
            else
            {
                //資料少
                var Checks = vw_Checks_Others.GetAll().Where(a => a.workTable == workTable);
                jHiatusCountByBusi = iqueryBusi.Join(Checks, a => a.CheckNo, b => b.CheckNo, (o, c) => new { o.CheckNo, c.name, c.value })
                    .Where(a => a.value == "2")
                    .GroupBy(a => a.name).Select(a => new AmountClass { name = a.Key, amount = a.Count() });

                jHiatusCountByYear = iqueryAll.Join(Checks, a => a.CheckNo, b => b.CheckNo, (o, c) => new { o.CheckNo, c.name, c.value })
                    .Where(a => a.value == "2")                    
                    .GroupBy(a => a.name).Select(a => new AmountClass { name = a.Key, amount = a.Count() });

                //(b)查核
                jHiatusCheckByBusi = iqueryBusi.Join(Checks, a => a.CheckNo, b => b.CheckNo, (o, c) => new { o.CheckNo, c.name, c.value })
                    .GroupBy(a => a.name).Select(a => new AmountClass { name = a.Key, amount = a.Count() });

                jHiatusCheckByYear = iqueryAll.Join(Checks, a => a.CheckNo, b => b.CheckNo, (o, c) => new { o.CheckNo, c.name, c.value })
                    .GroupBy(a => a.name).Select(a => new AmountClass { name = a.Key, amount = a.Count() });
            }

            //結果:(vw)物件回傳
            Dou.Models.DB.IModelEntity<vw_Audit_StatisticReportItemView> vw = new Dou.Models.DB.ModelEntity<vw_Audit_StatisticReportItemView>(dbContext);
            var result = vw.GetAll().Where(a => a.CheckItemTable == workTable).AsEnumerable();

            result = result.GroupJoin(jHiatusCountByBusi, a => a.CheckItemDescNo, b => b.name, (o, c) => new
                {
                    o.CheckItemTitelNo, o.CheckItemDescNo, o.ItemName, ItemHiatusCountByBusi = c.FirstOrDefault() == null ? 0 : c.FirstOrDefault().amount
                })
                .GroupJoin(jHiatusCountByYear, a => a.CheckItemDescNo, b => b.name, (o, c) => new
                {
                    o.CheckItemTitelNo, o.CheckItemDescNo, o.ItemName, o.ItemHiatusCountByBusi,
                    ItemHiatusCountByYear = c.FirstOrDefault() == null ? 0 : c.FirstOrDefault().amount
                })
                .GroupJoin(jHiatusCheckByBusi, a => a.CheckItemDescNo, b => b.name, (o, c) => new
                {
                    o.CheckItemTitelNo, o.CheckItemDescNo, o.ItemName, o.ItemHiatusCountByBusi, o.ItemHiatusCountByYear,
                    ItemHiatusCheckByBusi = c.FirstOrDefault() == null ? 0 : c.FirstOrDefault().amount
                })
                .GroupJoin(jHiatusCheckByYear, a => a.CheckItemDescNo, b => b.name, (o, c) => new
                {
                    o.CheckItemTitelNo, o.CheckItemDescNo,
                    o.ItemName, o.ItemHiatusCountByBusi, o.ItemHiatusCountByYear, o.ItemHiatusCheckByBusi,
                    ItemHiatusCheckByYear = c.FirstOrDefault() == null ? 0 : c.FirstOrDefault().amount
                })
                .Select(o=>new vw_Audit_StatisticReportItemView
                {
                    CheckItemDescNo = o.CheckItemDescNo,
                    CheckItemTitelNo = o.CheckItemTitelNo,
                    ItemName = o.ItemName,
                    ItemHiatusCountByBusi = o.ItemHiatusCountByBusi,
                    ItemHiatusCountByYear = o.ItemHiatusCountByYear,
                    ItemHiatusCheckByBusi = o.ItemHiatusCheckByBusi,
                    ItemHiatusCheckByYear = o.ItemHiatusCheckByYear,
                    ItemHiatusPercentByBusi = o.ItemHiatusCountByYear == 0 ? 0 : Math.Round(((double)o.ItemHiatusCountByBusi / o.ItemHiatusCountByYear) * 100, 2),
                    ItemHiatusPercentByYear = o.ItemHiatusCheckByYear == 0 ? 0 : Math.Round(((double)o.ItemHiatusCountByYear / o.ItemHiatusCheckByYear) * 100, 2),
                });

            //var ss = result.ToList();
            //var a1 = jHiatusCountByBusi.ToList();
            //var a2 = jHiatusCountByYear.ToList();
            //var a3 = jHiatusCheckByBusi.ToList();
            //var a4 = jHiatusCheckByYear.ToList();

            return result;
        }        
    }

    public class vw_Audit_StatisticReportItemView
    {
        [Display(Name = "缺失項目標頭編號")]
        [ColumnDef(Visible = false)]
        public string CheckItemTitelNo { get; set; }

        [Key]
        [Display(Name = "缺失項目代碼")]
        [ColumnDef(Visible = false)]
        [Column(Order = 1)]
        public string CheckItemDescNo { get; set; }

        [Key]
        [Display(Name = "缺失項目名稱")]
        [ColumnDef(Sortable = true)]
        [Column(Order = 2)]
        public string ItemName { get; set; }        

        [Display(Name = "工作報表")]
        [ColumnDef(Visible = false)]
        public string CheckItemTable { get; set; }

        [Display(Name = "oooo公司")]
        [ColumnDef(Sortable = true)]
        public int ItemHiatusCountByBusi { get; set; }

        [Display(Name = "全國缺失家數")]
        [ColumnDef(Sortable = true)]
        public int ItemHiatusCountByYear { get; set; }

        [Display(Name = "oooo公司")]
        [ColumnDef(Sortable = true)]
        public int ItemHiatusCheckByBusi { get; set; }

        [Display(Name = "全國查核家數")]
        [ColumnDef(Sortable = true)]
        public int ItemHiatusCheckByYear { get; set; }

        [Display(Name = "oooo公司")]
        [ColumnDef(Sortable = true)]
        public double ItemHiatusPercentByBusi { get; set; }

        [Display(Name = "全國缺失百分比")]
        [ColumnDef(Sortable = true)]
        public double ItemHiatusPercentByYear { get; set; }

        [Display(Name = "石油設施類型", Order = 1)]
        [ColumnDef(Visible = false, Filter = true, EditType = EditType.Select,
            SelectGearingWith = "Business_theme,CaseType,true",
            SelectItemsClassNamespace = OrganizationCaseTypeSelectItemsClassImp.AssemblyQualifiedName)]
        public string CaseType { get; }

        [Display(Name = "查核區間", Order = 2)]
        [ColumnDef(Visible = false, Filter = true, EditType = EditType.Date, FilterAssign = FilterAssignType.Between)]
        public DateTime CheckDate { get; }

        [Display(Name = "營業主體", Order = 3)]
        [ColumnDef(Visible = false, Filter = true, EditType = EditType.Select, 
            SelectItemsClassNamespace = CarVehicleGas_BusinessOrganizationVSelectItemsClassImp.AssemblyQualifiedName)]
        [StringLength(70)]
        public string Business_theme { get; }


        
    }
}