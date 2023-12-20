using Dou.Controllers;
using Dou.Misc.Attr;
using Dou.Models.DB;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarGas
{
    [MenuDef(Id = "SelfFuel_Insurance", Name = "自用加儲油設施保險公司查詢、修改", MenuPath = "隱藏選單/自用加儲油基本資料", Action = "Index", Index = 0, Func = FuncEnum.ALL, AllowAnonymous = false)]
    public class SelfFuel_InsuranceController : APaginationModelController<SelfFuel_Insurance>
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: CarGas_Insurance
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<SelfFuel_Insurance> GetModelEntity()
        {
            return new ModelEntity<SelfFuel_Insurance>(new OilGasModelContextExt());
        }
        protected override IQueryable<SelfFuel_Insurance> BeforeIQueryToPagedList(IQueryable<SelfFuel_Insurance> iquery, params KeyValueParams[] paras)
        {
            var CaseNo = Request.QueryString["CaseNo"];

            basic.iscityedit(CaseNo);//確定縣市跟帳號縣市相同


            iquery = iquery.Where(X => X.CaseNo == CaseNo);
            return base.BeforeIQueryToPagedList(iquery, paras);
        }

        protected override void UpdateDBObject(IModelEntity<SelfFuel_Insurance> dbEntity, IEnumerable<SelfFuel_Insurance> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同


            //確保不是改前端畫面的資料
            var ID = objs.First().Id;
            var selectobjs = db.SelfFuel_Insurance.Where(X => X.Id == ID).FirstOrDefault();
            if (selectobjs.CaseNo.Replace(" ", "") != objs.First().CaseNo.Replace(" ", ""))
            {
                throw new Exception("資料有誤");
            }

            objs.First().Change = selectobjs.Change + 1;
         



            base.UpdateDBObject(dbEntity, objs);
        }
        protected override void AddDBObject(IModelEntity<SelfFuel_Insurance> dbEntity, IEnumerable<SelfFuel_Insurance> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同


            objs.First().Change = 0;            
            base.AddDBObject(dbEntity, objs);
        }
        protected override void DeleteDBObject(IModelEntity<SelfFuel_Insurance> dbEntity, IEnumerable<SelfFuel_Insurance> objs)
        {
            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同


            base.DeleteDBObject(dbEntity, objs);

        }
    }
}