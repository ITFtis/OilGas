using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_CounselingReportRate2", Name = "參加講習會的集團出席率統計", MenuPath = "查核輔導專區/G輔導講習專區", Action = "Index", Index = 3, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Audit_CounselingReportRate2Controller : Controller
    {
        // GET: Audit_CounselingReportRate2
        public ActionResult Index()
        {
            return View();
        }
    }
}