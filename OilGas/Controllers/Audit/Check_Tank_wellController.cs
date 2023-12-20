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
    [Dou.Misc.Attr.MenuDef(Id = "Check_Tank_well", Name = "加油站油槽陰井油氣檢測紀錄表", MenuPath = "隱藏選單/新增查核結果", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Check_Tank_wellController : APaginationModelController<Check_Tank_well>
    {
        public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: Check_Tank_well
        public ActionResult Index()
        {
            return View();
        }
        protected override IModelEntity<Check_Tank_well> GetModelEntity()
        {
            return new ModelEntity<Check_Tank_well>(new OilGasModelContextExt());
        }

        protected override IQueryable<Check_Tank_well> BeforeIQueryToPagedList(IQueryable<Check_Tank_well> iquery, params KeyValueParams[] paras)
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

        protected override void AddDBObject(IModelEntity<Check_Tank_well> dbEntity, IEnumerable<Check_Tank_well> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同

           



            base.AddDBObject(dbEntity, objs);

        }

        protected override void UpdateDBObject(IModelEntity<Check_Tank_well> dbEntity, IEnumerable<Check_Tank_well> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同





            base.AddDBObject(dbEntity, objs);

        }

        public string GetCheckBasic(string CheckNo)
        {
            var Check_Basic = from a in db.Check_Basic
                     where a.CheckNo == CheckNo
                     select a;

            //非ADMIN帳號只能看自己縣市
            if (!Dou.Context.CurrentIsAdminUser && !basic.Permissions("admin"))
            {
                var CITYdata = Dou.Context.CurrentUser<User>().city.Split(',');
                Check_Basic = Check_Basic.ToList().Where(x => CITYdata.Contains(x.CITY)).AsQueryable();
            }



            string jsonString = JsonConvert.SerializeObject(Check_Basic.FirstOrDefault());


            return jsonString;
        }

        public string SaveCheckBasic(string CheckNo,int? Weather,string Testing_personnel,int? TankCount)
        {
            


            var Check_Basic = from a in db.Check_Basic
                              where a.CheckNo == CheckNo
                              select a;


            if (Check_Basic.Count() > 0)
            {

                basic.iscityedit(Check_Basic.First().CaseNo);//確定縣市跟帳號縣市相同

                Check_Basic.First().Weather = Weather;
                Check_Basic.First().Testing_personnel = Testing_personnel;
                Check_Basic.First().TankCount = TankCount;
                db.SaveChanges();

            }
           

         


            return "true";
        }

    }
}