using Dou.Controllers;
using Dou.Misc.Attr;
using Dou.Models.DB;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.FishGas
{
    [MenuDef(Id = "SelfFuel_Facility", Name = "自用加儲油設施類型", MenuPath = "隱藏選單/自用加儲油基本資料", Action = "Index", Index = 0, Func = FuncEnum.ALL, AllowAnonymous = false)]
    public class SelfFuel_FacilityController : APaginationModelController<SelfFuel_Facility>
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: FishGas_Insurance
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<SelfFuel_Facility> GetModelEntity()
        {
            return new ModelEntity<SelfFuel_Facility>(new OilGasModelContextExt());
        }
        protected override IQueryable<SelfFuel_Facility> BeforeIQueryToPagedList(IQueryable<SelfFuel_Facility> iquery, params KeyValueParams[] paras)
        {
            var CaseNo = Request.QueryString["CaseNo"];

            basic.iscityedit(CaseNo);//確定縣市跟帳號縣市相同


            iquery = iquery.Where(X => X.CaseNo == CaseNo);
            return base.BeforeIQueryToPagedList(iquery, paras);
        }

        protected override void UpdateDBObject(IModelEntity<SelfFuel_Facility> dbEntity, IEnumerable<SelfFuel_Facility> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同


            //確保不是改前端畫面的資料
            var ID = objs.First().Id;
            var selectobjs = db.SelfFuel_Facility.Where(X => X.Id == ID).FirstOrDefault();
            if (selectobjs.CaseNo.Replace(" ", "") != objs.First().CaseNo.Replace(" ", ""))
            {
                throw new Exception("資料有誤");
            }

            objs.First().Change = selectobjs.Change + 1;

            objs = SUM(objs);



            base.UpdateDBObject(dbEntity, objs);
        }
        protected override void AddDBObject(IModelEntity<SelfFuel_Facility> dbEntity, IEnumerable<SelfFuel_Facility> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同


            objs = SUM(objs);
            objs.First().Change = 0;
            base.AddDBObject(dbEntity, objs);
        }

        public IEnumerable<SelfFuel_Facility> SUM(IEnumerable<SelfFuel_Facility> objs)
        {
            objs.First().TotalPump = nonullint(objs.First().SinglePump )+ nonullint(objs.First().DualPump) + nonullint( objs.First().FourPump )+ nonullint( objs.First().SixPump )+ nonullint( objs.First().EightPump);
           
            return objs;

        }

        public int nonullint(int? number)
        {
            int result = number is null ? 0 : number.Value;
            return result;
        }
        protected override void DeleteDBObject(IModelEntity<SelfFuel_Facility> dbEntity, IEnumerable<SelfFuel_Facility> objs)
        {
            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同


            base.DeleteDBObject(dbEntity, objs);

        }

    }
}