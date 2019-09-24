USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoParam]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE  [dbo].[Sp_CSGetInfoParam]

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



FROM A2ZCSPARAM  WHERE AccType = @AccType 










GO
