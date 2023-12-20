using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.GasLawBan
{
    [Dou.Misc.Attr.MenuDef(Id = "GasLawBan_StatisticReportPlace", Name = "年度取締處分案件查獲地點彙整表", MenuPath = "取締管理作業/F統計報表專區", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class GasLawBan_StatisticReportPlaceController : Controller
    {
        // GET: GasLawBan_StatisticReportPlace
        public ActionResult Index()
        {
            return View();
        }
    }
}