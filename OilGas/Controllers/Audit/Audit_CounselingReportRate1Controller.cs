using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_CounselingReportRate1", Name = "參加講習會的縣市出席率統計", MenuPath = "查核輔導專區/G輔導講習專區", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Audit_CounselingReportRate1Controller : Controller
    {
        // GET: Audit_CounselingReportRate1
        public ActionResult Index()
        {
            return View();
        }
    }
}