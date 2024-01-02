using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_Guidance_Check_Basic_AuditHalfYear", Name = "加油站檢查表_半年", MenuPath = "查核輔導專區/G加油站業者", Action = "Index", Index = 3, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Audit_Guidance_Check_Basic_AuditHalfYearController : Controller
    {
        // GET: Audit_Guidance_Check_Basic_AuditHalfYear
        public ActionResult Index()
        {
            return View();
        }
    }
}