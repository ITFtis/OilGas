using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "CarFuel_TGOS_Select", Name = "加油站地圖資料查詢", MenuPath = "加油站/A管理專區", Action = "Index", Index = 3, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class CarFuel_TGOS_SelectController : Controller
    {
        // GET: CarFuel_TGOS_Select
        public ActionResult Index()
        {
            return View();
        }
    }
}