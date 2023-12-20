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
    [Dou.Misc.Attr.MenuDef(Id = "Check97_CarFuel_BasicData", Name = "加油站查核(舊)", MenuPath = "隱藏選單/新增查核結果", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Check97_CarFuel_BasicDataController : APaginationModelController<Check_Item_97>
    {
        public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: Check_Item
        public ActionResult Index()
        {
            return View();
        }
        protected override IModelEntity<Check_Item_97> GetModelEntity()
        {
            return new ModelEntity<Check_Item_97>(new OilGasModelContextExt());
        }

        protected override IQueryable<Check_Item_97> BeforeIQueryToPagedList(IQueryable<Check_Item_97> iquery, params KeyValueParams[] paras)
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
            var iquery = db.Check_Item_97.Where(X => X.CheckNo == CheckNo).Take(1);
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



            options.singleDataEditCompletedReturnUrl = "/StatisticsAtatisticsAuditCarFuel97?CheckNo=" + CheckNo + "&CaseNo=" + CaseNo;
            options.editformWindowStyle = "showEditformOnly";

            return options;
        }



        protected override void AddDBObject(IModelEntity<Check_Item_97> dbEntity, IEnumerable<Check_Item_97> objs)
        {
            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同
            base.AddDBObject(dbEntity, objs);
        }

        protected override void UpdateDBObject(IModelEntity<Check_Item_97> dbEntity, IEnumerable<Check_Item_97> objs)
        {
            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同
            base.UpdateDBObject(dbEntity, objs);
        }




   
    }
}