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
using System.Security.Cryptography;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "CarFuel_BasicData_List", Name = "現況資料-基本資料欄位清單", MenuPath = "加油站/A統計報表專區", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]    
    public class CarFuel_BasicData_ListController : Controller
    {
        // GET: CarFuel_BasicData_List
        public ActionResult Index()
        {
            if (Dou.Context.CurrentUserBase == null)
            {
                return Redirect("~/Home/Index");
            }

            //未知(server端第一次實體化會影響效能)
            //ancillaryFacility.GetAll().OrderBy(a => a.Rank).ToList();

            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();

            //代碼-營業主體
            Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> carVehicleGas_BusinessOrganization = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);
            ViewBag.carVehicleGas_BusinessOrganization = carVehicleGas_BusinessOrganization.GetAll().Where(a=>a.IsEnable == true).OrderBy(a => a.Rank).ToList();

            //附屬設施 AncillaryFacility
            Dou.Models.DB.IModelEntity<AncillaryFacility> ancillaryFacility = new Dou.Models.DB.ModelEntity<AncillaryFacility>(dbContext);
            ViewBag.ancillaryFacility = ancillaryFacility.GetAll().OrderBy(a => a.Rank).ToList();

            //兼營設施 CarVehicleGas_Facility
            Dou.Models.DB.IModelEntity<CarVehicleGas_Facility> carVehicleGas_Facility = new Dou.Models.DB.ModelEntity<CarVehicleGas_Facility>(dbContext);
            ViewBag.carVehicleGas_Facility = carVehicleGas_Facility.GetAll().OrderBy(a => a.Rank).ToList();

            //營運狀態 UsageStateCode
            Dou.Models.DB.IModelEntity<UsageStateCode> usageStateCode = new Dou.Models.DB.ModelEntity<UsageStateCode>(dbContext);
            ViewBag.usageStateCode = usageStateCode.GetAll().OrderBy(a => a.Rank).ToList();

            //登入者縣市
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysGSLs();
            var userCitys = pCitys;
            ViewBag.userCitys = userCitys;

            //代碼-縣市別
            Dou.Models.DB.IModelEntity<CityCode> cityCode = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
            ViewBag.cityCode = cityCode.GetAll().Where(a => userCitys.Any(b => b == a.GSLCode))
                                    .OrderBy(a => a.Rank).ToList();

            //ViewBag.cityCode = Dou.Context.CurrentUser<User>().PowerCity.ToList();

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

        public ActionResult ExportCarFuel_BasicData_List(ReportData.ViewParams objs)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.加油站_A統計報表專區_現況資料_基本資料欄位清單);
            string fileTitle = "加油站_基本資料欄位清單";


            try
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                List<string> titles = new List<string>() { "加油站_現況資料，查詢條件:" };

                //1.取得資料
                System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();                
                var datas = OilGas.Rpt_CarFuel_BasicData_List.GetAllDatas();
                var n = datas.Count();

                //權限查詢
                var pCitys = Dou.Context.CurrentUser<User>().PowerCitysGSLs();
                datas = datas.Where(a => pCitys.Any(b => a.案件編號.Substring(4, 2) == b));

                //輸出欄位 (特殊狀況:列管現況有條件顯示欄位->公告污染場址類型,公告日期,解列日期)
                bool spec_1 = false;

                //2.條件篩選 (conditions)                                    
                if (objs.conditions != null)
                {
                    var txt_mod_date = objs.conditions.Where(a => a.Id == "txt_mod_date").FirstOrDefault();
                    if (txt_mod_date != null)
                    {
                        DateTime date = DateTime.Parse(txt_mod_date.Value);
                        datas = datas.Where(a => a.Mod_date >= date);
                        titles.Add("查詢期間(現況資料修改時間)起:" + txt_mod_date.Value);
                    }

                    var txt_mod_dateE = objs.conditions.Where(a => a.Id == "txt_mod_dateE").FirstOrDefault();
                    if (txt_mod_dateE != null)
                    {
                        DateTime date = DateTime.Parse(txt_mod_dateE.Value);
                        datas = datas.Where(a => a.Mod_date <= date);
                        titles.Add("查詢期間(現況資料修改時間)迄:" + txt_mod_dateE.Value);
                    }

                    var txt_Recipient_dateS = objs.conditions.Where(a => a.Id == "txt_Recipient_dateS").FirstOrDefault();
                    if (txt_Recipient_dateS != null)
                    {
                        //收件日期(Recipient_date)
                        DateTime date = DateTime.Parse(txt_Recipient_dateS.Value);
                        datas = datas.Where(a => a.收件日期 >= date);
                        titles.Add("收件日期起:" + txt_Recipient_dateS.Value);
                    }

                    var txt_Recipient_dateE = objs.conditions.Where(a => a.Id == "txt_Recipient_dateE").FirstOrDefault();
                    if (txt_Recipient_dateE != null)
                    {
                        //收件日期(Recipient_date)
                        DateTime date = DateTime.Parse(txt_Recipient_dateE.Value);
                        datas = datas.Where(a => a.收件日期 <= date);
                        titles.Add("收件日期迄:" + txt_Recipient_dateE.Value);
                    }

                    var txt_Dispatch_dateS = objs.conditions.Where(a => a.Id == "txt_Dispatch_dateS").FirstOrDefault();
                    if (txt_Dispatch_dateS != null)
                    {
                        DateTime date = DateTime.Parse(txt_Dispatch_dateS.Value);
                        datas = datas.Where(a => a.Dispatch_date >= date);
                        titles.Add("發文日期起:" + txt_Dispatch_dateS.Value);
                    }

                    var txt_Dispatch_dateE = objs.conditions.Where(a => a.Id == "txt_Dispatch_dateE").FirstOrDefault();
                    if (txt_Dispatch_dateE != null)
                    {
                        DateTime date = DateTime.Parse(txt_Dispatch_dateE.Value);
                        datas = datas.Where(a => a.Dispatch_date <= date);
                        titles.Add("發文日期迄:" + txt_Dispatch_dateE.Value);
                    }

                    var CaseNoS = objs.conditions.Where(a => a.Id == "CaseNoS").FirstOrDefault();
                    if (CaseNoS != null)
                    {                        
                        int num = int.Parse(CaseNoS.Value.ToString());
                        datas = datas.Where(a => int.Parse((a.案件編號).ToString().Substring(1, 9)) >= num);
                        titles.Add("案件編號起:" + num.ToString());
                    }

                    var CaseNoE = objs.conditions.Where(a => a.Id == "CaseNoE").FirstOrDefault();
                    if (CaseNoE != null)
                    {
                        int num = int.Parse(CaseNoE.Value.ToString());
                        datas = datas.Where(a => int.Parse((a.案件編號).ToString().Substring(1, 9)) <= num);
                        titles.Add("案件編號迄:" + num.ToString());
                    }

                    var ddl_City = objs.conditions.Where(a => a.Id == "ddl_City").FirstOrDefault();
                    if (ddl_City != null)
                    {
                        string str = ddl_City.Value;
                        List<string> sels = str.Split(',').ToList();                        
                        datas = datas.Where(a => sels.Any(b => a.案件編號.Substring(4, 2) == b));

                        //代碼-縣市別
                        Dou.Models.DB.IModelEntity<CityCode> cityCode = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
                        titles.Add("縣市:" + cityCode.GetAll().Where(a => sels.Any(b => a.GSLCode.IndexOf(b) > -1)).FirstOrDefault().CityName);
                    }

                    var ddl_Area = objs.conditions.Where(a => a.Id == "ddl_Area").FirstOrDefault();
                    if (ddl_Area != null)
                    {
                        string str = ddl_Area.Value.Trim();
                        datas = datas.Where(a => a.鄉鎮市區 == str);

                        //代碼-鄉鎮
                        Dou.Models.DB.IModelEntity<AreaCode> m_areaCode = new Dou.Models.DB.ModelEntity<AreaCode>(dbContext);
                        titles.Add("鄉鎮:" + m_areaCode.GetAll().Where(a => a.AreaCode1 == str).FirstOrDefault().AreaName);
                    }

                    var carVehicleGas_BusinessOrganization = objs.conditions.Where(a => a.Id == "carVehicleGas_BusinessOrganization").FirstOrDefault();
                    if (carVehicleGas_BusinessOrganization != null)
                    {                        
                        List<string> strs = objs.conditions.Where(a => a.Id == "carVehicleGas_BusinessOrganization").Select(a => a.Value).ToList();
                        datas = datas.Where(a => strs.Contains(a.Business_theme));

                        //titles add
                        //代碼-營業主體
                        Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> dal = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);
                        var text = dal.GetAll().Where(a => strs.Any(b => b == a.Value)).Select(a => a.Name).ToList();
                        titles.Add("營業主體:" + string.Join(",", text));
                    }

                    //因id(radio)要唯一，才能使用label for
                    List<string> rblUsageStates = new List<string>() { "rblUsageState1_1", "rblUsageState1_2", "rblUsageState1_3", "rblUsageState1_4" };
                    var rblUsageState1 = objs.conditions.Where(a => rblUsageStates.Any(b => b == a.Id)).FirstOrDefault();
                    if (rblUsageState1 != null)
                    {
                        string str = rblUsageState1.Value;
                        if (str != "")
                        {
                            datas = datas.Where(a => a.營業別 == str);
                        }

                        titles.Add("營業別:" + str);
                    }

                    var ddl_UsageState = objs.conditions.Where(a => a.Id == "ddl_UsageState").FirstOrDefault();
                    if (ddl_UsageState != null)
                    {
                        string str = ddl_UsageState.Value;
                        datas = datas.Where(a => a.UsageState == str);

                        //titles add
                        Dou.Models.DB.IModelEntity<UsageStateCode> usageStateCode = new Dou.Models.DB.ModelEntity<UsageStateCode>(dbContext);
                        titles.Add("營運狀態:" + usageStateCode.GetAll().Where(a => a.Value == str).FirstOrDefault().Name);
                    }

                    var ddl_GSM_Situation = objs.conditions.Where(a => a.Id == "ddl_GSM_Situation").FirstOrDefault();
                    if (ddl_GSM_Situation != null)
                    {
                        string ch1 = ddl_GSM_Situation.Value;
                        if (ch1 == "0")
                        {
                            //不限                            
                        }
                        if (ch1 == "1")
                        {
                            //列管
                            titles.Add("列管現況:" + "列管");

                            spec_1 = true;

                            var ddl_GSM_Situation_Control = objs.conditions.Where(a => a.Id == "ddl_GSM_Situation_Control").FirstOrDefault();
                            if (ddl_GSM_Situation_Control != null)
                            {
                                string str = ddl_GSM_Situation_Control.Value;
                                datas = datas.Where(a => a.公告污染場址類型 == str);
                                titles.Add("列管現況2:" + str);
                            }
                            else
                            {
                                List<string> ts = new List<string>() { "依細則第八條限期採取適當措施", "劃定地下水受污染限制使用地區及限制事項", "控制場址", "整治場址", "依七條五採取應變必要措施" };
                                datas = datas.Where(a => ts.Any(b => b == a.公告污染場址類型));
                            }
                        }
                        else if (ch1 == "2")
                        {
                            //解除列管
                            titles.Add("列管現況:" + "解除列管");

                            spec_1 = true;

                            var ddl_GSM_Situation_NotControl = objs.conditions.Where(a => a.Id == "ddl_GSM_Situation_NotControl").FirstOrDefault();
                            if (ddl_GSM_Situation_NotControl != null)
                            {
                                string str = ddl_GSM_Situation_NotControl.Value;
                                datas = datas.Where(a => a.公告污染場址類型 == str);
                                titles.Add("列管現況2:" + str);
                            }
                            else
                            {
                                List<string> ts = new List<string>() { "解除依細則第八條限期採取適當措施", "公告解除劃定地下水受污染限制使用地區及限制事項", "公告解除控制場址", "公告解除整治場址", "解除依七條五採取應變必要措施" };
                                datas = datas.Where(a => ts.Any(b => b == a.公告污染場址類型));
                            }
                        }
                        else if (ch1 == "3")
                        {
                            //未列管
                            titles.Add("列管現況:" + "未列管");

                            spec_1 = true;

                            datas = datas.Where(a => string.IsNullOrEmpty(a.公告污染場址類型));
                        }
                    }

                    List<string> oilServices = new List<string>() { "oilService_1", "oilService_2"};
                    var oilService = objs.conditions.Where(a => oilServices.Contains(a.Id)).ToList();
                    if (oilService.Count() > 0)
                    {
                        datas = datas.Where(a => oilService.Any(b => b.Value == a.SoilServerData));

                        //titles add
                        List<string> strs = new List<string>();
                        if (oilService.Any(a => a.Value == "1"))
                            strs.Add("台灣中油");
                        if (oilService.Any(a => a.Value == "2"))
                            strs.Add("台塑石化");
                        titles.Add("油品供應商:" + string.Join(",", strs));
                    }
                }

                //3.輸出欄位(distinct) (no distinct error:顯示全部欄位)
                bool cbOperationDate = false;
                bool cbRecipient_date = false;
                bool cbBoss = false;
                bool cbTelNo = false;
                bool cbLicenseNo = false;
                bool cbAddr = false;
                bool cbReport_date = false;
                bool cbSoilServerData = false;
                bool cbBusiness_theme = false;
                bool cbUsageState1 = false;
                bool cbUsageState = false;
                bool cbStopDate = false;
                bool ancillaryFacility_head = false;
                bool Insurance_Company = false;
                bool Insurance_No = false;
                bool Insurance_policy = false;
                bool Insurance_TypeN = false;
                bool LandPriority = false;
                bool Land_acreage = false;
                bool LandClass = false;
                bool LandUsageZone = false;
                bool carVehicleGas_Facility_head = false;
                bool Island = false;
                bool one_gun = false;
                bool two_gun = false;
                bool four_gun = false;
                bool six_gun = false;
                bool eight_gun = false;
                bool self_gun = false;
                bool self_one_gun = false;
                bool self_two_gun = false;
                bool self_four_gun = false;
                bool self_six_gun = false;
                bool self_eight_gun = false;
                bool Tank = false;
                bool Tank_type_tank = false;
                bool Tank_type_tank_seat = false;
                bool SaleSoilClass = false;

                if (objs.columns != null)
                {
                    cbOperationDate = objs.columns.Where(b => b.Id == "cbOperationDate").Count() > 0;
                    cbRecipient_date = objs.columns.Where(b => b.Id == "cbRecipient_date").Count() > 0;
                    cbBoss = objs.columns.Where(b => b.Id == "cbBoss").Count() > 0; ;
                    cbTelNo = objs.columns.Where(b => b.Id == "cbTelNo").Count() > 0;
                    cbLicenseNo = objs.columns.Where(b => b.Id == "cbLicenseNo").Count() > 0;
                    cbAddr = objs.columns.Where(b => b.Id == "cbAddr").Count() > 0;
                    cbReport_date = objs.columns.Where(b => b.Id == "cbReport_date").Count() > 0;
                    cbSoilServerData = objs.columns.Where(b => b.Id == "cbSoilServerData").Count() > 0;
                    cbBusiness_theme = objs.columns.Where(b => b.Id == "cbBusiness_theme").Count() > 0;
                    cbUsageState1 = objs.columns.Where(b => b.Id == "cbUsageState1").Count() > 0;
                    cbUsageState = objs.columns.Where(b => b.Id == "cbUsageState").Count() > 0;
                    cbStopDate = objs.columns.Where(b => b.Id == "cbStopDate").Count() > 0;
                    ancillaryFacility_head = objs.columns.Where(b => b.Id == "ancillaryFacility").Count() > 0;
                    Insurance_Company = objs.columns.Where(b => b.Id == "Insurance_Company").Count() > 0;
                    Insurance_No = objs.columns.Where(b => b.Id == "Insurance_No").Count() > 0;
                    Insurance_policy = objs.columns.Where(b => b.Id == "Insurance_policy").Count() > 0;
                    Insurance_TypeN = objs.columns.Where(b => b.Id == "Insurance_TypeN").Count() > 0;
                    LandPriority = objs.columns.Where(b => b.Id == "LandPriority").Count() > 0;
                    Land_acreage = objs.columns.Where(b => b.Id == "Land_acreage").Count() > 0;
                    LandClass = objs.columns.Where(b => b.Id == "LandClass").Count() > 0;
                    LandUsageZone = objs.columns.Where(b => b.Id == "LandUsageZone").Count() > 0;
                    carVehicleGas_Facility_head = objs.columns.Where(b => b.Id == "carVehicleGas_Facility").Count() > 0;
                    Island = objs.columns.Where(b => b.Id == "Island").Count() > 0;
                    one_gun = objs.columns.Where(b => b.Id == "one_gun").Count() > 0;
                    two_gun = objs.columns.Where(b => b.Id == "two_gun").Count() > 0;
                    four_gun = objs.columns.Where(b => b.Id == "four_gun").Count() > 0;
                    six_gun = objs.columns.Where(b => b.Id == "six_gun").Count() > 0;
                    eight_gun = objs.columns.Where(b => b.Id == "eight_gun").Count() > 0;
                    self_gun = objs.columns.Where(b => b.Id == "self_gun").Count() > 0;
                    self_one_gun = objs.columns.Where(b => b.Id == "self_one_gun").Count() > 0;
                    self_two_gun = objs.columns.Where(b => b.Id == "self_two_gun").Count() > 0;
                    self_four_gun = objs.columns.Where(b => b.Id == "self_four_gun").Count() > 0;
                    self_six_gun = objs.columns.Where(b => b.Id == "self_six_gun").Count() > 0;
                    self_eight_gun = objs.columns.Where(b => b.Id == "self_eight_gun").Count() > 0;
                    Tank = objs.columns.Where(b => b.Id == "Tank").Count() > 0;
                    Tank_type_tank = objs.columns.Where(b => b.Id == "Tank_type_tank").Count() > 0;
                    Tank_type_tank_seat = objs.columns.Where(b => b.Id == "Tank_type_tank_seat").Count() > 0;
                    SaleSoilClass = objs.columns.Where(b => b.Id == "SaleSoilClass").Count() > 0;                                        
                }

                var output = datas.Select(a => new
                {
                    a.案件編號,
                    a.加油站名稱,
                    a.縣市別,
                    a.鄉鎮市區,
                    a.地址,
                    營業日期 = cbOperationDate ? a.營業日期 : null,
                    收件日期 = cbRecipient_date ? a.收件日期 : null,
                    負責人姓名 = cbBoss ? a.負責人姓名 : "",
                    汽車加油站電話 = cbTelNo ? a.汽車加油站電話 : "",
                    經營許可證號 = cbLicenseNo ? a.經營許可證號 : "",
                    汽車加油站地址 = cbAddr ? a.汽車加油站地址 : "",
                    汽車加油站地號 = cbAddr ? a.汽車加油站地號 : "",
                    核發證號日期 = cbReport_date ? a.核發證號日期 : null,
                    油品供應商 = cbSoilServerData ? a.油品供應商 : "",
                    營業主體 = cbBusiness_theme ? a.營業主體 : "",
                    營業別 = cbUsageState1 ? a.營業別 : "",
                    營運狀態 = cbUsageState ? a.營運狀態 : "",
                    歇業日期 = cbStopDate ? a.歇業日期 : null,
                    附屬設施_項目 = ancillaryFacility_head ? a.附屬設施_項目 : "",
                    保險公司名稱 = Insurance_Company ? a.保險公司名稱 : "",
                    保險號碼 = Insurance_No ? a.保險號碼 : "",
                    保單有效期限 = Insurance_policy ? a.保單有效期限 : "",
                    保單類型 = Insurance_TypeN ? a.保單類型 : "",
                    土地權屬 = LandPriority ? a.土地權屬 : "",
                    用地總面積 = Land_acreage ? a.用地總面積 : null,
                    用地類別 = LandClass ? a.用地類別 : "",
                    土地使用分區 = LandUsageZone ? a.土地使用分區 : "",
                    兼營設施_項目 = carVehicleGas_Facility_head ? a.兼營設施_項目 : "",
                    加油泵島數 = Island ? a.加油泵島數 : null,
                    單槍 = one_gun ? a.單槍 : null,
                    雙槍 = two_gun ? a.雙槍 : null,
                    四槍 = four_gun ? a.四槍 : null,
                    六槍 = six_gun ? a.六槍 : null,
                    八槍 = eight_gun ? a.八槍 : null,
                    自助加油機數Str = self_gun ? a.自助加油機數Str : null,
                    self_單槍 = self_one_gun ? a.self_單槍 : null,
                    self_雙槍 = self_two_gun ? a.self_雙槍 : null,
                    self_四槍 = self_four_gun ? a.self_四槍 : null,
                    self_六槍 = self_six_gun ? a.self_六槍 : null,
                    self_八槍 = self_eight_gun ? a.self_八槍 : null,
                    油槽總數 = Tank ? a.油槽總數 : null,
                    儲槽容量_公秉 = Tank_type_tank ? a.儲槽容量_公秉 : null,
                    儲槽數量_座 = Tank_type_tank_seat ? a.儲槽數量_座 : null,
                    販售油品種類 = SaleSoilClass ? a.販售油品種類 : "",
                    公告污染場址類型 = spec_1 ? a.公告污染場址類型 : "",
                    公告日期 = spec_1 ? a.公告污染場址類型公告日期 : null,
                    解列日期 = spec_1 ? a.公告污染場址類型解列日期 : null,
                }).Distinct().ToList();

                //4.產出Dynamic資料 (給Excel)
                List<dynamic> list = new List<dynamic>();
                
                //代碼-保險資料
                Dou.Models.DB.IModelEntity<AncillaryFacility> ancillaryFacility = new Dou.Models.DB.ModelEntity<AncillaryFacility>(dbContext);
                var code_ancillaryFacility = ancillaryFacility.GetAll().OrderBy(a => a.Rank).ToList();
                //代碼-兼營設施
                Dou.Models.DB.IModelEntity<CarVehicleGas_Facility> carVehicleGas_Facility = new Dou.Models.DB.ModelEntity<CarVehicleGas_Facility>(dbContext);
                var code_carVehicleGas_Facility = carVehicleGas_Facility.GetAll().OrderBy(a => a.Rank).ToList();

                foreach (var data in output)
                {
                    dynamic f = new ExpandoObject();                    
                    f.案件編號 = data.案件編號;
                    f.加油站名稱 = data.加油站名稱;
                    f.縣市別 = data.縣市別;
                    f.鄉鎮市區 = data.鄉鎮市區;
                    f.地址 = data.地址;

                    //欄位挑選
                    if (objs.columns != null)
                    {
                        if (cbOperationDate)
                        {
                            f.營業日期 = data.營業日期 == null ? "" : DateFormat.ToDate4((DateTime)data.營業日期);
                        }
                        if (cbRecipient_date)
                        {                            
                            f.收件日期 = data.收件日期 == null ? "" : DateFormat.ToDate4((DateTime)data.收件日期);
                        }
                        if (cbBoss)
                        {
                            f.負責人姓名 = data.負責人姓名;
                        }
                        if (cbTelNo)
                        {
                            f.汽車加油站電話 = data.汽車加油站電話;
                        }
                        if (cbLicenseNo)
                        {
                            f.經營許可證號 = data.經營許可證號;
                        }
                        if (cbAddr)
                        {
                            f.汽車加油站地址 = data.汽車加油站地址;
                            f.汽車加油站地號 = data.汽車加油站地號;
                        }
                        if (cbReport_date)
                        {                            
                            f.核發證號日期 = data.核發證號日期 == null ? "" : DateFormat.ToDate4((DateTime)data.核發證號日期);
                        }
                        if (cbSoilServerData)
                        {
                            f.油品供應商 = data.油品供應商;
                        }
                        if (cbBusiness_theme)
                        {                            
                            f.營業主體 = data.營業主體;
                        }
                        if (cbUsageState1)
                        {
                            f.營業別 = data.營業別;
                        }
                        if (cbUsageState)
                        {
                            f.營運狀態 = data.營運狀態;
                        }
                        if (cbStopDate)
                        {                                                        
                            f.歇業日期 = data.歇業日期 == null ? "" : DateFormat.ToDate4((DateTime)data.歇業日期);
                        }
                        //動態多選
                        if (ancillaryFacility_head)
                        {
                            var z = objs.columns.Where(a => a.Id == "ancillaryFacility").Select(a => a.Value);
                            foreach (var item in code_ancillaryFacility.Where(a => z.Contains(a.Value)))
                            {
                                string str = "";
                                if (string.IsNullOrEmpty(data.附屬設施_項目))
                                {
                                    str = "無";
                                }
                                else
                                {
                                    //str = data.附屬設施_項目.Split(';').Contains(item.Value) ? "有" : "無";
                                    str = data.附屬設施_項目.Contains(item.Value) ? "有" : "無";
                                }
                                ((IDictionary<string, object>)f).Add(new KeyValuePair<string, object>(item.Name, str));
                            }
                        }
                        if (Insurance_Company)
                        {                            
                            f.保險公司名稱 = data.保險公司名稱;
                        }
                        if (Insurance_No)
                        {
                            f.保險號碼 = data.保險號碼;
                        }
                        if (Insurance_policy)
                        {
                            f.保單有效期限 = data.保單有效期限;
                        }
                        if (Insurance_TypeN)
                        {
                            f.保單類型 = data.保單類型;
                        }
                        if (LandPriority)
                        {                            
                            f.土地權屬 = data.土地權屬;
                        }
                        if (Land_acreage)
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
                        //動態多選
                        if (carVehicleGas_Facility_head)
                        {
                            var z = objs.columns.Where(a => a.Id == "carVehicleGas_Facility").Select(a => a.Value);
                            foreach (var item in code_carVehicleGas_Facility.Where(a => z.Contains(a.Value)))
                            {
                                string str = "";
                                if (string.IsNullOrEmpty(data.兼營設施_項目))
                                {
                                    str = "無";
                                }
                                else
                                {
                                    //str = data.兼營設施_項目.Split(';').Contains(item.Value) ? "有" : "無";
                                    str = data.兼營設施_項目.Contains(item.Value) ? "有" : "無";
                                }
                                ((IDictionary<string, object>)f).Add(new KeyValuePair<string, object>(item.Name, str));
                            }
                        }                       
                        if (Island)
                        {
                            f.加油泵島數 = data.加油泵島數;
                        }
                        if (one_gun)
                        {
                            f.單槍 = data.單槍;
                        }
                        if (two_gun)
                        {
                            f.雙槍 = data.雙槍;
                        }
                        if (four_gun)
                        {
                            f.四槍 = data.四槍;
                        }
                        if (six_gun)
                        {
                            f.六槍 = data.六槍;
                        }
                        if (eight_gun)
                        {
                            f.八槍 = data.八槍;
                        }
                        if (self_gun)
                        {
                            f.自助加油機數 = data.自助加油機數Str;
                        }
                        if (self_one_gun)
                        {
                            f.self_單槍 = data.self_單槍;
                        }
                        if (self_two_gun)
                        {
                            f.self_雙槍 = data.self_雙槍;
                        }
                        if (self_four_gun)
                        {
                            f.self_四槍 = data.self_四槍;
                        }
                        if (self_six_gun)
                        {
                            f.self_六槍 = data.self_六槍;
                        }
                        if (self_eight_gun)
                        {
                            f.self_八槍 = data.self_八槍;
                        }
                        if (Tank)
                        {
                            f.油槽總數 = data.油槽總數;
                        }
                        if (Tank_type_tank)
                        {
                            f.儲槽容量_公秉 = data.儲槽容量_公秉;
                        }
                        if (Tank_type_tank_seat)
                        {
                            f.儲槽數量_座 = data.儲槽數量_座;
                        }
                        if (SaleSoilClass)
                        {
                            f.販售油品種類 = data.販售油品種類;
                        }                        
                    }

                    if (spec_1)
                    {
                        f.公告污染場址類型 = data.公告污染場址類型;
                        f.公告日期 = data.公告日期 == null ? "" : DateFormat.ToDate4((DateTime)data.公告日期);
                        f.解列日期 = data.解列日期 == null ? "" : DateFormat.ToDate4((DateTime)data.解列日期);
                    }

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
                Rpt_CarFuel_BasicData_List.ResetGetAllDatas();
                Rpt_CarFuel_BasicData_List.GetAllDatas();
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