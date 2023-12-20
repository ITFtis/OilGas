using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarGas
{
    [Dou.Misc.Attr.MenuDef(Id = "CarGas_TGOS_Area", Name = "地圖環域查詢", MenuPath = "汽車加氣站/B管理專區", Action = "Index", Index = 3, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class CarGas_TGOS_AreaController : Controller
    {
        // GET: CarGas_TGOS_Area
        public ActionResult Index()
        {
            return View();
        }
    }
}