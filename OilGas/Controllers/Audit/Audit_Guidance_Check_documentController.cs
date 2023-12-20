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


namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_Guidance_Check_document", Name = "公文上傳專區", MenuPath = "查核輔導專區/G公文上傳專區", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Audit_Guidance_Check_documentController : APaginationModelController<Check_document>
    {
        // GET: Audit_Guidance_Check_document
        public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<Check_document> GetModelEntity()
        {
            return new ModelEntity<Check_document>(new OilGasModelContextExt());
        }

        protected override void UpdateDBObject(IModelEntity<Check_document> dbEntity, IEnumerable<Check_document> objs)
        {


            var CheckNo = objs.First().CheckNo;
            var selectobjs = db.Check_document.Where(X => X.CheckNo == CheckNo).FirstOrDefault();
            //上傳檔案存檔名
            objs.First().Gas_File = Path.GetFileName(objs.First().Gas_File)?? selectobjs.Gas_File;
            objs.First().EPB_File = Path.GetFileName(objs.First().EPB_File) ?? selectobjs.EPB_File;
            objs.First().GAS2_File = Path.GetFileName(objs.First().GAS2_File) ?? selectobjs.GAS2_File;
            objs.First().EPB2_File = Path.GetFileName(objs.First().EPB2_File) ?? selectobjs.EPB2_File;
            objs.First().GASend_File = Path.GetFileName(objs.First().GASend_File) ?? selectobjs.GASend_File;
            objs.First().ZERO_File = Path.GetFileName(objs.First().ZERO_File) ?? selectobjs.ZERO_File;
            objs.First().GAS3_File = Path.GetFileName(objs.First().GAS3_File) ?? selectobjs.GAS3_File;
            objs.First().EPB3_File = Path.GetFileName(objs.First().EPB3_File) ?? selectobjs.EPB3_File;



            base.UpdateDBObject(dbEntity, objs);
        }
        protected override void AddDBObject(IModelEntity<Check_document> dbEntity, IEnumerable<Check_document> objs)
        {


            objs.First().Gas_File = Path.GetFileName(objs.First().Gas_File);
            objs.First().EPB_File = Path.GetFileName(objs.First().EPB_File);
            objs.First().GAS2_File = Path.GetFileName(objs.First().GAS2_File);
            objs.First().EPB2_File = Path.GetFileName(objs.First().EPB2_File);
            objs.First().GASend_File = Path.GetFileName(objs.First().GASend_File);
            objs.First().ZERO_File = Path.GetFileName(objs.First().ZERO_File);
            objs.First().GAS3_File = Path.GetFileName(objs.First().GAS3_File);
            objs.First().EPB3_File = Path.GetFileName(objs.First().EPB3_File);






         

            var CheckNo = objs.First().CheckNo;
            var selectobjs = db.Check_Basic.Where(X => X.CheckNo == CheckNo).FirstOrDefault();
            var selectobjs_Action = db.Check_Basic_Action.Where(X => X.CheckNo == CheckNo);

            objs.First().CheckDate = selectobjs.CheckDate;
            objs.First().CheckDate_Action = selectobjs_Action.Count()>0? selectobjs_Action.FirstOrDefault().CheckDate:null;
            objs.First().Gas_Name = selectobjs.Gas_Name;
            objs.First().Business_theme = selectobjs.Business_theme;
            objs.First().Addr = selectobjs.Addr;
   










            base.AddDBObject(dbEntity, objs);
        }





        [HttpPost]
        public string Sendupload(string ID, string CaseNo, HttpPostedFileBase file,string filetype)//這裡的CaseNo其實是CheckNo，因為寫共用function的時候取名子沒想到，順帶一提ID沒有用
        {
            //先抓原本資料的File_name
            var selectobjs = (from a in db.Check_document
                              where a.CheckNo == CaseNo
                              select a).FirstOrDefault();
            bool add = false;
            var Old_File_name = "NULL";
            if (selectobjs is null)
            {
                add = true;
            }
            else
            {
                switch (filetype)
                {
                    case "Gas_File":
                        Old_File_name = selectobjs.Gas_File is null ? "NULL" : selectobjs.Gas_File;//File_name如果是空給字串NULL
                        break;
                    case "EPB_File":
                        Old_File_name = selectobjs.EPB_File is null ? "NULL" : selectobjs.EPB_File;//File_name如果是空給字串NULL
                        break;
                    case "GAS2_File":
                        Old_File_name = selectobjs.GAS2_File is null ? "NULL" : selectobjs.GAS2_File;//File_name如果是空給字串NULL
                        break;
                    case "EPB2_File":
                        Old_File_name = selectobjs.EPB2_File is null ? "NULL" : selectobjs.EPB2_File;//File_name如果是空給字串NULL
                        break;
                    case "GASend_File":
                        Old_File_name = selectobjs.GASend_File is null ? "NULL" : selectobjs.GASend_File;//File_name如果是空給字串NULL
                        break;
                    case "ZERO_File":
                        Old_File_name = selectobjs.ZERO_File is null ? "NULL" : selectobjs.ZERO_File;//File_name如果是空給字串NULL
                        break;
                    case "GAS3_File":
                        Old_File_name = selectobjs.GAS3_File is null ? "NULL" : selectobjs.GAS3_File;//File_name如果是空給字串NULL
                        break;
                    case "EPB3_File":
                        Old_File_name = selectobjs.EPB3_File is null ? "NULL" : selectobjs.EPB3_File;//File_name如果是空給字串NULL
                        break;

                }



            }


            //上傳檔案，並拿檔名
            var filename = "";
            switch (filetype)
            {
                case "Gas_File":
                     filename = basic.upload(file, Old_File_name, "Audit_Guidance_Check_document/Gas_File/");
                    break;
                case "EPB_File":
                    filename = basic.upload(file, Old_File_name, "Audit_Guidance_Check_document/EPB_File/");
                    break;
                case "GAS2_File":
                    filename = basic.upload(file, Old_File_name, "Audit_Guidance_Check_document/GAS2_File/");
                    break;
                case "EPB2_File":
                    filename = basic.upload(file, Old_File_name, "Audit_Guidance_Check_document/EPB2_File/");
                    break;
                case "GASend_File":
                    filename = basic.upload(file, Old_File_name, "Audit_Guidance_Check_document/GASend_File/");
                    break;
                case "ZERO_File":
                    filename = basic.upload(file, Old_File_name, "Audit_Guidance_Check_document/ZERO_File/");
                    break;
                case "GAS3_File":
                    filename = basic.upload(file, Old_File_name, "Audit_Guidance_Check_document/GAS3_File/");
                    break;
                case "EPB3_File":
                    filename = basic.upload(file, Old_File_name, "Audit_Guidance_Check_document/EPB3_File/");
                    break;

            }

          


            //修改檔名
            if (filename != "false")
            {
                if (!add)
                {
                    switch (filetype)
                    {
                        case "Gas_File":
                            selectobjs.Gas_File = filename;
                            break;
                        case "EPB_File":
                            selectobjs.EPB_File = filename;
                            break;
                        case "GAS2_File":
                            selectobjs.GAS2_File = filename;
                            break;
                        case "EPB2_File":
                            selectobjs.EPB2_File = filename;
                            break;
                        case "GASend_File":
                            selectobjs.GASend_File = filename;
                            break;
                        case "ZERO_File":
                            selectobjs.ZERO_File = filename;
                            break;
                        case "GAS3_File":
                            selectobjs.GAS3_File = filename;
                            break;
                        case "EPB3_File":
                            selectobjs.EPB3_File = filename;
                            break;

                    }
                 
                    db.SaveChanges();//修改SQL的檔名
                }

                return filename;//回傳檔名給下一個funtion用
            }

            return "false";

        }






        //搜尋CheckNo讓下拉選單有選向
        public string GetCheckNoSelectList(string CheckNoOrName)
        {
            string[] CITYdata = { "ALL" };
            //非ADMIN帳號只能看自己縣市
            if (!Dou.Context.CurrentIsAdminUser && !basic.Permissions("admin"))
            {
                CITYdata = Dou.Context.CurrentUser<User>().city.Split(',');
            }


            IQueryable<BasicDataForSelect> result;
            var resultJson = "";

            result = from a in db.Check_Basic
                     where (a.CheckNo.Contains(CheckNoOrName) || a.Gas_Name.Contains(CheckNoOrName)) && (CITYdata.Contains(a.CaseNo.Substring(4, 2)) || CITYdata.Contains("ALL"))
                     join b in db.Check_document on a.CheckNo equals b.CheckNo into abGroup
                     from ab in abGroup.DefaultIfEmpty()
                     where ab == null
                     select new BasicDataForSelect()
                     {
                         CheckNo = a.CheckNo,
                         Gas_Name = a.Gas_Name
                     };
            resultJson = JsonConvert.SerializeObject(result);

            return resultJson;
        }


        public class BasicDataForSelect
        {
            public string CheckNo { get; set; }
            public string Gas_Name { get; set; }
        }
    }
}