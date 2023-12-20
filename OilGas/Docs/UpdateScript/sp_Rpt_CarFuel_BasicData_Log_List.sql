CREATE PROCEDURE [dbo].[sp_Rpt_CarFuel_BasicData_Log_List]
	--cb.CaseNo,cb.Gas_Name
	@disColumns nvarchar(1000),
	@strWhr Nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DECLARE @qry nvarchar(max);

	Set @qry = 'Select Distinct ' + @disColumns + '
						from CarFuel_BasicData_Log cb
						left join CarFuel_OilData_Log co on cb.CaseNo = co.CaseNo
						left join CarFuel_Insurance_Log ci on cb.CaseNo = ci.CaseNo
						left join CarFuel_Dispatch_Log cd on cb.CaseNo = cd.CaseNo
						left join UsageStateCode u On u.Value = cb.UsageState
						left Join CarVehicleGas_BusinessOrganization bo on cb.Business_theme = bo.value
						Left Join CarVehicleGas_InsuranceCompanyName c on ci.Insurance_Company = c.value
						Left join LandClassCode lc on cb.LandClass = lc.value
						Left join LandUsageZoneCode luc on cb.LandUsageZone = luc.value
						Left join CarVehicleGas_SaleSoilClass cs on co.SaleSoilClass = cs.Value
						Left Join WS_GSM_Relation r with(nolock) On cb.CaseNo = r.CaseNo 
						Left Join WS_GSM g with(nolock) On g.gsm_id = r.FacNo
						Where 1 = 1
					'
				+ @strWhr

	--Print @qry
	EXECUTE sp_executesql @qry
END
GO


