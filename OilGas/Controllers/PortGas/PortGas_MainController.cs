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

namespace OilGas.Controllers.PortGas
{
    [Dou.Misc.Attr.MenuDef(Id = "PortGas_Main", Name = "航港設施基本資料查詢修改", MenuPath = "航港自用加儲油/E設施管理專區", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class PortGas_MainController : APaginationModelController<PortGas_BasicData>
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();

        static public basicController basic = new basicController();

        public ActionResult Index()
        {
            return View();
        }


        protected override IQueryable<PortGas_BasicData> BeforeIQueryToPagedList(IQueryable<PortGas_BasicData> iquery, params KeyValueParams[] paras)
        {


            return base.BeforeIQueryToPagedList(iquery, paras);
        }


        protected override void AddDBObject(IModelEntity<PortGas_BasicData> dbEntity, IEnumerable<PortGas_BasicData> objs)
        {
           // objs.First().UsageState = basic.changeUsageState<PortGas_BasicData>(objs);

            objs.First().Create_date = DateTime.Now;
            objs.First().Mod_date = DateTime.Now;
            objs.First().Create_name = Dou.Context.CurrentUser<User>().Id;
            objs.First().Mod_name = Dou.Context.CurrentUser<User>().Id;
            objs.First().MemberID = Dou.Context.CurrentUser<User>().Id;




            objs = SUM(objs);






            base.AddDBObject(dbEntity, objs);
        }

        protected override void UpdateDBObject(IModelEntity<PortGas_BasicData> dbEntity, IEnumerable<PortGas_BasicData> objs)
        {
            var ID = objs.First().ID;
            var selectobjs = db.PortGas_BasicData.Where(X => X.ID == ID).FirstOrDefault();



            if (selectobjs.CaseNo != objs.First().CaseNo || !basic.timecompare(selectobjs.Create_date, objs.First().Create_date) || selectobjs.Create_name != objs.First().Create_name || !basic.timecompare(DateTime.Parse(selectobjs.Report_date??"1999-01-01 00:00"), DateTime.Parse(objs.First().Report_date ?? "1999-01-01 00:00")))
            {
                throw new Exception("資料有誤");
            }



            //objs.First().UsageState = basic.changeUsageState<PortGas_BasicData>(objs);
            objs.First().MemberID = Dou.Context.CurrentUser<User>().Id;
            objs.First().Mod_date = DateTime.Now;
            objs.First().Mod_name = Dou.Context.CurrentUser<User>().Id;




            objs = SUM(objs);




            base.UpdateDBObject(dbEntity, objs);
        }

        protected override void DeleteDBObject(IModelEntity<PortGas_BasicData> dbEntity, IEnumerable<PortGas_BasicData> objs)
        {
            base.DeleteDBObject(dbEntity, objs);
        }

        protected override IModelEntity<PortGas_BasicData> GetModelEntity()
        {
            return new ModelEntity<PortGas_BasicData>(new OilGasModelContextExt());
        }
        public override DataManagerOptions GetDataManagerOptions()
        {
            var options = base.GetDataManagerOptions();



            options.editformWindowStyle = "showEditformOnly";
            return options;
        }


        public IEnumerable<PortGas_BasicData> SUM(IEnumerable<PortGas_BasicData> objs)
        {
            // objs.First().total_gun = objs.First().one_gun + objs.First().two_gun + objs.First().four_gun + objs.First().eight_gun + objs.First().other_gun + objs.First().six_gun;
            return objs;

        }

    }
}