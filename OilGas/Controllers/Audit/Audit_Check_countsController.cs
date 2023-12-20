using Dou.Controllers;
using Dou.Misc.Attr;
using Dou.Models.DB;
using NPOI.OpenXmlFormats.Dml.Diagram;
using NPOI.SS.Formula.Functions;
using NPOI.XSSF.Streaming.Values;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_Check_counts", Name = "已查核家數統計表", MenuPath = "查核輔導專區/G統計報表專區", Action = "Index", Index = 7, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_Check_countsController : AGenericModelController<vw_Audit_Check_counts>
    {
        // GET: Audit_Check_counts
        public ActionResult Index()
        {
            return View();
        }

        protected override Dou.Models.DB.IModelEntity<vw_Audit_Check_counts> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<vw_Audit_Check_counts>(new OilGasModelContextExt());
        }

        protected override IEnumerable<vw_Audit_Check_counts> GetDataDBObject(IModelEntity<vw_Audit_Check_counts> dbEntity, params KeyValueParams[] paras)
        {
            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return new List<vw_Audit_Check_counts>().AsQueryable();
            }

            return base.GetDataDBObject(dbEntity, paras);
        }

        public ActionResult ExportAudit_Check_counts(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G統計報表專區_已查核家數統計表);
            string fileTitle = "查核輔導專區_已查核家數統計表";
            
            //解決資料查詢錯誤，但查詢數量為全部(非分頁數量)
            //不使用dou filter過濾資料(iquery)            
            //var result = iquery.AsEnumerable();
            List<string> titles = new List<string>() { "查核輔導專區_已查核家數統計表，查詢條件:" };
            List<dynamic> result = GetOutputData(ref titles, paras);

            //產出Dynamic資料 (給Excel)
            var list = result;

            //查無符合資料表數
            if (list.Count == 0)
            {
                return Json(new { result = false, errorMessage = "查無符合資料表數" }, JsonRequestBehavior.AllowGet);
            }

            Dictionary<string, int> dicsHeaderMerge = new Dictionary<string, int>();
            foreach (var v in list[0])
            {
                string key = v.Key.ToString();
                string value = v.Value.ToString();

                if (key == "縣市別")
                {
                }
                else
                {
                    string str = key.Split('_')[1];  //標頭欄位切字串('_')取位置1
                    if (!dicsHeaderMerge.ContainsKey(str))
                    {
                        dicsHeaderMerge.Add(str, 2);
                    }
                }
            }

            foreach (var row in list)
            {
                var f = row;
                f.SheetName = fileTitle;//sheep.名稱;
            }
            
            //產出excel            
            string fileName = OilGas.ExcelSpecHelper.GenerateExcelByLinqF2_1(fileTitle, titles, dicsHeaderMerge, list, folder, "Y");
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

        private List<object> GetOutputData(ref List<string> titles, params KeyValueParams[] paras)
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();

            Dou.Models.DB.IModelEntity<CityCode> cityCode = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
            Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganizationV> carVehicleGas_BusinessOrganizationV = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganizationV>(dbContext);

            Dou.Models.DB.IModelEntity<Check_Basic> check_Basic = new Dou.Models.DB.ModelEntity<Check_Basic>(dbContext);
            Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> carVehicleGas_BusinessOrganization = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);

            Dou.Models.DB.IModelEntity<gas_total_tempV> gas_total_tempV = new Dou.Models.DB.ModelEntity<gas_total_tempV>(dbContext);

            //條件
            var CaseType = KeyValue.GetFilterParaValue(paras, "CaseType");
            var CheckYear = KeyValue.GetFilterParaValue(paras, "CheckYear");

            string rCaseType = "";
            int rCheckYear = 0;
            if (!string.IsNullOrEmpty(CaseType))
            {
                string str = CaseType;
                rCaseType = str;                
                
                titles.Add("石油設施類型:" + Code.GetCaseType().Where(a => a.Key == CaseType).FirstOrDefault().Value);
            }

            if (!string.IsNullOrEmpty(CheckYear))
            {
                int num = int.Parse(CheckYear);
                rCheckYear = num;
                titles.Add("年度查詢:" + num.ToString());
            }

            //統計
            //母表 Cross Join            
            var datas = cityCode.GetAll().AsEnumerable().SelectMany(a => carVehicleGas_BusinessOrganizationV.GetAll()
                .Select(b => new
                {
                    a.CityName,
                    a.CityCode1,
                    Business_themeShort = b.ShortName,
                    Business_theme = b.Value,
                    Business_sort = b.ShortName == "其他" ? "1000" : b.Value,//其他=>非集團，且放最後面(其他16, 車容18, 不可Business_theme排序)
                    b.CaseType
                })).Where(o => o.CaseType == rCaseType).ToList();

            //行列互換(實體化)
            var pivot = datas.OrderBy(a => string.IsNullOrEmpty(a.Business_sort) ? 0: int.Parse(a.Business_sort))
                            .ToPivotArray(
                           item => item.Business_theme, 
                           item => item.CityCode1,
                           v => v.Any() ? 0 : 0);


            var a1 = check_Basic.GetAll().GroupJoin(carVehicleGas_BusinessOrganization.GetAll(), a => a.Business_theme, b => b.Value, (o, c) => new
            {
                CityCode1 = o.CheckNo.Substring(0, 1).Replace("L", "B").Replace("S", "E").Replace("R", "D"),
                o.Business_theme,
                o.CaseType, o.CheckDate
            })
            .Where(o => o.CaseType == rCaseType)
            .Where(o => o.CheckDate != null && ((DateTime)o.CheckDate).Year - 1911 == rCheckYear)
            .GroupBy(o => new { o.CityCode1, o.Business_theme })
            .Select(o => new
            {
                o.Key.CityCode1, o.Key.Business_theme, amount = o.Count()
            }).ToList();

            var a2 = gas_total_tempV.GetAll().Select(o => new
            {
                CityCode1 = o.area.Replace("L", "B").Replace("S", "E").Replace("R", "D"),
                o.CaseType, o.value, o.Total
            })
            .Where(o => o.CaseType == rCaseType)
            .ToList();

            //結果
            List<dynamic> reslt = new List<dynamic>();
            foreach (var row in pivot
                .OrderBy(row => datas.Where(a => a.CityCode1 == row.CityCode1).First().CityCode1)
                )
            {
                string CityCode1 = "";
                string Business_theme = "";

                dynamic f = new ExpandoObject();
                int sum_amount = 0;
                int sum_total = 0;
                foreach (var v in row)
                {
                    string key = v.Key.ToString();
                    string value = v.Value.ToString();

                    if (key == "CityCode1")
                    {
                        CityCode1 = value;
                        string CityName = datas.Where(a => a.CityCode1 == CityCode1).FirstOrDefault().CityName;
                        
                        f.縣市別 = CityName;
                    }
                    else
                    {
                        Business_theme = key;
                        string Business_themeShort = datas.Where(a => a.Business_theme == key).FirstOrDefault().Business_themeShort;
                        Business_themeShort = Business_themeShort == "其他" ? "非集團" : Business_themeShort;
                        
                        //總計
                        var obj2 = a2.Where(a => a.CityCode1 == CityCode1 && a.value == Business_theme);
                        int total = obj2.FirstOrDefault() == null ? 0 : obj2.FirstOrDefault().Total;
                        sum_total += total;                        
                        ((IDictionary<string, object>)f).Add(new KeyValuePair<string, object>("總計_" + Business_themeShort, total));

                        //已查
                        var obj1 = a1.Where(a => a.CityCode1 == CityCode1 && a.Business_theme == Business_theme);
                        int amount = obj1.FirstOrDefault() == null ? 0 : obj1.FirstOrDefault().amount;
                        sum_amount += amount;                        
                        ((IDictionary<string, object>)f).Add(new KeyValuePair<string, object>("已查_" + Business_themeShort, amount));
                    }
                }
                
                ((IDictionary<string, object>)f).Add(new KeyValuePair<string, object>("總計_" + "合計", sum_total));
                ((IDictionary<string, object>)f).Add(new KeyValuePair<string, object>("已查_" + "合計", sum_amount));

                reslt.Add(f);
            }            

            return reslt;
        }        

    }

    public class vw_Audit_Check_counts
    {        
        [Display(Name = "石油設施類型", Order = 1)]
        [ColumnDef(Visible = false, Filter = true, EditType = EditType.Select,
            SelectItemsClassNamespace = ReportCaseTypeSelectItemsClassImp.AssemblyQualifiedName)]
        public string CaseType { get; }

        [Display(Name = "年度查詢", Order = 2)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
            Filter = true, SelectItemsClassNamespace = OilGas.CheckYearSelectItems.AssemblyQualifiedName)]
        public int CheckYear { get; }
    }
}