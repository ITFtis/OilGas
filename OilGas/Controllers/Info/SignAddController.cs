using Dou.Controllers;
using Dou.Misc;
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

namespace OilGas.Controllers.Info
{
    [Dou.Misc.Attr.MenuDef(Id = "SignAdd", Name = "課程報名", MenuPath = "隱藏選單/課程報名", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL , AllowAnonymous = true)]
    public class SignAddController : APaginationModelController<Sign>
    {
         public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: FileDownload
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<Sign> GetModelEntity()
        {
            return new ModelEntity<Sign>(new OilGasModelContextExt());
        }


        protected override void UpdateDBObject(IModelEntity<Sign> dbEntity, IEnumerable<Sign> objs)
        {
            objs.First().SignId = Guid.NewGuid();


            base.AddDBObject(dbEntity, objs);//這裡是要新增
        }
        protected override void AddDBObject(IModelEntity<Sign> dbEntity, IEnumerable<Sign> objs)
        {
            objs.First().SignId = Guid.NewGuid();


            base.AddDBObject(dbEntity, objs);
        }

        public override DataManagerOptions GetDataManagerOptions()
        {
            var opts = base.GetDataManagerOptions();






            //找禁止NULL的欄位
            var id = Request.QueryString["id"];
            var Lesson = (from a in db.Lesson
                     where a.LessonID.ToString() == id
                     select a).FirstOrDefault();
            string[] columns= { };
            if (Lesson.NoNullcolumn!= null)
            {
                columns = Lesson.NoNullcolumn.Split(';');//分割需要禁止NULL的欄位
            }
            foreach (var data in columns) 
            { 
                opts.GetFiled(data).allowNull = false; //禁止NULL
            }







            opts.datas = new Sign[] { new Sign() };
            opts.singleDataEdit = true;
            opts.singleDataEditCompletedReturnUrl = "/OilGas/Success.html";//填完進入成功畫面
            opts.editformWindowStyle = "showEditformOnly";
            return opts;
        }

    }
}