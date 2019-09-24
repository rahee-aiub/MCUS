USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSDeleteMonthlyBenefit]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[Sp_CSDeleteMonthlyBenefit](@AccType INT,@FuncOpt SMALLINT)  


AS

BEGIN

DECLARE @trnDate smalldatetime;

SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccBenefitDate = A2ZACCOUNT.AccPrevBenefitDate,
                      A2ZACCOUNT.AccProvBalance = A2ZACCOUNT.AccPrevProvBalance,     
                      A2ZACCOUNT.AccNoBenefit = A2ZACCOUNT.AccPrevNoBenefit     


FROM A2ZACCOUNT,WFCSMONTHLYBENEFIT
WHERE A2ZACCOUNT.AccType = WFCSMONTHLYBENEFIT.AccType AND A2ZACCOUNT.AccNo = WFCSMONTHLYBENEFIT.AccNo AND 
A2ZACCOUNT.CuType = WFCSMONTHLYBENEFIT.CuType AND A2ZACCOUNT.CuNo = WFCSMONTHLYBENEFIT.CuNo AND 
A2ZACCOUNT.MemNo = WFCSMONTHLYBENEFIT.MemNo;

DELETE FROM A2ZTRANSACTION  WHERE AccType = @AccType AND FuncOpt = @FuncOpt AND TrnDate = @trnDate;

TRUNCATE TABLE WFCSMONTHLYBENEFIT;

END;






GO
