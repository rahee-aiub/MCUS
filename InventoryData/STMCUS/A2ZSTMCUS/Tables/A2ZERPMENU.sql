USE [A2ZSTMCUS]
GO

/****** Object:  Table [dbo].[A2ZERPMENU]    Script Date: 6/23/2018 10:01:35 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[A2ZERPMENU](
	[UserId] [smallint] NOT NULL,
	[ModuleNo] [tinyint] NOT NULL,
	[MenuNo] [smallint] NOT NULL,
	[MenuName] [nvarchar](50) NOT NULL,
	[MenuParentNo] [smallint] NULL,
	[MenuUrl] [nvarchar](100) NULL,
	[MenuLogFlag] [tinyint] NULL,
 CONSTRAINT [PK_A2ZERPMENU] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ModuleNo] ASC,
	[MenuNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

