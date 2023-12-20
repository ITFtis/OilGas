CREATE TABLE [dbo].[FileDownload](
	[UUID] [uniqueidentifier] NOT NULL,
	[File_name] [nvarchar](200) NOT NULL,
	[type] [int] NOT NULL,
	[Detail] [nvarchar](500) NULL,
	[AddMemberID] [nvarchar](200) NULL,
	[AddTime] [datetime] NULL,
	[UpdateMemberID] [nvarchar](200) NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_FileDownload] PRIMARY KEY CLUSTERED 
(
	[UUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

GO
CREATE TABLE [dbo].[FileType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[type] [nvarchar](50) NOT NULL,
	[Remark] [nvarchar](200) NULL,
 CONSTRAINT [PK_FileType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO







ALTER TABLE [CarFuel_BasicData]
ALTER COLUMN [SoilServerData] NVARCHAR(50);
GO

ALTER TABLE CarFuel_OilData
ALTER COLUMN SaleSoilClass NVARCHAR(50);
GO
UPDATE [dbo].CarFuel_OilData
   SET SaleSoilClass =REPLACE(SaleSoilClass,' ','')
   GO



ALTER TABLE FishGas_BasicData
ALTER COLUMN Oil_barge NVARCHAR(50);
GO
UPDATE [dbo].FishGas_BasicData
   SET Oil_barge =REPLACE(Oil_barge,' ','')
   GO

ALTER TABLE FishGas_BasicData
ALTER COLUMN SoilServerData NVARCHAR(50);
GO
UPDATE [dbo].FishGas_BasicData
   SET SoilServerData =REPLACE(SoilServerData,' ','')
   GO

ALTER TABLE FishGas_OilData
ALTER COLUMN Tank_type NVARCHAR(50);
GO
UPDATE [dbo].FishGas_OilData
   SET Tank_type =REPLACE(Tank_type,' ','')
   GO

ALTER TABLE FishGas_OilData
ALTER COLUMN Oil_type NVARCHAR(50);
GO
UPDATE [dbo].FishGas_OilData
   SET Oil_type =REPLACE(Oil_type,' ','')
   GO


ALTER TABLE Check_Item_97
ALTER COLUMN CheckNo NVARCHAR(50);
GO
UPDATE [dbo].Check_Item_97
   SET CheckNo =REPLACE(CheckNo,' ','')
   GO

ALTER TABLE Check_Basic
ALTER COLUMN CheckNo NVARCHAR(50);
GO
UPDATE [dbo].Check_Basic
   SET CheckNo =REPLACE(CheckNo,' ','')
   GO

ALTER TABLE [Check_Item]
ALTER COLUMN CheckNo NVARCHAR(50);
GO
UPDATE [dbo].[Check_Item]
   SET CheckNo =REPLACE(CheckNo,' ','')
   GO

ALTER TABLE Check_Item_Action
ALTER COLUMN CheckNo NVARCHAR(50);
GO
UPDATE [dbo].Check_Item_Action
   SET CheckNo =REPLACE(CheckNo,' ','')
   GO

ALTER TABLE Check_Basic_Action
ALTER COLUMN CheckNo NVARCHAR(50);
GO
UPDATE [dbo].Check_Basic_Action
   SET CheckNo =REPLACE(CheckNo,' ','')
   GO








ALTER TABLE Check_Basic
ALTER COLUMN CaseNo NVARCHAR(50);
GO
UPDATE [dbo].Check_Basic
   SET CaseNo =REPLACE(CaseNo,' ','')
   GO




ALTER TABLE Check_Counseling
ALTER COLUMN CaseNo NVARCHAR(50);
GO
UPDATE [dbo].Check_Counseling
   SET CaseNo =REPLACE(CaseNo,' ','')
   GO

ALTER TABLE Check_Item_Action
ALTER COLUMN CaseNo NVARCHAR(50);
GO
UPDATE [dbo].Check_Item_Action
   SET CaseNo =REPLACE(CaseNo,' ','')
   GO

ALTER TABLE [Check_Basic_Action]
ALTER COLUMN CaseNo NVARCHAR(50);
GO
UPDATE [dbo].[Check_Basic_Action]
   SET CaseNo =REPLACE(CaseNo,' ','')
   GO

