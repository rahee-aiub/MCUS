USE [A2ZCSMCUS]
GO
/****** Object:  Table [dbo].[A2ZODINTDUEHST]    Script Date: 08/10/2017 10:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[A2ZODINTDUEHST](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TrnDate] [smalldatetime] NULL,
	[CuType] [tinyint] NULL,
	[CuNo] [int] NULL,
	[MemNo] [int] NULL,
	[AccType] [tinyint] NULL,
	[AccNo] [bigint] NULL,
	[AccODPaidInt] [money] NULL,
	[AccODDueInt] [money] NULL CONSTRAINT [DF_A2ZODINTDUEHST_AccBalance]  DEFAULT ((0)),
	[AccODDisbAmt] [money] NULL CONSTRAINT [DF_A2ZODINTDUEHST_AccODDueInt1]  DEFAULT ((0)),
	[AccDrCr] [tinyint] NULL,
 CONSTRAINT [PK_A2ZODINTDUEHST] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
