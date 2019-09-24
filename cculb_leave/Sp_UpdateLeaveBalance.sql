USE A2ZHRMCUS
GO
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

CREATE PROCEDURE [dbo].[Sp_UpdateLeaveBalance] (@periodFlag INT)

--
--EXECUTE Sp_UpdateLeaveBalance 3
--
--

AS
BEGIN

DECLARE @strSQL NVARCHAR(MAX);
--DECLARE @periodFlag int;
DECLARE @CurrentrMonth INT;
DECLARE @EarnLeaveDay int;
DECLARE @CasualLeaveDay int;
DECLARE @EmpCode int;
DECLARE @EmpCodeChk int;
DECLARE @EmpGrade int;
DECLARE @EmpRelagion int;
DECLARE @EmpGender int;
DECLARE @ProcessDate smalldatetime;
DECLARE @EmpPerDate smalldatetime
DECLARE @EmpPerDateChk smalldatetime


-------
SET @CurrentrMonth = (SELECT CurrentMonth FROM A2ZCSMCUS..A2ZCSPARAMETER);
SET @ProcessDate = (SELECT ProcessDate FROM A2ZCSMCUS..A2ZCSPARAMETER);
SET @EmpPerDateChk = (SELECT DATEADD(Year,-1,@ProcessDate));
-------
PRINT @CurrentrMonth
PRINT @periodFlag
PRINT @ProcessDate
PRINT @EmpPerDateChk
---------
DECLARE EmpTable CURSOR FOR
SELECT EmpCode FROM A2ZHRMCUS..A2ZEMPLOYEE WHERE EmpServiceType = 1;

OPEN EmpTable;
FETCH NEXT FROM EmpTable INTO @EmpCode;

WHILE @@FETCH_STATUS = 0 
	BEGIN

			SET @EmpCodeChk = (SELECT EmpCode FROM A2ZHRMCUS..A2ZEMPLEAVEBALANCE WHERE  EmpCode=@EmpCode AND LeaveCode = 2);
    		
			IF(@EmpCodeChk IS NULL)
				BEGIN
					INSERT INTO A2ZHRMCUS..A2ZEMPLEAVEBALANCE (EmpCode,LeaveYear,LeaveCode,LeaveDays,LeaveBalDays)
					VALUES(@EmpCode,2019,2,0,0);
				END	
			SET @EmpCodeChk = (SELECT EmpCode FROM A2ZHRMCUS..A2ZEMPLEAVEBALANCE WHERE  EmpCode=@EmpCode AND LeaveCode = 4);
    		
			IF(@EmpCodeChk IS NULL)
				BEGIN
					INSERT INTO A2ZHRMCUS..A2ZEMPLEAVEBALANCE (EmpCode,LeaveYear,LeaveCode,LeaveDays,LeaveBalDays)
					VALUES(@EmpCode,2019,4,0,0);
				END

			SET @EmpCodeChk = (SELECT EmpCode FROM A2ZHRMCUS..A2ZEMPLEAVEBALANCE WHERE  EmpCode=@EmpCode AND LeaveCode = 5);
    		
			IF(@EmpCodeChk IS NULL)
				BEGIN
					INSERT INTO A2ZHRMCUS..A2ZEMPLEAVEBALANCE (EmpCode,LeaveYear,LeaveCode,LeaveDays,LeaveBalDays)
					VALUES(@EmpCode,2019,5,0,0);
				END

			SET @EmpCodeChk = (SELECT EmpCode FROM A2ZHRMCUS..A2ZEMPLEAVEBALANCE WHERE  EmpCode=@EmpCode AND LeaveCode = 6);
    		
			IF(@EmpCodeChk IS NULL)
				BEGIN
					INSERT INTO A2ZHRMCUS..A2ZEMPLEAVEBALANCE (EmpCode,LeaveYear,LeaveCode,LeaveDays,LeaveBalDays)
					VALUES(@EmpCode,2019,6,0,0);              
				END

			SET @EmpCodeChk = (SELECT EmpCode FROM A2ZHRMCUS..A2ZEMPLEAVEBALANCE WHERE  EmpCode=@EmpCode AND LeaveCode = 9);
    		
			IF(@EmpCodeChk IS NULL)
				BEGIN
					INSERT INTO A2ZHRMCUS..A2ZEMPLEAVEBALANCE (EmpCode,LeaveYear,LeaveCode,LeaveDays,LeaveBalDays)
					VALUES(@EmpCode,2019,9,0,0);
				END

