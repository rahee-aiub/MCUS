USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoPensionDefaulter]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE  [dbo].[Sp_CSGetInfoPensionDefaulter]
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
,CalDepositAmt
,UptoDueDepositAmt
,PayableDepositAmt
,PayablePenalAmt
,PaidDepositAmt
,PaidPenalAmt
,CurrDueDepositAmt
,DepositFlag


FROM A2ZPENSIONDEFAULTER  WHERE CuType=@CuType AND CuNo=@CuNo AND MemNo=@MemNo AND 
                       AccType=@AccType AND AccNo=@AccNo AND MONTH(TrnDate) = MONTH(@TrnDate) AND 
                       YEAR(TrnDate) = YEAR(@TrnDate)







GO
