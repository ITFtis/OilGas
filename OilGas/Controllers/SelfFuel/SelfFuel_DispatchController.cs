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

namespace OilGas.Controllers.CarGas
{
    [MenuDef(Id = "CarGas_Dispatch", Name = "自用加儲油設施發文歷程", MenuPath = "隱藏選單/自用加儲油基本資料", Action = "Index", Index = 0, Func = FuncEnum.ALL, AllowAnonymous = false)]
    public class SelfFuel_DispatchController : APaginationModelController<SelfFuel_Dispatch>
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: CarGas_Insurance
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<SelfFuel_Dispatch> GetModelEntity()
        {
            return new ModelEntity<SelfFuel_Dispatch>(new OilGasModelContextExt());
        }
        protected override IQueryable<SelfFuel_Dispatch> BeforeIQueryToPagedList(IQueryable<SelfFuel_Dispatch> iquery, params KeyValueParams[] paras)
        {
            var CaseNo = Request.QueryString["CaseNo"];

            basic.iscityedit(CaseNo);//確定縣市跟帳號縣市相同


            iquery = iquery.Where(X => X.CaseNo == CaseNo);
            return base.BeforeIQueryToPagedList(iquery, paras);
        }

        protected override void UpdateDBObject(IModelEntity<SelfFuel_Dispatch> dbEntity, IEnumerable<SelfFuel_Dispatch> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同


            //確保不是改前端畫面的資料
            var ID = objs.First().Id;
            var selectobjs = db.SelfFuel_Dispatch.Where(X => X.Id == ID).FirstOrDefault();
            if (selectobjs.CaseNo.Replace(" ", "") != objs.First().CaseNo.Replace(" ", ""))
            {
                throw new Exception("資料有誤");
            }

            objs.First().Change = selectobjs.Change + 1;




            objs.First().FileNewName = selectobjs.FileNewName;//File_name再上傳的時候給




            base.UpdateDBObject(dbEntity, objs);
        }
        protected override void AddDBObject(IModelEntity<SelfFuel_Dispatch> dbEntity, IEnumerable<SelfFuel_Dispatch> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同


            objs.First().Change = 0;



            objs.First().FileNewName = Path.GetFileName(objs.First().FileNewName);






            base.AddDBObject(dbEntity, objs);
        }


        protected override void DeleteDBObject(IModelEntity<SelfFuel_Dispatch> dbEntity, IEnumerable<SelfFuel_Dispatch> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同



            if (objs.First().FileNewName is null)
            {

            }
            else
            {
                var path = ConfigurationManager.AppSettings["uploadfilepath"];
                System.IO.File.Delete(path + @"SelfFuel\Dispatch\" + objs.First().FileNewName);//刪除舊檔案
            }


            base.DeleteDBObject(dbEntity, objs);
        }

        [HttpPost]
        public string Sendupload(string ID, string CaseNo, HttpPostedFileBase file)
        {
            //先抓原本資料的File_name
            var selectobjs = (from a in db.SelfFuel_Dispatch
                              where a.Id.ToString() == ID && a.CaseNo.ToString() == CaseNo
                              select a).FirstOrDefault();
            bool add = false;
            var Old_File_name = "NULL";
            if (selectobjs is null)
            {
                add = true;
            }
            else
            {
                Old_File_name = selectobjs.FileNewName is null ? "NULL" : selectobjs.FileNewName;//File_name如果是空給字串NULL
            }


            //上傳檔案，並拿檔名
            var filename = basic.upload(file, Old_File_name, "SelfFuel\\Dispatch");


            //修改檔名
            if (filename != "false")
            {
                if (!add)
                {
                    selectobjs.FileNewName = filename;
                    db.SaveChanges();//修改SQL的檔名
                }

                return filename;//回傳檔名給下一個funtion用
            }

            return "false";

        }


    }
}