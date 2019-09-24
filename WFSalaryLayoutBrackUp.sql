USE [A2ZHRMCUS]
GO
/****** Object:  Table [dbo].[WFSalaryLayoutBrackUp]    Script Date: 03/19/2017 10:50:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WFSalaryLayoutBrackUp](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmpNo] [int] NULL CONSTRAINT [DF_WFSalaryLayoutBrackUp_EmpNo]  DEFAULT ((0)),
	[EmpName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmpDesigDesc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmpGrade] [int] NULL,
	[EmpPayLabel] [int] NULL,
	[Code1] [int] NULL CONSTRAINT [DF_WFSalaryLayoutBrackUp_Code1]  DEFAULT ((0)),
	[Code1Desc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Amount1] [money] NULL CONSTRAINT [DF_WFSalaryLayoutBrackUp_Allowance1]  DEFAULT ((0)),
	[Code2] [int] NULL CONSTRAINT [DF_WFSalaryLayoutBrackUp_Code2]  DEFAULT ((0)),
	[Code2Desc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Amount2] [money] NULL CONSTRAINT [DF_WFSalaryLayoutBrackUp_Amount11]  DEFAULT ((0)),
	[Code3] [int] NULL CONSTRAINT [DF_WFSalaryLayoutBrackUp_Code3]  DEFAULT ((0)),
	[Code3Desc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Amount3] [money] NULL CONSTRAINT [DF_WFSalaryLayoutBrackUp_Amount12]  DEFAULT ((0)),
	[Code4] [int] NULL CONSTRAINT [DF_WFSalaryLayoutBrackUp_Code4]  DEFAULT ((0)),
	[Code4Desc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Amount4] [money] NULL CONSTRAINT [DF_WFSalaryLayoutBrackUp_Amount13]  DEFAULT ((0)),
	[Code5] [int] NULL CONSTRAINT [DF_WFSalaryLayoutBrackUp_Code5]  DEFAULT ((0)),
	[Code5Desc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Amount5] [money] NULL CONSTRAINT [DF_WFSalaryLayoutBrackUp_Amount41]  DEFAULT ((0)),
	[Code6] [int] NULL CONSTRAINT [DF_WFSalaryLayoutBrackUp_Code51]  DEFAULT ((0)),
	[Code6Desc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Amount6] [money] NULL CONSTRAINT [DF_WFSalaryLayoutBrackUp_Amount51]  DEFAULT ((0)),
 CONSTRAINT [PK_WFSalaryLayoutBrackUp] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
