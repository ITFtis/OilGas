using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.PortGas
{
    [Dou.Misc.Attr.MenuDef(Id = "PortGas_DownLoad_Operate", Name = "航港設施管理專區操作手冊", MenuPath = "航港自用加儲油/E系統操作手冊", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class PortGas_DownLoad_OperateController : Controller
    {
        // GET: PortGas_DownLoad_Operate
        public ActionResult Index()
        {
            return View();
        }
    }
}