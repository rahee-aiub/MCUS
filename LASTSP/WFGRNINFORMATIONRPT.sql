USE [A2ZCSMCUS]
GO

/****** Object:  Table [dbo].[WFGRNINFORMATIONRPT]    Script Date: 30/01/2018 10:16:30 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WFGRNINFORMATIONRPT](
	[CuType] [smallint] NULL,
	[CuNo] [int] NULL,
	[CuName] [nvarchar](50) NULL,
	[AccType] [smallint] NULL,
	[AccTypeDescription] [nvarchar](50) NULL,
	[MemNo] [int] NULL,
	[MemName] [nvarchar](50) NULL,
	[AccNo] [bigint] NULL,
	[AccStatus] [tinyint] NULL,
	[AccBalance] [money] NULL,
	[AccLoanSancAmt] [money] NULL,
	[GurLoanApplicationNo] [int] NULL,
	[GurAccAmount] [money] NULL,
	[GurAccNo] [nvarchar](50) NULL,
	[GurAccLeanAmount] [money] NULL,
	[SortFlag] [int] NULL,
	[LoanApplicationNo] [int] NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[WFGRNINFORMATIONRPT] ADD  CONSTRAINT [DF_WFGRNINFORMATIONRPT_AccLoanSancAmt]  DEFAULT ((0)) FOR [AccLoanSancAmt]
GO

ALTER TABLE [dbo].[WFGRNINFORMATIONRPT] ADD  CONSTRAINT [DF_WFGRNINFORMATIONRPT_LoanApplicationNo]  DEFAULT ((0)) FOR [GurLoanApplicationNo]
GO

ALTER TABLE [dbo].[WFGRNINFORMATIONRPT] ADD  CONSTRAINT [DF_WFGRNINFORMATIONRPT_AccAmount]  DEFAULT ((0)) FOR [GurAccAmount]
GO

ALTER TABLE [dbo].[WFGRNINFORMATIONRPT] ADD  CONSTRAINT [DF_WFGRNINFORMATIONRPT_AccNo1]  DEFAULT ((0)) FOR [GurAccNo]
GO

ALTER TABLE [dbo].[WFGRNINFORMATIONRPT] ADD  CONSTRAINT [DF_WFGRNINFORMATIONRPT_GurAccAmount1]  DEFAULT ((0)) FOR [GurAccLeanAmount]
GO

