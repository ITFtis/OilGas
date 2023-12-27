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
            //取得最新消息
            Dou.Models.DB.IModelEntity<news> news = new Dou.Models.DB.ModelEntity<news>(_dbContext);
            ViewBag.news = news.GetAll()
                            .OrderByDescending(x => x.news_date)
                            .Take(4)
                            .ToList();

            //取得懶人包
            Dou.Models.DB.IModelEntity<Lazybag> lazyBag = new Dou.Models.DB.ModelEntity<Lazybag>(_dbContext);
            ViewBag.lazyBag = lazyBag.GetAll().ToList();

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