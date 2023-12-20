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
    [MenuDef(Id = "PortGas_Dispatch", Name = "航港設施基發文歷程", MenuPath = "隱藏選單/航港設施基本資料", Action = "Index", Index = 0, Func = FuncEnum.ALL, AllowAnonymous = false)]
    public class PortGas_DispatchController : APaginationModelController<PortGas_Dispatch>
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();

        // GET: CarGas_Insurance
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<PortGas_Dispatch> GetModelEntity()
        {
            return new ModelEntity<PortGas_Dispatch>(new OilGasModelContextExt());
        }
        protected override IQueryable<PortGas_Dispatch> BeforeIQueryToPagedList(IQueryable<PortGas_Dispatch> iquery, params KeyValueParams[] paras)
        {
            var CaseNo = Request.QueryString["CaseNo"];
            iquery = iquery.Where(X => X.CaseNo == CaseNo);
            return base.BeforeIQueryToPagedList(iquery, paras);
        }

        protected override void UpdateDBObject(IModelEntity<PortGas_Dispatch> dbEntity, IEnumerable<PortGas_Dispatch> objs)
        {
            //確保不是改前端畫面的資料
            var ID = objs.First().ID;
            var selectobjs = db.PortGas_Dispatch.Where(X => X.ID == ID).FirstOrDefault();
            if (selectobjs.CaseNo.Replace(" ", "") != objs.First().CaseNo.Replace(" ", ""))
            {
                throw new Exception("資料有誤");
            }

            objs.First().Change = selectobjs.Change + 1;
            objs.First().MemberID = Dou.Context.CurrentUser<User>().Id;


            objs.First().File_name = selectobjs.File_name;//File_name再上傳的時候給

            base.UpdateDBObject(dbEntity, objs);
        }
        protected override void AddDBObject(IModelEntity<PortGas_Dispatch> dbEntity, IEnumerable<PortGas_Dispatch> objs)
        {
            objs.First().Change = 0;
            objs.First().MemberID = Dou.Context.CurrentUser<User>().Id;

            objs.First().File_name = Path.GetFileName(objs.First().File_name);



            base.AddDBObject(dbEntity, objs);
        }


        protected override void DeleteDBObject(IModelEntity<PortGas_Dispatch> dbEntity, IEnumerable<PortGas_Dispatch> objs)
        {

            if (objs.First().File_name is null)
            {

            }
            else
            {
                var path = ConfigurationManager.AppSettings["uploadfilepath"];
                System.IO.File.Delete(path + @"PortGas\Dispatch\" + objs.First().File_name);//刪除舊檔案
            }


            base.DeleteDBObject(dbEntity, objs);
        }
        [HttpPost]
        public string Sendupload(string ID, string CaseNo, HttpPostedFileBase file)
        {
            //先抓原本資料的File_name
            var selectobjs = (from a in db.PortGas_Dispatch
                              where a.ID.ToString() == ID && a.CaseNo.ToString() == CaseNo
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
            var filename = basic.upload(file, Old_File_name, "PortGas\\Dispatch");


            //修改檔名
            if (filename != "false")
            {
                if (!add)
                {
                    selectobjs.MemberID = Dou.Context.CurrentUser<User>().Id;
                    selectobjs.File_name = filename;
                    db.SaveChanges();//修改SQL的檔名
                }

                return filename;//回傳檔名給下一個funtion用
            }

            return "false";

        }

    }
}