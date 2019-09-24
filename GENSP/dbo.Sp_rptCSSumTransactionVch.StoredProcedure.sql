USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptCSSumTransactionVch]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




/*
EXECUTE Sp_rptCSSumTransactionVch 1 
*/


CREATE PROCEDURE [dbo].[Sp_rptCSSumTransactionVch] (@CommonNo1 tinyint) 
AS
BEGIN

SELECT     TOP (100) PERCENT dbo.A2ZTRANSACTION.VchNo, dbo.A2ZTRANSACTION.TrnFlag, SUM(dbo.A2ZTRANSACTION.GLDebitAmt) AS GLDebitAmt, 
                      SUM(dbo.A2ZTRANSACTION.GLCreditAmt) AS GLCreditAmt, dbo.A2ZTRANSACTION.UserID, dbo.A2ZTRANSACTION.VerifyUserID, 
                      A2ZGLMCUS.dbo.A2ZCGLMST.GLAccDesc, dbo.A2ZTRANSACTION.GLAccNo, dbo.A2ZTRANSACTION.TrnCSGL, dbo.A2ZTRANSACTION.TrnDate,dbo.A2ZTRANSACTION.TrnDesc 
                      
FROM         dbo.A2ZTRANSACTION LEFT OUTER JOIN
                      A2ZGLMCUS.dbo.A2ZCGLMST ON dbo.A2ZTRANSACTION.GLAccNo = A2ZGLMCUS.dbo.A2ZCGLMST.GLAccNo
WHERE     (dbo.A2ZTRANSACTION.TrnCSGL = @CommonNo1)

GROUP BY dbo.A2ZTRANSACTION.VchNo, dbo.A2ZTRANSACTION.TrnFlag, dbo.A2ZTRANSACTION.UserID, dbo.A2ZTRANSACTION.VerifyUserID, 
                      A2ZGLMCUS.dbo.A2ZCGLMST.GLAccDesc, dbo.A2ZTRANSACTION.GLAccNo, dbo.A2ZTRANSACTION.TrnCSGL, dbo.A2ZTRANSACTION.TrnDate,dbo.A2ZTRANSACTION.TrnDesc 
                     
ORDER BY dbo.A2ZTRANSACTION.TrnFlag
END














































GO
