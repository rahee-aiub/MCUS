USE [A2ZCSMCUS]
GO
/****** Object:  Table [dbo].[WFCSODLEDGERBALANCE]    Script Date: 08/10/2017 10:25:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WFCSODLEDGERBALANCE](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CuType] [smallint] NULL,
	[CuNo] [int] NULL CONSTRAINT [DF_WFCSODLEDGERBALANCE_DepositeAmt]  DEFAULT ((0)),
	[MemNo] [int] NULL CONSTRAINT [DF_WFCSODLEDGERBALANCE_Period]  DEFAULT ((0)),
	[MemName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_WFCSODLEDGERBALANCE_MaturedAmt]  DEFAULT ((0)),
	[AccType] [tinyint] NULL,
	[AccNo] [nvarchar](16) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_WFCSODLEDGERBALANCE_IntRate]  DEFAULT ((0)),
	[OpenDate] [smalldatetime] NULL CONSTRAINT [DF_WFCSODLEDGERBALANCE_PenalAmt]  DEFAULT ((0)),
	[Status] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_WFCSODLEDGERBALANCE_BonusAmt]  DEFAULT ((0)),
	[AccODDisbAmt] [money] NULL,
	[LastTrnDate] [smalldatetime] NULL,
	[AccIntRate] [smallmoney] NULL CONSTRAINT [DF_WFCSODLEDGERBALANCE_AccIntRate]  DEFAULT ((0)),
	[IntNoDays] [int] NULL,
	[AccStatus] [tinyint] NULL,
	[GLCashCode] [int] NULL,
	[AccStatusDate] [smalldatetime] NULL,
	[DueIntAmount] [money] NULL CONSTRAINT [DF_WFCSODLEDGERBALANCE_DueIntAmount]  DEFAULT ((0)),
	[CurrIntAmount] [money] NULL CONSTRAINT [DF_WFCSODLEDGERBALANCE_CurrIntAmount]  DEFAULT ((0)),
	[NetIntAmount] [money] NULL CONSTRAINT [DF_WFCSODLEDGERBALANCE_NetIntAmount]  DEFAULT ((0)),
	[Balance] [money] NULL CONSTRAINT [DF_WFCSODLEDGERBALANCE_Balance]  DEFAULT ((0)),
	[CuNumber] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_WFCSODLEDGERBALANCE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
