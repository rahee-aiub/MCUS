USE [A2ZCSMCUS]
GO
/****** Object:  Table [dbo].[WFCSMONTHLYBENEFITCREDIT]    Script Date: 10/27/2017 22:04:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WFCSMONTHLYBENEFITCREDIT](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CuType] [tinyint] NULL,
	[CuNo] [int] NULL,
	[MemNo] [int] NULL,
	[AccType] [tinyint] NULL,
	[AccNo] [bigint] NULL,
	[TrnCode] [int] NULL,
	[TrnDate] [smalldatetime] NULL,
	[VoucherNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccMthBenefitAmt] [money] NULL,
	[NoMonths] [int] NULL,
	[AccTotalBenefitAmt] [money] NULL,
	[AccAdjProvBalance] [money] NULL,
	[FuncOpt] [smallint] NULL,
	[PayType] [smallint] NULL,
	[TrnDesc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TrnType] [tinyint] NULL,
	[TrnDrCr] [tinyint] NULL,
	[ShowInterest] [tinyint] NULL,
	[TrnGLAccNoDr] [int] NULL,
	[TrnGLAccNoCr] [int] NULL,
	[CuNumber] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MemName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TrnUpdate] [tinyint] NULL CONSTRAINT [DF_WFCSMONTHLYBENEFITCREDIT_TrnUpdate]  DEFAULT ((0)),
	[ProcStat] [tinyint] NULL,
	[UserId] [smallint] NULL CONSTRAINT [DF_WFCSMONTHLYBENEFITCREDIT_UserId]  DEFAULT ((0)),
	[AccCorrAccType] [int] NULL CONSTRAINT [DF_WFCSMONTHLYBENEFITCREDIT_AccCorrAccType]  DEFAULT ((0)),
	[AccCorrAccNo] [bigint] NULL CONSTRAINT [DF_WFCSMONTHLYBENEFITCREDIT_AccCorrAccNo]  DEFAULT ((0)),
	[CreateDate] [datetime] NULL CONSTRAINT [DF_WFCSMONTHLYBENEFITCREDIT_CreateDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_WFCSMONTHLYBENEFITCREDIT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
