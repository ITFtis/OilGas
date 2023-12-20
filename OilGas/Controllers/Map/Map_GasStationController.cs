using Dou.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Map
{
    [Dou.Misc.Attr.MenuDef(Id = "Map_GasStation", Name = "加油站地圖及環域查詢", MenuPath = "加油站/A管理專區", Action = "Index", Index = 999)]
    public class Map_GasStationController : Dou.Controllers.AGenericModelController<Object>
    {
        // GET: GasMap
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<object> GetModelEntity()
        {
            throw new NotImplementedException();
        }
    }
}