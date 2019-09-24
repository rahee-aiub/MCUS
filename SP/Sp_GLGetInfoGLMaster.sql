
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE  [dbo].[Sp_GLGetInfoGLMaster]

@glAccNo INT


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


FROM A2ZCGLMST  WHERE GLAccNo = @glAccNo
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

