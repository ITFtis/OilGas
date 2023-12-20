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
    [MenuDef(Id = "FishGas_OilData", Name = "漁船加油站販售油品", MenuPath = "隱藏選單/漁船加油站基本資料", Action = "Index", Index = 0, Func = FuncEnum.ALL, AllowAnonymous = false)]
    public class FishGas_OilDataController : APaginationModelController<FishGas_OilData>
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: CarFuel_Insurance
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<FishGas_OilData> GetModelEntity()
        {
            return new ModelEntity<FishGas_OilData>(new OilGasModelContextExt());
        }
        protected override IQueryable<FishGas_OilData> BeforeIQueryToPagedList(IQueryable<FishGas_OilData> iquery, params KeyValueParams[] paras)
        {
            var CaseNo = Request.QueryString["CaseNo"];

            basic.iscityedit(CaseNo);//確定縣市跟帳號縣市相同


            iquery = iquery.Where(X => X.CaseNo == CaseNo);
            return base.BeforeIQueryToPagedList(iquery, paras);
        }


        protected override void UpdateDBObject(IModelEntity<FishGas_OilData> dbEntity, IEnumerable<FishGas_OilData> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同


            //確保不是改前端畫面的資料
            var ID = objs.First().ID;
            var selectobjs = db.FishGas_OilData.Where(X => X.ID == ID).FirstOrDefault();

            if (selectobjs.CaseNo.Replace(" ", "") != objs.First().CaseNo.Replace(" ", ""))
            {
                throw new Exception("資料有誤");
            }

            objs.First().Change = selectobjs.Change + 1;
            objs.First().MemberID = Dou.Context.CurrentUser<User>().Id;
            base.UpdateDBObject(dbEntity, objs);
        }

        protected override void AddDBObject(IModelEntity<FishGas_OilData> dbEntity, IEnumerable<FishGas_OilData> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同


            objs.First().Change = 0;
            objs.First().MemberID = Dou.Context.CurrentUser<User>().Id;
            base.AddDBObject(dbEntity, objs);
        }

        protected override void DeleteDBObject(IModelEntity<FishGas_OilData> dbEntity, IEnumerable<FishGas_OilData> objs)
        {
            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同


            base.DeleteDBObject(dbEntity, objs);

        }
    }
}