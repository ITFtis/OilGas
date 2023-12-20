using Dou.Controllers;
using Dou.Misc;
using Dou.Models.DB;
using NPOI.OpenXmlFormats.Dml.Diagram;
using NPOI.SS.Formula.Functions;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;

namespace OilGas.Controllers.CarFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "CarFuel_Closed", Name = "各縣市暫停營業家數統計報表", MenuPath = "加油站/A統計報表專區", Action = "Index", Index = 7, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]    
    public class CarFuel_ClosedController : AGenericModelController<vw_CarFuel_Closed_show>
    {
        public static List<vw_CarFuel_Closed_show> _vwCFCs = new List<vw_CarFuel_Closed_show>();
        static string _Date_Type = "";
        static string _ModDate_Start_Between_ = "";
        static string _ModDate_End_Between_ = "";
        static string _CityCode = "";
        string _GSLCode = "";

        // GET: CarFuel_Closed
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
        protected override Dou.Models.DB.IModelEntity<OilGas.Models.vw_CarFuel_Closed_show> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<OilGas.Models.vw_CarFuel_Closed_show>(new OilGasModelContextExt());
        }     
        protected override IEnumerable<vw_CarFuel_Closed_show> GetDataDBObject(IModelEntity<vw_CarFuel_Closed_show> dbEntity, params KeyValueParams[] paras)
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
                return new List<vw_CarFuel_Closed_show>().AsQueryable();
            }

            var dbContext = new OilGasModelContextExt();
            Dou.Models.DB.IModelEntity<vw_CarFuel_Closed> model = new Dou.Models.DB.ModelEntity<vw_CarFuel_Closed>(dbContext);

            var res = model.GetAll();

            //where 1.
            if (!string.IsNullOrEmpty(_Date_Type))
                res = _Date_Type == "1" ? res.Where(x => x.CaseType == "1") : res.Where(x => x.CaseType == "2");
            else
            {
                _vwCFCs = new List<vw_CarFuel_Closed_show>();
                return new List<vw_CarFuel_Closed_show>().AsQueryable();
            }

            //where 2.
            if (!string.IsNullOrEmpty(_ModDate_Start_Between_))
            {
                DateTime date = DateTime.Parse(_ModDate_Start_Between_);
                res = res.Where(s => s.Mod_date >= date);
            }
            //where 3.
            if (!string.IsNullOrEmpty(_ModDate_End_Between_))
            {
                DateTime date = DateTime.Parse(_ModDate_End_Between_);
                res = res.Where(s => s.Mod_date <= date);
            }
            //where 4.
            if (!string.IsNullOrEmpty(_CityCode))
            {
                //P092 04 0001=>(P092(04))0001
                var lsGSL = _GSLCode.Split(',');
                res = res.Where(a => a.CaseNo != "" && lsGSL.Any(b => a.CaseNo.Substring(4, 2) == b));
            }

            //欄位加總並依照縣市排序
            var ColumeSum = res.GroupBy(p => new {CityName = p.CityName, CityRank = p.CityRank}).Select(p => new
            {
                CityName = p.Key.CityName,
                CityRank = p.Key.CityRank,
                cpc = p.Sum(x => x.cpc) - p.Sum(x => x.cpc_closed),
                cpc_closed = p.Sum(x => x.cpc_closed),
                notcpc = p.Sum(x => x.notcpc) - p.Sum(x => x.notcpc_closed),
                notcpc_closed = p.Sum(x => x.notcpc_closed),
                tv = (p.Sum(x => x.cpc) - p.Sum(x => x.cpc_closed)) + p.Sum(x => x.cpc_closed) +
                (p.Sum(x => x.notcpc) - p.Sum(x => x.notcpc_closed)) + p.Sum(x => x.notcpc_closed)
            }).OrderBy(x => x.CityRank).ToList();

            //將資料匯入畫面顯示用的Class
            var lsColumeSumShow = ColumeSum.Select(a => new vw_CarFuel_Closed_show
            {                
                CityName = a.CityName,
                cpc = a.cpc,
                cpc_closed = a.cpc_closed,
                notcpc = a.notcpc,
                notcpc_closed = a.notcpc_closed,
                tv = a.tv,
            }).ToList();

            _vwCFCs = lsColumeSumShow;

            return lsColumeSumShow;
        }
        /// <summary>
        /// 計算最後一列加總資料
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSumData()
        {
            #region 底部總計
            int sumcpc = 0;
            int sumcpcclosed = 0;
            int sumnotcpc = 0;
            int sumnotcpcclosed = 0;
            int sumtv = 0;

            if (string.IsNullOrEmpty(_Date_Type))
                return Json(new List<string> { "", "", "", "", "" });

            foreach (var item in _vwCFCs)
            {
                sumcpc = sumcpc + item.cpc;
                sumcpcclosed = sumcpcclosed + item.cpc_closed;
                sumnotcpc = sumnotcpc + item.notcpc;
                sumnotcpcclosed = sumnotcpcclosed + item.notcpc_closed;
                sumtv = sumtv + item.tv;
            }
            # endregion 底部總計
            //沒有資料就不顯示統計資料
            return Json(new List<string> { sumcpc.ToString(), sumcpcclosed.ToString(), sumnotcpc.ToString(), 
                sumnotcpcclosed.ToString(), sumtv.ToString()});
        }
        /// <summary>
        /// 匯出EXCEL
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public ActionResult ExportCarFuel_Closed(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.加油站_A統計報表專區_各縣市暫停營業家數統計報表);
            string fileTitle = "加油站_各縣市暫停營業家數統計報表";

            //List<string> titles = new List<string>() { "加油站_各縣市暫停營業家數統計報表，查詢條件:" };

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
            QryString = string.IsNullOrEmpty(_CityCode) ? "全國" : citydata.Where(s => s.CityCode1 == _CityCode).First().CityName.ToString();
            DataTable dt = StatisticReportFunc.ConvertToDataTable(_vwCFCs);

            dt.Columns.Remove("CaseType");
            dt.Columns.Remove("CITY");
            dt.Columns.Remove("Mod_date");

            string Title = string.Format(@"<tr>" +
                                           "  <td>縣市別</td>" +
                                           "  <td>中油(己開業扣除暫停營業)</td>" +
                                           "  <td>中油暫停營業</td>" +
                                           "  <td>非中油(己開業扣除暫停營業)</td>" +
                                           "  <td>非中油暫停營業</td>" +
                                           "  <td>總計</td>" +
                                           "</tr>");

            ReportName = "各縣市暫停營業家數統計報表";

            #region 處理加總
            int[] array = new int[5];
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    array[0] += Convert.ToInt32(dt.Rows[i]["cpc"].ToString().Trim());
                    array[1] += Convert.ToInt32(dt.Rows[i]["cpc_closed"].ToString().Trim());
                    array[2] += Convert.ToInt32(dt.Rows[i]["notcpc"].ToString().Trim());
                    array[3] += Convert.ToInt32(dt.Rows[i]["notcpc_closed"].ToString().Trim());
                    array[4] += Convert.ToInt32(dt.Rows[i]["tv"].ToString().Trim());
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
    }
}