USE [A2ZGLMCUS]
GO
/****** Object:  Table [dbo].[WFINCEXPREP]    Script Date: 03/07/2017 22:19:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WFINCEXPREP](
	[GLAccType] [int] NULL,
	[GLAccNo] [int] NULL,
	[GLAccDesc] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GLOpBal] [money] NULL CONSTRAINT [DF_WFINCEXPREP_GLOPBAL]  DEFAULT ((0)),
	[GLDrSumC] [money] NULL CONSTRAINT [DF_WFINCEXPREP_GLDRSUMC]  DEFAULT ((0)),
	[GLDrSumT] [money] NULL CONSTRAINT [DF_WFINCEXPREP_GLDRSUMT]  DEFAULT ((0)),
	[GLCrSumC] [money] NULL CONSTRAINT [DF_WFINCEXPREP_GLCRSUMC]  DEFAULT ((0)),
	[GLCrSumT] [money] NULL CONSTRAINT [DF_WFINCEXPREP_GLCRSUMT]  DEFAULT ((0)),
	[GLClBal] [money] NULL CONSTRAINT [DF_WFINCEXPREP_GLCLBAL]  DEFAULT ((0)),
	[CodeFlag] [int] NULL,
	[FromCashCode] [int] NULL,
	[GLHead] [int] NULL,
	[GLSubHead] [int] NULL,
	[GLCodeOrder] [int] NULL
) ON [PRIMARY]
