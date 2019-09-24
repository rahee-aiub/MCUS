USE [A2ZCSMCUS]
GO

/****** Object:  Table [dbo].[WFTRANSFERTRN]    Script Date: 2019-06-20 2:37:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WFTRANSFERTRN](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccNo] [bigint] NULL,
	[CuType] [int] NULL,
	[CreditUnionNo] [int] NULL,
	[CreditUnionName] [nvarchar](100) NULL,
	[DepositorNo] [int] NULL,
	[DepositorName] [nvarchar](100) NULL,
	[TrnAmount] [money] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_WFTRANSFERTRN] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
