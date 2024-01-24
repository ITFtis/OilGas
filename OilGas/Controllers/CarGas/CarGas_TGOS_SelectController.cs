using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarGas
{
    [Dou.Misc.Attr.MenuDef(Id = "CarGas_TGOS_Select", Name = "汽車加氣站地圖資料查詢", MenuPath = "加氣站/B管理專區", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class CarGas_TGOS_SelectController : Controller
    {
        // GET: CarGas_TGOS_Select
        public ActionResult Index()
        {
            return View();
        }
    }
}