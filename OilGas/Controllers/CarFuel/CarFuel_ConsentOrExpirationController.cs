﻿using Dou.Controllers;
using Dou.Misc;
using Dou.Models.DB;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "CarFuel_ConsentOrExpiration", Name = "加油站申請設置同意認定/籌建到期報表", MenuPath = "加油站/A異常報表", Action = "Index", Index = 3, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class CarFuel_ConsentOrExpirationController : AGenericModelController<CarFuel_ConsentOrExpiration>
    {
        public static List<CarFuel_ConsentOrExpiration> _lsCFC = new List<CarFuel_ConsentOrExpiration>();
        static string _Date_Type = "";
        static string _ModDate_Start_Between_ = "";
        static string _ModDate_End_Between_ = "";
        static string _CityCode = "";
        string _GSLCode = "";
        // GET: CarFuel_ConsentOrExpiration
        public ActionResult Index()
        {
            return View();
        }

        public override DataManagerOptions GetDataManagerOptions()
        {
            var opts = base.GetDataManagerOptions();
            opts.editable = false;
            opts.deleteable = false;
            opts.addable = false;
            return opts;
        }

        protected override Dou.Models.DB.IModelEntity<OilGas.Models.CarFuel_ConsentOrExpiration> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<OilGas.Models.CarFuel_ConsentOrExpiration>(new OilGasModelContextExt());
        }

        protected override IEnumerable<CarFuel_ConsentOrExpiration> GetDataDBObject(IModelEntity<CarFuel_ConsentOrExpiration> dbEntity, params KeyValueParams[] paras)
        {
            Rpt_CarFuel_Land.ResetGetGSLCodeByCityCode();
            _Date_Type = HelperUtilities.GetFilterParaValue(paras, "CaseType");
            _ModDate_Start_Between_ = HelperUtilities.GetFilterParaValue(paras, "Mod_date-Start-Between_");
            _ModDate_End_Between_ = HelperUtilities.GetFilterParaValue(paras, "Mod_date-End-Between_");
            _CityCode = HelperUtilities.GetFilterParaValue(paras, "CITY");
            _GSLCode = _CityCode != null ? Rpt_CarFuel_Land.GetGSLCodeByCityCode(_CityCode).First().GSLCode.ToString() : "";

            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return new List<CarFuel_ConsentOrExpiration>();
            }

            var res = getData();
            _lsCFC = StatisticReportFunc.ConvertToList<CarFuel_ConsentOrExpiration>(res);
            return _lsCFC;
        }

        private DataTable getData()
        {
            string strSQL = StatisticReportFunc.GetConsentOrExpirationSql(
            OilGas.Code.SubSystemType.CarFuel, _GSLCode, _Date_Type, _ModDate_Start_Between_, _ModDate_End_Between_);
            DataTable dt = StatisticReportFunc.getDataTable(strSQL);
            return dt;
        }

        public ActionResult ExportCarFuel_ConsentOrExpiration()
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.加油站_A統計報表專區_申請設置案件同意認定_籌建到期報表);
            string fileTitle = "加油站_申請設置案件同意認定_籌建到期報表";

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
            string ReportName, QryString = "", Total = "";
            QryString = !string.IsNullOrEmpty(_ModDate_Start_Between_) && !string.IsNullOrEmpty(_ModDate_End_Between_) ?
                string.Format("<BR> 到期日期：{0} 至 {1} <BR>", _ModDate_Start_Between_, _ModDate_End_Between_) : "";
            QryString += string.IsNullOrEmpty(_CityCode) ? "縣市別：全國" : "縣市別：" + citydata.Where(s => s.CityCode1 == _CityCode).First().CityName.ToString();
            DataTable dt = StatisticReportFunc.ConvertToDataTable(_lsCFC);

            dt.Columns.Remove("Mod_date");
            dt.Columns.Remove("CITY");
            dt.Columns.Remove("CaseType");

            //案件編號	縣市別	收件日期	名稱	營運狀況	發文日期	到期日期	土地使用分區	土地類別
            string Title = string.Format(@"<tr>" +
                                       "  <td>案件編號</td>" +
                                       "  <td>縣市別</td>" +
                                       "  <td>收件日期</td>" +
                                       "  <td>名稱</td>" +
                                       "  <td>營運狀況</td>" +
                                       "  <td>發文日期</td>" +
                                       "  <td>到期日期</td>" +
                                       "  <td>土地使用分區</td>" +
                                       "  <td>土地類別</td>" +
                                       "</tr>");

            ReportName = "申請設置案件同意認定_籌建到期報表";

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