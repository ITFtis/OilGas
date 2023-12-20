using Dou.Controllers;
using Dou.Misc;
using Dou.Models.DB;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "CarFuel_CheckOilTypeByYear", Name = "各縣市已發照及營業中家數統計報表", MenuPath = "加油站/A統計報表專區", Action = "Index", Index = 10, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]    
    public class CarFuel_CheckOilTypeByYearController : AGenericModelController<vw_CarFuel_CheckOilTypeByYear>
    {
        // GET: CarFuel_CheckOilTypeByYear
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
        protected override IEnumerable<vw_CarFuel_CheckOilTypeByYear> GetDataDBObject(IModelEntity<vw_CarFuel_CheckOilTypeByYear> dbEntity, params KeyValueParams[] paras)
        {
            var result = base.GetDataDBObject(dbEntity, paras).OrderBy(x=>x.CityRank);
            return result;
        }
        protected override Dou.Models.DB.IModelEntity<OilGas.Models.vw_CarFuel_CheckOilTypeByYear> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<OilGas.Models.vw_CarFuel_CheckOilTypeByYear>(new OilGasModelContextExt());
        }

        /// <summary>
        /// 匯出EXCEL
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public ActionResult ExportCarFuel_CheckOilTypeByYear()
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.加油站_A統計報表專區_各縣市已發照及營業中家數統計報表);
            string fileTitle = "加油站_各縣市已發照及營業中家數統計報表";

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
            QryString = "";

            var dbContext = new OilGasModelContextExt();
            Dou.Models.DB.IModelEntity<vw_CarFuel_CheckOilTypeByYear> vw_CarFuel_CheckOilTypeByYear = 
                new Dou.Models.DB.ModelEntity<vw_CarFuel_CheckOilTypeByYear>(dbContext);
            var ls_vwCFCOT = vw_CarFuel_CheckOilTypeByYear.GetAll().OrderBy(x => x.CityRank).ToList();

            DataTable dt = StatisticReportFunc.ConvertToDataTable(ls_vwCFCOT);

            dt.Columns.Remove("CityRank");

            string Title = string.Format(@"<tr>" +
                                           "  <td>縣市別</td>" +
                                           "  <td>中油自營</td>" +
                                           "  <td>中油加盟</td>" +
                                           "  <td>台塑加盟</td>" +
                                           "</tr>");

            ReportName = "各縣市已發照及營業中家數統計報表";

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