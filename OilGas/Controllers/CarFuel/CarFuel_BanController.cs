using DocumentFormat.OpenXml.Bibliography;
using Dou.Controllers;
using Dou.Misc;
using Dou.Models.DB;
using DouHelper;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "CarFuel_Ban", Name = "違規案件資料查詢", MenuPath = "加油站/A違規案件專區", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class CarFuel_BanController : APaginationModelController<CarFuel_Ban>
    {
        // GET: CarFuel_Ban
        public ActionResult Index()
        {
            return View();
        }

        protected override IQueryable<CarFuel_Ban> BeforeIQueryToPagedList(IQueryable<CarFuel_Ban> iquery, params KeyValueParams[] paras)
        {
            //用站名查出caseNo 因為虛擬欄位無法直接查詢
            List<string> caseNo = new List<string>();
            var _db = new OilGasModelContextExt();
            var gasName = HelperUtilities.GetFilterParaValue(paras, "Name");
            gasName = gasName != null ? gasName.Trim() : gasName;
            var city = HelperUtilities.GetFilterParaValue(paras, "CITY");

            if (!string.IsNullOrEmpty(gasName))
                caseNo = _db.CarFuel_BasicData.Where(x => x.Gas_Name.Contains(gasName)).Select(x => x.CaseNo).ToList();

            

            //權限查詢 (縣市權限，變動清除catch)
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysGSLs();

            if(!string.IsNullOrEmpty(city))
                pCitys = city.Split(',').ToList();

            var query = iquery.Where(a => a.CaseNo != null && pCitys.Any(b => b == a.CaseNo.Substring(4, 2)));

            if (caseNo.Count > 0)
            {
                query = query.Where(a => caseNo.Any(b => a.CaseNo == b));
            }
            else if (!string.IsNullOrEmpty(gasName) && caseNo.Count == 0)
            {
                return new List<CarFuel_Ban>().AsQueryable();
            }
                

            return base.BeforeIQueryToPagedList(query, paras);
        }

        protected override IModelEntity<CarFuel_Ban> GetModelEntity()
        {
            return new ModelEntity<CarFuel_Ban>(new OilGasModelContextExt());
        }

        public override DataManagerOptions GetDataManagerOptions()
        {
            var option = base.GetDataManagerOptions();
            foreach (var o in option.fields)
            {
                o.align = "center";
            }


            return option;
        }
    }
}