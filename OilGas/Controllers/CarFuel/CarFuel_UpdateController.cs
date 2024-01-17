using Dou.Controllers;
using Dou.Misc;
using Dou.Misc.Attr;
using Dou.Models;
using Dou.Models.DB;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace OilGas.Controllers.CarFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "CarFuel_Update", Name = "負責人批次變更", MenuPath = "加油站/A管理專區", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class CarFuel_UpdateController : APaginationModelController<CarFuel_BasicData>
    {
        // GET: CarFuel_Update
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<CarFuel_BasicData> GetModelEntity()
        {
            return new ModelEntity<CarFuel_BasicData>(new OilGasModelContextExt());
        }

        protected override IQueryable<CarFuel_BasicData> BeforeIQueryToPagedList(IQueryable<CarFuel_BasicData> iquery, params KeyValueParams[] paras)
        {
            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return new List<CarFuel_BasicData>().AsQueryable();
            }

            //權限查詢 (縣市權限，變動清除catch)
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysGSLs();
            iquery = iquery.Where(a => a.CaseNo != null && pCitys.Any(b => b == a.CaseNo.Substring(4, 2)));

            return base.BeforeIQueryToPagedList(iquery, paras);
        }

        //批次變更表單
        public ActionResult BatchSave(List<string> CaseNos, vwe_CarFuel_UpdateForm obj)
        {
            try
            {
                string ab = "!23";

                //1.CarFuel_BasicData_Log
                //Boss_Tel = '聯絡電話1',
                //LicenseNo1 = '經〈110〉能高中油',
                //MemberID = '6E9D42DE5E6E2738',
                //LicenseNo2 = '186',
                //Address2 = '台中市大甲區八德街一巷二弄三之四號五樓之六',
                //LicenseNo3 = '5',
                //ChangeReport_date = '2024/01/12',
                //ID_No = '2',
                //Boss_Email = 'brianlin12345@gmail.com',
                //ZipCode2 = '437',
                //Boss = '負責人111'

                //2.CarFuel_Dispatch
                //Dispatch_date,
                //otherCopyUnit,
                //DispatchClass,
                //License_No,
                //Shouwen_Units,
                //Dispatch_No,
                //DispatchUnit,
                //Note,
                //CaseNo,
                //MemberID,
                //CopyUnit

                using (var dbContext = new OilGasModelContextExt())
                {                    
                    foreach (string CaseNo in CaseNos)
                    {
                        CarFuel_BasicData u_carFuel = dbContext.CarFuel_BasicData.Where(a => a.CaseNo == CaseNo).FirstOrDefault();
                        if (u_carFuel == null)
                            continue;

                        //****1.新增 CarFuel_BasicData_Log****
                        CarFuel_BasicData_Log a_log = new CarFuel_BasicData_Log();
                        a_log.Boss_Tel = u_carFuel.Boss_Tel;
                        a_log.LicenseNo1 = u_carFuel.LicenseNo1;
                        a_log.MemberID = u_carFuel.MemberID;
                        a_log.LicenseNo2 = u_carFuel.LicenseNo2;
                        a_log.Address2 = u_carFuel.Address2;
                        a_log.LicenseNo3 = u_carFuel.LicenseNo3;
                        a_log.ChangeReport_date = u_carFuel.ChangeReport_date;
                        a_log.ID_No = u_carFuel.ID_No;
                        a_log.Boss_Email = u_carFuel.Boss_Email;
                        a_log.ZipCode2 = u_carFuel.ZipCode2;
                        a_log.Boss = u_carFuel.Boss;

                        dbContext.CarFuel_BasicData_Log.Add(a_log);

                        //****2.修改 CarFuel_BasicData****
                        u_carFuel.Boss_Tel = obj.txt_Boss_Tel;               //Boss_Tel = '聯絡電話1',
                        //不變動的資料 LicenseNo1 = '經〈110〉能高中油', LicenseNo2 = '186', LicenseNo3 = '5',
                        u_carFuel.MemberID = Dou.Context.CurrentUserBase.Id; //MemberID = '6E9D42DE5E6E2738',
                        //ZipCode2 = '437',
                        //Address2 = '台中市大甲區八德街一巷二弄三之四號五樓之六',                        
                        u_carFuel.ChangeReport_date = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd")); //ChangeReport_date = '2024/01/12',
                        u_carFuel.ID_No = obj.txt_ID_No;                     //ID_No = '2',
                        u_carFuel.Boss_Email = obj.txt_Boss_Email;           //Boss_Email = 'brianlin12345@gmail.com',                        
                        u_carFuel.Boss = obj.txt_BossName;                   //Boss = '負責人111'
                        u_carFuel.CaseNo = CaseNo;

                        //****3.新增 寫入變更歷程檔****                        
                        string recordData = "";
                        recordData += "批次變更負責人為" + u_carFuel.Boss + "<br/>";
                        recordData += "[證號]" + "變更為" + u_carFuel.LicenseNo1 + "字第" + u_carFuel.LicenseNo2 + "之" + u_carFuel.LicenseNo3 + "號<br/>";
                        RecordLog record = new RecordLog();
                        record.CaseNo = u_carFuel.CaseNo;
                        record.recordData = recordData;
                        record.File_name = "";
                        record.Mod_name = Dou.Context.CurrentUserBase.Name;
                        record.Mod_date = DateTime.Now;
                        record.MemberID = Dou.Context.CurrentUserBase.Id;
                        dbContext.RecordLog.Add(record);

                        //****4.新增 發文紀錄 CarFuel_Dispatch****
                        CarFuel_Dispatch v2 = new CarFuel_Dispatch();
                        v2.Dispatch_date = obj.txt_Dispatch_date;
                        v2.otherCopyUnit = obj.txt_OtherCopyUnit;
                        v2.DispatchClass = "37";
                        v2.License_No = obj.ddl_selectLicenseNo;
                        v2.Shouwen_Units = obj.txt_Shouwen_Units;
                        v2.Dispatch_No = obj.txt_Dispatch_No;
                        v2.DispatchUnit = Dou.Context.CurrentUser<User>().OrganizationFullName;
                        v2.Note = "批次變更";
                        v2.CaseNo = CaseNo;
                        v2.MemberID = Dou.Context.CurrentUserBase.Id;
                        v2.CopyUnit = obj.cbl_CopyUnit;

                        dbContext.CarFuel_Dispatch.Add(v2);
                    }

                    dbContext.SaveChanges();
                }


            }
            catch (Exception ex)
            {
                return Json(new { result = false, errorMessage = ex.Message });
            }

            return Json(new { result = true });
        }

        public override DataManagerOptions GetDataManagerOptions()
        {
            var opts = base.GetDataManagerOptions();

            //全部欄位排序
            foreach (var field in opts.fields)
                field.filter = false;

            opts.GetFiled("Gas_Name").filter = true;            
            opts.GetFiled("Business_theme").filter = true;            
            opts.GetFiled("Boss").visible = true;
            opts.GetFiled("Recipient_date").visible = false;            

            return opts;
        }

        public virtual ActionResult getUpdateForm()
        {
            var opts = Dou.Misc.DataManagerScriptHelper.GetDataManagerOptions<vwe_CarFuel_UpdateForm>();

            //全部欄位排序
            foreach (var field in opts.fields)
                field.sortable = true;

            opts.addable = false;
            //opts.editable = false;
            opts.deleteable = false;

            opts.singleDataEdit = true;
            opts.editformWindowStyle = "showEditformOnly";

            vwe_CarFuel_UpdateForm data = new vwe_CarFuel_UpdateForm();
            opts.datas = new List<vwe_CarFuel_UpdateForm>() { data };

            var jstr = JsonConvert.SerializeObject(opts, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            jstr = jstr.Replace(DataManagerScriptHelper.JavaScriptFunctionStringStart, "(").Replace(DataManagerScriptHelper.JavaScriptFunctionStringEnd, ")");
            return Content(jstr, "application/json");
        }

    }

    public class vwe_CarFuel_UpdateForm
    {
        [Display(Name = "負責人名稱")]
        [ColumnDef(ColSize = 3)]
        public string txt_BossName { get; set; }

        [Display(Name = "負責人身份證字號")]
        [ColumnDef(ColSize = 3)]
        public string txt_ID_No { get; set; }

        ////[Display(Name = "負責人聯絡地址")]
        ///[ColumnDef(ColSize = 3)]
        ////public string xxxxx { get; set; }

        [Display(Name = "負責人聯絡電話")]
        [ColumnDef(ColSize = 3)]
        public string txt_Boss_Tel { get; set; }

        [Display(Name = "電子郵件信箱")]
        [ColumnDef(ColSize = 3)]
        public string txt_Boss_Email { get; set; }

        [Display(Name = "發文日期")]
        [ColumnDef(EditType = EditType.Date, ColSize = 3)]
        public DateTime? txt_Dispatch_date { get; set; }

        [Display(Name = "發文字號")]
        [ColumnDef(EditType = EditType.Select, SelectItemsClassNamespace = CarVehicleGas_LicenseNoSelectItems.AssemblyQualifiedName,
            Filter = true, FilterAssign = FilterAssignType.Contains,
            ColSize = 3)]
        public string ddl_selectLicenseNo { get; set; }

        [Display(Name = "發文字號No")]
        [ColumnDef(ColSize = 3)]
        public string txt_Dispatch_No { get; set; }

        ////[Display(Name = "發文資料")]
        ///[ColumnDef(ColSize = 3)]
        ////public string xxxxx { get; set; }

        [Display(Name = "受文者單位")]
        [ColumnDef(ColSize = 3)]
        public string txt_Shouwen_Units { get; set; }

        [Display(Name = "副本單位")]        
        [ColumnDef(EditType = EditType.Select, SelectItemsClassNamespace = CarVehicleGas_CopyUnitSelectItems.AssemblyQualifiedName,
            Filter = true, FilterAssign = FilterAssignType.Contains,
            ColSize = 3)]
        public string cbl_CopyUnit { get; set; }

        [Display(Name = "副本其他說明")]
        [ColumnDef(ColSize = 3)]
        public string txt_OtherCopyUnit { get; set; }

    }
}