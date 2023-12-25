using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.FishGas
{
    [Dou.Misc.Attr.MenuDef(Id = "FishGas_Grow", Name = "漁船加油站成長分析報表", MenuPath = "漁船加油站/C統計報表專區", Action = "Index", Index = 6, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class FishGas_GrowController : Controller
    {
        Code.SubSystemType sst = Code.SubSystemType.FishGas;
        // GET: FishGas_Grow
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

            DataTable dtYear = GetYearRangeYYYY(1900, 1);
            ViewBag.txt_mod_date = new SelectList(dtYear.AsDataView(), "Text", "Value");
            ViewBag.txt_mod_dateE = new SelectList(dtYear.AsDataView(), "Text", "Value");
            return View();
        }

        public DataTable GetYearRangeYYYY(int startYear, int plusYear)
        {
            int nowYear = DateTime.Now.Year;
            int futureYear = nowYear + plusYear;

            DataTable dtYear = new DataTable();
            dtYear.Columns.Add("Text");
            dtYear.Columns.Add("Value");

            DataRow drYear = null;

            for (int i = startYear; i <= futureYear; i++)
            {
                drYear = dtYear.NewRow();

                drYear["Text"] = i;
                drYear["Value"] = i;

                dtYear.Rows.Add(drYear);
            }
            return dtYear;
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
            Dou.Models.DB.IModelEntity<FishGas_BasicData> FishGas_BasicData = new Dou.Models.DB.ModelEntity<FishGas_BasicData>(dbContext);
            var bdata = FishGas_BasicData.GetAll().Where(x=>x.UsageState != "-99" & x.Report_date != null).ToArray();
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
                var oDt2 = lstquery.Where(s => s.year == x.ToString()).ToList();
                if (oDt2.Count() > 0)
                {
                    NowCount = oDt2[0].counts;
                    GrowthRate = System.Math.Round(((NowCount - topCount) % NowCount), 2, MidpointRounding.AwayFromZero);
                    topCount = oDt2[0].counts;
                    lstgrowquery.Add(new YearlyGrowData { year = x.ToString(), rate = GrowthRate });
                }
            }
            return Json(lstgrowquery, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 匯出Excel
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public ActionResult ExportFishGas_Grow(ReportData.ViewParams objs)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.漁船加油站_C統計報表專區_加氣站成長分析報表);
            string fileTitle = "漁船加氣站_加氣站成長分析報表";

            #region 取得 查詢年度
            string ddYearS = "";
            string ddYearE = "";

            if (objs.conditions != null)
            {
                ddYearS = objs.conditions.Where(a => a.Id == "txt_mod_date").FirstOrDefault().Value.ToString();
                ddYearE = objs.conditions.Where(a => a.Id == "txt_mod_dateE").FirstOrDefault().Value.ToString();
            }
            var ltrResults = string.Empty;
            int dbYearS, dbYearE;

            StatisticReportFunc.GetReportDateMinYearAndMaxYear(sst, ddYearS, ddYearE, out dbYearS, out dbYearE);

            if (dbYearS == 0)
            {
                return Json(new { result = false, errorMessage = "查無資料" }, JsonRequestBehavior.AllowGet); ;
            }
            #endregion

            string qryCityCode = "";
            if (!Public.Public.listCityCode.Contains(qryCityCode)) qryCityCode = string.Empty;

            StringBuilder sbResultsTblHtml = new StringBuilder();
            StatisticReportFunc srf = new StatisticReportFunc();
            for (int i = dbYearS; i <= dbYearE; i++)
                sbResultsTblHtml.Append(srf.GetResultsTblHtml(sst, i, qryCityCode));

            if (sbResultsTblHtml.Length == 0)
            {
                return Json(new { result = false, errorMessage = "查無資料" }, JsonRequestBehavior.AllowGet);
            }

            ltrResults = string.Format(@"
<html>
<head>
<meta http-equiv=""Content-Type"" content=""text/html;charset=utf-8"" />
<style type=""text/css"">
td {{ mso-number-format:""\@""; }}
.gv_thead {{ background-color:#91b44a; }}
.gv_thead, .trH2 {{ height:30px; }}
table.formx2 th {{ line-height:normal; }}
table.tbl1 {{ margin-bottom:20px; }}
table.tbl1 td {{ line-height:normal; }}
table.tbl1 {{ border-collapse:collapse; width:100%; }}
.trH2 th, .td1 {{ white-space:nowrap; }}
.trR {{ height:26px; }}
</style>
</head>
{0}
</html>"
                , sbResultsTblHtml.ToString()
                    .Replace("<table class=\"formx2 tbl1\">", "<table border=\"1\" class=\"formx2 tbl1\">")
                    .Replace("</table>", "</table><table></table>")
                );

            //5.產出excel
            string fileName = OilGas.ExcelSpecHelper.GenerateExcelGrow(fileTitle, folder, ltrResults, "N");
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

        /// <summary>
        /// 重設報表
        /// </summary>
        /// <returns></returns>
        public ActionResult ResetExport()
        {
            try
            {
                Rpt_FishGas_BasicData_List.ResetGetAllDatas();
                Rpt_FishGas_BasicData_List.GetAllDatas();
            }
            catch (Exception ex)
            {
                string error = ex.Message + ex.StackTrace;
                return Json(new { result = false, errorMessage = error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }
    }
}