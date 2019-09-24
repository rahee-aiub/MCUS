USE [A2ZGLMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GLAddTransaction]    Script Date: 04/29/2015 15:39:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO










CREATE PROCEDURE  [dbo].[Sp_GLAddTransaction] 
 @VoucherDate  smalldatetime
,@BatchNo  int
,@VoucherNo  varchar(10)
,@GLAccNo  int
,@GLDebitAmt  money
,@GLCreditAmt  money
,@TrnDesc   varchar(50)
,@TrnMode  int
,@TrnType  tinyint
,@TrnDrCr  tinyint
,@TrnGLAccNoDr  int
,@TrnGLAccNoCr  int
,@TrnFlag  tinyint
,@GLAmount money
,@TrnCSGL  int
,@FromCashCode int
,@TrnModule tinyint
,@UserID   int

AS
INSERT INTO WFGLTrannsaction 
(
 VoucherDate 
,BatchNo  
,VoucherNo  
,GLAccNo  
,GLDebitAmt  
,GLCreditAmt  
,TrnDesc  
,TrnMode  
,TrnType  
,TrnDrCr  
,TrnGLAccNoDr  
,TrnGLAccNoCr  
,TrnFlag
,GLAmount
,TrnCSGL
,FromCashCode
,TrnModule
,UserID  
)
VALUES
(
 @VoucherDate  
,@BatchNo  
,@VoucherNo  
,@GLAccNo  
,@GLDebitAmt  
,@GLCreditAmt  
,@TrnDesc   
,@TrnMode  
,@TrnType  
,@TrnDrCr  
,@TrnGLAccNoDr  
,@TrnGLAccNoCr  
,@TrnFlag  
,@GLAmount
,@TrnCSGL 
,@FromCashCode
,@TrnModule
,@UserID 
)
--SELECT @ID = CAST( SCOPE_IDENTITY() AS smallint )



















