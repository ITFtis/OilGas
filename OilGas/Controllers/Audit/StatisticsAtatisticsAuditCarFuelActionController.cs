using Dou.Controllers;
using Dou.Misc;
using Dou.Models.DB;
using Newtonsoft.Json;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "StatisticsAtatisticsAuditCarFuelAction", Name = "加油站複查統計", MenuPath = "隱藏選單/新增複查結果", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class StatisticsAtatisticsAuditCarFuelActionController : APaginationModelController<Check_Item_Action>
    {
        public OilGasModelContextExt db = new OilGasModelContextExt();
        // GET: Check_Item
        public ActionResult Index()
        {
            return View();
        }
        protected override IModelEntity<Check_Item_Action> GetModelEntity()
        {
            return new ModelEntity<Check_Item_Action>(new OilGasModelContextExt());
        }
        protected override IQueryable<Check_Item_Action> BeforeIQueryToPagedList(IQueryable<Check_Item_Action> iquery, params KeyValueParams[] paras)
        {
            var CheckNo = Request.QueryString["CheckNo"];




            iquery = iquery.Where(X => X.CheckNo == CheckNo);
            return base.BeforeIQueryToPagedList(iquery, paras);
        }


        public override DataManagerOptions GetDataManagerOptions()
        {
            var options = base.GetDataManagerOptions();

            var visiblefield = new List<string>()
            {

        "A_Count",
        "A_Conform",
        "A_Doesmeet",
        "B_Count",
        "B_Conform",
        "B_Doesmeet",
        "C_Count",
        "C_Conform",
        "C_Doesmeet",
        "C_Unable",
        "D_Count",
        "D_Conform",
        "D_Doesmeet",
        "E_Count",
        "E_Conform",
        "E_Doesmeet",
        "F_Count",
        "F_Conform",
        "F_Doesmeet",
        "G_Count",
        "G_Conform",
        "G_Doesmeet",
        "H_Count",
        "H_Conform",
        "H_Doesmeet",
        "H_Unable",
        "I_Count",
        "I_Conform",
        "I_Doesmeet",
        "J_Count",
        "J_Conform",
        "J_Doesmeet",
        "K_Count",
        "K_Conform",
        "K_Doesmeet",
        "L_Count",
        "L_Conform",
        "L_Doesmeet",
       "AllCount",
       "AllConform",
         "AllDoesmeet",
       "AllUnable"
            };

            foreach (var data in options.fields)
            {
                if (visiblefield.Contains(data.field))
                {
                    data.visible = true;
                    data.visibleView = true;
                }
                else
                {
                    data.visible = false;
                    data.visibleEdit = false;
                    data.visibleView = false;
                }
            }



            return options;
        }




    }
}