using Dou.Controllers;
using Dou.Misc;
using Dou.Misc.Attr;
using Dou.Models.DB;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Info
{
    [Dou.Misc.Attr.MenuDef(Id = "Law_Math", Name = "相關法條", MenuPath = "隱藏選單/函釋資料庫維護", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Law_MathController : APaginationModelController<Law_Math>
    {
        // GET: Law_Math
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<Law_Math> GetModelEntity()
        {
            return new ModelEntity<Law_Math>(new OilGasModelContextExt());
        }
        protected override IQueryable<Law_Math> BeforeIQueryToPagedList(IQueryable<Law_Math> iquery, params KeyValueParams[] paras)
        {
            var LawData_FileName = Request.QueryString["LawData_FileName"];
            iquery = iquery.Where(X => X.LawMath_LawData_FileName == LawData_FileName);
            return base.BeforeIQueryToPagedList(iquery, paras);
        }
    }
}