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
    [MenuDef(Id = "PortGas_Insurance", Name = "航港設施保險公司查詢、修改", MenuPath = "隱藏選單/航港設施基本資料", Action = "Index", Index = 0, Func = FuncEnum.ALL, AllowAnonymous = false)]
    public class PortGas_InsuranceController : APaginationModelController<PortGas_Insurance>
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();

        // GET: CarGas_Insurance
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<PortGas_Insurance> GetModelEntity()
        {
            return new ModelEntity<PortGas_Insurance>(new OilGasModelContextExt());
        }
        protected override IQueryable<PortGas_Insurance> BeforeIQueryToPagedList(IQueryable<PortGas_Insurance> iquery, params KeyValueParams[] paras)
        {
            var CaseNo = Request.QueryString["CaseNo"];
            iquery = iquery.Where(X => X.CaseNo == CaseNo);
            return base.BeforeIQueryToPagedList(iquery, paras);
        }

        protected override void UpdateDBObject(IModelEntity<PortGas_Insurance> dbEntity, IEnumerable<PortGas_Insurance> objs)
        {
            //確保不是改前端畫面的資料
            var ID = objs.First().ID;
            var selectobjs = db.PortGas_Insurance.Where(X => X.ID == ID).FirstOrDefault();
            if (selectobjs.CaseNo.Replace(" ","") != objs.First().CaseNo.Replace(" ", ""))
            {
                throw new Exception("資料有誤");
            }

            objs.First().Change = selectobjs.Change + 1;
            objs.First().MemberID = Dou.Context.CurrentUser<User>().Id; ;



            base.UpdateDBObject(dbEntity, objs);
        }
        protected override void AddDBObject(IModelEntity<PortGas_Insurance> dbEntity, IEnumerable<PortGas_Insurance> objs)
        {
            objs.First().Change = 0;
            objs.First().MemberID = Dou.Context.CurrentUser<User>().Id; ;
            base.AddDBObject(dbEntity, objs);
        }
    }
}