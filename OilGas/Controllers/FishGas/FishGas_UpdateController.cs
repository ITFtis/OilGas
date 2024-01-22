using Dou.Controllers;
using Dou.Models.DB;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.FishGas
{
    [Dou.Misc.Attr.MenuDef(Id = "FishGas_Update", Name = "負責人批次變更", MenuPath = "漁船加油站/C管理專區", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class FishGas_UpdateController : APaginationModelController<FishGas_BasicData>
    {
        // GET: FishGas_Update
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<FishGas_BasicData> GetModelEntity()
        {
            return new ModelEntity<FishGas_BasicData>(new OilGasModelContextExt());
        }

        protected override IQueryable<FishGas_BasicData> BeforeIQueryToPagedList(IQueryable<FishGas_BasicData> iquery, params KeyValueParams[] paras)
        {
            //已開業的usestage
            var _db = new OilGasModelContextExt();
            var useStage = _db.UsageStateCode.Where(x => x.Type == "已開業").Select(x => x.Value).ToList();

            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return new List<FishGas_BasicData>().AsQueryable();
            }

            //權限查詢 (縣市權限，變動清除catch)
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysGSLs();


            iquery = iquery.Where(a => a.CaseNo != null && pCitys.Any(b => b == a.CaseNo.Substring(4, 2)) && useStage.Any(b => b == a.UsageState));

            return base.BeforeIQueryToPagedList(iquery, paras);
        }
    }
}