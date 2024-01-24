using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarGas
{
    [Dou.Misc.Attr.MenuDef(Id = "CarGas_BasicData_Statistic", Name = "現況資料-基本資料欄位統計", MenuPath = "加氣站/B統計報表專區", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class CarGas_BasicData_StatisticController : Controller
    {
        // GET: CarGas_BasicData_Statistic
        public ActionResult Index()
        {
            return View();
        }
    }
}