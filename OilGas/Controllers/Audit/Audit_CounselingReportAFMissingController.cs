using Dou.Controllers;
using Dou.Misc;
using Dou.Models.DB;
using DouHelper;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_CounselingReportAFMissing", Name = "參加講習會前後之查核缺失數比較", MenuPath = "查核輔導專區/G輔導講習專區", Action = "Index", Index = 6, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_CounselingReportAFMissingController : AGenericModelController<Audit_CounselingReportAFMissing>
    {
        static List<Audit_CounselingReportAFMissing> _lsAuditCRAFM = new List<Audit_CounselingReportAFMissing>();
        static string Business_theme = "";
        static string CityCode1 = "";

        // GET: Audit_CounselingReportAFMissing
        public ActionResult Index()
        {
            return View();
        }

        protected override Dou.Models.DB.IModelEntity<OilGas.Models.Audit_CounselingReportAFMissing> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<OilGas.Models.Audit_CounselingReportAFMissing>(new OilGasModelContextExt());
        }

        protected override IEnumerable<Audit_CounselingReportAFMissing> GetDataDBObject(IModelEntity<Audit_CounselingReportAFMissing> dbEntity, params KeyValueParams[] paras)
        {
            //條件
            Business_theme = KeyValue.GetFilterParaValue(paras, "Business_theme");
            CityCode1 = KeyValue.GetFilterParaValue(paras, "CityCode1");
            //進入頁面不顯示清單(未使用查詢)
            if (string.IsNullOrEmpty(Business_theme) && string.IsNullOrEmpty(CityCode1))
            {
                return new List<Audit_CounselingReportAFMissing>().AsQueryable();
            }
            Business_theme = Business_theme.Replace("_CarFuel_BasicData", "");
            _lsAuditCRAFM = StatisticReportFunc.ConvertToList<Audit_CounselingReportAFMissing>(getListData());

            return _lsAuditCRAFM;
        }

        private Decimal CStrToDecimal(string workStr)
        {
            Decimal workDecimal = 0;

            try
            {
                workDecimal = Convert.ToDecimal(workStr);
            }
            catch
            {
                workDecimal = 0;
            }
            return workDecimal;
        }

        //計算資料
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
            //if (CaseType.SelectedValue != "")
            //{
            //    sqlWhere += string.Format(" and isnull(CaseType,'CarFuel_BasicData') = '{0}' ", CaseType.SelectedValue);
            //}

            if (!string.IsNullOrEmpty(sqlWhere))
                sqlWhere = SQLHelper.ToWhr(sqlWhere);
            #endregion

            #region<執行SQL>
            sql = string.Format(@"
select 
isnull((select SUM(A_CheckCount) from Check_Basic_AFN where A_year is not null and CaseNo in (select CaseNo from Check_Basic_View {0} )),0) as Sum_A_CheckCount
,isnull((select SUM(A_CheckDoesmeet) from Check_Basic_AFN where A_year is not null and CaseNo in (select CaseNo from Check_Basic_View {0} )),0) as Sum_A_CheckDoesmeet
,'-' as Sum_A_AvgDoesmeet
,isnull((select SUM(F_CheckCount) from Check_Basic_AFN where A_year is not null and CaseNo in (select CaseNo from Check_Basic_View {0} )),0) as Sum_F_CheckCount
,isnull((select SUM(F_CheckDoesmeet) from Check_Basic_AFN where A_year is not null and CaseNo in (select CaseNo from Check_Basic_View {0} )),0) as Sum_F_CheckDoesmeet
,'-' as Sum_F_AvgDoesmeet
,isnull((select SUM(N_CheckCount) from Check_Basic_AFN where A_year is null and CaseNo in (select CaseNo from Check_Basic_View {0} )),0) as Sum_N_CheckCount
,isnull((select SUM(N_CheckDoesmeet) from Check_Basic_AFN where A_year is null and CaseNo in (select CaseNo from Check_Basic_View {0} )),0) as Sum_N_CheckDoesmeet               
,'-' as Sum_N_AvgDoesmeet
", sqlWhere);

            DataTable dtResult = new DataTable();
            DataTable dt = StatisticReportFunc.getDataTable(sql);;

            dtResult.Columns.Add(new DataColumn("Sum_A_CheckCount", typeof(Int32)));
            dtResult.Columns.Add(new DataColumn("Sum_A_CheckDoesmeet", typeof(Int32)));
            dtResult.Columns.Add(new DataColumn("Sum_A_AvgDoesmeet", typeof(string)));
            dtResult.Columns.Add(new DataColumn("Sum_F_CheckCount", typeof(Int32)));
            dtResult.Columns.Add(new DataColumn("Sum_F_CheckDoesmeet", typeof(Int32)));
            dtResult.Columns.Add(new DataColumn("Sum_F_AvgDoesmeet", typeof(string)));
            dtResult.Columns.Add(new DataColumn("Sum_N_CheckCount", typeof(Int32)));
            dtResult.Columns.Add(new DataColumn("Sum_N_CheckDoesmeet", typeof(Int32)));
            dtResult.Columns.Add(new DataColumn("Sum_N_AvgDoesmeet", typeof(string)));

            foreach (DataRow row in dt.Rows)
            {
                Decimal Sum_A_CheckCount = 0;
                Decimal Sum_A_CheckDoesmeet = 0;
                Decimal Sum_A_AvgDoesmeet = 0;
                Decimal Sum_F_CheckCount = 0;
                Decimal Sum_F_CheckDoesmeet = 0;
                Decimal Sum_F_AvgDoesmeet = 0;
                Decimal Sum_N_CheckCount = 0;
                Decimal Sum_N_CheckDoesmeet = 0;
                Decimal Sum_N_AvgDoesmeet = 0;

                DataRow rowNew = dtResult.NewRow();
                Sum_A_CheckCount = CStrToDecimal(row["Sum_A_CheckCount"].ToString());
                Sum_A_CheckDoesmeet = CStrToDecimal(row["Sum_A_CheckDoesmeet"].ToString());

                if (Sum_A_CheckCount > 0)
                {
                    Sum_A_AvgDoesmeet = Math.Round(Sum_A_CheckDoesmeet * 1 / Sum_A_CheckCount, 2);
                }
                rowNew["Sum_A_CheckCount"] = Sum_A_CheckCount;
                rowNew["Sum_A_CheckDoesmeet"] = Sum_A_CheckDoesmeet;
                int ss = Sum_A_AvgDoesmeet.ToString().Trim().Length;
                rowNew["Sum_A_AvgDoesmeet"] = Sum_A_AvgDoesmeet.ToString().Trim();

                Sum_F_CheckCount = CStrToDecimal(row["Sum_F_CheckCount"].ToString());
                Sum_F_CheckDoesmeet = CStrToDecimal(row["Sum_F_CheckDoesmeet"].ToString());

                if (Sum_F_CheckCount > 0)
                {
                    Sum_F_AvgDoesmeet = Math.Round(Sum_F_CheckDoesmeet * 1 / Sum_F_CheckCount, 2);
                }
                rowNew["Sum_F_CheckCount"] = Sum_F_CheckCount;
                rowNew["Sum_F_CheckDoesmeet"] = Sum_F_CheckDoesmeet;
                rowNew["Sum_F_AvgDoesmeet"] = Sum_F_AvgDoesmeet.ToString().Trim();

                Sum_N_CheckCount = CStrToDecimal(row["Sum_N_CheckCount"].ToString());
                Sum_N_CheckDoesmeet = CStrToDecimal(row["Sum_N_CheckDoesmeet"].ToString());

                if (Sum_N_CheckCount > 0)
                {
                    Sum_N_AvgDoesmeet = Math.Round(Sum_N_CheckDoesmeet * 1 / Sum_N_CheckCount, 2);
                }
                rowNew["Sum_N_CheckCount"] = Sum_N_CheckCount;
                rowNew["Sum_N_CheckDoesmeet"] = Sum_N_CheckDoesmeet;
                rowNew["Sum_N_AvgDoesmeet"] = Sum_N_AvgDoesmeet.ToString().Trim();

                dtResult.Rows.Add(rowNew);
            }

            return dtResult;

            //for (int row = 0; row < dt.Rows.Count; row++)
            //{
            //    Decimal Sum_A_CheckCount = 0;
            //    Decimal Sum_A_CheckDoesmeet = 0;
            //    Decimal Sum_A_AvgDoesmeet = 0;
            //    Decimal Sum_F_CheckCount = 0;
            //    Decimal Sum_F_CheckDoesmeet = 0;
            //    Decimal Sum_F_AvgDoesmeet = 0;
            //    Decimal Sum_N_CheckCount = 0;
            //    Decimal Sum_N_CheckDoesmeet = 0;
            //    Decimal Sum_N_AvgDoesmeet = 0;

            //    Sum_A_CheckCount = CStrToDecimal(dt.Rows[row]["Sum_A_CheckCount"].ToString());
            //    Sum_A_CheckDoesmeet = CStrToDecimal(dt.Rows[row]["Sum_A_CheckDoesmeet"].ToString());

            //    if (Sum_A_CheckCount > 0)
            //    {
            //        Sum_A_AvgDoesmeet = Math.Round(Sum_A_CheckDoesmeet * 1 / Sum_A_CheckCount, 2);
            //    }
 
            //    dt.Rows[row]["Sum_A_AvgDoesmeet"] = Sum_A_AvgDoesmeet.ToString();

            //    Sum_F_CheckCount = CStrToDecimal(dt.Rows[row]["Sum_F_CheckCount"].ToString());
            //    Sum_F_CheckDoesmeet = CStrToDecimal(dt.Rows[row]["Sum_F_CheckDoesmeet"].ToString());
            //    if (Sum_F_CheckCount > 0)
            //    {
            //        Sum_F_AvgDoesmeet = Math.Round(Sum_F_CheckDoesmeet * 1 / Sum_F_CheckCount, 2);
            //    }
            //    dt.Rows[row]["Sum_F_AvgDoesmeet"] = Sum_F_AvgDoesmeet.ToString();

            //    Sum_N_CheckCount = CStrToDecimal(dt.Rows[row]["Sum_N_CheckCount"].ToString());
            //    Sum_N_CheckDoesmeet = CStrToDecimal(dt.Rows[row]["Sum_N_CheckDoesmeet"].ToString());
            //    if (Sum_N_CheckCount > 0)
            //    {
            //        Sum_N_AvgDoesmeet = Math.Round(Sum_N_CheckDoesmeet * 1 / Sum_N_CheckCount, 2);
            //    }
            //    dt.Rows[row]["Sum_N_AvgDoesmeet"] = Sum_N_AvgDoesmeet.ToString();
            //}
            #endregion

            //return dt;
        }
    }
}