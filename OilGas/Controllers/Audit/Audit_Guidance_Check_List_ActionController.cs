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
    [Dou.Misc.Attr.MenuDef(Id = "Audit_Guidance_Check_List_Action", Name = "新增複查結果", MenuPath = "查核輔導專區/G查核輔導資料", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.Update, AllowAnonymous = false)]
    public class Audit_Guidance_Check_List_ActionController : APaginationModelController<Check_Basic_Action>
    {
        public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: Audit_Guidance_Check_List
        public ActionResult Index()
        {
            return View();
        }
        protected override IModelEntity<Check_Basic_Action> GetModelEntity()
        {
            return new ModelEntity<Check_Basic_Action>(new OilGasModelContextExt());
        }
        protected override IQueryable<Check_Basic_Action> BeforeIQueryToPagedList(IQueryable<Check_Basic_Action> iquery, params KeyValueParams[] paras)
        {
            //非ADMIN帳號只能看自己縣市
            if (!Dou.Context.CurrentIsAdminUser && !basic.Permissions("admin"))
            {
                var CITYdata = Dou.Context.CurrentUser<User>().city.Split(',');
                iquery = iquery.ToList().Where(x => CITYdata.Contains(x.CITY)).AsQueryable();
            }

            //搜尋縣市
            var CITY = basic.getfilter(paras, "CITY");
            if (CITY != "")
            {
                //因為CITY可能用,分成兩個ID
                var CITYdata = CITY.Split(',');
                iquery = iquery.ToList().Where(x => CITYdata.Contains(x.CITY)).AsQueryable();
            }

            //從查詢頁進來的，只能看被選到的CheckNo
            if (Request.QueryString["edit"]=="0")
            {
                var CheckNo = Request.QueryString["CheckNo"];
                iquery = iquery.Where(X => X.CheckNo == CheckNo);
                return base.BeforeIQueryToPagedList(iquery, paras);
            }



            //正是需要這行，但測試因為麻煩所以先註解掉
            //Check_Style是null才顯示
            iquery = iquery.Where(X => X.Check_Style == null);
            return base.BeforeIQueryToPagedList(iquery, paras);
        }


        //protected override void AddDBObject(IModelEntity<Check_Basic_Action> dbEntity, IEnumerable<Check_Basic_Action> objs)
        //{
        //    //CaseNo在前端的下拉選單會給CaseNo,Gas_Name  ,所以用","取CaseNo跟Gas_Name
        //    var CaseNoAndGas_Name = objs.First().CaseNo.Split(',');

        //    //以防Gas_Name有","  ，所以用迴圈把後面的字直接組起來
        //    var Gas_Name = "";
        //    for(int i = 1; i< CaseNoAndGas_Name.Length; i++)
        //    {
        //        Gas_Name = Gas_Name+","+ CaseNoAndGas_Name[i];
        //    }

        //    objs.First().CaseNo = CaseNoAndGas_Name[0];
        //    objs.First().Gas_Name = Gas_Name.Substring(1);//拿掉第一個","




        //    base.AddDBObject(dbEntity, objs);

        //}

        protected override void UpdateDBObject(IModelEntity<Check_Basic_Action> dbEntity, IEnumerable<Check_Basic_Action> objs)
        {



            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同





            //確保不是改前端畫面的資料
            var ID = objs.First().id;
            var selectobjs = db.Check_Basic_Action.Where(X => X.id == ID).FirstOrDefault();
            if (selectobjs.CaseNo != objs.First().CaseNo || selectobjs.Gas_Name != objs.First().Gas_Name || selectobjs.CheckNo != objs.First().CheckNo || selectobjs.CaseType != objs.First().CaseType || selectobjs.Tank_Well != objs.First().Tank_Well)
            {
                throw new Exception("資料有誤");
            }




            base.UpdateDBObject(dbEntity, objs);
   
        }







































        ////搜尋CaseNo讓下拉選單有選向
        //public string GetCaseNoSelectList(string CaseNoOrName, string type = "CarFuel_BasicData")
        //{



        //    IQueryable<BasicDataForSelect> result;
        //    var resultJson ="";
        //    switch (type)
        //    {
        //        case "CarFuel_BasicData":
        //            result = from a in db.CarFuel_BasicData
        //                                    where a.CaseNo.Contains(CaseNoOrName) || a.Gas_Name.Contains(CaseNoOrName)
        //                                    select new BasicDataForSelect()
        //                                    {
        //                                        CaseNo = a.CaseNo,
        //                                        Gas_Name = a.Gas_Name
        //                                    };
        //            resultJson = JsonConvert.SerializeObject(result);
        //            break;

        //        case "FishGas_BasicData":
        //            result = from a in db.FishGas_BasicData
        //                     where a.CaseNo.Contains(CaseNoOrName) || a.Gas_Name.Contains(CaseNoOrName)
        //                     select new BasicDataForSelect()
        //                     {
        //                         CaseNo = a.CaseNo,
        //                         Gas_Name = a.Gas_Name
        //                     };
        //            resultJson = JsonConvert.SerializeObject(result);
        //            break;

        //        case "SelfFuel_Basic":
        //            result = from a in db.SelfFuel_Basic
        //                     where a.CaseNo.Contains(CaseNoOrName) || a.FuelName.Contains(CaseNoOrName)
        //                     select new BasicDataForSelect()
        //                     {
        //                         CaseNo = a.CaseNo,
        //                         Gas_Name = a.FuelName
        //                     };
        //            resultJson = JsonConvert.SerializeObject(result);
        //            break;
        //    }

        //    return resultJson;
        //}


        //public  class BasicDataForSelect
        //{
        //    public string CaseNo { get; set; }
        //    public string Gas_Name { get; set; }
        //}

    }
}