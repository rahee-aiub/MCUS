USE [A2ZSTMCUS]
GO

/****** Object:  Table [dbo].[A2ZSYSIDS]    Script Date: 6/23/2018 10:03:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[A2ZSYSIDS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdsNo] [int] NULL,
	[IdsPass] [nvarchar](100) NULL,
	[IdsLevel] [tinyint] NULL,
	[IdsLogInFlag] [tinyint] NULL,
	[IdsLockFlag] [tinyint] NULL,
	[IdsName] [nvarchar](100) NULL,
	[IdsFlag] [nchar](10) NULL,
	[IdsType] [nchar](10) NULL,
	[IdsStatus] [nchar](10) NULL,
	[EmpCode] [int] NULL,
	[GLCashCode] [int] NULL,
	[UserId] [smallint] NULL,
	[CreateDate] [datetime] NULL,
	[IdsSODFlag] [bit] NULL,
	[IdsVPrintFlag] [bit] NULL,
	[IdsCWarehouseFlag] [bit] NULL,
 CONSTRAINT [PK_A2ZSYSIDS_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[A2ZSYSIDS] ADD  CONSTRAINT [DF_A2ZSYSIDS_IdsLogInFlag]  DEFAULT ((0)) FOR [IdsLogInFlag]
GO

ALTER TABLE [dbo].[A2ZSYSIDS] ADD  CONSTRAINT [DF_A2ZSYSIDS_GLCashCode]  DEFAULT ((0)) FOR [GLCashCode]
GO

ALTER TABLE [dbo].[A2ZSYSIDS] ADD  CONSTRAINT [DF_A2ZSYSIDS_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

