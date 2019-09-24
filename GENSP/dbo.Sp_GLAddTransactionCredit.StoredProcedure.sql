USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GLAddTransactionCredit]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[Sp_GLAddTransactionCredit] 
 @TrnDate  smalldatetime
,@BatchNo  int
,@VoucherNo  varchar(20)
,@GLAccNo  int
,@GLDebitAmt  money
,@GLCreditAmt  money
,@TrnDesc   varchar(50)
,@TrnChqNo  int
,@TrnType  tinyint
,@TrnGLAccNoDr  int
,@TrnGLAccNoCr  int
,@TrnFlag  tinyint

AS
INSERT INTO A2ZTRANSACTION 
(
TrnDate  
,BatchNo  
,VoucherNo  
,GLAccNo  
,GLDebitAmt  
,GLCreditAmt  
,TrnDesc  
,TrnChqNo  
,TrnType  
,TrnGLAccNoDr  
,TrnGLAccNoCr  
,TrnFlag  
)
VALUES
(
 @TrnDate  
,@BatchNo  
,@VoucherNo  
,@GLAccNo  
,@GLDebitAmt  
,@GLCreditAmt  
,@TrnDesc   
,@TrnChqNo  
,@TrnType  
,@TrnGLAccNoDr  
,@TrnGLAccNoCr  
,@TrnFlag  

)
--SELECT @ID = CAST( SCOPE_IDENTITY() AS smallint )








GO
