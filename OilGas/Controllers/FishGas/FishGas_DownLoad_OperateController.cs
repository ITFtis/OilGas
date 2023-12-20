using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.FishGas
{
    [Dou.Misc.Attr.MenuDef(Id = "FishGas_DownLoad_Operate", Name = "漁船加油站管理專區操作手冊", MenuPath = "漁船加油站/C系統操作手冊", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class FishGas_DownLoad_OperateController : Controller
    {
        // GET: FishGas_DownLoad_Operate
        public ActionResult Index()
        {
            return View();
        }
    }
}