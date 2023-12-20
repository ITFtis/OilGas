using Dou.Controllers;
using Dou.Misc.Attr;
using Dou.Models.DB;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_ReportPreviousDistribution", Name = "歷年度查核次數與缺失分布", MenuPath = "查核輔導專區/G交叉分析報表", Action = "Index", Index = 5, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_ReportPreviousDistributionController : AGenericModelController<vw_Audit_ReportPreviousDistribution>
    {
        // GET: Audit_ReportPreviousDistribution
        public ActionResult Index()
        {
            return View();
        }

        protected override Dou.Models.DB.IModelEntity<vw_Audit_ReportPreviousDistribution> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<vw_Audit_ReportPreviousDistribution>(new OilGasModelContextExt());
        }

        protected override IEnumerable<vw_Audit_ReportPreviousDistribution> GetDataDBObject(IModelEntity<vw_Audit_ReportPreviousDistribution> dbEntity, params KeyValueParams[] paras)
        {
            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return new List<vw_Audit_ReportPreviousDistribution>().AsQueryable();
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
                else if (ksort.value.ToString() == "CheckAllCount")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.CheckAllCount);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.CheckAllCount);
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
                else if (ksort.value.ToString() == "CheckAveCount")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.CheckAveCount);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.CheckAveCount);
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
                else if (ksort.value.ToString() == "CheckNoHiatusRate")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.CheckNoHiatusRate);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.CheckNoHiatusRate);
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

        public ActionResult ExportAudit_ReportPreviousDistribution(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G交叉分析報表_歷年度查核次數與缺失分布);
            string fileTitle = "查核輔導專區_歷年度查核次數與缺失分布";

            List<string> titles = new List<string>() { "查核輔導專區_歷年度查核次數與缺失分布，查詢條件:" }; ;
            var result = GetOutputData(ref titles, paras);
            //預設排序            
            result = result.OrderBy(a => a.CheckYear);

            //產出Dynamic資料 (給Excel)
            List<dynamic> list = new List<dynamic>();
            var output = result.ToList();
                        
            foreach (var data in output)
            {
                dynamic f = new ExpandoObject();
                f.年度 = data.CheckYear;                                
                f.查核家數 = data.CheckAllCount;                                
                f.總缺失數 = data.CheckAllDoesmeet;
                f.平均缺失數 = data.CheckAveCount;
                f.零缺失家數 = data.CheckNoHiatusCount;
                f.零缺失比例 = data.CheckNoHiatusRate + "%";

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

        private IEnumerable<vw_Audit_ReportPreviousDistribution> GetOutputData(ref List<string> titles, params KeyValueParams[] paras)
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
            Dou.Models.DB.IModelEntity<Check_Basic_View> check_Basic_View = new Dou.Models.DB.ModelEntity<Check_Basic_View>(dbContext);           
            Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganizationV> carVehicleGas_BusinessOrganizationV = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganizationV>(dbContext);
            var cars = carVehicleGas_BusinessOrganizationV.GetAll();

            var CityCode1 = KeyValue.GetFilterParaValue(paras, "CityCode1");
            var Business_theme = KeyValue.GetFilterParaValue(paras, "Business_theme");

            
            var iquery = check_Basic_View.GetAll().Where(a => a.Check_Style != null)
                            .Where(a => a.CaseType == null || a.CaseType == "CarFuel_BasicData")
                            .Where(a => cars.Any(b => b.Value == a.Business_theme));

            //權限查詢
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysCodes();
            iquery = iquery.Where(a => a.CheckNo != ""
                    && pCitys.Any(b => a.AreaCode == b));

            //條件            
            if (!string.IsNullOrEmpty(CityCode1))
            {
                var codes = CityCode1.Split(',').ToList();
                codes = Code.ConvertTwCity(codes);
                iquery = iquery.Where(a => a.CheckNo != ""
                    && codes.Any(b => a.AreaCode == b));

                //代碼-縣市別                
                Dou.Models.DB.IModelEntity<CityCode> cityCode = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
                var citys = cityCode.GetAll().Where(a => codes.Any(b => b == a.CityCode1)).OrderBy(a => a.Rank);
                titles.Add("縣市:" + string.Join(",", citys.Select(a => a.CityName).ToList()));
            }

            if (!string.IsNullOrEmpty(Business_theme))
            {
                var codes = Business_theme.Split(',').ToList();
                iquery = iquery.Where(a => codes.Any(b => a.Business_theme == b));

                //代碼-營業主體                
                var texts = cars.Where(a => codes.Any(b => b == a.Value)).OrderBy(a => a.Rank);                
                titles.Add("營業主體:" + string.Join(",", texts.Select(a => a.Name).ToList()));
            }

            var result = iquery.Select(a => new
                        {
                            CheckYear = a.CheckDate.Year - 1911,
                            a.AllDoesmeet,
                        })
                      .GroupBy(a => a.CheckYear)
                      .AsEnumerable()
                      .Select(a => new
                      {
                          CheckYear = a.Key,
                          CheckAllCount = a.Count(),
                          CheckAllDoesmeet = a.Sum(p => p.AllDoesmeet == null ? 0 : p.AllDoesmeet),
                          CheckAveCount = Math.Round((double)(a.Sum(p => p.AllDoesmeet == null ? 0 : p.AllDoesmeet)) / a.Count(), 2, MidpointRounding.AwayFromZero),
                          CheckNoHiatusCount = a.Sum(p => p.AllDoesmeet == null || p.AllDoesmeet != 0 ? 0: 1),                          
                      }).Select(a => new vw_Audit_ReportPreviousDistribution
                      {
                          CheckYear = a.CheckYear,
                          CheckAllCount = a.CheckAllCount,
                          CheckAllDoesmeet = a.CheckAllDoesmeet,
                          CheckAveCount = a.CheckAveCount,
                          CheckNoHiatusCount = a.CheckNoHiatusCount,
                          CheckNoHiatusRate = Math.Round((double)a.CheckNoHiatusCount * 100 / a.CheckAllCount, 2)
                      });

            return result;
        }
    }

    public class vw_Audit_ReportPreviousDistribution
    {
        [Display(Name = "年度")]
        [ColumnDef(Sortable = true)]
        public int CheckYear { get; set; }

        [Display(Name = "查核家數")]
        [ColumnDef(Sortable = true)]
        public int CheckAllCount { get; set; }

        [Display(Name = "總缺失數")]
        [ColumnDef(Sortable = true)]
        public int? CheckAllDoesmeet { get; set; }

        [Display(Name = "平均缺失數")]
        [ColumnDef(Sortable = true)]
        public double CheckAveCount { get; set; }

        [Display(Name = "零缺失家數")]
        [ColumnDef(Sortable = true)]
        public int CheckNoHiatusCount { get; set; }

        [Display(Name = "零缺失比例")]
        [ColumnDef(Sortable = true)]
        public double CheckNoHiatusRate { get; set; }

        [Display(Name = "縣市別", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Select,
                Filter = true, SelectItemsClassNamespace = UsercityCodeSelectItems.AssemblyQualifiedName)]
        public string CityCode1 { get; set; }

        [Display(Name = "營業主體", Order = 2)]
        [ColumnDef(Visible = false, Filter = true, EditType = EditType.Select,
            SelectItemsClassNamespace = CarVehicleGas_BusinessOrganizationSelectItemsClassImp.AssemblyQualifiedName)]
        [StringLength(70)]
        public string Business_theme { get; }
    }
}