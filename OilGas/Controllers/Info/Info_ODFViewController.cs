using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Info
{
    [Dou.Misc.Attr.MenuDef(Id = "Info_ODFView", Name = "ODF說明", MenuPath = "資訊查詢/IODF相關文件", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Info_ODFViewController : Controller
    {
        // GET: Info_ODFView
        public ActionResult Index()
        {
            return View();
        }
    }
}