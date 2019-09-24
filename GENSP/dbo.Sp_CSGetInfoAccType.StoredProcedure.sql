USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoAccType]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[Sp_CSGetInfoAccType]

@Typecode INT

AS
SELECT 

AccTypeCode
,AccTypeDescription
,AccFlag
,AccTypeClass
,AccTypeMode
,AccCertNo
,AccAccessFlag
,AcessType1
,AcessType2
,AcessType3
,AccDepRoundingBy
,AccTypeGuaranty
,AccCorrType


FROM A2ZACCTYPE  WHERE AccTypeCode = @Typecode

GO
