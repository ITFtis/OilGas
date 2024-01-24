using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarGas
{
    [Dou.Misc.Attr.MenuDef(Id = "CarGas_BasicData_Log_Statistic", Name = "變更歷程-基本資料欄位統計", MenuPath = "加氣站/B統計報表專區", Action = "Index", Index = 4, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class CarGas_BasicData_Log_StatisticController : Controller
    {
        // GET: CarGas_BasicData_Log_Statistic
        public ActionResult Index()
        {
            return View();
        }
    }
}