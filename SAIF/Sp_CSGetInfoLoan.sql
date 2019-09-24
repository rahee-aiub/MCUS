USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoLoan]    Script Date: 04/07/2018 12:59:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE  [dbo].[Sp_CSGetInfoLoan]
@AppNo INT


AS
SELECT  lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo, LoanApplicationNo, LoanApplicationDate, AccType, MemNo, LoanApplicationAmt, LoanIntRate, LoanGrace, LoanInstlAmt, LoanLastInstlAmt, LoanNoInstl, LoanFirstInstlDt, LoanExpiryDate,LoanIntPeriod, LoanIntMethod, LoanPurpose, LoanCategory, LoanSuretyMemNo, LoanStatus,LoanStatDate, LoanProcFlag,LoanExpiryDate,FromCashCode,LoanTotGuarantorAmt,InputBy,ApprovBy,InputByDate,ApprovByDate,AccPeriod

FROM A2ZLOAN  WHERE LoanApplicationNo = @AppNo

GO

