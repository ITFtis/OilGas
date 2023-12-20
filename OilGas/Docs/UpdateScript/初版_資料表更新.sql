------------------------------------------------------------------------------------------------------------------------------
----alter Table CarFuel_Insurance add MID bigint
----Go

----Update CarFuel_Insurance
----Set MID = b.ID 
----From CarFuel_Insurance a 
----Left Join CarFuel_BasicData b On a.CaseNo = b.CaseNo
----Go

----------Select a.MID, b.ID 
----------From CarFuel_Insurance a 
----------Left Join CarFuel_BasicData b On a.CaseNo = b.CaseNo


------------------------------------------------------------------
--正祥
--1.新增view(vw_UNPIVOT_Check_Item)
Create View [dbo].[vw_UNPIVOT_Check_Item] AS
Select CheckNo, name, value
FROM (
	Select CheckNo, A01, A02, A03, A04, A05, A06, B01, B02, B03, B04, B05, B06, B07, B08, B09, B10, C01, C02, C03, C04, C05, C06, C07, C08, C09, C10, C11, C12, C13, C14, D01, D02, D03, D04, D05, D06, D07, D08, D09, D10, D11, E01, E02, E03, F01, F02, F03, F04, F05, F06, F07, F08, F09, G01, G02, G03, G04, G05, G06, H01, H02, H03, H04, H05, I01, I02, I03, I04, I05, I06, I07, I08, I09, I10, J01, J02, J03, K01, K02, L01, L02, L03, M01
	From Check_Item
) AS p
UNPIVOT
(
    value FOR name IN (A01, A02, A03, A04, A05, A06, B01, B02, B03, B04, B05, B06, B07, B08, B09, B10, C01, C02, C03, C04, C05, C06, C07, C08, C09, C10, C11, C12, C13, C14, D01, D02, D03, D04, D05, D06, D07, D08, D09, D10, D11, E01, E02, E03, F01, F02, F03, F04, F05, F06, F07, F08, F09, G01, G02, G03, G04, G05, G06, H01, H02, H03, H04, H05, I01, I02, I03, I04, I05, I06, I07, I08, I09, I10, J01, J02, J03, K01, K02, L01, L02, L03, M01)
) AS pv
GO
--2.新增sp(sp_Rpt_CarFuel_BasicData_Log_List)
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

--3.新增view(vw_Audit_StatisticReportEquipView)
CREATE View [dbo].[vw_Audit_StatisticReportEquipView] AS
Select *
From
(
	SELECT Distinct 1 AS Rkind, 
		a.CityName as MapName, b.CityCode AS MapCode,
		year(CheckDate)-1911 as CheckYear,
		0 as CheckCount, 
		0 as CheckAllDoesmeet, 
		0 as CheckNoHiatusCount, 
		'' as Average
	FROM Check_Basic_View a
	Left Join CityCode b On a.CityName = b.CityName
	Union
	SELECT Distinct 2 AS rkind,
		ISNULL(Business_themeS, '') as MapName, ISNULL(Business_theme, '') AS MapCode,
		year(CheckDate)-1911 as CheckYear,
		0 as CheckCount, 
		0 as CheckAllDoesmeet, 
		0 as CheckNoHiatusCount, 
		'' as Average
	FROM Check_Basic_View
	Where ISNULL(Business_theme, '') <>  '' And ISNULL(Business_themeS, '') <>  ''
)aaa
--4.新增view(vw_Audit_StatisticReportItemView)
CREATE View [dbo].[vw_Audit_StatisticReportItemView] as
SELECT
	CheckItemTable,
	CheckItemDescNo,
	CheckItemTitel + '<br/>' + CheckItemDesc as ItemName,
	0 as ItemHiatusCountByBusi,
	0 as ItemHiatusCountByYear,
	0 as ItemHiatusCheckByBusi,
	0 as ItemHiatusCheckByYear,
	Convert(float, 0) as ItemHiatusPercentByBusi,
	Convert(float, 0) as ItemHiatusPercentByYear
