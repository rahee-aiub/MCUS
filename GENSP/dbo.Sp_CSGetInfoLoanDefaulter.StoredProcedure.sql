USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoLoanDefaulter]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE  [dbo].[Sp_CSGetInfoLoanDefaulter]
@TrnDate smalldatetime
,@CuType smallint
,@CuNo int
,@MemNo int
,@AccType int
,@AccNo Bigint

AS
SELECT 

TrnDate
,CuType
,CuNo
,MemNo
,AccType
,AccNo
,CalPrincAmt
,CalIntAmt
,UptoDuePrincAmt
,UptoDueIntAmt
,PayablePrincAmt
,PayableIntAmt
,PayablePenalAmt
,PaidPrincAmt
,PaidIntAmt
,PaidPenalAmt
,CurrDuePrincAmt
,CurrDueIntAmt

FROM A2ZLOANDEFAULTER  WHERE CuType=@CuType AND CuNo=@CuNo AND MemNo=@MemNo AND 
                       AccType=@AccType AND AccNo=@AccNo AND MONTH(TrnDate) = MONTH(@TrnDate) AND 
                       YEAR(TrnDate) = YEAR(@TrnDate)






GO
