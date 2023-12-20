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
    [Dou.Misc.Attr.MenuDef(Id = "Info_LawSearchM", Name = "函釋資料庫維護", MenuPath = "資訊查詢/I函釋資料庫", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Info_LawSearchMController : APaginationModelController<Law_Data>
    {
        // GET: Info_LawSearchM

        public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();

        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<Law_Data> GetModelEntity()
        {
            return new ModelEntity<Law_Data>(new OilGasModelContextExt());
        }

        protected override IQueryable<Law_Data> BeforeIQueryToPagedList(IQueryable<Law_Data> iquery, params KeyValueParams[] paras)
        {

            //搜尋法規與條文
            var LawMath_LawItem = basic.getfilter(paras, "LawMath_LawItem");
            if (LawMath_LawItem != "")
            {

                var Law_Data = iquery.ToList();

                //搜尋條文
                var LawMath_LawItemNo = basic.getfilter(paras, "LawMath_LawItemNo").Split(',')[0];
                if (LawMath_LawItemNo != "")
                {
                    var Law_Math = db.Law_Math.Where(x => x.LawMath_LawItemNo == LawMath_LawItemNo && x.LawMath_LawItem == LawMath_LawItem);
                    List<string> LawMath_LawData_FileName = new List<string>();
                    foreach (var data in Law_Math)
                    {
                        LawMath_LawData_FileName.Add(data.LawMath_LawData_FileName);
                    }

                    Law_Data = Law_Data.Where(x => LawMath_LawData_FileName.Contains(x.LawData_FileName)).ToList();
                }
                else
                {
                    //搜尋法規
                    var Law_Math = db.Law_Math.Where(x => x.LawMath_LawItem == LawMath_LawItem);
                    List<string> LawMath_LawData_FileName = new List<string>();
                    foreach (var data in Law_Math)
                    {
                        LawMath_LawData_FileName.Add(data.LawMath_LawData_FileName);
                    }

                    Law_Data = Law_Data.Where(x => LawMath_LawData_FileName.Contains(x.LawData_FileName)).ToList();
                }


                iquery = Law_Data.AsQueryable();
            }



          

            return base.BeforeIQueryToPagedList(iquery, paras);
        }


        protected override void UpdateDBObject(IModelEntity<Law_Data> dbEntity, IEnumerable<Law_Data> objs)
        {


            var LawData_Index = objs.First().LawData_Index;
            var selectobjs = db.Law_Data.Where(X => X.LawData_Index == LawData_Index).FirstOrDefault();
            objs.First().LawData_DownLad = selectobjs.LawData_DownLad;//File_name再上傳的時候給


            base.UpdateDBObject(dbEntity, objs);
        }
        protected override void AddDBObject(IModelEntity<Law_Data> dbEntity, IEnumerable<Law_Data> objs)
        {
   

            objs.First().LawData_DownLad = Path.GetFileName(objs.First().LawData_DownLad);


            base.AddDBObject(dbEntity, objs);
        }











        [HttpPost]
        public string Sendupload(string ID, string CaseNo, HttpPostedFileBase file)//這裡的CaseNo其實是CheckNo，因為寫共用function的時候取名子沒想到，順帶一提ID沒有用
        {
            //先抓原本資料的File_name
            var selectobjs = (from a in db.Law_Data
                              where a.LawData_Index.ToString() == ID
                              select a).FirstOrDefault();
            bool add = false;
            var Old_File_name = "NULL";
            if (selectobjs is null)
            {
                add = true;
            }
            else
            {
                Old_File_name = selectobjs.LawData_DownLad is null ? "NULL" : selectobjs.LawData_DownLad;//File_name如果是空給字串NULL
            }


            //上傳檔案，並拿檔名
            var filename = basic.upload(file, Old_File_name, "Info_LawSearchM\\");


            //修改檔名
            if (filename != "false")
            {
                if (!add)
                {
                    selectobjs.LawData_DownLad = filename;
                    db.SaveChanges();//修改SQL的檔名
                }

                return filename;//回傳檔名給下一個funtion用
            }

            return "false";

        }


    }
}