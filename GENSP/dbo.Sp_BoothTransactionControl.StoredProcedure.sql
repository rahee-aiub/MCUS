USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_BoothTransactionControl]    Script Date: 1/4/2018 1:40:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_BoothTransactionControl] 

AS
BEGIN

DECLARE @trnDate smalldatetime;

SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

INSERT INTO A2ZCSMCUS.dbo.A2ZBTRNCTRL(CashCodeNo,CashCodeName) 
 SELECT GLAccNo,GLAccDesc 
   FROM A2ZGLMCUS.dbo.A2ZCGLMST 
       WHERE GLRecType = 2 and GLSubHead = 10101000;

UPDATE A2ZCSMCUS.dbo.A2ZBTRNCTRL SET ProcessDate=@trnDate WHERE ProcessDate IS NULL;

UPDATE A2ZCSMCUS..A2ZUSERCASHCODE SET Status=0; 

END

GO
