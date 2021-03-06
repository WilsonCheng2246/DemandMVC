USE [dbDemand]
GO
/****** Object:  Table [dbo].[AccountDetail]    Script Date: 2018/11/29 上午 06:46:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountDetail](
	[AccIndex] [nvarchar](5) NOT NULL,
	[AccNo] [nvarchar](15) NOT NULL,
	[AccName] [nvarchar](20) NOT NULL,
	[AccClass] [nvarchar](1) NOT NULL,
	[AccDeptNo] [nvarchar](3) NOT NULL,
	[AccJobNo] [nvarchar](3) NOT NULL,
	[AccMobile] [nvarchar](20) NULL,
	[AccPhone] [nvarchar](20) NULL,
	[AccEmail] [nvarchar](50) NULL,
	[AccPassword] [nvarchar](50) NULL,
	[AccNotation] [nvarchar](max) NULL,
	[AccImage] [nvarchar](1) NULL,
	[AccDateS] [nvarchar](20) NULL,
	[AccDateE] [nvarchar](20) NULL,
	[AccStatus] [nvarchar](1) NULL,
	[AccNotationS] [nvarchar](max) NULL,
 CONSTRAINT [PK_AccountDetail] PRIMARY KEY CLUSTERED 
(
	[AccIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AccountRelation]    Script Date: 2018/11/29 上午 06:46:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountRelation](
	[AccIndex] [nvarchar](5) NOT NULL,
	[AccDeptNo] [nvarchar](2) NOT NULL,
	[RelationClass] [nvarchar](1) NOT NULL,
	[RelationAccIndex] [nvarchar](5) NOT NULL,
 CONSTRAINT [PK_AccountRelation_1] PRIMARY KEY CLUSTERED 
(
	[AccIndex] ASC,
	[AccDeptNo] ASC,
	[RelationClass] ASC,
	[RelationAccIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DemandDetail]    Script Date: 2018/11/29 上午 06:46:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DemandDetail](
	[DemandIndex] [nvarchar](20) NOT NULL,
	[DemandDate] [nvarchar](20) NOT NULL,
	[DemandTitle] [nvarchar](500) NOT NULL,
	[DemandClass] [nvarchar](2) NOT NULL,
	[DemandTest] [nvarchar](1) NULL,
	[DemandStep] [nvarchar](2) NULL,
	[DemandFrom] [nvarchar](500) NULL,
	[DemandProject] [nvarchar](500) NULL,
	[DemandDateS] [nvarchar](20) NULL,
	[DemandDateE] [nvarchar](20) NULL,
	[DemandNotation] [nvarchar](max) NULL,
	[DemandRemark] [nvarchar](max) NULL,
	[DemandStatus] [nvarchar](2) NULL,
	[Update_DateTime] [datetime] NULL,
 CONSTRAINT [PK_DemandDetail] PRIMARY KEY CLUSTERED 
(
	[DemandIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DemandSchedule]    Script Date: 2018/11/29 上午 06:46:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DemandSchedule](
	[DemandIndex] [nvarchar](20) NOT NULL,
	[DemandStep] [nvarchar](1) NOT NULL,
	[SchAccIndex] [nvarchar](10) NULL,
	[SchNotation] [nvarchar](200) NULL,
	[SchDateTime] [datetime] NULL,
	[SchStatus] [nvarchar](1) NULL,
 CONSTRAINT [PK_DemandSchedule_1] PRIMARY KEY CLUSTERED 
(
	[DemandIndex] ASC,
	[DemandStep] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DemandUploadFile]    Script Date: 2018/11/29 上午 06:46:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DemandUploadFile](
	[DemandIndex] [nvarchar](20) NOT NULL,
	[FileIndex] [nvarchar](2) NOT NULL,
	[FileClass] [nvarchar](1) NOT NULL,
	[FileName] [nvarchar](50) NULL,
	[FileDate] [nvarchar](20) NULL,
	[FileNotation] [nvarchar](500) NULL,
	[FileSort] [nvarchar](2) NULL,
	[FileStatus] [nvarchar](1) NULL,
	[FileNameExt] [nvarchar](10) NULL,
 CONSTRAINT [PK_DemandUploadFile] PRIMARY KEY CLUSTERED 
(
	[DemandIndex] ASC,
	[FileIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SystemDataDetail]    Script Date: 2018/11/29 上午 06:46:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemDataDetail](
	[SystemClass] [nvarchar](50) NOT NULL,
	[SystemValue] [nvarchar](50) NOT NULL,
	[SystemTitle] [nvarchar](500) NOT NULL,
	[SystemNotation] [nvarchar](500) NULL,
	[SystemRemark] [nvarchar](500) NULL,
	[SystemStatus] [nvarchar](1) NULL,
 CONSTRAINT [PK_SystemDataDetail] PRIMARY KEY CLUSTERED 
(
	[SystemClass] ASC,
	[SystemValue] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SystemMessageDetail]    Script Date: 2018/11/29 上午 06:46:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemMessageDetail](
	[MessIndex] [nvarchar](20) NOT NULL,
	[MessClass] [nvarchar](2) NOT NULL,
	[MessDateTime] [nvarchar](20) NOT NULL,
	[MessTitle] [nvarchar](200) NOT NULL,
	[MessNotation] [nvarchar](max) NULL,
	[MessStatus] [nvarchar](1) NULL,
 CONSTRAINT [PK_SystemMessageDetail] PRIMARY KEY CLUSTERED 
(
	[MessIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[AccountDetail] ([AccIndex], [AccNo], [AccName], [AccClass], [AccDeptNo], [AccJobNo], [AccMobile], [AccPhone], [AccEmail], [AccPassword], [AccNotation], [AccImage], [AccDateS], [AccDateE], [AccStatus], [AccNotationS]) VALUES (N'00001', N'Wilson', N'Wilson', N'A', N'A09', N'A', N'0955944805', N'0955944805', N'wilson@email.com', N'wilson', N'', N'', N'2018/10/15', N'', N'O', N'')
INSERT [dbo].[AccountDetail] ([AccIndex], [AccNo], [AccName], [AccClass], [AccDeptNo], [AccJobNo], [AccMobile], [AccPhone], [AccEmail], [AccPassword], [AccNotation], [AccImage], [AccDateS], [AccDateE], [AccStatus], [AccNotationS]) VALUES (N'00002', N'Cheng', N'程嘉偉', N'B', N'B01', N'B', N'0955944805', N'0955944805', N'wilsoncheng@gmail.com', N'0955944805', N'', N'', N'2018/11/22', N'', N'O', N'')
INSERT [dbo].[AccountDetail] ([AccIndex], [AccNo], [AccName], [AccClass], [AccDeptNo], [AccJobNo], [AccMobile], [AccPhone], [AccEmail], [AccPassword], [AccNotation], [AccImage], [AccDateS], [AccDateE], [AccStatus], [AccNotationS]) VALUES (N'00003', N'tina', N'tina chang', N'A', N'A03', N'D', N'0955944805', N'0955944805', N'wilsoncheng@gmail.com', N'0955944805', N'', N'', N'2018/11/25', N'', N'O', N'')
INSERT [dbo].[AccountDetail] ([AccIndex], [AccNo], [AccName], [AccClass], [AccDeptNo], [AccJobNo], [AccMobile], [AccPhone], [AccEmail], [AccPassword], [AccNotation], [AccImage], [AccDateS], [AccDateE], [AccStatus], [AccNotationS]) VALUES (N'00004', N'Brian', N'Brian Chang', N'B', N'A03', N'B', N'0955944805', N'0955944805', N'wilsoncheng@gmail.com', N'0955944805', N'', N'', N'2018/11/25', N'', N'O', N'')
INSERT [dbo].[AccountDetail] ([AccIndex], [AccNo], [AccName], [AccClass], [AccDeptNo], [AccJobNo], [AccMobile], [AccPhone], [AccEmail], [AccPassword], [AccNotation], [AccImage], [AccDateS], [AccDateE], [AccStatus], [AccNotationS]) VALUES (N'00005', N'Chen', N'BrianChen', N'B', N'B01', N'A', N'0955944805', N'0955944805', N'wilsoncheng@gmail.com', N'0955944805', N'', N'', N'2018/11/25', N'', N'O', N'')
INSERT [dbo].[AccountDetail] ([AccIndex], [AccNo], [AccName], [AccClass], [AccDeptNo], [AccJobNo], [AccMobile], [AccPhone], [AccEmail], [AccPassword], [AccNotation], [AccImage], [AccDateS], [AccDateE], [AccStatus], [AccNotationS]) VALUES (N'00006', N'Michelle', N'Michelle Li', N'B', N'B01', N'C', N'0955944805', N'0955944805', N'wilsoncheng@gmail.com', N'0955944805', N'', N'', N'2018/11/25', N'', N'O', N'')
INSERT [dbo].[AccountDetail] ([AccIndex], [AccNo], [AccName], [AccClass], [AccDeptNo], [AccJobNo], [AccMobile], [AccPhone], [AccEmail], [AccPassword], [AccNotation], [AccImage], [AccDateS], [AccDateE], [AccStatus], [AccNotationS]) VALUES (N'00007', N'Jeffiner', N'Jeffinner Hsish', N'B', N'B01', N'A', N'0955944805', N'0955944805', N'wilsoncheng@gmail.com', N'0955944805', N'', N'', N'2018/11/25', N'', N'O', N'')
INSERT [dbo].[AccountDetail] ([AccIndex], [AccNo], [AccName], [AccClass], [AccDeptNo], [AccJobNo], [AccMobile], [AccPhone], [AccEmail], [AccPassword], [AccNotation], [AccImage], [AccDateS], [AccDateE], [AccStatus], [AccNotationS]) VALUES (N'00008', N'Jeffery', N'Jeffery Chen', N'B', N'B01', N'G', N'0955944805', N'0955944805', N'wilsoncheng@gmail.com', N'0955944805', N'', N'', N'2018/11/25', N'', N'O', N'')
INSERT [dbo].[AccountDetail] ([AccIndex], [AccNo], [AccName], [AccClass], [AccDeptNo], [AccJobNo], [AccMobile], [AccPhone], [AccEmail], [AccPassword], [AccNotation], [AccImage], [AccDateS], [AccDateE], [AccStatus], [AccNotationS]) VALUES (N'00009', N'Alice', N'Alice Wu', N'B', N'B01', N'H', N'0955944805', N'0955944805', N'wilsoncheng@gmail.com', N'0955944805', N'', N'', N'2018/11/25', N'', N'O', N'')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccClass', N'A', N'需求廠商', N'廠商類別', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccClass', N'B', N'開發團隊', N'廠商類別', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccClass', N'C', N'開發廠商', N'廠商類別', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccClass', N'DEV', N'廠商所屬類別', N'廠商所屬類別', N'AccountDetail', N'D')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'A01', N'陽明大學教務處', N'A', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'A02', N'陽明大學國際事務處', N'A', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'A03', N'東南旅行社會計部', N'A', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'A04', N'東南旅行社票務部', N'A', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'A05', N'五福旅行社票務部', N'A', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'A06', N'永豐銀行消金部', N'A', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'A07', N'兆豐銀行消金部', N'A', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'A08', N'中信銀行消金部', N'A', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'A09', N'百威旅行社', N'A', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'A10', N'台灣大學教務處', N'A', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'A11', N'台灣大學學務處', N'A', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'B01', N'資訊部', N'B', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'B02', N'會計部', N'B', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'B03', N'票務部', N'B', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'B04', N'營業部', N'B', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'B05', N'消金部', N'B', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'B06', N'人資處', N'B', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'B07', N'財務部', N'B', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'B08', N'台北分公司', N'B', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'B09', N'台中分公司', N'B', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'B10', N'高雄分公司', N'B', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'C01', N'永威資訊', N'C', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'C02', N'力威資訊', N'C', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'C03', N'科威資訊', N'C', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'C04', N'敦陽科技', N'C', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'C05', N'凌群電腦', N'C', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccDeptNo', N'DEV', N'帳號所屬部門', N'帳號所屬部門', N'AccountDetail', N'D')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccJobNo', N'A', N'工程師', N'帳號類別', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccJobNo', N'B', N'系統分析師', N'帳號類別', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccJobNo', N'C', N'專案經理', N'帳號類別', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccJobNo', N'D', N'專案助理', N'帳號類別', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccJobNo', N'DEV', N'帳號工作職稱', N'帳號工作職稱', N'AccountDetail', N'D')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccJobNo', N'E', N'助理', N'帳號類別', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccJobNo', N'F', N'專員', N'帳號類別', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccJobNo', N'G', N'副理', N'帳號類別', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccJobNo', N'H', N'經理', N'帳號類別', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccJobNo', N'I', N'副總經理', N'帳號類別', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'AccJobNo', N'J', N'總經理', N'帳號類別', N'AccountDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandClass', N'A', N'完整專案', N'', N'DemandDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandClass', N'B', N'功能新增', N'', N'DemandDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandClass', N'C', N'功能調整', N'', N'DemandDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandClass', N'D', N'程式除錯', N'', N'DemandDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandClass', N'DEV', N'專案申請類別', N'專案申請類別', N'DemandDetail', N'D')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandClass', N'E', N'權限設定', N'', N'DemandDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandClass', N'F', N'資料異動', N'', N'DemandDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStatus', N'DEV', N'專案進度類別', N'專案進度類別', N'DemandDetail', N'D')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStatus', N'O', N'已結案', N'', N'DemandDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStatus', N'X', N'進行中', N'', N'DemandDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'A', N'需求申請中', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'B', N'需求申請完成', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'C', N'需求申請核准', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'D', N'需求申請確認', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'DEV', N'需求單處理流程類別', N'需求單處理流程類別', N'DemandSchedule', N'D')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'E', N'開發單位受理', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'F', N'開發單位受理中', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'G', N'需求開發中', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'H', N'需求開發完成', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'I', N'需求單位測試', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'J', N'需求單位測試完成', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'K', N'需求單位測試未過', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'L', N'測試完成確認', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'M', N'需求單完成', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'N', N'需求單完成核准', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'O', N'需求單完成確認', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'P', N'需求單完成結案', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'Q', N'需求單退回(需求單位)', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'R', N'需求單退回核准(需求單位)', N'', N'DemandSchedule and DemandDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'S', N'需求單退回確認(需求單位)', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'T', N'需求單退回(開發單位)', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'U', N'需求單退回核准(開發單位)', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'DemandStep', N'V', N'需求單退回確認(開發單位)', N'', N'DemandSchedule', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'FileClass', N'A', N'需求單文件', N'', N'DemandUploadFile', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'FileClass', N'B', N'需求單圖片', N'', N'DemandUploadFile', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'FileClass', N'C', N'需求端測試文件', N'', N'DemandUploadFile', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'FileClass', N'D', N'需求端測試圖片', N'', N'DemandUploadFile', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'FileClass', N'DEV', N'專案文件上傳類別', N'專案文件上傳類別', N'DemandUploadFile', N'D')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'FileClass', N'E', N'開發端系統文件', N'', N'DemandUploadFile', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'FileClass', N'F', N'開發端測試圖片', N'', N'DemandUploadFile', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'FileClass', N'G', N'開發端差異比對', N'', N'DemandUploadFile', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'MessClass', N'AA', N'新增帳號基本資料', N'', N'SystemMessageDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'MessClass', N'AB', N'修改帳號基本資料', N'', N'SystemMessageDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'MessClass', N'AC', N'刪除帳號基本資料', N'', N'SystemMessageDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'MessClass', N'AD', N'匯入帳號基本資料', N'', N'SystemMessageDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'MessClass', N'AE', N'匯出帳號基本資料', N'', N'SystemMessageDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'MessClass', N'AF', N'停用帳號狀態', N'', N'SystemMessageDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'MessClass', N'AG', N'啟用帳號狀態', N'', N'SystemMessageDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'MessClass', N'BA', N'新增系統參數資料', N'', N'SystemMessageDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'MessClass', N'BB', N'修改系統參數資料', N'', N'SystemMessageDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'MessClass', N'BC', N'刪除系統參數資料', N'', N'SystemMessageDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'MessClass', N'BD', N'匯入系統參數資料', N'', N'SystemMessageDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'MessClass', N'BE', N'匯出系統參數資料', N'', N'SystemMessageDetail', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'MessClass', N'DEV', N'系統LOG紀錄類別', N'系統LOG紀錄類別', N'SystemMessageDetail', N'D')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'RelationClass', N'A', N'一般帳號', N'', N'AccountRelation', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'RelationClass', N'B', N'上級主管', N'', N'AccountRelation', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'RelationClass', N'C', N'二級主管', N'', N'AccountRelation', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'RelationClass', N'D', N'一級主管', N'', N'AccountRelation', N'O')
GO
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'RelationClass', N'DEV', N'帳號關聯類別', N'帳號關聯類別', N'AccountRelation', N'D')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'RelationClass', N'E', N'負責窗口', N'', N'AccountRelation', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'RelationClass', N'F', N'代理人', N'', N'AccountRelation', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'RelationClass', N'G', N'專案經理', N'', N'AccountRelation
', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'RelationClass', N'H', N'系統分析師', N'', N'AccountRelation', N'O')
INSERT [dbo].[SystemDataDetail] ([SystemClass], [SystemValue], [SystemTitle], [SystemNotation], [SystemRemark], [SystemStatus]) VALUES (N'RelationClass', N'I', N'開發工程師', N'', N'AccountRelation', N'O')
