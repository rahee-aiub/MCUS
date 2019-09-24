USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoParam]    Script Date: 04/16/2018 12:05:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






ALTER PROCEDURE  [dbo].[Sp_CSGetInfoParam]

@AccType INT

AS
SELECT 

AccType
,PrmCalPeriod
,PrmCalMethod
,PrmLoanCalMethod
,PrmIntRate
,PrmFundRate
,PrmProdCon
,PrmProdIntType
,PrmMinDeposit
,PrmRoundFlag
,PrmIntWithdrDays
,PrmAccProcFees
,PrmAccClosingFees
,PrmPeriod



FROM A2ZCSPARAM  WHERE AccType = @AccType 










GO

