using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Admin
{
    [Dou.Misc.Attr.MenuDef(Id = "Admin_AccountUseCount", Name = "系統使用率", MenuPath = "系統管理專區/H帳號權限管理", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Admin_AccountUseCountController : Controller
    {
        // GET: Admin_AccountUseCount
        public ActionResult Index()
        {
            return View();
        }
    }
}