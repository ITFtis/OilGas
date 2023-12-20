using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.GasLawBan
{
    [Dou.Misc.Attr.MenuDef(Id = "GasLawBan_DownLoad_Operate", Name = "加油站管理專區操作手冊", MenuPath = "取締管理作業/F系統操作手冊", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class GasLawBan_DownLoad_OperateController : Controller
    {
        // GET: GasLawBan_DownLoad_Operate
        public ActionResult Index()
        {
            return View();
        }
    }
}