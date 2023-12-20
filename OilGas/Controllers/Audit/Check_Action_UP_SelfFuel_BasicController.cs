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
    [Dou.Misc.Attr.MenuDef(Id = "Check_Action_UP_SelfFuel_Basic", Name = "地上自用加儲油", MenuPath = "隱藏選單/新增複查結果", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Check_Action_UP_SelfFuel_BasicController : APaginationModelController<Check_Item_SelfUP_Action>
    {
        public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: Check_Item
        public ActionResult Index()
        {
            return View();
        }
        protected override IModelEntity<Check_Item_SelfUP_Action> GetModelEntity()
        {
            return new ModelEntity<Check_Item_SelfUP_Action>(new OilGasModelContextExt());
        }

        protected override IQueryable<Check_Item_SelfUP_Action> BeforeIQueryToPagedList(IQueryable<Check_Item_SelfUP_Action> iquery, params KeyValueParams[] paras)
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

        public override DataManagerOptions GetDataManagerOptions()
        {
            var options = base.GetDataManagerOptions();





            //進頁面直接跳出編輯
            var CheckNo = Request.QueryString["CheckNo"];
            var CaseNo = Request.QueryString["CaseNo"];
            var iquery = db.Check_Item_SelfUP_Action.Where(X => X.CheckNo == CheckNo).Take(1);
            //非ADMIN帳號只能看自己縣市
            if (!Dou.Context.CurrentIsAdminUser && !basic.Permissions("admin"))
            {
                var CITYdata = Dou.Context.CurrentUser<User>().city.Split(',');
                iquery = iquery.ToList().Where(x => CITYdata.Contains(x.CITY)).AsQueryable();
            }
            options.datas = iquery;

            if (iquery.Count() >= 1)
            {
                options.singleDataEdit = true;
            }



            options.singleDataEditCompletedReturnUrl = "/StatisticsAtatisticsAuditSelfUPAction?CheckNo=" + CheckNo + "&CaseNo=" + CaseNo;
            options.editformWindowStyle = "showEditformOnly";

            return options;
        }



        protected override void AddDBObject(IModelEntity<Check_Item_SelfUP_Action> dbEntity, IEnumerable<Check_Item_SelfUP_Action> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同

            objs = SUM(objs);





            base.AddDBObject(dbEntity, objs);

        }

        protected override void UpdateDBObject(IModelEntity<Check_Item_SelfUP_Action> dbEntity, IEnumerable<Check_Item_SelfUP_Action> objs)
        {
            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同
            objs = SUM(objs);



            base.UpdateDBObject(dbEntity, objs);

        }


        public IEnumerable<Check_Item_SelfUP_Action> SUM(IEnumerable<Check_Item_SelfUP_Action> objs)
        {

            objs.First().A_Count = (objs.First().A01 == "0" ? 1 : 0) + (objs.First().A02 == "0" ? 1 : 0) + (objs.First().A03 == "0" ? 1 : 0) + (objs.First().A04 == "0" ? 1 : 0) + (objs.First().A05 == "0" ? 1 : 0) + (objs.First().A06 == "0" ? 1 : 0);
            objs.First().A_Conform = (objs.First().A01 == "1" ? 1 : 0) + (objs.First().A02 == "1" ? 1 : 0) + (objs.First().A03 == "1" ? 1 : 0) + (objs.First().A04 == "1" ? 1 : 0) + (objs.First().A05 == "1" ? 1 : 0) + (objs.First().A06 == "1" ? 1 : 0);
            objs.First().A_Doesmeet = (objs.First().A01 == "2" ? 1 : 0) + (objs.First().A02 == "2" ? 1 : 0) + (objs.First().A03 == "2" ? 1 : 0) + (objs.First().A04 == "2" ? 1 : 0) + (objs.First().A05 == "2" ? 1 : 0) + (objs.First().A06 == "2" ? 1 : 0);
            objs.First().A_Unable = (objs.First().A01 == "3" ? 1 : 0) + (objs.First().A02 == "3" ? 1 : 0) + (objs.First().A03 == "3" ? 1 : 0) + (objs.First().A04 == "3" ? 1 : 0) + (objs.First().A05 == "3" ? 1 : 0) + (objs.First().A06 == "3" ? 1 : 0);


            objs.First().B_Count = (objs.First().B01 == "0" ? 1 : 0) + (objs.First().B02 == "0" ? 1 : 0) + (objs.First().B03 == "0" ? 1 : 0);
            objs.First().B_Conform = (objs.First().B01 == "1" ? 1 : 0) + (objs.First().B02 == "1" ? 1 : 0) + (objs.First().B03 == "1" ? 1 : 0);
            objs.First().B_Doesmeet = (objs.First().B01 == "2" ? 1 : 0) + (objs.First().B02 == "2" ? 1 : 0) + (objs.First().B03 == "2" ? 1 : 0);
            objs.First().B_Unable = (objs.First().B01 == "3" ? 1 : 0) + (objs.First().B02 == "3" ? 1 : 0) + (objs.First().B03 == "3" ? 1 : 0);

            objs.First().C_Count = (objs.First().C01 == "0" ? 1 : 0) + (objs.First().C02 == "0" ? 1 : 0) + (objs.First().C03 == "0" ? 1 : 0) + (objs.First().C04 == "0" ? 1 : 0) + (objs.First().C05 == "0" ? 1 : 0) + (objs.First().C06 == "0" ? 1 : 0) + (objs.First().C07 == "0" ? 1 : 0) + (objs.First().C08 == "0" ? 1 : 0);
            objs.First().C_Conform = (objs.First().C01 == "1" ? 1 : 0) + (objs.First().C02 == "1" ? 1 : 0) + (objs.First().C03 == "1" ? 1 : 0) + (objs.First().C04 == "1" ? 1 : 0) + (objs.First().C05 == "1" ? 1 : 0) + (objs.First().C06 == "1" ? 1 : 0) + (objs.First().C07 == "1" ? 1 : 0) + (objs.First().C08 == "1" ? 1 : 0);
            objs.First().C_Doesmeet = (objs.First().C01 == "2" ? 1 : 0) + (objs.First().C02 == "2" ? 1 : 0) + (objs.First().C03 == "2" ? 1 : 0) + (objs.First().C04 == "2" ? 1 : 0) + (objs.First().C05 == "2" ? 1 : 0) + (objs.First().C06 == "2" ? 1 : 0) + (objs.First().C07 == "2" ? 1 : 0) + (objs.First().C08 == "2" ? 1 : 0);
            objs.First().C_Unable = (objs.First().C01 == "3" ? 1 : 0) + (objs.First().C02 == "3" ? 1 : 0) + (objs.First().C03 == "3" ? 1 : 0) + (objs.First().C04 == "3" ? 1 : 0) + (objs.First().C05 == "3" ? 1 : 0) + (objs.First().C06 == "3" ? 1 : 0) + (objs.First().C07 == "3" ? 1 : 0) + (objs.First().C08 == "3" ? 1 : 0);


            objs.First().D_Count = (objs.First().D01 == "0" ? 1 : 0) + (objs.First().D02 == "0" ? 1 : 0);
            objs.First().D_Conform = (objs.First().D01 == "1" ? 1 : 0) + (objs.First().D02 == "1" ? 1 : 0);
            objs.First().D_Doesmeet = (objs.First().D01 == "2" ? 1 : 0) + (objs.First().D02 == "2" ? 1 : 0);
            objs.First().D_Unable = (objs.First().D01 == "3" ? 1 : 0) + (objs.First().D02 == "3" ? 1 : 0);



            objs.First().G_Count = (objs.First().G01 == "0" ? 1 : 0);
            objs.First().G_Conform = (objs.First().G01 == "1" ? 1 : 0);
            objs.First().G_Doesmeet = (objs.First().G01 == "2" ? 1 : 0);
            objs.First().G_Unable = (objs.First().G01 == "3" ? 1 : 0);

            objs.First().H_Count = (objs.First().H01 == "0" ? 1 : 0) + (objs.First().H02 == "0" ? 1 : 0) + (objs.First().H03 == "0" ? 1 : 0) + (objs.First().H04 == "0" ? 1 : 0) + (objs.First().H05 == "0" ? 1 : 0);
            objs.First().H_Conform = (objs.First().H01 == "1" ? 1 : 0) + (objs.First().H02 == "1" ? 1 : 0) + (objs.First().H03 == "1" ? 1 : 0) + (objs.First().H04 == "1" ? 1 : 0) + (objs.First().H05 == "1" ? 1 : 0);
            objs.First().H_Doesmeet = (objs.First().H01 == "2" ? 1 : 0) + (objs.First().H02 == "2" ? 1 : 0) + (objs.First().H03 == "2" ? 1 : 0) + (objs.First().H04 == "2" ? 1 : 0) + (objs.First().H05 == "2" ? 1 : 0);
            objs.First().H_Unable = (objs.First().H01 == "3" ? 1 : 0) + (objs.First().H02 == "3" ? 1 : 0) + (objs.First().H03 == "3" ? 1 : 0) + (objs.First().H04 == "3" ? 1 : 0) + (objs.First().H05 == "3" ? 1 : 0);




            objs.First().AllCount = (objs.First().A_Count ?? 0) + (objs.First().B_Count ?? 0) + (objs.First().C_Count ?? 0) + (objs.First().D_Count ?? 0) + (objs.First().G_Count ?? 0) + (objs.First().H_Count ?? 0);
            objs.First().AllConform = (objs.First().A_Conform ?? 0) + (objs.First().B_Conform ?? 0) + (objs.First().C_Conform ?? 0) + (objs.First().D_Conform ?? 0) + (objs.First().G_Conform ?? 0) + (objs.First().H_Conform ?? 0);
            objs.First().AllDoesmeet = (objs.First().A_Doesmeet ?? 0) + (objs.First().B_Doesmeet ?? 0) + (objs.First().C_Doesmeet ?? 0) + (objs.First().D_Doesmeet ?? 0) + (objs.First().G_Doesmeet ?? 0) + (objs.First().H_Doesmeet ?? 0);
            objs.First().AllUnable = (objs.First().A_Unable ?? 0) + (objs.First().B_Unable ?? 0) + (objs.First().C_Unable ?? 0) + (objs.First().D_Unable ?? 0) + (objs.First().G_Unable ?? 0) + (objs.First().H_Unable ?? 0);


            return objs;

        }
    }
}