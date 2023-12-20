using Dou.Controllers;
using Dou.Misc;
using Dou.Models.DB;
using NPOI.SS.Formula.Functions;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "CarFuel_CarVehicleGas_CheckError1", Name = "查核系統與油氣設施子系統連結異常之清單", MenuPath = "加油站/A異常報表", Action = "Index", Index = 4, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class CarFuel_CarVehicleGas_CheckError1Controller : AGenericModelController<vw_CarFuel_CarVehicleGas_CheckError1>
    {
        public static List<vw_CarFuel_CarVehicleGas_CheckError1> _lsCFCCE1 = new List<vw_CarFuel_CarVehicleGas_CheckError1>();
        // GET: CarFuel_CarVehicleGas_CheckError1
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

        protected override Dou.Models.DB.IModelEntity<OilGas.Models.vw_CarFuel_CarVehicleGas_CheckError1> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<OilGas.Models.vw_CarFuel_CarVehicleGas_CheckError1>(new OilGasModelContextExt());
        }

        protected override IEnumerable<vw_CarFuel_CarVehicleGas_CheckError1> GetDataDBObject(IModelEntity<vw_CarFuel_CarVehicleGas_CheckError1> dbEntity, params KeyValueParams[] paras)
        {
            basicController basic = new basicController();
            _lsCFCCE1 = dbEntity.GetAll().OrderBy(x => x.CheckNo).ToList();
            if (!Dou.Context.CurrentIsAdminUser && !basic.Permissions("admin"))
            {
                //權限查詢
                var pCitys = Dou.Context.CurrentUser<User>().PowerCitysGSLs();
                var q1 = _lsCFCCE1.Where(x => !string.IsNullOrEmpty(x.CaseNo));
                _lsCFCCE1 = q1.Where(x => pCitys.Contains(x.CaseNo.Substring(4, 2))).OrderBy(x => x.CheckNo).ToList();
            }              
            return _lsCFCCE1;
        }

        public ActionResult ExportCarFuel_CarVehicleGas_CheckError1()
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.加油站_A異常報表_查核系統與油氣設施子系統連結異常之清單);
            string fileTitle = "加油站_查核系統與油氣設施子系統連結異常之清單)";

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
            DataTable dt = StatisticReportFunc.ConvertToDataTable(_lsCFCCE1);
            dt.Columns.Remove("Case_UsageState");
            string Title = string.Format(@"<tr>" +
                                       "  <td rowspan=\"2\" align=\"center\">項次</td>" +
                                       "  <td rowspan=\"2\" align=\"center\">查核編號</td>" +
                                       "  <td rowspan=\"2\" align=\"center\">查核日期</td>" +
                                       "  <td rowspan=\"2\" align=\"center\">設施案號</td>" +
                                       "  <td colspan=\"3\" align=\"center\">查核系統資料</td>" +
                                       "  <td colspan=\"3\" align=\"center\">石油設施資料</td>" +
                                       "</tr>" +
                                       "<tr>" +
                                       "  <td>名稱</td>" +
                                       "  <td>營業主體</td>" +
                                       "  <td>地址</td>" +
                                       "  <td>名稱</td>" +
                                       "  <td>營業主體</td>" +
                                       "  <td>地址</td>" +
                                       "</tr>");

            ReportName = "查核系統與油氣設施子系統連結異常之清單";

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