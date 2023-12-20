using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Info
{
    [Dou.Misc.Attr.MenuDef(Id = "Info_ODFDownLoad", Name = "ODF轉檔文件", MenuPath = "資訊查詢/IODF相關文件", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Info_ODFDownLoadController : Controller
    {
        // GET: Info_ODFDownLoad
        public ActionResult Index()
        {
            return View();
        }
    }
}