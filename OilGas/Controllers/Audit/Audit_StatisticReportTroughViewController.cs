using Dou.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OilGas.Models;
using System.Web.Mvc;
using Dou.Misc;
using System.Dynamic;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Data.Entity;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_StatisticReportTroughView", Name = "儲槽總數差異勾稽報表", MenuPath = "查核輔導專區/G統計報表專區", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_StatisticReportTroughViewController : APaginationModelController<Check_Basic>
    {
        // GET: Audit_StatisticReportTroughView
        public ActionResult Index()
        {
            return View();
        }

        protected override Dou.Models.DB.IModelEntity<OilGas.Models.Check_Basic> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<OilGas.Models.Check_Basic>(new OilGasModelContextExt());
        }

        protected override IQueryable<OilGas.Models.Check_Basic> BeforeIQueryToPagedList(IQueryable<OilGas.Models.Check_Basic> iquery, params KeyValueParams[] paras)
        {
            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return null;
            }

            List<string> titles = new List<string>();

            //儲槽總數差異勾稽報表            
            iquery = SetConditions(iquery, ref titles, paras);


            KeyValueParams ksort = paras.FirstOrDefault((KeyValueParams s) => s.key == "sort");
            KeyValueParams korder = paras.FirstOrDefault((KeyValueParams s) => s.key == "order");

            //分頁排序
            if (ksort.value != null && korder.value != null)
            {
                if (ksort.value.ToString() == "Business_theme_FullName")
                {
                    string sort = ksort.value.ToString();
                    string order = korder.value.ToString();

                    if (order == "asc")
                    {
                        iquery = iquery.OrderBy(a => a.Business_theme);
                    }
                    else if (order == "desc")
                    {
                        iquery = iquery.OrderByDescending(a => a.Business_theme);
                    }
                }
            }

            return base.BeforeIQueryToPagedList(iquery, paras);
        }

        public override DataManagerOptions GetDataManagerOptions()
        {
            var opts = base.GetDataManagerOptions();

            //全部欄位排序
            foreach (var field in opts.fields)
            {
                field.sortable = true;
                field.visible = false;
                field.filter = false;
            }

            opts.GetFiled("CheckNo").visible = true;
            opts.GetFiled("CheckNo").title = "查核編號";
            opts.GetFiled("CheckDate").visible = true;
            opts.GetFiled("CheckDate").title = "查核日期";
            opts.GetFiled("Gas_Name").visible = true;
            opts.GetFiled("Gas_Name").title = "加油站名稱";
            opts.GetFiled("Business_theme_FullName").visible = true;
            opts.GetFiled("Business_theme_FullName").title = "營業主體";
            opts.GetFiled("Addr").visible = true;
            opts.GetFiled("Addr").title = "加油站地址";
            opts.GetFiled("TankCount").visible = true;
            opts.GetFiled("TankCount").title = "基本資料儲槽總數";
            opts.GetFiled("Tank_total").visible = true;
            opts.GetFiled("Tank_total").title = "現場查核儲槽總數";

            //查詢
            opts.GetFiled("CaseType").filter = true;            
            opts.GetFiled("Business_theme_FullName").filter = true;
            opts.GetFiled("CITY").filter = true;
            opts.GetFiled("CheckDate-Start-Between_").filter = true;
            opts.GetFiled("CheckDate-End-Between_").filter = true;

            return opts;
        }

        public ActionResult ExportAudit_StatisticReportTroughView(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.查核輔導專區_G統計報表專區_儲槽總數差異勾稽報表);
            string fileTitle = "查核輔導專區_儲槽總數差異勾稽報表";

            List<string> titles = new List<string>() { "查核輔導專區_儲槽總數差異勾稽報表，查詢條件:" };
            
            var iquery = GetModelEntity().GetAll();
            iquery = SetConditions(iquery, ref titles, paras);           

            //產出Dynamic資料 (給Excel)
            List<dynamic> list = new List<dynamic>();
            var output = iquery.ToList();
            foreach (var data in output)
            {
                dynamic f = new ExpandoObject();
                f.案件編號 = data.CaseNo;
                f.查核日期 = data.CheckDate;
                f.加油站名稱 = data.Gas_Name;
                f.營業主體 = data.Business_theme_FullName;
                f.加油站地址 = data.Addr;
                f.基本資料儲槽總數 = data.TankCount;
                f.現場查核儲槽總數 = data.Tank_total;

                f.SheetName = fileTitle;//sheep.名稱;
                list.Add(f);
            }

            //查無符合資料表數
            if (list.Count == 0)
            {
                return Json(new { result = false, errorMessage = "查無符合資料表數" }, JsonRequestBehavior.AllowGet);
            }

            //產出excel
            string fileName = OilGas.ExcelSpecHelper.GenerateExcelByLinqF1(fileTitle, titles, list, folder, "N");
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

        private IQueryable<OilGas.Models.Check_Basic> SetConditions(IQueryable<OilGas.Models.Check_Basic> iquery, ref List<string> titles,
                                                                    params KeyValueParams[] paras)
        {
            var CaseType = KeyValue.GetFilterParaValue(paras, "CaseType");
            var Business_theme_FullName = KeyValue.GetFilterParaValue(paras, "Business_theme_FullName");
            var CITY = KeyValue.GetFilterParaValue(paras, "CITY");
            var CheckDate_Start_Between_ = KeyValue.GetFilterParaValue(paras, "CheckDate-Start-Between_");
            var CheckDate_End_Between_ = KeyValue.GetFilterParaValue(paras, "CheckDate-End-Between_");
            ////var txt_Start_Disposal_date = KeyValue.GetFilterParaValue(paras, "txt_Start_Disposal_date");
            ////var txt_End_Disposal_date = KeyValue.GetFilterParaValue(paras, "txt_End_Disposal_date");

            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();

            //權限查詢
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysCodes();            
            iquery = iquery.Where(a => a.CheckNo != ""
                    && pCitys.Any(b => DbFunctions.Left(a.CheckNo, 1) == b));

            //條件
            if (!string.IsNullOrEmpty(CaseType))
            {
                iquery = iquery.Where(a => a.CaseType == CaseType);
                titles.Add("儲油槽:" + Code.GetCaseType().Where(a => a.Key == CaseType).FirstOrDefault().Value);
            }

            if (!string.IsNullOrEmpty(Business_theme_FullName))
            {
                Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> model = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);                
                iquery = iquery.Where(a => a.Business_theme == Business_theme_FullName);
                titles.Add("營業主體:" + model.GetAll().Where(a => a.Value == Business_theme_FullName).FirstOrDefault().Name);
            }

            if (!string.IsNullOrEmpty(CITY))
            {
                Dou.Models.DB.IModelEntity<CityCode> model = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
                List<string> sels = CITY.Split(',').ToList();

                //GSL Code轉CityCode(ex:03->F)
                Dou.Models.DB.IModelEntity<CityCode> cityCode = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
                var citys = cityCode.GetAll().Where(a => sels.Any(b => a.GSLCode.IndexOf(b) > -1));

                var codes = citys.Select(a => a.CityCode1).ToList();
                codes = Code.ConvertTwCity(codes);
                
                iquery = iquery.Where(a => a.CheckNo != ""
                    && codes.Any(b => a.CheckNo.Substring(0, 1) == b));

                //代碼-縣市別
                titles.Add("縣市:" + string.Join(",", citys.Select(a => a.CityName).ToList()));
            }

            if (!string.IsNullOrEmpty(CheckDate_Start_Between_))
            {
                DateTime date = DateTime.Parse(CheckDate_Start_Between_);
                iquery = iquery.Where(a => a.CheckDate >= date);
                titles.Add("查核日期(起):" + CheckDate_Start_Between_);
            }
            
            if (!string.IsNullOrEmpty(CheckDate_End_Between_))
            {
                DateTime date = DateTime.Parse(CheckDate_End_Between_);
                iquery = iquery.Where(a => a.CheckDate <= date);
                titles.Add("查核日期(迄):" + CheckDate_End_Between_);
            }

            return iquery;
        }
    }
}