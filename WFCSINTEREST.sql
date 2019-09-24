USE [A2ZCSMCUS]
GO
/****** Object:  Table [dbo].[WFCSINTEREST]    Script Date: 02/06/2017 21:24:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WFCSINTEREST](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TrnDate] [smalldatetime] NULL,
	[AccType] [int] NULL CONSTRAINT [DF_WFCSINTEREST_AccType]  DEFAULT ((0)),
	[AccNo] [bigint] NULL CONSTRAINT [DF_WFCSINTEREST_AccNo]  DEFAULT ((0)),
	[CuType] [smallint] NULL,
	[CuNo] [int] NULL CONSTRAINT [DF_WFCSINTEREST_CuNo]  DEFAULT ((0)),
	[CuNumber] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MemNo] [int] NULL CONSTRAINT [DF_WFCSINTEREST_MemberNo]  DEFAULT ((0)),
	[MemName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccOpenDate] [smalldatetime] NULL,
	[AccStatus] [tinyint] NULL CONSTRAINT [DF_WFCSINTEREST_AccStatus]  DEFAULT ((0)),
	[AccIntRate] [smallmoney] NULL,
	[AmtOpening] [money] NULL,
	[AmtJul] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtJul]  DEFAULT ((0)),
	[AmtAug] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtAug]  DEFAULT ((0)),
	[AmtSep] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtSep]  DEFAULT ((0)),
	[AmtOct] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtOct]  DEFAULT ((0)),
	[AmtNov] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtNov]  DEFAULT ((0)),
	[AmtDec] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtDec]  DEFAULT ((0)),
	[AmtJan] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtJan]  DEFAULT ((0)),
	[AmtFeb] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtFeb]  DEFAULT ((0)),
	[AmtMar] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtMar]  DEFAULT ((0)),
	[AmtApr] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtApr]  DEFAULT ((0)),
	[AmtMay] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtMay]  DEFAULT ((0)),
	[AmtJun] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtJun]  DEFAULT ((0)),
	[AmtProduct] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtProduct]  DEFAULT ((0)),
	[AmtInterest] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtInterest]  DEFAULT ((0)),
	[ProcStat] [tinyint] NULL,
	[UserId] [smallint] NULL CONSTRAINT [DF_WFCSINTEREST_UserId]  DEFAULT ((0)),
	[IntRateJul] [smallmoney] NULL CONSTRAINT [DF_WFCSINTEREST_AccIntRate1]  DEFAULT ((0)),
	[IntRateAug] [smallmoney] NULL CONSTRAINT [DF_WFCSINTEREST_AccIntRate2]  DEFAULT ((0)),
	[IntRateSep] [smallmoney] NULL CONSTRAINT [DF_WFCSINTEREST_AccIntRate3]  DEFAULT ((0)),
	[IntRateOct] [smallmoney] NULL CONSTRAINT [DF_WFCSINTEREST_AccIntRate4]  DEFAULT ((0)),
	[IntRateNov] [smallmoney] NULL CONSTRAINT [DF_WFCSINTEREST_AccIntRate5]  DEFAULT ((0)),
	[IntRateDec] [smallmoney] NULL CONSTRAINT [DF_WFCSINTEREST_AccIntRate6]  DEFAULT ((0)),
	[IntRateJan] [smallmoney] NULL CONSTRAINT [DF_WFCSINTEREST_AccIntRate7]  DEFAULT ((0)),
	[IntRateFeb] [smallmoney] NULL CONSTRAINT [DF_WFCSINTEREST_AccIntRate8]  DEFAULT ((0)),
	[IntRateMar] [smallmoney] NULL CONSTRAINT [DF_WFCSINTEREST_AccIntRate9]  DEFAULT ((0)),
	[IntRateApr] [smallmoney] NULL CONSTRAINT [DF_WFCSINTEREST_AccIntRate10]  DEFAULT ((0)),
	[IntRateMay] [smallmoney] NULL CONSTRAINT [DF_WFCSINTEREST_AccIntRate11]  DEFAULT ((0)),
	[IntRateJun] [smallmoney] NULL CONSTRAINT [DF_WFCSINTEREST_AccIntRate12]  DEFAULT ((0)),
	[IntAmtJul] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtJul1]  DEFAULT ((0)),
	[IntAmtAug] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtAug1]  DEFAULT ((0)),
	[IntAmtSep] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtSep1]  DEFAULT ((0)),
	[IntAmtOct] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtOct1]  DEFAULT ((0)),
	[IntAmtNov] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtNov1]  DEFAULT ((0)),
	[IntAmtDec] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtDec1]  DEFAULT ((0)),
	[IntAmtJan] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtJan1]  DEFAULT ((0)),
	[IntAmtFeb] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtFeb1]  DEFAULT ((0)),
	[IntAmtMar] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtMar1]  DEFAULT ((0)),
	[IntAmtApr] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtApr1]  DEFAULT ((0)),
	[IntAmtMay] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtMay1]  DEFAULT ((0)),
	[IntAmtJun] [money] NULL CONSTRAINT [DF_WFCSINTEREST_AmtJun1]  DEFAULT ((0)),
 CONSTRAINT [PK_WFCSINTEREST] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
