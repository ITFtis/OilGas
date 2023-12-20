using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.FishGas
{
    [Dou.Misc.Attr.MenuDef(Id = "FishGas_TGOS_Area", Name = "地圖環域查詢", MenuPath = "漁船加油站/C管理專區", Action = "Index", Index = 3, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class FishGas_TGOS_AreaController : Controller
    {
        // GET: FishGas_TGOS_Area
        public ActionResult Index()
        {
            return View();
        }
    }
}