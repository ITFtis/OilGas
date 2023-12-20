using DouHelper;
using Microsoft.Ajax.Utilities;
using NPOI.OpenXml4Net.OPC.Internal;
using NPOI.OpenXmlFormats.Dml.Diagram;
using NPOI.OpenXmlFormats.Vml;
using NPOI.POIFS.FileSystem;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.Util;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using static OilGas.Rpt_SelfFuel_BasicData;

namespace OilGas.Controllers.SelfFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "SelfFuel_BasicData", Name = "基本資料清單查詢", MenuPath = "自用加儲油/D統計報表專區", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class SelfFuel_BasicDataController : Controller
    {
        // GET: SelfFuel_BasicData
        public ActionResult Index()
        {            
            if (Dou.Context.CurrentUserBase == null)
            {
                return Redirect("~/Home/Index");
            }

            //////////////效能測試問題
            ////////////System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
            ////////////Dou.Models.DB.IModelEntity<SelfFuel_Basic> selfFuel_Basic = new Dou.Models.DB.ModelEntity<SelfFuel_Basic>(dbContext);
            ////////////Dou.Models.DB.IModelEntity<SelfFuel_Land> selfFuel_Land = new Dou.Models.DB.ModelEntity<SelfFuel_Land>(dbContext);

            ////////////var allData = selfFuel_Basic.GetAll().GroupJoin(selfFuel_Land.GetAll(), a => a.CaseNo, b => b.CaseNo, (o, c) => new { o.CaseNo, o.FuelName, o.Change, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, c })
            ////////////                .Select(o => new SelfFuel_1()
            ////////////                {
            ////////////                    案件編號 = o.CaseNo,
            ////////////                    名稱 = o.FuelName,
            ////////////                    變更次數 = o.Change,
            ////////////                    UsageState = o.UsageState,
            ////////////                    UsageState_Second = o.UsageState_Second,
            ////////////                    UsageState_Third = o.UsageState_Third,
            ////////////                    UsageState_Fourth = o.UsageState_Fourth,
            ////////////                }).Where(a => a.案件編號 != "");
            ////////////                ////.Select(o => new 
            ////////////                ////{
            ////////////                ////    o.CaseNo,
            ////////////                ////    o.FuelName,
            ////////////                ////    o.Change,
            ////////////                ////    o.UsageState,
            ////////////                ////    o.UsageState_Second,
            ////////////                ////    o.UsageState_Third,
            ////////////                ////     o.UsageState_Fourth,
            ////////////                ////}).Where(a => a.CaseNo != "");

            ////////////var test = allData.ToList();
            //////////////---

            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();

            //登入者縣市
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysGSLs();
            var userCitys = pCitys;
            ViewBag.userCitys = userCitys;

            //代碼-縣市別
            Dou.Models.DB.IModelEntity<CityCode> cityCode = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
            ViewBag.cityCode = cityCode.GetAll().Where(a => userCitys.Any(b => b == a.GSLCode))
                                            .OrderBy(a => a.Rank).ToList();

            //代碼-鄉鎮
            Dou.Models.DB.IModelEntity<AreaCode> m_areaCode = new Dou.Models.DB.ModelEntity<AreaCode>(dbContext);
            var areaCode = m_areaCode.GetAll();
            if (userCitys.Count > 0)
            {
                areaCode = areaCode.Where(a => userCitys.Any(b => a.GSLCode == b));
            }

            ViewBag.areaCode = areaCode.OrderBy(a => a.Rank).ToList();

            return View();
        }

        public ActionResult ExportSelfFuel_BasicData(ReportData.ViewParams objs)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.自用加儲油_D統計報表專區_基本資料清單查詢);
            string fileTitle = "自用加儲油_基本資料欄位清單";


            try
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                List<string> titles = new List<string>();

                //1.取得資料
                System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
                IEnumerable<SelfFuel_1> datas = null;

                //1.進行中 2.Log
                string SType = "1";
                if (objs.conditions != null)
                {
                    //資料進行中或Log
                    var inIng = objs.conditions.Where(a => a.Id == "inIng").FirstOrDefault();
                    var inLog = objs.conditions.Where(a => a.Id == "inLog").FirstOrDefault();
                    if (inIng != null)
                    {
                        SType = inIng.Value;
                    }
                    else if (inLog != null)
                    {
                        SType = inLog.Value;
                    }
                }

                if (SType == "1")
                {
                    titles.Add("自用加儲油_現況資料，查詢條件:");
                    datas = OilGas.Rpt_SelfFuel_BasicData.GetAllDatas();
                }
                else if (SType == "2")
                {
                    titles.Add("自用加儲油_變更歷程，查詢條件:");
                    datas = OilGas.Rpt_SelfFuel_BasicData.GetLogAllDatas();
                }
                else
                {                    
                    return Json(new { result = false, errorMessage = "無此資料取得方式(SType):" + SType }, JsonRequestBehavior.AllowGet);
                }

                datas = datas.Where(a => a.IsConfirm == true);
                var n = datas.Count();

                //權限查詢
                var pCitys = Dou.Context.CurrentUser<User>().PowerCitysGSLs();
                datas = datas.Where(a => pCitys.Any(b => a.案件編號.Substring(4, 2) == b));

                //2.條件篩選 (conditions)                
                if (objs.conditions != null)
                {                    
                    var txt_ModifyStartTime = objs.conditions.Where(a => a.Id == "txt_ModifyStartTime").FirstOrDefault();
                    if (txt_ModifyStartTime != null)
                    {
                        DateTime date = DateTime.Parse(txt_ModifyStartTime.Value);
                        datas = datas.Where(a => a.ModifyTime >= date);
                        titles.Add("查詢期間(現況資料修改時間)起:" + txt_ModifyStartTime.Value);
                    }
                    var txt_ModifyEndTime = objs.conditions.Where(a => a.Id == "txt_ModifyEndTime").FirstOrDefault();
                    if (txt_ModifyEndTime != null)
                    {
                        DateTime date = DateTime.Parse(txt_ModifyEndTime.Value);
                        datas = datas.Where(a => a.ModifyTime <= date);
                        titles.Add("查詢期間(現況資料修改時間)迄:" + txt_ModifyEndTime.Value);
                    }

                    var ddl_City = objs.conditions.Where(a => a.Id == "ddl_City").FirstOrDefault();
                    if (ddl_City != null)
                    {
                        string str = ddl_City.Value;
                        List<string> sels = str.Split(',').ToList();
                        datas = datas.Where(a => sels.Any(b => a.案件編號.Substring(4, 2) == b));

                        //代碼-縣市別
                        Dou.Models.DB.IModelEntity<CityCode> cityCode = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
                        //titles.Add("縣市:" + cityCode.GetAll().Where(a => sels.Any(b => b == a.GSLCode)).FirstOrDefault().CityName);
                        titles.Add("縣市:" + cityCode.GetAll().Where(a => sels.Any(b => a.GSLCode.IndexOf(b) > -1)).FirstOrDefault().CityName);
                    }

                    var ddl_Area = objs.conditions.Where(a => a.Id == "ddl_Area").FirstOrDefault();
                    if (ddl_Area != null)
                    {
                        string str = ddl_Area.Value;
                        datas = datas.Where(a => a.AreaNo == str);

                        //代碼-鄉鎮
                        Dou.Models.DB.IModelEntity<AreaCode> m_areaCode = new Dou.Models.DB.ModelEntity<AreaCode>(dbContext);
                        titles.Add("鄉鎮:" + m_areaCode.GetAll().Where(a => a.AreaCode1 == str).FirstOrDefault().AreaName);
                    }                    

                    //使用狀況Lv1
                    var ddl_BigUsage = objs.conditions.Where(a => a.Id == "ddl_BigUsage").FirstOrDefault();
                    if (ddl_BigUsage != null)
                    {
                        string str = ddl_BigUsage.Value;
                        var code = RptCode.GetAnyUsageStateM(str);                        
                        datas = datas.Where(a => code.Any(b => b.UsageState == a.UsageState
                                                                && b.UsageState_Second == a.UsageState_Second
                                                                && b.UsageState_Third == a.UsageState_Third
                                                                && b.UsageState_Fourth == a.UsageState_Fourth));
                        //title add                        
                        titles.Add("使用狀況Lv1:" + WebUI.GetDDLUsageStateMaster().Where(a => a.Value == str).FirstOrDefault().Key);
                    }

                    //使用狀況Lv2
                    var ddl_UsageState = objs.conditions.Where(a => a.Id == "ddl_UsageState").FirstOrDefault();
                    if (ddl_UsageState != null)
                    {
                        string str = ddl_UsageState.Value;

                        string[] ary = str.Split(',').ToArray();
                        var m = new UsageStateM("", ary[0], ary[1], ary[2], ary[3]);

                        var code = RptCode.GetAnyUsageStateM(m);
                        datas = datas.Where(a => code.Any(b => b.UsageState == a.UsageState
                                                                && b.UsageState_Second == a.UsageState_Second
                                                                && b.UsageState_Third == a.UsageState_Third
                                                                && b.UsageState_Fourth == a.UsageState_Fourth));                        
                        //title add                        
                        titles.Add("使用狀況Lv2:" + WebUI.GetDDLUsageStateDetail().Where(a => a.Value == str).FirstOrDefault().Key);
                    }

                    var txt_CaseNo = objs.conditions.Where(a => a.Id == "txt_CaseNo").FirstOrDefault();
                    if (txt_CaseNo != null)
                    {
                        string str = txt_CaseNo.Value;
                        datas = datas.Where(a => a.案件編號 == str);
                        titles.Add("案件編號:" + str);
                    }

                    var txt_FuelName = objs.conditions.Where(a => a.Id == "txt_FuelName").FirstOrDefault();
                    if (txt_FuelName != null)
                    {
                        string str = txt_FuelName.Value;
                        datas = datas.Where(a => a.設施名稱.Contains(str));
                        titles.Add("設施名稱:" + str);
                    }
                }                

                //3.輸出欄位(distinct) (no distinct error:顯示全部欄位)
                bool BusiOrg = false;
                bool UsageNames = false;
                bool ExpiredDate = false;
                bool StartDate = false;
                bool EndDate = false;
                bool LicenseNo = false;
                bool FacilityPlace = false;
                bool Responsor = false;
                bool FacilityPhone = false;
                bool Address = false;
                bool AddressNo = false;
                bool insurance = false;
                bool LandPriority = false;
                bool LandTotalSquare = false;
                bool LandClass = false;
                bool LandUsageZone = false;
                bool iptFacilityType = false;
                bool Tank = false;
                bool Pump = false;
                bool SoilClass = false;

                if (objs.columns != null)
                {
                    BusiOrg = objs.columns.Where(b => b.Id == "BusiOrg").Count() > 0;
                    UsageNames = objs.columns.Where(b => b.Id == "UsageNames").Count() > 0;
                    ExpiredDate = objs.columns.Where(b => b.Id == "ExpiredDate").Count() > 0;
                    StartDate = objs.columns.Where(b => b.Id == "StartDate").Count() > 0;
                    EndDate = objs.columns.Where(b => b.Id == "EndDate").Count() > 0;
                    LicenseNo = objs.columns.Where(b => b.Id == "LicenseNo").Count() > 0;
                    FacilityPlace = objs.columns.Where(b => b.Id == "FacilityPlace").Count() > 0;
                    Responsor = objs.columns.Where(b => b.Id == "Responsor").Count() > 0;
                    FacilityPhone = objs.columns.Where(b => b.Id == "FacilityPhone").Count() > 0;
                    Address = objs.columns.Where(b => b.Id == "Address").Count() > 0;
                    AddressNo = objs.columns.Where(b => b.Id == "AddressNo").Count() > 0;
                    insurance = objs.columns.Where(b => b.Id == "insurance").Count() > 0;
                    LandPriority = objs.columns.Where(b => b.Id == "LandPriority").Count() > 0;
                    LandTotalSquare = objs.columns.Where(b => b.Id == "LandTotalSquare").Count() > 0;
                    LandClass = objs.columns.Where(b => b.Id == "LandClass").Count() > 0;
                    LandUsageZone = objs.columns.Where(b => b.Id == "LandUsageZone").Count() > 0;
                    iptFacilityType = objs.columns.Where(b => b.Id == "iptFacilityType").Count() > 0;
                    Tank = objs.columns.Where(b => b.Id == "Tank").Count() > 0;
                    Pump = objs.columns.Where(b => b.Id == "Pump").Count() > 0;
                    SoilClass = objs.columns.Where(b => b.Id == "SoilClass").Count() > 0;
                }

                var output = datas.Select(a => new
                {
                    a.案件編號,
                    a.縣市別,
                    a.設施名稱,
                    a.變更次數,
                    營業主體 = BusiOrg ? a.營業主體 : "",
                    使用狀況第一層 = UsageNames?a.使用狀況第一層 : "",
                    使用狀況第二層 = UsageNames ? a.使用狀況第二層 : "",
                    使用狀況第三層 = UsageNames ? a.使用狀況第三層 : "",
                    使用狀況第四層 = UsageNames ? a.使用狀況第四層 : "",
                    收件日期 = ExpiredDate ? a.收件日期 : null,
                    核准使用日期 = StartDate ? a.核准使用日期 : null,
                    結束使用日期 = EndDate ? a.結束使用日期 : null,
                    特許執照號碼 = LicenseNo ? a.特許執照號碼 : "",
                    設置場所第一層 = FacilityPlace ? a.設置場所第一層 : "",
                    設置場所第二層 = FacilityPlace ? a.設置場所第二層 : "",
                    設置場所其他 = FacilityPlace ? a.設置場所其他 : "",
                    設置場所基地 = FacilityPlace ? a.設置場所基地 : "",
                    負責人姓名 = Responsor ? a.負責人姓名 : "",
                    設施電話 = FacilityPhone ? a.設施電話 : "",
                    設施地址 = Address ? a.設施地址 : "",
                    設施地號 = AddressNo ? a.設施地號 : "",
                    保單號碼 = insurance ? a.保單號碼 : "",
                    保險公司名稱 = insurance ? a.保險公司名稱 : "",
                    保險公司名稱_其他 = insurance ? a.保險公司名稱_其他 : "",
                    保單有效期間_起 = insurance ? a.保單有效期間_起 : null,
                    保單有效期間_迄 = insurance ? a.保單有效期間_迄 : null,
                    保險類型 = insurance ? a.保險類型 : "",
                    土地權屬 = LandPriority ? a.土地權屬 : "",
                    用地總面積 = LandTotalSquare ? a.用地總面積 : null,
                    用地類別 = LandClass ? a.用地類別 : "",
                    土地使用分區 = LandUsageZone ? a.土地使用分區 : "",
                    設施狀況之設施類型 = iptFacilityType ? a.設施狀況之設施類型 : "",
                    設施狀況之油槽總數 = Tank ? a.設施狀況之油槽總數 : null,
                    加油槍數_單槍 = Pump ? a.加油槍數_單槍 : null,
                    加油槍數_雙槍 = Pump ? a.加油槍數_雙槍 : null,
                    加油槍數_四槍 = Pump ? a.加油槍數_四槍 : null,
                    加油槍數_六槍 = Pump ? a.加油槍數_六槍 : null,
                    加油槍數_八槍 = Pump ? a.加油槍數_八槍 : null,
                    加油槍數_總計 = Pump ? a.加油槍數_總計 : null,
                    油品種類 = SoilClass ? a.油品種類 : null,
                    儲槽容量_油罐車載油容量 = SoilClass ? a.儲槽容量_油罐車載油容量 : null,
                    儲槽位置數量_地上 = SoilClass ? a.儲槽位置數量_地上 : null,
                    儲槽位置數量_地下 = SoilClass ? a.儲槽位置數量_地下 : null,
                }).Distinct().ToList();

                //4.產出Dynamic資料 (給Excel)
                List<dynamic> list = new List<dynamic>();

                foreach (var data in output)
                {
                    dynamic f = new ExpandoObject();
                    f.案件編號 = data.案件編號;
                    f.縣市別 = data.縣市別;
                    f.設施名稱 = data.設施名稱;                    

                    //欄位挑選
                    if (objs.columns != null)
                    {
                        if (BusiOrg)
                        {
                            f.營業主體 = data.營業主體;
                        }
                        if (UsageNames)
                        {
                            f.使用狀況第一層 = data.使用狀況第一層;
                            f.使用狀況第二層 = data.使用狀況第二層;
                            f.使用狀況第三層 = data.使用狀況第三層;
                            f.使用狀況第四層 = data.使用狀況第四層;
                        }
                        if (ExpiredDate)
                        {
                            f.收件日期 = data.收件日期 == null ? "" : DateFormat.ToDate4((DateTime)data.收件日期);
                        }
                        if (StartDate)
                        {
                            f.核准使用日期 = data.核准使用日期 == null ? "" : DateFormat.ToDate4((DateTime)data.核准使用日期);
                        }
                        if (EndDate)
                        {
                            f.結束使用日期 = data.結束使用日期 == null ? "" : DateFormat.ToDate4((DateTime)data.結束使用日期);
                        }
                        if (LicenseNo)
                        {
                            f.特許執照號碼 = data.特許執照號碼;
                        }
                        if (FacilityPlace)
                        {
                            f.設置場所第一層 = data.設置場所第一層;
                        }
                        if (FacilityPlace)
                        {
                            f.設置場所第二層 = data.設置場所第二層;
                        }
                        if (FacilityPlace)
                        {
                            f.設置場所其他 = data.設置場所其他;
                        }
                        if (FacilityPlace)
                        {
                            f.設置場所基地 = data.設置場所基地;
                        }
                        if (Responsor)
                        {
                            f.負責人姓名 = data.負責人姓名;
                        }
                        if (FacilityPhone)
                        {
                            f.設施電話 = data.設施電話;
                        }
                        if (Address)
                        {
                            f.設施地址 = data.設施地址;
                        }
                        if (AddressNo)
                        {
                            f.設施地號 = data.設施地號;
                        }

                        if (insurance)
                        {
                            f.保單號碼 = data.保單號碼;
                            f.保險公司名稱 = data.保險公司名稱;
                            f.保險公司名稱_其他 = data.保險公司名稱_其他;
                            f.保單有效期間_起 = data.保單有效期間_起 == null ? "" : DateFormat.ToDate4((DateTime)data.保單有效期間_起);
                            f.保單有效期間_迄 = data.保單有效期間_迄 == null ? "" : DateFormat.ToDate4((DateTime)data.保單有效期間_迄);
                            f.保險類型 = data.保險類型;
                        }
                        if (LandPriority)
                        {
                            f.土地權屬 = data.土地權屬;
                        }
                        if (LandTotalSquare)
                        {
                            f.用地總面積 = data.用地總面積;
                        }
                        if (LandClass)
                        {
                            f.用地類別 = data.用地類別;
                        }
                        if (LandUsageZone)
                        {
                            f.土地使用分區 = data.土地使用分區;
                        }
                        if (iptFacilityType)
                        {
                            f.設施狀況之設施類型 = data.設施狀況之設施類型;
                        }
                        if (Tank)
                        {
                            f.設施狀況之油槽總數 = data.設施狀況之油槽總數;
                        }
                        if (Pump)
                        {
                            f.加油槍數_單槍 = data.加油槍數_單槍;
                            f.加油槍數_雙槍 = data.加油槍數_雙槍;
                            f.加油槍數_四槍 = data.加油槍數_四槍;
                            f.加油槍數_六槍 = data.加油槍數_六槍;
                            f.加油槍數_八槍 = data.加油槍數_八槍;
                            f.加油槍數_總計 = data.加油槍數_總計;
                        }
                        if (SoilClass)
                        {
                            f.油品種類 = data.油品種類;
                            f.儲槽容量_油罐車載油容量 = data.儲槽容量_油罐車載油容量;
                            f.儲槽位置數量_地上 = data.儲槽位置數量_地上;
                            f.儲槽位置數量_地下 = data.儲槽位置數量_地下;
                        }
                    }

                    //最後欄位顯示
                    f.變更次數 = data.變更次數;

                    f.SheetName = fileTitle;//sheep.名稱;
                    list.Add(f);
                }

                //查無符合資料表數
                if (list.Count == 0)
                {
                    return Json(new { result = false, errorMessage = "查無符合資料表數" }, JsonRequestBehavior.AllowGet);
                }

                //5.產出excel
                string fileName = OilGas.ExcelSpecHelper.GenerateExcelByLinqF1(fileTitle, titles, list, folder, "N");
                string path = folder + fileName;
                url = OilGas.Cm.PhysicalToUrl(path);
            }
            catch (Exception ex)
            {
                error = ex.Message + ex.StackTrace;
            }            

            if (url == "")
            {
                return Json(new { result = false, errorMessage = error }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = true, url = url }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ResetExport()
        {
            try
            {
                Rpt_SelfFuel_BasicData.ResetGetAllDatas();
                Rpt_SelfFuel_BasicData.GetAllDatas();

                Rpt_SelfFuel_BasicData.ResetGetLogAllDatas();
                Rpt_SelfFuel_BasicData.GetLogAllDatas();
            }
            catch (Exception ex)
            {
                string error = ex.Message + ex.StackTrace;
                return Json(new { result = false, errorMessage = error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }
    }
}