USE [A2ZHKMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetInfoDesignation]    Script Date: 04/29/2015 15:45:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE  [dbo].[Sp_GetInfoDesignation]
@Designcode INT

AS
SELECT 

id
,DesigCode
,DesigDescription

FROM A2ZDESIGNATION  WHERE DesigCode = @Designcode



