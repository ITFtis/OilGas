using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.SelfFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "SelfFuel_SelfFuelBanView", Name = "違規案件資料查詢修改", MenuPath = "自用加儲油/D違規案件專區", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]    
    public class SelfFuel_SelfFuelBanViewController : Controller
    {
        // GET: SelfFuel_SelfFuelBanView
        public ActionResult Index()
        {
            return View();
        }
    }
}