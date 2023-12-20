using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Info
{
    [Dou.Misc.Attr.MenuDef(Id = "Info_EnergyLaw", Name = "最新能源法規", MenuPath = "資訊查詢/I最新能源法規", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Info_EnergyLawController : Controller
    {
        // GET: Info_EnergyLaw
        public ActionResult Index()
        {

            Response.Redirect("https://www.moeaea.gov.tw/ECW/populace/content/SubMenu.aspx?menu_id=220");
            return View();
        }
    }
}