using Dou.Controllers;
using Dou.Misc.Attr;
using Dou.Models.DB;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static OilGas.Controllers.Audit.Audit_ReportCheck_counts_CrossAnalysisController;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_ReportCheck_counts_CrossAnalysis", Name = "查核缺失趨勢交叉分析報表", MenuPath = "查核輔導專區/G交叉分析報表", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_ReportCheck_counts_CrossAnalysisController : AGenericModelController<vw_Audit_ReportCheck_counts_CrossAnalysis>
    {
        // GET: Audit_ReportCheck_counts_CrossAnalysis
        public ActionResult Index()
        {
            return View();
        }

        protected override Dou.Models.DB.IModelEntity<vw_Audit_ReportCheck_counts_CrossAnalysis> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<vw_Audit_ReportCheck_counts_CrossAnalysis>(new OilGasModelContextExt());
        }

        protected override IEnumerable<vw_Audit_ReportCheck_counts_CrossAnalysis> GetDataDBObject(IModelEntity<vw_Audit_ReportCheck_counts_CrossAnalysis> dbEntity, params KeyValueParams[] paras)
        {
            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return new List<vw_Audit_ReportCheck_counts_CrossAnalysis>().AsQueryable();
            }

            List<string> titles = new List<string>();
            var result = GetOutputData(ref titles, paras);

            var SType = KeyValue.GetFilterParaValue(paras, "SType");
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
                else if (ksort.value.ToString() == "CityName")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.CityRank);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.CityRank);
                    }
                }
                else if (ksort.value.ToString() == "AvgMiss")
                {
                    if (order == "asc")
                    {
                        result = result.OrderBy(a => a.AvgMiss);
                    }
                    else if (order == "desc")
                    {
                        result = result.OrderByDescending(a => a.AvgMiss);
                    }
                }
            }
            else
            {
                if (SType == "byCity")
                {
                    //預設排序                
                    result = result.OrderBy(a => a.CityRank).ThenBy(a => a.intCheckYear);
                }
                else if (SType == "byBusi")
                {
                    //預設排序                
                    result = result.OrderBy(a => a.BusinessOrgnizationRank).ThenBy(a => a.intCheckYear);
                }
            }            

            return result;
        }

        public ActionResult ExportAudit_ReportCheck_counts_CrossAnalysis(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G交叉分析報表_查核缺失趨勢交叉分析報表);
            string fileTitle = "查核輔導專區_查核缺失趨勢交叉分析報表";

            //Filter = true, SelectItems = "{\"byCity\":\"縣市查\",\"byBusi\":\"營業主體查\"}")]
            var SType = KeyValue.GetFilterParaValue(paras, "SType");
            string name = "";
            if (SType == "byCity")
            {
                name = "「縣市查」";
            }
            else if (SType == "byBusi")
            {
                name = "「營業主體查」";
            }

            List<string> titles = new List<string>() { string.Format("查核輔導專區_查核缺失趨勢交叉分析報表{0}，查詢條件:", name) };
            var result = GetOutputData(ref titles, paras);

            if (SType == "byCity")
            {
                //預設排序                
                result = result.OrderBy(a => a.CityRank).ThenBy(a => a.intCheckYear);
            }
            else if (SType == "byBusi")
            {
                //預設排序                
                result = result.OrderBy(a => a.BusinessOrgnizationRank).ThenBy(a => a.intCheckYear);
            }            

            //產出Dynamic資料 (給Excel)
            List<dynamic> list = new List<dynamic>();
            var output = result.ToList();

            foreach (var data in output)
            {
                dynamic f = new ExpandoObject();
                f.年份 = data.intCheckYear;
                //f.縣市別 = data.CityName;
                if (SType == "byCity")
                {
                    f.縣市別 = data.MapName;
                }
                else if (SType == "byBusi")
                {
                    f.營業主體 = data.MapName;
                }
                f.平均缺失 = data.AvgMiss;

                f.SheetName = fileTitle;//sheep.名稱;
                list.Add(f);
            }

            //查無符合資料表數
            if (list.Count == 0)
            {
                return Json(new { result = false, errorMessage = "查無符合資料表數" }, JsonRequestBehavior.AllowGet);
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

        private IEnumerable<vw_Audit_ReportCheck_counts_CrossAnalysis> GetOutputData(ref List<string> titles, params KeyValueParams[] paras)
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();

            Dou.Models.DB.IModelEntity<RP_Check_Hiatus_ByBusiOrgAndCity> rP_Check_Hiatus_ByBusiOrgAndCity = new Dou.Models.DB.ModelEntity<RP_Check_Hiatus_ByBusiOrgAndCity>(dbContext);
            Dou.Models.DB.IModelEntity<CityCode> cityCode = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
            Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> carVehicleGas_BusinessOrganization = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);

            var iquery = rP_Check_Hiatus_ByBusiOrgAndCity.GetAll();
            
            //條件
            var SYear = KeyValue.GetFilterParaValue(paras, "SYear");
            var EYear = KeyValue.GetFilterParaValue(paras, "EYear");
            var SType = KeyValue.GetFilterParaValue(paras, "SType");
            var CityCode1 = KeyValue.GetFilterParaValue(paras, "CityCode1");
            var DDLCityCode1 = KeyValue.GetFilterParaValue(paras, "DDLCityCode1");
            var Business_theme = KeyValue.GetFilterParaValue(paras, "Business_theme");
            var DDLBusiness_theme = KeyValue.GetFilterParaValue(paras, "DDLBusiness_theme");

            //條件            
            if (!string.IsNullOrEmpty(DDLCityCode1))
            {
                var codes = DDLCityCode1.Split(',').ToList();
                codes = Code.ConvertTwCity(codes);

                iquery = iquery.Where(a => codes.Any(b => a.CityCode == b));

                //代碼-縣市別                
                var citys = cityCode.GetAll().Where(a => codes.Any(b => b == a.CityCode1));
                citys = citys.OrderBy(a => a.Rank);
                titles.Add("縣市:" + string.Join(",", citys.Select(a => a.CityName).ToList()));
            }

            if (!string.IsNullOrEmpty(DDLBusiness_theme))
            {
                var codes = DDLBusiness_theme.Split(',').ToList();                
                iquery = iquery.Where(a => codes.Any(b => a.BusinessOrgnizationCode == b));

                //代碼-營業主體                
                var cars = carVehicleGas_BusinessOrganization.GetAll().Where(a => codes.Any(b => b == a.Value));
                cars = cars.OrderBy(a => a.Rank);
                titles.Add("營業主體:" + string.Join(",", cars.Select(a => a.Name).ToList()));
            }

            //年度條件(必備)
            int start = 95;
            int end = DateTime.Now.Year - 1911;
            if (!string.IsNullOrEmpty(SYear))
            {
                start = int.Parse(SYear);
            }
            if (!string.IsNullOrEmpty(EYear))
            {
                end = int.Parse(EYear);
            }
            List<string> years = new List<string>();
            for (int i = start; i <= end; i++)
            {
                years.Add(i.ToString());
            }
            iquery = iquery.Where(a => years.Any(b => b == a.CheckYear));
            titles.Add("查詢年:" + string.Join(",", years));

            //條件
            if (!string.IsNullOrEmpty(Business_theme))
            {
                iquery = iquery.Where(a => a.BusinessOrgnizationCode == Business_theme);
                titles.Add("營業主體:" + carVehicleGas_BusinessOrganization.GetAll().Where(a => a.Value == Business_theme).FirstOrDefault().Name);
            }
            if (!string.IsNullOrEmpty(CityCode1))
            {
                iquery = iquery.Where(a => a.CityCode == CityCode1);
                titles.Add("縣市:" + cityCode.GetAll().Where(a => a.CityCode1 == CityCode1).FirstOrDefault().CityName);
            }

            IEnumerable<vw_Audit_ReportCheck_counts_CrossAnalysis> result = null;

            //tmp 統計資料
            IEnumerable<vw_Audit_ReportCheck_counts_CrossAnalysis> tmp = null;
            if (SType == "byCity")
            {
                //(a)縣市統計
                if (!string.IsNullOrEmpty(DDLCityCode1))
                {
                    //條件：多筆縣市
                    var codes = DDLCityCode1.Split(',').ToList();
                    codes = Code.ConvertTwCity(codes);

                    //母體：年度 CrossJoin 縣市
                    var datas = years.CrossJoin(cityCode.GetAll())
                            .Select(a => new
                            {
                                CheckYear = a.Item1,
                                CityCode1 = a.Item2.CityCode1
                            }).Where(a => codes.Any(b => b == a.CityCode1));
                    
                    tmp = datas.GroupJoin(iquery,
                                    a => new { a.CheckYear, a.CityCode1 },
                                    b => new { b.CheckYear, CityCode1 = b.CityCode }, (o, c) => new { o.CheckYear, o.CityCode1, c })
                            .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new
                            {
                                CheckYear = o.CheckYear,
                                CityCode1 = o.CityCode1,
                                CheckAllDoesmeet = c == null ? 0 : c.CheckAllDoesmeet,
                                CheckCount = c == null ? 0 : c.CheckCount
                            }).Distinct()       //Distinct 配合舊系統數據(濾掉 年度縣市查核結果資料重複))
                            .Select(o => new vw_Audit_ReportCheck_counts_CrossAnalysis
                            {
                                CheckYear = o.CheckYear,
                                CityCode1 = o.CityCode1,
                                CheckAllDoesmeet = o.CheckAllDoesmeet,
                                CheckCount = o.CheckCount
                            }).AsEnumerable();

                    //權限查詢
                    var pCitys = Dou.Context.CurrentUser<User>().PowerCitysCodes();
                    tmp = tmp.Where(a => pCitys.Any(b => a.CityCode1 == b));
                }
                else
                {
                    //全國
                    var datas = iquery.Select(a => new vw_Audit_ReportCheck_counts_CrossAnalysis
                            {
                                CheckYear = a.CheckYear,
                                CheckAllDoesmeet = a.CheckAllDoesmeet,
                                CheckCount = a.CheckCount
                            }).Distinct().AsEnumerable();  //Distinct 配合舊系統數

                    tmp = years.GroupJoin(datas, a => a, b => b.CheckYear, (o, c) => new { CheckYear = o, c })
                            .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new vw_Audit_ReportCheck_counts_CrossAnalysis
                            {
                                CheckYear = o.CheckYear,
                                CityCode1 = "全國",
                                CheckAllDoesmeet = c == null ? 0 : c.CheckAllDoesmeet,
                                CheckCount = c == null ? 0 : c.CheckCount
                            });
                }

                //舊系統,縣市查詢五都合併沒加到(為配合數據，故保留一致)
                //結果
                //isNULL(Convert(decimal(5,2),Round(1.0*SUM(CheckAllDoesmeet)/SUM(CheckCount),2) ),0.00) [平均缺失] 
                result = tmp.GroupBy(a => new { a.CheckYear, a.CityCode1 })
                .Select(a => new
                {
                    a.Key.CheckYear,
                    a.Key.CityCode1,
                    CheckAllDoesmeet = a.Sum(p => p.CheckAllDoesmeet),
                    CheckCount = a.Sum(p => p.CheckCount == null ? 0 : (int)p.CheckCount),
                })
                .GroupJoin(cityCode.GetAll(), a => a.CityCode1, b => b.CityCode1, (o, c) => new vw_Audit_ReportCheck_counts_CrossAnalysis
                {
                    CheckYear = o.CheckYear,
                    MapName = c.FirstOrDefault() == null ? o.CityCode1 : c.FirstOrDefault().CityName,
                    CityRank = c.FirstOrDefault() == null ? 100 : c.FirstOrDefault().Rank,
                    AvgMiss = o.CheckCount == 0 ? 0 : Decimal.Round((decimal)o.CheckAllDoesmeet / o.CheckCount, 2)
                })
                .Where(a => a.MapName != "L" && a.MapName != "R" && a.MapName != "S");  //L.R.S 配合舊系統數據(5都條件沒算到)
            }
            else if (SType == "byBusi")
            {
                //(b)營業主體統計
                //母體 tmp(全國、營業主體)
                if (!string.IsNullOrEmpty(DDLBusiness_theme))
                {
                    //條件：多筆營業主體
                    var codes = DDLBusiness_theme.Split(',').ToList();

                    //母體：年度 CrossJoin 營業主體
                    var datas = years.CrossJoin(carVehicleGas_BusinessOrganization.GetAll().OrderBy(a => a.Value))
                            .Select(a => new
                            {
                                CheckYear = a.Item1,
                                Value = a.Item2.Value,
                                Name = a.Item2.Name,
                                BusinessOrgnizationRank = a.Item2.Rank == null ? 0 : a.Item2.Rank
                            }).Where(a => codes.Any(b => b == a.Value));

                    tmp = datas.GroupJoin(iquery,
                                    a => new { a.CheckYear, a.Value },
                                    b => new { b.CheckYear, Value = b.BusinessOrgnizationCode }, (o, c) => new { o.CheckYear, o.BusinessOrgnizationRank, o.Name, c })
                            .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new
                            {
                                CheckYear = o.CheckYear,
                                MapName = o.Name,
                                BusinessOrgnizationRank = o.BusinessOrgnizationRank,
                                CheckAllDoesmeet = c == null ? 0 : c.CheckAllDoesmeet,
                                CheckCount = c == null ? 0 : c.CheckCount
                            }).Distinct()       //Distinct 配合舊系統數據(濾掉 年度縣市查核結果資料重複))
                            .Select(o => new vw_Audit_ReportCheck_counts_CrossAnalysis
                            {
                                CheckYear = o.CheckYear,
                                MapName = o.MapName,
                                BusinessOrgnizationRank = o.BusinessOrgnizationRank,
                                CheckAllDoesmeet = o.CheckAllDoesmeet,
                                CheckCount = o.CheckCount
                            }).AsEnumerable();                    
                }
                else
                {
                    iquery = iquery.Where(a => !string.IsNullOrEmpty(a.BusinessOrgnizationCode));

                    //條件：營業主體集團
                    var codes = Code.GetBusinessOrganizationGroup();

                    //母體：年度 CrossJoin 集團
                    var datas = years.CrossJoin(codes)
                            .Select(a => new
                            {
                                CheckYear = a.Item1,
                                BusinessOrgnizationRank = a.Item2.Key,
                                BusinessOrgnizationRankName = a.Item2.Value,
                            });

                    var z1 = iquery.Where(a => a.BusinessOrgnizationCode != "16").Select(a => new 
                    { 
                        a.CheckYear,
                        BusinessOrgnizationRank = 1,
                        a.CheckAllDoesmeet,
                        a.CheckCount
                    });
                    var z2 = iquery.Where(a => a.BusinessOrgnizationCode == "16").Select(a => new
                    {
                        a.CheckYear,
                        BusinessOrgnizationRank = 2,
                        a.CheckAllDoesmeet,
                        a.CheckCount
                    });
                    var all = z1.Concat(z2);
                    
                    tmp = datas.GroupJoin(all, 
                                a => new { a.CheckYear, a.BusinessOrgnizationRank }, 
                                b => new { b.CheckYear, b.BusinessOrgnizationRank }, (o, c) => new { CheckYear = o.CheckYear, o.BusinessOrgnizationRank, o.BusinessOrgnizationRankName, c })
                            .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new vw_Audit_ReportCheck_counts_CrossAnalysis
                            {
                                CheckYear = o.CheckYear,
                                BusinessOrgnizationRank = o.BusinessOrgnizationRank,
                                MapName = o.BusinessOrgnizationRankName,                                
                                CheckAllDoesmeet = c == null ? 0 : c.CheckAllDoesmeet,
                                CheckCount = c == null ? 0 : c.CheckCount
                            });                    
                }

                //結果
                //isNULL(Convert(decimal(5,2),Round(1.0*SUM(CheckAllDoesmeet)/SUM(CheckCount),2) ),0.00) [平均缺失] 
                result = tmp.GroupBy(a => new { a.CheckYear, a.MapName, a.BusinessOrgnizationRank })
                .Select(a => new
                {
                    a.Key.CheckYear,
                    a.Key.MapName,
                    a.Key.BusinessOrgnizationRank,
                    CheckAllDoesmeet = a.Sum(p => p.CheckAllDoesmeet),
                    CheckCount = a.Sum(p => p.CheckCount == null ? 0 : (int)p.CheckCount),
                })
                .Select(o => new vw_Audit_ReportCheck_counts_CrossAnalysis
                {
                    CheckYear = o.CheckYear,
                    MapName = o.MapName,
                    BusinessOrgnizationRank = o.BusinessOrgnizationRank,
                    AvgMiss = o.CheckCount == 0 ? 0 : Decimal.Round((decimal)o.CheckAllDoesmeet / o.CheckCount, 2)
                });                
            }

            ////var aa = result.ToList();

            return result;
        }

        public class vw_Audit_ReportCheck_counts_CrossAnalysis
        {
            [Display(Name = "年份(起)")]
            [ColumnDef(Sortable = true)]
            public string CheckYear { get; set; }

            [Display(Name = "年份(起)")]
            [ColumnDef(Visible = false)]
            public int intCheckYear { get { return int.Parse(this.CheckYear); } }

            [Display(Name = "縣市別")]
            [ColumnDef(Sortable = true)]
            public string MapName { get; set; }

            [Display(Name = "縣市排序")]
            [ColumnDef(Visible = false)]
            public int? CityRank { get; set; }
            
            [Display(Name = "營業主體排序")]
            [ColumnDef(Visible = false)]
            public int? BusinessOrgnizationRank { get; set; }
            
            [ColumnDef(Visible = false)]
            public int? CheckAllDoesmeet { get; set; }

            [ColumnDef(Visible = false)]
            public int? CheckCount { get; set; }

            [Display(Name = "平均缺失")]
            [ColumnDef(Sortable = true)]
            public decimal AvgMiss { get; set; }

            [Display(Name = "查詢年(起)", Order = 1)]
            [ColumnDef(Visible = false, EditType = EditType.Select,
            Filter = true, SelectItemsClassNamespace = OilGas.CheckYear2SelectItems.AssemblyQualifiedName)]
            public int SYear { get; }

            [Display(Name = "查詢年(迄)", Order = 2)]
            [ColumnDef(Visible = false, EditType = EditType.Select,
           Filter = true, SelectItemsClassNamespace = OilGas.CheckYear2SelectItems.AssemblyQualifiedName)]
            public int EYear { get; }

            [Display(Name = "查詢類別", Order = 3)]
            [ColumnDef(Visible = false, EditType = EditType.Select,
            Filter = true, SelectItems = "{\"byCity\":\"縣市查\",\"byBusi\":\"營業主體查\"}")]
            public string SType { get; set; }

            [Display(Name = "縣市別", Order = 4)]
            [ColumnDef(Visible = false, EditType = EditType.Select,
               Filter = true, SelectItemsClassNamespace = UsercityCodeSelectItems.AssemblyQualifiedName)]
            public string CityCode1 { get; set; }

            [Display(Name = "縣市別", Order = 5)]
            [ColumnDef(Visible = false, EditType = EditType.Select,
               Filter = true, SelectItemsClassNamespace = UsercityCodeSelectItems.AssemblyQualifiedName)]
            public string DDLCityCode1 { get; set; }

            [Display(Name = "營業主體", Order = 6)]
            [ColumnDef(Visible = false, Filter = true, EditType = EditType.Select,
            SelectItemsClassNamespace = CarVehicleGas_BusinessOrganizationSelectItemsClassImp.AssemblyQualifiedName)]
            [StringLength(70)]
            public string Business_theme { get; }

            [Display(Name = "營業主體", Order = 7)]
            [ColumnDef(Visible = false, Filter = true, EditType = EditType.Select,
            SelectItemsClassNamespace = CarVehicleGas_BusinessOrganizationSelectItemsClassImp.AssemblyQualifiedName)]
            [StringLength(70)]
            public string DDLBusiness_theme { get; }
        }
    }
}