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
    [MenuDef(Id = "SelfFuel_Oil", Name = "自用加儲油設施類販售油品", MenuPath = "隱藏選單/自用加儲油基本資料", Action = "Index", Index = 0, Func = FuncEnum.ALL, AllowAnonymous = false)]
    public class SelfFuel_OilController : APaginationModelController<SelfFuel_Oil>
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: CarFuel_Insurance
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<SelfFuel_Oil> GetModelEntity()
        {
            return new ModelEntity<SelfFuel_Oil>(new OilGasModelContextExt());
        }
        protected override IQueryable<SelfFuel_Oil> BeforeIQueryToPagedList(IQueryable<SelfFuel_Oil> iquery, params KeyValueParams[] paras)
        {
            var CaseNo = Request.QueryString["CaseNo"];

            basic.iscityedit(CaseNo);//確定縣市跟帳號縣市相同


            iquery = iquery.Where(X => X.CaseNo == CaseNo);
            return base.BeforeIQueryToPagedList(iquery, paras);
        }


        protected override void UpdateDBObject(IModelEntity<SelfFuel_Oil> dbEntity, IEnumerable<SelfFuel_Oil> objs)
        {
            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同


            //確保不是改前端畫面的資料
            var ID = objs.First().Id;
            var selectobjs = db.SelfFuel_Oil.Where(X => X.Id == ID).FirstOrDefault();

            if (selectobjs.CaseNo.Replace(" ", "") != objs.First().CaseNo.Replace(" ", ""))
            {
                throw new Exception("資料有誤");
            }

            objs.First().Change = selectobjs.Change + 1;            
            base.UpdateDBObject(dbEntity, objs);
        }

        protected override void AddDBObject(IModelEntity<SelfFuel_Oil> dbEntity, IEnumerable<SelfFuel_Oil> objs)
        {
            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同

            objs.First().Change = 0;
            base.AddDBObject(dbEntity, objs);
        }
        protected override void DeleteDBObject(IModelEntity<SelfFuel_Oil> dbEntity, IEnumerable<SelfFuel_Oil> objs)
        {
            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同


            base.DeleteDBObject(dbEntity, objs);

        }
    }
}