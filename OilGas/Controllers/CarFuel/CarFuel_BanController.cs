using Dou.Controllers;
using Dou.Models.DB;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "CarFuel_Ban", Name = "現況資料-基本資料欄位清單", MenuPath = "加油站/A統計報表專區", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class CarFuel_BanController : APaginationModelController<CarFuel_Ban>
    {
        // GET: CarFuel_Ban
        public ActionResult Index()
        {
            return View();
        }

        protected override IQueryable<CarFuel_Ban> BeforeIQueryToPagedList(IQueryable<CarFuel_Ban> iquery, params KeyValueParams[] paras)
        {
            var basic = new basicController();
            if (!Dou.Context.CurrentIsAdminUser && !basic.Permissions("admin"))
            {
                var CITYdata = Dou.Context.CurrentUser<User>().city.Split(',');
                iquery = iquery.ToList().Where(x => CITYdata.Contains(x.CITY)).AsQueryable();

            }




            //搜尋縣市
            var CITY = basic.getfilter(paras, "CITY");
            if (CITY != "")
            {
                //因為CITY可能用,分成兩個ID
                var CITYdata = CITY.Split(',');
                iquery = iquery.ToList().Where(x => CITYdata.Contains(x.CITY)).AsQueryable();
            }




            return base.BeforeIQueryToPagedList(iquery, paras);
        }

        protected override IModelEntity<CarFuel_Ban> GetModelEntity()
        {
            return new ModelEntity<CarFuel_Ban>(new OilGasModelContextExt());
        }
    }
}