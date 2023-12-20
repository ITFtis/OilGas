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
    [Dou.Misc.Attr.MenuDef(Id = "Check_Action_FishGas_BasicData", Name = "漁船加油站複查", MenuPath = "隱藏選單/新增複查結果", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Check_Action_FishGas_BasicDataController : APaginationModelController<Check_Item_Fish_Action>
    {
        public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: Check_Item
        public ActionResult Index()
        {
            return View();
        }
        protected override IModelEntity<Check_Item_Fish_Action> GetModelEntity()
        {
            return new ModelEntity<Check_Item_Fish_Action>(new OilGasModelContextExt());
        }

        protected override IQueryable<Check_Item_Fish_Action> BeforeIQueryToPagedList(IQueryable<Check_Item_Fish_Action> iquery, params KeyValueParams[] paras)
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
            var iquery = db.Check_Item_Fish_Action.Where(X => X.CheckNo == CheckNo).Take(1);
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



            options.singleDataEditCompletedReturnUrl = "/StatisticsAtatisticsAuditFishAction?CheckNo=" + CheckNo + "&CaseNo=" + CaseNo;
            options.editformWindowStyle = "showEditformOnly";

            return options;
        }



        protected override void AddDBObject(IModelEntity<Check_Item_Fish_Action> dbEntity, IEnumerable<Check_Item_Fish_Action> objs)
        {
            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同


            objs = SUM(objs);






            base.AddDBObject(dbEntity, objs);

        }

        protected override void UpdateDBObject(IModelEntity<Check_Item_Fish_Action> dbEntity, IEnumerable<Check_Item_Fish_Action> objs)
        {
            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同
            objs = SUM(objs);






            base.UpdateDBObject(dbEntity, objs);

        }




        public IEnumerable<Check_Item_Fish_Action> SUM(IEnumerable<Check_Item_Fish_Action> objs)
        {

            objs.First().A_Count = (objs.First().A01 == "0" ? 1 : 0) + (objs.First().A02 == "0" ? 1 : 0);
            objs.First().A_Conform = (objs.First().A01 == "1" ? 1 : 0) + (objs.First().A02 == "1" ? 1 : 0);
            objs.First().A_Doesmeet = (objs.First().A01 == "2" ? 1 : 0) + (objs.First().A02 == "2" ? 1 : 0);
            objs.First().A_Unable = (objs.First().A01 == "3" ? 1 : 0) + (objs.First().A02 == "3" ? 1 : 0);

            objs.First().B_Count = (objs.First().B01 == "0" ? 1 : 0) + (objs.First().B02 == "0" ? 1 : 0);
            objs.First().B_Conform = (objs.First().B01 == "1" ? 1 : 0) + (objs.First().B02 == "1" ? 1 : 0);
            objs.First().B_Doesmeet = (objs.First().B01 == "2" ? 1 : 0) + (objs.First().B02 == "2" ? 1 : 0);
            objs.First().B_Unable = (objs.First().B01 == "3" ? 1 : 0) + (objs.First().B02 == "3" ? 1 : 0);

            objs.First().C_Count = (objs.First().C01 == "0" ? 1 : 0) + (objs.First().C02 == "0" ? 1 : 0) + (objs.First().C03 == "0" ? 1 : 0);
            objs.First().C_Conform = (objs.First().C01 == "1" ? 1 : 0) + (objs.First().C02 == "1" ? 1 : 0) + (objs.First().C03 == "1" ? 1 : 0);
            objs.First().C_Doesmeet = (objs.First().C01 == "2" ? 1 : 0) + (objs.First().C02 == "2" ? 1 : 0) + (objs.First().C03 == "2" ? 1 : 0);
            objs.First().C_Unable = (objs.First().C01 == "3" ? 1 : 0) + (objs.First().C02 == "3" ? 1 : 0) + (objs.First().C03 == "3" ? 1 : 0);


            objs.First().D_Count = (objs.First().D01 == "0" ? 1 : 0) + (objs.First().D02 == "0" ? 1 : 0) + (objs.First().D03 == "0" ? 1 : 0);
            objs.First().D_Conform = (objs.First().D01 == "1" ? 1 : 0) + (objs.First().D02 == "1" ? 1 : 0) + (objs.First().D03 == "1" ? 1 : 0);
            objs.First().D_Doesmeet = (objs.First().D01 == "2" ? 1 : 0) + (objs.First().D02 == "2" ? 1 : 0) + (objs.First().D03 == "2" ? 1 : 0);
            objs.First().D_Unable = (objs.First().D01 == "3" ? 1 : 0) + (objs.First().D02 == "3" ? 1 : 0) + (objs.First().D03 == "3" ? 1 : 0);

            objs.First().E_Count = (objs.First().E01 == "0" ? 1 : 0);
            objs.First().E_Conform = (objs.First().E01 == "1" ? 1 : 0);
            objs.First().E_Doesmeet = (objs.First().E01 == "2" ? 1 : 0);
            objs.First().E_Unable = (objs.First().E01 == "3" ? 1 : 0);


            objs.First().F_Count = (objs.First().F01 == "0" ? 1 : 0) + (objs.First().F02 == "0" ? 1 : 0) + (objs.First().F03 == "0" ? 1 : 0) + (objs.First().F04 == "0" ? 1 : 0) + (objs.First().F05 == "0" ? 1 : 0);
            objs.First().F_Conform = (objs.First().F01 == "1" ? 1 : 0) + (objs.First().F02 == "1" ? 1 : 0) + (objs.First().F03 == "1" ? 1 : 0) + (objs.First().F04 == "1" ? 1 : 0) + (objs.First().F05 == "1" ? 1 : 0);
            objs.First().F_Doesmeet = (objs.First().F01 == "2" ? 1 : 0) + (objs.First().F02 == "2" ? 1 : 0) + (objs.First().F03 == "2" ? 1 : 0) + (objs.First().F04 == "2" ? 1 : 0) + (objs.First().F05 == "2" ? 1 : 0);
            objs.First().F_Unable = (objs.First().F01 == "3" ? 1 : 0) + (objs.First().F02 == "3" ? 1 : 0) + (objs.First().F03 == "3" ? 1 : 0) + (objs.First().F04 == "3" ? 1 : 0) + (objs.First().F05 == "3" ? 1 : 0);



            objs.First().G_Count = (objs.First().G01 == "0" ? 1 : 0) + (objs.First().G02 == "0" ? 1 : 0) + (objs.First().G03 == "0" ? 1 : 0) + (objs.First().G04 == "0" ? 1 : 0);
            objs.First().G_Conform = (objs.First().G01 == "1" ? 1 : 0) + (objs.First().G02 == "1" ? 1 : 0) + (objs.First().G03 == "1" ? 1 : 0) + (objs.First().G04 == "1" ? 1 : 0);
            objs.First().G_Doesmeet = (objs.First().G01 == "2" ? 1 : 0) + (objs.First().G02 == "2" ? 1 : 0) + (objs.First().G03 == "2" ? 1 : 0) + (objs.First().G04 == "2" ? 1 : 0);
            objs.First().G_Unable = (objs.First().G01 == "3" ? 1 : 0) + (objs.First().G02 == "3" ? 1 : 0) + (objs.First().G03 == "3" ? 1 : 0) + (objs.First().G04 == "3" ? 1 : 0);

            objs.First().H_Count = (objs.First().H01 == "0" ? 1 : 0) + (objs.First().H02 == "0" ? 1 : 0) + (objs.First().H03 == "0" ? 1 : 0);
            objs.First().H_Conform = (objs.First().H01 == "1" ? 1 : 0) + (objs.First().H02 == "1" ? 1 : 0) + (objs.First().H03 == "1" ? 1 : 0);
            objs.First().H_Doesmeet = (objs.First().H01 == "2" ? 1 : 0) + (objs.First().H02 == "2" ? 1 : 0) + (objs.First().H03 == "2" ? 1 : 0);
            objs.First().H_Unable = (objs.First().H01 == "3" ? 1 : 0) + (objs.First().H02 == "3" ? 1 : 0) + (objs.First().H03 == "3" ? 1 : 0);




            objs.First().AllCount = (objs.First().A_Count ?? 0) + (objs.First().B_Count ?? 0) + (objs.First().C_Count ?? 0) + (objs.First().D_Count ?? 0) + (objs.First().E_Count ?? 0) + (objs.First().F_Count ?? 0) + (objs.First().G_Count ?? 0) + (objs.First().H_Count ?? 0);
            objs.First().AllConform = (objs.First().A_Conform ?? 0) + (objs.First().B_Conform ?? 0) + (objs.First().C_Conform ?? 0) + (objs.First().D_Conform ?? 0) + (objs.First().E_Conform ?? 0) + (objs.First().F_Conform ?? 0) + (objs.First().G_Conform ?? 0) + (objs.First().H_Conform ?? 0);
            objs.First().AllDoesmeet = (objs.First().A_Doesmeet ?? 0) + (objs.First().B_Doesmeet ?? 0) + (objs.First().C_Doesmeet ?? 0) + (objs.First().D_Doesmeet ?? 0) + (objs.First().E_Doesmeet ?? 0) + (objs.First().F_Doesmeet ?? 0) + (objs.First().G_Doesmeet ?? 0) + (objs.First().H_Doesmeet ?? 0);
            objs.First().AllUnable = (objs.First().A_Unable ?? 0) + (objs.First().B_Unable ?? 0) + (objs.First().C_Unable ?? 0) + (objs.First().D_Unable ?? 0) + (objs.First().E_Unable ?? 0) + (objs.First().F_Unable ?? 0) + (objs.First().G_Unable ?? 0) + (objs.First().H_Unable ?? 0);


            return objs;

        }
    }
}