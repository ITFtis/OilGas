using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.PortGas
{
    [Dou.Misc.Attr.MenuDef(Id = "PortGas_Ban_View", Name = "違規案件資料查詢修改", MenuPath = "航港自用加儲油/E違規案件專區", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class PortGas_Ban_ViewController : Controller
    {
        // GET: PortGas_Ban_View
        public ActionResult Index()
        {
            return View();
        }
    }
}