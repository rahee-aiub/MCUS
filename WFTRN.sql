USE [A2ZSTMCUS]
GO

/****** Object:  Table [dbo].[WFTRN]    Script Date: 06/30/2019 1:36:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WFTRN](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ItemGroupNo] [int] NULL,
	[ItemGroupDesc] [nvarchar](50) NULL,
	[ItemCategoryNo] [int] NULL,
	[ItemCategoryDesc] [nvarchar](50) NULL,
	[ItemCode] [int] NULL,
	[ItemName] [nvarchar](50) NULL,
	[ItemUnit] [int] NULL,
	[ItemUnitDesc] [nvarchar](20) NULL,
	[ItemQty] [int] NULL,
	[ItemUnitPrice] [money] NULL,
	[ItemVATAmt] [money] NULL,
	[ItemTAXAmt] [money] NULL,
	[ItemNetCostPrice] [money] NULL,
	[ItemTotalPrice] [money] NULL,
	[ItemSellPrice] [money] NULL,
	[TrnFlag] [tinyint] NULL,
	[VchNo] [nvarchar](20) NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_WFTRN] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[WFTRN] ADD  CONSTRAINT [DF_WFTRN_TrnFlag]  DEFAULT ((0)) FOR [TrnFlag]
GO

