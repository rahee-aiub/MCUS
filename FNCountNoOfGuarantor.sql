USE A2ZCSMCUS
GO


ALTER FUNCTION [dbo].[FNCountNoOfGuarantor] (@CuType INT, @CuNo INT, @MemNo INT, @nFlag INT)

RETURNS INT

-- @nFlag = 1 = Share Guarantor
-- @nFlag = 2 = Account Guarantor
-- @nFlag = 3 = Property Guarantor
--
--	@Result = Total No. of Guarantor
--
--  
--  SELECT DBO.FNCountNoOfGuarantor(3,23,0,1) 
--  SELECT DBO.FNCountNoOfGuarantor(3,23,0,2) 
--

AS
BEGIN


	DECLARE @Result INT;

	SET @Result = 0;

	IF @nFlag = 1
		BEGIN
			SET @Result = (SELECT COUNT(AccNo) FROM WFA2ZSHGUAR
			WHERE @CuType = CuType AND CuNo = @CuNo AND MemNo = @MemNo);
		END 

	IF @nFlag = 2
		BEGIN
			SET @Result = (SELECT COUNT(AccNo) FROM WFA2ZACGUAR
			WHERE @CuType = CuType AND CuNo = @CuNo AND MemNo = @MemNo);
		END 

	RETURN @Result;
END


