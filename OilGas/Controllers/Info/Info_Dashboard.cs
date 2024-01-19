using Dou.Controllers;
using Dou.Misc;
using Dou.Misc.Attr;
using Dou.Models.DB;
using NPOI.SS.Formula.Functions;
using OilGas.Controllers.Audit;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;


namespace OilGas.Controllers.Info
{
    [Dou.Misc.Attr.MenuDef(Id = "Info_Dashboard", Name = "儀錶板", MenuPath = "資訊查詢/I儀錶板", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Info_DashboardController : AGenericModelController<Info_Dashboard>
    {
        static List<Info_Dashboard> _lsID = new List<Info_Dashboard>();
        static string CityCode1 = "";
        static string _GSLCode = "";
        static string CaseType = "";
        static string CheckYear = "";
        static int lastSumAmount = 0;

        // GET: Info_Dashboard
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<Info_Dashboard> GetModelEntity()
        {
            return new ModelEntity<Info_Dashboard>(new OilGasModelContextExt());
        }

        protected override IEnumerable<Info_Dashboard> GetDataDBObject(IModelEntity<Info_Dashboard> dbEntity, params KeyValueParams[] paras)
        {
            //條件
            CityCode1 = KeyValue.GetFilterParaValue(paras, "CityCode1");
            CaseType = KeyValue.GetFilterParaValue(paras, "CaseType");
            CheckYear = KeyValue.GetFilterParaValue(paras, "CheckYear");
            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return new List<Info_Dashboard>().AsQueryable();
            }
            _GSLCode = string.IsNullOrEmpty(CityCode1) ? "" : Rpt_CarFuel_Land.GetGSLCodeByCityCode(CityCode1).First().GSLCode.ToString();
            List<string> titles = new List<string>() { "查核輔導專區_已查核家數統計表，查詢條件:" };
            List<dynamic> result = GetOutputData(ref titles, paras);
            _lsID = new List<Info_Dashboard>();
            _lsID.Add(new Info_Dashboard { Sum_Total = lastSumAmount });
            return _lsID;
        }

        /// <summary>
        /// 取得所有站數
        /// </summary>
        /// <returns></returns>
        public ActionResult GetChartData_CarFuel()
        {
            var lstquery = _lstYearlyData_CF();
            return Json(lstquery, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetChartData_CarGas()
        {
            var lstquery = _lstYearlyData_CS();
            return Json(lstquery, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetChartData_FishGas()
        {
            var lstquery = _lstYearlyData_FG();
            return Json(lstquery, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 條列出所有年份所有站數資料
        /// </summary>
        /// <returns></returns>
        private List<YearlyData> _lstYearlyData_CF()
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
            Dou.Models.DB.IModelEntity<CarFuel_BasicData> CarFuel_BasicData = new Dou.Models.DB.ModelEntity<CarFuel_BasicData>(dbContext);
            var bdata = CarFuel_BasicData.GetAll().ToArray();
            //查驗權限
            //if (!Dou.Context.CurrentIsAdminUser && !basic.Permissions("admin"))
            //{
            //    //權限查詢
            //    var pCitys = Dou.Context.CurrentUser<User>().PowerCitysGSLs();
            //    var q1 = _lsCFCCE1.Where(x => !string.IsNullOrEmpty(x.CaseNo));
            //    _lsCFCCE1 = q1.Where(x => pCitys.Contains(x.CaseNo.Substring(4, 2))).OrderBy(x => x.CheckNo).ToList();
            //}

            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysGSLs();
            bdata = bdata.Where(x => pCitys.Contains(x.CITY)).ToArray();
            Dou.Models.DB.IModelEntity<UsageStateCode> usageStateCode = new Dou.Models.DB.ModelEntity<UsageStateCode>(dbContext);
            var uscode = usageStateCode.GetAll().OrderBy(a => a.Rank).ToArray();

            var q1 = (from cb in bdata
                      join p in uscode on cb.UsageState equals p.Value
                      into groupjoin
                      from a in groupjoin.DefaultIfEmpty()
                      select a).Where(x => x != null).ToArray();
            var query = (from a in q1
                         where a.Type == "已開業"
                         select new
                         {
                             Year = DateTime.Now.Year.ToString(),
                         }).GroupBy(n => n.Year)
                         .Select(n => new
                         {
                             year = n.Key,
                             counts = n.Count()
                         })
                         .OrderBy(n => n.year).ToArray();

            //var query = (from cb in bdata
            //             join p in uscode on cb.UsageState equals p.Value
            //             into groupjoin
            //             from a in groupjoin.DefaultIfEmpty()
            //             where a.Type == "已開業"
            //             select new
            //             {
            //                 Year = DateTime.Now.Year.ToString(),
            //             }).GroupBy(n => n.Year)
            //             .Select(n => new
            //             {
            //                 year = n.Key,
            //                 counts = n.Count()
            //             })
            //             .OrderBy(n => n.year).ToArray();

            List<YearlyData> lstquery = new List<YearlyData>();

            lstquery.Add(new YearlyData { year = "1998", counts = 1723 });
            lstquery.Add(new YearlyData { year = "1999", counts = 1884 });
            lstquery.Add(new YearlyData { year = "2000", counts = 2020 });
            lstquery.Add(new YearlyData { year = "2001", counts = 2149 });
            lstquery.Add(new YearlyData { year = "2002", counts = 2268 });
            lstquery.Add(new YearlyData { year = "2003", counts = 2369 });
            lstquery.Add(new YearlyData { year = "2004", counts = 2471 });
            lstquery.Add(new YearlyData { year = "2005", counts = 2533 });
            lstquery.Add(new YearlyData { year = "2006", counts = 2592 });
            lstquery.Add(new YearlyData { year = "2007", counts = 2635 });
            lstquery.Add(new YearlyData { year = "2008", counts = 2667 });
            lstquery.Add(new YearlyData { year = "2009", counts = 2698 });
            lstquery.Add(new YearlyData { year = "2010", counts = 2696 });
            lstquery.Add(new YearlyData { year = "2011", counts = 2698 });
            lstquery.Add(new YearlyData { year = "2012", counts = 2668 });
            lstquery.Add(new YearlyData { year = "2013", counts = 2621 });
            lstquery.Add(new YearlyData { year = "2014", counts = 2619 });
            lstquery.Add(new YearlyData { year = query[0].year.ToString(), counts = query[0].counts });

            return lstquery;
        }

        /// <summary>
        /// 條列出所有年份所有站數資料
        /// </summary>
        /// <returns></returns>
        private List<YearlyData> _lstYearlyData_CS()
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
            Dou.Models.DB.IModelEntity<CarGas_BasicData> CarGas_BasicData = new Dou.Models.DB.ModelEntity<CarGas_BasicData>(dbContext);
            var bdata = CarGas_BasicData.GetAll().Where(x => x.UsageState != "-99" & x.Report_date != null).ToArray();
            Dou.Models.DB.IModelEntity<UsageStateCode> usageStateCode = new Dou.Models.DB.ModelEntity<UsageStateCode>(dbContext);
            var uscode = usageStateCode.GetAll().OrderBy(a => a.Rank).ToArray();

            var query = (from cb in bdata
                         join p in uscode on cb.UsageState equals p.Value
                         into groupjoin
                         from a in groupjoin.DefaultIfEmpty()
                         where cb.Report_date.Value.ToString("yyyy") != "1900" && (a.Type != null && a.Type == "已開業")
                         select new
                         {
                             Year = cb.Report_date.Value.ToString("yyyy"),
                         }).GroupBy(n => n.Year)
                         .Select(n => new
                         {
                             year = n.Key,
                             counts = n.Count()
                         })
                         .OrderBy(n => n.year).ToArray();

            List<YearlyData> lstquery = new List<YearlyData>();

            if (query.Count() > 0)
            {
                for (int i = 0; i < query.Count(); i++)
                {
                    lstquery.Add(new YearlyData { year = query[i].year.ToString(), counts = query[i].counts });
                }
            }

            return lstquery;
        }

        /// <summary>
        /// 條列出所有年份所有站數資料
        /// </summary>
        /// <returns></returns>
        private List<YearlyData> _lstYearlyData_FG()
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
            Dou.Models.DB.IModelEntity<FishGas_BasicData> FishGas_BasicData = new Dou.Models.DB.ModelEntity<FishGas_BasicData>(dbContext);
            var bdata = FishGas_BasicData.GetAll().Where(x => x.UsageState != "-99" & x.Report_date != null).ToArray();
            Dou.Models.DB.IModelEntity<UsageStateCode> usageStateCode = new Dou.Models.DB.ModelEntity<UsageStateCode>(dbContext);
            var uscode = usageStateCode.GetAll().OrderBy(a => a.Rank).ToArray();

            var query = (from cb in bdata
                         join p in uscode on cb.UsageState equals p.Value
                         into groupjoin
                         from a in groupjoin.DefaultIfEmpty()
                         where cb.Report_date.Value.ToString("yyyy") != "1900" && (a.Type != null && a.Type == "已開業")
                         select new
                         {
                             Year = cb.Report_date.Value.ToString("yyyy"),
                         }).GroupBy(n => n.Year)
                         .Select(n => new
                         {
                             year = n.Key,
                             counts = n.Count()
                         })
                         .OrderBy(n => n.year).ToArray();

            List<YearlyData> lstquery = new List<YearlyData>();

            if (query.Count() > 0)
            {
                for (int i = 0; i < query.Count(); i++)
                {
                    lstquery.Add(new YearlyData { year = query[i].year.ToString(), counts = query[i].counts });
                }
            }
            return lstquery;
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
            //var CaseType = KeyValue.GetFilterParaValue(paras, "CaseType");
            //var CheckYear = KeyValue.GetFilterParaValue(paras, "CheckYear");

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
            var pivot = datas.OrderBy(a => string.IsNullOrEmpty(a.Business_sort) ? 0 : int.Parse(a.Business_sort))
                            .ToPivotArray(
                           item => item.Business_theme,
                           item => item.CityCode1,
                           v => v.Any() ? 0 : 0);


            var a1 = check_Basic.GetAll().GroupJoin(carVehicleGas_BusinessOrganization.GetAll(), a => a.Business_theme, b => b.Value, (o, c) => new
            {
                CityCode1 = o.CheckNo.Substring(0, 1).Replace("L", "B").Replace("S", "E").Replace("R", "D"),
                o.Business_theme,
                o.CaseType,
                o.CheckDate
            })
            .Where(o => o.CaseType == rCaseType)
            .Where(o => o.CheckDate != null && ((DateTime)o.CheckDate).Year - 1911 == rCheckYear)
            .GroupBy(o => new { o.CityCode1, o.Business_theme })
            .Select(o => new
            {
                o.Key.CityCode1,
                o.Key.Business_theme,
                amount = o.Count()
            }).ToList();

            var a2 = gas_total_tempV.GetAll().Select(o => new
            {
                CityCode1 = o.area.Replace("L", "B").Replace("S", "E").Replace("R", "D"),
                o.CaseType,
                o.value,
                o.Total
            })
            .Where(o => o.CaseType == rCaseType)
            .ToList();
            lastSumAmount = 0;
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
                lastSumAmount = lastSumAmount + sum_amount;
                reslt.Add(f);
            }
            var ss = lastSumAmount;
            return reslt;
        }

    }
}