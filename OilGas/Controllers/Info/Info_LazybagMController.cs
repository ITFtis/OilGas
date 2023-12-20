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
    [Dou.Misc.Attr.MenuDef(Id = "Info_LazybagM", Name = "圖文懶人包維護", MenuPath = "資訊查詢/I圖文懶人包", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Info_LazybagMController : APaginationModelController<Lazybag>
    {
        public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: Info_Main
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<Lazybag> GetModelEntity()
        {
            return new ModelEntity<Lazybag>(new OilGasModelContextExt());
        }

        protected override void UpdateDBObject(IModelEntity<Lazybag> dbEntity, IEnumerable<Lazybag> objs)
        {
          

            var ID = objs.First().s_index;
            var selectobjs = db.Lazybag.Where(X => X.s_index == ID).FirstOrDefault();
            objs.First().s_pic = selectobjs.s_pic;//File_name再上傳的時候給

            base.UpdateDBObject(dbEntity, objs);
        }
        protected override void AddDBObject(IModelEntity<Lazybag> dbEntity, IEnumerable<Lazybag> objs)
        {
    

            objs.First().s_pic = Path.GetFileName(objs.First().s_pic);

            base.AddDBObject(dbEntity, objs);
        }








        [HttpPost]
        public string Sendupload(string ID, string CaseNo, HttpPostedFileBase file)
        {
            //先抓原本資料的File_name
            var selectobjs = (from a in db.Lazybag
                              where a.s_index.ToString() == ID
                              select a).FirstOrDefault();
            bool add = false;
            var Old_File_name = "NULL";
            if (selectobjs is null)
            {
                add = true;
            }
            else
            {
                Old_File_name = selectobjs.s_pic is null ? "NULL" : selectobjs.s_pic;//File_name如果是空給字串NULL
            }


            //上傳檔案，並拿檔名
            var filename = basic.upload(file, Old_File_name, "Info_LazybagM/");


            //修改檔名
            if (filename != "false")
            {
                if (!add)
                {
                    selectobjs.s_pic = filename;
                    db.SaveChanges();//修改SQL的檔名
                }

                return filename;//回傳檔名給下一個funtion用
            }

            return "false";

        }


    }
}