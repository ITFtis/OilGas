using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Admin
{
    [Dou.Misc.Attr.MenuDef(Id = "Admin_NewsMaintain", Name = "即時新聞", MenuPath = "系統管理專區/H最新消息管理", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Admin_NewsMaintainController : Controller
    {
        // GET: Admin_NewsMaintain
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Info_Main");
        }
    }
}