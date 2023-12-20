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
    [Dou.Misc.Attr.MenuDef(Id = "Audit_ReportZeroMissing", Name = "歷年度查核零缺失比例統計", MenuPath = "查核輔導專區/G交叉分析報表", Action = "Index", Index = 10, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_ReportZeroMissingController : AGenericModelController<vw_Audit_ReportZeroMissing>
    {
        // GET: Audit_ReportZeroMissing
        public ActionResult Index()
        {
            return View();
        }

        protected override Dou.Models.DB.IModelEntity<vw_Audit_ReportZeroMissing> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<vw_Audit_ReportZeroMissing>(new OilGasModelContextExt());
        }

        protected override IEnumerable<vw_Audit_ReportZeroMissing> GetDataDBObject(IModelEntity<vw_Audit_ReportZeroMissing> dbEntity, params KeyValueParams[] paras)
        {
            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return new List<vw_Audit_ReportZeroMissing>().AsQueryable();
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

                if (ksort.value.ToString() == "CheckYear")
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
                        result = result.OrderBy(a => a.Average);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.Average);
                    }
                }
            }
            else
            {
                //預設排序
                result = result.OrderBy(a => a.CheckYear);
            }

            return result;
        }

        private IEnumerable<vw_Audit_ReportZeroMissing> GetOutputData(ref List<string> titles, params KeyValueParams[] paras)
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
            
            Dou.Models.DB.IModelEntity<Check_Basic_View> check_Basic_View = new Dou.Models.DB.ModelEntity<Check_Basic_View>(dbContext);

            var CaseType = KeyValue.GetFilterParaValue(paras, "CaseType");
            var SType = KeyValue.GetFilterParaValue(paras, "SType");
            var CityCode1 = KeyValue.GetFilterParaValue(paras, "CityCode1");
            var Business_theme = KeyValue.GetFilterParaValue(paras, "Business_theme");

            titles.Add("石油設施類型:" + Code.GetCaseType().Where(a => a.Key == CaseType).FirstOrDefault().Value);
                        
             //統計            
            var iquery = check_Basic_View.GetAll()
                        .Where(a => a.CaseType == CaseType && a.Check_Style != null)
                        .Select(a => new 
                        {                            
                            a.AreaCode, a.CityName, a.Business_theme, a.Business_themeS, a.CaseNo, a.AllDoesmeet,                            
                            CheckYear = a.CheckDate == null ? 0 : ((DateTime)a.CheckDate).Year - 1911                            
                        });

            if (SType == "byCity")
            {
                //權限查詢
                var pCitys = Dou.Context.CurrentUser<User>().PowerCitysCodes();
                iquery = iquery.Where(a => pCitys.Any(b => a.AreaCode == b));

                titles.Add("報表類型:" + "特定縣市");
            }
            else if (SType == "byBusi")
            {
                titles.Add("報表類型:" + "特定集團");
            }

            //代碼-縣市別
            if (!string.IsNullOrEmpty(CityCode1))
            {
                List<string> sels = CityCode1.Split(',').ToList();
                sels = Code.ConvertTwCity(sels);
                iquery = iquery.Where(a => sels.Any(b => a.AreaCode == b));

                //代碼-縣市別
                var citys = CityCode.GetAllDatas().Where(a => sels.Any(b => b == a.CityCode1)).OrderBy(a => a.Rank);
                titles.Add("縣市別:" + string.Join(",", citys.Select(a => a.CityName).ToList()));
            }

            if (!string.IsNullOrEmpty(Business_theme))
            {
                var codes = Business_theme.Split(',').ToList();
                iquery = iquery.Where(a => codes.Any(b => a.Business_theme == b));

                //代碼-營業主體                
                Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> carVehicleGas_BusinessOrganization = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);
                var cars = carVehicleGas_BusinessOrganization.GetAll().Where(a => codes.Any(b => b == a.Value));
                cars = cars.OrderBy(a => a.Rank);
                titles.Add("營業主體:" + string.Join(",", cars.Select(a => a.Name).ToList()));
            }

            var result = iquery
                    .GroupBy(a => new { a.CheckYear })
                    .Select(a => new
                    {                        
                        a.Key.CheckYear,
                        CheckCount = a.Count(),                        
                        CheckNoHiatusCount = a.Sum(p => p.AllDoesmeet == null || p.AllDoesmeet == 0 ? 1 : 0),
                    })
                    .AsEnumerable()
                    .Select(a => new vw_Audit_ReportZeroMissing
                    {                        
                        CheckYear = a.CheckYear,
                        CheckCount = a.CheckCount,                        
                        CheckNoHiatusCount = a.CheckNoHiatusCount,
                        Average = Math.Round((double)a.CheckNoHiatusCount * 100 / a.CheckCount, 2, MidpointRounding.AwayFromZero)
                    });           
            
            return result;
        }
    }

    public class vw_Audit_ReportZeroMissing
    {        
        [Key]
        [Display(Name = "年度")]
        [ColumnDef(Sortable = true)]
        public int CheckYear { get; set; }

        [Display(Name = "檢查家數")]
        [ColumnDef(Sortable = true)]
        public int CheckCount { get; set; }

        [ColumnDef(Sortable = true)]
        [Display(Name = "查核零缺失")]
        public int CheckNoHiatusCount { get; set; }

        [ColumnDef(Sortable = true)]
        [Display(Name = "零缺失比例")]
        public double Average { get; set; }

        [Display(Name = "石油設施類型", Order = 1)]
        [ColumnDef(Visible = false, Filter = true, EditType = EditType.Select,
            SelectGearingWith = "Business_theme,CaseType,true",
            SelectItemsClassNamespace = ReportCaseTypeSelectItemsClassImp.AssemblyQualifiedName)]
        public string CaseType { get; }

        [Display(Name = "報表類型", Order = 2)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
            Filter = true, SelectItems = "{\"byCity\":\"特定縣市\",\"byBusi\":\"特定集團\"}")]
        public string SType { get; set; }

        [Display(Name = "縣市別", Order = 3)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
                Filter = true, SelectItemsClassNamespace = UsercityCodeSelectItems.AssemblyQualifiedName)]
        public string CityCode1 { get; set; }

        [Display(Name = "營業主體", Order = 5)]
        [ColumnDef(Visible = false, Filter = true, EditType = EditType.Select,
           SelectItemsClassNamespace = CarVehicleGas_BusinessOrganizationV2SelectItemsClassImp.AssemblyQualifiedName)]
        [StringLength(70)]
        public string Business_theme { get; set; }
    }
}