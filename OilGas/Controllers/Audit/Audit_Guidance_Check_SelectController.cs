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
    [Dou.Misc.Attr.MenuDef(Id = "Audit_Guidance_Check_Select", Name = "查核輔導資料查詢修改", MenuPath = "查核輔導專區/G查核輔導資料", Action = "Index", Index = 4, Func = Dou.Misc.Attr.FuncEnum.Update, AllowAnonymous = false)]
    public class Audit_Guidance_Check_SelectController : APaginationModelController<Check_Basic>
    {
        public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: Audit_Guidance_Check_List
        public ActionResult Index()
        {
            return View();
        }
        protected override IModelEntity<Check_Basic> GetModelEntity()
        {
            return new ModelEntity<Check_Basic>(new OilGasModelContextExt());
        }
        protected override IQueryable<Check_Basic> BeforeIQueryToPagedList(IQueryable<Check_Basic> iquery, params KeyValueParams[] paras)
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







   
            return base.BeforeIQueryToPagedList(iquery, paras);
        }


   

        protected override void UpdateDBObject(IModelEntity<Check_Basic> dbEntity, IEnumerable<Check_Basic> objs)
        {


            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同





            //確保不是改前端畫面的資料
            var ID = objs.First().id;
            var selectobjs = db.Check_Basic.Where(X => X.id == ID).FirstOrDefault();
            if (selectobjs.CaseNo != objs.First().CaseNo || selectobjs.Gas_Name != objs.First().Gas_Name || selectobjs.CheckNo != objs.First().CheckNo)
            {
                throw new Exception("資料有誤");
            }




            base.UpdateDBObject(dbEntity, objs);

        }

        public override DataManagerOptions GetDataManagerOptions()
        {
            var opts = base.GetDataManagerOptions();

         

            opts.GetFiled("Talk_time").visible = false;
            opts.GetFiled("Officials").visible = false;
            opts.GetFiled("Record").visible = false;

            opts.GetFiled("URLAction").visible = true;
            opts.GetFiled("URLCounseling").visible = true;

            return opts;
        }


    }
}