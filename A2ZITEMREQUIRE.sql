USE [A2ZSTMCUS]
GO

/****** Object:  Table [dbo].[A2ZITEMREQUIRE]    Script Date: 07/09/2019 1:03:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[A2ZITEMREQUIRE](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReqDate] [smalldatetime] NULL,
	[ReqNo] [int] NULL,
	[ReqWarehouseNo] [int] NULL,
	[ReqWarehouseName] [nvarchar](50) NULL,
	[ReqItemGroupNo] [int] NULL,
	[ReqItemGroupDesc] [nvarchar](50) NULL,
	[ReqItemCategoryNo] [int] NULL,
	[ReqItemCategoryDesc] [nvarchar](50) NULL,
	[ReqItemCode] [int] NULL,
	[ReqItemName] [nvarchar](50) NULL,
	[ReqUnitQtyBalance] [int] NULL,
	[ReqReqUnitQty] [int] NULL,
	[ReqNote] [nvarchar](50) NULL,
	[ReqProcStat] [tinyint] NULL,
 CONSTRAINT [PK_A2ZITEMREQUIRE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[A2ZITEMREQUIRE] ADD  CONSTRAINT [DF_A2ZITEMREQUIRE_STKUnitQty]  DEFAULT ((0)) FOR [ReqUnitQtyBalance]
GO

ALTER TABLE [dbo].[A2ZITEMREQUIRE] ADD  CONSTRAINT [DF_A2ZITEMREQUIRE_STKUnitQty1]  DEFAULT ((0)) FOR [ReqReqUnitQty]
GO

