using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.SelfFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "SelfFuel_TGOS_Select", Name = "自用加儲氣設施地圖資料查詢", MenuPath = "自用加儲油/D管理專區", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]    
    public class SelfFuel_TGOS_SelectController : Controller
    {
        // GET: SelfFuel_TGOS_Select
        public ActionResult Index()
        {
            return View();
        }
    }
}