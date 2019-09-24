USE [A2ZHRMCUS]
GO

/****** Object:  Table [dbo].[WFBankAdvise]    Script Date: 02/13/2019 9:51:32 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WFBankAdvise](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmpBank] [smallint] NULL,
	[EmpNo] [int] NULL,
	[EmpName] [nvarchar](50) NULL,
	[EmpAccNo] [nvarchar](50) NULL,
	[NetPayment] [money] NULL,
	[EmpArea] [int] NULL,
	[EmpAreaDesc] [nvarchar](50) NULL,
	[EmpLocation] [int] NULL,
	[EmpLocationDesc] [nvarchar](50) NULL,
 CONSTRAINT [PK_WFBankAdvise] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[WFBankAdvise] ADD  CONSTRAINT [DF_WFBankAdvise_EmpNo]  DEFAULT ((0)) FOR [EmpNo]
GO

ALTER TABLE [dbo].[WFBankAdvise] ADD  CONSTRAINT [DF_WFBankAdvise_Basic]  DEFAULT ((0)) FOR [NetPayment]
GO

ALTER TABLE [dbo].[WFBankAdvise] ADD  CONSTRAINT [DF_WFBankAdvise_EmpArea]  DEFAULT ((0)) FOR [EmpArea]
GO

ALTER TABLE [dbo].[WFBankAdvise] ADD  CONSTRAINT [DF_WFBankAdvise_EmpLocation]  DEFAULT ((0)) FOR [EmpLocation]
GO