FROM CheckItemList
GO
--5.新增view(vw_UNPIVOT_Check_Item)
CREATE View [dbo].[vw_UNPIVOT_Check_Item] AS
--checkcode:A01, A02, A03, A04, A05, A06, A07, A08, A09, A10, A11, A12, B01, B02, B03, B04, B05, B06, B07, B08, B09, B10, C01, C02, C03, C04, C05, C06, C07, C08, C09, C10, C11, C12, C13, C14, D01, D02, D03, D04, D05, D06, D07, D08, D09, D10, D11, E01, E02, E03, F01, F02, F03, F04, F05, F06, F07, F08, F09, F10, G01, G02, G03, G04, G05, G06, H01, H02, H03, H04, H05, I01, I02, I03, I04, I05, I06, I07, I08, I09, I10, J01, J02, J03, J04, J05, J06, J07, J08, J09, J10, J11, K01, K02, K03, K04, L01, L02, L03, M01, M02
Select CheckNo, name, value
FROM (
	Select CheckNo, A01, A02, A03, A04, A05, A06, B01, B02, B03, B04, B05, B06, B07, B08, B09, B10, C01, C02, C03, C04, C05, C06, C07, C08, C09, C10, C11, C12, C13, C14, D01, D02, D03, D04, D05, D06, D07, D08, D09, D10, D11, E01, E02, E03, F01, F02, F03, F04, F05, F06, F07, F08, F09, G01, G02, G03, G04, G05, G06, H01, H02, H03, H04, H05, I01, I02, I03, I04, I05, I06, I07, I08, I09, I10, J01, J02, J03, K01, K02, L01, L02, L03, M01
	From Check_Item
) AS p
UNPIVOT
(
	value FOR name IN (A01, A02, A03, A04, A05, A06, B01, B02, B03, B04, B05, B06, B07, B08, B09, B10, C01, C02, C03, C04, C05, C06, C07, C08, C09, C10, C11, C12, C13, C14, D01, D02, D03, D04, D05, D06, D07, D08, D09, D10, D11, E01, E02, E03, F01, F02, F03, F04, F05, F06, F07, F08, F09, G01, G02, G03, G04, G05, G06, H01, H02, H03, H04, H05, I01, I02, I03, I04, I05, I06, I07, I08, I09, I10, J01, J02, J03, K01, K02, L01, L02, L03, M01)
) AS pv
GO

--6.新增view(vw_UNPIVOT_Check_Item_Other)
CREATE View [dbo].[vw_UNPIVOT_Check_Item_Other] AS
--checkcode:A01, A02, A03, A04, A05, A06, A07, A08, A09, A10, A11, A12, B01, B02, B03, B04, B05, B06, B07, B08, B09, B10, C01, C02, C03, C04, C05, C06, C07, C08, C09, C10, C11, C12, C13, C14, D01, D02, D03, D04, D05, D06, D07, D08, D09, D10, D11, E01, E02, E03, F01, F02, F03, F04, F05, F06, F07, F08, F09, F10, G01, G02, G03, G04, G05, G06, H01, H02, H03, H04, H05, I01, I02, I03, I04, I05, I06, I07, I08, I09, I10, J01, J02, J03, J04, J05, J06, J07, J08, J09, J10, J11, K01, K02, K03, K04, L01, L02, L03, M01, M02
Select workTable, CheckNo, name, value
From
(	
	Select 'Check_Item_97' AS workTable, CheckNo, name, value
	FROM (
		Select CheckNo, A01, A02, A03, A04, A05, A06, A07, A08, A09, A10, A11, A12, B01, B02, B03, B04, B05, B06, B07, B08, B09, B10, C01, C02, C03, C04, C05, C06, C07, C08, C09, C10, C11, C12, C13, C14, D01, D02, D03, D04, D05, D06, D07, D08, D09, D10, D11, E01, E02, E03, F01, F02, F03, F04, F05, F06, F07, F08, F09, F10, G01, G02, G03, Convert(char(1), G04) AS G04, Convert(char(1), G05) AS G05, Convert(char(1), G06) AS G06, H01, H02, H03, H04, I01, J01, J02, J03, J04, J05, J06, J07, J08, J09, J10, J11, K01, K02, K03, K04, L01, L02, M01, M02
		From Check_Item_97
	) AS p
	UNPIVOT
	(
		value FOR name IN (A01, A02, A03, A04, A05, A06, A07, A08, A09, A10, A11, A12, B01, B02, B03, B04, B05, B06, B07, B08, B09, B10, C01, C02, C03, C04, C05, C06, C07, C08, C09, C10, C11, C12, C13, C14, D01, D02, D03, D04, D05, D06, D07, D08, D09, D10, D11, E01, E02, E03, F01, F02, F03, F04, F05, F06, F07, F08, F09, F10, G01, G02, G03, G04, G05, G06, H01, H02, H03, H04, I01, J01, J02, J03, J04, J05, J06, J07, J08, J09, J10, J11, K01, K02, K03, K04, L01, L02, M01, M02)
	) AS pv
	Union
	Select 'Check_Item_Fish' AS workTable, CheckNo, name, value
	FROM (
		Select CheckNo, A01, A02, B01, B02, C01, C02, C03, D01, D02, D03, E01, F01, F02, F03, F04, F05, G01, G02, G03, G04, H01, H02, H03
		From Check_Item_Fish
	) AS p
	UNPIVOT
	(
		value FOR name IN (A01, A02, B01, B02, C01, C02, C03, D01, D02, D03, E01, F01, F02, F03, F04, F05, G01, G02, G03, G04, H01, H02, H03)
	) AS pv
	Union
	Select 'Check_Item_Fish103' AS workTable, CheckNo, name, value
	FROM (
		Select CheckNo, A01, A02, B01, B02, C01, C02, C03, D01, D02, D03, F01, F02, F03, F04, F05, G01, G02, G03, G04, H01, H02, H03
		From Check_Item_Fish103
	) AS p
	UNPIVOT
	(
		value FOR name IN (A01, A02, B01, B02, C01, C02, C03, D01, D02, D03, F01, F02, F03, F04, F05, G01, G02, G03, G04, H01, H02, H03)
	) AS pv
	Union
	Select 'Check_Item_SelfDown' AS workTable, CheckNo, name, value
	FROM (
		Select CheckNo, A01, A02, A03, A04, B01, B02, B03, C01, C02, C03, C04, C05, C06, D01, D02, G01, H01, H02, H03, H04, H05
		From Check_Item_SelfDown
	) AS p
	UNPIVOT
	(
		value FOR name IN (A01, A02, A03, A04, B01, B02, B03, C01, C02, C03, C04, C05, C06, D01, D02, G01, H01, H02, H03, H04, H05)
	) AS pv
	Union
	Select 'Check_Item_SelfUP' AS workTable, CheckNo, name, value
	FROM (
		Select CheckNo, A01, A02, A03, A04, A05, A06, B01, B02, B03, C01, C02, C03, C04, C05, C06, C07, C08, D01, D02, G01, H01, H02, H03, H04, H05
		From Check_Item_SelfUP
	) AS p
	UNPIVOT
	(
		value FOR name IN (A01, A02, A03, A04, A05, A06, B01, B02, B03, C01, C02, C03, C04, C05, C06, C07, C08, D01, D02, G01, H01, H02, H03, H04, H05)
	) AS pv
)aaa
GO

