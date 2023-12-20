using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_CounselingReportAFMissing", Name = "參加講習會前後之查核缺失數比較", MenuPath = "查核輔導專區/G輔導講習專區", Action = "Index", Index = 6, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Audit_CounselingReportAFMissingController : Controller
    {
        // GET: Audit_CounselingReportAFMissing
        public ActionResult Index()
        {
            return View();
        }
    }
}