USE [A2ZCSMCUS]
GO
/****** Object:  Table [dbo].[wfDEVIDENTSHARE]    Script Date: 11/16/2016 19:34:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[wfDEVIDENTSHARE](
	[OldCuNo] [int] NULL CONSTRAINT [DF_wfDEVIDENTSHARE_CuNo]  DEFAULT ((0)),
	[OldMemNo] [int] NULL,
	[AccType] [int] NULL,
	[VchNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TrnAmount] [money] NULL,
	[TrnDesc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
