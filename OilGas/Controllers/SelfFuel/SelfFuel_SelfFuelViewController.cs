using Dou.Controllers;
using Dou.Misc;
using Dou.Misc.Attr;
using Dou.Models.DB;
using Newtonsoft.Json;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.SelfFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "SelfFuel_SelfFuelView", Name = "自用加儲油設施基本資料查詢修改", MenuPath = "自用加儲油/D管理專區", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class SelfFuel_SelfFuelViewController : APaginationModelController<SelfFuel_Basic>
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();

        static public basicController basic = new basicController();

        public ActionResult Index()
        {
            return View();
        }


        protected override IQueryable<SelfFuel_Basic> BeforeIQueryToPagedList(IQueryable<SelfFuel_Basic> iquery, params KeyValueParams[] paras)
        {
            iquery = iquery.Where(x => x.IsConfirm == true);

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


        protected override void AddDBObject(IModelEntity<SelfFuel_Basic> dbEntity, IEnumerable<SelfFuel_Basic> objs)
        {


            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同

            // objs.First().UsageState = basic.changeUsageState<SelfFuel_Basic>(objs);

            objs.First().CreateTime = DateTime.Now;
            objs.First().ModifyTime = DateTime.Now;
            objs.First().CreateUser = Dou.Context.CurrentUser<User>().Id;
            objs.First().LastModifyUser = Dou.Context.CurrentUser<User>().Id;

            objs.First().IsConfirm = true;


            objs = SUM(objs);






            base.AddDBObject(dbEntity, objs);
            OilGas.Models.CarFuel_Insurance.ResetGetAllCarFuel_Insurance();
        }

        protected override void UpdateDBObject(IModelEntity<SelfFuel_Basic> dbEntity, IEnumerable<SelfFuel_Basic> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同



            //確保不是改前端畫面的資料
            var ID = objs.First().Id;
            var selectobjs = db.SelfFuel_Basic.Where(X => X.Id == ID).FirstOrDefault();



            if (selectobjs.CaseNo != objs.First().CaseNo || !basic.timecompare(selectobjs.CreateTime, objs.First().CreateTime) || selectobjs.CreateUser != objs.First().CreateUser )
            {
                throw new Exception("資料有誤");
            }



           // objs.First().UsageState = basic.changeUsageState<SelfFuel_Basic>(objs);
           // objs.First().MemberID = Dou.Context.CurrentUser<User>().Id;
            objs.First().ModifyTime = DateTime.Now;
            objs.First().LastModifyUser = Dou.Context.CurrentUser<User>().Id;

            objs.First().IsConfirm = true;


            objs = SUM(objs);




            base.UpdateDBObject(dbEntity, objs);
         
        }

        protected override void DeleteDBObject(IModelEntity<SelfFuel_Basic> dbEntity, IEnumerable<SelfFuel_Basic> objs)
        {
            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同

            base.DeleteDBObject(dbEntity, objs);
        }

        protected override IModelEntity<SelfFuel_Basic> GetModelEntity()
        {
            return new ModelEntity<SelfFuel_Basic>(new OilGasModelContextExt());
        }
        public override DataManagerOptions GetDataManagerOptions()
        {
            var options = base.GetDataManagerOptions();



            options.editformWindowStyle = "showEditformOnly";
            return options;
        }


        public IEnumerable<SelfFuel_Basic> SUM(IEnumerable<SelfFuel_Basic> objs)
        {
           // objs.First().total_gun = objs.First().one_gun + objs.First().two_gun + objs.First().four_gun + objs.First().eight_gun + objs.First().other_gun + objs.First().six_gun;
            return objs;

        }


    }

}