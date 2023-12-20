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

namespace OilGas.Controllers.SelfFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "SelfFuel_StopBusiness", Name = "各縣市新設與歇業之自用加儲氣家數統計報表", MenuPath = "自用加儲油/D統計報表專區", Action = "Index", Index = 3, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]    
    public class SelfFuel_StopBusinessController : AGenericModelController<SelfFuel_StopBusiness>
    {
        public static List<SelfFuel_StopBusiness> _vwSFSB = new List<SelfFuel_StopBusiness>();
        static string _Year_Start = "";
        static DataTable _DataTable = new DataTable();
        // GET: SelfFuel_StopBusiness
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
            //進入頁面不顯示明細(未使用查詢)
            if (string.IsNullOrEmpty(_Year_Start))
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            var lstquery = _vwSFSB;
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
        protected override IEnumerable<SelfFuel_StopBusiness> GetDataDBObject(IModelEntity<SelfFuel_StopBusiness> dbEntity, params KeyValueParams[] paras)
        {
            _Year_Start = HelperUtilities.GetFilterParaValue(paras, "Year_Start");

            //進入頁面不顯示明細(未使用查詢)
            if (string.IsNullOrEmpty(_Year_Start))
            {
                return new List<SelfFuel_StopBusiness>().AsQueryable();
            }

            var tmpdt = getData(_Year_Start);

            List<SelfFuel_StopBusiness> SelfFuel_StopBusiness = new List<SelfFuel_StopBusiness>();
            SelfFuel_StopBusiness = (from DataRow dr in tmpdt.Rows
                                    select new SelfFuel_StopBusiness()
                                    {
                                        workCity = dr["workCity"].ToString(),
                                        CityCode = dr["CityCode"].ToString(),
                                        Rank = int.Parse(dr["Rank"].ToString()),
                                        GSLCode = dr["GSLCode"].ToString(),
                                        AddBusiness = int.Parse(dr["AddBusiness"].ToString()),
                                        EndBusiness = int.Parse(dr["EndBusiness"].ToString()),
                                        Business = int.Parse(dr["Business"].ToString()),
                                    }).ToList();

            //權限查詢
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysCodes();
            _vwSFSB = SelfFuel_StopBusiness.Where(x => pCitys.Contains(x.CityCode)).ToList();
            return _vwSFSB;
        }
        protected override Dou.Models.DB.IModelEntity<OilGas.Models.SelfFuel_StopBusiness> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<OilGas.Models.SelfFuel_StopBusiness>(new OilGasModelContextExt());
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
            string sqlWhr3 = " 1=1 ";
            sqlWhr3 += string.Format(@" AND (( UsageState = '0' ");
            sqlWhr3 += string.Format(@" AND UsageState_Second = '0' ");
            sqlWhr3 += string.Format(@" AND UsageState_Third = '0' ");
            sqlWhr3 += string.Format(@" AND UsageState_Fourth = '0' )  ");
            sqlWhr3 += string.Format(@" OR ( UsageState = '1' ");
            sqlWhr3 += string.Format(@" AND UsageState_Second = '1' ");
            sqlWhr3 += string.Format(@" AND UsageState_Third in ('','6') ");
            sqlWhr3 += string.Format(@" AND UsageState_Fourth in ('','5','6','7','8') )) ");
            strSQL = string.Format(@"
                Select CityName as workCity
        	   ,[CityCode] 
        	   ,[Rank]
               ,GSLCode
	           ,(Select count(distinct CaseNo) From SelfFuel_Dispatch p Left Join CityCode cc with(nolock) On GSLCode LIKE '%' + SUBSTRING(p.CaseNo, 5, 2) + '%' where DispatchClass in ('1') and cc.CityCode=a.CityCode and Year(DispatchDate)={0} ) as AddBusiness
        	   ,(Select count(distinct CaseNo) From SelfFuel_Dispatch p Left Join CityCode cc with(nolock) On GSLCode LIKE '%' + SUBSTRING(p.CaseNo, 5, 2) + '%' where DispatchClass in ('7','8','10') and cc.CityCode=a.CityCode and Year(DispatchDate)={0} 
               and CaseNo not in (select CaseNo From SelfFuel_Dispatch p Left Join CityCode cc with(nolock) On GSLCode LIKE '%' + SUBSTRING(p.CaseNo, 5, 2) + '%' where DispatchClass in ('7','8','10') and cc.CityCode=a.CityCode and Year(DispatchDate) < {0}) ) as EndBusiness
	           ,0 as Business
                From CityCode a
                order by Rank
                ", workYear1, workYear2, sqlWhr3);
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
            //int sumStopBusiness = 0;
            //int sumReBusiness = 0;
            int sumEndBusiness = 0;

            if (string.IsNullOrEmpty(_Year_Start))
                return Json(new List<string> { "", "" });

            foreach (var item in _vwSFSB)
            {
                sumAddBusiness = sumAddBusiness + item.AddBusiness;
                //sumStopBusiness = sumStopBusiness + item.StopBusiness;
                //sumReBusiness = sumReBusiness + item.ReBusiness;
                sumEndBusiness = sumEndBusiness + item.EndBusiness;
            }
            # endregion 底部總計
            //沒有資料就不顯示統計資料
            //return Json(new List<string> { sumAddBusiness.ToString(), sumStopBusiness.ToString(), sumReBusiness.ToString(),
            //    sumEndBusiness.ToString()});
            return Json(new List<string> { sumAddBusiness.ToString(), sumEndBusiness.ToString()});
        }

        /// <summary>
        /// 匯出EXCEL
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public ActionResult ExportSelfFuel_StopBusiness(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.自用加儲油站_D統計報表專區_新設與歇業之自用加儲油站家數統計報表);
            string fileTitle = "自用加儲油站_各縣市新設與歇業加油站家數統計報表";

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
            DataTable dt = StatisticReportFunc.ConvertToDataTable(_vwSFSB);

            dt.Columns.Remove("Year_Start");
            dt.Columns.Remove("CityCode");
            dt.Columns.Remove("Rank");
            dt.Columns.Remove("GSLCode");
            dt.Columns.Remove("Business");

            string Title = string.Format(@"<tr>" +
                                       "  <td>縣市別</td>" +
                                       "  <td>新設</td>" +
                                       //"  <td>暫停營業</td>" +
                                       //"  <td>恢復營業</td>" +
                                       "  <td>歇業</td>" +
                                       "</tr>");

            ReportName = "各縣市新設與歇業自用加儲油站家數統計報表";

            #region 處理加總
            int[] array = new int[4];
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    array[0] += Convert.ToInt32(dt.Rows[i]["AddBusiness"].ToString().Trim());
                    //array[1] += Convert.ToInt32(dt.Rows[i]["StopBusiness"].ToString().Trim());
                    //array[2] += Convert.ToInt32(dt.Rows[i]["ReBusiness"].ToString().Trim());
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
            dicCol.Add("CaseNo", "自用加儲油站編號");
            dicCol.Add("Gas_Name", "自用加儲油站名稱");
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
                    sqlWhere = string.Format(" where DispatchClass in ('1') {0} and Year(DispatchDate)={1} ", strWhr1, workYear);
                }
                else if (workType == "EndBusiness")
                {
                    sqlWhere = string.Format(@"
                    where DispatchClass in ('7','8','10') {0} and Year(DispatchDate)={1} 
                    and t.CaseNo not in (select CaseNo From SelfFuel_Dispatch where DispatchClass in ('7','8','10') and Year(DispatchDate) < {1})
                    and p.id not in (select p.id From SelfFuel_Dispatch where id < p.id and CaseNo =t.CaseNo and DispatchClass in ('7','8','10') and Year(DispatchDate) = {1})
                ", strWhr1, workYear);
                }
                strSQL += string.Format(@"select 
                         p.CaseNo
	                    ,t.FuelName as Fuel_Name
	                    ,BusiOrg as Business_theme
	                    ,CONVERT(varchar,p.DispatchDate,111) as Dispatch_date
	                    ,p.DispatchNo as Dispatch_No
	                    ,c.Name as DispatchName
	                    ,cc.CityName
            from SelfFuel_Dispatch p 
            Left Join CarVehicleGas_DispatchClass c with(nolock) On p.DispatchClass = c.Value
	        Left Join SelfFuel_Basic t with(nolock) On p.CaseNo = t.CaseNo
	        Left Join CityCode cc with(nolock) On GSLCode LIKE '%' + SUBSTRING(p.CaseNo, 5, 2) + '%'
            {0} 
            order by t.CaseNo,p.DispatchDate,p.DispatchNo", sqlWhere);
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
            var lsSFWBD = StatisticReportFunc.ConvertToList<SelfFuel_WorkBusinessDesc>(_DataTable);
            return Json(lsSFWBD, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 匯出明細EXCEL
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public ActionResult ExportSelfFuel_workBusinessDesc(workParas paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.自用加儲油站_D統計報表專區_新設與歇業之自用加儲油站家數統計報表);
            string fileTitle = "自用加儲油站_新設與歇業之自用加儲油站家數統計報表";
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
                                       "  <td>自用加儲油站編號</td>" +
                                       "  <td>自用加儲油站名稱</td>" +
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