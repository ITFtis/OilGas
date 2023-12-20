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


namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_CounselingDataM", Name = "輔導講習會參加者資料維護", MenuPath = "查核輔導專區/G輔導講習專區", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Audit_CounselingDataMController : APaginationModelController<CounselingData>
    {
        // GET: Audit_CounselingDataM
        public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();

        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<CounselingData> GetModelEntity()
        {
            return new ModelEntity<CounselingData>(new OilGasModelContextExt());
        }





        protected override void AddDBObject(IModelEntity<CounselingData> dbEntity, IEnumerable<CounselingData> objs)
        {


         

            //CaseNo在前端的下拉選單會給CaseNo,Gas_Name  ,所以用","取CaseNo跟Gas_Name
            var CaseNoAndGas_Name = objs.First().s_CaseNo.Split(',');

            //以防Gas_Name有","  ，所以用迴圈把後面的字直接組起來
            var Gas_Name = "";
            for (int i = 1; i < CaseNoAndGas_Name.Length; i++)
            {
                Gas_Name = Gas_Name + "," + CaseNoAndGas_Name[i];
            }

            objs.First().s_CaseNo = CaseNoAndGas_Name[0];
            objs.First().s_GasName = Gas_Name.Substring(1);//拿掉第一個","




            base.AddDBObject(dbEntity, objs);

        }



        //protected override void UpdateDBObject(IModelEntity<CounselingData> dbEntity, IEnumerable<CounselingData> objs)
        //{



        //    basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同





        //    //確保不是改前端畫面的資料
        //    var ID = objs.First().id;
        //    var selectobjs = db.Check_Basic.Where(X => X.id == ID).FirstOrDefault();
        //    if (selectobjs.CaseNo != objs.First().CaseNo || selectobjs.Gas_Name != objs.First().Gas_Name || selectobjs.CheckNo != objs.First().CheckNo)
        //    {
        //        throw new Exception("資料有誤");
        //    }




        //    base.UpdateDBObject(dbEntity, objs);

        //}




    }
}