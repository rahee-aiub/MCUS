USE [A2ZHRMCUS]
GO
/****** Object:  Table [dbo].[WFSCertificate]    Script Date: 03/22/2017 13:30:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WFSCertificate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmpNo] [int] NULL CONSTRAINT [DF_WFSCertificate_EmpNo]  DEFAULT ((0)),
	[EmpArea] [int] NULL CONSTRAINT [DF_WFSCertificate_EmpArea]  DEFAULT ((0)),
	[EmpLocation] [int] NULL CONSTRAINT [DF_WFSCertificate_EmpLocation]  DEFAULT ((0)),
	[EmpProject] [smallint] NULL CONSTRAINT [DF_WFSCertificate_EmpProject]  DEFAULT ((0)),
	[EmpRelagion] [smallint] NULL,
	[EmpGender] [smallint] NULL,
	[Status] [smallint] NULL,
 CONSTRAINT [PK_WFSCertificate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
