using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.FishGas
{
    [Dou.Misc.Attr.MenuDef(Id = "FishGas_BasicData_Log_Statistic", Name = "變更歷程-基本資料欄位統計", MenuPath = "漁船加油站/C統計報表專區", Action = "Index", Index = 4, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class FishGas_BasicData_Log_StatisticController : Controller
    {
        // GET: FishGas_BasicData_Log_Statistic
        public ActionResult Index()
        {
            return View();
        }
    }
}