using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using static OilGas.Rpt_CarFuel_BasicData_Log_List;

namespace OilGas
{
    /// <summary>
    /// 自用加儲油/D統計報表專區/基本資料清單查詢
    /// </summary>
    public class Rpt_SelfFuel_BasicData
    {
        static object lockGetAllDatas = new object();
        static object lockGetLogAllDatas = new object();

        //1.進行中資料
        public static IEnumerable<SelfFuel_1> GetAllDatas(int cachetimer = 0)
        {
            if (cachetimer == 0) cachetimer = Constant.cacheReportTime;

            string key = "OilGas.Rpt_SelfFuel_BasicData";
            var allData = DouHelper.Misc.GetCache<IEnumerable<SelfFuel_1>>(cachetimer, key);
            lock (lockGetAllDatas)
            {
                if (allData == null)
                //if(true)
                {
                    System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
                    Dou.Models.DB.IModelEntity<SelfFuel_Basic> selfFuel_Basic = new Dou.Models.DB.ModelEntity<SelfFuel_Basic>(dbContext);                    
                    Dou.Models.DB.IModelEntity<SelfFuel_Land> selfFuel_Land = new Dou.Models.DB.ModelEntity<SelfFuel_Land>(dbContext);
                    Dou.Models.DB.IModelEntity<SelfFuel_Facility> selfFuel_Facility = new Dou.Models.DB.ModelEntity<SelfFuel_Facility>(dbContext);
                    Dou.Models.DB.IModelEntity<UsageState> usageState = new Dou.Models.DB.ModelEntity<UsageState>(dbContext);
                    Dou.Models.DB.IModelEntity<UsageState_Second> usageState_Second = new Dou.Models.DB.ModelEntity<UsageState_Second>(dbContext);
                    Dou.Models.DB.IModelEntity<UsageState_Third> usageState_Third = new Dou.Models.DB.ModelEntity<UsageState_Third>(dbContext);
                    Dou.Models.DB.IModelEntity<UsageState_Fourth> usageState_Fourth = new Dou.Models.DB.ModelEntity<UsageState_Fourth>(dbContext);

                    Dou.Models.DB.IModelEntity<Facility> facility = new Dou.Models.DB.ModelEntity<Facility>(dbContext);
                    Dou.Models.DB.IModelEntity<Facility_Detail> facility_Detail = new Dou.Models.DB.ModelEntity<Facility_Detail>(dbContext);                    
                    Dou.Models.DB.IModelEntity<SelfFuel_Insurance> selfFuel_Insurance = new Dou.Models.DB.ModelEntity<SelfFuel_Insurance>(dbContext);                    
                    Dou.Models.DB.IModelEntity<CarVehicleGas_InsuranceCompanyName> carVehicleGas_InsuranceCompanyName = new Dou.Models.DB.ModelEntity<CarVehicleGas_InsuranceCompanyName>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_LandPriority> carVehicleGas_LandPriority = new Dou.Models.DB.ModelEntity<CarVehicleGas_LandPriority>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_LandClass> carVehicleGas_LandClass = new Dou.Models.DB.ModelEntity<CarVehicleGas_LandClass>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_LandUsageZone> carVehicleGas_LandUsageZone = new Dou.Models.DB.ModelEntity<CarVehicleGas_LandUsageZone>(dbContext);
                    Dou.Models.DB.IModelEntity<FacilityType> facilityType = new Dou.Models.DB.ModelEntity<FacilityType>(dbContext);                   
                    Dou.Models.DB.IModelEntity<SelfFuel_Oil> selfFuel_Oil = new Dou.Models.DB.ModelEntity<SelfFuel_Oil>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_SaleSoilClass> carVehicleGas_SaleSoilClass = new Dou.Models.DB.ModelEntity<CarVehicleGas_SaleSoilClass>(dbContext);



                    allData = selfFuel_Basic.GetAll().GroupJoin(selfFuel_Land.GetAll(), 
                                            a => new { a.CaseNo, a.Change }, 
                                            b => new { b.CaseNo, b.Change }, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, c })
                                    .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new
                                    {
                                        o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo,
                                        c.LandPriority, c.LandTotalSquare, c.LandClass, c.LandUsageZone
                                    })
                                    .GroupJoin(selfFuel_Facility.GetAll(), 
                                            a => new { a.CaseNo, a.Change }, 
                                            b => new { b.CaseNo, b.Change }, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                                Fuel_FacilityType = c.FirstOrDefault().FacilityType, c.FirstOrDefault().Tank, c.FirstOrDefault().SinglePump, c.FirstOrDefault().DualPump, c.FirstOrDefault().FourPump, c.FirstOrDefault().SixPump, c.FirstOrDefault().EightPump, c.FirstOrDefault().TotalPump })
                                    .GroupJoin(usageState.GetAll(), a => a.UsageState, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, UsageStateName = c.FirstOrDefault().Name })
                                    .GroupJoin(usageState_Second.GetAll(), a => a.UsageState_Second, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, UsageState_SecondName = c.FirstOrDefault().Name })
                                    .GroupJoin(usageState_Third.GetAll(), a => a.UsageState_Third, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, UsageState_ThirdName = c.FirstOrDefault().Name })
                                    .GroupJoin(usageState_Fourth.GetAll(), a => a.UsageState_Fourth, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, UsageState_FourthName = c.FirstOrDefault().Name })
                                    .GroupJoin(facility.GetAll(), a => a.Facility, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, FacilityName = c.FirstOrDefault().Name })
                                    .GroupJoin(facility_Detail.GetAll(), a => a.FacilityDetail, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, facilitySecondName = c.FirstOrDefault().Name })
                                    .GroupJoin(selfFuel_Insurance.GetAll(), 
                                        a => new { a.CaseNo, a.Change },
                                        b => new { b.CaseNo, b.Change }, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                            o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, o.facilitySecondName, c })
                                    .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new {                                    
                                        o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, o.facilitySecondName,
                                        c.InsuranceCompanyName, c.InsuranceNo, c.OtherInsuranceCompanyName, c.InsuranceValidateStartDate, c.InsuranceValidateEndDate, c.InsuranceType
                                    })
                                    .GroupJoin(carVehicleGas_InsuranceCompanyName.GetAll(), a => a.InsuranceCompanyName, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, o.facilitySecondName,
                                        o.InsuranceNo, o.OtherInsuranceCompanyName, o.InsuranceValidateStartDate, o.InsuranceValidateEndDate, o.InsuranceType, InsuranceCompanyName = c.FirstOrDefault().Name })
                                    .GroupJoin(carVehicleGas_LandPriority.GetAll(), a => a.LandPriority, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, o.facilitySecondName,
                                        o.InsuranceNo, o.OtherInsuranceCompanyName, o.InsuranceValidateStartDate, o.InsuranceValidateEndDate, o.InsuranceType, o.InsuranceCompanyName, LandPriorityName = c.FirstOrDefault().Name })
                                    .GroupJoin(carVehicleGas_LandClass.GetAll(), a => a.LandClass, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, o.facilitySecondName,
                                        o.InsuranceNo, o.OtherInsuranceCompanyName, o.InsuranceValidateStartDate, o.InsuranceValidateEndDate, o.InsuranceType, o.InsuranceCompanyName, o.LandPriorityName, LandClassName = c.FirstOrDefault().Name })
                                    .GroupJoin(carVehicleGas_LandUsageZone.GetAll(), a => a.LandUsageZone, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, o.facilitySecondName,
                                        o.InsuranceNo, o.OtherInsuranceCompanyName, o.InsuranceValidateStartDate, o.InsuranceValidateEndDate, o.InsuranceType, o.InsuranceCompanyName, o.LandPriorityName, o.LandClassName, LandUsageZoneName = c.FirstOrDefault().Name })
                                    .GroupJoin(facilityType.GetAll(), a => a.Fuel_FacilityType, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, o.facilitySecondName,
                                        o.InsuranceNo, o.OtherInsuranceCompanyName, o.InsuranceValidateStartDate, o.InsuranceValidateEndDate, o.InsuranceType, o.InsuranceCompanyName, o.LandPriorityName, o.LandClassName, o.LandUsageZoneName, FacilityTypeName = c.FirstOrDefault().Name })                                    
                                    .GroupJoin(selfFuel_Oil.GetAll(),
                                            a => new { a.CaseNo, a.Change },
                                            b => new { b.CaseNo, b.Change }, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, o.facilitySecondName,
                                        o.InsuranceNo, o.OtherInsuranceCompanyName, o.InsuranceValidateStartDate, o.InsuranceValidateEndDate, o.InsuranceType, o.InsuranceCompanyName, o.LandPriorityName, o.LandClassName, o.LandUsageZoneName, o.FacilityTypeName, c })
                                    .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new
                                    {
                                        o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, o.facilitySecondName,
                                        o.InsuranceNo, o.OtherInsuranceCompanyName, o.InsuranceValidateStartDate, o.InsuranceValidateEndDate, o.InsuranceType, o.InsuranceCompanyName, o.LandPriorityName, o.LandClassName, o.LandUsageZoneName, o.FacilityTypeName, c.SoilClass, c.TroughCapacity, c.Ground, c.UnderGround 
                                    })
                                    .GroupJoin(carVehicleGas_SaleSoilClass.GetAll(), a => a.SoilClass, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, o.facilitySecondName,
                                        o.InsuranceNo, o.OtherInsuranceCompanyName, o.InsuranceValidateStartDate, o.InsuranceValidateEndDate, o.InsuranceType, o.InsuranceCompanyName, o.LandPriorityName, o.LandClassName, o.LandUsageZoneName, o.FacilityTypeName, o.SoilClass, o.TroughCapacity, o.Ground, o.UnderGround, SoilClassName = c.FirstOrDefault().Name })
                                    .Select(o => new SelfFuel_1()
                                    {                                        
                                        IsConfirm = o.IsConfirm,
                                        案件編號 = o.CaseNo,
                                        設施名稱 = o.FuelName,
                                        變更次數 = o.Change,
                                        營業主體 = o.BusiOrg,
                                        使用狀況第一層 = o.UsageStateName,
                                        使用狀況第二層 = o.UsageState_SecondName,
                                        使用狀況第三層 = o.UsageState_ThirdName,
                                        使用狀況第四層 = o.UsageState_FourthName,
                                        UsageState = o.UsageState,
                                        UsageState_Second = o.UsageState_Second,
                                        UsageState_Third = o.UsageState_Third,
                                        UsageState_Fourth = o.UsageState_Fourth,
                                        收件日期 = o.ExpiredDate,
                                        核准使用日期 = o.StartDate,
                                        結束使用日期 = o.EndDate,                                        
                                        特許執照號碼 = o.LicenseNo,
                                        設置場所第一層 = o.FacilityName,
                                        設置場所第二層 = o.facilitySecondName,
                                        設置場所其他 = o.FacilityOther,
                                        設置場所基地 = o.FacilityBase,                                        
                                        負責人姓名 = o.Responsor,
                                        設施電話 = o.FacilityPhone,
                                        設施地址 = o.AreaNo + o.Address,
                                        設施地號 = o.AddressNo,                                        
                                        保單號碼 = o.InsuranceNo,
                                        保險公司名稱 = o.InsuranceCompanyName,
                                        保險公司名稱_其他 = o.OtherInsuranceCompanyName,
                                        保單有效期間_起 = o.InsuranceValidateStartDate,
                                        保單有效期間_迄 = o.InsuranceValidateEndDate,
                                        保險類型 = o.InsuranceType,
                                        土地權屬 = o.LandPriorityName,                                        
                                        用地總面積 = o.LandTotalSquare,
                                        用地類別 = o.LandClassName,
                                        土地使用分區 = o.LandUsageZoneName,
                                        設施狀況之設施類型 = o.FacilityTypeName,
                                        設施狀況之油槽總數 = o.Tank,
                                        加油槍數_單槍 = o.SinglePump,
                                        加油槍數_雙槍 = o.DualPump,
                                        加油槍數_四槍 = o.FourPump,
                                        加油槍數_六槍 = o.SixPump,
                                        加油槍數_八槍 = o.EightPump,
                                        加油槍數_總計 = o.TotalPump,
                                        油品種類 = o.SoilClassName, 
                                        儲槽容量_油罐車載油容量 = o.TroughCapacity, 
                                        儲槽位置數量_地上 = o.Ground, 
                                        儲槽位置數量_地下 = o.UnderGround,
                                        ModifyTime = o.ModifyTime,
                                        AreaNo = o.AreaNo
                                    })
                                    //.Where(a => a.案件編號 == "E091110002") //////////////////
                                    .Where(a => a.案件編號 != "");
                                

                    var code1 = RptCode.GetAnyUsageStateM();

                    //字串測試
                    ////var a1 = allData.ToList();
                    ////List<string> allys = new List<string>();
                    ////foreach(var ss in code1)
                    ////{
                    ////    allys.Add(string.Format("{0}/{1}/{2}/{3}/", ss.UsageState, ss.UsageState_Second, ss.UsageState_Third, ss.UsageState_Fourth));
                    ////}
                    ////string aaa = "!23";

                    //使用狀況篩選
                    if (code1.Count > 0)
                    {
                        allData = allData.Where(a => code1.Any(b => b.UsageState == a.UsageState
                                                            && b.UsageState_Second == a.UsageState_Second
                                                            && b.UsageState_Third == a.UsageState_Third
                                                            && b.UsageState_Fourth == a.UsageState_Fourth));
                    }

                    allData = allData.Distinct().OrderBy(a => a.案件編號).ToList();

                    allData = ToCacheData(allData);

                    DouHelper.Misc.AddCache(allData, key);
                }
            }

            return allData;
        }

        //清除(進行中)
        public static void ResetGetAllDatas()
        {
            string key = "OilGas.Rpt_SelfFuel_BasicData";
            DouHelper.Misc.ClearCache(key);
        }

        //2.Log資料
        public static IEnumerable<SelfFuel_1> GetLogAllDatas(int cachetimer = 0)
        {
            if (cachetimer == 0) cachetimer = Constant.cacheReportTime;

            string key = "OilGas.Rpt_SelfFuel_Log_BasicData";
            var allData = DouHelper.Misc.GetCache<IEnumerable<SelfFuel_1>>(cachetimer, key);
            lock (lockGetLogAllDatas)
            {
                if (allData == null)
                //if(true)
                {
                    System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
                    Dou.Models.DB.IModelEntity<SelfFuel_Basic_Log> selfFuel_Basic_Log = new Dou.Models.DB.ModelEntity<SelfFuel_Basic_Log>(dbContext);
                    Dou.Models.DB.IModelEntity<SelfFuel_Land_Log> selfFuel_Land_Log = new Dou.Models.DB.ModelEntity<SelfFuel_Land_Log>(dbContext);
                    Dou.Models.DB.IModelEntity<SelfFuel_Facility_Log> selfFuel_Facility_Log = new Dou.Models.DB.ModelEntity<SelfFuel_Facility_Log>(dbContext);
                    Dou.Models.DB.IModelEntity<UsageState> usageState = new Dou.Models.DB.ModelEntity<UsageState>(dbContext);
                    Dou.Models.DB.IModelEntity<UsageState_Second> usageState_Second = new Dou.Models.DB.ModelEntity<UsageState_Second>(dbContext);
                    Dou.Models.DB.IModelEntity<UsageState_Third> usageState_Third = new Dou.Models.DB.ModelEntity<UsageState_Third>(dbContext);
                    Dou.Models.DB.IModelEntity<UsageState_Fourth> usageState_Fourth = new Dou.Models.DB.ModelEntity<UsageState_Fourth>(dbContext);

                    Dou.Models.DB.IModelEntity<Facility> facility = new Dou.Models.DB.ModelEntity<Facility>(dbContext);
                    Dou.Models.DB.IModelEntity<Facility_Detail> facility_Detail = new Dou.Models.DB.ModelEntity<Facility_Detail>(dbContext);
                    Dou.Models.DB.IModelEntity<SelfFuel_Insurance_Log> selfFuel_Insurance_Log = new Dou.Models.DB.ModelEntity<SelfFuel_Insurance_Log>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_InsuranceCompanyName> carVehicleGas_InsuranceCompanyName = new Dou.Models.DB.ModelEntity<CarVehicleGas_InsuranceCompanyName>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_LandPriority> carVehicleGas_LandPriority = new Dou.Models.DB.ModelEntity<CarVehicleGas_LandPriority>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_LandClass> carVehicleGas_LandClass = new Dou.Models.DB.ModelEntity<CarVehicleGas_LandClass>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_LandUsageZone> carVehicleGas_LandUsageZone = new Dou.Models.DB.ModelEntity<CarVehicleGas_LandUsageZone>(dbContext);
                    Dou.Models.DB.IModelEntity<FacilityType> facilityType = new Dou.Models.DB.ModelEntity<FacilityType>(dbContext);
                    Dou.Models.DB.IModelEntity<SelfFuel_Oil_Log> selfFuel_Oil_Log = new Dou.Models.DB.ModelEntity<SelfFuel_Oil_Log>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_SaleSoilClass> carVehicleGas_SaleSoilClass = new Dou.Models.DB.ModelEntity<CarVehicleGas_SaleSoilClass>(dbContext);


                    allData = selfFuel_Basic_Log.GetAll().GroupJoin(selfFuel_Land_Log.GetAll(), 
                                            a => new { a.CaseNo, a.Change }, 
                                            b => new { b.CaseNo, b.Change }, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, c })
                                    .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new
                                    {
                                        o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo,
                                        c.LandPriority, c.LandTotalSquare, c.LandClass, c.LandUsageZone
                                    })
                                    .GroupJoin(selfFuel_Facility_Log.GetAll(), 
                                            a => new { a.CaseNo, a.Change }, 
                                            b => new { b.CaseNo, b.Change }, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                                Fuel_FacilityType = c.FirstOrDefault().FacilityType, c.FirstOrDefault().Tank, c.FirstOrDefault().SinglePump, c.FirstOrDefault().DualPump, c.FirstOrDefault().FourPump, c.FirstOrDefault().SixPump, c.FirstOrDefault().EightPump, c.FirstOrDefault().TotalPump })
                                    .GroupJoin(usageState.GetAll(), a => a.UsageState, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, UsageStateName = c.FirstOrDefault().Name })
                                    .GroupJoin(usageState_Second.GetAll(), a => a.UsageState_Second, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, UsageState_SecondName = c.FirstOrDefault().Name })
                                    .GroupJoin(usageState_Third.GetAll(), a => a.UsageState_Third, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, UsageState_ThirdName = c.FirstOrDefault().Name })
                                    .GroupJoin(usageState_Fourth.GetAll(), a => a.UsageState_Fourth, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, UsageState_FourthName = c.FirstOrDefault().Name })
                                    .GroupJoin(facility.GetAll(), a => a.Facility, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, FacilityName = c.FirstOrDefault().Name })
                                    .GroupJoin(facility_Detail.GetAll(), a => a.FacilityDetail, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, facilitySecondName = c.FirstOrDefault().Name })
                                    .GroupJoin(selfFuel_Insurance_Log.GetAll(), 
                                        a => new { a.CaseNo, a.Change },
                                        b => new { b.CaseNo, b.Change }, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                            o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, o.facilitySecondName, c })
                                    .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new {                                    
                                        o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, o.facilitySecondName,
                                        c.InsuranceCompanyName, c.InsuranceNo, c.OtherInsuranceCompanyName, c.InsuranceValidateStartDate, c.InsuranceValidateEndDate, c.InsuranceType
                                    })
                                    .GroupJoin(carVehicleGas_InsuranceCompanyName.GetAll(), a => a.InsuranceCompanyName, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, o.facilitySecondName,
                                        o.InsuranceNo, o.OtherInsuranceCompanyName, o.InsuranceValidateStartDate, o.InsuranceValidateEndDate, o.InsuranceType, InsuranceCompanyName = c.FirstOrDefault().Name })
                                    .GroupJoin(carVehicleGas_LandPriority.GetAll(), a => a.LandPriority, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, o.facilitySecondName,
                                        o.InsuranceNo, o.OtherInsuranceCompanyName, o.InsuranceValidateStartDate, o.InsuranceValidateEndDate, o.InsuranceType, o.InsuranceCompanyName, LandPriorityName = c.FirstOrDefault().Name })
                                    .GroupJoin(carVehicleGas_LandClass.GetAll(), a => a.LandClass, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, o.facilitySecondName,
                                        o.InsuranceNo, o.OtherInsuranceCompanyName, o.InsuranceValidateStartDate, o.InsuranceValidateEndDate, o.InsuranceType, o.InsuranceCompanyName, o.LandPriorityName, LandClassName = c.FirstOrDefault().Name })
                                    .GroupJoin(carVehicleGas_LandUsageZone.GetAll(), a => a.LandUsageZone, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, o.facilitySecondName,
                                        o.InsuranceNo, o.OtherInsuranceCompanyName, o.InsuranceValidateStartDate, o.InsuranceValidateEndDate, o.InsuranceType, o.InsuranceCompanyName, o.LandPriorityName, o.LandClassName, LandUsageZoneName = c.FirstOrDefault().Name })
                                    .GroupJoin(facilityType.GetAll(), a => a.Fuel_FacilityType, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, o.facilitySecondName,
                                        o.InsuranceNo, o.OtherInsuranceCompanyName, o.InsuranceValidateStartDate, o.InsuranceValidateEndDate, o.InsuranceType, o.InsuranceCompanyName, o.LandPriorityName, o.LandClassName, o.LandUsageZoneName, FacilityTypeName = c.FirstOrDefault().Name })                                    
                                    .GroupJoin(selfFuel_Oil_Log.GetAll(),
                                            a => new { a.CaseNo, a.Change },
                                            b => new { b.CaseNo, b.Change }, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, o.facilitySecondName,
                                        o.InsuranceNo, o.OtherInsuranceCompanyName, o.InsuranceValidateStartDate, o.InsuranceValidateEndDate, o.InsuranceType, o.InsuranceCompanyName, o.LandPriorityName, o.LandClassName, o.LandUsageZoneName, o.FacilityTypeName, c })
                                    .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new
                                    {
                                        o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, o.facilitySecondName,
                                        o.InsuranceNo, o.OtherInsuranceCompanyName, o.InsuranceValidateStartDate, o.InsuranceValidateEndDate, o.InsuranceType, o.InsuranceCompanyName, o.LandPriorityName, o.LandClassName, o.LandUsageZoneName, o.FacilityTypeName, c.SoilClass, c.TroughCapacity, c.Ground, c.UnderGround 
                                    })
                                    .GroupJoin(carVehicleGas_SaleSoilClass.GetAll(), a => a.SoilClass, b => b.Value, (o, c) => new { o.IsConfirm, o.CaseNo, o.FuelName, o.Change, o.ModifyTime, o.BusiOrg, o.UsageState, o.UsageState_Second, o.UsageState_Third, o.UsageState_Fourth, o.ExpiredDate, o.StartDate, o.EndDate, o.LicenseNo, o.Facility, o.FacilityDetail, o.FacilityOther, o.FacilityBase, o.Responsor, o.FacilityPhone, o.AreaNo, o.Address, o.AddressNo, o.LandPriority, o.LandTotalSquare, o.LandClass, o.LandUsageZone, 
                                        o.Fuel_FacilityType, o.Tank, o.SinglePump, o.DualPump, o.FourPump, o.SixPump, o.EightPump, o.TotalPump, o.UsageStateName, o.UsageState_SecondName, o.UsageState_ThirdName, o.UsageState_FourthName, o.FacilityName, o.facilitySecondName,
                                        o.InsuranceNo, o.OtherInsuranceCompanyName, o.InsuranceValidateStartDate, o.InsuranceValidateEndDate, o.InsuranceType, o.InsuranceCompanyName, o.LandPriorityName, o.LandClassName, o.LandUsageZoneName, o.FacilityTypeName, o.SoilClass, o.TroughCapacity, o.Ground, o.UnderGround, SoilClassName = c.FirstOrDefault().Name })
                                    .Select(o => new SelfFuel_1()
                                    {                                        
                                        IsConfirm = o.IsConfirm,
                                        案件編號 = o.CaseNo,
                                        設施名稱 = o.FuelName,
                                        變更次數 = o.Change,
                                        營業主體 = o.BusiOrg,
                                        使用狀況第一層 = o.UsageStateName,
                                        使用狀況第二層 = o.UsageState_SecondName,
                                        使用狀況第三層 = o.UsageState_ThirdName,
                                        使用狀況第四層 = o.UsageState_FourthName,
                                        UsageState = o.UsageState,
                                        UsageState_Second = o.UsageState_Second,
                                        UsageState_Third = o.UsageState_Third,
                                        UsageState_Fourth = o.UsageState_Fourth,
                                        收件日期 = o.ExpiredDate,
                                        核准使用日期 = o.StartDate,
                                        結束使用日期 = o.EndDate,                                        
                                        特許執照號碼 = o.LicenseNo,
                                        設置場所第一層 = o.FacilityName,
                                        設置場所第二層 = o.facilitySecondName,
                                        設置場所其他 = o.FacilityOther,
                                        設置場所基地 = o.FacilityBase,                                        
                                        負責人姓名 = o.Responsor,
                                        設施電話 = o.FacilityPhone,
                                        設施地址 = o.AreaNo + o.Address,
                                        設施地號 = o.AddressNo,                                        
                                        保單號碼 = o.InsuranceNo,
                                        保險公司名稱 = o.InsuranceCompanyName,
                                        保險公司名稱_其他 = o.OtherInsuranceCompanyName,
                                        保單有效期間_起 = o.InsuranceValidateStartDate,
                                        保單有效期間_迄 = o.InsuranceValidateEndDate,
                                        保險類型 = o.InsuranceType,
                                        土地權屬 = o.LandPriorityName,                                        
                                        用地總面積 = o.LandTotalSquare,
                                        用地類別 = o.LandClassName,
                                        土地使用分區 = o.LandUsageZoneName,
                                        設施狀況之設施類型 = o.FacilityTypeName,
                                        設施狀況之油槽總數 = o.Tank,
                                        加油槍數_單槍 = o.SinglePump,
                                        加油槍數_雙槍 = o.DualPump,
                                        加油槍數_四槍 = o.FourPump,
                                        加油槍數_六槍 = o.SixPump,
                                        加油槍數_八槍 = o.EightPump,
                                        加油槍數_總計 = o.TotalPump,
                                        油品種類 = o.SoilClassName, 
                                        儲槽容量_油罐車載油容量 = o.TroughCapacity, 
                                        儲槽位置數量_地上 = o.Ground, 
                                        儲槽位置數量_地下 = o.UnderGround,
                                        ModifyTime = o.ModifyTime,
                                        AreaNo = o.AreaNo
                                    })
                                    //.Where(a => a.案件編號 == "E091110002") //////////////////
                                    .Where(a => a.案件編號 != "");
                    


                    var code1 = RptCode.GetAnyUsageStateM();

                    //字串測試
                    ////var a1 = allData.ToList();
                    ////List<string> allys = new List<string>();
                    ////foreach(var ss in code1)
                    ////{
                    ////    allys.Add(string.Format("{0}/{1}/{2}/{3}/", ss.UsageState, ss.UsageState_Second, ss.UsageState_Third, ss.UsageState_Fourth));
                    ////}
                    ////string aaa = "!23";

                    //使用狀況篩選
                    if (code1.Count > 0)
                    {
                        allData = allData.Where(a => code1.Any(b => b.UsageState == a.UsageState
                                                            && b.UsageState_Second == a.UsageState_Second
                                                            && b.UsageState_Third == a.UsageState_Third
                                                            && b.UsageState_Fourth == a.UsageState_Fourth));
                    }

                    allData = allData.Distinct().OrderBy(a => a.案件編號).ToList();

                    allData = ToCacheData(allData);

                    DouHelper.Misc.AddCache(allData, key);
                }
            }

            return allData;
        }

        //清除(Log)
        public static void ResetGetLogAllDatas()
        {
            string key = "OilGas.Rpt_SelfFuel_Log_BasicData";
            DouHelper.Misc.ClearCache(key);
        }

        //取完DB資料，基本轉換公式
        public static IEnumerable<SelfFuel_1> ToCacheData(IEnumerable<SelfFuel_1> allData)
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();

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

                //////經營許可證號
                ////str = "";
                ////string no1 = data.LicenseNo1 == null ? "" : data.LicenseNo1.Trim();
                ////string no2 = data.LicenseNo2 == null ? "" : data.LicenseNo2.Trim();
                ////string no3 = data.LicenseNo3 == null ? "" : data.LicenseNo3.Trim();
                ////if (!string.IsNullOrEmpty(no2))
                ////    str = no1 + "字第" + no2 + "之" + no3 + "號";
                ////else
                ////    str = no1 + no2 + no3;
                ////data.經營許可證號 = str;

                ////if (data.油品供應商 != null)
                ////{
                ////    data.油品供應商 = Code.GetSoilServerName(data.油品供應商);
                ////}
                ////else
                ////{
                ////    data.油品供應商 = Code.GetSoilServerName("");
                ////}

                //////營業主體
                ////str = data.營業主體;
                ////if (data.Business_theme != null)
                ////{
                ////    if (data.Business_theme == "16")
                ////        str = data.otherBusiness_theme;
                ////}
                ////data.營業主體 = str;

                //////歇業日期                        
                ////if (code_cbStops.Contains(data.DispatchClass))
                ////{
                ////    date = data.歇業日期;
                ////}
                ////else
                ////{
                ////    date = DateTime.MinValue;
                ////}
                ////data.歇業日期 = date;

                //////保險公司名稱
                ////data.保險公司名稱 = data.保險公司名稱 == "其他" ? data.Insurance_otherCompany : data.保險公司名稱;

                //////保單有效期限
                ////if (data.保單有效期限_起 != null && data.保單有效期限_迄 != null)
                ////{
                ////    data.保單有效期限 = DateFormat.ToDate4((DateTime)data.保單有效期限_起) + "~" + DateFormat.ToDate4((DateTime)data.保單有效期限_迄);
                ////}
                //保險類型
                if (data.保險類型 != null)
                {
                    data.保險類型 = data.保險類型.Replace("0", "公共意外責任保險").Replace("1", "意外污染責任保險");
                }
                //////土地權屬
                ////str = "";
                ////if (data.土地權屬 == "1")
                ////{
                ////    str = "自用";
                ////}
                ////else if (data.土地權屬 == "2")
                ////{
                ////    str = "租用";
                ////}
                ////else if (data.土地權屬 == "3")
                ////{
                ////    str = "自用及租用";
                ////}
                ////data.土地權屬 = str;

                //////土地使用分區
                //////case cb.LandUsageZone when '99' then cb.OtherLandUsageZone when '11' then luc.Name+':'+lc.Name else luc.Name End  [土地使用分區]
                ////str = "";
                ////if (data.LandUsageZone == "99")
                ////{
                ////    str = data.OtherLandUsageZone;
                ////}
                ////else if (data.LandUsageZone == "11")
                ////{
                ////    str = data.土地使用分區 + ':' + data.用地類別;
                ////}
                ////else
                ////{
                ////    str = data.土地使用分區;
                ////}
                ////data.土地使用分區 = str;

                //////用地類別
                //////case when cb.LandClass in ('98','99') then cb.OtherLandClass when cb.LandUsageZone = '11' then '' else lc.Name End  [用地類別]
                ////str = "";
                ////if (data.LandClass == "98" || data.LandClass == "99")
                ////{
                ////    str = data.OtherLandClass;
                ////}
                ////else if (data.LandUsageZone == "11")
                ////{
                ////    str = "";
                ////}
                ////else
                ////{
                ////    str = data.用地類別;
                ////}
                ////data.用地類別 = str;

                //////加油泵島數
                ////data.加油泵島數 = data.加油泵島數 == null ? 0 : data.加油泵島數;
                ////data.單槍 = data.單槍 == null ? 0 : data.單槍;
                ////data.雙槍 = data.雙槍 == null ? 0 : data.雙槍;
                ////data.四槍 = data.四槍 == null ? 0 : data.四槍;
                ////data.六槍 = data.六槍 == null ? 0 : data.六槍;
                ////data.八槍 = data.八槍 == null ? 0 : data.八槍;
                ////data.自助加油機數Str = data.自助加油機數 == null || data.自助加油機數 == 0 ? "無" : "有";
                ////data.self_單槍 = data.self_單槍 == null ? 0 : data.self_單槍;
                ////data.self_雙槍 = data.self_雙槍 == null ? 0 : data.self_雙槍;
                ////data.self_四槍 = data.self_四槍 == null ? 0 : data.self_四槍;
                ////data.self_六槍 = data.self_六槍 == null ? 0 : data.self_六槍;
                ////data.self_八槍 = data.self_八槍 == null ? 0 : data.self_八槍;
                ////data.油槽總數 = data.油槽總數 == null ? 0 : data.油槽總數;

                //////列管現況(公告污染場址類型公告日期)
                ////date = DateTime.MinValue;
                ////if (data.公告污染場址類型 == "依細則第八條限期採取適當措施")
                ////{
                ////    date = data.Limit_Date;
                ////}
                ////else if (data.公告污染場址類型 == "依七條五採取應變必要措施")
                ////{
                ////    date = data.take_Date;
                ////}
                ////else if (data.公告污染場址類型 == "劃定地下水受污染限制使用地區及限制事項")
                ////{
                ////    date = data.GW_Date;
                ////}
                ////else if (data.公告污染場址類型 == "控制場址")
                ////{
                ////    date = data.Control_Date;
                ////}
                ////else if (data.公告污染場址類型 == "整治場址")
                ////{
                ////    date = data.Rem_Date;
                ////}
                ////data.公告污染場址類型公告日期 = date;

                //////列管現況(公告污染場址類型解列日期)
                ////date = DateTime.MinValue;
                ////if (data.公告污染場址類型 == "解除依細則第八條限期採取適當措施" ||
                ////    data.公告污染場址類型 == "公告解除劃定地下水受污染限制使用地區及限制事項" ||
                ////    data.公告污染場址類型 == "公告解除控制場址" ||
                ////    data.公告污染場址類型 == "公告解除整治場址" ||
                ////    data.公告污染場址類型 == "解除依七條五採取應變必要措施")
                ////{
                ////    date = data.Situation_Date;
                ////}

                ////data.公告污染場址類型解列日期 = date;
            }

            return allData;
        }

        public class SelfFuel_1
        {
            public bool? IsConfirm { get; set; }
            public string 案件編號 { get; set; }            
            public string 縣市別 { get; set; }
            public string 設施名稱 { get; set; }
            public int? 變更次數 { get; set; }
            public string 營業主體 { get; set; }
            public string 使用狀況第一層 { get; set; }
            public string 使用狀況第二層 { get; set; }
            public string 使用狀況第三層 { get; set; }
            public string 使用狀況第四層 { get; set; }

            public string UsageState { get; set; }
            public string UsageState_Second { get; set; }
            public string UsageState_Third { get; set; }
            public string UsageState_Fourth { get; set; }            
            public DateTime? 收件日期 { get; set; }
            
            public DateTime? 核准使用日期 { get; set; }
            public DateTime? 結束使用日期 { get; set; }
            public string 特許執照號碼 { get; set; }
            public string 設置場所第一層 { get; set; }
            public string 設置場所第二層 { get; set; }
            public string 設置場所其他 { get; set; }
            public string 設置場所基地 { get; set; }
            public string 負責人姓名 { get; set; }
            public string 設施電話 { get; set; }            
            public string 設施地址 { get; set; }
            public string 設施地號 { get; set; }

            public string 保單號碼 { get; set; }
            public string 保險公司名稱 { get; set; }
            public string 保險公司名稱_其他 { get; set; }
            public DateTime? 保單有效期間_起 { get; set; }
            public DateTime? 保單有效期間_迄 { get; set; }
            public string 保險類型 { get; set; }            
            public string 土地權屬 { get; set; }            
            public double? 用地總面積 { get; set; }
            public string 用地類別 { get; set; }
            public string 土地使用分區 { get; set; }            
            public string 設施狀況之設施類型 { get; set; }
            
            public int? 設施狀況之油槽總數 { get; set; }

            public int? 加油槍數_單槍 { get; set; }
            public int? 加油槍數_雙槍 { get; set; }
            public int? 加油槍數_四槍 { get; set; }
            public int? 加油槍數_六槍 { get; set; }
            public int? 加油槍數_八槍 { get; set; }
            public int? 加油槍數_總計 { get; set; }

            public string 油品種類 { get; set; }
            public double? 儲槽容量_油罐車載油容量 { get; set; }
            public int? 儲槽位置數量_地上 { get; set; }
            public int? 儲槽位置數量_地下 { get; set; }            
            public DateTime? ModifyTime { get; set; }
            public string AreaNo { get; set; }
        }
    }
}