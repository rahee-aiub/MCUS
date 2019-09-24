USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoLoan]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[Sp_CSGetInfoLoan]
@AppNo INT


AS
SELECT  lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo, LoanApplicationNo, LoanApplicationDate, AccType, MemNo, LoanApplicationAmt, LoanIntRate, LoanGrace, LoanInstlAmt, LoanLastInstlAmt, LoanNoInstl, LoanFirstInstlDt, LoanExpiryDate,LoanIntPeriod, LoanIntMethod, LoanPurpose, LoanCategory, LoanSuretyMemNo, LoanStatus,LoanStatDate, LoanProcFlag,LoanExpiryDate,FromCashCode,LoanTotGuarantorAmt,InputBy,ApprovBy,InputByDate,ApprovByDate

FROM A2ZLOAN  WHERE LoanApplicationNo = @AppNo

GO
