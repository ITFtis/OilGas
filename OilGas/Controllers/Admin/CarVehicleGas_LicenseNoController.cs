using Dou.Controllers;
using Dou.Models.DB;
using DouHelper;
using Force.DeepCloner;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Admin
{
    [Dou.Misc.Attr.MenuDef(Id = "CarVehicleGas_LicenseNo", Name = "增修發文字號專區", MenuPath = "系統管理專區/H發文字號專區", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class CarVehicleGas_LicenseNoController : AGenericModelController<CarVehicleGas_LicenseNo>
    {
        private static OilGasModelContextExt _db = new OilGasModelContextExt();
        private Dou.Models.DB.IModelEntity<CarVehicleGas_LicenseNo_Log> LicenseNo_Log = new Dou.Models.DB.ModelEntity<CarVehicleGas_LicenseNo_Log>(_db);


        private static DateTime now = DateTime.Now;
        private DateTime timeForDB = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

        private User user = Dou.Context.CurrentUser<User>();

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
                var pCitys = user.PowerCitysGSLs();

                data = data.Where(x => pCitys.Contains(x.CityCode)).ToList();
            }
                
            
            return data;
        }

        protected override void AddDBObject(IModelEntity<CarVehicleGas_LicenseNo> dbEntity, IEnumerable<CarVehicleGas_LicenseNo> objs)
        {
            var data = objs.First();

            data.City = user.PowerCitysNames().First();
            data.CityCode = user.PowerCitysCodes().First();
            data.LicenseNo = "";
            data.Act = "add";
            data.CreateTime = timeForDB;
            data.Creator = user.Name;

            base.AddDBObject(dbEntity, objs);
        }

        protected override void UpdateDBObject(IModelEntity<CarVehicleGas_LicenseNo> dbEntity, IEnumerable<CarVehicleGas_LicenseNo> objs)
        {
            //新增log
            //先新增進log再更新
            var data = objs.First();

            var oriData = dbEntity.Get(x => x.ID == data.ID);
            InsertLog(oriData,"");

            data.Act = "mod";
            data.ModifyTime = timeForDB;
            data.Modifier = user.Name;
           
            base.UpdateDBObject(dbEntity, objs);

        }


        protected override void DeleteDBObject(IModelEntity<CarVehicleGas_LicenseNo> dbEntity, IEnumerable<CarVehicleGas_LicenseNo> objs)
        {
            //同步新增log
            var data = objs.First();
            var oriData = dbEntity.Get(x => x.ID == data.ID);
            InsertLog(oriData,"delete");

            base.DeleteDBObject(dbEntity, objs);
        }

        protected override IModelEntity<CarVehicleGas_LicenseNo> GetModelEntity()
        {
            return new ModelEntity<CarVehicleGas_LicenseNo>(new OilGasModelContextExt());
        }

        private void InsertLog(CarVehicleGas_LicenseNo oriData, string method)
        {
            var data = convertToLogData(oriData, method);


            var sql = @"INSERT INTO zz_CarVehicleGas_LicenseNo_Logs (ID,City,CityCode,Year,LicenseNo,DispatchNo,Act,CreateTime,Creator,ModifyTime,Modifier,DeleteTime,Deletor)
VALUES(@ID,@City,@CityCode,@Year,@LicenseNo,@DispatchNo,@Act,@CreateTime,@Creator,@ModifyTime,@Modifier,@DeleteTime,@Deletor)";

            SqlParameter[] paras = {
            new SqlParameter("@ID",(object)data.ID ?? DBNull.Value),
            new SqlParameter("@City",(object)data.City ?? DBNull.Value),
            new SqlParameter("@CityCode",(object)data.CityCode ?? DBNull.Value),
            new SqlParameter("@Year",(object) data.Year ?? DBNull.Value),
            new SqlParameter("@LicenseNo",(object) data.LicenseNo ?? DBNull.Value),
            new SqlParameter("@DispatchNo",(object) data.DispatchNo ?? DBNull.Value),
            new SqlParameter("@Act",(object) data.Act ?? DBNull.Value),
            new SqlParameter("@CreateTime",(object) data.CreateTime ?? DBNull.Value),
            new SqlParameter("@Creator",(object) data.Creator ?? DBNull.Value),
            new SqlParameter("@ModifyTime",(object) data.ModifyTime ?? DBNull.Value),
            new SqlParameter("@Modifier",(object) data.Modifier ?? DBNull.Value),
            new SqlParameter("@DeleteTime",(object) data.DeleteTime ?? DBNull.Value),
            new SqlParameter("@Deletor",(object) data.Deletor ?? DBNull.Value)
            };

            _db.Database.ExecuteSqlCommand(sql, paras);
        }


        private CarVehicleGas_LicenseNo_Log convertToLogData(CarVehicleGas_LicenseNo data,string method)
        {
            var result = new CarVehicleGas_LicenseNo_Log()
            {
                ID = data.ID,
                City = data.City,
                CityCode = data.CityCode,
                Year = data.Year,
                LicenseNo = data.LicenseNo,
                DispatchNo = data.DispatchNo,
                Act = data.Act,
                CreateTime = data.CreateTime,
                Creator = data.Creator,
                Modifier = data.Modifier,
                ModifyTime = data.ModifyTime,
  
            };

            if (method == "delete")
            {
                result.DeleteTime = timeForDB;
                result.Deletor = user.Name;
            }

            return result;
        }
    }
}