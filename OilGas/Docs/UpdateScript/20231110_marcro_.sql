CREATE TABLE [dbo].[Lesson](
	[LessonID] [uniqueidentifier] NOT NULL,
	[ClassName] [nvarchar](50) NULL,
	[detail] [nvarchar](500) NULL,
	[NoNullcolumn] [nvarchar](max) NULL,
	[time] [datetime] NULL,
 CONSTRAINT [PK_class] PRIMARY KEY CLUSTERED 
(
	[LessonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sign]    Script Date: 2023/11/10 上午 09:32:18 ******/

CREATE TABLE [dbo].[Sign](
	[SignId] [uniqueidentifier] NOT NULL,
	[LessonID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](30) NULL,
	[IdentityId] [char](10) NULL,
	[Tel] [varchar](20) NULL,
	[Mobile] [char](10) NULL,
	[Email] [varchar](50) NULL,
	[birth] [varchar](15) NULL,
	[gender] [int] NULL,
	[address] [nvarchar](200) NULL,
	[Occupation] [nvarchar](50) NULL,
	[Hobbies] [nvarchar](200) NULL,
 CONSTRAINT [PK_Sign] PRIMARY KEY CLUSTERED 
(
	[SignId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
