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
    [Dou.Misc.Attr.MenuDef(Id = "FileType", Name = "檔案分類", MenuPath = "資訊查詢/I檔案下載", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class FileTypeController : APaginationModelController<FileType>
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: FileDownload
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<FileType> GetModelEntity()
        {
            return new ModelEntity<FileType>(new OilGasModelContextExt());
        }


        protected override void UpdateDBObject(IModelEntity<FileType> dbEntity, IEnumerable<FileType> objs)
        {



            base.UpdateDBObject(dbEntity, objs);
        }
        protected override void AddDBObject(IModelEntity<FileType> dbEntity, IEnumerable<FileType> objs)
        {
           
            base.AddDBObject(dbEntity, objs);
        }



    }
}