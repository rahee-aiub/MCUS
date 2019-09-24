USE [A2ZCSMCUS]
GO
/****** Object:  Table [dbo].[WF_TRFTRANSACTION]    Script Date: 07/19/2017 15:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WF_TRFTRANSACTION](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DelUserId] [int] NULL,
	[TrnDate] [smalldatetime] NULL,
	[VchNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VoucherNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_WF_TRFTRANSACTION_VoucherNo1]  DEFAULT ((0)),
	[CuType] [smallint] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_CuType1]  DEFAULT ((0)),
	[CuNo] [int] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_CuNo]  DEFAULT ((0)),
	[MemNo] [int] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_MemNo]  DEFAULT ((0)),
	[AccType] [int] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_AccType]  DEFAULT ((0)),
	[AccNo] [bigint] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_AccNo]  DEFAULT ((0)),
	[FuncOpt] [smallint] NULL,
	[PayType] [int] NULL,
	[PayTypeDes] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TrnType] [int] NULL,
	[TrnDrCr] [tinyint] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_TrnDrCr]  DEFAULT ((0)),
	[TrnDesc] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TrnMode] [tinyint] NULL,
	[TrnCredit] [money] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_Credit]  DEFAULT ((0)),
	[TrnDebit] [money] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_Debit]  DEFAULT ((0)),
	[TrnInterestAmt] [money] NULL,
	[TrnDueIntAmt] [money] NULL,
	[ShowInterest] [tinyint] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_ShowInt]  DEFAULT ((0)),
	[TrnCSGL] [tinyint] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_TrnCSGL]  DEFAULT ((0)),
	[TrnGLAccNoDR] [int] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_GLAccNoDR]  DEFAULT ((0)),
	[TrnGLAccNoCR] [int] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_GLAccNoCR]  DEFAULT ((0)),
	[GLAccNo] [int] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_GLAccNo]  DEFAULT ((0)),
	[GLAmount] [money] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_GLAmount]  DEFAULT ((0)),
	[GLDebitAmt] [money] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_GLDebitAmt]  DEFAULT ((0)),
	[GLCreditAmt] [money] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_GLCreditAmt]  DEFAULT ((0)),
	[TrnFlag] [tinyint] NULL,
	[TrnSysUser] [tinyint] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_TrnSysUser]  DEFAULT ((0)),
	[TrnModule] [tinyint] NULL,
	[FromCashCode] [int] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_FromCashCode]  DEFAULT ((0)),
	[ValueDate] [smalldatetime] NULL,
	[UserID] [smallint] NULL,
	[TrnRevFlag] [smallint] NULL,
	[ProvAdjFlag] [int] NULL,
	[TrnID] [int] NULL,
	[TrnSign] [tinyint] NULL,
	[ToCuType] [smallint] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_CuType1_1]  DEFAULT ((0)),
	[ToCuNo] [int] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_CuNo1]  DEFAULT ((0)),
	[ToMemNo] [int] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_MemNo1]  DEFAULT ((0)),
	[ToAccType] [int] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_AccType1]  DEFAULT ((0)),
	[ToAccNo] [bigint] NULL CONSTRAINT [DF_WF_TRFTRANSACTION_AccNo1]  DEFAULT ((0)),
 CONSTRAINT [PK_WF_TRFTRANSACTION] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
