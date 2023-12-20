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
    [Dou.Misc.Attr.MenuDef(Id = "FileDownload", Name = "檔案下載", MenuPath = "資訊查詢/I檔案下載", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class FileDownloadController : APaginationModelController<FileDownload>
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: FileDownload
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<FileDownload> GetModelEntity()
        {
            return new ModelEntity<FileDownload>(new OilGasModelContextExt());
        }


        protected override void UpdateDBObject(IModelEntity<FileDownload> dbEntity, IEnumerable<FileDownload> objs)
        {


            var UUID = objs.First().UUID;
            var selectobjs = db.FileDownload.Where(X => X.UUID == UUID).FirstOrDefault();
         
            objs.First().UpdateMemberID = Dou.Context.CurrentUser<User>().Id;

            objs.First().File_name = selectobjs.File_name;//File_name再上傳的時候給


            base.UpdateDBObject(dbEntity, objs);
        }
        protected override void AddDBObject(IModelEntity<FileDownload> dbEntity, IEnumerable<FileDownload> objs)
        {
            objs.First().UUID = Guid.NewGuid();
            objs.First().AddMemberID = Dou.Context.CurrentUser<User>().Id;
            objs.First().UpdateMemberID = Dou.Context.CurrentUser<User>().Id;
            objs.First().UpdateTime = DateTime.Now;
            objs.First().AddTime = DateTime.Now;

            objs.First().File_name = Path.GetFileName(objs.First().File_name);


            base.AddDBObject(dbEntity, objs);
        }



        [HttpPost]
        public string Sendupload(string ID, string CaseNo, HttpPostedFileBase file)
        {
            //先抓原本資料的File_name
            var selectobjs = (from a in db.FileDownload
                              where a.UUID.ToString() == ID 
                              select a).FirstOrDefault();
            bool add = false;
            var Old_File_name = "NULL";
            if (selectobjs is null)
            {
                add = true;
            }
            else
            {
                Old_File_name = selectobjs.File_name is null ? "NULL" : selectobjs.File_name;//File_name如果是空給字串NULL
            }


            //上傳檔案，並拿檔名
            var filename = basic.upload(file, Old_File_name, "FileDownload/");


            //修改檔名
            if (filename != "false")
            {
                if (!add)
                {
                    selectobjs.File_name = filename;
                    db.SaveChanges();//修改SQL的檔名
                }

                return filename;//回傳檔名給下一個funtion用
            }

            return "false";

        }
    }
}