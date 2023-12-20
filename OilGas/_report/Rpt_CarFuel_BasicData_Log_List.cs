using Microsoft.Ajax.Utilities;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OilGas
{
    /// <summary>
    /// 加油站/A統計報表專區/變更歷程-基本資料欄位清單
    /// </summary>
    public class Rpt_CarFuel_BasicData_Log_List
    {
        static object lockGetAllDatas = new object();

        //1.SP(T-Sql):即時連結DB取得資料
        public class sp_Rpt_CarFuel_BasicData_Log_List
        {
            public string CaseNo { get; set; }
            public string Gas_Name { get; set; }
            public DateTime? OperationDate { get; set; }
            public DateTime? Recipient_date { get; set; }
            public string Boss { get; set; }
            public string TelNo { get; set; }
            public string LicenseNo1 { get; set; }
            public string LicenseNo2 { get; set; }
            public string LicenseNo3 { get; set; }
            public string Address { get; set; }
            public string AddressNo { get; set; }
            public DateTime? Report_date { get; set; }
            public string SoilServerData { get; set; }
            public string bus_name { get; set; }
            public string Business_theme { get; set; }
            public string otherBusiness_theme { get; set; }
            public string Type { get; set; }
            public string ShortName { get; set; }
            public DateTime? Dispatch_date { get; set; }
            public string DispatchClass { get; set; }
            public string AncillaryFacility { get; set; }
            public string CompanyName { get; set; }
            public string Insurance_otherCompany { get; set; }
            public string Insurance_No { get; set; }
            public string 保單有效期限 { get; set; }
            public DateTime? Insurance_policy_start { get; set; }
            public DateTime? Insurance_policy_end { get; set; }
            public string Insurance_Type { get; set; }
            public string LandPriority { get; set; }
            public double? Land_acreage { get; set; }
            public string LandClassName { get; set; }
            public string LandUsageName { get; set; }
            public string LandClass { get; set; }
            public string OtherLandClass { get; set; }
            public string LandUsageZone { get; set; }
            public string OtherLandUsageZone { get; set; }
            public string Facility { get; set; }
            public int? Island { get; set; }
            public int? one_gun { get; set; }
            public int? two_gun { get; set; }
            public int? four_gun { get; set; }
            public int? six_gun { get; set; }
            public int? eight_gun { get; set; }
            public int? Self_total_gun { get; set; }
            public int? Self_one_gun { get; set; }
            public int? Self_two_gun { get; set; }
            public int? Self_four_gun { get; set; }
            public int? Self_six_gun { get; set; }
            public int? Self_eight_gun { get; set; }
            public int? Tank { get; set; }
            public int? Tank_type_tank { get; set; }
            public int? Tank_type_tank_seat { get; set; }
            public string SaleSoilName { get; set; }
            public DateTime? Mod_date { get; set; }
            public string Situation { get; set; }
            public DateTime? Limit_Date { get; set; }
            public DateTime? take_Date { get; set; }
            public DateTime? GW_Date { get; set; }
            public DateTime? Control_Date { get; set; }
            public DateTime? Rem_Date { get; set; }
            public DateTime? Situation_Date { get; set; }
            public string UsageState { get; set; }
        }

        //2.Cache:從Cache取得資料
        public static IEnumerable<CarFuel_Log_1> GetAllDatas(int cachetimer = 0)
        {
            if (cachetimer == 0) cachetimer = Constant.cacheBigReportTime;

            string key = "OilGas.Rpt_CarFuel_BasicData_Log_List";
            var allData = DouHelper.Misc.GetCache<IEnumerable<CarFuel_Log_1>>(cachetimer, key);
            lock (lockGetAllDatas)
            {
                if (allData == null)
                //if(true)
                {
                    System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
                    dbContext.Database.CommandTimeout = 60 * 60;  //60分
                    Dou.Models.DB.IModelEntity<CarFuel_BasicData_Log> carFuel_BasicData_Log = new Dou.Models.DB.ModelEntity<CarFuel_BasicData_Log>(dbContext);
                    Dou.Models.DB.IModelEntity<CarFuel_OilData_Log> carFuel_OilData_Log = new Dou.Models.DB.ModelEntity<CarFuel_OilData_Log>(dbContext);
                    Dou.Models.DB.IModelEntity<CarFuel_Insurance_Log> carFuel_Insurance_Log = new Dou.Models.DB.ModelEntity<CarFuel_Insurance_Log>(dbContext);
                    Dou.Models.DB.IModelEntity<CarFuel_Dispatch_Log> carFuel_Dispatch_Log = new Dou.Models.DB.ModelEntity<CarFuel_Dispatch_Log>(dbContext);
                    Dou.Models.DB.IModelEntity<UsageStateCode> usageStateCode = new Dou.Models.DB.ModelEntity<UsageStateCode>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> carVehicleGas_BusinessOrganization = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_InsuranceCompanyName> carVehicleGas_InsuranceCompanyName = new Dou.Models.DB.ModelEntity<CarVehicleGas_InsuranceCompanyName>(dbContext);
                    Dou.Models.DB.IModelEntity<LandClassCode> landClassCode = new Dou.Models.DB.ModelEntity<LandClassCode>(dbContext);
                    Dou.Models.DB.IModelEntity<LandUsageZoneCode> landUsageZoneCode = new Dou.Models.DB.ModelEntity<LandUsageZoneCode>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_SaleSoilClass> carVehicleGas_SaleSoilClass = new Dou.Models.DB.ModelEntity<CarVehicleGas_SaleSoilClass>(dbContext);
                    Dou.Models.DB.IModelEntity<WS_GSM_Relation> wS_GSM_Relation = new Dou.Models.DB.ModelEntity<WS_GSM_Relation>(dbContext);
                    Dou.Models.DB.IModelEntity<WS_GSM> wS_GSM = new Dou.Models.DB.ModelEntity<WS_GSM>(dbContext);

                    var d1 = carFuel_BasicData_Log.GetAll().GroupJoin(carFuel_OilData_Log.GetAll(), a => a.CaseNo, b => b.CaseNo, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date,
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, c })
                                    .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new {
                                        o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, SoilServerData = o.SoilServerData.Trim(), o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, Facility = o.Facility.Trim(), o.Island, o.Mod_date,
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, c.Tank_type_tank, c.Tank_type_tank_seat, SaleSoilClass = c.SaleSoilClass.Trim()
                                    })
                                    //.Take(3000)////////////////////////
                                    //.Where(a => a.案件編號 != null && a.案件編號 != "")
                                    //.Where(a => a.CaseNo == "P079030006")  /////////////
                                    .Distinct().ToList();

                    //carFuel_Dispatch_Log
                    var m1 = carFuel_Dispatch_Log.GetAll().Select(o => new
                    {
                        o.CaseNo, o.DispatchClass,o.Dispatch_date
                    });

                    //carVehicleGas_InsuranceCompanyName
                    var d2 = carFuel_Insurance_Log.GetAll().GroupJoin(carVehicleGas_InsuranceCompanyName.GetAll(), a=> a.Insurance_Company, b=>b.Value, (o, c) => new
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

                    //carVehicleGas_SaleSoilClass
                    var d7 = carVehicleGas_SaleSoilClass.GetAll().Select(o => new
                    {
                        o.Value,
                        SaleSoilName = o.Name
                    });

                    //wS_GSM_Relation, wS_GSM
                    var GSM = wS_GSM_Relation.GetAll().GroupJoin(wS_GSM.GetAll(), a => a.FacNo, b => b.gsm_id, (o, c) => new
                    {
                        o.CaseNo, c
                    })
                    .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new
                    {
                        o.CaseNo,
                        c.Situation,
                        c.Limit_Date,
                        c.take_Date,
                        c.GW_Date,
                        c.Control_Date,
                        c.Rem_Date,
                        c.Situation_Date
                    });

                    allData =   d1.GroupJoin(m1, a => a.CaseNo, b => b.CaseNo, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date,  
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, c })
                                    .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new {
                                        o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, 
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass,
                                        DispatchClass = c == null ? "": c.DispatchClass,
                                        Dispatch_date = c == null ? null : c.Dispatch_date
                                    })
                                .GroupJoin(d2, a => a.CaseNo, b => b.CaseNo, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.DispatchClass, o.Dispatch_date, 
                                    o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, c })
                                .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new {
                                    o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.DispatchClass, o.Dispatch_date, 
                                    o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass,                                    
                                    CompanyName = c == null ? "" : c.CompanyName,
                                    Insurance_Company = c == null ? "" : c.Insurance_Company, 
                                    Insurance_otherCompany = c == null ? "" : c.Insurance_otherCompany, 
                                    Insurance_No = c == null ? "" : c.Insurance_No, 
                                    Insurance_policy_start = c == null ? null : c.Insurance_policy_start, 
                                    Insurance_policy_end = c == null ? null : c.Insurance_policy_end, 
                                    Insurance_Type = c == null ? "" : c.Insurance_Type,
                                })
                                .GroupJoin(d3, a => a.UsageState, b => b.Value, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.DispatchClass, o.Dispatch_date, 
                                    o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, o.CompanyName, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type,
                                    Type = c.FirstOrDefault() == null? "": c.FirstOrDefault().Type,
                                    ShortName = (c.FirstOrDefault() == null ? "1" : c.FirstOrDefault().ShortName)
                                })
                                .GroupJoin(d4, a => a.Business_theme, b => b.Value, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.DispatchClass, o.Dispatch_date, 
                                    o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, o.CompanyName, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type,o.Type, o.ShortName,
                                    bus_name = c.FirstOrDefault() == null? null : c.FirstOrDefault().bus_name
                                })
                                .GroupJoin(d5, a => a.LandClass, b => b.Value, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.DispatchClass, o.Dispatch_date, 
                                    o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, o.CompanyName, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type, o.Type, o.ShortName, o.bus_name,                                    
                                    LandClassName = c.FirstOrDefault() == null ? null : c.FirstOrDefault().LandClassName
                                })
                                .GroupJoin(d6, a => a.LandUsageZone, b => b.Value, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.DispatchClass, o.Dispatch_date, 
                                    o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, o.CompanyName, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type, o.Type, o.ShortName, o.bus_name, o.LandClassName,
                                    LandUsageName = c.FirstOrDefault() == null ? null : c.FirstOrDefault().LandUsageName
                                })
                                .GroupJoin(d7, a => a.SaleSoilClass, b => b.Value, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.DispatchClass, o.Dispatch_date, 
                                    o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, o.CompanyName, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type, o.Type, o.ShortName, o.bus_name, o.LandClassName, o.LandUsageName,
                                    SaleSoilName = c.FirstOrDefault() == null ? null : c.FirstOrDefault().SaleSoilName
                                })
                                .GroupJoin(GSM, a => a.CaseNo, b => b.CaseNo, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.DispatchClass, o.Dispatch_date, 
                                    o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, o.CompanyName, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type, o.Type, o.ShortName, o.bus_name, o.LandClassName, o.LandUsageName, o.SaleSoilName, c
                                })
                                .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new
                                {
                                    o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.DispatchClass, o.Dispatch_date, 
                                    o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, o.CompanyName, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type, o.Type, o.ShortName, o.bus_name, o.LandClassName, o.LandUsageName, o.SaleSoilName,                                                                        
                                    Situation = c == null ? null : c.Situation,
                                    Limit_Date = c == null ? null : c.Limit_Date,
                                    take_Date = c == null ? null : c.take_Date,
                                    GW_Date = c == null ? null : c.GW_Date,
                                    Control_Date = c == null ? null : c.Control_Date,
                                    Rem_Date = c == null ? null : c.Rem_Date,
                                    Situation_Date = c == null ? null : c.Situation_Date
                                })
                                .Select(o => new CarFuel_Log_1()
                                {
                                    案件編號 = o.CaseNo,
                                    加油站名稱 = o.Gas_Name,
                                    鄉鎮市區 = o.ZipCode,
                                    營業日期 = o.OperationDate,
                                    收件日期 = o.Recipient_date,
                                    負責人姓名 = o.Boss,
                                    汽車加油站電話 = o.TelNo,
                                    LicenseNo1 = o.LicenseNo1,
                                    LicenseNo2 = o.LicenseNo2,
                                    LicenseNo3 = o.LicenseNo3,
                                    汽車加油站地址 = o.Address,
                                    汽車加油站地號 = o.AddressNo,
                                    核發證號日期 = o.Report_date,
                                    油品供應商 = o.SoilServerData,
                                    SoilServerData = o.SoilServerData,
                                    營業主體 = o.bus_name,
                                    Business_theme = o.Business_theme,
                                    otherBusiness_theme = o.otherBusiness_theme,
                                    營業別 = o.Type,
                                    營運狀態 = o.ShortName,
                                    歇業日期 = o.Dispatch_date,
                                    DispatchClass = o.DispatchClass,                                   
                                    附屬設施_項目 = o.AncillaryFacility,
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
                                    兼營設施_項目 = o.Facility,
                                    加油泵島數 = o.Island,
                                    單槍 = o.one_gun,
                                    雙槍 = o.two_gun,
                                    四槍 = o.four_gun,
                                    六槍 = o.six_gun,
                                    八槍 = o.eight_gun,
                                    自助加油機數 = o.Self_total_gun,
                                    self_單槍 = o.Self_one_gun,
                                    self_雙槍 = o.Self_two_gun,
                                    self_四槍 = o.Self_four_gun,
                                    self_六槍 = o.Self_six_gun,
                                    self_八槍 = o.Self_eight_gun,
                                    油槽總數 = o.Tank,
                                    儲槽容量_公秉 = o.Tank_type_tank,
                                    儲槽數量_座 = o.Tank_type_tank_seat,
                                    販售油品種類 = o.SaleSoilName,
                                    Mod_date = o.Mod_date,
                                    Dispatch_date = o.Dispatch_date,
                                    公告污染場址類型 = o.Situation,
                                    Limit_Date = o.Limit_Date,
                                    take_Date = o.take_Date,
                                    GW_Date = o.GW_Date,
                                    Control_Date = o.Control_Date,
                                    Rem_Date = o.Rem_Date,
                                    Situation_Date = o.Situation_Date,
                                    UsageState = o.UsageState
                                }).Distinct().OrderBy(a => a.案件編號).ToList();

                    allData = ToCacheData(allData);

                    DouHelper.Misc.AddCache(allData, key);
                }
            }

            return allData;
        }

        //清除Cache
        public static void ResetGetAllDatas()
        {
            string key = "OilGas.Rpt_CarFuel_BasicData_Log_List";
            DouHelper.Misc.ClearCache(key);
        }

        //取完DB資料，基本轉換公式
        public static IEnumerable<CarFuel_Log_1> ToCacheData(IEnumerable<CarFuel_Log_1> allData)
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();

            //代碼-兼營設施
            Dou.Models.DB.IModelEntity<CarVehicleGas_Facility> carVehicleGas_Facility = new Dou.Models.DB.ModelEntity<CarVehicleGas_Facility>(dbContext);
            var code_carVehicleGas_Facility = carVehicleGas_Facility.GetAll().OrderBy(a => a.Rank).ToList();
            //代碼-歇業資料
            List<string> code_cbStops = new List<string>() { "60", "61", "62", "66", "68", "18" };
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
                string str = "";
                DateTime? date = DateTime.MinValue;

                data.加油站名稱 = data.加油站名稱 == null ? "" : data.加油站名稱.Trim();
                data.負責人姓名 = data.負責人姓名 == null ? "" : data.負責人姓名.TrimEnd();  //data.負責人姓名.Trim() 舊系統用R
                data.汽車加油站電話 = data.汽車加油站電話 == null ? " " : data.汽車加油站電話.TrimEnd();  //data.負責人姓名.Trim() 舊系統用R  (另:舊" "和Null算2筆)

                //經營許可證號
                str = "";
                string no1 = data.LicenseNo1 == null ? "" : data.LicenseNo1.Trim();
                string no2 = data.LicenseNo2 == null ? "" : data.LicenseNo2.Trim();
                string no3 = data.LicenseNo3 == null ? "" : data.LicenseNo3.Trim();
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

                //歇業日期                        
                if (code_cbStops.Contains(data.DispatchClass))
                {
                    date = data.歇業日期;
                }
                else
                {
                    date = DateTime.MinValue;
                }
                data.歇業日期 = date;

                //設施最後一個字元';',移除
                if (data.附屬設施_項目 == null)
                {
                    data.附屬設施_項目 = "";
                }
                else if (data.附屬設施_項目 != null && data.附屬設施_項目.Length > 0)
                {
                    ////尾
                    //if (data.附屬設施_項目.Right(1) == ";")
                    //{
                    //    int len = data.附屬設施_項目.Length;
                    //    data.附屬設施_項目 = data.附屬設施_項目.Substring(0, len - 1);
                    //}

                    //保留正常的數字
                    List<string> nums = new List<string>();
                    List<string> strs = data.附屬設施_項目.Split(';').ToList();
                    int n;
                    foreach (string v in strs)
                    {
                        if (int.TryParse(v, out n))
                        {
                            nums.Add(v.Trim());
                        }
                    }
                    data.附屬設施_項目 = string.Join(";", nums);
                }

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

                //兼營設施_項目
                if (data.兼營設施_項目 != null)
                {
                    string[] strs = data.兼營設施_項目.Split(';')
                                    .Select(s => s.Trim())
                                    .Where(a => code_carVehicleGas_Facility.Any(b => b.Value == a))  //809 垃圾資料
                                    .OrderBy(x => x).ToArray();
                    data.兼營設施_項目 = string.Join(";", strs);
                }
                else
                {
                    data.兼營設施_項目 = "";
                }

                //加油泵島數
                data.加油泵島數 = data.加油泵島數 == null ? 0 : data.加油泵島數;
                data.單槍 = data.單槍 == null ? 0 : data.單槍;
                data.雙槍 = data.雙槍 == null ? 0 : data.雙槍;
                data.四槍 = data.四槍 == null ? 0 : data.四槍;
                data.六槍 = data.六槍 == null ? 0 : data.六槍;
                data.八槍 = data.八槍 == null ? 0 : data.八槍;
                data.自助加油機數Str = data.自助加油機數 == null || data.自助加油機數 == 0 ? "無" : "有";
                data.self_單槍 = data.self_單槍 == null ? 0 : data.self_單槍;
                data.self_雙槍 = data.self_雙槍 == null ? 0 : data.self_雙槍;
                data.self_四槍 = data.self_四槍 == null ? 0 : data.self_四槍;
                data.self_六槍 = data.self_六槍 == null ? 0 : data.self_六槍;
                data.self_八槍 = data.self_八槍 == null ? 0 : data.self_八槍;
                data.油槽總數 = data.油槽總數 == null ? 0 : data.油槽總數;

                //列管現況(公告污染場址類型公告日期)
                date = DateTime.MinValue;
                if (data.公告污染場址類型 == "依細則第八條限期採取適當措施")
                {
                    date = data.Limit_Date;
                }
                else if (data.公告污染場址類型 == "依七條五採取應變必要措施")
                {
                    date = data.take_Date;
                }
                else if (data.公告污染場址類型 == "劃定地下水受污染限制使用地區及限制事項")
                {
                    date = data.GW_Date;
                }
                else if (data.公告污染場址類型 == "控制場址")
                {
                    date = data.Control_Date;
                }
                else if (data.公告污染場址類型 == "整治場址")
                {
                    date = data.Rem_Date;
                }
                data.公告污染場址類型公告日期 = date;

                //列管現況(公告污染場址類型解列日期)
                date = DateTime.MinValue;
                if (data.公告污染場址類型 == "解除依細則第八條限期採取適當措施" ||
                    data.公告污染場址類型 == "公告解除劃定地下水受污染限制使用地區及限制事項" ||
                    data.公告污染場址類型 == "公告解除控制場址" ||
                    data.公告污染場址類型 == "公告解除整治場址" ||
                    data.公告污染場址類型 == "解除依七條五採取應變必要措施")
                {
                    date = data.Situation_Date;
                }

                data.公告污染場址類型解列日期 = date;
            }

            return allData;
        }

        public class CarFuel_Log_1
        {
            public string 案件編號 { get; set; }
            public string 加油站名稱 { get; set; }
            public string 縣市別 { get; set; }
            public string 鄉鎮市區 { get; set; }
            ////public string 地址 { get; set; }
            public DateTime? 營業日期 { get; set; }
            public DateTime? 收件日期 { get; set; }
            public string 負責人姓名 { get; set; }
            public string 汽車加油站電話 { get; set; }
            public string 經營許可證號 { get; set; }
            public string LicenseNo1 { get; set; }
            public string LicenseNo2 { get; set; }
            public string LicenseNo3 { get; set; }
            public string 汽車加油站地址 { get; set; }
            public string 汽車加油站地號 { get; set; }
            public DateTime? 核發證號日期 { get; set; }
            public string 油品供應商 { get; set; }
            public string SoilServerData { get; set; }
            public string 營業主體 { get; set; }
            public string Business_theme { get; set; }
            public string otherBusiness_theme { get; set; }
            public string 營業別 { get; set; }
            public string 營運狀態 { get; set; }
            public DateTime? 歇業日期 { get; set; }
            public string DispatchClass { get; set; }
            public string 附屬設施_項目 { get; set; }
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
            public string 兼營設施_項目 { get; set; }
            public int? 加油泵島數 { get; set; }
            public int? 單槍 { get; set; }
            public int? 雙槍 { get; set; }
            public int? 四槍 { get; set; }
            public int? 六槍 { get; set; }
            public int? 八槍 { get; set; }
            public string 自助加油機數Str { get; set; }
            public int? 自助加油機數 { get; set; }
            public int? self_單槍 { get; set; }
            public int? self_雙槍 { get; set; }
            public int? self_四槍 { get; set; }
            public int? self_六槍 { get; set; }
            public int? self_八槍 { get; set; }
            public int? 油槽總數 { get; set; }
            public int? 儲槽容量_公秉 { get; set; }
            public int? 儲槽數量_座 { get; set; }
            public string 販售油品種類 { get; set; }
            public DateTime? Mod_date { get; set; }
            public DateTime? Dispatch_date { get; set; }

            public string 公告污染場址類型 { get; set; }
            public DateTime? 公告污染場址類型公告日期 { get; set; }
            public DateTime? 公告污染場址類型解列日期 { get; set; }

            public DateTime? Limit_Date { get; set; }
            public DateTime? take_Date { get; set; }
            public DateTime? GW_Date { get; set; }
            public DateTime? Control_Date { get; set; }
            public DateTime? Rem_Date { get; set; }
            public DateTime? Situation_Date { get; set; }
            public string UsageState { get; set; }
        }
        
    }
}