USE [A2ZGLMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GlUpdateTransaction]    Script Date: 04/29/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Sp_GlUpdateTransaction](@userID INT, @vchNo nvarchar(20), @ProcStat smallint) 
AS
BEGIN


INSERT INTO  A2ZCSMCUS.dbo.A2ZTRANSACTION( TrnDate, BatchNo,  VoucherNo, GLAccNo, GLDebitAmt, GLCreditAmt, TrnDesc,  TrnType, TrnDrCr, GLAmount, TrnGLAccNoDr, TrnGLAccNoCr, TrnFlag, 
                      TrnCSGL, FromCashCode, TrnProcStat, TrnModule, UserID)SELECT     VoucherDate, BatchNo,  @vchNo, GLAccNo, GLDebitAmt, GLCreditAmt, TrnDesc,  TrnType, TrnDrCr, GLAmount, TrnGLAccNoDr, TrnGLAccNoCr, TrnFlag, 
                      TrnCSGL, FromCashCode, @ProcStat, TrnModule, @userID
FROM      A2ZGLMCUS.dbo.WFGLTrannsaction

END









