USE [A2ZCSMCUS]
GO
/****** Object:  Table [dbo].[WFCSSHAREINT]    Script Date: 06/20/2017 13:07:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WFCSSHAREINT](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccType] [int] NULL CONSTRAINT [DF_WFCSSHAREINT_AccType]  DEFAULT ((0)),
	[AccNo] [bigint] NULL CONSTRAINT [DF_WFCSSHAREINT_AccNo]  DEFAULT ((0)),
	[CuType] [smallint] NULL,
	[CuNo] [int] NULL CONSTRAINT [DF_WFCSSHAREINT_CuNo]  DEFAULT ((0)),
	[MemNo] [int] NULL CONSTRAINT [DF_WFCSSHAREINT_MemberNo]  DEFAULT ((0)),
	[MemName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccOpenDate] [smalldatetime] NULL,
	[AccStatus] [tinyint] NULL CONSTRAINT [DF_WFCSSHAREINT_AccStatus]  DEFAULT ((0)),
	[AccIntRate] [smallmoney] NULL,
	[AmtOpening] [money] NULL,
	[AmtJul] [money] NULL CONSTRAINT [DF_WFCSSHAREINT_AmtJul]  DEFAULT ((0)),
	[AmtAug] [money] NULL CONSTRAINT [DF_WFCSSHAREINT_AmtAug]  DEFAULT ((0)),
	[AmtSep] [money] NULL CONSTRAINT [DF_WFCSSHAREINT_AmtSep]  DEFAULT ((0)),
	[AmtOct] [money] NULL CONSTRAINT [DF_WFCSSHAREINT_AmtOct]  DEFAULT ((0)),
	[AmtNov] [money] NULL CONSTRAINT [DF_WFCSSHAREINT_AmtNov]  DEFAULT ((0)),
	[AmtDec] [money] NULL CONSTRAINT [DF_WFCSSHAREINT_AmtDec]  DEFAULT ((0)),
	[AmtJan] [money] NULL CONSTRAINT [DF_WFCSSHAREINT_AmtJan]  DEFAULT ((0)),
	[AmtFeb] [money] NULL CONSTRAINT [DF_WFCSSHAREINT_AmtFeb]  DEFAULT ((0)),
	[AmtMar] [money] NULL CONSTRAINT [DF_WFCSSHAREINT_AmtMar]  DEFAULT ((0)),
	[AmtApr] [money] NULL CONSTRAINT [DF_WFCSSHAREINT_AmtApr]  DEFAULT ((0)),
	[AmtMay] [money] NULL CONSTRAINT [DF_WFCSSHAREINT_AmtMay]  DEFAULT ((0)),
	[AmtJun] [money] NULL CONSTRAINT [DF_WFCSSHAREINT_AmtJun]  DEFAULT ((0)),
	[AmtProduct] [money] NULL CONSTRAINT [DF_WFCSSHAREINT_AmtProduct]  DEFAULT ((0)),
	[AmtInterest] [money] NULL CONSTRAINT [DF_WFCSSHAREINT_AmtInterest]  DEFAULT ((0)),
	[CuNumber] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProcStat] [tinyint] NULL,
	[UserId] [smallint] NULL CONSTRAINT [DF_WFCSSHAREINT_UserId]  DEFAULT ((0)),
	[TrnDate] [smalldatetime] NULL,
 CONSTRAINT [PK_WFCSSHAREINT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
