using Dou.Controllers;
using Dou.Misc;
using Dou.Models.DB;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using System.Dynamic;
using NPOI.SS.Formula.Functions;
using System.Web.Services.Description;
using System.Data;

namespace OilGas.Controllers.GasLawBan
{
    [Dou.Misc.Attr.MenuDef(Id = "GasLawBan_Select", Name = "石油管理法取締案件資料查詢", MenuPath = "取締管理作業/F違規案件維護", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class GasLawBan_SelectController : APaginationModelController<OilGas.Models.GasLawBan>
    {
        // GET: GasLawBan_Select
        public ActionResult Index()
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();

            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysGSLs();

            //代碼-縣市別
            Dou.Models.DB.IModelEntity<CityCode> cityCode = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
            ViewBag.cityCode = cityCode.GetAll().Where(a => pCitys.Any(b => b == a.GSLCode))
                                        .OrderBy(a => a.Rank).ToList();

            return View();
        }
        
        protected override Dou.Models.DB.IModelEntity<OilGas.Models.GasLawBan> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<OilGas.Models.GasLawBan>(new OilGasModelContextExt());
        }

        protected override IQueryable<OilGas.Models.GasLawBan> BeforeIQueryToPagedList(IQueryable<OilGas.Models.GasLawBan> iquery, params KeyValueParams[] paras)
        {
            List<string> titles = new List<string>();
            iquery = SetConditions(iquery, ref titles, paras);

            //虛擬欄位排序
            KeyValueParams ksort = paras.FirstOrDefault((KeyValueParams s) => s.key == "sort");
            KeyValueParams korder = paras.FirstOrDefault((KeyValueParams s) => s.key == "order");

            //分頁排序
            if (ksort.value != null && korder.value != null)
            {
                if (ksort.value.ToString() == "vs_Seized_date")
                {
                    string sort = ksort.value.ToString();
                    string order = korder.value.ToString();

                    if (order == "asc")
                    {
                        iquery = iquery.OrderBy(a => a.Seized_date);
                    }
                    else if (order == "desc")
                    {
                        iquery = iquery.OrderByDescending(a => a.Seized_date);
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
            }

            opts.GetFiled("CaseNo").visible = true;
            opts.GetFiled("CaseNo").title = "案件編號";
            opts.GetFiled("Name").visible = true;
            opts.GetFiled("Name").title = "受處分人_公司名稱";
            opts.GetFiled("Investigation_unit").visible = true;
            opts.GetFiled("Investigation_unit").title = "查緝單位";
            opts.GetFiled("Organizers").visible = true;
            opts.GetFiled("Organizers").title = "主辦單位";
            opts.GetFiled("vs_Seized_date").visible = true;
            opts.GetFiled("vs_Seized_date").title = "查獲日期";
            opts.GetFiled("Fine").visible = true;
            opts.GetFiled("Fine").title = "罰款金額";

            //查詢            
            opts.GetFiled("CaseNo").filter = true;

            return opts;
        }

        public ActionResult ExportGasLawBan_Select(params KeyValueParams[] paras)
        {            
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.取締管理作業_F違規案件維護_石油管理法取締案件資料查詢);
            string fileTitle = "取締管理作業_石油管理法取締案件資料查詢";

            List<string> titles = new List<string>() { "取締管理作業_石油管理法取締案件資料查詢，查詢條件:" };

            var iquery = GetModelEntity().GetAll();
            iquery = SetConditions(iquery, ref titles, paras);

            //產出Dynamic資料 (給Excel)
            List<dynamic> list = new List<dynamic>();
            var output = iquery.ToList();
            foreach (var data in output)
            {
                dynamic f = new ExpandoObject();
                f.案件編號 = data.CaseNo;
                f.受處分人_公司名稱 = data.Name;
                f.查緝單位 = data.Investigation_unit;
                f.主辦單位 = data.Organizers;
                f.查獲日期 = data.Seized_date;
                f.罰款金額 = data.Fine;

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

        private IQueryable<OilGas.Models.GasLawBan> SetConditions(IQueryable<OilGas.Models.GasLawBan> iquery, ref List<string> titles, 
                                                                    params KeyValueParams[] paras)
        {
            var chk_City = KeyValue.GetFilterParaValue(paras, "chk_City");
            var txt_CaseNo = KeyValue.GetFilterParaValue(paras, "txt_CaseNo");
            var txt_Name = KeyValue.GetFilterParaValue(paras, "txt_Name");                        
            var txt_Start_Seized_date = KeyValue.GetFilterParaValue(paras, "txt_Start_Seized_date");
            var txt_End_Seized_date = KeyValue.GetFilterParaValue(paras, "txt_End_Seized_date");
            var txt_Start_Disposal_date = KeyValue.GetFilterParaValue(paras, "txt_Start_Disposal_date");
            var txt_End_Disposal_date = KeyValue.GetFilterParaValue(paras, "txt_End_Disposal_date");

            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();

            //權限查詢
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysGSLs();
            iquery = iquery.Where(a => pCitys.Any(b => a.CaseNo.Substring(5, 2) == b));

            if (chk_City != null)
            {
                List<string> sels = chk_City.Split(',').ToList();
                iquery = iquery.Where(a => sels.Any(b => a.CaseNo.Substring(5, 2) == b));

                //代碼-縣市別
                Dou.Models.DB.IModelEntity<CityCode> cityCode = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
                var citys = cityCode.GetAll().Where(a => sels.Any(b => a.GSLCode.IndexOf(b) > -1))
                                            .Select(a => a.CityName).ToList();
                titles.Add("縣市:" + string.Join(",", citys));
            }

            if (txt_CaseNo != null)
            {
                iquery = iquery.Where(a => a.CaseNo.IndexOf(txt_CaseNo) != -1);
                titles.Add("案件編號:" + txt_CaseNo);
            }

            if (txt_Name != null)
            {
                iquery = iquery.Where(a => a.Name.IndexOf(txt_Name) != -1);
                titles.Add("受處分人/公司名稱:" + txt_Name);
            }            

            if (txt_Start_Seized_date != null)
            {
                DateTime date = DateTime.Parse(txt_Start_Seized_date);
                iquery = iquery.Where(a => a.Seized_date >= date);
                titles.Add("查獲日期起:" + DateFormat.ToDate4(date));
            }

            if (txt_End_Seized_date != null)
            {
                DateTime date = DateTime.Parse(txt_End_Seized_date);
                iquery = iquery.Where(a => a.Seized_date <= date);
                titles.Add("查獲日期迄:" + DateFormat.ToDate4(date));
            }

            if (txt_Start_Disposal_date != null)
            {
                DateTime date = DateTime.Parse(txt_Start_Disposal_date);
                iquery = iquery.Where(a => a.Disposal_date >= date);
                titles.Add("處分日期起:" + DateFormat.ToDate4(date));
            }

            if (txt_End_Disposal_date != null)
            {
                DateTime date = DateTime.Parse(txt_End_Disposal_date);
                iquery = iquery.Where(a => a.Disposal_date <= date);
                titles.Add("處分日期迄:" + DateFormat.ToDate4(date));
            }

            return iquery;
        }
    }
}