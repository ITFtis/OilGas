using OilGas.Controllers.Audit;
using OilGas.Models.TableView;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace OilGas.Models
{

    public partial class OilGasModelContextExt : Dou.Models.ModelContextBase<User, Role>
    {
        public OilGasModelContextExt()
            : base("name=OilGasModelContextExt")
        {
            Database.SetInitializer<OilGasModelContextExt>(null);
        }

        public virtual DbSet<Check_Basic_local> Check_Basic_local { get; set; }
        public virtual DbSet<Sign> Sign { get; set; }
        public virtual DbSet<Lesson> Lesson { get; set; }
        public virtual DbSet<FileType> FileType { get; set; }
        public virtual DbSet<FileDownload> FileDownload { get; set; }
        public virtual DbSet<Facility_Detail> Facility_Detail { get; set; }
        public virtual DbSet<Facility> Facility { get; set; }
        public virtual DbSet<UsageState> UsageState { get; set; }
        public virtual DbSet<UsageStateCode1> UsageStateCode1 { get; set; }
        public virtual DbSet<UsageStateCode2> UsageStateCode2 { get; set; }
        public virtual DbSet<UsageStateCode3> UsageStateCode3 { get; set; }
        public virtual DbSet<UsageStateCode4> UsageStateCode4 { get; set; }
        public virtual DbSet<UsageStateCode5> UsageStateCode5 { get; set; }
        public virtual DbSet<UsageStateCode6> UsageStateCode6 { get; set; }
        public virtual DbSet<UsageStateCode7> UsageStateCode7 { get; set; }
        public virtual DbSet<UsageStateDetail> UsageStateDetail { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Administration> Administration { get; set; }
        public virtual DbSet<AreaCode> AreaCode { get; set; }
        public virtual DbSet<CarVehicleGas_BusinessOrganization> CarVehicleGas_BusinessOrganization { get; set; }
        public virtual DbSet<CarVehicleGas_BusinessState> CarVehicleGas_BusinessState { get; set; }
        public virtual DbSet<CarVehicleGas_CopyUnit> CarVehicleGas_CopyUnit { get; set; }
        public virtual DbSet<CarVehicleGas_DispatchClass> CarVehicleGas_DispatchClass { get; set; }
        public virtual DbSet<CarVehicleGas_DispatchUnit> CarVehicleGas_DispatchUnit { get; set; }
        public virtual DbSet<CarVehicleGas_Facility> CarVehicleGas_Facility { get; set; }
        public virtual DbSet<CarVehicleGas_Facility2> CarVehicleGas_Facility2 { get; set; }
        public virtual DbSet<CarVehicleGas_GasBasicData_ColumnName> CarVehicleGas_GasBasicData_ColumnName { get; set; }
        public virtual DbSet<CarVehicleGas_GasBasicData_Dispatch_Temp> CarVehicleGas_GasBasicData_Dispatch_Temp { get; set; }
        public virtual DbSet<CarVehicleGas_GasBasicData_GasBasicData_Temp> CarVehicleGas_GasBasicData_GasBasicData_Temp { get; set; }
        public virtual DbSet<CarVehicleGas_GasBasicData_Insurance_Temp> CarVehicleGas_GasBasicData_Insurance_Temp { get; set; }
        public virtual DbSet<CarVehicleGas_GasBasicData_Land_Temp> CarVehicleGas_GasBasicData_Land_Temp { get; set; }
        public virtual DbSet<CarVehicleGas_GasBasicData_SaleSoil_Temp> CarVehicleGas_GasBasicData_SaleSoil_Temp { get; set; }
        public virtual DbSet<CarVehicleGas_InsuranceCompanyName> CarVehicleGas_InsuranceCompanyName { get; set; }
        public virtual DbSet<CarVehicleGas_LandClass> CarVehicleGas_LandClass { get; set; }
        public virtual DbSet<CarVehicleGas_LandPriority> CarVehicleGas_LandPriority { get; set; }
        public virtual DbSet<CarVehicleGas_LandUsageZone> CarVehicleGas_LandUsageZone { get; set; }
        public virtual DbSet<CarVehicleGas_LicenseNo> CarVehicleGas_LicenseNo { get; set; }
        public virtual DbSet<CarVehicleGas_SaleSoilClass> CarVehicleGas_SaleSoilClass { get; set; }
        public virtual DbSet<CarVehicleGas_SoilServer> CarVehicleGas_SoilServer { get; set; }
        public virtual DbSet<Check_Item_Fish> Check_Item_Fish { get; set; }
        public virtual DbSet<Check_Item_Fish_Action> Check_Item_Fish_Action { get; set; }
        public virtual DbSet<Check_Item_Fish103> Check_Item_Fish103 { get; set; }
        public virtual DbSet<Check_Item_Fish103_Action> Check_Item_Fish103_Action { get; set; }
        public virtual DbSet<Check_Item_SelfDown> Check_Item_SelfDown { get; set; }
        public virtual DbSet<Check_Item_SelfDown_Action> Check_Item_SelfDown_Action { get; set; }
        public virtual DbSet<Check_Item_SelfUP> Check_Item_SelfUP { get; set; }
        public virtual DbSet<Check_Item_SelfUP_Action> Check_Item_SelfUP_Action { get; set; }
        public virtual DbSet<CheckCaseList> CheckCaseList { get; set; }
        public virtual DbSet<CheckItemList> CheckItemList { get; set; }
        public virtual DbSet<CheckTrack> CheckTrack { get; set; }
        public virtual DbSet<CityCode> CityCode { get; set; }
        public virtual DbSet<CounselingData> CounselingData { get; set; }
        public virtual DbSet<IllegalCaseGasBasicData> IllegalCaseGasBasicData { get; set; }
        public virtual DbSet<IllegalCaseState> IllegalCaseState { get; set; }
        public virtual DbSet<IllegalLaw> IllegalLaw { get; set; }
        public virtual DbSet<IllegalNotice> IllegalNotice { get; set; }
        public virtual DbSet<Law_Data> Law_Data { get; set; }
        public virtual DbSet<Law_Item> Law_Item { get; set; }
        public virtual DbSet<Law_Math> Law_Math { get; set; }
        public virtual DbSet<Lazybag> Lazybag { get; set; }
     
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<PleadMain> PleadMain { get; set; }
        public virtual DbSet<PortGas_Ban> PortGas_Ban { get; set; }
        public virtual DbSet<PortGas_Ban_Installment> PortGas_Ban_Installment { get; set; }
        public virtual DbSet<PortGas_Ban_Log> PortGas_Ban_Log { get; set; }
        public virtual DbSet<PortGas_Ban_Penalty> PortGas_Ban_Penalty { get; set; }
        public virtual DbSet<PortGas_Ban_Temp> PortGas_Ban_Temp { get; set; }
        public virtual DbSet<PortGas_BasicData> PortGas_BasicData { get; set; }
        public virtual DbSet<PortGas_BasicData_Log> PortGas_BasicData_Log { get; set; }
        public virtual DbSet<PortGas_BasicData_Temp> PortGas_BasicData_Temp { get; set; }
        public virtual DbSet<PortGas_Code> PortGas_Code { get; set; }
        public virtual DbSet<PortGas_CopyUnit> PortGas_CopyUnit { get; set; }
        public virtual DbSet<PortGas_Counseling> PortGas_Counseling { get; set; }
        public virtual DbSet<PortGas_CounselingFile> PortGas_CounselingFile { get; set; }
        public virtual DbSet<PortGas_Dispatch> PortGas_Dispatch { get; set; }
        public virtual DbSet<PortGas_Dispatch_log> PortGas_Dispatch_log { get; set; }
        public virtual DbSet<PortGas_Insurance> PortGas_Insurance { get; set; }
        public virtual DbSet<PortGas_Insurance_Log> PortGas_Insurance_Log { get; set; }
        public virtual DbSet<PortGas_OilData> PortGas_OilData { get; set; }
        public virtual DbSet<PortGas_OilData_Log> PortGas_OilData_Log { get; set; }
        public virtual DbSet<PortGas_UsageState> PortGas_UsageState { get; set; }
        public virtual DbSet<Protection_BasicData> Protection_BasicData { get; set; }
        public virtual DbSet<Protection_WorkExperience> Protection_WorkExperience { get; set; }
      
    
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SelfFuel_Ban> SelfFuel_Ban { get; set; }
        public virtual DbSet<SelfFuel_Ban_Log> SelfFuel_Ban_Log { get; set; }
        public virtual DbSet<SelfFuel_Ban_ManyPayMoney> SelfFuel_Ban_ManyPayMoney { get; set; }
        public virtual DbSet<SelfFuel_Ban_Penalty> SelfFuel_Ban_Penalty { get; set; }
        public virtual DbSet<SelfFuel_Ban_UploadFile> SelfFuel_Ban_UploadFile { get; set; }
        public virtual DbSet<SelfFuel_Basic> SelfFuel_Basic { get; set; }
        public virtual DbSet<SelfFuel_Basic_Log> SelfFuel_Basic_Log { get; set; }
        public virtual DbSet<SelfFuel_Dispatch> SelfFuel_Dispatch { get; set; }
        public virtual DbSet<SelfFuel_Dispatch_Log> SelfFuel_Dispatch_Log { get; set; }
        public virtual DbSet<SelfFuel_Facility> SelfFuel_Facility { get; set; }
        public virtual DbSet<SelfFuel_Facility_Log> SelfFuel_Facility_Log { get; set; }
        public virtual DbSet<SelfFuel_Insurance> SelfFuel_Insurance { get; set; }
        public virtual DbSet<SelfFuel_Land> SelfFuel_Land { get; set; }
        public virtual DbSet<SelfFuel_Land_Log> SelfFuel_Land_Log { get; set; }
        public virtual DbSet<SelfFuel_Oil> SelfFuel_Oil { get; set; }
        public virtual DbSet<SelfFuel_Oil_Log> SelfFuel_Oil_Log { get; set; }
        public virtual DbSet<SelfGas_Ban> SelfGas_Ban { get; set; }
        public virtual DbSet<SelfGas_Ban_Log> SelfGas_Ban_Log { get; set; }
        public virtual DbSet<SystemPwdLog> SystemPwdLog { get; set; }
        public virtual DbSet<SystemUsageRate> SystemUsageRate { get; set; }
        public virtual DbSet<Test_FuncInGroup> Test_FuncInGroup { get; set; }
        public virtual DbSet<Test_Group> Test_Group { get; set; }
        public virtual DbSet<Test_MemberBasicData> Test_MemberBasicData { get; set; }
        public virtual DbSet<Test_MemberFuncPower> Test_MemberFuncPower { get; set; }

        public virtual DbSet<Agenda> Agenda { get; set; }
        public virtual DbSet<AncillaryFacility> AncillaryFacility { get; set; }
        public virtual DbSet<Briefing> Briefing { get; set; }
        public virtual DbSet<CarFuel_Ban> CarFuel_Ban { get; set; }
        public virtual DbSet<CarFuel_Ban_Installment> CarFuel_Ban_Installment { get; set; }
        public virtual DbSet<CarFuel_Ban_Log> CarFuel_Ban_Log { get; set; }
        public virtual DbSet<CarFuel_Ban_Penalty> CarFuel_Ban_Penalty { get; set; }
        public virtual DbSet<CarFuel_Ban_Temp> CarFuel_Ban_Temp { get; set; }
        public virtual DbSet<CarFuel_BasicData> CarFuel_BasicData { get; set; }
        public virtual DbSet<CarFuel_BasicData_Log> CarFuel_BasicData_Log { get; set; }
        public virtual DbSet<CarFuel_BasicData_Temp> CarFuel_BasicData_Temp { get; set; }
        public virtual DbSet<CarFuel_Dispatch> CarFuel_Dispatch { get; set; }
        public virtual DbSet<CarFuel_Dispatch_Log> CarFuel_Dispatch_Log { get; set; }
        public virtual DbSet<CarFuel_Insurance> CarFuel_Insurance { get; set; }
        public virtual DbSet<CarFuel_Insurance_Log> CarFuel_Insurance_Log { get; set; }
        public virtual DbSet<CarFuel_OilData> CarFuel_OilData { get; set; }
        public virtual DbSet<CarFuel_OilData_Log> CarFuel_OilData_Log { get; set; }
        public virtual DbSet<CarGas_Ban> CarGas_Ban { get; set; }
        public virtual DbSet<CarGas_Ban_Installment> CarGas_Ban_Installment { get; set; }
        public virtual DbSet<CarGas_Ban_Log> CarGas_Ban_Log { get; set; }
        public virtual DbSet<CarGas_Ban_Penalty> CarGas_Ban_Penalty { get; set; }
        public virtual DbSet<CarGas_Ban_Temp> CarGas_Ban_Temp { get; set; }
        public virtual DbSet<CarGas_BasicData> CarGas_BasicData { get; set; }
        public virtual DbSet<CarGas_BasicData_Log> CarGas_BasicData_Log { get; set; }
        public virtual DbSet<CarGas_BasicData_Temp> CarGas_BasicData_Temp { get; set; }
        public virtual DbSet<CarGas_Dispatch> CarGas_Dispatch { get; set; }
        public virtual DbSet<CarGas_Dispatch_Log> CarGas_Dispatch_Log { get; set; }
        public virtual DbSet<CarGas_Insurance> CarGas_Insurance { get; set; }
        public virtual DbSet<CarGas_Insurance_Log> CarGas_Insurance_Log { get; set; }
        public virtual DbSet<CarGas_OilData> CarGas_OilData { get; set; }
        public virtual DbSet<CarGas_OilData_Log> CarGas_OilData_Log { get; set; }
        public virtual DbSet<CarVehicleGas_GasBasicData_Dispatch_Temp_Log> CarVehicleGas_GasBasicData_Dispatch_Temp_Log { get; set; }
        public virtual DbSet<CarVehicleGas_GasBasicData_GasBasicData_Temp_Log> CarVehicleGas_GasBasicData_GasBasicData_Temp_Log { get; set; }
        public virtual DbSet<CarVehicleGas_GasBasicData_Insurance_Temp_Log> CarVehicleGas_GasBasicData_Insurance_Temp_Log { get; set; }
        public virtual DbSet<CarVehicleGas_GasBasicData_Land_Temp_Log> CarVehicleGas_GasBasicData_Land_Temp_Log { get; set; }
        public virtual DbSet<CarVehicleGas_GasBasicData_SaleSoil_Temp_Log> CarVehicleGas_GasBasicData_SaleSoil_Temp_Log { get; set; }
        public virtual DbSet<CaseNo> CaseNo { get; set; }
        public virtual DbSet<changebusiness> changebusiness { get; set; }
        public virtual DbSet<Check_Basic> Check_Basic { get; set; }
        public virtual DbSet<Check_Basic_Action> Check_Basic_Action { get; set; }
        public virtual DbSet<Check_Consolidated_Comments> Check_Consolidated_Comments { get; set; }
        public virtual DbSet<Check_Consolidated_Comments_Action> Check_Consolidated_Comments_Action { get; set; }
        public virtual DbSet<Check_Counseling> Check_Counseling { get; set; }
        public virtual DbSet<Check_document> Check_document { get; set; }
        public virtual DbSet<Check_File> Check_File { get; set; }
        public virtual DbSet<Check_File_Action> Check_File_Action { get; set; }
        public virtual DbSet<Check_File_Counseling> Check_File_Counseling { get; set; }
        public virtual DbSet<Check_Item> Check_Item { get; set; }
        public virtual DbSet<Check_Item_97> Check_Item_97 { get; set; }
        public virtual DbSet<Check_Item_Action> Check_Item_Action { get; set; }
        public virtual DbSet<Check_PdfFile> Check_PdfFile { get; set; }
        public virtual DbSet<Check_Tank_well> Check_Tank_well { get; set; }
        public virtual DbSet<Check_Tank_well_Action> Check_Tank_well_Action { get; set; }
        public virtual DbSet<Communications> Communications { get; set; }
        public virtual DbSet<Counseling_Rate_Business> Counseling_Rate_Business { get; set; }
        public virtual DbSet<Counseling_Rate_City> Counseling_Rate_City { get; set; }
        public virtual DbSet<DispatchClassCode> DispatchClassCode { get; set; }
        public virtual DbSet<FishGas_Ban> FishGas_Ban { get; set; }
        public virtual DbSet<FishGas_Ban_Installment> FishGas_Ban_Installment { get; set; }
        public virtual DbSet<FishGas_Ban_Log> FishGas_Ban_Log { get; set; }
        public virtual DbSet<FishGas_Ban_Penalty> FishGas_Ban_Penalty { get; set; }
        public virtual DbSet<FishGas_Ban_Temp> FishGas_Ban_Temp { get; set; }
        public virtual DbSet<FishGas_BasicData> FishGas_BasicData { get; set; }
        public virtual DbSet<FishGas_BasicData_Log> FishGas_BasicData_Log { get; set; }
        public virtual DbSet<FishGas_BasicData_Temp> FishGas_BasicData_Temp { get; set; }
        public virtual DbSet<FishGas_Dispatch> FishGas_Dispatch { get; set; }
        public virtual DbSet<FishGas_Dispatch_Log> FishGas_Dispatch_Log { get; set; }
        public virtual DbSet<FishGas_Insurance> FishGas_Insurance { get; set; }
        public virtual DbSet<FishGas_Insurance_Log> FishGas_Insurance_Log { get; set; }
        public virtual DbSet<FishGas_OilData> FishGas_OilData { get; set; }
        public virtual DbSet<FishGas_OilData_Log> FishGas_OilData_Log { get; set; }
        public virtual DbSet<Gas_Total_Temp> Gas_Total_Temp { get; set; }
        public virtual DbSet<GasLawBan> GasLawBan { get; set; }
        public virtual DbSet<GasLawBan_Installment> GasLawBan_Installment { get; set; }
        public virtual DbSet<GasLawBan_Temp> GasLawBan_Temp { get; set; }
        public virtual DbSet<LandClassCode> LandClassCode { get; set; }
        public virtual DbSet<LandUsageZoneCode> LandUsageZoneCode { get; set; }
        public virtual DbSet<news> news { get; set; }
        public virtual DbSet<PermissionSetting> PermissionSetting { get; set; }
        public virtual DbSet<RecordLog> RecordLog { get; set; }
        public virtual DbSet<registration> registration { get; set; }
        public virtual DbSet<RP_Check_Hiatus_ByBusinessOrganization> RP_Check_Hiatus_ByBusinessOrganization { get; set; }
        public virtual DbSet<RP_Check_Hiatus_ByBusiOrgAndCity> RP_Check_Hiatus_ByBusiOrgAndCity { get; set; }
        public virtual DbSet<RP_Check_Hiatus_ByCity> RP_Check_Hiatus_ByCity { get; set; }
        public virtual DbSet<RP_Check_HiatusItem97_ByYear> RP_Check_HiatusItem97_ByYear { get; set; }
        public virtual DbSet<RP_Check_HiatusItem97_ByYearAndBusiOrg> RP_Check_HiatusItem97_ByYearAndBusiOrg { get; set; }
        public virtual DbSet<RP_Check_HiatusItem97_ByYearAndBusiOrgAndCity> RP_Check_HiatusItem97_ByYearAndBusiOrgAndCity { get; set; }
        public virtual DbSet<RP_Check_HiatusItem97_ByYearAndCity> RP_Check_HiatusItem97_ByYearAndCity { get; set; }
        public virtual DbSet<RP_Check_HiatusItem98_ByYear> RP_Check_HiatusItem98_ByYear { get; set; }
        public virtual DbSet<RP_Check_HiatusItem98_ByYearAndBusiOrg> RP_Check_HiatusItem98_ByYearAndBusiOrg { get; set; }
        public virtual DbSet<RP_Check_HiatusItem98_ByYearAndBusiOrgAndCity> RP_Check_HiatusItem98_ByYearAndBusiOrgAndCity { get; set; }
        public virtual DbSet<RP_Check_HiatusItem98_ByYearAndCity> RP_Check_HiatusItem98_ByYearAndCity { get; set; }
        public virtual DbSet<SeaportZone> SeaportZone { get; set; }
        public virtual DbSet<SelfFuel_Insurance_Log> SelfFuel_Insurance_Log { get; set; }
        public virtual DbSet<SelfFuel_Insurance_Log1080104> SelfFuel_Insurance_Log1080104 { get; set; }
        public virtual DbSet<SelfGas_Ban_ManyPayMoney> SelfGas_Ban_ManyPayMoney { get; set; }
        public virtual DbSet<SelfGas_Ban_Penalty> SelfGas_Ban_Penalty { get; set; }
        public virtual DbSet<SelfGas_Ban_UploadFile> SelfGas_Ban_UploadFile { get; set; }
        public virtual DbSet<SelfGas_Basic> SelfGas_Basic { get; set; }
        public virtual DbSet<SelfGas_Basic_log> SelfGas_Basic_log { get; set; }
        public virtual DbSet<SelfGas_Dispatch> SelfGas_Dispatch { get; set; }
        public virtual DbSet<SelfGas_Dispatch_Log> SelfGas_Dispatch_Log { get; set; }
        public virtual DbSet<SelfGas_Facility> SelfGas_Facility { get; set; }
        public virtual DbSet<SelfGas_Facility_Log> SelfGas_Facility_Log { get; set; }
        public virtual DbSet<SelfGas_Insurance> SelfGas_Insurance { get; set; }
        public virtual DbSet<SelfGas_Insurance_Log> SelfGas_Insurance_Log { get; set; }
        public virtual DbSet<SelfGas_Insurance_Log1080104> SelfGas_Insurance_Log1080104 { get; set; }
        public virtual DbSet<SelfGas_Land> SelfGas_Land { get; set; }
        public virtual DbSet<SelfGas_Land_Log> SelfGas_Land_Log { get; set; }
        public virtual DbSet<SelfGas_Oil> SelfGas_Oil { get; set; }
        public virtual DbSet<SelfGas_Oil_Log> SelfGas_Oil_Log { get; set; }
        public virtual DbSet<UsageState_Fourth> UsageState_Fourth { get; set; }
        public virtual DbSet<UsageState_Second> UsageState_Second { get; set; }
        public virtual DbSet<UsageState_Third> UsageState_Third { get; set; }
        public virtual DbSet<UsageStateCode> UsageStateCode { get; set; }

        public virtual DbSet<WS_GSM_Relation> WS_GSM_Relation { get; set; }
        public virtual DbSet<WS_GSM> WS_GSM { get; set; }       
        public virtual DbSet<FacilityType> FacilityType { get; set; }        
        public virtual DbSet<Check_Basic_View> Check_Basic_View { get; set; }

        public virtual DbSet<Check_Basic_AuditDay> Check_Basic_AuditDay { get; set; }
        public virtual DbSet<PublicFacility> PublicFacility { get; set; }

        #region view

        public virtual DbSet<vw_Audit_StatisticReportEquipView> vw_Audit_StatisticReportEquipView { get; set; }
        public virtual DbSet<vw_Audit_StatisticReportItemView> vw_Audit_StatisticReportItemView { get; set; }     
        public virtual DbSet<vw_UNPIVOT_Check_Item> vw_UNPIVOT_Check_Item { get; set; }
        public virtual DbSet<vw_CarFuel_Closed> vw_CarFuel_Closed { get; set; }
        public virtual DbSet<vw_CarFuel_CheckOilTypeByYear> vw_CarFuel_CheckOilTypeByYear { get; set; }
        public virtual DbSet<vw_CarFuel_GSM_Select> vw_CarFuel_GSM_Select { get; set; }
        public virtual DbSet<vw_CarFuel_CaseError1> vw_CarFuel_CaseError1 { get; set; }
        public virtual DbSet<vw_CarFuel_CarVehicleGas_CheckError1> vw_CarFuel_CarVehicleGas_CheckError1 { get; set; }
        public virtual DbSet<vw_CarGas_Closed> vw_CarGas_Closed { get; set; }
        public virtual DbSet<vw_FishGas_Closed> vw_FishGas_Closed { get; set; }

        public virtual DbSet<vw_UNPIVOT_Check_Item_Other> vw_UNPIVOT_Check_Item_Other { get; set; }
        public virtual DbSet<vw_UNPIVOT_Check_Item_For_Doesmeet> vw_UNPIVOT_Check_Item_For_Doesmeet { get; set; }
        
        public virtual DbSet<vw_UNION_Check_Item_For_AllDoesmeet> vw_UNION_Check_Item_For_AllDoesmeet { get; set; }
        public virtual DbSet<CarVehicleGas_BusinessOrganizationV> CarVehicleGas_BusinessOrganizationV { get; set; }

        public virtual DbSet<vw_Audit_ReportCheckItemOil> vw_Audit_ReportCheckItemOil { get; set; }

        public virtual DbSet<gas_total_tempV> gas_total_tempV { get; set; }

        #endregion
    }
}
