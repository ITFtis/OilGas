using NPOI.SS.Formula.Functions;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "CarFuel_Grow", Name = "加油站成長分析報表", MenuPath = "加油站/A統計報表專區", Action = "Index", Index = 5, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]   
    public class CarFuel_GrowController : Controller
    {
        // GET: CarFuel_Grow
        public ActionResult Index()
        {
            if (!AppConfig.IsDev)
            {
                //非開發階段
                if (Dou.Context.CurrentUserBase == null)
                {
                    return Redirect("~/Home/Index");
                }
            }
            return View();
        }

        /// <summary>
        /// 取得所有站數
        /// </summary>
        /// <returns></returns>
        public ActionResult GetData()
        {
            var lstquery = _lstYearlyData();
            return Json(lstquery, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 條列出所有年份所有站數資料
        /// </summary>
        /// <returns></returns>
        private List<YearlyData> _lstYearlyData()
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
            Dou.Models.DB.IModelEntity<CarFuel_BasicData> CarFuel_BasicData = new Dou.Models.DB.ModelEntity<CarFuel_BasicData>(dbContext);
            var bdata = CarFuel_BasicData.GetAll().ToArray();
            //查驗權限
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysGSLs();
            bdata = bdata.Where(x => pCitys.Contains(x.CITY)).ToArray();
            Dou.Models.DB.IModelEntity<UsageStateCode> usageStateCode = new Dou.Models.DB.ModelEntity<UsageStateCode>(dbContext);
            var uscode = usageStateCode.GetAll().OrderBy(a => a.Rank).ToArray();

            var q1 = (from cb in bdata
                      join p in uscode on cb.UsageState equals p.Value
                      into groupjoin
                      from a in groupjoin.DefaultIfEmpty()
                      select a).Where(x=> x != null).ToArray();
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
        /// 計算成長率
        /// </summary>
        /// <returns></returns>
        public ActionResult GetGrowData()
        {
            Double topCount = 0;
            Double NowCount = 0;
            Double GrowthRate = 0;

            var lstquery = _lstYearlyData();

            List<YearlyGrowData> lstgrowquery = new List<YearlyGrowData>();
            for (int x = Convert.ToInt32(lstquery[0].year); x <= Convert.ToInt32(lstquery[lstquery.Count() - 1].year); x++)
            {
                var oDt2 = lstquery.Where(s=>s.year==x.ToString()).ToList();
                if (oDt2.Count() > 0)
                {
                    NowCount = oDt2[0].counts;

                    if (topCount > 0)
                        GrowthRate = System.Math.Round(((NowCount - topCount) / topCount)*100, 2, MidpointRounding.AwayFromZero);
                    else
                        GrowthRate = 0;
                    topCount = oDt2[0].counts;
                    lstgrowquery.Add(new YearlyGrowData { year = x.ToString(), rate = GrowthRate });
                }
            }         
            return Json(lstgrowquery, JsonRequestBehavior.AllowGet);
        }
    }
}

