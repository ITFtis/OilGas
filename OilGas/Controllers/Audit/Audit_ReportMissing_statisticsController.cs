using Dou.Controllers;
using Dou.Models.DB;
using Newtonsoft.Json.Linq;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_ReportMissing_statistics", Name = "依不同年度篩選各集團站檢查發現缺失加油站名單及統計報表", MenuPath = "查核輔導專區/G交叉分析報表", Action = "Index", Index = 13, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_ReportMissing_statisticsController : AGenericModelController<Audit_ReportMissing_statistics>
    {
        static List<Audit_ReportMissing_statistics> _lsARS = new List<Audit_ReportMissing_statistics>();
        static string _CaseType = "";
        static string _workYear = "";
        static string _Business_theme = "";

        // GET: Audit_ReportMissing_statistics
        public ActionResult Index()
        {
            return View();
        }
        protected override Dou.Models.DB.IModelEntity<OilGas.Models.Audit_ReportMissing_statistics> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<OilGas.Models.Audit_ReportMissing_statistics>(new OilGasModelContextExt());
        }
        protected override IEnumerable<Audit_ReportMissing_statistics> GetDataDBObject(IModelEntity<Audit_ReportMissing_statistics> dbEntity, params KeyValueParams[] paras)
        {
            //條件
            _CaseType = KeyValue.GetFilterParaValue(paras, "CaseType");
            _workYear = KeyValue.GetFilterParaValue(paras, "workYear");
            _Business_theme = KeyValue.GetFilterParaValue(paras, "Business_theme");

            //進入頁面不顯示清單(未使用查詢)
            if (string.IsNullOrEmpty(_workYear) || string.IsNullOrEmpty(_Business_theme))
            {
                return new List<Audit_ReportMissing_statistics>().AsQueryable();
            }
            _lsARS = StatisticReportFunc.ConvertToList<Audit_ReportMissing_statistics>(GetListData());
            return _lsARS;
        }
        //集團
        public ActionResult GetCheckItemList(string caseType)
        {
            var f = Code.GetCaseType2().Where(a => a.Key == caseType);

            List<CarVehicleGas_BusinessOrganizationV> itemList = null;
            if (f.Count() == 0)
            {
                return Json(new { result = false, errorMessage = caseType + "查無對應CheckItemTable" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var dbContext = new OilGasModelContextExt();
                Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganizationV> model = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganizationV>(dbContext);

                var datas = model.GetAll().Where(a => a.CaseType == "CarFuel_BasicData").Select(a => new {
                    CaseType = a.CaseType,
                    a.Name,
                    a.Value,
                    a.Rank
                }).Concat(model.GetAll().Where(a => a.CaseType == "FishGas_BasicData").Select(a => new {
                    CaseType = a.CaseType,
                    a.Name,
                    a.Value,
                    a.Rank
                })).Concat(model.GetAll().Where(a => a.CaseType == "SelfFuel_Basic").Select(a => new {
                    CaseType = a.CaseType + "_Up",
                    a.Name,
                    a.Value,
                    a.Rank
                })).Concat(model.GetAll().Where(a => a.CaseType == "SelfFuel_Basic").Select(a => new {
                    CaseType = a.CaseType + "_Down",
                    a.Name,
                    a.Value,
                    a.Rank
                })).ToList();

                var _buss = datas.Select(a => new CarVehicleGas_BusinessOrganizationV
                {
                    CaseType = a.CaseType,
                    Name = a.Name,
                    Value = a.Value,
                    Rank = a.Rank,
                }).OrderBy(a => a.Rank);

                itemList = _buss.Where(x=>x.CaseType == caseType).OrderBy(x=>x.Rank).ToList();

                //string text = f.FirstOrDefault().Value.ToString();
                //JObject json = JObject.Parse(text);

                //var otable = json["CheckItemTable"];
                //if (otable == null)
                //{
                //    return Json(new { result = false, errorMessage = "json無欄位：" + "CheckItemTable" }, JsonRequestBehavior.AllowGet);
                //}

                //string table = otable.ToString();
                //itemList = CheckItemList.GetAllDatas().Where(a => a.CheckItemTable == table)
                //                        .Select(a => new
                //                        {
                //                            CheckItemTitelNo = a.CheckItemTitelNo,
                //                            CheckItemTitel = a.CheckItemTitel,
                //                            CheckItemTitelSum = a.CheckItemTitelSum
                //                        }).Distinct()
                //                        .OrderBy(a => a.CheckItemTitelNo).ThenBy(a => a.CheckItemTitelSum).ThenBy(a => a.CheckItemTitel)
                //                        .ToList()
                //                        .Select(a => new CheckItemList
                //                        {
                //                            CheckItemTitel = a.CheckItemTitel,
                //                            CheckItemTitelSum = a.CheckItemTitelSum
                //                        }).ToList();
            }

            if (itemList == null)
            {
                return Json(new { result = false, errorMessage = caseType + "對應錯誤" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = true, itemList = itemList }, JsonRequestBehavior.AllowGet);
        }

        private DataTable GetListData()
        {
            //UserBasicInfo.UserBasicInfo user = new UserBasicInfo.UserBasicInfo();
            //user.IsLoginAndRedirect();

            //決定那一年度
            string workYear = _workYear.Replace(string.Format("_{0}", _CaseType), "");
            //決定那一縣市
            string _group = _Business_theme;
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
                int CheckItemErrCount = 0;
                for (int row = 0; row < oDT.Rows.Count; row++)
                {
                    DataRow dr = oDT.Rows[row];
                    string workCol = dr["CheckItemTitelSum"].ToString();
                    strSQL = string.Format(@"SELECT sum(isnull({0},0)) as CheckItemErrCount from {1} a join Check_Basic b on a.CheckNo = b.CheckNo
                                         where (year(b.CheckDate)-1911 = {2}) AND (b.Business_theme = '{3}')"
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
                }
            }
            return oDT;
        }
        public ActionResult ExportAudit_ReportMissingstatistics(params KeyValueParams[] paras)
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
            var business = CarVehicleGas_BusinessOrganization.GetAllDatas();
            var citydata = Rpt_CarFuel_Land.GetAllCityCode();
            string ReportName, QryString = "", Total = "", strTotal = "";
            QryString = !string.IsNullOrEmpty(_workYear) ?
                string.Format("<BR> 查詢年度：{0} 年度<BR>", _workYear.Replace(string.Format("_{0}", _CaseType), "")) : "";
            QryString += string.IsNullOrEmpty(_Business_theme) ? "" : "集團：" + business.Where(s => s.Value == _Business_theme).First().Name.ToString();
            DataTable dt = StatisticReportFunc.ConvertToDataTable(_lsARS);

            dt.Columns.Remove("CaseType");
            dt.Columns.Remove("workYear");
            dt.Columns.Remove("Business_theme");

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