USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoAccStatus]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE  [dbo].[Sp_CSGetInfoAccStatus]

@Statuscode INT

AS
SELECT 

AccStatusCode
,AccStatusDescription


FROM A2ZACCSTATUS  WHERE AccStatusCode = @Statuscode 








GO
