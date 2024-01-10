using Dou.Controllers;
using Dou.Models.DB;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Admin
{
    [Dou.Misc.Attr.MenuDef(Id = "CarVehicleGas_LicenseNo", Name = "增修發文字號專區", MenuPath = "系統管理專區/H發文字號專區", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class CarVehicleGas_LicenseNoController : AGenericModelController<CarVehicleGas_LicenseNo>
    {
        // GET: CarVehicleGas_LicenseNo
        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable<CarVehicleGas_LicenseNo> GetDataDBObject(IModelEntity<CarVehicleGas_LicenseNo> dbEntity, params KeyValueParams[] paras)
        {

            basicController basic = new basicController();
            var data = dbEntity.GetAll().ToList();
            if (!Dou.Context.CurrentIsAdminUser && !basic.Permissions("admin"))
            {
                //權限查詢
                var pCitys = Dou.Context.CurrentUser<User>().PowerCitysGSLs();

                data = data.Where(x => pCitys.Contains(x.CityCode)).ToList();
            }
                
            
            return data;
        }

        protected override void AddDBObject(IModelEntity<CarVehicleGas_LicenseNo> dbEntity, IEnumerable<CarVehicleGas_LicenseNo> objs)
        {
            base.AddDBObject(dbEntity, objs);
        }

        protected override IModelEntity<CarVehicleGas_LicenseNo> GetModelEntity()
        {
            return new ModelEntity<CarVehicleGas_LicenseNo>(new OilGasModelContextExt());
        }
    }
}