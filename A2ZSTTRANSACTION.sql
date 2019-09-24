USE [A2ZSTMCUS]
GO

/****** Object:  Table [dbo].[A2ZSTTRANSACTION]    Script Date: 4/22/2018 3:44:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[A2ZSTTRANSACTION](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionDate] [smalldatetime] NULL,
	[VchNo] [int] NULL,
	[VoucherNo] [int] NULL,
	[FuncOpt] [tinyint] NULL,
	[FuncOptDesc] [nvarchar](20) NULL,
	[ItemGroupNo] [int] NULL,
	[ItemGroupDesc] [nvarchar](50) NULL,
	[ItemCategoryNo] [int] NULL,
	[ItemCategoryDesc] [nvarchar](50) NULL,
	[ItemCode] [int] NULL,
	[ItemName] [nvarchar](50) NULL,
	[ItemUnit] [int] NULL,
	[ItemUnitDesc] [nvarchar](50) NULL,
	[ItemUnitPrice] [money] NULL,
	[ItemPurchaseQty] [int] NULL,
	[ItemNetCostPrice] [money] NULL,
	[ItemTotalPrice] [money] NULL,
	[TransactionType] [int] NULL,
	[TransactionTypeDesc] [nvarchar](50) NULL,
	[TransactionNote] [nvarchar](max) NULL,
	[TrnAmtDr] [money] NULL,
	[TrnAmtCr] [money] NULL,
	[TrnQtyDr] [int] NULL,
	[TrnQtyCr] [int] NULL,
	[TrnProcFlag] [tinyint] NULL,
	[OrderNo] [int] NULL,
	[ChalanNo] [int] NULL,
	[SupplierNo] [int] NULL,
	[SupplierName] [nvarchar](50) NULL,
	[WarehouseNo] [int] NULL,
	[WarehouseName] [nvarchar](50) NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_A2ZSTKRCVTRANSACTION] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

