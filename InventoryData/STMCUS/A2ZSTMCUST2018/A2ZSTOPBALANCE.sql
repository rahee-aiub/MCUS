USE [A2ZSTMCUST2018]
GO

/****** Object:  Table [dbo].[A2ZSTOPBALANCE]    Script Date: 6/23/2018 10:20:23 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[A2ZSTOPBALANCE](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[STKWarehouseNo] [int] NULL,
	[STKItemCode] [int] NULL,
	[STKItemName] [nvarchar](50) NULL,
	[STKItemGroupNo] [int] NULL,
	[STKItemCategoryNo] [int] NULL,
	[STKUnitQty] [int] NULL,
 CONSTRAINT [PK_A2ZSTOPBALANCE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

