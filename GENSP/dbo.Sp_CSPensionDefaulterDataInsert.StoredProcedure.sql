USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSPensionDefaulterDataInsert]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





create PROCEDURE [dbo].[Sp_CSPensionDefaulterDataInsert]
(
    @CuType	smallint,
	@CuNo	int,
    @MemNo int,
    @AccType int,
    @AccNo bigint,
    @DepositAmt money
	
	)

AS

BEGIN

DECLARE @TrnDate smalldatetime;
DECLARE @NewTrnDate smalldatetime;

SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);
SET @NewTrnDate = (DATEADD(dd, -DAY(DATEADD(mm, 1, @TrnDate)), DATEADD(mm, 1, @TrnDate)));


INSERT INTO dbo.A2ZPENSIONDEFAULTER( TrnDate,CuType, CuNo, MemNo,AccType,AccNo,CalDepositAmt,UptoDueDepositAmt,PayableDepositAmt,PayablePenalAmt,PaidDepositAmt,PaidPenalAmt,CurrDueDepositAmt,NoDueDeposit)


VALUES( @NewTrnDate,@CuType, @CuNo, @MemNo,@AccType,@AccNo,@DepositAmt,0,@DepositAmt,0,0,0,@DepositAmt,0)


END




GO
