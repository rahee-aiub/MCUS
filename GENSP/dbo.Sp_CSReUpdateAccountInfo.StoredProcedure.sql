USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSReUpdateAccountInfo]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CSReUpdateAccountInfo] (@accNo BIGINT,@openDate varchar(10),@lTrnDt varchar(10),@period int,@monthlydep money,@matureAmt money)

AS
BEGIN

DECLARE @accMatureDate smalldatetime;

    
    SET @accMatureDate = (DATEADD(month,@period,@openDate));   
    

    UPDATE A2ZACCOUNT SET AccOpenDate=@openDate, AccPeriod=@period, AccMonthlyDeposit=@monthlydep, AccMatureDate=@accMatureDate, AccLastTrnDateU=@lTrnDt, AccMatureAmt=@matureAmt
    WHERE AccNo = @accNo; 

EXECUTE A2ZCSMCUS..Sp_CSReCalculatePensionDefaulter @accNo;
    
END

GO
