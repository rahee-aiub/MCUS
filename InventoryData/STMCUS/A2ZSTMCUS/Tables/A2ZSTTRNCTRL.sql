USE [A2ZSTMCUS]
GO

/****** Object:  Table [dbo].[A2ZSTTRNCTRL]    Script Date: 6/23/2018 10:02:35 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[A2ZSTTRNCTRL](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FuncNo] [int] NULL,
	[FuncDesc] [nvarchar](50) NULL,
	[GroupCode] [int] NULL,
	[SubGroupCode] [int] NULL,
	[PayType] [int] NULL,
	[TrnType] [int] NULL,
	[TrnMode] [int] NULL,
	[GLAccNoDr] [int] NULL,
	[GLAccNoCr] [int] NULL,
	[TrnRecDesc] [nvarchar](max) NULL,
 CONSTRAINT [PK_A2ZSTTRNCTRL] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

