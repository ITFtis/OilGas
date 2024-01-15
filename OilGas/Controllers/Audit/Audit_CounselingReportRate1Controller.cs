﻿using Dou.Controllers;
using Dou.Models.DB;
using DouHelper;
using NPOI.SS.Formula.Functions;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_CounselingReportRate1", Name = "參加講習會的縣市出席率統計", MenuPath = "查核輔導專區/G輔導講習專區", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.Update, AllowAnonymous = false)]
    public class Audit_CounselingReportRate1Controller : AGenericModelController<Counseling_Rate_City>
    {
        static List<Counseling_Rate_City> _lsAuditCRR1 = new List<Counseling_Rate_City>();
        static string Counseling_Year = "";

        // GET: Audit_CounselingReportRate1
        public ActionResult Index()
        {
            return View();
        }
        protected override Dou.Models.DB.IModelEntity<OilGas.Models.Counseling_Rate_City> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<OilGas.Models.Counseling_Rate_City>(new OilGasModelContextExt());
        }

        protected override IEnumerable<Counseling_Rate_City> GetDataDBObject(IModelEntity<Counseling_Rate_City> dbEntity, params KeyValueParams[] paras)
        {
            //條件
            Counseling_Year = KeyValue.GetFilterParaValue(paras, "Counseling_Year");

            //進入頁面不顯示清單(未使用查詢)
            if (string.IsNullOrEmpty(Counseling_Year))
            {
                return new List<Counseling_Rate_City>().AsQueryable();
            }

            _lsAuditCRR1 = StatisticReportFunc.ConvertToList<Counseling_Rate_City>(getListData(Counseling_Year));
            return _lsAuditCRR1;
        }

        protected override void UpdateDBObject(IModelEntity<Counseling_Rate_City> dbEntity, IEnumerable<Counseling_Rate_City> objs)
        {
            if (CStrToInt16(objs.First().DenominatorCount.ToString()) != 0)
            {
                var APCount = String.IsNullOrEmpty(objs.First().AttendPCount.ToString()) ? 0 : objs.First().AttendPCount;
                var ASCount = String.IsNullOrEmpty(objs.First().AttendSCount.ToString()) ? 0 : objs.First().AttendSCount;
                var avg1 = ((decimal)APCount / (decimal)objs.First().DenominatorCount) * 100;
                var avg2 = ((decimal)ASCount / (decimal)objs.First().DenominatorCount) * 100;
                objs.First().Average1 = String.IsNullOrEmpty(objs.First().AttendPCount.ToString()) ? 0 :
                    Math.Round(avg1, 2);
                objs.First().Average2 = String.IsNullOrEmpty(objs.First().AttendSCount.ToString()) ? 0 :
                    Math.Round(avg2, 2);
            }
            else
            {
                objs.First().Average1 = 0;
                objs.First().Average2 = 0;
            }

            base.UpdateDBObject(dbEntity, objs);
        }

        private Int16 CStrToInt16(string workStr)
        {
            Int16 workDecimal = 0;

            try
            {
                workDecimal = Convert.ToInt16(workStr);
            }
            catch
            {
                workDecimal = 0;
            }
            return workDecimal;
        }

        //計算資料
        private DataTable getListData(string workYear)
        {
            string strSQL = "";
            strSQL = string.Format(@"select [workYear],
                                            [workItem],
                                            [CityCode],
                                            [Rank],
                                            [GSLCode],
                                            [AttendPCount],
                                            [AttendSCount],
                                            [DenominatorCount],
                                            [Average1],
                                            [Average2] 
                                     from Counseling_Rate_City where workYear='{0}'
                                     order by Rank", workYear);
            DataTable dt = StatisticReportFunc.getDataTable(strSQL);
            return dt;
        }

        //產出該年度的出席率資料
        private int setInsertData(string workYear)
        {
            int workCount;
            string strSQL = "";
            strSQL = string.Format(@"insert into Counseling_Rate_City (workYear,workItem,CityCode,Rank,GSLCode,AttendPCount,AttendSCount,DenominatorCount,Average1,Average2)
 SELECT 
      '{0}' as workYear
      ,[CityName] as workItem
      ,[CityCode]
      ,[Rank]
      ,[GSLCode],
	  (select count(*) from CounselingData where s_year='{0}' and s_CaseNo is not null and s_CityCode=a.CityCode) as AttendPCount,
	  (select count(distinct s_CaseNo) from CounselingData where s_year='{0}' and s_CaseNo is not null and s_CityCode=a.CityCode) as AttendSCount,
	  (select sum(Total) from Gas_Total_TempV where CaseType='CarFuel_BasicData' and area=a.CityCode ) as DenominatorCount,
      case when isnull((select sum(Total) from Gas_Total_TempV where CaseType='CarFuel_BasicData' and area=a.CityCode),0)=0 then 0 else convert(decimal(6, 2),isnull((select count(*) from CounselingData where s_year='{0}' and s_CaseNo is not null and s_CityCode=a.CityCode),0) * 100.0/isnull((select sum(Total) from Gas_Total_TempV where CaseType='CarFuel_BasicData' and area=a.CityCode),1)) end as Average1,
	  case when isnull((select sum(Total) from Gas_Total_TempV where CaseType='CarFuel_BasicData' and area=a.CityCode),0)=0 then 0 else convert(decimal(6, 2),isnull((select count(distinct s_CaseNo) from CounselingData where s_year='{0}' and s_CaseNo is not null and s_CityCode=a.CityCode),0) * 100.0/isnull((select sum(Total) from Gas_Total_TempV where CaseType='CarFuel_BasicData' and area=a.CityCode),1)) end as Average2
  FROM [dbo].[CityCode] a
  order by Rank", workYear);
            workCount = StatisticReportFunc.getDataTable(strSQL).Rows.Count;

            return workCount;
        }

        public ActionResult ExportAudit_CounselingReportRate1()
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G輔導講習會專區_參加講習會的縣市出席率統計);
            string fileTitle = "參加講習會的縣市出席率統計";

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
            string ReportName, QryString = "", Total = "";
            DataTable dt = StatisticReportFunc.ConvertToDataTable(_lsAuditCRR1);
            dt.Columns.Remove("Counseling_Year");
            dt.Columns.Remove("CityCode");
            dt.Columns.Remove("Rank");
            dt.Columns.Remove("GSLCode");
            string Title = string.Format(@"<tr>" +
                                       "  <td>年度</td>" +
                                       "  <td>縣市別</td>" +
                                       "  <td>出席人數</td>" +
                                       "  <td>出席站</td>" +
                                       "  <td>轄內加油站</td>" +
                                       "  <td>出席率(人)</td>" +
                                       "  <td>出席率(站)</td>" +
                                       "</tr>");

            ReportName = "參加講習會的縣市出席率統計";

            //欄位
            string[] arrField = new string[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                arrField[i] = dt.Columns[i].ColumnName;
            }

            return StatisticReportFunc.DownLoad_Excel(ref dt, ReportName, QryString, arrField, Title, Total, ReportName + DateTime.Now.ToString("yyyyMMddhhmmss"));
        }

        //重新計算(btn_Recalculate)
        public ActionResult Recalculate()
        {
            try
            {
                string sql = string.Format(@"update Counseling_Rate_City set 
AttendPCount = (select count(*) from CounselingData where s_year='{0}' and s_CaseNo is not null and s_CityCode=Counseling_Rate_City.CityCode),
AttendSCount = (select count(distinct s_CaseNo) from CounselingData where s_year='{0}' and s_CaseNo is not null and s_CityCode=Counseling_Rate_City.CityCode),
DenominatorCount= (select sum(Total) from Gas_Total_TempV where CaseType='CarFuel_BasicData' and area=Counseling_Rate_City.CityCode ),
Average1=case when isnull((select sum(Total) from Gas_Total_TempV where CaseType='CarFuel_BasicData' and area=Counseling_Rate_City.CityCode),0)=0 then 0 else convert(decimal(6, 2),isnull((select count(*) from CounselingData where s_year='{0}' and s_CaseNo is not null and s_CityCode=Counseling_Rate_City.CityCode),0) * 100.0/isnull((select sum(Total) from Gas_Total_TempV where CaseType='CarFuel_BasicData' and area=Counseling_Rate_City.CityCode),1)) end,
Average2=case when isnull((select sum(Total) from Gas_Total_TempV where CaseType='CarFuel_BasicData' and area=Counseling_Rate_City.CityCode),0)=0 then 0 else convert(decimal(6, 2),isnull((select count(distinct s_CaseNo) from CounselingData where s_year='{0}' and s_CaseNo is not null and s_CityCode=Counseling_Rate_City.CityCode),0) * 100.0/isnull((select sum(Total) from Gas_Total_TempV where CaseType='CarFuel_BasicData' and area=Counseling_Rate_City.CityCode),1)) end
            where workYear='{0}' ", Counseling_Year);
                using (var db = new OilGasModelContextExt())
                {
                    SqlParameter[] parameters = new SqlParameter[] { };
                    db.Database.ExecuteSqlCommand(sql, parameters);
                }
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