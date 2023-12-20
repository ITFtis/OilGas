using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Info
{
    [Dou.Misc.Attr.MenuDef(Id = "Info_LawSearch", Name = "函釋資料庫搜尋", MenuPath = "資訊查詢/I函釋資料庫", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Info_LawSearchController : Controller
    {
        // GET: Info_LawSearch
        public ActionResult Index()
        {
            return View();
        }
    }
}