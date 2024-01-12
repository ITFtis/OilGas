using Dou.Controllers;
using Dou.Models.DB;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_Guidance_Check_Basic_NoTime", Name = "加油站檢查表_不限時間", MenuPath = "查核輔導專區/G加油站業者", Action = "Index", Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Audit_Guidance_Check_Basic_NoTimeController : AGenericModelController<Check_Basic_NoTime>
    {
        // GET: Audit_Guidance_Check_Basic_NoTime
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<Check_Basic_NoTime> GetModelEntity()
        {
            return new ModelEntity<Check_Basic_NoTime>(new OilGasModelContextExt());
        }
    }
}