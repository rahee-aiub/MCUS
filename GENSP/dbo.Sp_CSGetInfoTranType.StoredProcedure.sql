USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoTranType]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE  [dbo].[Sp_CSGetInfoTranType]

@Typecode INT


AS
SELECT 

TrnType
,TrnTypeDes


FROM A2ZTRNTYPE  WHERE TrnType = @Typecode 









GO
