using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarGas
{
    [Dou.Misc.Attr.MenuDef(Id = "CarGas_DownLoad_Operate", Name = "加氣站管理專區操作手冊", MenuPath = "加氣站/B系統操作手冊", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class CarGas_DownLoad_OperateController : Controller
    {
        // GET: CarGas_DownLoad_Operate
        public ActionResult Index()
        {
            return View();
        }
    }
}