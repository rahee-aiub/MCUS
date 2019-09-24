
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE  [dbo].[Sp_GLGetInfoGLOldCode]

@glOldAccNo INT


AS
SELECT 

Id
,GLCoNo
,GLAccType
,GLAccNo
,GLRecType
,GLPrtPos
,GLAccDesc
,GLBgtType
,GLOpBal
,GLDrSumC
,GLDrSumT
,GLCrSumC
,GLCrSumT
,GLClBal
,GLHead
,GLMainHead
,GLSubHead
,GLHeadDesc
,GLMainHeadDesc
,GLSubHeadDesc
,LastVoucherNo
,GLBalanceType
,GLAccMode
,Status


FROM A2ZCGLMST  WHERE GLOldAccNo = @glOldAccNo
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

