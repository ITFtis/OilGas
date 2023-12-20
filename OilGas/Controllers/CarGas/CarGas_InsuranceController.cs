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
    [MenuDef(Id = "CarGas_Insurance", Name = "加氣站保險公司查詢、修改", MenuPath = "隱藏選單/加氣站基本資料", Action = "Index", Index = 0, Func = FuncEnum.ALL, AllowAnonymous = false)]
    public class CarGas_InsuranceController : APaginationModelController<CarGas_Insurance>
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: CarGas_Insurance
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<CarGas_Insurance> GetModelEntity()
        {
            return new ModelEntity<CarGas_Insurance>(new OilGasModelContextExt());
        }
        protected override IQueryable<CarGas_Insurance> BeforeIQueryToPagedList(IQueryable<CarGas_Insurance> iquery, params KeyValueParams[] paras)
        {
            var CaseNo = Request.QueryString["CaseNo"];

            basic.iscityedit(CaseNo);//確定縣市跟帳號縣市相同


            iquery = iquery.Where(X => X.CaseNo == CaseNo);
            return base.BeforeIQueryToPagedList(iquery, paras);
        }

        protected override void UpdateDBObject(IModelEntity<CarGas_Insurance> dbEntity, IEnumerable<CarGas_Insurance> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同


            //確保不是改前端畫面的資料
            var ID = objs.First().ID;
            var selectobjs = db.CarGas_Insurance.Where(X => X.ID == ID).FirstOrDefault();
            if (selectobjs.CaseNo.Replace(" ", "") != objs.First().CaseNo.Replace(" ", ""))
            {
                throw new Exception("資料有誤");
            }

            objs.First().Change = selectobjs.Change + 1;
            objs.First().MemberID = Dou.Context.CurrentUser<User>().Id;




            base.UpdateDBObject(dbEntity, objs);
        }
        protected override void AddDBObject(IModelEntity<CarGas_Insurance> dbEntity, IEnumerable<CarGas_Insurance> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同


            objs.First().Change = 0;
            objs.First().MemberID = Dou.Context.CurrentUser<User>().Id;
            base.AddDBObject(dbEntity, objs);
        }

        protected override void DeleteDBObject(IModelEntity<CarGas_Insurance> dbEntity, IEnumerable<CarGas_Insurance> objs)
        {
            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同


            base.DeleteDBObject(dbEntity, objs);

        }
    }
}