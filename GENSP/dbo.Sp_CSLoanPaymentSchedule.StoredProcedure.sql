USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSLoanPaymentSchedule]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[Sp_CSLoanPaymentSchedule]

AS

BEGIN
SELECT ScheduleLoanAmt,NoofInstl,IntRate,InstlAmount,LastInstlAmount,TotalIntAmount,NetPayable,LoanMth,InstlAmt,LoanAmt,IntAmt,LoanPayable FROM dbo.A2ZWFLOANSCHEDULE

END


GO
