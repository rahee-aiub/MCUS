USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoParameter]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE  [dbo].[Sp_CSGetInfoParameter]

AS
SELECT 

FinancialMonth
,FinancialBegYear
,FinancialEndYear
,CurrentMonth
,CurrentYear                                    
,LastVoucherNo
,LastUpdateDate            
,NumberOfUser
,SingleUserFlag            
,ProcessDate
,ProcessStatus
,BackupStatus
,InstallDate
,CashCode
,ApprovBy
,ApprovByDate


FROM A2ZCSPARAMETER 








GO
