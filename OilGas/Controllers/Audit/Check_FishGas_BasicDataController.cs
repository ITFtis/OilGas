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
    [Dou.Misc.Attr.MenuDef(Id = "Check_FishGas_BasicData", Name = "漁船查核", MenuPath = "隱藏選單/新增查核結果", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Check_FishGas_BasicDataController : APaginationModelController<Check_Item_Fish>
    {
        public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: Check_Item
        public ActionResult Index()
        {
            return View();
        }
        protected override IModelEntity<Check_Item_Fish> GetModelEntity()
        {
            return new ModelEntity<Check_Item_Fish>(new OilGasModelContextExt());
        }

        protected override IQueryable<Check_Item_Fish> BeforeIQueryToPagedList(IQueryable<Check_Item_Fish> iquery, params KeyValueParams[] paras)
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
            var iquery = db.Check_Item_Fish.Where(X => X.CheckNo == CheckNo).Take(1);
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



            options.singleDataEditCompletedReturnUrl = "/StatisticsAtatisticsAuditFish?CheckNo=" + CheckNo + "&CaseNo=" + CaseNo;
            options.editformWindowStyle = "showEditformOnly";

            return options;
        }



        protected override void AddDBObject(IModelEntity<Check_Item_Fish> dbEntity, IEnumerable<Check_Item_Fish> objs)
        {

            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同

            objs = SUM(objs);


            puttoaction(objs);



            base.AddDBObject(dbEntity, objs);

        }

        protected override void UpdateDBObject(IModelEntity<Check_Item_Fish> dbEntity, IEnumerable<Check_Item_Fish> objs)
        {
            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同
            objs = SUM(objs);

            puttoaction(objs);

            base.UpdateDBObject(dbEntity, objs);

        }


        //把資料複製到action
        public void puttoaction(IEnumerable<Check_Item_Fish> objs)
        {
            if (objs.First().AllDoesmeet != 0)
            {



                Check_Item_Fish_Action Check_Item_Fish_Action = new Check_Item_Fish_Action()
                {
                    //id =  objs.First().id,
                    CheckNo = objs.First().CheckNo,
                    CheckDate = objs.First().CheckDate,
                    CaseNo = objs.First().CaseNo,
                  
                    A01 = objs.First().A01,
                    A02 = objs.First().A02,
                 
                    A_Notes = objs.First().A_Notes,
                    B01 = objs.First().B01,
                    B02 = objs.First().B02,
                   
                    B_Notes = objs.First().B_Notes,
                    C01 = objs.First().C01,
                    C02 = objs.First().C02,
                    C03 = objs.First().C03,
                
                    C_Notes = objs.First().C_Notes,
                    D01 = objs.First().D01,
                    D02 = objs.First().D02,
                    D03 = objs.First().D03,
                  
                    D_Notes = objs.First().D_Notes,
                    E01 = objs.First().E01,
                  
                    E_Notes = objs.First().E_Notes,
                    F01 = objs.First().F01,
                    F02 = objs.First().F02,
                    F03 = objs.First().F03,
                    F04 = objs.First().F04,
                    F05 = objs.First().F05,
                
                    F_Notes = objs.First().F_Notes,
                    G01 = objs.First().G01,
                    G02 = objs.First().G02,
                    G03 = objs.First().G03,
                    G04 = objs.First().G04,
               
                    G_Notes = objs.First().G_Notes,
                    H01 = objs.First().H01,
                    H01_Value = objs.First().H01_Value,
                    H02 = objs.First().H02,
                    H02_Value = objs.First().H02_Value,
                    H03 = objs.First().H03,
                    H03_Value = objs.First().H03_Value,
                  
                    H_Notes = objs.First().H_Notes,
                  
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
                  
                    AllCount = objs.First().AllCount,
                    AllConform = objs.First().AllConform,
                    AllDoesmeet = objs.First().AllDoesmeet,
                    AllUnable = objs.First().AllUnable,
                    B_Unable = objs.First().B_Unable,

                    F_Unable = objs.First().F_Unable,
                    D_Unable = objs.First().D_Unable,
                    E_Unable = objs.First().E_Unable,
                    A_Unable = objs.First().A_Unable,
                    G_Unable = objs.First().G_Unable,
                };


                var Check_Item_Fish_ActionDB = db.Check_Item_Fish_Action.FirstOrDefault(p => p.CheckNo == Check_Item_Fish_Action.CheckNo);

                if (Check_Item_Fish_ActionDB != null)
                {
                    //更新
                    Check_Item_Fish_ActionDB = Check_Item_Fish_Action;
                }
                else
                {
                    //新增
                    db.Check_Item_Fish_Action.Add(Check_Item_Fish_Action);
                }

                db.SaveChanges();

            }

        }




        public IEnumerable<Check_Item_Fish> SUM(IEnumerable<Check_Item_Fish> objs)
        {

            objs.First().A_Count = (objs.First().A01 == "0" ? 1 : 0) + (objs.First().A02 == "0" ? 1 : 0);
            objs.First().A_Conform = (objs.First().A01 == "1" ? 1 : 0) + (objs.First().A02 == "1" ? 1 : 0);
            objs.First().A_Doesmeet = (objs.First().A01 == "2" ? 1 : 0) + (objs.First().A02 == "2" ? 1 : 0);
            objs.First().A_Unable = (objs.First().A01 == "3" ? 1 : 0) + (objs.First().A02 == "3" ? 1 : 0);

            objs.First().B_Count = (objs.First().B01 == "0" ? 1 : 0) + (objs.First().B02 == "0" ? 1 : 0) ;
            objs.First().B_Conform = (objs.First().B01 == "1" ? 1 : 0) + (objs.First().B02 == "1" ? 1 : 0);
            objs.First().B_Doesmeet = (objs.First().B01 == "2" ? 1 : 0) + (objs.First().B02 == "2" ? 1 : 0);
            objs.First().B_Unable = (objs.First().B01 == "3" ? 1 : 0) + (objs.First().B02 == "3" ? 1 : 0);

            objs.First().C_Count = (objs.First().C01 == "0" ? 1 : 0) + (objs.First().C02 == "0" ? 1 : 0) + (objs.First().C03 == "0" ? 1 : 0);
            objs.First().C_Conform = (objs.First().C01 == "1" ? 1 : 0) + (objs.First().C02 == "1" ? 1 : 0) + (objs.First().C03 == "1" ? 1 : 0);
            objs.First().C_Doesmeet = (objs.First().C01 == "2" ? 1 : 0) + (objs.First().C02 == "2" ? 1 : 0) + (objs.First().C03 == "2" ? 1 : 0) ;
            objs.First().C_Unable = (objs.First().C01 == "3" ? 1 : 0) + (objs.First().C02 == "3" ? 1 : 0) + (objs.First().C03 == "3" ? 1 : 0) ;


            objs.First().D_Count = (objs.First().D01 == "0" ? 1 : 0) + (objs.First().D02 == "0" ? 1 : 0) + (objs.First().D03 == "0" ? 1 : 0) ;
            objs.First().D_Conform = (objs.First().D01 == "1" ? 1 : 0) + (objs.First().D02 == "1" ? 1 : 0) + (objs.First().D03 == "1" ? 1 : 0) ;
            objs.First().D_Doesmeet = (objs.First().D01 == "2" ? 1 : 0) + (objs.First().D02 == "2" ? 1 : 0) + (objs.First().D03 == "2" ? 1 : 0) ;
            objs.First().D_Unable = (objs.First().D01 == "3" ? 1 : 0) + (objs.First().D02 == "3" ? 1 : 0) + (objs.First().D03 == "3" ? 1 : 0) ;

            objs.First().E_Count = (objs.First().E01 == "0" ? 1 : 0) ;
            objs.First().E_Conform = (objs.First().E01 == "1" ? 1 : 0) ;
            objs.First().E_Doesmeet = (objs.First().E01 == "2" ? 1 : 0) ;
            objs.First().E_Unable = (objs.First().E01 == "3" ? 1 : 0);


            objs.First().F_Count = (objs.First().F01 == "0" ? 1 : 0) + (objs.First().F02 == "0" ? 1 : 0) + (objs.First().F03 == "0" ? 1 : 0) + (objs.First().F04 == "0" ? 1 : 0) + (objs.First().F05 == "0" ? 1 : 0) ;
            objs.First().F_Conform = (objs.First().F01 == "1" ? 1 : 0) + (objs.First().F02 == "1" ? 1 : 0) + (objs.First().F03 == "1" ? 1 : 0) + (objs.First().F04 == "1" ? 1 : 0) + (objs.First().F05 == "1" ? 1 : 0);
            objs.First().F_Doesmeet = (objs.First().F01 == "2" ? 1 : 0) + (objs.First().F02 == "2" ? 1 : 0) + (objs.First().F03 == "2" ? 1 : 0) + (objs.First().F04 == "2" ? 1 : 0) + (objs.First().F05 == "2" ? 1 : 0);
            objs.First().F_Unable = (objs.First().F01 == "3" ? 1 : 0) + (objs.First().F02 == "3" ? 1 : 0) + (objs.First().F03 == "3" ? 1 : 0) + (objs.First().F04 == "3" ? 1 : 0) + (objs.First().F05 == "3" ? 1 : 0);



            objs.First().G_Count = (objs.First().G01 == "0" ? 1 : 0) + (objs.First().G02 == "0" ? 1 : 0) + (objs.First().G03 == "0" ? 1 : 0) + (objs.First().G04 == "0" ? 1 : 0);
            objs.First().G_Conform = (objs.First().G01 == "1" ? 1 : 0) + (objs.First().G02 == "1" ? 1 : 0) + (objs.First().G03 == "1" ? 1 : 0) + (objs.First().G04 == "1" ? 1 : 0) ;
            objs.First().G_Doesmeet = (objs.First().G01 == "2" ? 1 : 0) + (objs.First().G02 == "2" ? 1 : 0) + (objs.First().G03 == "2" ? 1 : 0) + (objs.First().G04 == "2" ? 1 : 0);
            objs.First().G_Unable = (objs.First().G01 == "3" ? 1 : 0) + (objs.First().G02 == "3" ? 1 : 0) + (objs.First().G03 == "3" ? 1 : 0) + (objs.First().G04 == "3" ? 1 : 0);

            objs.First().H_Count = (objs.First().H01 == "0" ? 1 : 0) + (objs.First().H02 == "0" ? 1 : 0) + (objs.First().H03 == "0" ? 1 : 0);
            objs.First().H_Conform = (objs.First().H01 == "1" ? 1 : 0) + (objs.First().H02 == "1" ? 1 : 0) + (objs.First().H03 == "1" ? 1 : 0) ;
            objs.First().H_Doesmeet = (objs.First().H01 == "2" ? 1 : 0) + (objs.First().H02 == "2" ? 1 : 0) + (objs.First().H03 == "2" ? 1 : 0);
            objs.First().H_Unable = (objs.First().H01 == "3" ? 1 : 0) + (objs.First().H02 == "3" ? 1 : 0) + (objs.First().H03 == "3" ? 1 : 0);


            

            objs.First().AllCount = (objs.First().A_Count ?? 0) + (objs.First().B_Count ?? 0) + (objs.First().C_Count ?? 0) + (objs.First().D_Count ?? 0) + (objs.First().E_Count ?? 0) + (objs.First().F_Count ?? 0) + (objs.First().G_Count ?? 0) + (objs.First().H_Count ?? 0) ;
            objs.First().AllConform = (objs.First().A_Conform ?? 0) + (objs.First().B_Conform ?? 0) + (objs.First().C_Conform ?? 0) + (objs.First().D_Conform ?? 0) + (objs.First().E_Conform ?? 0) + (objs.First().F_Conform ?? 0) + (objs.First().G_Conform ?? 0) + (objs.First().H_Conform ?? 0);
            objs.First().AllDoesmeet = (objs.First().A_Doesmeet ?? 0) + (objs.First().B_Doesmeet ?? 0) + (objs.First().C_Doesmeet ?? 0) + (objs.First().D_Doesmeet ?? 0) + (objs.First().E_Doesmeet ?? 0) + (objs.First().F_Doesmeet ?? 0) + (objs.First().G_Doesmeet ?? 0) + (objs.First().H_Doesmeet ?? 0);
            objs.First().AllUnable  = (objs.First().A_Unable  ?? 0) + (objs.First().B_Unable  ?? 0) + (objs.First().C_Unable  ?? 0) + (objs.First().D_Unable  ?? 0) + (objs.First().E_Unable  ?? 0) + (objs.First().F_Unable  ?? 0) + (objs.First().G_Unable  ?? 0) + (objs.First().H_Unable  ?? 0);

          
            return objs;

        }
    }
}