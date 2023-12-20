using Dou.Controllers;
using Dou.Misc;
using Dou.Models.DB;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace OilGas.Controllers.CarFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "CarFuel_GSM_Select", Name = "地下儲油槽列管狀況查詢", MenuPath = "加油站/A統計報表專區", Action = "Index", Index = 9, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]    
    public class CarFuel_GSM_SelectController : APaginationModelController<vw_CarFuel_GSM_Select>
    {
        public static List<vw_CarFuel_GSM_Select> _vwCFGS = new List<vw_CarFuel_GSM_Select>();
        List<string> _Date_Type = null;
        static string _CityCode = "";
        static string _CityName = "";

        // GET: CarFuel_GSM_Select
        public ActionResult Index()
        {
            if (!AppConfig.IsDev)
            {
                //非開發階段
                if (Dou.Context.CurrentUserBase == null)
                {
                    return Redirect("~/Home/Index");
                }
            }
            return View();
        }

        protected override IQueryable<vw_CarFuel_GSM_Select> BeforeIQueryToPagedList(IQueryable<vw_CarFuel_GSM_Select> iquery, params KeyValueParams[] paras)
        {
            Rpt_CarFuel_Land.ResetGetGSLCodeByCityCode();
            var _tmp_Date_Type = HelperUtilities.GetFilterParaValue(paras, "CaseType");
            _Date_Type = _tmp_Date_Type != null ? HelperUtilities.GetFilterParaValue(paras, "CaseType").Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).ToList() : null;
            _CityCode = HelperUtilities.GetFilterParaValue(paras, "CITY");
            _CityName = _CityCode != null ? Rpt_CarFuel_Land.GetGSLCodeByCityCode(_CityCode).First().CityName.ToString() : "";

            if (string.IsNullOrEmpty(_CityName) && _Date_Type == null)
                return null;

            _vwCFGS = StatisticReportFunc.ConvertToList<vw_CarFuel_GSM_Select>(getListData());

            foreach (var item in _vwCFGS)
            {
                string strlog = "";
                strlog += !string.IsNullOrEmpty(item.Limit_Date.ToString()) ? "公告細八：" + String.Format("{0:yyyy/MM/dd}", item.Limit_Date) + "<br>" : "";
                strlog += !string.IsNullOrEmpty(item.take_Date.ToString()) ? "公告七條五：" + String.Format("{0:yyyy/MM/dd}", item.take_Date) + "<br>" : "";
                strlog += !string.IsNullOrEmpty(item.GW_Date.ToString()) ? "公告地下水：" + String.Format("{0:yyyy/MM/dd}", item.GW_Date) + "<br>" : "";
                strlog += !string.IsNullOrEmpty(item.Control_Date.ToString()) ? "公告控制場址：" + String.Format("{0:yyyy/MM/dd}", item.Control_Date) + "<br>" : "";
                strlog += !string.IsNullOrEmpty(item.Rem_Date.ToString()) ? "公告整治場址：" + String.Format("{0:yyyy/MM/dd}", item.Rem_Date) : "";
                item.LTGCR_Date = strlog;
            }

            return base.BeforeIQueryToPagedList(_vwCFGS.AsQueryable(), paras);
        }

        public override DataManagerOptions GetDataManagerOptions()
        {
            var opts = base.GetDataManagerOptions();
            opts.editable = false;
            opts.deleteable = false;
            opts.addable = false;
            return opts;
        }

        protected override IModelEntity<vw_CarFuel_GSM_Select> GetModelEntity()
        {
            return new ModelEntity<vw_CarFuel_GSM_Select>(new OilGasModelContextExt());
        }

        private DataTable getListData()
        {
            string strWhere = "";

            if (_Date_Type != null)
            {
                strWhere += " and ( 1=0 ";
                foreach (var item in _Date_Type)
                {
                    switch (item)
                    {
                        case "0":
                            strWhere += " or (isnull(Limit_Date,'') <> '') ";
                            break;
                        case "1":
                            strWhere += " or (isnull(take_Date,'') <> '') ";
                            break;
                        case "2":
                            strWhere += " or (isnull(GW_Date,'') <> '') ";
                            break;
                        case "3":
                            strWhere += " or (isnull(Control_Date,'') <> '') ";
                            break;
                        case "4":
                            strWhere += " or (isnull(Rem_Date,'') <> '') ";
                            break;
                    }
                }
                strWhere += " ) ";
            }
            if (!string.IsNullOrEmpty(_CityName))
            {
                strWhere += "and gsm_field03 like '" + _CityName + "%'";
            }

            string strSQL = string.Format(@"
        Select r.[CaseNo]  
            , [gsm_id]
            , [gsm_name]
            , [gsm_field03]
            , [gsm_field07]
            , [gsm_register]
            , [gsm_field31]
            , [gsm_field30]
            , [Situation]
            , [Limit_Date]
            , [take_Date]
            , [GW_Date]
            , [Control_Date]
            , [Rem_Date]
            , COALESCE(Limit_Date,take_Date,GW_Date,Control_Date,Rem_Date) [Post_Date]
            , Case When Situation like '%解除%' Then [Situation_Date] Else NULL End [Situation_Date]
        From WS_GSM w with(nolock)
        Left Join WS_GSM_Relation r with(nolock) On r.FacNo like '%'+w.gsm_id+'%'
        Left Join CarFuel_BasicData g with(nolock) On r.CaseNo=g.CaseNo
        Where 1=1 {0} order by w.gsm_id ", strWhere);

            DataTable dt = StatisticReportFunc.getDataTable(strSQL);
            return dt;
        }
    }
}