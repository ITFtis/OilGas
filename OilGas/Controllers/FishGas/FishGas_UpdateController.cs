using Dou.Controllers;
using Dou.Misc;
using Dou.Misc.Attr;
using Dou.Models.DB;
using Newtonsoft.Json;
using OilGas._report;
using OilGas.Controllers.CarFuel;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.FishGas
{
    [Dou.Misc.Attr.MenuDef(Id = "FishGas_Update", Name = "負責人批次變更", MenuPath = "漁船加油站/C管理專區", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class FishGas_UpdateController : APaginationModelController<FishGas_BasicData>
    {
        // GET: FishGas_Update
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<FishGas_BasicData> GetModelEntity()
        {
            return new ModelEntity<FishGas_BasicData>(new OilGasModelContextExt());
        }

        protected override IQueryable<FishGas_BasicData> BeforeIQueryToPagedList(IQueryable<FishGas_BasicData> iquery, params KeyValueParams[] paras)
        {
            //已開業的usestage
            var _db = new OilGasModelContextExt();
            var useStage = _db.UsageStateCode.Where(x => x.Type == "已開業").Select(x => x.Value).ToList();

            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return new List<FishGas_BasicData>().AsQueryable();
            }

            //權限查詢 (縣市權限，變動清除catch)
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysGSLs();


            iquery = iquery.Where(a => a.CaseNo != null && pCitys.Any(b => b == a.CaseNo.Substring(4, 2)) && useStage.Any(b => b == a.UsageState));

            return base.BeforeIQueryToPagedList(iquery, paras);
        }

        public override DataManagerOptions GetDataManagerOptions()
        {
            var opts = base.GetDataManagerOptions();

            //全部欄位排序
            foreach (var field in opts.fields)
                field.filter = false;

            opts.GetFiled("Gas_Name").filter = true;
            opts.GetFiled("Business_theme").filter = true;
            opts.GetFiled("CITY").filter = true;
            opts.GetFiled("Boss").visible = true;
            opts.GetFiled("Recipient_date").visible = false;

            return opts;
        }

        [HttpPost]
        public string Sendupload(string ID, string[] CaseNo, HttpPostedFileBase file)
        {
            using (var db = new OilGasModelContextExt())
            {
                foreach (var caseNo in CaseNo)
                {
                    var dataInDB = db.FishGas_BasicData.Where(x => x.CaseNo == caseNo).FirstOrDefault();

                    if (dataInDB != null)
                    {
                        var oldFileName = dataInDB.File_name ?? "NULL";
                        new basicController().upload(file, dataInDB.File_name, "FishGas\\basic");
                    }

                }

            }

            return "ok";

        }

        public ActionResult BatchSave(List<string> CaseNos, vwe_FishGas_UpdateForm obj)
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
                        FishGas_BasicData u_fishGas = dbContext.FishGas_BasicData.Where(a => a.CaseNo == CaseNo).FirstOrDefault();
                        if (u_fishGas == null)
                            continue;

                        //****1.新增 FishGas_BasicData_Log****
                        FishGas_BasicData_Log a_log = new FishGas_BasicData_Log();
                        a_log.Boss_Tel = u_fishGas.Boss_Tel;
                        a_log.LicenseNo1 = u_fishGas.LicenseNo1;
                        a_log.MemberID = u_fishGas.MemberID;
                        a_log.LicenseNo2 = u_fishGas.LicenseNo2;
                        a_log.Address2 = u_fishGas.Address2;
                        a_log.LicenseNo3 = u_fishGas.LicenseNo3;
                        a_log.ChangeReport_date = u_fishGas.ChangeReport_date;
                        a_log.ID_No = u_fishGas.ID_No;
                        a_log.Boss_Email = u_fishGas.Boss_Email;
                        a_log.ZipCode2 = u_fishGas.ZipCode2;
                        a_log.Boss = u_fishGas.Boss;
                        a_log.File_name = u_fishGas.File_name;

                        dbContext.FishGas_BasicData_Log.Add(a_log);

                        //****2.修改 FishGas_BasicData****
                        //不變動的資料 LicenseNo1 = '經〈110〉能高中油', LicenseNo2 = '186', LicenseNo3 = '5',
                        u_fishGas.Boss_Tel = obj.txt_Boss_Tel;               //Boss_Tel = '聯絡電話1',                        
                        u_fishGas.MemberID = Dou.Context.CurrentUserBase.Id; //MemberID = '6E9D42DE5E6E2738',
                        u_fishGas.ZipCode2 = obj.ZipCode2;
                        u_fishGas.Address2 = obj.Address2;
                        u_fishGas.ChangeReport_date = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd")); //ChangeReport_date = '2024/01/12',
                        u_fishGas.ID_No = obj.txt_ID_No;                     //ID_No = '2',
                        u_fishGas.Boss_Email = obj.txt_Boss_Email;           //Boss_Email = 'brianlin12345@gmail.com',                        
                        u_fishGas.Boss = obj.txt_BossName;                   //Boss = '負責人111'
                        u_fishGas.CaseNo = CaseNo;
                        u_fishGas.File_name = obj.FileName;

                        //****3.新增 寫入變更歷程檔****                        
                        string recordData = "";
                        recordData += "批次變更負責人為" + u_fishGas.Boss + "<br/>";
                        recordData += "[證號]" + "變更為" + u_fishGas.LicenseNo1 + "字第" + u_fishGas.LicenseNo2 + "之" + u_fishGas.LicenseNo3 + "號<br/>";
                        RecordLog record = new RecordLog();
                        record.CaseNo = u_fishGas.CaseNo;
                        record.recordData = recordData;
                        record.File_name = "";
                        record.Mod_name = Dou.Context.CurrentUserBase.Name;
                        record.Mod_date = DateTime.Now;
                        record.MemberID = Dou.Context.CurrentUserBase.Id;
                        record.File_name = obj.FileName;
                        dbContext.RecordLog.Add(record);

                        //****4.新增 發文紀錄 FishGas_Dispatch****
                        FishGas_Dispatch v2 = new FishGas_Dispatch();
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
                        v2.File_name = obj.FileName;

                        dbContext.FishGas_Dispatch.Add(v2);
                    }

                    dbContext.SaveChanges();
                }


            }
            catch (Exception ex)
            {

                //return Json(new { result = false, errorMessage = ex.Message });
            }

            return Json(new { result = true });
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

            vwe_FishGas_UpdateForm data = new vwe_FishGas_UpdateForm();
            opts.datas = new List<vwe_FishGas_UpdateForm>() { data };

            var jstr = JsonConvert.SerializeObject(opts, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            jstr = jstr.Replace(DataManagerScriptHelper.JavaScriptFunctionStringStart, "(").Replace(DataManagerScriptHelper.JavaScriptFunctionStringEnd, ")");
            return Content(jstr, "application/json");
        }

        public ActionResult ExportExcel(string CaseNo)
        {
            var query = getDataQuery(CaseNo);
            Ppt_FishGas_Update_Lience rep = new Ppt_FishGas_Update_Lience();
            string url = rep.Export(query);

            if (url == "")
            {
                return Json(new { result = false, errorMessage = rep.ErrorMessage }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = true, url = url }, JsonRequestBehavior.AllowGet);


            }
        }

        /// <summary>
        /// 取得證照的內容
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private FishGas_Update_Lience getDataQuery(string caseNo)
        {
            var _db = new OilGasModelContextExt();

            var enableBT = _db.CarVehicleGas_BusinessOrganization.Where(x => x.IsEnable == true);
            var basicData = _db.FishGas_BasicData.Where(x => x.CaseNo == caseNo);


            var dataInDB = basicData
                .Join(enableBT, a => a.Business_theme, b => b.Value, (a, b) =>
                new FishGas_Update_Lience
                {

                    Gas_Name = a.Gas_Name,
                    Business_Theme = a.Business_theme == "16" ? a.otherBusiness_theme : b.Name,
                    ReportDate = a.ChangeReport_date ?? a.Report_date,
                    Boss = a.Boss ?? "",
                    Address = a.ZipCode + a.Address
                })
                .FirstOrDefault();

            var oilType = _db.FishGas_OilData
                        .Where(x => x.CaseNo == caseNo)
                        .Select(x => x.Oil_type)
                        .Distinct()
                        .ToList();

            var strOilType = string.Join("、", oilType);

            dataInDB.SellType = strOilType;
            dataInDB.LienceNo = getLienceNumber(basicData.FirstOrDefault());

            return dataInDB;

        }

        private string getLienceNumber(FishGas_BasicData a)
        {
            string lienceNo;
            var l1 = string.IsNullOrWhiteSpace(a.LicenseNo1) ? string.Empty : a.LicenseNo1.Trim();
            var l2 = string.IsNullOrWhiteSpace(a.LicenseNo2) ? string.Empty : " 字 第 " + a.LicenseNo2.Trim();
            var l3 = string.IsNullOrWhiteSpace(a.LicenseNo3) ? string.Empty : " 之 " + a.LicenseNo3.Trim();

            lienceNo = l1 + l2 + l3;
            return string.IsNullOrEmpty(lienceNo) ? string.Empty : lienceNo + " 號";

        }
    }
    public class vwe_FishGas_UpdateForm
    {
        [Required]
        [Display(Name = "負責人名稱")]
        [ColumnDef(ColSize = 3)]
        public string txt_BossName { get; set; }

        [Display(Name = "負責人身份證字號")]
        [ColumnDef(ColSize = 3)]
        public string txt_ID_No { get; set; }

        [Display(Name = "負責人郵遞區號")]
        [ColumnDef(ColSize = 3)]
        public string ZipCode2 { get; set; }

        [Display(Name = "負責人聯絡地址")]
        [ColumnDef(ColSize = 3)]
        public string Address2 { get; set; }

        [Display(Name = "負責人聯絡電話")]
        [ColumnDef(ColSize = 3)]
        public string txt_Boss_Tel { get; set; }

        [Display(Name = "電子郵件信箱")]
        [ColumnDef(EditType = EditType.Email, ColSize = 3)]
        public string txt_Boss_Email { get; set; }



        [Display(Name = "發文資料")]
        [ColumnDef(ColSize = 3)]
        public string FileName { get; set; }

        [Required]
        [Display(Name = "發文日期")]
        [ColumnDef(EditType = EditType.Date, ColSize = 3)]
        public DateTime? txt_Dispatch_date { get; set; }

        [Required]
        [Display(Name = "發文字號")]
        [ColumnDef(EditType = EditType.Select, SelectItemsClassNamespace = CarVehicleGas_LicenseNoSelectItems.AssemblyQualifiedName,
            Filter = true, FilterAssign = FilterAssignType.Contains,
            ColSize = 3)]
        public string ddl_selectLicenseNo { get; set; }

        [Required]
        [Display(Name = "發文字號No")]
        [ColumnDef(ColSize = 3)]
        public string txt_Dispatch_No { get; set; }

        [Required]
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

    public class FishGas_Update_Lience
    {
        public string LienceNo { get; set; }
        public string Gas_Name { get; set; }
        public string Business_Theme { get; set; }
        public DateTime? ReportDate { get; set; }
        public string Boss { get; set; }
        public string Address { get; set; }
        public string SellType { get; set; }

    }
}