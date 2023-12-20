using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_ReportCheckError1", Name = "查核系統與油氣設施子系統連結異常之清單", MenuPath = "查核輔導專區/G異常報表", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Audit_ReportCheckError1Controller : Controller
    {
        // GET: Audit_ReportCheckError1
        public ActionResult Index()
        {
            return View();
        }
    }
}