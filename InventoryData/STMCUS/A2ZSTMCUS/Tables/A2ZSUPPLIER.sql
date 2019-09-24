USE [A2ZSTMCUS]
GO

/****** Object:  Table [dbo].[A2ZSUPPLIER]    Script Date: 6/23/2018 10:02:54 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[A2ZSUPPLIER](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SuppCode] [int] NULL,
	[SuppName] [nvarchar](50) NULL,
	[SuppAddL1] [nvarchar](100) NULL,
	[SuppAddL2] [nvarchar](100) NULL,
	[SuppAddL3] [nvarchar](100) NULL,
	[SuppTel] [nvarchar](30) NULL,
	[SuppMobile] [nvarchar](30) NULL,
	[SuppFax] [nvarchar](30) NULL,
	[SuppEmail] [nvarchar](30) NULL,
	[SuppBalance] [money] NULL,
	[SuppVATAmt] [money] NULL,
	[SuppTAXAmt] [money] NULL,
	[SuppStatus] [tinyint] NULL,
	[SuppStatDesc] [nvarchar](50) NULL,
	[SuppStatDate] [smalldatetime] NULL,
 CONSTRAINT [PK_A2ZSUPPLIER] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[A2ZSUPPLIER] ADD  CONSTRAINT [DF_A2ZSUPPLIER_SuppCode]  DEFAULT ((0)) FOR [SuppCode]
GO

