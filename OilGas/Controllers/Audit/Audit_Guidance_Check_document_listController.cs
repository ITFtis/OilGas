using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_Guidance_Check_document_list", Name = "查詢公文附件資料", MenuPath = "查核輔導專區/G公文上傳專區", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Audit_Guidance_Check_document_listController : Controller
    {
        // GET: Audit_Guidance_Check_document_list
        public ActionResult Index()
        {
            return View();
        }
    }
}