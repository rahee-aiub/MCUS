USE [A2ZGLMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptGLChartOfAccountReport]    Script Date: 04/29/2015 15:43:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




/*
EXECUTE Sp_rptGLChartOfAccountReport
*/

CREATE PROCEDURE [dbo].[Sp_rptGLChartOfAccountReport] 
AS
BEGIN

SELECT     GLCoNo, GLAccType, GLAccNo, GLRecType, GLPrtPos, GLAccDesc, GLBgtType, GLOpBal, GLDrSumC, GLDrSumT, GLCrSumC, GLCrSumT, GLClBal, GLHead, 
                      GLMainHead, GLSubHead, GLHeadDesc, GLMainHeadDesc, GLSubHeadDesc, GLOldAccNo, LastVoucherNo
FROM         dbo.A2ZCGLMST

END