--7.新增view(vw_UNPIVOT_Check_Item_For_Doesmeet)
CREATE View [dbo].[vw_UNPIVOT_Check_Item_For_Doesmeet] AS
--A_Doesmeet, B_Doesmeet, C_Doesmeet, D_Doesmeet, E_Doesmeet, F_Doesmeet, G_Doesmeet, H_Doesmeet, I_Doesmeet, J_Doesmeet, K_Doesmeet, L_Doesmeet, M_Doesmeet
Select 'Check_Item' AS workTable, CheckNo, CheckDate, name, value
FROM (
	Select a.CheckNo, b.CheckDate, A_Doesmeet, B_Doesmeet, C_Doesmeet, D_Doesmeet, E_Doesmeet, F_Doesmeet, G_Doesmeet, H_Doesmeet, I_Doesmeet, J_Doesmeet, K_Doesmeet, L_Doesmeet, M_Doesmeet
	From Check_Item　a
	Inner Join Check_Basic b on a.CheckNo = b.CheckNo
) AS p
UNPIVOT
(
	value FOR name IN (A_Doesmeet, B_Doesmeet, C_Doesmeet, D_Doesmeet, E_Doesmeet, F_Doesmeet, G_Doesmeet, H_Doesmeet, I_Doesmeet, J_Doesmeet, K_Doesmeet, L_Doesmeet, M_Doesmeet)
) AS pv
Union
Select 'Check_Item_97' AS workTable, CheckNo, CheckDate, name, value
FROM (
	Select a.CheckNo, b.CheckDate, A_Doesmeet, B_Doesmeet, C_Doesmeet, D_Doesmeet, E_Doesmeet, F_Doesmeet, G_Doesmeet, H_Doesmeet, I_Doesmeet, J_Doesmeet, K_Doesmeet, L_Doesmeet, M_Doesmeet
	From Check_Item_97 a
	Inner Join Check_Basic b on a.CheckNo = b.CheckNo
) AS p
UNPIVOT
(
	value FOR name IN (A_Doesmeet, B_Doesmeet, C_Doesmeet, D_Doesmeet, E_Doesmeet, F_Doesmeet, G_Doesmeet, H_Doesmeet, I_Doesmeet, J_Doesmeet, K_Doesmeet, L_Doesmeet, M_Doesmeet)
) AS pv
Union
Select 'Check_Item_Fish' AS workTable, CheckNo, CheckDate, name, value
FROM (
	Select a.CheckNo, b.CheckDate, A_Doesmeet, B_Doesmeet, C_Doesmeet, D_Doesmeet, E_Doesmeet, F_Doesmeet, G_Doesmeet, H_Doesmeet, 0 AS I_Doesmeet, 0 AS J_Doesmeet, 0 AS K_Doesmeet, 0 AS L_Doesmeet, 0 AS M_Doesmeet
	From Check_Item_Fish a
	Inner Join Check_Basic b on a.CheckNo = b.CheckNo
) AS p
UNPIVOT
(
	value FOR name IN (A_Doesmeet, B_Doesmeet, C_Doesmeet, D_Doesmeet, E_Doesmeet, F_Doesmeet, G_Doesmeet, H_Doesmeet, I_Doesmeet, J_Doesmeet, K_Doesmeet, L_Doesmeet, M_Doesmeet)
) AS pv
Union
Select 'Check_Item_Fish103' AS workTable, CheckNo, CheckDate, name, value
FROM (
	Select a.CheckNo, b.CheckDate, A_Doesmeet, B_Doesmeet, C_Doesmeet, D_Doesmeet, 0 AS E_Doesmeet, F_Doesmeet, G_Doesmeet, H_Doesmeet, 0 AS I_Doesmeet, 0 AS J_Doesmeet, 0 AS K_Doesmeet, 0 AS L_Doesmeet, 0 AS M_Doesmeet
	From Check_Item_Fish103 a
	Inner Join Check_Basic b on a.CheckNo = b.CheckNo
) AS p
UNPIVOT
(
	value FOR name IN (A_Doesmeet, B_Doesmeet, C_Doesmeet, D_Doesmeet, E_Doesmeet, F_Doesmeet, G_Doesmeet, H_Doesmeet, I_Doesmeet, J_Doesmeet, K_Doesmeet, L_Doesmeet, M_Doesmeet)
) AS pv
Union
Select 'Check_Item_SelfDown' AS workTable, CheckNo, CheckDate, name, value
FROM (
	Select a.CheckNo, b.CheckDate, A_Doesmeet, B_Doesmeet, C_Doesmeet, D_Doesmeet, 0 AS E_Doesmeet, 0 AS F_Doesmeet, G_Doesmeet, H_Doesmeet, 0 AS I_Doesmeet, 0 AS J_Doesmeet, 0 AS K_Doesmeet, 0 AS L_Doesmeet, 0 AS M_Doesmeet
	From Check_Item_SelfDown a
	Inner Join Check_Basic b on a.CheckNo = b.CheckNo
) AS p
UNPIVOT
(
	value FOR name IN (A_Doesmeet, B_Doesmeet, C_Doesmeet, D_Doesmeet, E_Doesmeet, F_Doesmeet, G_Doesmeet, H_Doesmeet, I_Doesmeet, J_Doesmeet, K_Doesmeet, L_Doesmeet, M_Doesmeet)
) AS pv
Union
Select 'Check_Item_SelfUP' AS workTable, CheckNo, CheckDate, name, value
FROM (
	Select a.CheckNo, b.CheckDate, A_Doesmeet, B_Doesmeet, C_Doesmeet, D_Doesmeet, 0 AS E_Doesmeet, 0 AS F_Doesmeet, G_Doesmeet, H_Doesmeet, 0 AS I_Doesmeet, 0 AS J_Doesmeet, 0 AS K_Doesmeet, 0 AS L_Doesmeet, 0 AS M_Doesmeet
	From Check_Item_SelfUP a
	Inner Join Check_Basic b on a.CheckNo = b.CheckNo
) AS p
UNPIVOT
(
	value FOR name IN (A_Doesmeet, B_Doesmeet, C_Doesmeet, D_Doesmeet, E_Doesmeet, F_Doesmeet, G_Doesmeet, H_Doesmeet, I_Doesmeet, J_Doesmeet, K_Doesmeet, L_Doesmeet, M_Doesmeet)
) AS pv
GO

----新增view(vw_UNION_Check_Item_For_AllDoesmeet)
Create View [dbo].[vw_UNION_Check_Item_For_AllDoesmeet] AS
Select workTable, CheckNo, AllDoesmeet
From
(
	Select 'Check_Item' AS workTable, CheckNo, AllDoesmeet
	From Check_Item
	Union
	Select 'Check_Item_97' AS workTable, CheckNo, AllDoesmeet
	From Check_Item_97
	Union
	Select 'Check_Item_Fish' AS workTable, CheckNo, AllDoesmeet
	From Check_Item_Fish
	Union
	Select 'Check_Item_Fish103' AS workTable, CheckNo, AllDoesmeet
	From Check_Item_Fish103
	Union
	Select 'Check_Item_SelfDown' AS workTable, CheckNo, AllDoesmeet
	From Check_Item_SelfDown
	Union
	Select 'Check_Item_SelfUP' AS workTable, CheckNo, AllDoesmeet
	From Check_Item_SelfUP
)aaa
GO