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
    [Dou.Misc.Attr.MenuDef(Id = "Info_Main", Name = "最新消息資料", MenuPath = "資訊查詢/I最新消息", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Info_MainController : APaginationModelController<news>
    {
        public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: Info_Main
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<news> GetModelEntity()
        {
            return new ModelEntity<news>(new OilGasModelContextExt());
        }


        protected override void UpdateDBObject(IModelEntity<news> dbEntity, IEnumerable<news> objs)
        {
            objs.First().news_user = Dou.Context.CurrentUser<User>().Id;


            var ID = objs.First().news_id;
            var selectobjs = db.news.Where(X => X.news_id == ID).FirstOrDefault();
            objs.First().news_file = selectobjs.news_file;//File_name再上傳的時候給

            base.UpdateDBObject(dbEntity, objs);
        }
        protected override void AddDBObject(IModelEntity<news> dbEntity, IEnumerable<news> objs)
        {
            objs.First().news_user = Dou.Context.CurrentUser<User>().Id;
            objs.First().news_date = DateTime.Now;


            objs.First().news_file = Path.GetFileName(objs.First().news_file);

            base.AddDBObject(dbEntity, objs);
        }








        [HttpPost]
        public string Sendupload(string ID, string CaseNo, HttpPostedFileBase file)
        {
            //先抓原本資料的File_name
            var selectobjs = (from a in db.news
                              where a.news_id.ToString() == ID 
                              select a).FirstOrDefault();
            bool add = false;
            var Old_File_name = "NULL";
            if (selectobjs is null)
            {
                add = true;
            }
            else
            {
                Old_File_name = selectobjs.news_file is null ? "NULL" : selectobjs.news_file;//File_name如果是空給字串NULL
            }


            //上傳檔案，並拿檔名
            var filename = basic.upload(file, Old_File_name, "Info_Main/");


            //修改檔名
            if (filename != "false")
            {
                if (!add)
                {                   
                    selectobjs.news_file = filename;
                    db.SaveChanges();//修改SQL的檔名
                }

                return filename;//回傳檔名給下一個funtion用
            }

            return "false";

        }






    }
}