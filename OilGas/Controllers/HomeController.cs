using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers
{
    public class HomeController : Dou.Controllers.UserBaseControll<User, Role>
    {
        public ActionResult Index()
        {
            return View();
        }

        internal static System.Data.Entity.DbContext _dbContext = new OilGasModelContextExt();
        protected override Dou.Models.DB.IModelEntity<User> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<User>(_dbContext);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}