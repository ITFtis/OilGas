using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "CarFuel_Update", Name = "負責人批次變更", MenuPath = "加油站/A管理專區", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class CarFuel_UpdateController : Controller
    {
        // GET: CarFuel_Update
        public ActionResult Index()
        {
            return View();
        }
    }
}