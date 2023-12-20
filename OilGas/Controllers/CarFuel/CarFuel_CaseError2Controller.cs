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
using System.Xml.Linq;

namespace OilGas.Controllers.CarFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "CarFuel_CaseError2", Name = "營運主體分類異常之清單", MenuPath = "加油站/A異常報表", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class CarFuel_CaseError2Controller : AGenericModelController<CarFuel_CaseError2>
    {
        public static List<CarFuel_CaseError2> _lsCFCE2 = new List<CarFuel_CaseError2>();
        // GET: CarFuel_CaseError2
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

        protected override Dou.Models.DB.IModelEntity<OilGas.Models.CarFuel_CaseError2> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<OilGas.Models.CarFuel_CaseError2>(new OilGasModelContextExt());
        }

        protected override IEnumerable<CarFuel_CaseError2> GetDataDBObject(IModelEntity<CarFuel_CaseError2> dbEntity, params KeyValueParams[] paras)
        {
            _lsCFCE2 = StatisticReportFunc.ConvertToList<CarFuel_CaseError2>(getData());

            //權限查詢
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysGSLs();
            _lsCFCE2 = _lsCFCE2.Where(x => pCitys.Contains(x.CaseNo.Substring(4, 2))).ToList();

            return _lsCFCE2;
            //return base.GetDataDBObject(dbEntity, paras);
        }

        private DataTable getData()
        {
            string strSQL = "";
            string whereStr = " ";
            string workCity = "";

            //繫結城市
            var dbContext = new OilGasModelContextExt();
            Dou.Models.DB.IModelEntity<CityCode> AllCity = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);

            var lsCity = AllCity.GetAll().ToList();

            foreach(var item in lsCity)
            {
                if (workCity == "")
                {
                    workCity = item.GSLCode.ToString();
                }
                else
                {
                    workCity += "," + item.GSLCode.ToString();
                }
            }

            whereStr += string.Format(" and RIGHT(LEFT(a.CaseNo,6),2) in ({0})", workCity);

            strSQL = string.Format(@"
select ROW_NUMBER() OVER(ORDER BY CaseNo,ID) as CaseIndex
      ,[CaseNo]
      ,[Gas_Name]
      ,[Address]
	  , b.Name  as Business_theme
      , Case when b.Name <> '其他' then b.Name else a.otherBusiness_theme End as Other_Business_theme
      , Case when isnull(a.Mod_date,'') = '' then a.Create_date else a.Mod_date end as Mod_date
      ,[UsageState]
      ,(select Name from UsageStateCode where Value = a.UsageState) as UsageStateName
FROM [GSL].[dbo].[CarFuel_BasicData] a 
left join CarVehicleGas_BusinessOrganization b with(nolock) on b.Value = a.Business_theme
where b.Name = '其他' and a.otherBusiness_theme in (select Name from CarVehicleGas_BusinessOrganization) {0} "
            , whereStr);

            DataTable dt = StatisticReportFunc.getDataTable(strSQL);
            return dt;
        }

        public ActionResult ExportCarFuel_CaseError2()
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.加油站_A異常報表_營運主體分類異常之清單);
            string fileTitle = "加油站_營運主體分類異常之清單)";

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
            DataTable dt = StatisticReportFunc.ConvertToDataTable(_lsCFCE2);

            dt.Columns.Remove("UsageState");

            string Title = string.Format(@"<tr>" +
                                       "  <td>項次</td>" +
                                       "  <td>案件編號</td>" +
                                       "  <td>石油設施</td>" +
                                       "  <td>營業主體集團</td>" +
                                       "  <td>營業主體名稱</td>" +
                                       "  <td>石油設施地址</td>" +
                                       "  <td>異動日期</td>" +
                                       "  <td>最後營運狀態</td>" +
                                       "</tr>");

            ReportName = "營運主體分類異常之清單";

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