using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.PortGas
{
    [Dou.Misc.Attr.MenuDef(Id = "PortGas_Check_View", Name = "查核輔導資料查詢修改", MenuPath = "航港自用加儲油/E查核輔導專區", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class PortGas_Check_ViewController : Controller
    {
        // GET: PortGas_Check_View
        public ActionResult Index()
        {
            return View();
        }
    }
}