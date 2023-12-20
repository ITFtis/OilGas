using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Admin
{
    [Dou.Misc.Attr.MenuDef(Id = "Admin_ApplyAccount", Name = "帳號申請", MenuPath = "系統管理專區/H帳號權限管理", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Admin_ApplyAccountController : Controller
    {
        // GET: Admin_ApplyAccount
        public ActionResult Index()
        {
            return View();
        }
    }
}