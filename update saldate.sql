
USE [A2ZHRMCUS]
GO
ALTER TABLE A2ZHRMCUS..A2ZEMPTSALARY  ADD  SalDate smalldatetime
GO
DECLARE @TrnDate smalldatetime;

SET @TrnDate = (SELECT ProcessDate FROM A2ZHRMCUS..A2ZHRPARAMETER);


UPDATE A2ZEMPTSALARY SET SalDate= @TrnDate;

