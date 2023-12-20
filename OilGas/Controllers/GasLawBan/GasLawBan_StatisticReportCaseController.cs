using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.GasLawBan
{
    [Dou.Misc.Attr.MenuDef(Id = "GasLawBan_StatisticReportCase", Name = "年度取締違規經營石油案件彙整表", MenuPath = "取締管理作業/F統計報表專區", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class GasLawBan_StatisticReportCaseController : Controller
    {
        // GET: GasLawBan_StatisticReportCase
        public ActionResult Index()
        {
            return View();
        }
    }
}