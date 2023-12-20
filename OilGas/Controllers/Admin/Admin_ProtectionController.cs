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


namespace OilGas.Controllers.Admin
{
    [Dou.Misc.Attr.MenuDef(Id = "Admin_Protection", Name = "修改工安及環保人才資料庫", MenuPath = "系統管理專區/H工安環保專區", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]    
    public class Admin_ProtectionController : APaginationModelController<Protection_BasicData>
    {
        public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();

        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<Protection_BasicData> GetModelEntity()
        {
            return new ModelEntity<Protection_BasicData>(new OilGasModelContextExt());
        }

        protected override void UpdateDBObject(IModelEntity<Protection_BasicData> dbEntity, IEnumerable<Protection_BasicData> objs)
        {


            var BasicDataId = objs.First().BasicDataId;
            var selectobjs = db.Protection_BasicData.Where(X => X.BasicDataId == BasicDataId).FirstOrDefault();

            objs.First().ModifyUser = Dou.Context.CurrentUser<User>().Id;
            objs.First().ModifyTime =DateTime.Now;

            objs.First().FileName = selectobjs.FileName;//File_name再上傳的時候給


            base.UpdateDBObject(dbEntity, objs);
        }
        protected override void AddDBObject(IModelEntity<Protection_BasicData> dbEntity, IEnumerable<Protection_BasicData> objs)
        {
            objs.First().CreateUser = Dou.Context.CurrentUser<User>().Id;
            objs.First().CreateTime = DateTime.Now;
            objs.First().ModifyUser = Dou.Context.CurrentUser<User>().Id;
            objs.First().ModifyTime = DateTime.Now;

            objs.First().FileName = Path.GetFileName(objs.First().FileName);


            base.AddDBObject(dbEntity, objs);
        }




        [HttpPost]
        public string Sendupload(string ID, string CaseNo, HttpPostedFileBase file)//這裡的CaseNo其實是CheckNo，因為寫共用function的時候取名子沒想到，順帶一提ID沒有用
        {
            //先抓原本資料的File_name
            var selectobjs = (from a in db.Protection_BasicData
                              where a.BasicDataId.ToString() == ID 
                              select a).FirstOrDefault();
            bool add = false;
            var Old_File_name = "NULL";
            if (selectobjs is null)
            {
                add = true;
            }
            else
            {
                Old_File_name = selectobjs.FileName is null ? "NULL" : selectobjs.FileName;//File_name如果是空給字串NULL
            }


            //上傳檔案，並拿檔名
            var filename = basic.upload(file, Old_File_name, "Admin_Protection\\");


            //修改檔名
            if (filename != "false")
            {
                if (!add)
                {
                    selectobjs.FileName = filename;
                    db.SaveChanges();//修改SQL的檔名
                }

                return filename;//回傳檔名給下一個funtion用
            }

            return "false";

        }



    }
}