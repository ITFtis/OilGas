using Dou.Controllers;
using Dou.Misc;
using Dou.Misc.Attr;
using Dou.Models.DB;
using Newtonsoft.Json;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.FishGas
{
    [Dou.Misc.Attr.MenuDef(Id = "FishGas_Select", Name = "漁船加油站基本資料查詢修改", MenuPath = "漁船加油站/C管理專區", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class FishGas_SelectController : APaginationModelController<FishGas_BasicData>
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();

        static public basicController basic = new basicController();

        public ActionResult Index()
        {
            return View();
        }


        protected override IQueryable<FishGas_BasicData> BeforeIQueryToPagedList(IQueryable<FishGas_BasicData> iquery, params KeyValueParams[] paras)
        {
            //透過網址ID取得資料
            Uri myUri = new Uri(Request.UrlReferrer.ToString());
            string ID = HttpUtility.ParseQueryString(myUri.Query).Get("ID");

            if (!string.IsNullOrEmpty(ID))
            {
                iquery = iquery.Where(a => a.ID.ToString() == ID);
            }

            if (!Dou.Context.CurrentIsAdminUser && !basic.Permissions("admin"))
            {
                var CITYdata = Dou.Context.CurrentUser<User>().city.Split(',');
                iquery = iquery.ToList().Where(x => CITYdata.Contains(x.CITY)).AsQueryable();
            }

            //搜尋縣市
            var CITY = basic.getfilter(paras, "CITY");
            if (CITY != "")
            {
                //因為CITY可能用,分成兩個ID
                var CITYdata = CITY.Split(',');
                iquery = iquery.ToList().Where(x => CITYdata.Contains(x.CITY)).AsQueryable();
            }




            return base.BeforeIQueryToPagedList(iquery, paras);
        }


        protected override void AddDBObject(IModelEntity<FishGas_BasicData> dbEntity, IEnumerable<FishGas_BasicData> objs)
        {
            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同


            objs.First().UsageState = basic.changeUsageState<FishGas_BasicData>(objs);

            objs.First().Create_date = DateTime.Now;
            objs.First().Mod_date = DateTime.Now;
            objs.First().Create_name = Dou.Context.CurrentUser<User>().Id;
            objs.First().Mod_name = Dou.Context.CurrentUser<User>().Id;
            objs.First().MemberID = Dou.Context.CurrentUser<User>().Id;


            objs.First().File_name = Path.GetFileName(objs.First().File_name);

            objs = SUM(objs);






            base.AddDBObject(dbEntity, objs);
            OilGas.Models.CarFuel_Insurance.ResetGetAllCarFuel_Insurance();
        }

        protected override void UpdateDBObject(IModelEntity<FishGas_BasicData> dbEntity, IEnumerable<FishGas_BasicData> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同


            //確保不是改前端畫面的資料
            var ID = objs.First().ID;
            var selectobjs = db.FishGas_BasicData.Where(X => X.ID == ID).FirstOrDefault();



            if (selectobjs.CaseNo != objs.First().CaseNo || !basic.timecompare(selectobjs.Create_date, objs.First().Create_date) || selectobjs.Create_name != objs.First().Create_name || !basic.timecompare(selectobjs.Report_date, objs.First().Report_date))
            {
                throw new Exception("資料有誤");
            }



            objs.First().UsageState = basic.changeUsageState<FishGas_BasicData>(objs);
            objs.First().MemberID = Dou.Context.CurrentUser<User>().Id;
            objs.First().Mod_date = DateTime.Now;
            objs.First().Mod_name = Dou.Context.CurrentUser<User>().Id;

            objs.First().File_name = selectobjs.File_name;//File_name再上傳的時候給


            objs = SUM(objs);




            base.UpdateDBObject(dbEntity, objs);

        }

        protected override void DeleteDBObject(IModelEntity<FishGas_BasicData> dbEntity, IEnumerable<FishGas_BasicData> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同


            if (objs.First().File_name is null)
            {

            }
            else
            {
                var path = ConfigurationManager.AppSettings["uploadfilepath"];
                System.IO.File.Delete(path + @"FishGas\basic\" + objs.First().File_name);//刪除舊檔案
            }

            base.DeleteDBObject(dbEntity, objs);
        }

        protected override IModelEntity<FishGas_BasicData> GetModelEntity()
        {
            return new ModelEntity<FishGas_BasicData>(new OilGasModelContextExt());
        }

        public override DataManagerOptions GetDataManagerOptions()
        {
            var options = base.GetDataManagerOptions();



            options.editformWindowStyle = "showEditformOnly";
            return options;
        }


        public IEnumerable<FishGas_BasicData> SUM(IEnumerable<FishGas_BasicData> objs)
        {
            objs.First().total_gun = (objs.First().one_gun ?? 0 )+ (objs.First().two_gun ?? 0 )+ (objs.First().four_gun ?? 0) + (objs.First().eight_gun ?? 0 )+ (objs.First().other_gun ?? 0) + (objs.First().six_gun ?? 0);
            return objs;

        }

        [HttpPost]
        public string Sendupload(string ID, string CaseNo, HttpPostedFileBase file)
        {
            //先抓原本資料的File_name
            var selectobjs = (from a in db.FishGas_BasicData
                              where a.ID.ToString() == ID && a.CaseNo.ToString() == CaseNo
                              select a).FirstOrDefault();
            var Old_File_name = "NULL";
            if (selectobjs is null)
            {
            }
            else
            {
                Old_File_name = selectobjs.File_name is null ? "NULL" : selectobjs.File_name;//File_name如果是空給字串NULL
            }

            //上傳檔案，並拿檔名
            var filename = basic.upload(file, Old_File_name, "FishGas\\basic");


            //修改檔名
            if (filename != "false")
            {
                if (selectobjs != null)
                {
                    selectobjs.Mod_name = Dou.Context.CurrentUser<User>().Id;
                    selectobjs.Mod_date = DateTime.Now;
                    selectobjs.File_name = filename;
                    db.SaveChanges();//修改SQL的檔名
                }



                return filename;//回傳檔名給下一個funtion用
            }

            return "false";

        }


    }

}