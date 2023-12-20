using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.SelfFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "SelfFuel_TGOS_Area", Name = "地圖環域查詢", MenuPath = "自用加儲油/D管理專區", Action = "Index", Index = 3, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]    
    public class SelfFuel_TGOS_AreaController : Controller
    {
        // GET: SelfFuel_TGOS_Area
        public ActionResult Index()
        {
            return View();
        }
    }
}