using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "CarFuel_DownLoad_Operate", Name = "加油站管理專區操作手冊", MenuPath = "加油站/A系統操作手冊", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class CarFuel_DownLoad_OperateController : Controller
    {
        // GET: CarFuel_DownLoad_Operate
        public ActionResult Index()
        {
            return View();
        }
    }
}