USE [A2ZSTMCUS]
GO

/****** Object:  Table [dbo].[A2ZSTCONSOLIDATED]    Script Date: 6/23/2018 10:02:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[A2ZSTCONSOLIDATED](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[STKItemCode] [int] NULL,
	[STKItemName] [nvarchar](50) NULL,
	[STKItemUnit] [int] NULL,
	[STKItemUnitDesc] [nvarchar](50) NULL,
	[STKOpUnitQty] [int] NULL,
	[STKOpUnitRate] [money] NULL,
	[STKOpUnitAmt] [money] NULL,
	[STKRcvUnitQty] [int] NULL,
	[STKRcvUnitRate] [money] NULL,
	[STKRcvUnitAmt] [money] NULL,
	[STKUnitTotalAmt] [money] NULL,
	[STKIssUnitQty] [int] NULL,
	[STKIssUnitRate] [money] NULL,
	[STKIssUnitAmt] [money] NULL,
	[STKCloUnitQty] [int] NULL,
	[STKCloUnitRate] [money] NULL,
	[STKCloUnitAmt] [money] NULL,
	[STKTotalProfit] [money] NULL,
	[STKSellingPrice] [money] NULL,
	[STKTotalSellAmt] [money] NULL,
	[CalUnitCost] [money] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_A2ZSTCOMP] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

