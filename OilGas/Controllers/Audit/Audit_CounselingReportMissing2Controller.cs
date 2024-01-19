using Dou.Controllers;
using Dou.Models.DB;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_CounselingReportMissing2", Name = "各集團出席率與查核缺失數比較圖", MenuPath = "查核輔導專區/G輔導講習專區", Action = "Index", Index = 5, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_CounselingReportMissing2Controller : AGenericModelController<Audit_CounselingReportMissing2>
    {
        static List<Audit_CounselingReportMissing2> _lsAuditCRM2 = new List<Audit_CounselingReportMissing2>();
        static List<ChartAuditCRM2> _lsChartAuditCRM2 = new List<ChartAuditCRM2>();
        static string Counseling_Year = "";
        static string CheckYear = "";
        static string workYear1 = "";
        static string workYear2 = "";
        static string workYear3 = "";
        static string workYear4 = "";

        // GET: Audit_CounselingReportMissing2
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetChartData()
        {
            //進入頁面不顯示清單(未使用查詢)

            if (string.IsNullOrEmpty(Counseling_Year) && string.IsNullOrEmpty(CheckYear))
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            var lstquery = _lsChartAuditCRM2;
            return Json(lstquery, JsonRequestBehavior.AllowGet);
        }

        protected override Dou.Models.DB.IModelEntity<OilGas.Models.Audit_CounselingReportMissing2> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<OilGas.Models.Audit_CounselingReportMissing2>(new OilGasModelContextExt());
        }
        protected override IEnumerable<Audit_CounselingReportMissing2> GetDataDBObject(IModelEntity<Audit_CounselingReportMissing2> dbEntity, params KeyValueParams[] paras)
        {
            //條件
            Counseling_Year = KeyValue.GetFilterParaValue(paras, "Counseling_Year");
            CheckYear = KeyValue.GetFilterParaValue(paras, "CheckYear");
            //進入頁面不顯示清單(未使用查詢)
            if ((string.IsNullOrEmpty(Counseling_Year) && string.IsNullOrEmpty(CheckYear)) ||
                (Counseling_Year.Split(',').Length > 2 || CheckYear.Split(',').Length > 2))
            {
                return new List<Audit_CounselingReportMissing2>().AsQueryable();
            }

            workYear1 = Counseling_Year.Split(',')[0];
            workYear2 = Counseling_Year.Split(',').Length == 1 ? "" : Counseling_Year.Split(',')[1];
            workYear3 = CheckYear.Split(',')[0];
            workYear4 = CheckYear.Split(',').Length == 1 ? "" : CheckYear.Split(',')[1];
            DataTable tmpdt1 = new DataTable();
            DataTable tmpdt2 = new DataTable();
            DataTable tmpdt3 = new DataTable();
            DataTable tmpdt4 = new DataTable();
            DataTable mergedt = new DataTable();
            mergedt.Columns.Add(new DataColumn() { ColumnName = "workItem", DataType = typeof(String) });
            mergedt.Columns.Add(new DataColumn() { ColumnName = "Value", DataType = typeof(String) });
            mergedt.Columns.Add(new DataColumn() { ColumnName = "Counseling_Year1", DataType = typeof(String) });
            mergedt.Columns.Add(new DataColumn() { ColumnName = "Counseling_Year2", DataType = typeof(String) });
            mergedt.Columns.Add(new DataColumn() { ColumnName = "CheckYear1", DataType = typeof(String) });
            mergedt.Columns.Add(new DataColumn() { ColumnName = "CheckYear2", DataType = typeof(String) });
            mergedt.Columns.Add(new DataColumn() { ColumnName = "Counseling_Year_Average1", DataType = typeof(Decimal) });
            mergedt.Columns.Add(new DataColumn() { ColumnName = "Counseling_Year_Average2", DataType = typeof(Decimal) });
            mergedt.Columns.Add(new DataColumn() { ColumnName = "CheckYear_Average1", DataType = typeof(Decimal) });
            mergedt.Columns.Add(new DataColumn() { ColumnName = "CheckYear_Average2", DataType = typeof(Decimal) });

            if (workYear1 != "")
            {
                tmpdt1 = getCounselingRate(workYear1);
                foreach (DataRow item in tmpdt1.Rows)
                {
                    DataRow dr = mergedt.NewRow();
                    dr["workItem"] = item["workItem"].ToString();
                    dr["Value"] = item["Value"].ToString();
                    dr["Counseling_Year1"] = workYear1;
                    dr["Counseling_Year_Average1"] = item["Average"];
                    mergedt.Rows.Add(dr);
                }
            }

            if (workYear2 != "")
            {
                tmpdt2 = getCounselingRate(workYear2);
                foreach (DataRow item in tmpdt2.Rows)
                {
                    for (int i = 0; i < mergedt.Rows.Count; i++)
                    {
                        if (mergedt.Rows[i]["workItem"].ToString() == item["workItem"].ToString())
                        {
                            mergedt.Rows[i]["Counseling_Year2"] = workYear2;
                            mergedt.Rows[i]["Counseling_Year_Average2"] = item["Average"];
                        }
                    }
                }
            }

            if (workYear3 != "")
            {
                tmpdt3 = getCheckRate(workYear3);
                foreach (DataRow item in tmpdt3.Rows)
                {
                    for (int i = 0; i < mergedt.Rows.Count; i++)
                    {
                        if (mergedt.Rows[i]["workItem"].ToString() == item["workItem"].ToString())
                        {
                            mergedt.Rows[i]["CheckYear1"] = workYear3;
                            mergedt.Rows[i]["CheckYear_Average1"] = item["Average"];
                        }
                    }
                }
            }

            if (workYear4 != "")
            {
                tmpdt4 = getCheckRate(workYear4);
                foreach (DataRow item in tmpdt4.Rows)
                {
                    for (int i = 0; i < mergedt.Rows.Count; i++)
                    {
                        if (mergedt.Rows[i]["workItem"].ToString() == item["workItem"].ToString())
                        {
                            mergedt.Rows[i]["CheckYear2"] = workYear4;
                            mergedt.Rows[i]["CheckYear_Average2"] = item["Average"];
                        }
                    }
                }
            }
            _lsChartAuditCRM2 = StatisticReportFunc.ConvertToList<ChartAuditCRM2>(mergedt);
            _lsAuditCRM2 = StatisticReportFunc.ConvertToList<Audit_CounselingReportMissing2>(getListData(workYear1, workYear2, workYear3, workYear4));

            return _lsAuditCRM2;
        }

        /// <summary>
        /// 製作HTML格式給匯出EXCEL使用
        /// </summary>
        /// <returns></returns>
        protected string getStrHtml()
        {
            string ReportName, QryString = "", Total = "";
            DataTable dt = StatisticReportFunc.ConvertToDataTable(_lsAuditCRM2);
            dt.Columns.Remove("Counseling_Year");
            dt.Columns.Remove("CheckYear");
            dt.Columns.Remove("CityCode");
            dt.Columns.Remove("Rank");
            dt.Columns.Remove("GSLCode");
            string Title = string.Format(@"<tr>" +
                                       "  <td rowspan=\"2\" align=\"center\">集團別</td>" +
                                       "  <td colspan=\"3\" align=\"center\">" + workYear1 + "年講習會出席</td>" +
                                       "  <td colspan=\"3\" align=\"center\">" + workYear2 + "年講習會出席</td>" +
                                       "  <td colspan=\"3\" align=\"center\">" + workYear3 + "年查核結果</td>" +
                                       "  <td colspan=\"3\" align=\"center\">" + workYear4 + "年查核結果</td>" +
                                       "</tr>" +
                                       "<tr>" +
                                       "  <td>出席站</td>" +
                                       "  <td>轄內加油站</td>" +
                                       "  <td>出席率(站)</td>" +
                                       "  <td>出席站</td>" +
                                       "  <td>轄內加油站</td>" +
                                       "  <td>出席率(站)</td>" +
                                       "  <td>總缺失數</td>" +
                                       "  <td>查核加油站</td>" +
                                       "  <td>平均缺失數</td>" +
                                       "  <td>總缺失數</td>" +
                                       "  <td>查核加油站</td>" +
                                       "  <td>平均缺失數</td>" +
                                       "</tr>");

            ReportName = "石油設施查核項目缺失彙整";

            //欄位
            string[] arrField = new string[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                arrField[i] = dt.Columns[i].ColumnName;
            }

            return StatisticReportFunc.DownLoad_Excel(ref dt, ReportName, QryString, arrField, Title, Total, ReportName + DateTime.Now.ToString("yyyyMMddhhmmss"));
        }

        public ActionResult ExportAudit_CounselingReportMissing2()
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G輔導講習會專區_各集團出席率與查核缺失數比較);
            string fileTitle = "各集團出席率與查核缺失數比較";

            var ltrResults = getStrHtml();

            if (ltrResults == "")
            {
                return Json(new { result = false, errorMessage = "查無資料" }, JsonRequestBehavior.AllowGet);
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

        //計算講習會出席率
        private DataTable getCounselingRate(string workYear)
        {
            string sql = "";
            sql += string.Format(@"
  SELECT [CaseType]
      ,[Name]
      ,[Value]
      ,[Rank]
      ,[IsEnable]
      ,[ShortName] as workItem
      ,[OldCode],
	  (select count(distinct s_CaseNo) from CounselingData where s_year='{0}' and s_CaseNo is not null and s_BusinessNo=a.Value) as MolecularCount,
	  (select sum(Total) from Gas_Total_TempV where CaseType='CarFuel_BasicData' and value=a.Value) as DenominatorCount,
      case when isnull((select sum(Total) from Gas_Total_TempV where CaseType='CarFuel_BasicData' and value=a.Value),0)=0 then 0 else convert(decimal(6, 2),isnull((select count(distinct s_CaseNo) from CounselingData where s_year='{0}' and s_CaseNo is not null and s_BusinessNo=a.Value),0) * 100.0/isnull((select sum(Total) from Gas_Total_TempV where CaseType='CarFuel_BasicData' and value=a.Value),1)) end as Average
  FROM [dbo].[CarVehicleGas_BusinessOrganizationV] a
  where CaseType='CarFuel_BasicData'
  order by Rank", workYear);

            DataTable dt = StatisticReportFunc.getDataTable(sql);

            return dt;
        }

        //計算查核平均缺失數
        private DataTable getCheckRate(string workYear)
        {
            string sql = "";
            sql += string.Format(@"
  SELECT [CaseType]
      ,[Name]
      ,[Value]
      ,[Rank]
      ,[IsEnable]
      ,[ShortName] as workItem
      ,[OldCode],
	  (select count(distinct CaseNo) from Check_Basic_View where convert(varchar,year(CheckDate))='{0}' and CaseNo is not null and Business_theme=a.Value) as MolecularCount,
	  (select sum(isnull(AllDoesmeet,0)) from Check_Basic_View where convert(varchar,year(CheckDate))='{0}' and CaseNo is not null and Business_theme=a.Value) as DenominatorCount,
      case when isnull((select count(distinct CaseNo) from Check_Basic_View where convert(varchar,year(CheckDate))='{0}' and CaseNo is not null and Business_theme=a.Value),0)=0 then 0 
        else convert(decimal(6, 2),isnull((select sum(isnull(AllDoesmeet,0)) from Check_Basic_View where convert(varchar,year(CheckDate))='{0}' and CaseNo is not null and Business_theme=a.Value),0) * 1.0/isnull((select count(distinct CaseNo) from Check_Basic_View where convert(varchar,year(CheckDate))='{0}' and CaseNo is not null and Business_theme=a.Value),1)) end as Average
  FROM [dbo].[CarVehicleGas_BusinessOrganizationV] a
  where CaseType='CarFuel_BasicData'
  order by Rank ", workYear);

            DataTable dt = StatisticReportFunc.getDataTable(sql);

            return dt;
        }

        //計算講習會出席率
        private DataTable getListData(string workYear1, string workYear2, string workYear3, string workYear4)
        {
            string sql = "";
            string workSql1 = "";
            string workSql2 = "";
            string workSql3 = "";
            string workSql4 = "";

            if (workYear1 != "")
            {
                workSql1 = string.Format(@"
	  ,(select count(distinct s_CaseNo) from CounselingData where s_year='{0}' and s_CaseNo is not null and s_BusinessNo=a.Value) as AttendSCount1
	  ,(select sum(Total) from Gas_Total_TempV where CaseType='CarFuel_BasicData' and value=a.Value) as DenominatorCount1
      ,case when isnull((select sum(Total) from Gas_Total_TempV where CaseType='CarFuel_BasicData' and value=a.Value),0)=0 then 0 else convert(decimal(6, 2),isnull((select count(distinct s_CaseNo) from CounselingData where s_year='{0}' and s_CaseNo is not null and s_BusinessNo=a.Value),0) * 100.0/isnull((select sum(Total) from Gas_Total_TempV where CaseType='CarFuel_BasicData' and value=a.Value),1)) end as CounselingRate1
            ", workYear1);
            }
            else
            {
                workSql1 = string.Format(@"
	        ,0 as AttendSCount1
	        ,0 as DenominatorCount1
            ,0 as CounselingRate1
            ");
            }
            if (workYear2 != "")
            {
                workSql2 = string.Format(@"
	  ,(select count(distinct s_CaseNo) from CounselingData where s_year='{0}' and s_CaseNo is not null and s_BusinessNo=a.Value) as AttendSCount2
	  ,(select sum(Total) from Gas_Total_TempV where CaseType='CarFuel_BasicData' and value=a.Value) as DenominatorCount2
      ,case when isnull((select sum(Total) from Gas_Total_TempV where CaseType='CarFuel_BasicData' and value=a.Value),0)=0 then 0 else convert(decimal(6, 2),isnull((select count(distinct s_CaseNo) from CounselingData where s_year='{0}' and s_CaseNo is not null and s_BusinessNo=a.Value),0) * 100.0/isnull((select sum(Total) from Gas_Total_TempV where CaseType='CarFuel_BasicData' and value=a.Value),1)) end as CounselingRate2
            ", workYear2);
            }
            else
            {
                workSql2 = string.Format(@"
	        ,0 as AttendSCount2
	        ,0 as DenominatorCount2
            ,0 as CounselingRate2
            ");
            }
            if (workYear3 != "")
            {
                workSql3 = string.Format(@"
	        ,isnull((select count(distinct CaseNo) from Check_Basic_View where convert(varchar,year(CheckDate))='{0}' and CaseNo is not null and Business_theme=a.Value),0) as SumCheckCount1
	        ,isnull((select sum(isnull(AllDoesmeet,0)) from Check_Basic_View where convert(varchar,year(CheckDate))='{0}' and CaseNo is not null and Business_theme=a.Value),0) as SumCheckError1
            ,case when isnull((select count(distinct CaseNo) from Check_Basic_View where convert(varchar,year(CheckDate))='{0}' and CaseNo is not null and Business_theme=a.Value),0)=0 then 0 
                else convert(decimal(6, 2),isnull((select sum(isnull(AllDoesmeet,0)) from Check_Basic_View where convert(varchar,year(CheckDate))='{0}' and CaseNo is not null and Business_theme=a.Value),0) * 1.0/isnull((select count(distinct CaseNo) from Check_Basic_View where convert(varchar,year(CheckDate))='{0}' and CaseNo is not null and Business_theme=a.Value),1)) end as Average1
            ", workYear3);
            }
            else
            {
                workSql3 = string.Format(@"
	        ,0 as SumCheckCount1
	        ,0 as SumCheckError1
            ,0 as Average1
            ");
            }
            if (workYear4 != "")
            {
                workSql4 = string.Format(@"
	        ,isnull((select count(distinct CaseNo) from Check_Basic_View where convert(varchar,year(CheckDate))='{0}' and CaseNo is not null and Business_theme=a.Value),0) as SumCheckCount2
	        ,isnull((select sum(isnull(AllDoesmeet,0)) from Check_Basic_View where convert(varchar,year(CheckDate))='{0}' and CaseNo is not null and Business_theme=a.Value),0) as SumCheckError2
            ,case when isnull((select count(distinct CaseNo) from Check_Basic_View where convert(varchar,year(CheckDate))='{0}' and CaseNo is not null and Business_theme=a.Value),0)=0 then 0 
                else convert(decimal(6, 2),isnull((select sum(isnull(AllDoesmeet,0)) from Check_Basic_View where convert(varchar,year(CheckDate))='{0}' and CaseNo is not null and Business_theme=a.Value),0) * 1.0/isnull((select count(distinct CaseNo) from Check_Basic_View where convert(varchar,year(CheckDate))='{0}' and CaseNo is not null and Business_theme=a.Value),1)) end as Average2
            ", workYear4);
            }
            else
            {
                workSql4 = string.Format(@"
	        ,0 as SumCheckCount2
	        ,0 as SumCheckError2
            ,0 as Average2
            ");
            }

            sql += string.Format(@"
SELECT [CaseType]
      ,[Name]
      ,[Value]
      ,[Rank]
      ,[IsEnable]
      ,[ShortName] as workItem
      ,[OldCode]
      {0}
      {1}
      {2}
      {3}
  FROM [dbo].[CarVehicleGas_BusinessOrganizationV] a
  where CaseType='CarFuel_BasicData'
  order by Rank", workSql1, workSql2, workSql3, workSql4);

            DataTable dt = StatisticReportFunc.getDataTable(sql);

            return dt;
        }
    }

    public class ChartAuditCRM2
    {
        public string workItem { get; set; }
        public string Value { get; set; }
        public string Counseling_Year1 { get; set; }
        public string Counseling_Year2 { get; set; }
        public string CheckYear1 { get; set; }
        public string CheckYear2 { get; set; }
        public Decimal? Counseling_Year_Average1 { get; set; }
        public Decimal? Counseling_Year_Average2 { get; set; }
        public Decimal? CheckYear_Average1 { get; set; }
        public Decimal? CheckYear_Average2 { get; set; }
    }
}