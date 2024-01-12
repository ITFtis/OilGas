using Dou.Controllers;
using Dou.Misc;
using Dou.Models.DB;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "CarFuel_Update", Name = "負責人批次變更", MenuPath = "加油站/A管理專區", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class CarFuel_UpdateController : APaginationModelController<CarFuel_BasicData>
    {
        // GET: CarFuel_Update
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<CarFuel_BasicData> GetModelEntity()
        {
            return new ModelEntity<CarFuel_BasicData>(new OilGasModelContextExt());
        }

        public override DataManagerOptions GetDataManagerOptions()
        {
            var opts = base.GetDataManagerOptions();

            //全部欄位排序
            foreach (var field in opts.fields)
                field.filter = false;

            opts.GetFiled("Gas_Name").filter = true;            
            opts.GetFiled("Business_theme").filter = true;            
            opts.GetFiled("Boss").visible = true;
            opts.GetFiled("Recipient_date").visible = false;

            return opts;
        }
    }
}