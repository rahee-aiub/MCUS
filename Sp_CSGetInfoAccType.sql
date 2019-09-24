
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE  [dbo].[Sp_CSGetInfoAccType]

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

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

