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

namespace OilGas.Controllers.CarFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "CarFuel_CaseError1", Name = "系統最後發文與營運狀況不符合之清單", MenuPath = "加油站/A異常報表", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class CarFuel_CaseError1Controller : AGenericModelController<vw_CarFuel_CaseError1>
    {
        public static List<vw_CarFuel_CaseError1> _lsCFCE1 = new List<vw_CarFuel_CaseError1>();
        // GET: CarFuel_CaseError1
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

        public override DataManagerOptions GetDataManagerOptions()
        {
            var opts = base.GetDataManagerOptions();
            opts.editable = false;
            opts.deleteable = false;
            opts.addable = false;
            return opts;
        }

        protected override Dou.Models.DB.IModelEntity<OilGas.Models.vw_CarFuel_CaseError1> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<OilGas.Models.vw_CarFuel_CaseError1>(new OilGasModelContextExt());
        }

        protected override IEnumerable<vw_CarFuel_CaseError1> GetDataDBObject(IModelEntity<vw_CarFuel_CaseError1> dbEntity, params KeyValueParams[] paras)
        {
            var dbContext = new OilGasModelContextExt();
            //繫結城市
            Dou.Models.DB.IModelEntity<CityCode> AllCity = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
            var lsCity = AllCity.GetAll().ToList();
            string workCity = "";
            //string[] lswc = null; 
            //foreach (var item in lsCity)
            //{
            //    if (workCity == "")
            //    {
            //        workCity = item.GSLCode.ToString();
            //    }
            //    else
            //    {
            //        workCity += "," + item.GSLCode.ToString();
            //    }
            //}          
            //lswc = workCity.Split(',');
            var lsData = Rpt_CarFuel_CaseError1.GetAllvwCFCE1().ToList();

            //權限查詢
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysGSLs();
            _lsCFCE1 = lsData.Where(x => pCitys.Contains(x.CaseNo.Substring(4, 2))).ToList();
            return _lsCFCE1;
            //return base.GetDataDBObject(dbEntity, paras);
        }

        public ActionResult ExportCarFuel_CaseError1()
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.加油站_A異常報表_系統最後發文與營運狀況不符合之清單);
            string fileTitle = "加油站_系統最後發文與營運狀況不符合之清單)";

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
            DataTable dt = StatisticReportFunc.ConvertToDataTable(_lsCFCE1);

            string Title = string.Format(@"<tr>" +
                                       "  <td rowspan=\"2\" align=\"center\">項次</td>" +
                                       "  <td rowspan=\"2\" align=\"center\">案件編號</td>" +
                                       "  <td rowspan=\"2\" align=\"center\">石油設施</td>" +
                                       "  <td rowspan=\"2\" align=\"center\">營業主體</td>" +
                                       "  <td rowspan=\"2\" align=\"center\">石油設施地址</td>" +
                                       "  <td colspan=\"2\" align=\"center\">營運狀況資料</td>" +
                                       "  <td colspan=\"2\" align=\"center\">最後發文資料</td>" +
                                       "</tr>" +
                                       "<tr>" +
                                       "  <td>異動日期</td>" +
                                       "  <td>最後營運狀態</td>" +
                                       "  <td>異動發文資料</td>" +
                                       "  <td>最後發文狀況</td>" +
                                       "</tr>");

            ReportName = "系統最後發文與營運狀況不符合之清單";

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