using Dou.Controllers;
using Dou.Misc;
using Dou.Models.DB;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarGas
{
    [Dou.Misc.Attr.MenuDef(Id = "CarGas_StopBusiness", Name = "各縣市新設、停業、復業與歇業之加氣站家數統計報表", MenuPath = "汽車加氣站/B統計報表專區", Action = "Index", Index = 9, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class CarGas_StopBusinessController : AGenericModelController<CarGas_StopBusiness>
    {
        public static List<CarGas_StopBusiness> _vwCGSB = new List<CarGas_StopBusiness>();
        static string _Year_Start = "";
        static DataTable _DataTable = new DataTable();

        // GET: CarGas_StopBusiness
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 取得所有站數
        /// </summary>
        /// <returns></returns>
        public ActionResult GetChartData()
        {
            //進入頁面不顯示清單(未使用查詢)
            if (string.IsNullOrEmpty(_Year_Start))
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            var lstquery = _vwCGSB;
            return Json(lstquery, JsonRequestBehavior.AllowGet);
        }
        public override DataManagerOptions GetDataManagerOptions()
        {
            var opts = base.GetDataManagerOptions();
            opts.editable = false;
            opts.deleteable = false;
            opts.addable = false;
            return opts;
        }
        protected override IEnumerable<CarGas_StopBusiness> GetDataDBObject(IModelEntity<CarGas_StopBusiness> dbEntity, params KeyValueParams[] paras)
        {
            _Year_Start = HelperUtilities.GetFilterParaValue(paras, "Year_Start");

            //進入頁面不顯示清單(未使用查詢)
            if (string.IsNullOrEmpty(_Year_Start))
            {
                return new List<CarGas_StopBusiness>().AsQueryable();
            }

            var tmpdt = getData(_Year_Start);

            List<CarGas_StopBusiness> CarGas_StopBusiness = new List<CarGas_StopBusiness>();
            CarGas_StopBusiness = (from DataRow dr in tmpdt.Rows
                                    select new CarGas_StopBusiness()
                                    {
                                        workCity = dr["workCity"].ToString(),
                                        CityCode = dr["CityCode"].ToString(),
                                        Rank = int.Parse(dr["Rank"].ToString()),
                                        GSLCode = dr["GSLCode"].ToString(),
                                        AddBusiness = int.Parse(dr["AddBusiness"].ToString()),
                                        StopBusiness = int.Parse(dr["StopBusiness"].ToString()),
                                        ReBusiness = int.Parse(dr["ReBusiness"].ToString()),
                                        EndBusiness = int.Parse(dr["EndBusiness"].ToString()),
                                        Business = int.Parse(dr["Business"].ToString()),
                                    }).ToList();
            //權限查詢
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysCodes();
            _vwCGSB = CarGas_StopBusiness.Where(x => pCitys.Contains(x.CityCode)).ToList();
            return _vwCGSB;
        }
        protected override Dou.Models.DB.IModelEntity<OilGas.Models.CarGas_StopBusiness> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<OilGas.Models.CarGas_StopBusiness>(new OilGasModelContextExt());
        }

        #region 報表
        /// <summary>
        /// 取得報表資料
        /// </summary>
        /// <param name="Year_Start"></param>
        /// <returns></returns>
        private DataTable getData(string Year_Start)
        {
            string strSQL = "";
            int workYear1 = Convert.ToInt32(Year_Start) + 1911;
            int workYear2 = Convert.ToInt32(Year_Start) + 1911 + 1;
            strSQL = string.Format(@"
                Select CityName as workCity
        	   ,[CityCode] 
        	   ,[Rank]
               ,GSLCode
               ,(Select count(distinct CaseNo) From CarGas_Dispatch p Left Join CityCode cc with(nolock) On GSLCode LIKE '%' + SUBSTRING(p.CaseNo, 5, 2) + '%' where DispatchClass in ('17') and cc.CityCode=a.CityCode and Year(Dispatch_date)={0} ) as AddBusiness
        	   ,(Select count(distinct CaseNo) From CarGas_Dispatch p Left Join CityCode cc with(nolock) On GSLCode LIKE '%' + SUBSTRING(p.CaseNo, 5, 2) + '%' where DispatchClass in ('44','46') and cc.CityCode=a.CityCode and Year(Dispatch_date)={0} ) as StopBusiness
        	   ,(Select count(distinct CaseNo) From CarGas_Dispatch p Left Join CityCode cc with(nolock) On GSLCode LIKE '%' + SUBSTRING(p.CaseNo, 5, 2) + '%' where DispatchClass in ('54') and cc.CityCode=a.CityCode and Year(Dispatch_date)={0} ) as ReBusiness
        	   ,(Select count(distinct CaseNo) From CarGas_Dispatch p Left Join CityCode cc with(nolock) On GSLCode LIKE '%' + SUBSTRING(p.CaseNo, 5, 2) + '%' where DispatchClass in ('18','60','61','62','66','68') and cc.CityCode=a.CityCode and Year(Dispatch_date)={0} 
                 and CaseNo not in (select CaseNo From CarGas_Dispatch p Left Join CityCode cc with(nolock) On GSLCode LIKE '%' + SUBSTRING(p.CaseNo, 5, 2) + '%' where DispatchClass in ('18','60','61','62','66','68') and cc.CityCode=a.CityCode and Year(Dispatch_date) < {0}) ) as EndBusiness
        	   ,0 as Business
                From CityCode a
                order by Rank
                ", workYear1, workYear2);
            DataTable dt = StatisticReportFunc.getDataTable(strSQL);
            return dt;
        }

        /// <summary>
        /// 計算最後一列加總資料
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSumData()
        {
            #region 底部總計
            int sumAddBusiness = 0;
            int sumStopBusiness = 0;
            int sumReBusiness = 0;
            int sumEndBusiness = 0;

            if (string.IsNullOrEmpty(_Year_Start))
                return Json(new List<string> { "", "", "", "" });

            foreach (var item in _vwCGSB)
            {
                sumAddBusiness = sumAddBusiness + item.AddBusiness;
                sumStopBusiness = sumStopBusiness + item.StopBusiness;
                sumReBusiness = sumReBusiness + item.ReBusiness;
                sumEndBusiness = sumEndBusiness + item.EndBusiness;
            }
            # endregion 底部總計
            //沒有資料就不顯示統計資料
            return Json(new List<string> { sumAddBusiness.ToString(), sumStopBusiness.ToString(), sumReBusiness.ToString(),
                sumEndBusiness.ToString()});
        }

        /// <summary>
        /// 匯出EXCEL
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public ActionResult ExportCarGas_StopBusiness(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.汽車加氣站_B統計報表專區_各縣市停歇業與新設加氣站家數統計報表);
            string fileTitle = "汽車加氣站_各縣市停歇業與新設加氣站家數統計報表";

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
            QryString = string.Format("{0} 年", _Year_Start);
            DataTable dt = StatisticReportFunc.ConvertToDataTable(_vwCGSB);

            dt.Columns.Remove("Year_Start");
            dt.Columns.Remove("CityCode");
            dt.Columns.Remove("Rank");
            dt.Columns.Remove("GSLCode");
            dt.Columns.Remove("Business");

            string Title = string.Format(@"<tr>" +
                                       "  <td>縣市別</td>" +
                                       "  <td>新設</td>" +
                                       "  <td>暫停營業</td>" +
                                       "  <td>恢復營業</td>" +
                                       "  <td>歇業</td>" +
                                       "</tr>");

            ReportName = "各縣市停、歇業與新設加氣站家數統計報表";

            #region 處理加總
            int[] array = new int[4];
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    array[0] += Convert.ToInt32(dt.Rows[i]["AddBusiness"].ToString().Trim());
                    array[1] += Convert.ToInt32(dt.Rows[i]["StopBusiness"].ToString().Trim());
                    array[2] += Convert.ToInt32(dt.Rows[i]["ReBusiness"].ToString().Trim());
                    array[3] += Convert.ToInt32(dt.Rows[i]["EndBusiness"].ToString().Trim());
                }
            }
            for (int i = 0; i < array.Length; i++)
            {
                strTotal += "<td>" + array[i] + "</td>";
            }
            Total = "<tr><td>合計</td>" + strTotal + "</tr>";
            #endregion

            //欄位
            string[] arrField = new string[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                arrField[i] = dt.Columns[i].ColumnName;
            }

            return StatisticReportFunc.DownLoad_Excel(ref dt, ReportName, QryString, arrField, Title, Total, ReportName + DateTime.Now.ToString("yyyyMMddhhmmss"));

        }
        #endregion

        #region 明細
        /// <summary>
        /// 取得明細明細資料
        /// </summary>
        /// <param name="workCity"></param>
        /// <param name="workYear"></param>
        /// <param name="workType"></param>
        /// <returns></returns>
        private DataTable getDetailData(string workCity, string workYear, string workType)
        {
            string sqlWhere = "";
            string strWhr1 = "";
            string strSQL = "";
            Dictionary<string, string> dicCol = new Dictionary<string, string>();

            #region<決定撈取的欄位>
            dicCol.Add("CaseNo", "加氣站編號");
            dicCol.Add("Gas_Name", "加氣站名稱");
            dicCol.Add("Business_theme", "營業主體");
            dicCol.Add("Dispatch_date", "發文日期");
            dicCol.Add("Dispatch_No", "發文文號");
            dicCol.Add("DispatchClass", "發文類別");
            dicCol.Add("DispatchName", "發文類別");
            dicCol.Add("CityName", "縣市別");
            dicCol.Add("OperationDate", "營運日期");
            dicCol.Add("Report_date", "核發證號日期");
            dicCol.Add("Create_date", "資料建立時間");

            if (workCity != "")
            {
                strWhr1 += string.Format(" and cc.CityCode='{0}' ", workCity);
            }

            if (workType != "Business")
            {
                if (workType == "AddBusiness")
                {
                    //sqlWhere = string.Format(" where DispatchClass in ('17') {0} and Year(Dispatch_date)={1} ", strWhr1, workYear.Value);
                    sqlWhere = string.Format(@" where DispatchClass in ('17') {0} and Year(Dispatch_date)={1} 
                    and p.id not in (select p.id From CarGas_Dispatch where id < p.id and CaseNo =t.CaseNo and DispatchClass in ('17') and Year(Dispatch_date) = {1})
                ", strWhr1, workYear);
                }
                else if (workType == "StopBusiness")
                {
                    //sqlWhere = string.Format(" where DispatchClass in ('44') {0} and Year(Dispatch_date)={1} ", strWhr1, workYear.Value);
                    sqlWhere = string.Format(@" where DispatchClass in ('44','46') {0} and Year(Dispatch_date)={1} 
                    and p.id not in (select p.id From CarGas_Dispatch where id < p.id and CaseNo =t.CaseNo and DispatchClass in ('44','46') and Year(Dispatch_date) = {1})
                ", strWhr1, workYear);
                }
                else if (workType == "ReBusiness")
                {
                    //sqlWhere = string.Format(" where DispatchClass in ('54') {0} and Year(Dispatch_date)={1} ", strWhr1, workYear.Value);
                    sqlWhere = string.Format(@" where DispatchClass in ('54') {0} and Year(Dispatch_date)={1} 
                    and p.id not in (select p.id From CarGas_Dispatch where id < p.id and CaseNo =t.CaseNo and DispatchClass in ('54') and Year(Dispatch_date) = {1})
                ", strWhr1, workYear);
                }
                else if (workType == "EndBusiness")
                {
                    sqlWhere = string.Format(@"
                    where DispatchClass in ('18','60','61','62','66','68') {0} and Year(Dispatch_date)={1} 
                    and t.CaseNo not in (select CaseNo From CarGas_Dispatch where DispatchClass in ('18','60','61','62','66','68') and Year(Dispatch_date) < {1})
                    and p.id not in (select p.id From CarGas_Dispatch where id < p.id and CaseNo =t.CaseNo and DispatchClass in ('18','60','61','62','66','68') and Year(Dispatch_date) = {1})
                ", strWhr1, workYear);
                }
                strSQL += string.Format(@"select 
                         t.CaseNo
	                    ,t.Gas_Name
	                    ,Case when b.Name <> '其他' then b.Name else t.otherBusiness_theme End as Business_theme
	                    ,CONVERT(varchar,p.Dispatch_date,111) as Dispatch_date
	                    ,p.Dispatch_No
	                    ,c.Name as DispatchName
	                    ,cc.CityName
            from CarGas_Dispatch p 
            Left Join DispatchClassCode c with(nolock) On p.DispatchClass = c.Value
	        Left Join CarGas_BasicData t with(nolock) On p.CaseNo = t.CaseNo
            Left Join CarVehicleGas_BusinessOrganization b with(nolock) on b.Value = t.Business_theme
	        Left Join CityCode cc with(nolock) On GSLCode LIKE '%' + SUBSTRING(p.CaseNo, 5, 2) + '%'
            {0} 
            order by t.CaseNo,p.Dispatch_date,p.Dispatch_No", sqlWhere);
            }
            DataTable dt = StatisticReportFunc.getDataTable(strSQL);
            #endregion
            #region<回傳結果>
            return dt;
            #endregion
        }

        /// <summary>
        /// 回傳明細表格資料
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public JsonResult GetDetailTableData(workParas paras)
        {
            var citydata = Rpt_CarFuel_Land.GetAllCityCode();
            var workCity = citydata.Where(x => x.CityName == paras.workCity).FirstOrDefault().CityCode1.ToString();
            _DataTable = new DataTable();
            _DataTable = getDetailData(workCity, paras.workYear, paras.workType);
            var lsCFWBD = StatisticReportFunc.ConvertToList<CarGas_WorkBusinessDesc>(_DataTable);
            return Json(lsCFWBD, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 匯出明細EXCEL
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public ActionResult ExportCarGas_WorkBusinessDesc(workParas paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.汽車加氣站_B統計報表專區_各縣市新設_停業與終止營運自用加儲油站家數統計報表);
            string fileTitle = "汽車加氣站_各縣市新設_停業與終止營運自用加儲油站家數統計報表";
            string titlename = paras.titleName;

            var ltrResults = getDescStrHtml(titlename);

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
        /// 製作HTML格式給匯出明細EXCEL使用
        /// </summary>
        /// <returns></returns>
        protected string getDescStrHtml(string titlename)
        {
            var citydata = Rpt_CarFuel_Land.GetAllCityCode();
            string ReportName, QryString = "", Total = "";
            QryString = string.Format("{0} 年", _Year_Start);

            DataTable dt_detail = _DataTable;

            string Title = string.Format(@"<tr>" +
                                       "  <td>加氣站編號</td>" +
                                       "  <td>加氣站名稱</td>" +
                                       "  <td>營業主體</td>" +
                                       "  <td>發文日期</td>" +
                                       "  <td>發文文號</td>" +
                                       "  <td>發文類別</td>" +
                                       "  <td>縣市別</td>" +
                                       "</tr>");

            ReportName = titlename;

            ////欄位
            string[] arrField = new string[dt_detail.Columns.Count];
            for (int i = 0; i < dt_detail.Columns.Count; i++)
            {
                arrField[i] = dt_detail.Columns[i].ColumnName;
            }

            return StatisticReportFunc.DownLoad_Excel(ref dt_detail, ReportName, QryString, arrField, Title, Total, ReportName + DateTime.Now.ToString("yyyyMMddhhmmss"));
        }
        #endregion
    }
}