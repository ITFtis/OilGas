using Dou.Controllers;
using Dou.Help;
using Dou.Misc;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_Guidance_Check_Basic_AuditDay", Name = "加油站檢查表_每日", MenuPath = "查核輔導專區/G加油站業者", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Audit_Guidance_Check_Basic_AuditDayController : APaginationModelController<Check_Basic_AuditDay>
    {
        // GET: Audit_Guidance_Check_Basic_AuditDay
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditFormLayout()
        {
            return View();
        }

        protected override Dou.Models.DB.IModelEntity<Check_Basic_AuditDay> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<Check_Basic_AuditDay>(new OilGasModelContextExt());
        }

        public override DataManagerOptions GetDataManagerOptions()
        {
            var opts = base.GetDataManagerOptions();

            opts.editformLayoutUrl = new UrlHelper(this.ControllerContext.RequestContext).Action("EditFormLayout");

            return opts;
        }

        //匯出基本資料表
        public ActionResult ExportExcel(string CaseNo)
        {
            Rpt_Audit_Guidance_Check_Basic_AuditDay rep = new Rpt_Audit_Guidance_Check_Basic_AuditDay();
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