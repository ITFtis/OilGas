using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilGas
{
    /// <summary>
    /// 航港自用加儲油/E統計報表專區/現況資料匯出
    /// </summary>
    public class Rpt_PortGas_BasicData
    {
        static object lockGetAllDatas = new object();
        public static IEnumerable<PortGas_1> GetAllDatas(int cachetimer = 0)
        {
            if (cachetimer == 0) cachetimer = Constant.cacheReportTime;

            string key = "OilGas.Rpt_PortGas_BasicData";
            var allData = DouHelper.Misc.GetCache<IEnumerable<PortGas_1>>(cachetimer, key);
            lock (lockGetAllDatas)
            {
                if (allData == null)
                //if(true)
                {
                    System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
                    Dou.Models.DB.IModelEntity<PortGas_BasicData> portGas_BasicData = new Dou.Models.DB.ModelEntity<PortGas_BasicData>(dbContext);
                    Dou.Models.DB.IModelEntity<PortGas_UsageState> portGas_UsageState = new Dou.Models.DB.ModelEntity<PortGas_UsageState>(dbContext);
                    Dou.Models.DB.IModelEntity<PortGas_Insurance> portGas_Insurance = new Dou.Models.DB.ModelEntity<PortGas_Insurance>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_InsuranceCompanyName> carVehicleGas_InsuranceCompanyName = new Dou.Models.DB.ModelEntity<CarVehicleGas_InsuranceCompanyName>(dbContext);

                    allData = portGas_BasicData.GetAll()
                        
                                    .GroupJoin(portGas_UsageState.GetAll(), a => a.UsageState, b => b.UsageState_TypeID, (o, c) => new { o.CaseNo, o.LocationType, o.Location, o.Gas_Name, o.Gas_Location, o.Change, o.ApparatusOwner, o.UsageState, o.UsageState1, o.UsageState2, o.UsageState3, o.UsageState4, o.Recipient_date, StartDate = o.Report_date, EndDate = o.StopReport_date, o.LicenseNo1, o.LicenseNo2, o.Boss, o.Boss_Tel, o.Boss_Email, o.BasicFacilities, o.OtherFacilities, o.SupplyTarget, o.Mod_date,
                                        UsageStateName = c.FirstOrDefault().UsageState_TypeName})
                                    .GroupJoin(portGas_UsageState.GetAll(), a => a.UsageState1, b => b.UsageState_TypeID, (o, c) => new { o.CaseNo, o.LocationType, o.Location, o.Gas_Name, o.Gas_Location, o.Change, o.ApparatusOwner, o.UsageState, o.UsageState1, o.UsageState2, o.UsageState3, o.UsageState4, o.UsageStateName, o.Recipient_date, o.StartDate, o.EndDate, o.LicenseNo1, o.LicenseNo2, o.Boss, o.Boss_Tel, o.Boss_Email, o.BasicFacilities, o.OtherFacilities, o.SupplyTarget, o.Mod_date,
                                        UsageStateName1 = c.FirstOrDefault().UsageState_TypeName})
                                    .GroupJoin(portGas_UsageState.GetAll(), a => a.UsageState2, b => b.UsageState_TypeID, (o, c) => new { o.CaseNo, o.LocationType, o.Location, o.Gas_Name, o.Gas_Location, o.Change, o.ApparatusOwner, o.UsageState, o.UsageState1, o.UsageState2, o.UsageState3, o.UsageState4, o.UsageStateName, o.UsageStateName1, o.Recipient_date, o.StartDate, o.EndDate, o.LicenseNo1, o.LicenseNo2, o.Boss, o.Boss_Tel, o.Boss_Email, o.BasicFacilities, o.OtherFacilities, o.SupplyTarget, o.Mod_date,
                                        UsageStateName2 = c.FirstOrDefault().UsageState_TypeName})
                                    .GroupJoin(portGas_UsageState.GetAll(), a => a.UsageState3, b => b.UsageState_TypeID, (o, c) => new { o.CaseNo, o.LocationType, o.Location, o.Gas_Name, o.Gas_Location, o.Change, o.ApparatusOwner, o.UsageState, o.UsageState1, o.UsageState2, o.UsageState3, o.UsageState4, o.UsageStateName, o.UsageStateName1, o.UsageStateName2, o.Recipient_date, o.StartDate, o.EndDate, o.LicenseNo1, o.LicenseNo2, o.Boss, o.Boss_Tel, o.Boss_Email, o.BasicFacilities, o.OtherFacilities, o.SupplyTarget, o.Mod_date,
                                        UsageStateName3 = c.FirstOrDefault().UsageState_TypeName})
                                    .GroupJoin(portGas_UsageState.GetAll(), a => a.UsageState4, b => b.UsageState_TypeID, (o, c) => new { o.CaseNo, o.LocationType, o.Location, o.Gas_Name, o.Gas_Location, o.Change, o.ApparatusOwner, o.UsageState, o.UsageState1, o.UsageState2, o.UsageState3, o.UsageState4, o.UsageStateName, o.UsageStateName1, o.UsageStateName2, o.UsageStateName3, o.Recipient_date, o.StartDate, o.EndDate, o.LicenseNo1, o.LicenseNo2, o.Boss, o.Boss_Tel, o.Boss_Email, o.BasicFacilities, o.OtherFacilities, o.SupplyTarget, o.Mod_date,
                                        UsageStateName4 = c.FirstOrDefault().UsageState_TypeName})
                                    .GroupJoin(portGas_Insurance.GetAll(), 
                                        a => new { a.CaseNo, a.Change },
                                        b => new { b.CaseNo, b.Change }, (o, c) => new { o.CaseNo, o.LocationType, o.Location, o.Gas_Name, o.Gas_Location, o.Change, o.ApparatusOwner, o.UsageState, o.UsageState1, o.UsageState2, o.UsageState3, o.UsageState4, o.UsageStateName, o.UsageStateName1, o.UsageStateName2, o.UsageStateName3, o.Recipient_date, o.StartDate, o.EndDate, o.LicenseNo1, o.LicenseNo2, o.Boss, o.Boss_Tel, o.Boss_Email, o.BasicFacilities, o.OtherFacilities, o.SupplyTarget, o.Mod_date, o.UsageStateName4, c})
                                    .SelectMany(b => b.c.DefaultIfEmpty(), (o, c) => new {                                    
                                        o.CaseNo, o.LocationType, o.Location, o.Gas_Name, o.Gas_Location, o.Change, o.ApparatusOwner, o.UsageState, o.UsageState1, o.UsageState2, o.UsageState3, o.UsageState4, o.UsageStateName, o.UsageStateName1, o.UsageStateName2, o.UsageStateName3, o.Recipient_date, o.StartDate, o.EndDate, o.LicenseNo1, o.LicenseNo2, o.Boss, o.Boss_Tel, o.Boss_Email, o.BasicFacilities, o.OtherFacilities, o.SupplyTarget, o.Mod_date, o.UsageStateName4,
                                        c.Insurance_Company, c.Insurance_No, c.Insurance_otherCompany, c.Insurance_policy_start, c.Insurance_policy_end, c.Insurance_Type
                                    })
                                    .GroupJoin(carVehicleGas_InsuranceCompanyName.GetAll(), a => a.Insurance_Company, b => b.Value, (o, c) => new { o.CaseNo, o.LocationType, o.Location, o.Gas_Name, o.Gas_Location, o.Change, o.ApparatusOwner, o.UsageState, o.UsageState1, o.UsageState2, o.UsageState3, o.UsageState4, o.UsageStateName, o.UsageStateName1, o.UsageStateName2, o.UsageStateName3, o.Recipient_date, o.StartDate, o.EndDate, o.LicenseNo1, o.LicenseNo2, o.Boss, o.Boss_Tel, o.Boss_Email, o.BasicFacilities, o.OtherFacilities, o.SupplyTarget, o.Mod_date, o.UsageStateName4,
                                        o.Insurance_Company, o.Insurance_No, o.Insurance_otherCompany, o.Insurance_policy_start, o.Insurance_policy_end, o.Insurance_Type
                                        , InsuranceCompanyName = c.FirstOrDefault().Name })
                                .Select(o => new PortGas_1
                                {
                                    案件編號 = o.CaseNo,
                                    設施地點類型 = o.LocationType,
                                    設施地點 = o.Location,
                                    設施名稱 = o.Gas_Name,
                                    設施所在地 = o.Gas_Location,
                                    變更次數 = o.Change,
                                    設備使用人 = o.ApparatusOwner,
                                    目前狀態 = o.UsageStateName,
                                    使用狀況第一層 = o.UsageStateName1,
                                    使用狀況第二層 = o.UsageStateName2,
                                    使用狀況第三層 = o.UsageStateName3,
                                    使用狀況第四層 = o.UsageStateName4,
                                    UsageState = o.UsageState,
                                    收件日期 = o.Recipient_date,
                                    核准設置日期 = o.StartDate,
                                    結束使用日期 = o.EndDate,
                                    LicenseNo1 = o.LicenseNo1,
                                    LicenseNo2 = o.LicenseNo2,
                                    負責人姓名 = o.Boss,
                                    聯絡電話 = o.Boss_Tel,
                                    電子郵件信箱 = o.Boss_Email,
                                    基本設施 = o.BasicFacilities,
                                    其他設施 = o.OtherFacilities,
                                    供油對象 = o.SupplyTarget,
                                    保單號碼 = o.Insurance_No,
                                    保險公司名稱 = o.InsuranceCompanyName,
                                    保險公司名稱_其他 = o.Insurance_otherCompany,
                                    保單有效期間_起 = o.Insurance_policy_start,
                                    保單有效期間_迄 = o.Insurance_policy_end,
                                    保險類型 = o.Insurance_Type,
                                    Mod_date = o.Mod_date,
                                });

                    allData = allData
                        //.Where(a => a.案件編號 == "G082090001")
                        .Distinct().OrderBy(a => a.案件編號).ToList();

                    foreach (var data in allData)
                    {
                        string str = "";
                        DateTime? date = DateTime.MinValue;

                        data.核准設置文號 = data.LicenseNo1 + data.LicenseNo2;
                        
                        //保險類型
                        if (data.保險類型 != null)
                        {
                            data.保險類型 = data.保險類型.Replace("0", "公共意外責任保險").Replace("1", "意外污染責任保險");
                        }                        
                    }

                    DouHelper.Misc.AddCache(allData, key);
                }
            }

            return allData;
        }

        public static void ResetGetAllDatas()
        {
            string key = "OilGas.Rpt_PortGas_BasicData";
            DouHelper.Misc.ClearCache(key);
        }

        public class PortGas_1
        {
            public string 案件編號 { get; set; }
            public string 設施地點類型 { get; set; }
            public string 設施地點 { get; set; }
            public string 設施名稱 { get; set; }
            public string 設施所在地 { get; set; }
            public int? 變更次數 { get; set; }
            public string 設備使用人 { get; set; }
            public string 目前狀態 { get; set; }
            public string 使用狀況第一層 { get; set; }
            public string 使用狀況第二層 { get; set; }
            public string 使用狀況第三層 { get; set; }
            public string 使用狀況第四層 { get; set; }            
            public string UsageState { get; set; }
            public string 收件日期 { get; set; }
            public string 核准設置日期 { get; set; }
            public string 結束使用日期 { get; set; }
            public string 核准設置文號 { get; set; }
            public string LicenseNo1 { get; set; }
            public string LicenseNo2 { get; set; }
            public string 負責人姓名 { get; set; }
            public string 聯絡電話 { get; set; }
            public string 電子郵件信箱 { get; set; }
            public string 基本設施 { get; set; }
            public string 其他設施 { get; set; }
            public string 供油對象 { get; set; }
            public string 保單號碼 { get; set; }
            public string 保險公司名稱 { get; set; }
            public string 保險公司名稱_其他 { get; set; }
            public DateTime? 保單有效期間_起 { get; set; }
            public DateTime? 保單有效期間_迄 { get; set; }
            public string 保險類型 { get; set; }
            public DateTime? Mod_date { get; set; }
        }
    }
}