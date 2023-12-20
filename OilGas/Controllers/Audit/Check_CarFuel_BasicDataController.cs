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
    [Dou.Misc.Attr.MenuDef(Id = "Check_CarFuel_BasicData", Name = "加油站查核", MenuPath = "隱藏選單/新增查核結果", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Check_CarFuel_BasicDataController : APaginationModelController<Check_Item>
    {
        public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: Check_Item
        public ActionResult Index()
        {
            return View();
        }
        protected override IModelEntity<Check_Item> GetModelEntity()
        {
            return new ModelEntity<Check_Item>(new OilGasModelContextExt());
        }

        protected override IQueryable<Check_Item> BeforeIQueryToPagedList(IQueryable<Check_Item> iquery, params KeyValueParams[] paras)
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
            var iquery = db.Check_Item.Where(X => X.CheckNo == CheckNo).Take(1);

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



            options.singleDataEditCompletedReturnUrl = "/StatisticsAtatisticsAuditCarFuel?CheckNo=" + CheckNo + "&CaseNo=" + CaseNo;
            options.editformWindowStyle = "showEditformOnly";

            return options;
        }



        protected override void AddDBObject(IModelEntity<Check_Item> dbEntity, IEnumerable<Check_Item> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同

            objs = SUM(objs);


            puttoaction(objs);



            base.AddDBObject(dbEntity, objs);
         
        }

        protected override void UpdateDBObject(IModelEntity<Check_Item> dbEntity, IEnumerable<Check_Item> objs)
        {
            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同
            objs = SUM(objs);


            puttoaction(objs);



            base.UpdateDBObject(dbEntity, objs);
         
        }

        //把資料複製到action
        public void puttoaction(IEnumerable<Check_Item> objs)
        {
            if (objs.First().AllDoesmeet != 0)
            {



                Check_Item_Action Check_Item_Action = new Check_Item_Action()
                {
                    //id =  objs.First().id,
                    CheckNo = objs.First().CheckNo,
                    CheckDate = objs.First().CheckDate,
                    CaseNo = objs.First().CaseNo,
                    I00 = objs.First().I00,
                    J00 = objs.First().J00,
                    K00 = objs.First().K00,
                    L00 = objs.First().L00,
                    A01 = objs.First().A01,
                    A02 = objs.First().A02,
                    A03 = objs.First().A03,
                    A04 = objs.First().A04,
                    A05 = objs.First().A05,
                    A06 = objs.First().A06,
                    A_Notes = objs.First().A_Notes,
                    B01 = objs.First().B01,
                    B02 = objs.First().B02,
                    B03 = objs.First().B03,
                    B04 = objs.First().B04,
                    B05 = objs.First().B05,
                    B06 = objs.First().B06,
                    B07 = objs.First().B07,
                    B08 = objs.First().B08,
                    B09 = objs.First().B09,
                    B10 = objs.First().B10,
                    B_Notes = objs.First().B_Notes,
                    C01 = objs.First().C01,
                    C02 = objs.First().C02,
                    C03 = objs.First().C03,
                    C04 = objs.First().C04,
                    C05 = objs.First().C05,
                    C06 = objs.First().C06,
                    C07 = objs.First().C07,
                    C08 = objs.First().C08,
                    C09 = objs.First().C09,
                    C10 = objs.First().C10,
                    C11 = objs.First().C11,
                    C12 = objs.First().C12,
                    C13 = objs.First().C13,
                    C14 = objs.First().C14,
                    C_Notes = objs.First().C_Notes,
                    D01 = objs.First().D01,
                    D02 = objs.First().D02,
                    D03 = objs.First().D03,
                    D04 = objs.First().D04,
                    D05 = objs.First().D05,
                    D06 = objs.First().D06,
                    D07 = objs.First().D07,
                    D08 = objs.First().D08,
                    D09 = objs.First().D09,
                    D10 = objs.First().D10,
                    D11 = objs.First().D11,
                    D_Notes = objs.First().D_Notes,
                    E01 = objs.First().E01,
                    E02 = objs.First().E02,
                    E03 = objs.First().E03,
                    E_Notes = objs.First().E_Notes,
                    F01 = objs.First().F01,
                    F02 = objs.First().F02,
                    F03 = objs.First().F03,
                    F04 = objs.First().F04,
                    F05 = objs.First().F05,
                    F06 = objs.First().F06,
                    F07 = objs.First().F07,
                    F08 = objs.First().F08,
                    F09 = objs.First().F09,
                    F_Notes = objs.First().F_Notes,
                    G01 = objs.First().G01,
                    G02 = objs.First().G02,
                    G03 = objs.First().G03,
                    G04 = Int32.Parse(objs.First().G04),
                    G05 = Int32.Parse(objs.First().G05),
                    G06 = Int32.Parse(objs.First().G06),
                    G_Notes = objs.First().G_Notes,
                    H01 = objs.First().H01,
                    H01_Value = objs.First().H01_Value,
                    H02 = objs.First().H02,
                    H02_Value = objs.First().H02_Value,
                    H03 = objs.First().H03,
                    H03_Value = objs.First().H03_Value,
                    H04 = objs.First().H04,
                    H04_Value = objs.First().H04_Value,
                    H05 = objs.First().H05,
                    H05_Value = objs.First().H05_Value,
                    H_Notes = objs.First().H_Notes,
                    I01 = objs.First().I01,
                    I02 = objs.First().I02,
                    I03 = objs.First().I03,
                    I04 = objs.First().I04,
                    I05 = objs.First().I05,
                    I06 = objs.First().I06,
                    I07 = objs.First().I07,
                    I08 = objs.First().I08,
                    I09 = objs.First().I09,
                    I10 = objs.First().I10,
                    I_Notes = objs.First().I_Notes,
                    J01 = objs.First().J01,
                    J02 = objs.First().J02,
                    J03 = objs.First().J03,
                    J_Notes = objs.First().J_Notes,
                    K01 = objs.First().K01,
                    K02 = objs.First().K02,
                    K_Notes = objs.First().K_Notes,
                    L01 = objs.First().L01,
                    L02 = objs.First().L02,
                    L03 = objs.First().L03,
                    L_Notes = objs.First().L_Notes,
                    //M01 =  objs.First().M01,
                    //M_Notes =  objs.First().M_Notes,
                    Change = objs.First().Change,
                    A_Count = objs.First().A_Count,
                    A_Conform = objs.First().A_Conform,
                    A_Doesmeet = objs.First().A_Doesmeet,
                    B_Count = objs.First().B_Count,
                    B_Conform = objs.First().B_Conform,
                    B_Doesmeet = objs.First().B_Doesmeet,
                    C_Count = objs.First().C_Count,
                    C_Conform = objs.First().C_Conform,
                    C_Doesmeet = objs.First().C_Doesmeet,
                    C_Unable = objs.First().C_Unable,
                    D_Count = objs.First().D_Count,
                    D_Conform = objs.First().D_Conform,
                    D_Doesmeet = objs.First().D_Doesmeet,
                    E_Count = objs.First().E_Count,
                    E_Conform = objs.First().E_Conform,
                    E_Doesmeet = objs.First().E_Doesmeet,
                    F_Count = objs.First().F_Count,
                    F_Conform = objs.First().F_Conform,
                    F_Doesmeet = objs.First().F_Doesmeet,
                    G_Count = objs.First().G_Count,
                    G_Conform = objs.First().G_Conform,
                    G_Doesmeet = objs.First().G_Doesmeet,
                    H_Count = objs.First().H_Count,
                    H_Conform = objs.First().H_Conform,
                    H_Doesmeet = objs.First().H_Doesmeet,
                    H_Unable = objs.First().H_Unable,
                    I_Count = objs.First().I_Count,
                    I_Conform = objs.First().I_Conform,
                    I_Doesmeet = objs.First().I_Doesmeet,
                    J_Count = objs.First().J_Count,
                    J_Conform = objs.First().J_Conform,
                    J_Doesmeet = objs.First().J_Doesmeet,
                    K_Count = objs.First().K_Count,
                    K_Conform = objs.First().K_Conform,
                    K_Doesmeet = objs.First().K_Doesmeet,
                    L_Count = objs.First().L_Count,
                    L_Conform = objs.First().L_Conform,
                    L_Doesmeet = objs.First().L_Doesmeet,
                    //M_Count =  objs.First().M_Count,
                    //M_Conform =  objs.First().M_Conform,
                    //M_Doesmeet =  objs.First().M_Doesmeet,
                    AllCount = objs.First().AllCount,
                    AllConform = objs.First().AllConform,
                    AllDoesmeet = objs.First().AllDoesmeet,
                    AllUnable = objs.First().AllUnable,
                    B_Unable = objs.First().B_Unable,
                    D08_Value = objs.First().D08_Value,
                    I05_Value = objs.First().I05_Value


                };


                var Check_Item_ActionDB = db.Check_Item_Action.FirstOrDefault(p => p.CheckNo == Check_Item_Action.CheckNo);

                if (Check_Item_ActionDB != null)
                {
                    //更新
                    Check_Item_ActionDB = Check_Item_Action;
                }
                else
                {
                    //新增
                    db.Check_Item_Action.Add(Check_Item_Action);
                }

                db.SaveChanges();

            }

        }



        public IEnumerable<Check_Item> SUM(IEnumerable<Check_Item> objs)
        {

            objs.First().A_Count = (objs.First().A01 == "0" ? 1 : 0) + (objs.First().A02 == "0" ? 1 : 0) + (objs.First().A03 == "0" ? 1 : 0) + (objs.First().A04 == "0" ? 1 : 0) + (objs.First().A05 == "0" ? 1 : 0) + (objs.First().A06 == "0" ? 1 : 0);
            objs.First().A_Conform = (objs.First().A01 == "1" ? 1 : 0) + (objs.First().A02 == "1" ? 1 : 0) + (objs.First().A03 == "1" ? 1 : 0) + (objs.First().A04 == "1" ? 1 : 0) + (objs.First().A05 == "1" ? 1 : 0) + (objs.First().A06 == "1" ? 1 : 0);
            objs.First().A_Doesmeet = (objs.First().A01 == "2" ? 1 : 0) + (objs.First().A02 == "2" ? 1 : 0) + (objs.First().A03 == "2" ? 1 : 0) + (objs.First().A04 == "2" ? 1 : 0) + (objs.First().A05 == "2" ? 1 : 0) + (objs.First().A06 == "2" ? 1 : 0);
           

            objs.First().B_Count = (objs.First().B01 == "0" ? 1 : 0) + (objs.First().B02 == "0" ? 1 : 0) + (objs.First().B03 == "0" ? 1 : 0) + (objs.First().B04 == "0" ? 1 : 0) + (objs.First().B05 == "0" ? 1 : 0) + (objs.First().B06 == "0" ? 1 : 0) + (objs.First().B07 == "0" ? 1 : 0) + (objs.First().B08 == "0" ? 1 : 0) + (objs.First().B09 == "0" ? 1 : 0) + (objs.First().B10 == "0" ? 1 : 0) ;
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

            objs.First().E_Count = (objs.First().E01 == "0" ? 1 : 0) + (objs.First().E02 == "0" ? 1 : 0) + (objs.First().E03 == "0" ? 1 : 0) ;
            objs.First().E_Conform = (objs.First().E01 == "1" ? 1 : 0) + (objs.First().E02 == "1" ? 1 : 0) + (objs.First().E03 == "1" ? 1 : 0) ;
            objs.First().E_Doesmeet = (objs.First().E01 == "2" ? 1 : 0) + (objs.First().E02 == "2" ? 1 : 0) + (objs.First().E03 == "2" ? 1 : 0) ;
           
            objs.First().F_Count = (objs.First().F01 == "0" ? 1 : 0) + (objs.First().F02 == "0" ? 1 : 0) + (objs.First().F03 == "0" ? 1 : 0) + (objs.First().F04 == "0" ? 1 : 0) + (objs.First().F05 == "0" ? 1 : 0) + (objs.First().F06 == "0" ? 1 : 0) + (objs.First().F07 == "0" ? 1 : 0) + (objs.First().F08 == "0" ? 1 : 0) + (objs.First().F09 == "0" ? 1 : 0);
            objs.First().F_Conform = (objs.First().F01 == "1" ? 1 : 0) + (objs.First().F02 == "1" ? 1 : 0) + (objs.First().F03 == "1" ? 1 : 0) + (objs.First().F04 == "1" ? 1 : 0) + (objs.First().F05 == "1" ? 1 : 0) + (objs.First().F06 == "1" ? 1 : 0) + (objs.First().F07 == "1" ? 1 : 0) + (objs.First().F08 == "1" ? 1 : 0) + (objs.First().F09 == "1" ? 1 : 0);
            objs.First().F_Doesmeet = (objs.First().F01 == "2" ? 1 : 0) + (objs.First().F02 == "2" ? 1 : 0) + (objs.First().F03 == "2" ? 1 : 0) + (objs.First().F04 == "2" ? 1 : 0) + (objs.First().F05 == "2" ? 1 : 0) + (objs.First().F06 == "2" ? 1 : 0) + (objs.First().F07 == "2" ? 1 : 0) + (objs.First().F08 == "2" ? 1 : 0) + (objs.First().F09 == "2" ? 1 : 0);

            objs.First().G_Count = (objs.First().G01 == "0" ? 1 : 0) + (objs.First().G02 == "0" ? 1 : 0) + (objs.First().G03 == "0" ? 1 : 0) + (objs.First().G04 == "0" ? 1 : 0) + (objs.First().G05 == "0" ? 1 : 0) + (objs.First().G06 == "0" ? 1 : 0);
            objs.First().G_Conform = (objs.First().G01 == "1" ? 1 : 0) + (objs.First().G02 == "1" ? 1 : 0) + (objs.First().G03 == "1" ? 1 : 0) + (objs.First().G04 == "1" ? 1 : 0) + (objs.First().G05 == "1" ? 1 : 0) + (objs.First().G06 == "1" ? 1 : 0);
            objs.First().G_Doesmeet = (objs.First().G01 == "2" ? 1 : 0) + (objs.First().G02 == "2" ? 1 : 0) + (objs.First().G03 == "2" ? 1 : 0) + (objs.First().G04 == "2" ? 1 : 0) + (objs.First().G05 == "2" ? 1 : 0) + (objs.First().G06 == "2" ? 1 : 0);
        
            objs.First().H_Count = (objs.First().H01 == "0" ? 1 : 0) + (objs.First().H02 == "0" ? 1 : 0) + (objs.First().H03 == "0" ? 1 : 0) + (objs.First().H04 == "0" ? 1 : 0) + (objs.First().H05 == "0" ? 1 : 0) ;
            objs.First().H_Conform = (objs.First().H01 == "1" ? 1 : 0) + (objs.First().H02 == "1" ? 1 : 0) + (objs.First().H03 == "1" ? 1 : 0) + (objs.First().H04 == "1" ? 1 : 0) + (objs.First().H05 == "1" ? 1 : 0) ;
            objs.First().H_Doesmeet = (objs.First().H01 == "2" ? 1 : 0) + (objs.First().H02 == "2" ? 1 : 0) + (objs.First().H03 == "2" ? 1 : 0) + (objs.First().H04 == "2" ? 1 : 0) + (objs.First().H05 == "2" ? 1 : 0) ;
            objs.First().H_Unable = (objs.First().H01 == "3" ? 1 : 0) + (objs.First().H02 == "3" ? 1 : 0) + (objs.First().H03 == "3" ? 1 : 0) + (objs.First().H04 == "3" ? 1 : 0) + (objs.First().H05 == "3" ? 1 : 0) ;


            objs.First().I_Count = (objs.First().I01 == "0" ? 1 : 0) + (objs.First().I02 == "0" ? 1 : 0) + (objs.First().I03 == "0" ? 1 : 0) + (objs.First().I04 == "0" ? 1 : 0) + (objs.First().I05 == "0" ? 1 : 0) + (objs.First().I06 == "0" ? 1 : 0) + (objs.First().I07 == "0" ? 1 : 0) + (objs.First().I08 == "0" ? 1 : 0) + (objs.First().I09 == "0" ? 1 : 0) + (objs.First().I10 == "0" ? 1 : 0);
            objs.First().I_Conform = (objs.First().I01 == "1" ? 1 : 0) + (objs.First().I02 == "1" ? 1 : 0) + (objs.First().I03 == "1" ? 1 : 0) + (objs.First().I04 == "1" ? 1 : 0) + (objs.First().I05 == "1" ? 1 : 0) + (objs.First().I06 == "1" ? 1 : 0) + (objs.First().I07 == "1" ? 1 : 0) + (objs.First().I08 == "1" ? 1 : 0) + (objs.First().I09 == "1" ? 1 : 0) + (objs.First().I10 == "1" ? 1 : 0);
            objs.First().I_Doesmeet = (objs.First().I01 == "2" ? 1 : 0) + (objs.First().I02 == "2" ? 1 : 0) + (objs.First().I03 == "2" ? 1 : 0) + (objs.First().I04 == "2" ? 1 : 0) + (objs.First().I05 == "2" ? 1 : 0) + (objs.First().I06 == "2" ? 1 : 0) + (objs.First().I07 == "2" ? 1 : 0) + (objs.First().I08 == "2" ? 1 : 0) + (objs.First().I09 == "2" ? 1 : 0) + (objs.First().I10 == "2" ? 1 : 0);

            objs.First().J_Count = (objs.First().J01 == "0" ? 1 : 0) + (objs.First().J02 == "0" ? 1 : 0) + (objs.First().J03 == "0" ? 1 : 0) ;
            objs.First().J_Conform = (objs.First().J01 == "1" ? 1 : 0) + (objs.First().J02 == "1" ? 1 : 0) + (objs.First().J03 == "1" ? 1 : 0) ;
            objs.First().J_Doesmeet = (objs.First().J01 == "2" ? 1 : 0) + (objs.First().J02 == "2" ? 1 : 0) + (objs.First().J03 == "2" ? 1 : 0) ;
        

            objs.First().K_Count = (objs.First().K01 == "0" ? 1 : 0) + (objs.First().K02 == "0" ? 1 : 0) ;
            objs.First().K_Conform = (objs.First().K01 == "1" ? 1 : 0) + (objs.First().K02 == "1" ? 1 : 0);
            objs.First().K_Doesmeet = (objs.First().K01 == "2" ? 1 : 0) + (objs.First().K02 == "2" ? 1 : 0);
       

            objs.First().L_Count = (objs.First().L01 == "0" ? 1 : 0) + (objs.First().L02 == "0" ? 1 : 0) + (objs.First().L03 == "0" ? 1 : 0);
            objs.First().L_Conform = (objs.First().L01 == "1" ? 1 : 0) + (objs.First().L02 == "1" ? 1 : 0) + (objs.First().L03 == "1" ? 1 : 0) ;
            objs.First().L_Doesmeet = (objs.First().L01 == "2" ? 1 : 0) + (objs.First().L02 == "2" ? 1 : 0) + (objs.First().L03 == "2" ? 1 : 0);

            objs.First().AllCount = (objs.First().A_Count??0)+ (objs.First().B_Count ?? 0) + (objs.First().C_Count ?? 0) + (objs.First().D_Count ?? 0) + (objs.First().E_Count ?? 0) + (objs.First().F_Count ?? 0) + (objs.First().G_Count ?? 0) + (objs.First().H_Count ?? 0) + (objs.First().I_Count ?? 0) + (objs.First().J_Count ?? 0) + (objs.First().K_Count ?? 0) + (objs.First().L_Count ?? 0);
            objs.First().AllConform = (objs.First().A_Conform ?? 0) + (objs.First().B_Conform ?? 0) + (objs.First().C_Conform ?? 0) + (objs.First().D_Conform ?? 0) + (objs.First().E_Conform ?? 0) + (objs.First().F_Conform ?? 0) + (objs.First().G_Conform ?? 0) + (objs.First().H_Conform ?? 0) + (objs.First().I_Conform ?? 0) + (objs.First().J_Conform ?? 0) + (objs.First().K_Conform ?? 0) + (objs.First().L_Conform ?? 0);
            objs.First().AllDoesmeet = (objs.First().A_Doesmeet ?? 0) + (objs.First().B_Doesmeet ?? 0) + (objs.First().C_Doesmeet ?? 0) + (objs.First().D_Doesmeet ?? 0) + (objs.First().E_Doesmeet ?? 0) + (objs.First().F_Doesmeet ?? 0) + (objs.First().G_Doesmeet ?? 0) + (objs.First().H_Doesmeet ?? 0) + (objs.First().I_Doesmeet ?? 0) + (objs.First().J_Doesmeet ?? 0) + (objs.First().K_Doesmeet ?? 0) + (objs.First().L_Doesmeet ?? 0);
            objs.First().AllUnable = (objs.First().B_Unable ?? 0) + (objs.First().C_Unable ?? 0) + (objs.First().H_Unable ?? 0);

            return objs;

        }
    }
}