FETCH NEXT FROM EmpTable INTO @EmpCode;
	END

CLOSE EmpTable; 
DEALLOCATE EmpTable;

------------------------------------
IF @periodFlag = 3
	BEGIN
		UPDATE A2ZHRMCUS..A2ZEMPLEAVEBALANCE SET LeaveBalDays = 8,LeaveDays =0 WHERE LeaveCode = 2
		UPDATE A2ZHRMCUS..A2ZEMPLEAVEBALANCE SET LeaveBalDays = 0,LeaveDays =0 WHERE LeaveCode = 4
		UPDATE A2ZHRMCUS..A2ZEMPLEAVEBALANCE SET LeaveBalDays = 90,LeaveDays =0 WHERE LeaveCode = 5 AND LeaveBalDays > 90
		UPDATE A2ZHRMCUS..A2ZEMPLEAVEBALANCE SET LeaveBalDays = 3,LeaveDays =0 WHERE LeaveCode = 9

DECLARE EmpTable CURSOR FOR
SELECT EmpCode,EmpGrade,EmpPerDate,EmpRelagion,EmpGender FROM A2ZHRMCUS..A2ZEMPLOYEE WHERE EmpServiceType = 1;

OPEN EmpTable;
FETCH NEXT FROM EmpTable INTO @EmpCode,@EmpGrade,@EmpPerDate,@EmpRelagion,@EmpGender;


WHILE @@FETCH_STATUS = 0 
	BEGIN
		
		IF @EmpRelagion = 3 OR @EmpRelagion = 4
			BEGIN
				UPDATE A2ZHRMCUS..A2ZEMPLEAVEBALANCE SET LeaveBalDays = 6,LeaveDays =0 WHERE LeaveCode = 9 AND EmpCode = @EmpCode
			END

		IF @EmpGender = 2 AND @EmpPerDate < @EmpPerDateChk
			BEGIN
				UPDATE A2ZHRMCUS..A2ZEMPLEAVEBALANCE SET LeaveBalDays = (180 - LeaveDays) WHERE LeaveDays BETWEEN 91 AND 180 AND LeaveCode = 6 AND EmpCode = @EmpCode
				UPDATE A2ZHRMCUS..A2ZEMPLEAVEBALANCE SET LeaveBalDays = (90 - LeaveDays) WHERE LeaveDays < 90 AND LeaveCode = 6 AND EmpCode = @EmpCode
				UPDATE A2ZHRMCUS..A2ZEMPLEAVEBALANCE SET LeaveBalDays = 90 WHERE LeaveDays = 90 AND LeaveCode = 6 AND EmpCode = @EmpCode
			END
--------	
FETCH NEXT FROM EmpTable INTO @EmpCode,@EmpGrade,@EmpPerDate,@EmpRelagion,@EmpGender;
	END

CLOSE EmpTable; 
DEALLOCATE EmpTable;
END
-------------
IF @periodFlag = 2 OR @periodFlag = 3
BEGIN
	IF @CurrentrMonth IN (1,2,4,5,7,8,10,11)
		BEGIN
			SET @EarnLeaveDay = 2
			SET @CasualLeaveDay = 1
		END

	IF @CurrentrMonth IN (3,6,9,12)
		BEGIN
			SET @EarnLeaveDay = 1
			SET @CasualLeaveDay = 1
		END

		UPDATE A2ZHRMCUS..A2ZEMPLEAVEBALANCE SET LeaveBalDays = LeaveBalDays + @EarnLeaveDay WHERE LeaveCode = 5
		UPDATE A2ZHRMCUS..A2ZEMPLEAVEBALANCE SET LeaveBalDays = LeaveBalDays + @CasualLeaveDay WHERE LeaveCode = 4
END

END