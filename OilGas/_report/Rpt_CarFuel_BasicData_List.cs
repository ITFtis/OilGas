using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilGas
{
    /// <summary>
    /// 加油站/A統計報表專區/現況資料-基本資料欄位清單
    /// </summary>
    public class Rpt_CarFuel_BasicData_List
    {
        static object lockGetAllDatas = new object();
        public static IEnumerable<CarFuel_1> GetAllDatas(int cachetimer = 0)
        {
            if (cachetimer == 0) cachetimer = Constant.cacheReportTime;

            string key = "OilGas.Rpt_CarFuel_BasicData_List";
            var allData = DouHelper.Misc.GetCache<IEnumerable<CarFuel_1>>(cachetimer, key);
            lock (lockGetAllDatas)
            {
                if (allData == null)
                //if(true)
                {
                    System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
                    Dou.Models.DB.IModelEntity<CarFuel_BasicData> carFuel_BasicData = new Dou.Models.DB.ModelEntity<CarFuel_BasicData>(dbContext);
                    Dou.Models.DB.IModelEntity<CarFuel_OilData> carFuel_OilData = new Dou.Models.DB.ModelEntity<CarFuel_OilData>(dbContext);
                    Dou.Models.DB.IModelEntity<CarFuel_Insurance> carFuel_Insurance = new Dou.Models.DB.ModelEntity<CarFuel_Insurance>(dbContext);
                    Dou.Models.DB.IModelEntity<CarFuel_Dispatch> carFuel_Dispatch = new Dou.Models.DB.ModelEntity<CarFuel_Dispatch>(dbContext);
                    Dou.Models.DB.IModelEntity<UsageStateCode> usageStateCode = new Dou.Models.DB.ModelEntity<UsageStateCode>(dbContext);                
                    Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> carVehicleGas_BusinessOrganization = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_InsuranceCompanyName> carVehicleGas_InsuranceCompanyName = new Dou.Models.DB.ModelEntity<CarVehicleGas_InsuranceCompanyName>(dbContext);
                    Dou.Models.DB.IModelEntity<LandClassCode> landClassCode = new Dou.Models.DB.ModelEntity<LandClassCode>(dbContext);
                    Dou.Models.DB.IModelEntity<LandUsageZoneCode> landUsageZoneCode = new Dou.Models.DB.ModelEntity<LandUsageZoneCode>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_SaleSoilClass> carVehicleGas_SaleSoilClass = new Dou.Models.DB.ModelEntity<CarVehicleGas_SaleSoilClass>(dbContext);
                    Dou.Models.DB.IModelEntity<WS_GSM_Relation> wS_GSM_Relation = new Dou.Models.DB.ModelEntity<WS_GSM_Relation>(dbContext);
                    Dou.Models.DB.IModelEntity<WS_GSM> wS_GSM = new Dou.Models.DB.ModelEntity<WS_GSM>(dbContext);



                    allData = carFuel_BasicData.GetAll().GroupJoin(carFuel_OilData.GetAll(), a => a.CaseNo, b => b.CaseNo, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, 
                                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, c })
                                    .GroupJoin(carFuel_OilData.GetAll(), a => a.CaseNo, b => b.CaseNo, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, 
                                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, c })
                                    .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new {
                                        o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, 
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, c.Tank_type_tank, c.Tank_type_tank_seat, c.SaleSoilClass
                                    })
                                    .GroupJoin(carFuel_Insurance.GetAll(), a => a.CaseNo, b => b.CaseNo, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, 
                                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, c })
                                    .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new {                                    
                                        o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date,
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass,
                                        c.Insurance_Company, c.Insurance_otherCompany, c.Insurance_No, c.Insurance_policy_start, c.Insurance_policy_end, c.Insurance_Type
                                    })
                                    .GroupJoin(carFuel_Dispatch.GetAll(), a => a.CaseNo, b => b.CaseNo, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type, 
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, c })
                                    .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new {
                                        o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type, 
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, c.DispatchClass, c.Dispatch_date
                                    })
                                    .GroupJoin(usageStateCode.GetAll(), a => a.UsageState, b => b.Value, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type, o.DispatchClass, o.Dispatch_date, 
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, c.FirstOrDefault().Type, c.FirstOrDefault().ShortName })
                                    .GroupJoin(carVehicleGas_BusinessOrganization.GetAll(), a => a.Business_theme, b => b.Value, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.Type, o.ShortName, o.DispatchClass, o.Dispatch_date, 
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, bus_name = c.FirstOrDefault().Name })
                                    .GroupJoin(carVehicleGas_InsuranceCompanyName.GetAll(), a => a.Insurance_Company, b => b.Value, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.Type, o.ShortName, o.DispatchClass, o.Dispatch_date, o.bus_name, 
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, CompanyName = c.FirstOrDefault().Name })
                                    .GroupJoin(landClassCode.GetAll(), a => a.LandClass, b => b.Value, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.Type, o.ShortName, o.DispatchClass, o.Dispatch_date, o.bus_name, o.CompanyName, 
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, LandClassName = c.FirstOrDefault().Name })
                                    .GroupJoin(landUsageZoneCode.GetAll(), a => a.LandUsageZone, b => b.Value, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.Type, o.ShortName, o.DispatchClass, o.Dispatch_date, o.bus_name, o.CompanyName, o.LandClassName, 
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, LandUsageName = c.FirstOrDefault().Name })                                   
                                    .GroupJoin(carVehicleGas_SaleSoilClass.GetAll(), a => a.SaleSoilClass, b => b.Value, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.Type, o.ShortName, o.DispatchClass, o.Dispatch_date, o.bus_name, o.CompanyName, o.LandClassName, 
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, o.LandUsageName, SaleSoilName = c.FirstOrDefault().Name })
                                    .GroupJoin(wS_GSM_Relation.GetAll(), a => a.CaseNo, b => b.CaseNo, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.Type, o.ShortName, o.DispatchClass, o.Dispatch_date, o.bus_name, o.CompanyName, o.LandClassName, 
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, o.LandUsageName, o.SaleSoilName, c })
                                    .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new
                                    {
                                        o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.Type, o.ShortName, o.DispatchClass, o.Dispatch_date, o.bus_name, o.CompanyName, o.LandClassName, 
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, o.LandUsageName, o.SaleSoilName, c.FacNo
                                    })
                                    .GroupJoin(wS_GSM.GetAll(), a => a.FacNo, b => b.gsm_id, (o, c) => new { o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.Type, o.ShortName, o.DispatchClass, o.Dispatch_date, o.bus_name, o.CompanyName, o.LandClassName, 
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, o.LandUsageName, o.SaleSoilName, c })
                                    .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new { 
                                        o.CaseNo, o.Gas_Name, o.UsageState, o.ZipCode, o.Address, o.OperationDate, o.Recipient_date, o.Boss, o.TelNo, o.LicenseNo1, o.LicenseNo2, o.LicenseNo3, o.AddressNo, o.Report_date, o.SoilServerData, o.Business_theme, o.otherBusiness_theme, o.Insurance_Company, o.Insurance_otherCompany, o.Insurance_No, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type, o.AncillaryFacility, o.LandPriority, o.Land_acreage, o.LandClass, o.OtherLandClass, o.LandUsageZone, o.OtherLandUsageZone, o.Facility, o.Island, o.Mod_date, o.Type, o.ShortName, o.DispatchClass, o.Dispatch_date, o.bus_name, o.CompanyName, o.LandClassName, 
                                        o.one_gun, o.two_gun, o.four_gun, o.six_gun, o.eight_gun, o.Self_total_gun, o.Self_one_gun, o.Self_two_gun, o.Self_four_gun, o.Self_six_gun, o.Self_eight_gun, o.Tank, o.Tank_type_tank, o.Tank_type_tank_seat, o.SaleSoilClass, o.LandUsageName, o.SaleSoilName, 
                                        c.Situation, c.Limit_Date, c.take_Date, c.GW_Date, c.Control_Date, c.Rem_Date, c.Situation_Date
                                    })
                                    .Select(o => new CarFuel_1()
                                    {
                                        案件編號 = o.CaseNo,
                                        加油站名稱 = o.Gas_Name,
                                        鄉鎮市區 = o.ZipCode,
                                        地址 = o.Address,
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
                                    });

                    allData = allData.Distinct().OrderBy(a => a.案件編號).ToList();

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
                        var c = citys.Where(a => a.GSLCode == data.案件編號.Substring(4, 2)).FirstOrDefault();
                        data.縣市別 = c == null ? "" : c.CityName;

                        string str = "";
                        DateTime? date = DateTime.MinValue;

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
                                str = data.otherBusiness_theme;
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

                    DouHelper.Misc.AddCache(allData, key);
                }
            }

            return allData;
        }

        public static void ResetGetAllDatas()
        {
            string key = "OilGas.Rpt_CarFuel_BasicData_List";
            DouHelper.Misc.ClearCache(key);
        }

        public class CarFuel_1
        {
            public string 案件編號 { get; set; }
            public string 加油站名稱 { get; set; }
            public string 縣市別 { get; set; }
            public string 鄉鎮市區 { get; set; }
            public string 地址 { get; set; }
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