USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptBoothTrnCTRLReport]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Sp_rptBoothTrnCTRLReport](@fDate varchar(10),@CommonNo1 INT)
AS
BEGIN
IF @CommonNo1 = 1
   BEGIN
      SELECT ProcessDate, CashCodeNo, CashCodeName, TrnDebit, TrnCredit,StatusName
      FROM dbo.A2ZBTRNCTRL
      WHERE (ProcessDate = @fDate) 
   END
IF @CommonNo1 = 2
   BEGIN
      SELECT ProcessDate, CashCodeNo, CashCodeName, TrnDebit, TrnCredit,StatusName
      FROM dbo.A2ZBTRNCTRL
      WHERE (ProcessDate = @fDate AND (TrnDebit > 0 OR TrnCredit > 0) ) 
   END
IF @CommonNo1 = 3
   BEGIN
      SELECT ProcessDate, CashCodeNo, CashCodeName, TrnDebit, TrnCredit,StatusName
      FROM dbo.A2ZBTRNCTRL
      WHERE (ProcessDate = @fDate AND TrnDebit = 0 AND TrnCredit = 0 ) 
   END
IF @CommonNo1 = 4
   BEGIN
      SELECT ProcessDate, CashCodeNo, CashCodeName, TrnDebit, TrnCredit,StatusName
      FROM dbo.A2ZBTRNCTRL
      WHERE (ProcessDate = @fDate AND Status=0) 
   END
IF @CommonNo1 = 5
   BEGIN
      SELECT ProcessDate, CashCodeNo, CashCodeName, TrnDebit, TrnCredit,StatusName
      FROM dbo.A2ZBTRNCTRL
      WHERE (ProcessDate = @fDate AND Status<>0) 
   END
IF @CommonNo1 = 6
   BEGIN
      SELECT ProcessDate, CashCodeNo, CashCodeName, TrnDebit, TrnCredit,StatusName
      FROM dbo.A2ZBTRNCTRL
      WHERE (ProcessDate = @fDate AND Status=1) 
   END
IF @CommonNo1 = 7
   BEGIN
      SELECT ProcessDate, CashCodeNo, CashCodeName, TrnDebit, TrnCredit,StatusName
      FROM dbo.A2ZBTRNCTRL
      WHERE (ProcessDate = @fDate AND Status=2) 
   END

END









GO
