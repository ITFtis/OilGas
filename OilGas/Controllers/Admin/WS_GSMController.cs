using Dou.Controllers;
using Dou.Models.DB;
using OilGas._report;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static NPOI.HSSF.Util.HSSFColor;

namespace OilGas.Controllers.Admin
{
    [Dou.Misc.Attr.MenuDef(Id = "WS_GSM", Name = "未對應清單", MenuPath = "系統管理專區/H地下儲槽系統資料交換專區", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]

    public class WS_GSMController : APaginationModelController<WS_GSM>
    {
        private static OilGasModelContextExt _db = new OilGasModelContextExt();
        // GET: WS_GSM
        public ActionResult Index()
        {
            return View();
        }

        protected override IQueryable<WS_GSM> BeforeIQueryToPagedList(IQueryable<WS_GSM> iquery, params KeyValueParams[] paras)
        {

            var result = getDataQuery();

            return base.BeforeIQueryToPagedList(result, paras);
        }

       

        protected override IModelEntity<WS_GSM> GetModelEntity()
        {
            return new ModelEntity<WS_GSM>(new OilGasModelContextExt());
        }

        //匯出excel
        public ActionResult ExportExcel()
        {
            var query = getDataQuery();
            Rpt_WS_GSM rep = new Rpt_WS_GSM();
            string url = rep.Export(query);

            if (url == "")
            {
                return Json(new { result = false, errorMessage = rep.ErrorMessage }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = true, url = url }, JsonRequestBehavior.AllowGet);
            }

            
        }

        //取得資料
        private IQueryable<WS_GSM> getDataQuery()
        {
            //舊專案當中的sql查詢
            //Select g.gsm_id, g.gsm_name, g.gsm_field03, g.Situation
            //From WS_GSM g with(nolock)
            //left Join WS_GSM_Relation r with(nolock) On g.gsm_id = r.FacNo
            //Where r.CaseNo is null and r.FacNo is null;


            IModelEntity<WS_GSM_Relation> gsmRelation = new ModelEntity<WS_GSM_Relation>(_db);
            IModelEntity<WS_GSM> gsm = new ModelEntity<WS_GSM>(_db);

            var gsmR = gsmRelation.GetAll();

            var gsmDB = gsm.GetAll();


            var result = gsmDB.GroupJoin(gsmR, a => a.gsm_id, b => b.FacNo, (g, r) => new
            {
                gsm = g,
                relation = r

            })
                .SelectMany(x => x.relation.DefaultIfEmpty(), (g, r) => new
                {
                    GSM = g.gsm,
                    relation = r
                })
                .Where(x => x.relation.CaseNo == null && x.relation.FacNo == null)
            .Select(x => x.GSM)
            //.OrderBy(x => x.gsm_id);測試比對用
            .OrderBy(x => x.GW_Date);

            return result;
        }
    }

    
}