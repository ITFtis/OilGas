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

namespace OilGas.Controllers.Info
{
    [Dou.Misc.Attr.MenuDef(Id = "Lesson", Name = "課程編輯", MenuPath = "資訊查詢/I線上報名", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class LessonController : APaginationModelController<Lesson>
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: FileDownload
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<Lesson> GetModelEntity()
        {
            return new ModelEntity<Lesson>(new OilGasModelContextExt());
        }


        protected override void UpdateDBObject(IModelEntity<Lesson> dbEntity, IEnumerable<Lesson> objs)
        {


            base.UpdateDBObject(dbEntity, objs);
        }
        protected override void AddDBObject(IModelEntity<Lesson> dbEntity, IEnumerable<Lesson> objs)
        {
            objs.First().LessonID = Guid.NewGuid();
          

            base.AddDBObject(dbEntity, objs);
        }



    }
}