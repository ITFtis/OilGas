using Dou.Controllers;
using Dou.Misc;
using Dou.Models.DB;
using Newtonsoft.Json;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "ComprehensiveOpinions", Name = "綜合意見", MenuPath = "隱藏選單/新增查核結果", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class ComprehensiveOpinionsController : APaginationModelController<Check_Consolidated_Comments>
    {
        public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: Check_Tank_well
        public ActionResult Index()
        {
            return View();
        }
        protected override IModelEntity<Check_Consolidated_Comments> GetModelEntity()
        {
            return new ModelEntity<Check_Consolidated_Comments>(new OilGasModelContextExt());
        }

        protected override IQueryable<Check_Consolidated_Comments> BeforeIQueryToPagedList(IQueryable<Check_Consolidated_Comments> iquery, params KeyValueParams[] paras)
        {
            var CheckNo = Request.QueryString["CheckNo"];


            //非ADMIN帳號只能看自己縣市
            if (!Dou.Context.CurrentIsAdminUser && !basic.Permissions("admin"))
            {
                var CITYdata = Dou.Context.CurrentUser<User>().city.Split(',');
                iquery = iquery.ToList().Where(x => CITYdata.Contains(x.CITY)).AsQueryable();
            }

            iquery = iquery.Where(X => X.CheckNo == CheckNo);
            return base.BeforeIQueryToPagedList(iquery, paras);
        }

        protected override void AddDBObject(IModelEntity<Check_Consolidated_Comments> dbEntity, IEnumerable<Check_Consolidated_Comments> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同

        


            base.AddDBObject(dbEntity, objs);

        }

        protected override void UpdateDBObject(IModelEntity<Check_Consolidated_Comments> dbEntity, IEnumerable<Check_Consolidated_Comments> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同

            base.UpdateDBObject(dbEntity, objs);

        }


        //取非DOU表資料
        public string GetCheckBasicWithPDF(string CheckNo)
        {
           

            var Check_Basic = from a in db.Check_Basic
                        join b in (from d in db.Check_PdfFile where d.IsAction == false select d)
                        on a.CheckNo equals b.CheckNo into c
                        where a.CheckNo == CheckNo
                        from b in c.DefaultIfEmpty()
                        select new
                        {
                            CheckBasic = a,
                            CheckPdfFile = b
                        };

            //非ADMIN帳號只能看自己縣市
            if (!Dou.Context.CurrentIsAdminUser && !basic.Permissions("admin"))
            {
                var CITYdata = Dou.Context.CurrentUser<User>().city.Split(',');
                Check_Basic = Check_Basic.ToList().Where(x => CITYdata.Contains(x.CheckBasic.CITY)).AsQueryable();
            }
            string jsonString = JsonConvert.SerializeObject(Check_Basic.FirstOrDefault());


            return jsonString;
        }

        //存非DOU表資料
        public string SaveCheckBasicWithPDF(string CheckNo, string Improve, string OtherImprove, string Improve_Day, string Improve_Notes)
        {
            var Check_Basic = from a in db.Check_Basic
                              where a.CheckNo == CheckNo
                              select a;


            if (Check_Basic.Count() > 0)
            {
                basic.iscityedit(Check_Basic.First().CaseNo);//確定縣市跟帳號縣市相同
                Check_Basic.First().Improve = Improve;
                Check_Basic.First().OtherImprove = OtherImprove;
                Check_Basic.First().Improve_Day = Improve_Day;
                Check_Basic.First().Improve_Notes = Improve_Notes;
                db.SaveChanges();

            }


            if(Improve=="1")
            {
                puttoaction(CheckNo);
            }


            return "true";
        }

        //把資料複製到action
        public void puttoaction(string CheckNo)
        {
            var Check_Basic = (from a in db.Check_Basic
                              where a.CheckNo == CheckNo
                              select a).FirstOrDefault();

            Check_Basic_Action Check_Basic_Action = new Check_Basic_Action()
            {
                CheckNo = Check_Basic.CheckNo,
                CheckDate = Check_Basic.CheckDate,
                Gas_Name = Check_Basic.Gas_Name,
                Business_theme = Check_Basic.Business_theme,
                Addr = Check_Basic.Addr,
                Talk_time = Check_Basic.Talk_time,
                Officials = Check_Basic.Officials,
                CaseNo = Check_Basic.CaseNo,
                CaseType = Check_Basic.CaseType,
                CheckTable = Check_Basic.CheckTable,
                Tank_Well = Check_Basic.Tank_Well,


            };

            var Check_Basic_ActionDB = db.Check_Basic_Action.FirstOrDefault(p => p.CheckNo == CheckNo);

            if (Check_Basic_ActionDB != null)
            {
                //更新
                Check_Basic_ActionDB = Check_Basic_Action;
            }
            else
            {
                //新增
                db.Check_Basic_Action.Add(Check_Basic_Action);
            }

            db.SaveChanges();

        }







        [HttpPost]
        public string Sendupload(string ID, string CaseNo, HttpPostedFileBase file)//這裡的CaseNo其實是CheckNo，因為寫共用function的時候取名子沒想到，順帶一提ID沒有用
        {
            //先抓原本資料的File_name
            var selectobjs = (from a in db.Check_PdfFile
                              where a.CheckNo.ToString() == CaseNo&&a.IsAction==false
                              select a).FirstOrDefault();
            bool add = false;
            var Old_File_name = "NULL";
            if (selectobjs is null)
            {
                add = true;
            }
            else
            {
                Old_File_name = selectobjs.File is null ? "NULL" : selectobjs.File;//File_name如果是空給字串NULL
            }


            //上傳檔案，並拿檔名
            var filename = basic.upload(file, Old_File_name, "Audit\\Check_Consolidated_Comments\\");


            //修改檔名
            if (filename != "false")
            {
                if (!add)
                {
                    selectobjs.CheckNo = CaseNo;
                    selectobjs.File = filename;
                    selectobjs.IsAction = false;
                    selectobjs.File_Class = "Check";
                    db.SaveChanges();//修改SQL的檔名
                }
                else
                {
                    Check_PdfFile Check_PdfFile = new Check_PdfFile()
                    {

                        CheckNo = CaseNo,
                        File = filename,
                        IsAction = false,
                        File_Class = "Check"

                    };

                    db.Check_PdfFile.Add(Check_PdfFile);
                    db.SaveChanges();

                }

                return filename;//回傳檔名給下一個funtion用
            }

            return "false";

        }

    }
}