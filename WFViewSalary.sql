USE [A2ZHRMCUS]
GO
/****** Object:  Table [dbo].[WFViewSalary]    Script Date: 03/24/2017 22:22:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WFViewSalary](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GLCode] [int] NULL CONSTRAINT [DF_WFViewSalary_EmpNo]  DEFAULT ((0)),
	[GLDesc] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GLDrCr] [tinyint] NULL,
	[GLAmount] [money] NULL,
 CONSTRAINT [PK_WFViewSalary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
