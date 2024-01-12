using Dou.Controllers;
using Dou.Models.DB;
using OilGas._report;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Admin
{
    [Dou.Misc.Attr.MenuDef(Id = "WS_GSM_Log", Name = "資料交換紀錄", MenuPath = "系統管理專區/H環保署地下儲槽系統資料交換專區", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class WS_GSM_LogController : APaginationModelController<WS_GSM_Log>
    {
        // GET: WS_GSM_Log
        public ActionResult Index()
        {
            return View();
        }

        protected override IQueryable<WS_GSM_Log> BeforeIQueryToPagedList(IQueryable<WS_GSM_Log> iquery, params KeyValueParams[] paras)
        {
            iquery = iquery.OrderByDescending(x => x.Sys_date);
            return base.BeforeIQueryToPagedList(iquery, paras);
        }

        protected override IModelEntity<WS_GSM_Log> GetModelEntity()
        {
            return new ModelEntity<WS_GSM_Log>(new OilGasModelContextExt());
        }

        //匯出excel
        public ActionResult ExportExcel()
        {

            Rpt_WS_GSM_Log rep = new Rpt_WS_GSM_Log();
            string url = rep.Export();

            if (url == "")
            {
                return Json(new { result = false, errorMessage = rep.ErrorMessage }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = true, url = url }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}