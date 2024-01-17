using Dou;
using Dou.Models.DB;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers
{
    [Dou.Misc.Attr.MenuDef(Name = "使用者管理", MenuPath = "系統管理", Action = "Index", Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class UserController : Dou.Controllers.UserBaseControll<User, Role>
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        
		protected override void AddDBObject(IModelEntity<User> dbEntity, IEnumerable<User> objs)
		{
			clearCache();

			convertCityAndGrade(objs);
			base.AddDBObject(dbEntity, objs);
		}

		
		protected override void UpdateDBObject(IModelEntity<User> dbEntity, IEnumerable<User> objs)
		{
			clearCache();

			convertCityAndGrade(objs);
			base.UpdateDBObject(dbEntity, objs);
		}

		protected override void DeleteDBObject(IModelEntity<User> dbEntity, IEnumerable<User> objs)
		{
			clearCache();

			base.DeleteDBObject(dbEntity, objs);
		}

        public override ActionResult DouLogin(User user, string returnUrl, bool redirectLogin = false)
        {
			//Rest登入者權限
			CarVehicleGas_LicenseNoSelectItems.Reset();

            return base.DouLogin(user, returnUrl, redirectLogin);
        }

        internal static System.Data.Entity.DbContext _dbContext = new OilGasModelContextExt();
        protected override Dou.Models.DB.IModelEntity<User> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<User>(_dbContext);
        }

		//避免新增帳號時city跟grade欄位出現null
		private void convertCityAndGrade(IEnumerable<User> objs)
		{
			//如果下拉選單送出是請選擇就會轉成空白

			var city = objs.FirstOrDefault().city;
			var grade = objs.FirstOrDefault().grade;

			objs.FirstOrDefault().city = city == null ? string.Empty : city;
			objs.FirstOrDefault().grade = grade == null ? string.Empty : grade;
		}

		//清除cache
		private void clearCache()
		{
			List<string> keys = new List<string>()
			{
				"OilGas.Models.Roles",
				"OilGas.Models.CityCode"
			};

			foreach (var key in keys)
			{
				DouHelper.Misc.ClearCache(key);
			}
		}
	}

}