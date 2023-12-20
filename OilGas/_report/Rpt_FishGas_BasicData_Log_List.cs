using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilGas
{
    /// <summary>
    /// 漁船加油站/C統計報表專區/變更歷程-基本資料欄位清單
    /// </summary>
    public class Rpt_FishGas_BasicData_Log_List
    {
        static object lockGetAllDatas = new object();
        public static IEnumerable<FishGas_Log_1> GetAllDatas(int cachetimer = 0)
        {
            if (cachetimer == 0) cachetimer = Constant.cacheBigReportTime;

            string key = "OilGas.Rpt_FishGas_BasicData_Log_List";
            var allData = DouHelper.Misc.GetCache<IEnumerable<FishGas_Log_1>>(cachetimer, key);
            lock (lockGetAllDatas)
            {
                if (allData == null)
                //if(true)
                {
                    System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();

                    Dou.Models.DB.IModelEntity<FishGas_BasicData_Log> fishGas_BasicData_Log = new Dou.Models.DB.ModelEntity<FishGas_BasicData_Log>(dbContext);
                    Dou.Models.DB.IModelEntity<FishGas_OilData_Log> fishGas_OilData_Log = new Dou.Models.DB.ModelEntity<FishGas_OilData_Log>(dbContext);
                    Dou.Models.DB.IModelEntity<FishGas_Insurance_Log> fishGas_Insurance_Log = new Dou.Models.DB.ModelEntity<FishGas_Insurance_Log>(dbContext);
                    Dou.Models.DB.IModelEntity<FishGas_Dispatch_Log> fishGas_Dispatch_Log = new Dou.Models.DB.ModelEntity<FishGas_Dispatch_Log>(dbContext);
                    Dou.Models.DB.IModelEntity<UsageStateCode> usageStateCode = new Dou.Models.DB.ModelEntity<UsageStateCode>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> carVehicleGas_BusinessOrganization = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_InsuranceCompanyName> carVehicleGas_InsuranceCompanyName = new Dou.Models.DB.ModelEntity<CarVehicleGas_InsuranceCompanyName>(dbContext);
                    Dou.Models.DB.IModelEntity<LandClassCode> landClassCode = new Dou.Models.DB.ModelEntity<LandClassCode>(dbContext);
                    Dou.Models.DB.IModelEntity<LandUsageZoneCode> landUsageZoneCode = new Dou.Models.DB.ModelEntity<LandUsageZoneCode>(dbContext);

                    var d1 = fishGas_BasicData_Log.GetAll().GroupJoin(fishGas_OilData_Log.GetAll(), a => a.CaseNo, b => b.CaseNo, (o, c) => new {  o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Mod_date, 
                                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Tank, o.Flowmeter, o.Oil_barge, o.Fire_Safety,o.Pollution_Prevention, c })
                                    .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new {
                                        o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, SoilServerData = o.SoilServerData.Trim(), o.Business_theme, o.otherBusiness_theme, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Mod_date, 
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Tank, o.Flowmeter, o.Oil_barge, o.Fire_Safety, o.Pollution_Prevention,
                                        c.Tank_type_tank, c.Tank_type_tank_seat, c.Oil_type, c.Tank_type
                                    })
                                    //.Where(a => a.案件編號 != null && a.案件編號 != "")
                                    //.Where(a => a.CaseNo == "F083150001")  /////////////
                                    .Distinct().ToList();

                    //fishGas_Dispatch_Log
                    var m1 = fishGas_Dispatch_Log.GetAll().Select(o => new
                    {
                        o.CaseNo, o.DispatchClass,o.Dispatch_date
                    });

                    //fishGas_Insurance_Log
                    var d2 = fishGas_Insurance_Log.GetAll().GroupJoin(carVehicleGas_InsuranceCompanyName.GetAll(), a=> a.Insurance_Company, b=>b.Value, (o, c) => new
                    {
                        o.CaseNo, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type,
                        CompanyName = c.FirstOrDefault().Name                                                
                    }).Distinct().ToList();

                    //usageStateCode
                    var d3 = usageStateCode.GetAll().Select(o => new { 
                        o.Value, o.Type, o.ShortName 
                    });

                    //carVehicleGas_BusinessOrganization
                    var d4 = carVehicleGas_BusinessOrganization.GetAll().Select(o => new { 
                        o.Value,
                        bus_name = o.Name
                    });

                    //landClassCode
                    var d5 = landClassCode.GetAll().Select(o => new
                    {
                        o.Value,
                        LandClassName = o.Name
                    });

                    //landUsageZoneCode
                    var d6 = landUsageZoneCode.GetAll().Select(o => new
                    {
                        o.Value,
                        LandUsageName = o.Name
                    });

                    allData =  d1
                                .GroupJoin(m1, a => a.CaseNo, b => b.CaseNo, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Mod_date,  
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Tank, o.Flowmeter, o.Oil_barge, o.Fire_Safety, o.Pollution_Prevention, o.Tank_type_tank, o.Tank_type_tank_seat, o.Oil_type, o.Tank_type, c })
                                    .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new {
                                        o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Mod_date, 
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Tank, o.Flowmeter, o.Oil_barge, o.Fire_Safety, o.Pollution_Prevention, o.Tank_type_tank, o.Tank_type_tank_seat, o.Oil_type, o.Tank_type,
                                        DispatchClass = c == null ? "": c.DispatchClass,
                                        Dispatch_date = c == null ? null : c.Dispatch_date
                                    })
                                .GroupJoin(d2, a => a.CaseNo, b => b.CaseNo, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Mod_date, o.DispatchClass, o.Dispatch_date, 
                                    o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Tank, o.Flowmeter, o.Oil_barge, o.Fire_Safety, o.Pollution_Prevention, o.Tank_type_tank, o.Tank_type_tank_seat, o.Oil_type, o.Tank_type, c })
                                .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new {
                                    o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Mod_date, o.DispatchClass, o.Dispatch_date, 
                                    o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Tank, o.Flowmeter, o.Oil_barge, o.Fire_Safety, o.Pollution_Prevention, o.Tank_type_tank, o.Tank_type_tank_seat, o.Oil_type, o.Tank_type,                                    
                                    CompanyName = c == null ? "" : c.CompanyName,
                                    Insurance_Company = c == null ? "" : c.Insurance_Company, 
                                    Insurance_otherCompany = c == null ? "" : c.Insurance_otherCompany, 
                                    Insurance_No = c == null ? "" : c.Insurance_No, 
                                    Insurance_policy_start = c == null ? null : c.Insurance_policy_start, 
                                    Insurance_policy_end = c == null ? null : c.Insurance_policy_end, 
                                    Insurance_Type = c == null ? "" : c.Insurance_Type,
                                })
                                .GroupJoin(d3, a => a.UsageState, b => b.Value, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Mod_date, o.DispatchClass, o.Dispatch_date, 
                                    o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Tank, o.Flowmeter, o.Oil_barge, o.Fire_Safety, o.Pollution_Prevention, o.Tank_type_tank, o.Tank_type_tank_seat, o.Oil_type, o.Tank_type, o.CompanyName, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type,
                                    Type = c.FirstOrDefault() == null? "": c.FirstOrDefault().Type,
                                    ShortName = (c.FirstOrDefault() == null ? "" : c.FirstOrDefault().ShortName)
                                })
                                .GroupJoin(d4, a => a.Business_theme, b => b.Value, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Mod_date, o.DispatchClass, o.Dispatch_date, 
                                    o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Tank, o.Flowmeter, o.Oil_barge, o.Fire_Safety, o.Pollution_Prevention, o.Tank_type_tank, o.Tank_type_tank_seat, o.Oil_type, o.Tank_type, o.CompanyName, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type,o.Type, o.ShortName,
                                    bus_name = c.FirstOrDefault() == null? null : c.FirstOrDefault().bus_name
                                })
                                .GroupJoin(d5, a => a.LandClass, b => b.Value, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Mod_date, o.DispatchClass, o.Dispatch_date, 
                                    o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Tank, o.Flowmeter, o.Oil_barge, o.Fire_Safety, o.Pollution_Prevention, o.Tank_type_tank, o.Tank_type_tank_seat, o.Oil_type, o.Tank_type, o.CompanyName, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type, o.Type, o.ShortName, o.bus_name,                                    
                                    LandClassName = c.FirstOrDefault() == null ? null : c.FirstOrDefault().LandClassName
                                })
                                .GroupJoin(d6, a => a.LandUsageZone, b => b.Value, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Mod_date, o.DispatchClass, o.Dispatch_date, 
                                    o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Tank, o.Flowmeter, o.Oil_barge, o.Fire_Safety, o.Pollution_Prevention, o.Tank_type_tank, o.Tank_type_tank_seat, o.Oil_type, o.Tank_type, o.CompanyName, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type, o.Type, o.ShortName, o.bus_name, o.LandClassName,
                                    LandUsageName = c.FirstOrDefault() == null ? null : c.FirstOrDefault().LandUsageName
                                })
                                .Select(o => new FishGas_Log_1()
                                {
                                    案件編號 = o.CaseNo,
                                    加油站名稱 = o.Gas_Name,
                                    鄉鎮市區 = o.ZipCode,
                                    營業日期 = o.OperationDate,
                                    收件日期 = o.Recipient_date,
                                    負責人姓名 = o.Boss,
                                    漁船加油站電話 = o.TelNo,
                                    LicenseNo1 = o.LicenseNo1,
                                    LicenseNo2 = o.LicenseNo2,
                                    LicenseNo3 = o.LicenseNo3,
                                    漁船加油站地址 = o.Address,
                                    漁船加油站地號 = o.AddressNo,
                                    核發證號日期 = o.Report_date,
                                    油品供應商 = o.SoilServerData,
                                    SoilServerData = o.SoilServerData,
                                    營業主體 = o.bus_name,
                                    Business_theme = o.Business_theme,
                                    otherBusiness_theme = o.otherBusiness_theme,
                                    營業別 = o.Type,
                                    營運狀態 = o.ShortName,
                                    保險公司名稱 = o.CompanyName,
                                    Insurance_otherCompany = o.Insurance_otherCompany,
                                    保險號碼 = o.Insurance_No,
                                    保單有效期限_起 = o.Insurance_policy_start,
                                    保單有效期限_迄 = o.Insurance_policy_end,
                                    保單類型 = o.Insurance_Type,
                                    土地權屬 = o.LandPriority,
                                    用地總面積 = o.Land_acreage,
                                    用地類別 = o.LandClassName,
                                    土地使用分區 = o.LandUsageName,
                                    LandClass = o.LandClass,
                                    OtherLandClass = o.OtherLandClass,
                                    LandUsageZone = o.LandUsageZone,
                                    OtherLandUsageZone = o.OtherLandUsageZone,
                                    流量計 = o.Flowmeter,
                                    單槍 = o.one_gun,
                                    雙槍 = o.two_gun,
                                    四槍 = o.four_gun,
                                    六槍 = o.six_gun,
                                    八槍 = o.eight_gun,
                                    油槽總數 = o.Tank,
                                    油駁船加油 = o.Oil_barge,
                                    消防安全措施 = o.Fire_Safety,
                                    污染防治措施 = o.Pollution_Prevention,
                                    販售油品種類 = o.Oil_type,
                                    油槽種類 = o.Tank_type,
                                    油槽種類_公秉 = o.Tank_type_tank,
                                    油槽種類_座 = o.Tank_type_tank_seat,
                                    Mod_date = o.Mod_date,
                                    Dispatch_date = o.Dispatch_date,
                                    UsageState = o.UsageState
                                });

                    allData = allData.Distinct().OrderBy(a => a.案件編號).ToList();

                    //代碼-縣市別
                    List<CityCode> citys = new List<CityCode>();
                    Dou.Models.DB.IModelEntity<CityCode> cityCode = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
                    foreach (var item in cityCode.GetAll().ToList())
                    {
                        string[] strs = item.GSLCode.Split(',');
                        if (strs.Count() > 1)
                        {
                            CityCode city = item;
                            foreach (string str in strs)
                            {
                                city = item.Clone();

                                city.GSLCode = str;
                                citys.Add(city);
                            }
                        }
                        else
                        {
                            citys.Add(item);
                        }
                    }

                    foreach (var data in allData)
                    {
                        if (!string.IsNullOrEmpty(data.案件編號))
                        {
                            var c = citys.Where(a => a.GSLCode == data.案件編號.Substring(4, 2)).FirstOrDefault();
                            data.縣市別 = c == null ? "" : c.CityName;
                        }

                        string str = "";
                        DateTime? date = DateTime.MinValue;

                        data.加油站名稱 = data.加油站名稱 == null ? "" : data.加油站名稱.Trim();
                        data.負責人姓名 = data.負責人姓名 == null ? "" : data.負責人姓名.TrimEnd();  //data.負責人姓名.Trim() 舊系統用R
                        data.漁船加油站電話 = data.漁船加油站電話 == null ? " " : data.漁船加油站電話.TrimEnd();  //data.負責人姓名.Trim() 舊系統用R  (另:舊" "和Null算2筆)

                        //經營許可證號
                        str = "";
                        string no1 = data.LicenseNo1 == null ? " " : data.LicenseNo1.Trim();
                        string no2 = data.LicenseNo2 == null ? "" : data.LicenseNo2.Trim();
                        string no3 = data.LicenseNo3 == null ? " " : data.LicenseNo3.Trim();
                        if (!string.IsNullOrEmpty(no2))
                            str = no1 + "字第" + no2 + "之" + no3 + "號";
                        else
                            str = no1 + no2 + no3;
                        data.經營許可證號 = str;

                        if (data.油品供應商 != null)
                        {
                            data.油品供應商 = Code.GetSoilServerName(data.油品供應商);
                        }
                        else
                        {
                            data.油品供應商 = Code.GetSoilServerName("");
                        }

                        //營業主體
                        str = data.營業主體;
                        if (data.Business_theme != null)
                        {
                            if (data.Business_theme == "16")
                                str = data.otherBusiness_theme == null ? " " : data.otherBusiness_theme.Trim();  //(另:舊" "和Null算2筆)
                        }
                        data.營業主體 = str;

                        data.otherBusiness_theme = data.otherBusiness_theme == null ? null : data.otherBusiness_theme.Trim();

                        //保險公司名稱
                        data.保險公司名稱 = data.保險公司名稱 == "其他" ? data.Insurance_otherCompany : data.保險公司名稱;

                        //保單有效期限
                        if (data.保單有效期限_起 != null && data.保單有效期限_迄 != null)
                        {
                            data.保單有效期限 = DateFormat.ToDate4((DateTime)data.保單有效期限_起) + "~" + DateFormat.ToDate4((DateTime)data.保單有效期限_迄);
                        }
                        //保單類型
                        if (data.保單類型 != null)
                        {
                            data.保單類型 = data.保單類型.Replace("0", "公共意外責任保險").Replace("1", "意外污染責任保險");
                        }
                        //土地權屬
                        str = "";
                        if (data.土地權屬 == "1")
                        {
                            str = "自用";
                        }
                        else if (data.土地權屬 == "2")
                        {
                            str = "租用";
                        }
                        else if (data.土地權屬 == "3")
                        {
                            str = "自用及租用";
                        }
                        data.土地權屬 = str;

                        //土地使用分區
                        //case cb.LandUsageZone when '99' then cb.OtherLandUsageZone when '11' then luc.Name+':'+lc.Name else luc.Name End  [土地使用分區]
                        str = "";
                        if (data.LandUsageZone == "99")
                        {
                            str = data.OtherLandUsageZone;
                        }
                        else if (data.LandUsageZone == "11")
                        {
                            str = data.土地使用分區 + ':' + data.用地類別;
                        }
                        else
                        {
                            str = data.土地使用分區;
                        }
                        data.土地使用分區 = str;

                        //用地類別
                        //case when cb.LandClass in ('98','99') then cb.OtherLandClass when cb.LandUsageZone = '11' then '' else lc.Name End  [用地類別]
                        str = "";
                        if (data.LandClass == "98" || data.LandClass == "99")
                        {
                            str = data.OtherLandClass;
                        }
                        else if (data.LandUsageZone == "11")
                        {
                            str = "";
                        }
                        else
                        {
                            str = data.用地類別;
                        }
                        data.用地類別 = str;

                        data.流量計 = data.流量計 == null ? null : data.流量計;
                        data.單槍 = data.單槍 == null ? 0 : data.單槍;
                        data.雙槍 = data.雙槍 == null ? 0 : data.雙槍;
                        data.四槍 = data.四槍 == null ? 0 : data.四槍;
                        data.六槍 = data.六槍 == null ? 0 : data.六槍;
                        data.八槍 = data.八槍 == null ? 0 : data.八槍;
                        data.油槽總數 = data.油槽總數 == null ? 0 : data.油槽總數;
                    }

                    DouHelper.Misc.AddCache(allData, key);
                }
            }

            return allData;
        }

        public static void ResetGetAllDatas()
        {
            string key = "OilGas.Rpt_FishGas_BasicData_Log_List";
            DouHelper.Misc.ClearCache(key);
        }

        public class FishGas_Log_1
        {
            public string 案件編號 { get; set; }
            public string 加油站名稱 { get; set; }
            public string 縣市別 { get; set; }
            public string 鄉鎮市區 { get; set; }
            public DateTime? 營業日期 { get; set; }
            public DateTime? 收件日期 { get; set; }
            public string 負責人姓名 { get; set; }
            public string 漁船加油站電話 { get; set; }
            public string 經營許可證號 { get; set; }
            public string LicenseNo1 { get; set; }
            public string LicenseNo2 { get; set; }
            public string LicenseNo3 { get; set; }
            public string 漁船加油站地址 { get; set; }
            public string 漁船加油站地號 { get; set; }
            public DateTime? 核發證號日期 { get; set; }
            public string 油品供應商 { get; set; }
            public string SoilServerData { get; set; }
            public string 營業主體 { get; set; }
            public string Business_theme { get; set; }
            public string otherBusiness_theme { get; set; }
            public string 營業別 { get; set; }
            public string 營運狀態 { get; set; }
            ////public string DispatchClass { get; set; }
            public string 保險公司名稱 { get; set; }
            public string Insurance_otherCompany { get; set; }
            public string 保險號碼 { get; set; }
            public string 保單有效期限 { get; set; }
            public DateTime? 保單有效期限_起 { get; set; }
            public DateTime? 保單有效期限_迄 { get; set; }
            public string 保單類型 { get; set; }
            public string 土地權屬 { get; set; }
            public double? 用地總面積 { get; set; }
            public string 用地類別 { get; set; }
            public string 土地使用分區 { get; set; }
            public string LandClass { get; set; }
            public string OtherLandClass { get; set; }
            public string LandUsageZone { get; set; }
            public string OtherLandUsageZone { get; set; }
            public int? 流量計 { get; set; }
            public int? 單槍 { get; set; }
            public int? 雙槍 { get; set; }
            public int? 四槍 { get; set; }
            public int? 六槍 { get; set; }
            public int? 八槍 { get; set; }
            public int? 油槽總數 { get; set; }
            public string 油駁船加油 { get; set; }
            public string 消防安全措施 { get; set; }
            public string 污染防治措施 { get; set; }

            public string 販售油品種類 { get; set; }
            public string 油槽種類 { get; set; }
            public int? 油槽種類_公秉 { get; set; }
            public int? 油槽種類_座 { get; set; }
            public DateTime? Mod_date { get; set; }
            public DateTime? Dispatch_date { get; set; }
            public string UsageState { get; set; }
        }
    }
}