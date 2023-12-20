using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Admin
{
    [Dou.Misc.Attr.MenuDef(Id = "Admin_PwdEdit", Name = "修改密碼", MenuPath = "系統管理專區/H帳號權限管理", Action = "Index", Index = 3, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Admin_PwdEditController : Controller
    {
        // GET: Admin_PwdEdit
        public ActionResult Index()
        {
            return View();
        }
    }
}