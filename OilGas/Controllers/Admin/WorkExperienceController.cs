using Dou.Controllers;
using Dou.Misc.Attr;
using Dou.Models.DB;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Admin
{
    [Dou.Misc.Attr.MenuDef(Id = "WorkExperience", Name = "工作經驗", MenuPath = "隱藏選單/工安及環保人才資料庫", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class WorkExperienceController : APaginationModelController<Protection_WorkExperience>
    {
        // GET: WorkExperience
        static public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();


        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<Protection_WorkExperience> GetModelEntity()
        {
            return new ModelEntity<Protection_WorkExperience>(new OilGasModelContextExt());
        }
        protected override IQueryable<Protection_WorkExperience> BeforeIQueryToPagedList(IQueryable<Protection_WorkExperience> iquery, params KeyValueParams[] paras)
        {

            var BasicDataId = Request.QueryString["BasicDataId"];

           

            iquery = iquery.Where(X => X.BasicDataId.ToString() == BasicDataId);
            return base.BeforeIQueryToPagedList(iquery, paras);
        }





        protected override void UpdateDBObject(IModelEntity<Protection_WorkExperience> dbEntity, IEnumerable<Protection_WorkExperience> objs)
        {

            objs.First().ModifyUser = Dou.Context.CurrentUser<User>().Id;
            objs.First().ModifyTime = DateTime.Now;


            base.UpdateDBObject(dbEntity, objs);
        }
        protected override void AddDBObject(IModelEntity<Protection_WorkExperience> dbEntity, IEnumerable<Protection_WorkExperience> objs)
        {
            objs.First().CreateUser = Dou.Context.CurrentUser<User>().Id;
            objs.First().CreateTime = DateTime.Now;
            objs.First().ModifyUser = Dou.Context.CurrentUser<User>().Id;
            objs.First().ModifyTime = DateTime.Now;


            base.AddDBObject(dbEntity, objs);
        }







    }
}