using Dou.Controllers;
using Dou.Misc;
using Dou.Models.DB;
using OilGas._report;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_Guidance_Check_Basic_NoTime", Name = "自評填報系統", MenuPath = "查核輔導專區/G加油站業者", Action = "Index", Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Audit_Guidance_Check_Basic_NoTimeController : AGenericModelController<Check_Basic_NoTime>
    {
        // GET: Audit_Guidance_Check_Basic_NoTime
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditFormLayout()
        {
            return View();
        }

        public override DataManagerOptions GetDataManagerOptions()
        {
            var opts = base.GetDataManagerOptions();
            opts.editformLayoutUrl = new UrlHelper(ControllerContext.RequestContext).Action("EditFormLayout");
            return opts;
        }

        protected override IModelEntity<Check_Basic_NoTime> GetModelEntity()
        {
            return new ModelEntity<Check_Basic_NoTime>(new OilGasModelContextExt());
        }

        //匯出資料表
        public ActionResult ExportExcel(string CaseNo)
        {
            Rpt_Audit_Guidance_Check_Basic_Audit_NoTime rep = new Rpt_Audit_Guidance_Check_Basic_Audit_NoTime();
            string url = rep.Export(CaseNo);

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