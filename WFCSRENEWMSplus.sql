USE [A2ZCSMCUS]
GO
/****** Object:  Table [dbo].[WFCSRENEWMSplus]    Script Date: 05/16/2017 16:26:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WFCSRENEWMSplus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CuType] [tinyint] NULL,
	[CuNo] [int] NULL,
	[MemNo] [int] NULL,
	[AccType] [tinyint] NULL,
	[AccNo] [bigint] NULL,
	[TrnCode] [int] NULL,
	[TrnDate] [smalldatetime] NULL,
	[VoucherNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccBalance] [money] NULL CONSTRAINT [DF_WFCSRENEWMSplus_AccBalance]  DEFAULT ((0)),
	[AccFixedAmt] [money] NULL CONSTRAINT [DF_WFCSRENEWMSplus_AccOrgAmt]  DEFAULT ((0)),
	[AccPrincipal] [money] NULL CONSTRAINT [DF_WFCSRENEWMSplus_AccPrincipal]  DEFAULT ((0)),
	[AccFixedMthInt] [money] NULL,
	[AccIntRate] [smallmoney] NULL CONSTRAINT [DF_WFCSRENEWMSplus_AccIntRate]  DEFAULT ((0)),
	[AccOpenDate] [smalldatetime] NULL,
	[AccRenwlDate] [smalldatetime] NULL,
	[AccAnniDate] [smalldatetime] NULL,
	[AccRenwlAmt] [money] NULL CONSTRAINT [DF_WFCSRENEWMSplus_AccRenwlAmt]  DEFAULT ((0)),
	[AccPeriodMonths] [smallint] NULL CONSTRAINT [DF_WFCSRENEWMSplus_AccPeriodMonths]  DEFAULT ((0)),
	[AccMatureDate] [smalldatetime] NULL,
	[AccNoAnni] [smallint] NULL CONSTRAINT [DF_WFCSRENEWMSplus_AccNoAnni]  DEFAULT ((0)),
	[AccNoRenwl] [smallint] NULL CONSTRAINT [DF_WFCSRENEWMSplus_AccNoRenwl]  DEFAULT ((0)),
	[CalAdjProvCr] [money] NULL,
	[CalAdjProvDr] [money] NULL,
	[CalInterest] [money] NULL CONSTRAINT [DF_WFCSRENEWMSplus_CalInterest]  DEFAULT ((0)),
	[CalLastIntCr] [money] NULL,
	[CalInterestPaid] [money] NULL CONSTRAINT [DF_WFCSRENEWMSplus_CalInterestPaid]  DEFAULT ((0)),
	[CalInterestDue] [money] NULL CONSTRAINT [DF_WFCSRENEWMSplus_CalInterestDue]  DEFAULT ((0)),
	[CalCurrentAmt] [money] NULL CONSTRAINT [DF_WFCSRENEWMSplus_CalCurrentAmt]  DEFAULT ((0)),
	[NewIntRate] [smallmoney] NULL CONSTRAINT [DF_WFCSRENEWMSplus_NewIntRate]  DEFAULT ((0)),
	[NewNoRenwl] [smallint] NULL,
	[NewNoAnni] [smallint] NULL,
	[NewRenwlDate] [smalldatetime] NULL,
	[NewRenwlAmt] [money] NULL,
	[NewFixedMthInt] [money] NULL,
	[NewMatureDate] [smalldatetime] NULL,
	[FDAmount] [money] NULL,
	[FuncOpt] [smallint] NULL,
	[PayType] [smallint] NULL,
	[TrnDesc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TrnType] [tinyint] NULL,
	[TrnDrCr] [tinyint] NULL,
	[TrnContraDrCr] [tinyint] NULL,
	[ShowInterest] [tinyint] NULL,
	[TrnGLAccNoDr] [int] NULL,
	[TrnGLAccNoCr] [int] NULL,
	[CalFromDate] [smalldatetime] NULL,
	[NoDays] [int] NULL CONSTRAINT [DF_WFCSRENEWMSplus_NoDays]  DEFAULT ((0)),
	[CuNumber] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MemName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TrnUpdate] [tinyint] NULL CONSTRAINT [DF_WFCSRENEWMSplus_TrnUpdate]  DEFAULT ((0)),
	[ProcStat] [tinyint] NULL,
	[InputBy] [smallint] NULL CONSTRAINT [DF_WFCSRENEWMSplus_InputBy]  DEFAULT ((0)),
	[VerifyBy] [smallint] NULL CONSTRAINT [DF_WFCSRENEWMSplus_VerifyBy]  DEFAULT ((0)),
	[ApprovBy] [smallint] NULL CONSTRAINT [DF_WFCSRENEWMSplus_ApprovBy]  DEFAULT ((0)),
	[InputByDate] [datetime] NULL,
	[VerifyByDate] [datetime] NULL,
	[ApprovByDate] [datetime] NULL,
	[UserId] [smallint] NULL CONSTRAINT [DF_WFCSRENEWMSplus_UserId]  DEFAULT ((0)),
	[CreateDate] [datetime] NULL CONSTRAINT [DF_WFCSRENEWMSplus_CreateDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_WFCSRENEWMSplus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
