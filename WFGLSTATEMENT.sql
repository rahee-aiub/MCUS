USE [A2ZGLMCUS]
GO
/****** Object:  Table [dbo].[WFGLSTATEMENT]    Script Date: 01/16/2017 00:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WFGLSTATEMENT](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BatchNo] [int] NULL CONSTRAINT [DF_WFGLSTATEMENT_BatchNo]  DEFAULT ((0)),
	[TrnDate] [smalldatetime] NULL,
	[VchNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VoucherNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CuType] [smallint] NULL CONSTRAINT [DF_WFGLSTATEMENT_CuType]  DEFAULT ((0)),
	[CuNo] [int] NULL CONSTRAINT [DF_WFGLSTATEMENT_CuNo]  DEFAULT ((0)),
	[MemNo] [int] NULL CONSTRAINT [DF_WFGLSTATEMENT_MemNo]  DEFAULT ((0)),
	[AccType] [int] NULL CONSTRAINT [DF_WFGLSTATEMENT_AccType]  DEFAULT ((0)),
	[AccNo] [bigint] NULL CONSTRAINT [DF_WFGLSTATEMENT_AccNo]  DEFAULT ((0)),
	[FuncOpt] [smallint] NULL CONSTRAINT [DF_WFGLSTATEMENT_FuncOpt]  DEFAULT ((0)),
	[FuncOptDesc] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PayType] [int] NULL CONSTRAINT [DF_WFGLSTATEMENT_PayType]  DEFAULT ((0)),
	[TrnType] [tinyint] NULL CONSTRAINT [DF_WFGLSTATEMENT_TrnType]  DEFAULT ((0)),
	[TrnDrCr] [tinyint] NULL CONSTRAINT [DF_WFGLSTATEMENT_TrnDrCr]  DEFAULT ((0)),
	[TrnDebit] [money] NULL CONSTRAINT [DF_WFGLSTATEMENT_TrnDebit]  DEFAULT ((0)),
	[TrnCredit] [money] NULL CONSTRAINT [DF_WFGLSTATEMENT_TrnCredit]  DEFAULT ((0)),
	[TrnDesc] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TrnVchType] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TrnChqPrx] [nvarchar](7) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TrnChqNo] [nvarchar](15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_WFGLSTATEMENT_TrnChqNo]  DEFAULT ((0)),
	[ShowInterest] [tinyint] NULL CONSTRAINT [DF_WFGLSTATEMENT_ShowInterest]  DEFAULT ((0)),
	[TrnInterestAmt] [money] NULL CONSTRAINT [DF_WFGLSTATEMENT_TrnInterestAmt]  DEFAULT ((0)),
	[TrnPenalAmt] [money] NULL CONSTRAINT [DF_WFGLSTATEMENT_TrnPenalAmt]  DEFAULT ((0)),
	[TrnChargeAmT] [money] NULL CONSTRAINT [DF_WFGLSTATEMENT_TrnChargeAmT]  DEFAULT ((0)),
	[TrnDueIntAmt] [money] NULL CONSTRAINT [DF_WFGLSTATEMENT_TrnDueIntAmt]  DEFAULT ((0)),
	[TrnODAmount] [money] NULL CONSTRAINT [DF_WFGLSTATEMENT_TrnODAmount]  DEFAULT ((0)),
	[BranchNo] [int] NULL CONSTRAINT [DF_WFGLSTATEMENT_BranchNo]  DEFAULT ((0)),
	[TrnCSGL] [tinyint] NULL CONSTRAINT [DF_WFGLSTATEMENT_TrnCSGL]  DEFAULT ((0)),
	[TrnGLAccNoDr] [int] NULL CONSTRAINT [DF_WFGLSTATEMENT_TrnGLAccNoDr]  DEFAULT ((0)),
	[TrnGLAccNoCr] [int] NULL CONSTRAINT [DF_WFGLSTATEMENT_TrnGLAccNoCr]  DEFAULT ((0)),
	[TrnGLFlag] [tinyint] NULL CONSTRAINT [DF_WFGLSTATEMENT_TrnGLFlag]  DEFAULT ((0)),
	[GLAccNo] [int] NULL CONSTRAINT [DF_WFGLSTATEMENT_GLAccNo]  DEFAULT ((0)),
	[GLAmount] [money] NULL CONSTRAINT [DF_WFGLSTATEMENT_GLAmount]  DEFAULT ((0)),
	[GLDebitAmt] [money] NULL CONSTRAINT [DF_WFGLSTATEMENT_GLDebitAmt]  DEFAULT ((0)),
	[GLCreditAmt] [money] NULL CONSTRAINT [DF_WFGLSTATEMENT_GLCreditAmt]  DEFAULT ((0)),
	[TrnFlag] [tinyint] NULL CONSTRAINT [DF_WFGLSTATEMENT_TrnFlag]  DEFAULT ((0)),
	[TrnStatus] [tinyint] NULL CONSTRAINT [DF_WFGLSTATEMENT_TrnStatus]  DEFAULT ((0)),
	[FromCashCode] [int] NULL CONSTRAINT [DF_WFGLSTATEMENT_FromCashCode]  DEFAULT ((0)),
	[TrnProcStat] [tinyint] NULL CONSTRAINT [DF_WFGLSTATEMENT_TrnProcStat]  DEFAULT ((0)),
	[TrnSysUser] [tinyint] NULL CONSTRAINT [DF_WFGLSTATEMENT_TrnSysUser]  DEFAULT ((0)),
	[TrnModule] [tinyint] NULL,
	[ValueDate] [smalldatetime] NULL,
	[UserIP] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UserID] [int] NULL CONSTRAINT [DF_WFGLSTATEMENT_UserID]  DEFAULT ((0)),
	[CreateDate] [datetime] NULL,
	[GLAccType] [tinyint] NULL CONSTRAINT [DF_WFGLSTATEMENT_GLCreditAmt1]  DEFAULT ((0)),
	[GLClosingBal] [money] NULL,
 CONSTRAINT [PK_WFGLSTATEMENT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
