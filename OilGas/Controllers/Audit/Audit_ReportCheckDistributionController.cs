using Dou.Controllers;
using Dou.Models.DB;
using DouHelper;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_ReportCheckDistribution", Name = "分級管理趨勢交叉分析報表", MenuPath = "查核輔導專區/G交叉分析報表", Action = "Index", Index = 3, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_ReportCheckDistributionController : AGenericModelController<Audit_ReportCheckDistribution>
    {
        static List<Audit_ReportCheckDistribution> _lsAuditRCD = new List<Audit_ReportCheckDistribution>();
        static string CaseType = "";
        static string Business_theme = "";
        static string CityCode1 = "";

        // GET: Audit_ReportCheckDistribution
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetChartData()
        {
            //進入頁面不顯示清單(未使用查詢)

            if (string.IsNullOrEmpty(Business_theme) && string.IsNullOrEmpty(CityCode1))
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            var lstquery = _lsAuditRCD;
            return Json(lstquery, JsonRequestBehavior.AllowGet);
        }
        protected override Dou.Models.DB.IModelEntity<OilGas.Models.Audit_ReportCheckDistribution> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<OilGas.Models.Audit_ReportCheckDistribution>(new OilGasModelContextExt());
        }
        protected override IEnumerable<Audit_ReportCheckDistribution> GetDataDBObject(IModelEntity<Audit_ReportCheckDistribution> dbEntity, params KeyValueParams[] paras)
        {
            //條件
            CaseType = KeyValue.GetFilterParaValue(paras, "CaseType");
            Business_theme = KeyValue.GetFilterParaValue(paras, "Business_theme");
            CityCode1 = KeyValue.GetFilterParaValue(paras, "CityCode1");
            //進入頁面不顯示清單(未使用查詢)
            if (string.IsNullOrEmpty(Business_theme) && string.IsNullOrEmpty(CityCode1))
            {
                return new List<Audit_ReportCheckDistribution>().AsQueryable();
            }
            Business_theme = Business_theme.Replace("_" + CaseType, "");
            _lsAuditRCD = StatisticReportFunc.ConvertToList<Audit_ReportCheckDistribution>(getListData());

            return _lsAuditRCD;
        }
        /// <summary>
        /// BindData
        /// </summary>
        /// <returns></returns>
        private DataTable getListData()
        {
            string sqlWhere = "";
            string sql = "";
            Dictionary<string, string> dicCol = new Dictionary<string, string>();

            //UserBasicInfo.UserBasicInfo user = new UserBasicInfo.UserBasicInfo();

            #region<Where查詢條件>

            if (!string.IsNullOrEmpty(Business_theme))
            {
                string value = "";
                value += string.Join("','", Business_theme.Split(','));
                sqlWhere += string.Format(" and Business_theme IN ('{0}')", value);
            }


            if (!string.IsNullOrEmpty(CityCode1))
            {
                string value = "";
                value += string.Join("','", CityCode1.Split(','));
                sqlWhere += string.Format(" and AreaCode IN ('{0}')", value);
            }


            //if (Public.Public.listCityCode.Contains(user.UserLv))
            //{
            //    sqlWhere += sqlHelper.AssembleWhrSQLWithAND(user.UserLv, " AreaCode = '{0}' ");
            //}

            //油氣設施類型
            if (CaseType != "")
            {
                sqlWhere += string.Format(" and isnull(CaseType,'CarFuel_BasicData') = '{0}' ", CaseType);
            }

            sqlWhere += " and Check_Style is not null ";

            if (!string.IsNullOrEmpty(sqlWhere))
                sqlWhere = SQLHelper.ToWhr(sqlWhere);
            #endregion

            #region<執行SQL>
            sql = string.Format(@"
            select convert(varchar,year(CheckDate)-1911) as CheckYear,
                   sum(case when AllDoesmeet = 0 then 1 else 0 end) as LevA, 
            	   sum(case when AllDoesmeet >= 1 and AllDoesmeet <= 2 then 1 else 0 end) as LevB, 
            	   sum(case when AllDoesmeet >= 3 and AllDoesmeet <= 5 then 1 else 0 end) as LevC, 
            	   sum(case when AllDoesmeet >= 6 and AllDoesmeet <= 9 then 1 else 0 end) as LevD, 
            	   sum(case when AllDoesmeet >= 10 then 1 else 0 end) as LevE, 
            	   sum(case when AllDoesmeet is null then 1 else 0 end) as LevN,
                   count(*) as LevAll 
            from Check_Basic_View 
            {0}
            group by year(CheckDate)
            order by year(CheckDate)                     
             ", sqlWhere);

            DataTable dt = StatisticReportFunc.getDataTable(sql);
            #endregion
            return dt;
        }
        /// <summary>
        /// 匯出EXCEL
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public ActionResult ExportAudit_ReportCheckDistribution(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G交叉分析報表_分級管理趨勢交叉分析報表);
            string fileTitle = "查核輔導專區_各年度查核結果等級分布統計表";

            var ltrResults = getStrHtml();

            if (ltrResults == "")
            {
                return Json(new { result = false, errorMessage = "查無資料" }, JsonRequestBehavior.AllowGet); ;
            }
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
        /// 製作HTML格式給匯出EXCEL使用
        /// </summary>
        /// <returns></returns>
        protected string getStrHtml()
        {
            var citydata = Rpt_CarFuel_Land.GetAllCityCode();
            string ReportName, QryString = "", Total = "", strTotal = "";
            QryString = "";
            DataTable dt = StatisticReportFunc.ConvertToDataTable(_lsAuditRCD);

            dt.Columns.Remove("CaseType");
            dt.Columns.Remove("Business_theme");
            dt.Columns.Remove("CityCode1");

            string Title = string.Format(@"<tr>" +
                                       "  <td>年度</td>" +
                                       "  <td>等級A</td>" +
                                       "  <td>等級B</td>" +
                                       "  <td>等級C</td>" +
                                       "  <td>等級D</td>" +
                                       "  <td>等級E</td>" +
                                       "  <td>等級N</td>" +
                                       "  <td>合計</td>" +
                                       "</tr>");

            ReportName = "各年度查核結果等級分布統計表";

            //欄位
            string[] arrField = new string[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                arrField[i] = dt.Columns[i].ColumnName;
            }

            return StatisticReportFunc.DownLoad_Excel(ref dt, ReportName, QryString, arrField, Title, Total, ReportName + DateTime.Now.ToString("yyyyMMddhhmmss"));

        }
    }
}