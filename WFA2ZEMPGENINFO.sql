USE [A2ZHRMCUS]
GO

/****** Object:  Table [dbo].[WFA2ZEMPGENINFO]    Script Date: 02/11/2019 1:28:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WFA2ZEMPGENINFO](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmpCode] [int] NULL,
	[EmpName] [nvarchar](50) NULL,
	[EmpBaseGrade] [smallint] NULL,
	[EmpGrade] [int] NULL,
	[EmpGradeDesc] [nvarchar](50) NULL,
	[EmpDesignation] [smallint] NULL,
	[EmpDesigDesc] [nvarchar](50) NULL,
	[EmpServiceType] [smallint] NULL,
	[EmpSTypeDesc] [nvarchar](50) NULL,
	[EmpProject] [smallint] NULL,
	[EmpProjectDesc] [nvarchar](50) NULL,
	[EmpJoinDate] [smalldatetime] NULL,
	[EmpPerDate] [smalldatetime] NULL,
	[EmpSection] [smallint] NULL,
	[EmpSectionDesc] [nvarchar](50) NULL,
	[EmpDepartment] [smallint] NULL,
	[EmpDepartmentDesc] [nvarchar](50) NULL,
	[EmpArea] [int] NULL,
	[EmpAreaDesc] [nvarchar](50) NULL,
	[EmpLocation] [int] NULL,
	[EmpLocationDesc] [nvarchar](50) NULL,
	[EmpPayLabel] [int] NULL,
	[EmpRelagion] [smallint] NULL,
	[EmpRelagionDesc] [nvarchar](50) NULL,
	[EmpGender] [smallint] NULL,
	[Status] [smallint] NULL,
 CONSTRAINT [PK_WFA2ZEMPGENINFO] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[WFA2ZEMPGENINFO] ADD  CONSTRAINT [DF_WFA2ZEMPGENINFO_EmpNo]  DEFAULT ((0)) FOR [EmpCode]
GO

ALTER TABLE [dbo].[WFA2ZEMPGENINFO] ADD  CONSTRAINT [DF_WFA2ZEMPGENINFO_EmpGrade]  DEFAULT ((0)) FOR [EmpGrade]
GO

ALTER TABLE [dbo].[WFA2ZEMPGENINFO] ADD  CONSTRAINT [DF_WFA2ZEMPGENINFO_EmpDesignation]  DEFAULT ((0)) FOR [EmpDesignation]
GO

ALTER TABLE [dbo].[WFA2ZEMPGENINFO] ADD  CONSTRAINT [DF_WFA2ZEMPGENINFO_EmpServiceType]  DEFAULT ((0)) FOR [EmpServiceType]
GO

ALTER TABLE [dbo].[WFA2ZEMPGENINFO] ADD  CONSTRAINT [DF_WFA2ZEMPGENINFO_EmpProject_1]  DEFAULT ((0)) FOR [EmpProject]
GO

ALTER TABLE [dbo].[WFA2ZEMPGENINFO] ADD  CONSTRAINT [DF_WFA2ZEMPGENINFO_EmpSection]  DEFAULT ((0)) FOR [EmpSection]
GO

ALTER TABLE [dbo].[WFA2ZEMPGENINFO] ADD  CONSTRAINT [DF_WFA2ZEMPGENINFO_EmpDepartment]  DEFAULT ((0)) FOR [EmpDepartment]
GO

ALTER TABLE [dbo].[WFA2ZEMPGENINFO] ADD  CONSTRAINT [DF_WFA2ZEMPGENINFO_EmpArea_1]  DEFAULT ((0)) FOR [EmpArea]
GO

ALTER TABLE [dbo].[WFA2ZEMPGENINFO] ADD  CONSTRAINT [DF_WFA2ZEMPGENINFO_EmpLocation]  DEFAULT ((0)) FOR [EmpLocation]
GO

