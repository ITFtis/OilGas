using Dou.Controllers;
using Dou.Models.DB;
using NPOI.OpenXmlFormats.Spreadsheet;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_ReportMissing_statistics_County", Name = "依各縣市篩選檢查發現缺失項目及統計報表", MenuPath = "查核輔導專區/G交叉分析報表", Action = "Index", Index = 14, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_ReportMissing_statistics_CountyController : AGenericModelController<Audit_ReportMissing_statistics_County>
    {
        static List<Audit_ReportMissing_statistics_County> _lsARSC = new List<Audit_ReportMissing_statistics_County>();
        static string _CaseType = "";
        static string _workYear = "";
        static string _CityCode = "";
        static string _GSLCode = "";

        // GET: Audit_ReportMissing_statistics_County
        public ActionResult Index()
        {
            return View();
        }
        protected override Dou.Models.DB.IModelEntity<OilGas.Models.Audit_ReportMissing_statistics_County> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<OilGas.Models.Audit_ReportMissing_statistics_County>(new OilGasModelContextExt());
        }
        protected override IEnumerable<Audit_ReportMissing_statistics_County> GetDataDBObject(IModelEntity<Audit_ReportMissing_statistics_County> dbEntity, params KeyValueParams[] paras)
        {
            Rpt_CarFuel_Land.ResetGetGSLCodeByCityCode();
            //條件
            _CaseType = KeyValue.GetFilterParaValue(paras, "CaseType");
            _workYear = KeyValue.GetFilterParaValue(paras, "workYear");
            _CityCode = KeyValue.GetFilterParaValue(paras, "CityCode1");
            _GSLCode = string.IsNullOrEmpty(_CityCode) ? "" : Rpt_CarFuel_Land.GetGSLCodeByCityCode(_CityCode).First().GSLCode.ToString();

            //進入頁面不顯示清單(未使用查詢)
            if (string.IsNullOrEmpty(_workYear) || string.IsNullOrEmpty(_CityCode))
            {
                return new List<Audit_ReportMissing_statistics_County>().AsQueryable();
            }
            _lsARSC = StatisticReportFunc.ConvertToList<Audit_ReportMissing_statistics_County>(GetListData());
            return _lsARSC;
        }
        private DataTable GetListData()
        {
            //UserBasicInfo.UserBasicInfo user = new UserBasicInfo.UserBasicInfo();
            //user.IsLoginAndRedirect();

            //決定那一年度
            string workYear = _workYear.Replace(string.Format("_{0}",_CaseType), "");
            //決定那一縣市
            string _group = "";
            if (!string.IsNullOrEmpty(_GSLCode))
            {
                string[] ls_GSLCode = _GSLCode.Split(',');
                foreach (string item in ls_GSLCode)
                {
                    _group += string.Format("'{0}',", item);
                }
                _group = _group.Substring(0, _group.Length - 1);
            }

            //決定那個查核項目
            string workTable = GetWorkTable(_CaseType, int.Parse(workYear));
            string strSQL = string.Format(@"SELECT CheckItemTitel,CheckItemTitelNo,CheckItemTitelSum
                                    ,(select count(*) from CheckItemList where CheckItemTable = '{0}' and CheckItemTitel=a.CheckItemTitel) as CheckItemCount
                                    FROM CheckItemList a 
                                    where CheckItemTable = '{0}'
                                    group by CheckItemTitelNo,CheckItemTitel,CheckItemTitelSum
                                    order by CheckItemTitelNo,CheckItemTitel", workTable);
            DataTable oDT = StatisticReportFunc.getDataTable(strSQL);
            oDT.Columns.Add(new DataColumn("CheckItemErrCount", typeof(Int32)));
            if (oDT.Rows.Count >= 1)
            {
                int CheckItemSum = 0;
                int CheckItemCount = 0;
                int CheckItemErrSum = 0;
                int CheckItemErrCount = 0;
                for (int row = 0; row < oDT.Rows.Count; row++)
                {
                    DataRow dr = oDT.Rows[row];
                    string workCol = dr["CheckItemTitelSum"].ToString();
                    CheckItemCount = Convert.ToInt16(dr["CheckItemCount"].ToString());
                    CheckItemSum += CheckItemCount;
                    strSQL = string.Format(@"SELECT sum(isnull({0},0)) as CheckItemErrCount from {1} a join Check_Basic b on a.CheckNo = b.CheckNo
                                         where (year(b.CheckDate)-1911 = {2})  and (SUBSTRING(b.CaseNo,5,2) in ({3}))"
                                        , workCol, workTable, workYear, _group);
                    DataTable ErrCount = StatisticReportFunc.getDataTable(strSQL);
                    if (ErrCount.Rows[0]["CheckItemErrCount"].ToString() != "")
                    {
                        CheckItemErrCount = Convert.ToInt16(ErrCount.Rows[0]["CheckItemErrCount"].ToString());
                    }
                    else
                    {
                        CheckItemErrCount = 0;
                    }
                    oDT.Rows[row]["CheckItemErrCount"] = CheckItemErrCount;

                    //CheckItemErrSum += CheckItemErrCount;
                }
            }
            return oDT;
        }

        public ActionResult ExportAudit_ReportMissingstatisticsCounty(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G交叉分析報表_依各縣市篩選檢查發現缺失項目及統計報表);
            string fileTitle = "查核輔導專區_加油站各項設備檢查缺失數";

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
            QryString = !string.IsNullOrEmpty(_workYear) ?
                string.Format("<BR> 查詢年度：{0} 年度<BR>", _workYear.Replace(string.Format("_{0}", _CaseType), "")) : "";
            QryString += string.IsNullOrEmpty(_CityCode) ? "" : "縣市：" + citydata.Where(s => s.CityCode1 == _CityCode).First().CityName.ToString();
            DataTable dt = StatisticReportFunc.ConvertToDataTable(_lsARSC);

            dt.Columns.Remove("CaseType");
            dt.Columns.Remove("workYear");
            dt.Columns.Remove("CityCode1");

            string Title = string.Format(@"<tr>" +
                                       "  <td>檢查設備名稱</td>" +
                                       "  <td>檢查項目</td>" +
                                       "  <td>缺失數(項次)</td>" +
                                       "</tr>");

            ReportName = "加油站各項設備檢查缺失數";


            #region 處理加總
            int[] array = new int[2];
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    array[0] += Convert.ToInt32(dt.Rows[i]["CheckItemCount"].ToString().Trim());
                    array[1] += Convert.ToInt32(dt.Rows[i]["CheckItemErrCount"].ToString().Trim());
                }
            }
            for (int i = 0; i < array.Length; i++)
            {
                strTotal += "<td>" + array[i] + "</td>";
            }
            Total = "<tr><td>合計</td>" + strTotal + "</tr>";
            #endregion
            Total = "<tr><td colspan='3'>缺失數定義：檢查設備之各項檢查項目不符合家數之總和。</td></tr>";
            //欄位
            string[] arrField = new string[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                arrField[i] = dt.Columns[i].ColumnName;
            }

            return StatisticReportFunc.DownLoad_Excel(ref dt, ReportName, QryString, arrField, Title, Total, ReportName + DateTime.Now.ToString("yyyyMMddhhmmss"));

        }

        //帶出本次要查甚麼TABLE
        private string GetWorkTable(string workCaseType, int workYear)
        {
            string workTable = "CarFuel_BasicData";

            if (workCaseType == "CarFuel_BasicData")
            {
                //帶出汽機車加油站查核輔導的年度
                if (workYear <= 97)
                {
                    workTable = "Check_Item_97";
                }
                else
                {
                    workTable = "Check_Item";
                }

            }
            else if (workCaseType == "FishGas_BasicData")
            {
                //帶出漁船加油站查核輔導的年度
                if (workYear <= 103)
                {
                    workTable = "Check_Item_Fish103";
                }
                else
                {
                    workTable = "Check_Item_Fish";
                }
            }
            else if (workCaseType == "SelfFuel_Basic_Up")
            {
                workTable = "Check_Item_SelfUP";
            }
            else if (workCaseType == "SelfFuel_Basic_Down")
            {
                workTable = "Check_Item_SelfDown";
            }

            return workTable;
        }
    }
}