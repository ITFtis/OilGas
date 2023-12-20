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
    [Dou.Misc.Attr.MenuDef(Id = "Check_Action_CarFuel_BasicData", Name = "加油站複查", MenuPath = "隱藏選單/新增複查結果", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Check_Action_CarFuel_BasicDataController : APaginationModelController<Check_Item_Action>
    {
        public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
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

            //非ADMIN帳號只能看自己縣市
            if (!Dou.Context.CurrentIsAdminUser && !basic.Permissions("admin"))
            {
                var CITYdata = Dou.Context.CurrentUser<User>().city.Split(',');
                iquery = iquery.ToList().Where(x => CITYdata.Contains(x.CITY)).AsQueryable();
            }


            iquery = iquery.Where(X => X.CheckNo == CheckNo);
            return base.BeforeIQueryToPagedList(iquery, paras);
        }

        public override DataManagerOptions GetDataManagerOptions()
        {
            var options = base.GetDataManagerOptions();





            //進頁面直接跳出編輯
            var CheckNo = Request.QueryString["CheckNo"];
            var CaseNo = Request.QueryString["CaseNo"];
            var iquery = db.Check_Item_Action.Where(X => X.CheckNo == CheckNo).Take(1);
            //非ADMIN帳號只能看自己縣市
            if (!Dou.Context.CurrentIsAdminUser && !basic.Permissions("admin"))
            {
                var CITYdata = Dou.Context.CurrentUser<User>().city.Split(',');
                iquery = iquery.ToList().Where(x => CITYdata.Contains(x.CITY)).AsQueryable();
            }
            options.datas = iquery;

            if (iquery.Count() >= 1)
            {
                options.singleDataEdit = true;
            }



            options.singleDataEditCompletedReturnUrl = "/StatisticsAtatisticsAuditCarFuelAction?CheckNo=" + CheckNo + "&CaseNo=" + CaseNo;
            options.editformWindowStyle = "showEditformOnly";

            return options;
        }



        protected override void AddDBObject(IModelEntity<Check_Item_Action> dbEntity, IEnumerable<Check_Item_Action> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同

            objs = SUM(objs);


      



            base.AddDBObject(dbEntity, objs);

        }

        protected override void UpdateDBObject(IModelEntity<Check_Item_Action> dbEntity, IEnumerable<Check_Item_Action> objs)
        {
            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同
            objs = SUM(objs);


           



            base.UpdateDBObject(dbEntity, objs);

        }

      


        public IEnumerable<Check_Item_Action> SUM(IEnumerable<Check_Item_Action> objs)
        {

            objs.First().A_Count = (objs.First().A01 == "0" ? 1 : 0) + (objs.First().A02 == "0" ? 1 : 0) + (objs.First().A03 == "0" ? 1 : 0) + (objs.First().A04 == "0" ? 1 : 0) + (objs.First().A05 == "0" ? 1 : 0) + (objs.First().A06 == "0" ? 1 : 0);
            objs.First().A_Conform = (objs.First().A01 == "1" ? 1 : 0) + (objs.First().A02 == "1" ? 1 : 0) + (objs.First().A03 == "1" ? 1 : 0) + (objs.First().A04 == "1" ? 1 : 0) + (objs.First().A05 == "1" ? 1 : 0) + (objs.First().A06 == "1" ? 1 : 0);
            objs.First().A_Doesmeet = (objs.First().A01 == "2" ? 1 : 0) + (objs.First().A02 == "2" ? 1 : 0) + (objs.First().A03 == "2" ? 1 : 0) + (objs.First().A04 == "2" ? 1 : 0) + (objs.First().A05 == "2" ? 1 : 0) + (objs.First().A06 == "2" ? 1 : 0);


            objs.First().B_Count = (objs.First().B01 == "0" ? 1 : 0) + (objs.First().B02 == "0" ? 1 : 0) + (objs.First().B03 == "0" ? 1 : 0) + (objs.First().B04 == "0" ? 1 : 0) + (objs.First().B05 == "0" ? 1 : 0) + (objs.First().B06 == "0" ? 1 : 0) + (objs.First().B07 == "0" ? 1 : 0) + (objs.First().B08 == "0" ? 1 : 0) + (objs.First().B09 == "0" ? 1 : 0) + (objs.First().B10 == "0" ? 1 : 0);
            objs.First().B_Conform = (objs.First().B01 == "1" ? 1 : 0) + (objs.First().B02 == "1" ? 1 : 0) + (objs.First().B03 == "1" ? 1 : 0) + (objs.First().B04 == "1" ? 1 : 0) + (objs.First().B05 == "1" ? 1 : 0) + (objs.First().B06 == "1" ? 1 : 0) + (objs.First().B07 == "1" ? 1 : 0) + (objs.First().B08 == "1" ? 1 : 0) + (objs.First().B09 == "1" ? 1 : 0) + (objs.First().B10 == "1" ? 1 : 0);
            objs.First().B_Doesmeet = (objs.First().B01 == "2" ? 1 : 0) + (objs.First().B02 == "2" ? 1 : 0) + (objs.First().B03 == "2" ? 1 : 0) + (objs.First().B04 == "2" ? 1 : 0) + (objs.First().B05 == "2" ? 1 : 0) + (objs.First().B06 == "2" ? 1 : 0) + (objs.First().B07 == "2" ? 1 : 0) + (objs.First().B08 == "2" ? 1 : 0) + (objs.First().B09 == "2" ? 1 : 0) + (objs.First().B10 == "2" ? 1 : 0);
            objs.First().B_Unable = (objs.First().B01 == "3" ? 1 : 0) + (objs.First().B02 == "3" ? 1 : 0) + (objs.First().B03 == "3" ? 1 : 0) + (objs.First().B04 == "3" ? 1 : 0) + (objs.First().B05 == "3" ? 1 : 0) + (objs.First().B06 == "3" ? 1 : 0) + (objs.First().B07 == "3" ? 1 : 0) + (objs.First().B08 == "3" ? 1 : 0) + (objs.First().B09 == "3" ? 1 : 0) + (objs.First().B10 == "3" ? 1 : 0);

            objs.First().C_Count = (objs.First().C01 == "0" ? 1 : 0) + (objs.First().C02 == "0" ? 1 : 0) + (objs.First().C03 == "0" ? 1 : 0) + (objs.First().C04 == "0" ? 1 : 0) + (objs.First().C05 == "0" ? 1 : 0) + (objs.First().C06 == "0" ? 1 : 0) + (objs.First().C07 == "0" ? 1 : 0) + (objs.First().C08 == "0" ? 1 : 0) + (objs.First().C09 == "0" ? 1 : 0) + (objs.First().C10 == "0" ? 1 : 0) + (objs.First().C11 == "0" ? 1 : 0) + (objs.First().C12 == "0" ? 1 : 0) + (objs.First().C13 == "0" ? 1 : 0) + (objs.First().C14 == "0" ? 1 : 0);
            objs.First().C_Conform = (objs.First().C01 == "1" ? 1 : 0) + (objs.First().C02 == "1" ? 1 : 0) + (objs.First().C03 == "1" ? 1 : 0) + (objs.First().C04 == "1" ? 1 : 0) + (objs.First().C05 == "1" ? 1 : 0) + (objs.First().C06 == "1" ? 1 : 0) + (objs.First().C07 == "1" ? 1 : 0) + (objs.First().C08 == "1" ? 1 : 0) + (objs.First().C09 == "1" ? 1 : 0) + (objs.First().C10 == "1" ? 1 : 0) + (objs.First().C11 == "1" ? 1 : 0) + (objs.First().C12 == "1" ? 1 : 0) + (objs.First().C13 == "1" ? 1 : 0) + (objs.First().C14 == "1" ? 1 : 0);
            objs.First().C_Doesmeet = (objs.First().C01 == "2" ? 1 : 0) + (objs.First().C02 == "2" ? 1 : 0) + (objs.First().C03 == "2" ? 1 : 0) + (objs.First().C04 == "2" ? 1 : 0) + (objs.First().C05 == "2" ? 1 : 0) + (objs.First().C06 == "2" ? 1 : 0) + (objs.First().C07 == "2" ? 1 : 0) + (objs.First().C08 == "2" ? 1 : 0) + (objs.First().C09 == "2" ? 1 : 0) + (objs.First().C10 == "2" ? 1 : 0) + (objs.First().C11 == "2" ? 1 : 0) + (objs.First().C12 == "2" ? 1 : 0) + (objs.First().C13 == "2" ? 1 : 0) + (objs.First().C14 == "2" ? 1 : 0);
            objs.First().C_Unable = (objs.First().C01 == "3" ? 1 : 0) + (objs.First().C02 == "3" ? 1 : 0) + (objs.First().C03 == "3" ? 1 : 0) + (objs.First().C04 == "3" ? 1 : 0) + (objs.First().C05 == "3" ? 1 : 0) + (objs.First().C06 == "3" ? 1 : 0) + (objs.First().C07 == "3" ? 1 : 0) + (objs.First().C08 == "3" ? 1 : 0) + (objs.First().C09 == "3" ? 1 : 0) + (objs.First().C10 == "3" ? 1 : 0) + (objs.First().C11 == "3" ? 1 : 0) + (objs.First().C12 == "3" ? 1 : 0) + (objs.First().C13 == "3" ? 1 : 0) + (objs.First().C14 == "3" ? 1 : 0);


            objs.First().D_Count = (objs.First().D01 == "0" ? 1 : 0) + (objs.First().D02 == "0" ? 1 : 0) + (objs.First().D03 == "0" ? 1 : 0) + (objs.First().D04 == "0" ? 1 : 0) + (objs.First().D05 == "0" ? 1 : 0) + (objs.First().D06 == "0" ? 1 : 0) + (objs.First().D07 == "0" ? 1 : 0) + (objs.First().D08 == "0" ? 1 : 0) + (objs.First().D09 == "0" ? 1 : 0) + (objs.First().D10 == "0" ? 1 : 0) + (objs.First().D11 == "0" ? 1 : 0);
            objs.First().D_Conform = (objs.First().D01 == "1" ? 1 : 0) + (objs.First().D02 == "1" ? 1 : 0) + (objs.First().D03 == "1" ? 1 : 0) + (objs.First().D04 == "1" ? 1 : 0) + (objs.First().D05 == "1" ? 1 : 0) + (objs.First().D06 == "1" ? 1 : 0) + (objs.First().D07 == "1" ? 1 : 0) + (objs.First().D08 == "1" ? 1 : 0) + (objs.First().D09 == "1" ? 1 : 0) + (objs.First().D10 == "1" ? 1 : 0) + (objs.First().D11 == "1" ? 1 : 0);
            objs.First().D_Doesmeet = (objs.First().D01 == "2" ? 1 : 0) + (objs.First().D02 == "2" ? 1 : 0) + (objs.First().D03 == "2" ? 1 : 0) + (objs.First().D04 == "2" ? 1 : 0) + (objs.First().D05 == "2" ? 1 : 0) + (objs.First().D06 == "2" ? 1 : 0) + (objs.First().D07 == "2" ? 1 : 0) + (objs.First().D08 == "2" ? 1 : 0) + (objs.First().D09 == "2" ? 1 : 0) + (objs.First().D10 == "2" ? 1 : 0) + (objs.First().D11 == "2" ? 1 : 0);

            objs.First().E_Count = (objs.First().E01 == "0" ? 1 : 0) + (objs.First().E02 == "0" ? 1 : 0) + (objs.First().E03 == "0" ? 1 : 0);
            objs.First().E_Conform = (objs.First().E01 == "1" ? 1 : 0) + (objs.First().E02 == "1" ? 1 : 0) + (objs.First().E03 == "1" ? 1 : 0);
            objs.First().E_Doesmeet = (objs.First().E01 == "2" ? 1 : 0) + (objs.First().E02 == "2" ? 1 : 0) + (objs.First().E03 == "2" ? 1 : 0);

            objs.First().F_Count = (objs.First().F01 == "0" ? 1 : 0) + (objs.First().F02 == "0" ? 1 : 0) + (objs.First().F03 == "0" ? 1 : 0) + (objs.First().F04 == "0" ? 1 : 0) + (objs.First().F05 == "0" ? 1 : 0) + (objs.First().F06 == "0" ? 1 : 0) + (objs.First().F07 == "0" ? 1 : 0) + (objs.First().F08 == "0" ? 1 : 0) + (objs.First().F09 == "0" ? 1 : 0);
            objs.First().F_Conform = (objs.First().F01 == "1" ? 1 : 0) + (objs.First().F02 == "1" ? 1 : 0) + (objs.First().F03 == "1" ? 1 : 0) + (objs.First().F04 == "1" ? 1 : 0) + (objs.First().F05 == "1" ? 1 : 0) + (objs.First().F06 == "1" ? 1 : 0) + (objs.First().F07 == "1" ? 1 : 0) + (objs.First().F08 == "1" ? 1 : 0) + (objs.First().F09 == "1" ? 1 : 0);
            objs.First().F_Doesmeet = (objs.First().F01 == "2" ? 1 : 0) + (objs.First().F02 == "2" ? 1 : 0) + (objs.First().F03 == "2" ? 1 : 0) + (objs.First().F04 == "2" ? 1 : 0) + (objs.First().F05 == "2" ? 1 : 0) + (objs.First().F06 == "2" ? 1 : 0) + (objs.First().F07 == "2" ? 1 : 0) + (objs.First().F08 == "2" ? 1 : 0) + (objs.First().F09 == "2" ? 1 : 0);

            objs.First().G_Count = (objs.First().G01 == "0" ? 1 : 0) + (objs.First().G02 == "0" ? 1 : 0) + (objs.First().G03 == "0" ? 1 : 0) + (objs.First().G04 == 0 ? 1 : 0) + (objs.First().G05 == 0 ? 1 : 0) + (objs.First().G06 == 0 ? 1 : 0);
            objs.First().G_Conform = (objs.First().G01 == "1" ? 1 : 0) + (objs.First().G02 == "1" ? 1 : 0) + (objs.First().G03 == "1" ? 1 : 0) + (objs.First().G04 == 1 ? 1 : 0) + (objs.First().G05 == 1 ? 1 : 0) + (objs.First().G06 == 1 ? 1 : 0);
            objs.First().G_Doesmeet = (objs.First().G01 == "2" ? 1 : 0) + (objs.First().G02 == "2" ? 1 : 0) + (objs.First().G03 == "2" ? 1 : 0) + (objs.First().G04 == 2 ? 1 : 0) + (objs.First().G05 == 2 ? 1 : 0) + (objs.First().G06 == 2 ? 1 : 0);

            objs.First().H_Count = (objs.First().H01 == "0" ? 1 : 0) + (objs.First().H02 == "0" ? 1 : 0) + (objs.First().H03 == "0" ? 1 : 0) + (objs.First().H04 == "0" ? 1 : 0) + (objs.First().H05 == "0" ? 1 : 0);
            objs.First().H_Conform = (objs.First().H01 == "1" ? 1 : 0) + (objs.First().H02 == "1" ? 1 : 0) + (objs.First().H03 == "1" ? 1 : 0) + (objs.First().H04 == "1" ? 1 : 0) + (objs.First().H05 == "1" ? 1 : 0);
            objs.First().H_Doesmeet = (objs.First().H01 == "2" ? 1 : 0) + (objs.First().H02 == "2" ? 1 : 0) + (objs.First().H03 == "2" ? 1 : 0) + (objs.First().H04 == "2" ? 1 : 0) + (objs.First().H05 == "2" ? 1 : 0);
            objs.First().H_Unable = (objs.First().H01 == "3" ? 1 : 0) + (objs.First().H02 == "3" ? 1 : 0) + (objs.First().H03 == "3" ? 1 : 0) + (objs.First().H04 == "3" ? 1 : 0) + (objs.First().H05 == "3" ? 1 : 0);


            objs.First().I_Count = (objs.First().I01 == "0" ? 1 : 0) + (objs.First().I02 == "0" ? 1 : 0) + (objs.First().I03 == "0" ? 1 : 0) + (objs.First().I04 == "0" ? 1 : 0) + (objs.First().I05 == "0" ? 1 : 0) + (objs.First().I06 == "0" ? 1 : 0) + (objs.First().I07 == "0" ? 1 : 0) + (objs.First().I08 == "0" ? 1 : 0) + (objs.First().I09 == "0" ? 1 : 0) + (objs.First().I10 == "0" ? 1 : 0);
            objs.First().I_Conform = (objs.First().I01 == "1" ? 1 : 0) + (objs.First().I02 == "1" ? 1 : 0) + (objs.First().I03 == "1" ? 1 : 0) + (objs.First().I04 == "1" ? 1 : 0) + (objs.First().I05 == "1" ? 1 : 0) + (objs.First().I06 == "1" ? 1 : 0) + (objs.First().I07 == "1" ? 1 : 0) + (objs.First().I08 == "1" ? 1 : 0) + (objs.First().I09 == "1" ? 1 : 0) + (objs.First().I10 == "1" ? 1 : 0);
            objs.First().I_Doesmeet = (objs.First().I01 == "2" ? 1 : 0) + (objs.First().I02 == "2" ? 1 : 0) + (objs.First().I03 == "2" ? 1 : 0) + (objs.First().I04 == "2" ? 1 : 0) + (objs.First().I05 == "2" ? 1 : 0) + (objs.First().I06 == "2" ? 1 : 0) + (objs.First().I07 == "2" ? 1 : 0) + (objs.First().I08 == "2" ? 1 : 0) + (objs.First().I09 == "2" ? 1 : 0) + (objs.First().I10 == "2" ? 1 : 0);

            objs.First().J_Count = (objs.First().J01 == "0" ? 1 : 0) + (objs.First().J02 == "0" ? 1 : 0) + (objs.First().J03 == "0" ? 1 : 0);
            objs.First().J_Conform = (objs.First().J01 == "1" ? 1 : 0) + (objs.First().J02 == "1" ? 1 : 0) + (objs.First().J03 == "1" ? 1 : 0);
            objs.First().J_Doesmeet = (objs.First().J01 == "2" ? 1 : 0) + (objs.First().J02 == "2" ? 1 : 0) + (objs.First().J03 == "2" ? 1 : 0);


            objs.First().K_Count = (objs.First().K01 == "0" ? 1 : 0) + (objs.First().K02 == "0" ? 1 : 0);
            objs.First().K_Conform = (objs.First().K01 == "1" ? 1 : 0) + (objs.First().K02 == "1" ? 1 : 0);
            objs.First().K_Doesmeet = (objs.First().K01 == "2" ? 1 : 0) + (objs.First().K02 == "2" ? 1 : 0);


            objs.First().L_Count = (objs.First().L01 == "0" ? 1 : 0) + (objs.First().L02 == "0" ? 1 : 0) + (objs.First().L03 == "0" ? 1 : 0);
            objs.First().L_Conform = (objs.First().L01 == "1" ? 1 : 0) + (objs.First().L02 == "1" ? 1 : 0) + (objs.First().L03 == "1" ? 1 : 0);
            objs.First().L_Doesmeet = (objs.First().L01 == "2" ? 1 : 0) + (objs.First().L02 == "2" ? 1 : 0) + (objs.First().L03 == "2" ? 1 : 0);

            objs.First().AllCount = (objs.First().A_Count ?? 0) + (objs.First().B_Count ?? 0) + (objs.First().C_Count ?? 0) + (objs.First().D_Count ?? 0) + (objs.First().E_Count ?? 0) + (objs.First().F_Count ?? 0) + (objs.First().G_Count ?? 0) + (objs.First().H_Count ?? 0) + (objs.First().I_Count ?? 0) + (objs.First().J_Count ?? 0) + (objs.First().K_Count ?? 0) + (objs.First().L_Count ?? 0);
            objs.First().AllConform = (objs.First().A_Conform ?? 0) + (objs.First().B_Conform ?? 0) + (objs.First().C_Conform ?? 0) + (objs.First().D_Conform ?? 0) + (objs.First().E_Conform ?? 0) + (objs.First().F_Conform ?? 0) + (objs.First().G_Conform ?? 0) + (objs.First().H_Conform ?? 0) + (objs.First().I_Conform ?? 0) + (objs.First().J_Conform ?? 0) + (objs.First().K_Conform ?? 0) + (objs.First().L_Conform ?? 0);
            objs.First().AllDoesmeet = (objs.First().A_Doesmeet ?? 0) + (objs.First().B_Doesmeet ?? 0) + (objs.First().C_Doesmeet ?? 0) + (objs.First().D_Doesmeet ?? 0) + (objs.First().E_Doesmeet ?? 0) + (objs.First().F_Doesmeet ?? 0) + (objs.First().G_Doesmeet ?? 0) + (objs.First().H_Doesmeet ?? 0) + (objs.First().I_Doesmeet ?? 0) + (objs.First().J_Doesmeet ?? 0) + (objs.First().K_Doesmeet ?? 0) + (objs.First().L_Doesmeet ?? 0);
            objs.First().AllUnable = (objs.First().B_Unable ?? 0) + (objs.First().C_Unable ?? 0) + (objs.First().H_Unable ?? 0);

            return objs;

        }
    }
}