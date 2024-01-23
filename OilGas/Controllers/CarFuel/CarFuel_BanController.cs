using Dou.Controllers;
using Dou.Misc;
using Dou.Models.DB;
using DouHelper;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "CarFuel_Ban", Name = "違規案件資料查詢", MenuPath = "加油站/B違規案件專區", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class CarFuel_BanController : APaginationModelController<CarFuel_Ban>
    {
        // GET: CarFuel_Ban
        public ActionResult Index()
        {
            return View();
        }

        protected override IQueryable<CarFuel_Ban> BeforeIQueryToPagedList(IQueryable<CarFuel_Ban> iquery, params KeyValueParams[] paras)
        {         
            //權限查詢 (縣市權限，變動清除catch)
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysGSLs();

            var query = iquery.Where(a => a.CaseNo != null && pCitys.Any(b => b == a.CaseNo.Substring(4, 2)));

            return base.BeforeIQueryToPagedList(query, paras);
        }

        protected override IModelEntity<CarFuel_Ban> GetModelEntity()
        {
            return new ModelEntity<CarFuel_Ban>(new OilGasModelContextExt());
        }

        public override DataManagerOptions GetDataManagerOptions()
        {
            var option = base.GetDataManagerOptions();
            foreach (var o in option.fields)
            {
                o.align = "center";
            }


            return option;
        }
    }
}