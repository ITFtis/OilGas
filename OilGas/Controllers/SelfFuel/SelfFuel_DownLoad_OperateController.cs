using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.SelfFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "SelfFuel_DownLoad_Operate", Name = "自用加儲氣管理專區操作手冊", MenuPath = "自用加儲油/D系統操作手冊", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]    
    public class SelfFuel_DownLoad_OperateController : Controller
    {
        // GET: SelfFuel_DownLoad_Operate
        public ActionResult Index()
        {
            return View();
        }
    }
}