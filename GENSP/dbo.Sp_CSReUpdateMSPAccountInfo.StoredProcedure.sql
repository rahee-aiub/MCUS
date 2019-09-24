USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSReUpdateMSPAccountInfo]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CSReUpdateMSPAccountInfo] (@accNo BIGINT,@NoMonths int,@MthBenefit money,@ProvBalance money)

AS
BEGIN

DECLARE @calProvBalance money;
DECLARE @accAdjProvBalance money;

    
    SET @calProvBalance = (@MthBenefit * @NoMonths); 
    SET @accAdjProvBalance = (@calProvBalance - @ProvBalance);  
    

    UPDATE A2ZACCOUNT SET AccAdjProvBalance=@accAdjProvBalance, AccFixedMthInt = @MthBenefit
    WHERE AccNo = @accNo; 

--EXECUTE A2ZCSMCUS..Sp_CSReCalculatePensionDefaulter @accNo;
    
END

GO
