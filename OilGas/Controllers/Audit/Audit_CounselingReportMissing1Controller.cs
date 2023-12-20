using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_CounselingReportMissing1", Name = "各縣市出席率與查核缺失數比較圖", MenuPath = "查核輔導專區/G輔導講習專區", Action = "Index", Index = 4, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Audit_CounselingReportMissing1Controller : Controller
    {
        // GET: Audit_CounselingReportMissing1
        public ActionResult Index()
        {
            return View();
        }
    }
}