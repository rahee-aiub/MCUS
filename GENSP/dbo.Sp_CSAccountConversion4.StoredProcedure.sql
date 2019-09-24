USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSAccountConversion4]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE  [dbo].[Sp_CSAccountConversion4]


AS
BEGIN

/*

EXECUTE Sp_CSAccountConversion4

*/


DECLARE @CuType INT;
DECLARE @CuNo INT;
DECLARE @OCuNo INT;

DECLARE @MemNo INT;
DECLARE @MemOld2MemNo INT;


DECLARE wfAcc1Table CURSOR FOR
SELECT CuType,CuNo,OCuNo 
FROM WFO5000CUNO;

			
OPEN wfAcc1Table; 
FETCH NEXT FROM wfAcc1Table INTO @CuType,@CuNo,@OCuNo; 
WHILE @@FETCH_STATUS = 0 
BEGIN   
      
     

      DECLARE wfMemberTable CURSOR FOR
      SELECT MemNo,MemOld2MemNo 
      FROM A2ZMEMBER WHERE  MemOld2CuNo=@OCuNo;
    
      OPEN wfMemberTable;
      FETCH NEXT FROM wfMemberTable INTO @MemNo,@MemOld2MemNo;

      WHILE @@FETCH_STATUS = 0 
	  BEGIN
         

                 
        UPDATE A2ZACCOUNT SET CuType=@CuType,CuNo=@CuNo,MemNo=@MemNo WHERE AccType=11 AND CuNo = @OCuNo AND MemNo=@MemOld2MemNo;
--        UPDATE A2ZACCOUNT SET CuType=@CuType,CuNo=@CuNo,MemNo=@MemNo WHERE AccType=12 AND CuNo = @OCuNo AND MemNo=@MemOld2MemNo;
--        UPDATE A2ZACCOUNT SET CuType=@CuType,CuNo=@CuNo,MemNo=@MemNo WHERE AccType=13 AND CuNo = @OCuNo AND MemNo=@MemOld2MemNo;
--        UPDATE A2ZACCOUNT SET CuType=@CuType,CuNo=@CuNo,MemNo=@MemNo WHERE AccType=14 AND CuNo = @OCuNo AND MemNo=@MemOld2MemNo;
--        UPDATE A2ZACCOUNT SET CuType=@CuType,CuNo=@CuNo,MemNo=@MemNo WHERE AccType=15 AND CuNo = @OCuNo AND MemNo=@MemOld2MemNo;
--        UPDATE A2ZACCOUNT SET CuType=@CuType,CuNo=@CuNo,MemNo=@MemNo WHERE AccType=16 AND CuNo = @OCuNo AND MemNo=@MemOld2MemNo;
--        UPDATE A2ZACCOUNT SET CuType=@CuType,CuNo=@CuNo,MemNo=@MemNo WHERE AccType=17 AND CuNo = @OCuNo AND MemNo=@MemOld2MemNo;
--        UPDATE A2ZACCOUNT SET CuType=@CuType,CuNo=@CuNo,MemNo=@MemNo WHERE AccType=18 AND CuNo = @OCuNo AND MemNo=@MemOld2MemNo;
--        UPDATE A2ZACCOUNT SET CuType=@CuType,CuNo=@CuNo,MemNo=@MemNo WHERE AccType=20 AND CuNo = @OCuNo AND MemNo=@MemOld2MemNo;
--        UPDATE A2ZACCOUNT SET CuType=@CuType,CuNo=@CuNo,MemNo=@MemNo WHERE AccType=21 AND CuNo = @OCuNo AND MemNo=@MemOld2MemNo;
--        UPDATE A2ZACCOUNT SET CuType=@CuType,CuNo=@CuNo,MemNo=@MemNo WHERE AccType=23 AND CuNo = @OCuNo AND MemNo=@MemOld2MemNo;
--        UPDATE A2ZACCOUNT SET CuType=@CuType,CuNo=@CuNo,MemNo=@MemNo WHERE AccType=24 AND CuNo = @OCuNo AND MemNo=@MemOld2MemNo;
--        UPDATE A2ZACCOUNT SET CuType=@CuType,CuNo=@CuNo,MemNo=@MemNo WHERE AccType=51 AND CuNo = @OCuNo AND MemNo=@MemOld2MemNo;
--        UPDATE A2ZACCOUNT SET CuType=@CuType,CuNo=@CuNo,MemNo=@MemNo WHERE AccType=52 AND CuNo = @OCuNo AND MemNo=@MemOld2MemNo AND MemNo NOT BETWEEN 9001 AND 9196 AND MemNo NOT BETWEEN 9701 AND 9744;
--        UPDATE A2ZACCOUNT SET CuType=@CuType,CuNo=@CuNo,MemNo=@MemNo WHERE AccType=52 AND CuNo = @OCuNo AND MemNo=@MemOld2MemNo;
--        UPDATE A2ZACCOUNT SET CuType=@CuType,CuNo=@CuNo,MemNo=@MemNo WHERE AccType=53 AND CuNo = @OCuNo AND MemNo=@MemOld2MemNo;
--        UPDATE A2ZACCOUNT SET CuType=@CuType,CuNo=@CuNo,MemNo=@MemNo WHERE AccType=54 AND CuNo = @OCuNo AND MemNo=@MemOld2MemNo;

        FETCH NEXT FROM wfMemberTable INTO @MemNo,@MemOld2MemNo;
        END;
        CLOSE wfMemberTable; 
        DEALLOCATE wfMemberTable;
 


FETCH NEXT FROM wfAcc1Table INTO @CuType,@CuNo,@OCuNo; 


END;	       
CLOSE wfAcc1Table; 
DEALLOCATE wfAcc1Table;


END;























GO
