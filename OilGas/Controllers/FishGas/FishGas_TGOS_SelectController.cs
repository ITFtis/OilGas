using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.FishGas
{
    [Dou.Misc.Attr.MenuDef(Id = "FishGas_TGOS_Select", Name = "漁船加油站地圖資料查詢", MenuPath = "漁船加油站/C管理專區", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class FishGas_TGOS_SelectController : Controller
    {
        // GET: FishGas_TGOS_Select
        public ActionResult Index()
        {
            return View();
        }
    }
}