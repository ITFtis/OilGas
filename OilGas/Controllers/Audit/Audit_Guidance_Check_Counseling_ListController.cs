using Dou.Controllers;
using Dou.Misc.Attr;
using Dou.Models.DB;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_Guidance_Check_Counseling_List", Name = "新增輔導結果", MenuPath = "查核輔導專區/G查核輔導資料", Action = "Index", Index = 3, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Audit_Guidance_Check_Counseling_ListController : APaginationModelController<Check_Counseling>
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: Audit_Guidance_Check_Counseling_List
        public ActionResult Index()
        {
            return View();
        }
        protected override IModelEntity<Check_Counseling> GetModelEntity()
        {
            return new ModelEntity<Check_Counseling>(new OilGasModelContextExt());
        }


        protected override IQueryable<Check_Counseling> BeforeIQueryToPagedList(IQueryable<Check_Counseling> iquery, params KeyValueParams[] paras)
        {

            //非ADMIN帳號只能看自己縣市
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



            //從查詢頁進來的，只能看被選到的CaseNo
            if (Request.QueryString["edit"] == "0")
            {
                var CaseNo = Request.QueryString["CaseNo"];
                iquery = iquery.Where(X => X.CaseNo == CaseNo);
                return base.BeforeIQueryToPagedList(iquery, paras);
            }


            return base.BeforeIQueryToPagedList(iquery, paras);
        }





        protected override void AddDBObject(IModelEntity<Check_Counseling> dbEntity, IEnumerable<Check_Counseling> objs)
        {


            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同




            objs.First().Isseud_Data = Path.GetFileName(objs.First().Isseud_Data);



            base.AddDBObject(dbEntity, objs);

        }
        protected override void UpdateDBObject(IModelEntity<Check_Counseling> dbEntity, IEnumerable<Check_Counseling> objs)
        {


            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同




            objs.First().Isseud_Data = Path.GetFileName(objs.First().Isseud_Data);



            base.UpdateDBObject(dbEntity, objs);

        }





        //取File_Name
        public string GETFile_Name(string Counseling_NO)
        {
            var Check_File_Counseling = from a in db.Check_File_Counseling
                                        where a.Counseling_NO == Counseling_NO
                                        select a;
            if (Check_File_Counseling.Count() == 0)
            {
                return "";
            }
            else
            {
                return Check_File_Counseling.First().File_Name;
            }
        }



        //上傳File_Name的檔案
        [HttpPost]
        public string SenduploadFile_Name(string ID, string CaseNo, HttpPostedFileBase file)//這裡的CaseNo其實是Counseling_No，因為寫共用function的時候取名子沒想到
        {
            //先抓原本資料的File_name
            var selectobjs = (from a in db.Check_File_Counseling
                              where a.Counseling_NO == CaseNo
                              select a).FirstOrDefault();
            var Old_File_name = "NULL";
            var filename = "false";
            if (selectobjs is null)
            {
                filename = basic.upload(file, Old_File_name, "Audit_Guidance_Check_Counseling_List\\File_Name");

                Check_File_Counseling Check_File_Counseling = new Check_File_Counseling()
                {
                    Counseling_NO= CaseNo,
                    File_Name= filename
                };
                db.Check_File_Counseling.Add(Check_File_Counseling);

            }
            else
            {
                Old_File_name = selectobjs.File_Name is null ? "NULL" : selectobjs.File_Name;//Isseud_Data如果是空給字串NULL
                filename = basic.upload(file, Old_File_name, "Audit_Guidance_Check_Counseling_List\\File_Name");
                selectobjs.File_Name = filename;
            }

           

            //修改檔名
            if (filename != "false")
            {
                db.SaveChanges();//修改SQL的檔名
                return filename;//回傳檔名給下一個funtion用
            }

            return "false";

        }


        //上傳Isseud_Data的檔案
        [HttpPost]
        public string Sendupload(string ID, string CaseNo, HttpPostedFileBase file)//這裡的CaseNo其實是Counseling_No，因為寫共用function的時候取名子沒想到
        {
            //先抓原本資料的File_name
            var selectobjs = (from a in db.Check_Counseling
                              where a.id.ToString() == ID && a.Counseling_No.ToString() == CaseNo
                              select a).FirstOrDefault();
            var Old_File_name = "NULL";
            if (selectobjs is null)
            {
            }
            else
            {
                Old_File_name = selectobjs.Isseud_Data is null ? "NULL" : selectobjs.Isseud_Data;//Isseud_Data如果是空給字串NULL
            }

            //上傳檔案，並拿檔名
            var filename = basic.upload(file, Old_File_name, "Audit_Guidance_Check_Counseling_List\\Isseud_Data");


            //修改檔名
            if (filename != "false")
            {

             
                selectobjs.Isseud_Data = filename;
                db.SaveChanges();//修改SQL的檔名


                return filename;//回傳檔名給下一個funtion用
            }

            return "false";

        }
    }
}