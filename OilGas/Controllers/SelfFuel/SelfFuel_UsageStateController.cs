using Dou.Controllers;
using Dou.Misc;
using Dou.Models.DB;
using NPOI.SS.Formula.Functions;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static StatisticReportFunc;

namespace OilGas.Controllers.SelfFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "SelfFuel_UsageState", Name = "使用狀況統計查詢", MenuPath = "自用加儲油/D統計報表專區", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class SelfFuel_UsageStateController : AGenericModelController<SelfFuel_UsageState>
    {
        // GET: SelfFuel_UsageState
        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable<SelfFuel_UsageState> GetDataDBObject(IModelEntity<SelfFuel_UsageState> dbEntity, params KeyValueParams[] paras)
        {
            return base.GetDataDBObject(dbEntity, paras);
        }

        string _CaseType = "";
        string _CaseTypeName = "";
        string _ModDate_Start_Between_ = "";
        string _ModDate_End_Between_ = "";
        string _BigUsage = "";
        string _UsageState = "";
        string _UsageStateName = "";
        int _UsageStateCount = 0;
        List<UsageStateDetail> _lsUsageStateDetail = new List<UsageStateDetail>();
        string _CaseNo = "";
        string _FuelName = "";
        string _Responsor = "";
        string _CityCode = "";

        public ActionResult ExportSelfFuelUsageStateView(params KeyValueParams[] paras)
        {
            var dbContext = new OilGasModelContextExt();
            Dou.Models.DB.IModelEntity<UsageStateDetail> UsageStateDetail = new Dou.Models.DB.ModelEntity<UsageStateDetail>(dbContext);
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.自用加儲油站_D統計報表專區_使用狀況統計報表);
            string fileTitle = "自用加儲油站_D統計報表專區_使用狀況統計報表";
            List<string> titles = new List<string>() { "自用加儲油站_使用狀況統計查詢，查詢條件:" };

            _CaseType = KeyValue.GetFilterParaValue(paras, "CaseType");
            if (_CaseType == "")
            {
                return Json(new { result = false, errorMessage = "查無資料" }, JsonRequestBehavior.AllowGet); ;
            }
            _CaseTypeName = _CaseType == "SelfFuel_Basic" ? "最新使用狀況設定日期" : "使用狀況變更歷程日期";
            _ModDate_Start_Between_ = KeyValue.GetFilterParaValue(paras, "Mod_date-Start-Between_");
            _ModDate_End_Between_ = KeyValue.GetFilterParaValue(paras, "Mod_date-End-Between_");
            _BigUsage = KeyValue.GetFilterParaValue(paras, "BigUsage");
            _UsageState = KeyValue.GetFilterParaValue(paras, "UsageState");
            _CaseNo = KeyValue.GetFilterParaValue(paras, "CaseNo");
            _FuelName = KeyValue.GetFilterParaValue(paras, "FuelName");
            _Responsor = KeyValue.GetFilterParaValue(paras, "Responsor");
            _lsUsageStateDetail = string.IsNullOrEmpty(_BigUsage) ? (string.IsNullOrEmpty(_UsageState) ?
                UsageStateDetail.GetAll().ToList().OrderBy(x => Convert.ToInt32(x.UsageStateDetailID)).ToList() :
                UsageStateDetail.GetAll().Where(x => x.UsageStateDetailID == _UsageState).ToList())
                :
                (string.IsNullOrEmpty(_UsageState) ?
                UsageStateDetail.GetAll().Where(x => x.BigUsageStateID == _BigUsage).ToList() :
                UsageStateDetail.GetAll().Where(x => x.UsageStateDetailID == _UsageState).ToList());
            _UsageStateCount = _lsUsageStateDetail.Count();
            _UsageStateName = string.IsNullOrEmpty(_UsageState) ? "" :
                UsageStateDetail.GetAll().Where(x => x.UsageStateDetailID == _UsageState).FirstOrDefault().Name.ToString();

            _CityCode = Dou.Context.CurrentUser<User>().OrganizationName;

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

        public string getStrHtml()
        {
            #region 設定 查詢結果資料簡介樣版
            StringBuilder sbHeader = new StringBuilder();
            string headerHtml = "";
            sbHeader.Append(@"<tr class=""gv_thead"">");
            sbHeader.AppendFormat(@"<th colspan=""{0}"">使用狀況統計查詢</th>"
                , (_UsageState == "") ? _UsageStateCount + 2 : 3);
            sbHeader.Append(@"</tr>");
            sbHeader.Append(@"<tr class=""gv_thead"">");
            sbHeader.AppendFormat(@"<th colspan=""{0}"" style=""text-align:right"">查詢條件：統計時間為{1}{2}<br />{3}<br />使用狀況為{4}<br />報表產出時間：{5}</th>"
                , (_UsageState == "") ? _UsageStateCount + 2 : 3
                , _CaseTypeName
                , (!string.IsNullOrEmpty(_ModDate_Start_Between_) || !string.IsNullOrEmpty(_ModDate_End_Between_))
                    ? _ModDate_Start_Between_ + "～ " + _ModDate_End_Between_
                    : ""
                , _CityCode
                , _UsageStateName
                , DateTime.Now
                );
            sbHeader.Append(@"</tr>");
            #endregion

            #region 設定 查詢結果表頭樣版
            sbHeader.Append(@"<tr class=""gv_thead"">");
            sbHeader.Append(@"<th>縣市/使用狀況</th>");
            for (int i = 0; i < _lsUsageStateDetail.Count; i++)
            {
                if (_UsageState == "")
                    sbHeader.AppendFormat(@"<th>{0}</th>", _lsUsageStateDetail[i].Name);
                else
                {
                    sbHeader.AppendFormat(@"<th>{0}</th>", _UsageStateName);
                    break;
                }
            }
            sbHeader.Append(@"<th>小計</th>");
            sbHeader.Append(@"</tr>");
            headerHtml = sbHeader.ToString();
            #endregion

            #region 設定 查詢結果內容樣版
            StringBuilder sbBody = new StringBuilder();
            string bodyHtml = "";
            string _tdCss = "gv_tbody";
            string tableName = _CaseType;
            List<CityCode> dtCity = new List<CityCode>();
            //權限查詢
            basicController basic = new basicController();
            if (!Dou.Context.CurrentIsAdminUser && !basic.Permissions("admin"))
            {
                var cc = CityCode.GetAllDatas();
                dtCity = cc.Where(x => x.GSLCode == Dou.Context.CurrentUser<User>().city).ToList();
            }
            //DataTable dtCity = Code.GetCity();
            DataTable dtTotalCount = new DataTable();//計算'總計'數量用的

            dtTotalCount.Columns.Add("縣市");
            foreach(var item in _lsUsageStateDetail)
                dtTotalCount.Columns.Add(item.Name);
            //for (int col = ((_UsageState == "") ? 0 : ddl_UsageState.SelectedIndex); col <= ((_UsageState == "") ? _lsUsageStateDetail.Count : ddl_UsageState.SelectedIndex); col++)
            //    dtTotalCount.Columns.Add(_lsUsageStateDetail[col].Name);
            dtTotalCount.Columns.Add("小計");

            foreach(var item1 in dtCity)
            {
                DataRow drTotalCount = dtTotalCount.NewRow();//計算'總計'數量用的
                drTotalCount["縣市"] = item1.CityName;//計算'總計'數量用的
                int total = 0, result = 0;
                sbBody.AppendFormat(@"<tr class=""trR""><td class=""{0} td1"">{1}</td>"
                    , _tdCss, item1.CityName);
                foreach (var item2 in _lsUsageStateDetail)
                {
                    #region 設定 查詢條件
                    SelfFuel_Condition condition = new SelfFuel_Condition();
                    condition.ModifyStartTime = ConvertHelper.ToDateTime(_ModDate_Start_Between_);
                    condition.ModifyEndTime = ConvertHelper.ToDateTime(_ModDate_End_Between_);
                    condition.CityCode = item1.GSLCode;//Code.GetOldSysCityCode(dtCity.Rows[row]["CityCode"].ToString());
                    SetUsageState(ref condition, item2);//解析所選的使用狀況
                    condition.CaseNo = _CaseNo;
                    condition.FuelName = _FuelName;
                    condition.Responsor = _Responsor;
                    condition.IsConfirm = true;
                    #endregion

                    #region 計算並統計-中間的值
                    //SelfFuel_Statistic statistic = new SelfFuel_Statistic();
                    result = GetUsageStateReport(tableName, condition);
                    total += result;
                    sbBody.AppendFormat(@"<td class=""{0} td1"">{1}</td>"
                    , _tdCss
                    , result);
                    #endregion

                    #region 計算'總計'數量
                    drTotalCount[item2.Name] = result;
                    #endregion
                }
                #region  計算並統計-最後小計
                sbBody.AppendFormat(@"
                    <td class=""{0} td1"">{1}</td>"
                    , _tdCss
                    , total);
                #endregion

                #region 計算'總計'數量
                drTotalCount["小計"] = total;
                dtTotalCount.Rows.Add(drTotalCount);
                #endregion

                sbBody.Append(@"</tr>");
            } 

            #region 產生最後一列(總計)
            sbBody.AppendFormat(@"
                <tr class=""trR""><td class=""{0} td1"">{1}</td>"
                    , _tdCss, "總計");
            for (int col = 1; col < dtTotalCount.Columns.Count; col++)
            {
                int colCount = 0;
                for (int row = 0; row < dtTotalCount.Rows.Count; row++)
                {
                    colCount += ConvertHelper.ToInt(dtTotalCount.Rows[row][col]);

                }
                sbBody.AppendFormat(@"
                    <td class=""{0} td1"">{1}</td>"
                    , _tdCss
                    , colCount);
            }
            sbBody.Append(@"</tr>");
            #endregion

            bodyHtml = sbBody.ToString();
            #endregion

            #region 設定 查詢結果合體樣版
            StringBuilder sbResult = new StringBuilder();
            string resultHtml = "";
            sbResult.AppendFormat(@"
                 <table class=""formx2 tbl1"">
                  <thead>{0}</thead>
                  <tbody>{1}</tbody>
                 </table>"
                , headerHtml
                , bodyHtml);

            #endregion

            #region 查無資料的訊息
            if (bodyHtml.Length == 0)
            {
                return "";
            }
            #endregion

            resultHtml = string.Format(@"
                    <html>
                        <head>
                            <meta http-equiv=""Content-Type"" content=""text/html;charset=utf-8"" />
                            <style type=""text/css"">
                                td {{ mso-number-format:""\@""; }}
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
                    , sbResult.ToString()
                        .Replace("<table class=\"formx2 tbl1\">", "<table border=\"1\" class=\"formx2 tbl1\">")
                        .Replace("</table>", "</table><table></table>")
                    );

            return resultHtml;
        }

        protected override Dou.Models.DB.IModelEntity<OilGas.Models.SelfFuel_UsageState> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<OilGas.Models.SelfFuel_UsageState>(new OilGasModelContextExt());
        }
        /// <summary>
        /// //解析所選的使用狀況
        /// </summary>
        /// <param name="condition">條件</param>
        /// <param name="UsageStateValue">所選的使用狀況</param>
        private void SetUsageState(ref SelfFuel_Condition condition, UsageStateDetail USD)
        {
            if (USD != null)
            {
                condition.UsageState = USD.UsageState;
                condition.UsageState_Second = USD.UsageState_Second;
                condition.UsageState_Third = USD.UsageState_Third;
                condition.UsageState_Fourth = USD.UsageState_Fourth;
            }
        }

        #region 使用狀況的統計資料
        /// <summary>
        /// 使用狀況的統計資料
        /// </summary>
        /// <param name="tableName">資料表名稱</param>
        /// <param name="condition">查詢條件</param>
        /// <returns></returns>
        public int GetUsageStateReport(string tableName, SelfFuel_Condition condition)
        {
            string sqlWhr = "";
            sqlWhr += SQLHelper.AssembleWhrSQLWithAND(ConvertHelper.DateToString(condition.ModifyStartTime), " ModifyTime >= '{0}' ");
            sqlWhr += SQLHelper.AssembleWhrSQLWithAND(ConvertHelper.DateToString(condition.ModifyEndTime), " ModifyTime <= '{0}' ");
            sqlWhr += string.Format(@" AND SUBSTRING(CaseNo,5,2) IN ('{0}') ", condition.CityCode.Replace(",", "','"));
            /*當使用狀況為使用管理-使用中(1,1,'','')，需包含
                 *  1. 申請設置-申請中-同意設置-同意使用(進入使用管理-使用中)(0,0,0,0)
                 *  2. 使用管理-使用中(1,1,'','')
                 */
            if (condition.UsageState == "1" && condition.UsageState_Second == "1" && condition.UsageState_Third == "" && condition.UsageState_Fourth == "")
            {
                sqlWhr += string.Format(@" AND (( UsageState = '0' ");
                sqlWhr += string.Format(@" AND UsageState_Second = '0' ");
                sqlWhr += string.Format(@" AND UsageState_Third = '0' ");
                sqlWhr += string.Format(@" AND UsageState_Fourth = '0' )  ");
                sqlWhr += string.Format(@" OR ( UsageState = '1' ");
                sqlWhr += string.Format(@" AND UsageState_Second = '1' ");
                sqlWhr += string.Format(@" AND UsageState_Third = '' ");
                sqlWhr += string.Format(@" AND UsageState_Fourth = '' )) ");
            }
            else if (condition.UsageState == "1" && condition.UsageState_Second == "2" && condition.UsageState_Third == "" && condition.UsageState_Fourth == "")
            {
                sqlWhr += string.Format(@" AND (( UsageState = '1' ");
                sqlWhr += string.Format(@" AND UsageState_Second = '1' ");
                sqlWhr += string.Format(@" AND (UsageState_Third = '3' OR UsageState_Third = '4' OR UsageState_Third = '5' ) ");
                sqlWhr += string.Format(@" AND UsageState_Fourth = '' )  ");
                sqlWhr += string.Format(@" OR ( UsageState = '1' ");
                sqlWhr += string.Format(@" AND UsageState_Second = '2' ");
                sqlWhr += string.Format(@" AND UsageState_Third = '' ");
                sqlWhr += string.Format(@" AND UsageState_Fourth = '' )) ");
            }
            //使用中變更需包含'','5'申請基地位置,'6'儲油(氣)槽,'7'營業主體,'8'負責人或法定代理人
            else if (condition.UsageState == "1" && condition.UsageState_Second == "1" && condition.UsageState_Third == "6")
            {
                sqlWhr += string.Format(@" AND (( UsageState = '1' ");
                sqlWhr += string.Format(@" AND UsageState_Second = '1' ");
                sqlWhr += string.Format(@" AND UsageState_Third ='6' ");
                sqlWhr += string.Format(@" AND UsageState_Fourth in ('','5','6','7','8') )) ");
            }
            else
            {
                sqlWhr += string.Format(@" AND UsageState = '{0}' ", condition.UsageState);
                sqlWhr += string.Format(@" AND UsageState_Second = '{0}' ", condition.UsageState_Second);
                sqlWhr += string.Format(@" AND UsageState_Third = '{0}' ", condition.UsageState_Third);
                sqlWhr += string.Format(@" AND UsageState_Fourth = '{0}' ", condition.UsageState_Fourth);
            }
            /*當使用狀況為使用管理-使用中時，
              應將申請設置-申請中-同意設置-同意使用與使用管理-使用中視為是相同的
            */
            //        if (condition.UsageState == "1" && condition.UsageState_Second == "1" && condition.UsageState_Third == "" && condition.UsageState_Fourth == "")
            //        {
            //            sqlWhr += @" AND (
            //                                (UsageState = '1' AND UsageState_Second = '1' AND UsageState_Third = '' AND UsageState_Fourth = '') OR 
            //                                (UsageState = '0' AND UsageState_Second = '0' AND UsageState_Third = '0' AND UsageState_Fourth = '0')
            //                              )";
            //        }
            //        else
            //        {
            //            sqlWhr += string.Format(" AND UsageState = '{0}'", condition.UsageState);
            //            sqlWhr += string.Format(" AND UsageState_Second = '{0}'", condition.UsageState_Second);
            //            sqlWhr += string.Format(" AND UsageState_Third = '{0}'", condition.UsageState_Third);
            //            sqlWhr += string.Format(" AND UsageState_Fourth = '{0}'", condition.UsageState_Fourth);
            //        }
            sqlWhr += SQLHelper.AssembleWhrSQLWithAND(condition.CaseNo, " CaseNo = '{0}' ");
            sqlWhr += SQLHelper.AssembleWhrSQLWithAND(condition.FuelName, " FuelName LIKE '%{0}%' ");
            sqlWhr += SQLHelper.AssembleWhrSQLWithAND(condition.Responsor, " Responsor = '{0}' ");
            sqlWhr += SQLHelper.AssembleWhrSQLWithAND(ConvertHelper.BooleanToString(condition.IsConfirm), " IsConfirm = '{0}' ");
            sqlWhr = SQLHelper.ToWhr(sqlWhr);

            string sql = string.Format(@"
                    SELECT COUNT(A.CaseNo) Counts
                    FROM
                    (
                        SELECT CaseNo FROM {0} {1} GROUP BY CaseNo
                    ) A"
                        , tableName
                        , sqlWhr);
            DataTable tmpdt = StatisticReportFunc.getDataTable(sql);
            return Convert.ToInt32(tmpdt.Rows[0]["Counts"]);
        }
        #endregion
    }
    public class SelfFuel_Condition
    {
        public Int32 Id = -1;
        public String FileUploadId = string.Empty;
        public String CityCode = string.Empty;
        public String AreaNo = string.Empty;
        public String FuelName = string.Empty;
        public String CaseNo = string.Empty;
        public String BanCaseNo = string.Empty;
        public Boolean? IsConfirm = null;
        public DateTime? ExpiredStartDate = null;
        public DateTime? ExpiredEndDate = null;
        public String Address = string.Empty;
        public String AddressNo = string.Empty;
        public String BusiOrg = string.Empty;
        public String BigUsage = string.Empty;
        public String UsageState = String.Empty;
        public String UsageState_Second = String.Empty;
        public String UsageState_Third = String.Empty;
        public String UsageState_Fourth = String.Empty;
        public DateTime? ModifyStartTime = null;
        public DateTime? ModifyEndTime = null;
        public String Responsor = string.Empty;
        public DateTime? IllegalStartDate = null;
        public DateTime? IllegalEndDate = null;

        public SelfFuel_Condition()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }
    }
}
