USE [A2ZSTMCUS]
GO

/****** Object:  Table [dbo].[A2ZSTMST]    Script Date: 6/24/2018 11:34:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[A2ZSTMST](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[STKItemCode] [int] NULL,
	[STKItemName] [nvarchar](50) NULL,
	[STKGroup] [int] NULL,
	[STKSubGroup] [int] NULL,
	[STKUnit] [int] NULL,
	[STKUnitQty] [int] NULL,
	[STKOpUnitQty] [int] NULL,
	[STKUnitAvgCost] [decimal](18, 10) NULL,
	[STKUnitAvgCostDate] [smalldatetime] NULL,
	[STKUnitSalePrice] [money] NULL,
	[STKStatus] [int] NULL,
	[STKStatusDesc] [nvarchar](50) NULL,
	[STKStatusDate] [smalldatetime] NULL,
	[STKReOrderLevel] [int] NULL,
	[CalUnitQty] [int] NULL,
	[CalUnitCost] [numeric](18, 10) NULL,
	[CalAvgUnitQty] [int] NULL,
	[STKTPUnitQty] [int] NULL,
	[STKTPUnitCost] [money] NULL,
 CONSTRAINT [PK_A2ZSTMST] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[A2ZSTMST] ADD  CONSTRAINT [DF_A2ZSTMST_STKUnitQty]  DEFAULT ((0)) FOR [STKUnitQty]
GO

ALTER TABLE [dbo].[A2ZSTMST] ADD  CONSTRAINT [DF_A2ZSTMST_STKUnitQty1]  DEFAULT ((0)) FOR [CalUnitQty]
GO

ALTER TABLE [dbo].[A2ZSTMST] ADD  CONSTRAINT [DF_A2ZSTMST_CalUnitQty1]  DEFAULT ((0)) FOR [CalAvgUnitQty]
GO

ALTER TABLE [dbo].[A2ZSTMST] ADD  CONSTRAINT [DF_A2ZSTMST_STKUnitQty1_1]  DEFAULT ((0)) FOR [STKTPUnitQty]
GO

