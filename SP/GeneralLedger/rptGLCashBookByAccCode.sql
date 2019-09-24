USE [A2ZGLMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptGLCashBookByAccCode]    Script Date: 04/29/2015 15:43:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



/*
EXECUTE Sp_rptGLCashBookByAccCode '2014-12-15','2014-12-15'
*/


CREATE PROCEDURE [dbo].[Sp_rptGLCashBookByAccCode] 
AS 
BEGIN
SELECT     dbo.WFINCEXPREP.GLAccType, dbo.WFINCEXPREP.GLAccNo, dbo.WFINCEXPREP.GLAccDesc, dbo.WFINCEXPREP.GLOpBal, 
                      dbo.WFINCEXPREP.GLDrSumC, dbo.WFINCEXPREP.GLDrSumT, dbo.WFINCEXPREP.GLCrSumC, dbo.WFINCEXPREP.GLCrSumT, 
                      dbo.WFINCEXPREP.GLClBal, dbo.WFINCEXPREP.CodeFlag, dbo.WFA2ZTRANSACTION.VoucherNo
FROM         dbo.WFINCEXPREP LEFT OUTER JOIN
                      dbo.WFA2ZTRANSACTION ON dbo.WFINCEXPREP.GLAccNo = dbo.WFA2ZTRANSACTION.GLAccNo

END














