using Dou.Controllers;
using Dou.Misc.Attr;
using Dou.Models.DB;
using NPOI.SS.Formula.Functions;
using NPOI.XSSF.Streaming.Values;
using OilGas.Models;
using OilGas.Models.TableView;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_ReportCheckProjectError", Name = "歷年加油站各檢查項目缺失統計", MenuPath = "查核輔導專區/G交叉分析報表", Action = "Index", Index = 7, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_ReportCheckProjectErrorController : AGenericModelController<vw_Audit_ReportCheckProjectErrorController>
    {
        // GET: Audit_ReportCheckProjectError
        public ActionResult Index()
        {
            return View();
        }

        protected override Dou.Models.DB.IModelEntity<vw_Audit_ReportCheckProjectErrorController> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<vw_Audit_ReportCheckProjectErrorController>(new OilGasModelContextExt());
        }

        protected override IEnumerable<vw_Audit_ReportCheckProjectErrorController> GetDataDBObject(IModelEntity<vw_Audit_ReportCheckProjectErrorController> dbEntity, params KeyValueParams[] paras)
        {
            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return new List<vw_Audit_ReportCheckProjectErrorController>().AsQueryable();
            }

            return base.GetDataDBObject(dbEntity, paras);
        }

        public ActionResult ExportAudit_ReportCheckProjectError(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G交叉分析報表_歷年加油站各檢查項目缺失統計);
            string fileTitle = "查核輔導專區_歷年加油站各檢查項目缺失統計";

            List<string> titles = new List<string>() { "查核輔導專區_歷年加油站各檢查項目缺失統計，查詢條件:" };            
            IEnumerable<dynamic> result = GetOutputData(ref titles, paras);

            //產出Dynamic資料 (給Excel)
            var list = result.ToList();

            //查無符合資料表數
            if (list.Count == 0)
            {
                return Json(new { result = false, errorMessage = "查無符合資料表數" }, JsonRequestBehavior.AllowGet);
            }

            foreach (var row in list)
            {
                var f = row;
                f.SheetName = fileTitle;//sheep.名稱;
            }

            //產出excel            
            string fileName = OilGas.ExcelSpecHelper.GenerateExcelByLinqF1(fileTitle, titles, list, folder, "Y");
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

        private IEnumerable<dynamic> GetOutputData(ref List<string> titles, params KeyValueParams[] paras)
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();

            Dou.Models.DB.IModelEntity<CheckItemList> checkItemList = new Dou.Models.DB.ModelEntity<CheckItemList>(dbContext);
            Dou.Models.DB.IModelEntity<Check_Basic> check_Basic = new Dou.Models.DB.ModelEntity<Check_Basic>(dbContext);

            var CaseType = KeyValue.GetFilterParaValue(paras, "CaseType");
            var SYear = KeyValue.GetFilterParaValue(paras, "SYear");
            var EYear = KeyValue.GetFilterParaValue(paras, "EYear");
            
            if (CaseType == "Check_Item")
            {
                titles.Add("石油設施類型:" + "汽/機車加油站");                
            }
            else if (CaseType == "Check_Item_Fish")
            {
                titles.Add("石油設施類型:" + "漁船加油站");                
            }
            else if (CaseType == "Check_Item_SelfUP")
            {
                titles.Add("石油設施類型:" + "自用加儲油設施(地上)");                
            }
            else if (CaseType == "Check_Item_SelfDown")
            {
                titles.Add("石油設施類型:" + "自用加儲油設施(地下)");                
            }
            titles.Add("查核年度:" + SYear + "~" + EYear);

            int workYear = 95; /////////////////////////////////xxxxxxxxxxxxxx
            if (!string.IsNullOrEmpty(SYear))
            {                
                workYear = int.Parse(SYear);
            }
            string workTable = Code.GetWorkTable(CaseType, workYear);

            //統計            
            var basic = check_Basic.GetAll().Select(a => new { 
                a.CheckNo,
                CheckYear = a.CheckDate == null ? 0 : ((DateTime)a.CheckDate).Year - 1911
            });

            if (!string.IsNullOrEmpty(SYear))
            {
                int num = int.Parse(SYear);
                basic = basic.Where(a => a.CheckYear >= num);
            }

            if (!string.IsNullOrEmpty(EYear))
            {
                int num = int.Parse(EYear);
                basic = basic.Where(a => a.CheckYear <= num);
            }

            //母體
            IQueryable<Check4Class> main = null;

            if (workTable == "Check_Item")
            {
                //資料多
                Dou.Models.DB.IModelEntity<vw_UNPIVOT_Check_Item> vw_Checks = new Dou.Models.DB.ModelEntity<vw_UNPIVOT_Check_Item>(dbContext);

                //(a)缺失 value == 2
                main = vw_Checks.GetAll()
                        .Join(basic, a => a.CheckNo, b => b.CheckNo, (o, c) => new Check3Class
                        {
                            CheckYear = c.CheckYear, 
                            Name = o.name, 
                            Value = o.value,
                        }).GroupBy(a => new { CheckYear = a.CheckYear, Name = a.Name }).Select(a => new Check4Class
                        {
                            CheckYear = a.Key.CheckYear,
                            Name = a.Key.Name,
                            HiatusCount = a.Where(p => p.Value == "2").Count()
                        });
            }
            else
            {
                //資料少
                Dou.Models.DB.IModelEntity<vw_UNPIVOT_Check_Item_Other> vw_Checks_Others = new Dou.Models.DB.ModelEntity<vw_UNPIVOT_Check_Item_Other>(dbContext);

                //(a)缺失 value == 2
                main = vw_Checks_Others.GetAll().Where(a => a.workTable == workTable)
                        .Join(basic, a => a.CheckNo, b => b.CheckNo, (o, c) => new Check3Class
                        {
                            CheckYear = c.CheckYear,
                            Name = o.name,
                            Value = o.value,
                        }).GroupBy(a => new { CheckYear = a.CheckYear, Name = a.Name }).Select(a => new Check4Class
                        {
                            CheckYear = a.Key.CheckYear,
                            Name = a.Key.Name,
                            HiatusCount = a.Where(p => p.Value == "2").Count()
                        });
            }

            //總數量
            Dou.Models.DB.IModelEntity<vw_UNION_Check_Item_For_AllDoesmeet> vw_AllDoesmeet = new Dou.Models.DB.ModelEntity<vw_UNION_Check_Item_For_AllDoesmeet>(dbContext);
            var a2 = vw_AllDoesmeet.GetAll().Where(a => a.workTable == workTable)
                            .Join(basic, a => a.CheckNo, b => b.CheckNo, (o, c) => new
                            {
                                c.CheckYear,
                                AllDoesmeet = o.AllDoesmeet == null ? 0 : o.AllDoesmeet
                            })
                            .GroupBy(a => a.CheckYear)
                            .Select(a => new
                            {
                                CheckYear = a.Key,
                                TotalCount = a.Sum(p => (int)p.AllDoesmeet)
                            });

            var datas = main.Join(a2, a => a.CheckYear, b => b.CheckYear, (o, c) => new
            {
                o.CheckYear,
                o.Name,
                o.HiatusCount,
                c.TotalCount,
            })
            .AsEnumerable()
            .Select(o => new Check4Class
            {
                CheckYear = o.CheckYear,
                Name = o.Name,
                HiatusCount = o.HiatusCount,
                TotalCount = o.TotalCount,
                HiatusCountPercent = Math.Round((double)o.HiatusCount * 100 / o.TotalCount , 2)
            })
            .OrderBy(a => a.CheckYear).ThenBy(a => a.Name)
            .ToList();

            //行列互換(實體化)
            var pivot = datas.ToPivotArray(
                           item => item.Name,
                           item => item.CheckYear,                           
                           v => v.Any() ? v.FirstOrDefault().HiatusCount : 0)
                           .ToList();
            

            var checkItems = checkItemList.GetAll()
                                .Where(a => a.CheckItemTable == workTable)
                                .ToList();

            //結果
            List<dynamic> result = new List<dynamic>();
            //各年度(Cross Join)
            var years = pivot.Select(a => a.CheckYear).ToList();
            //各項目
            var items = pivot.Where(a => a.CheckYear == years.FirstOrDefault());

            foreach (var row in items)
            {
                foreach (var v in row)
                {
                    string key = v.Key.ToString();                    

                    var t = checkItems.Where(a => a.CheckItemDescNo == key).FirstOrDefault();
                    if (t != null)
                    {
                        dynamic f = new ExpandoObject();

                        f.檢查大項 = t.CheckItemTitel;
                        f.檢查細項 = t.CheckItemDesc;
                        
                        foreach(var year in years)
                        {
                            var data = datas.Where(a => a.CheckYear == year).Where(a => a.Name == key).FirstOrDefault();
                            if (data != null)
                            {
                                ((IDictionary<string, object>)f).Add(new KeyValuePair<string, object>(year.ToString() + "年缺失數", data.HiatusCount));
                                ((IDictionary<string, object>)f).Add(new KeyValuePair<string, object>(year.ToString() + "年缺失百分比", data.HiatusCountPercent.ToString() + "%"));
                            }
                        }

                        result.Add(f);
                    }
                }
            }

            return result;
        }
    }

    public class vw_Audit_ReportCheckProjectErrorController
    {
        [Display(Name = "石油設施類型", Order = 1)]
        [ColumnDef(Visible = false, Filter = true, EditType = EditType.Select,
            SelectItems = "{\"CarFuel_BasicData\":\"汽/機車加油站\"}")]
        public string CaseType { get; }

        [Display(Name = "查詢年(起)", Order = 2)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
            Filter = true, SelectItemsClassNamespace = OilGas.CheckYear2SelectItems.AssemblyQualifiedName)]
        public int SYear { get; }

        [Display(Name = "查詢年(迄)", Order = 3)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
        Filter = true, SelectItemsClassNamespace = OilGas.CheckYear2SelectItems.AssemblyQualifiedName)]
        public int EYear { get; }

    }
}