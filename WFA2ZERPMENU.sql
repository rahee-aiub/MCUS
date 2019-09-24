USE [A2ZCSMCUS]
GO

/****** Object:  Table [dbo].[WFA2ZERPMENU]    Script Date: 07/16/2018 2:40:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WFA2ZERPMENU](
	[ModuleNo] [int] NULL,
	[ModuleName] [nvarchar](50) NULL,
	[UserId] [int] NULL,
	[UserName] [nvarchar](100) NULL,
	[MenuName] [nvarchar](50) NULL
) ON [PRIMARY]

GO

