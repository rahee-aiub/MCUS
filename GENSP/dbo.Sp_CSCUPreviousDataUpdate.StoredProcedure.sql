USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSCUPreviousDataUpdate]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_CSCUPreviousDataUpdate]
(
	@CuType	smallint,
	@CuNo	int,
	@CuReguCuType	smallint,
	@CuReguCuTypeName	nvarchar(50),
	@CuReguCuNo	int
	)

AS

BEGIN
	UPDATE dbo.A2ZCUNION SET
	CuType=@CuType,
	CuNo=@CuNo,
	CuReguCuType=@CuReguCuType,
	CuReguCuTypeName=@CuReguCuTypeName,
	CuReguCuNo=@CuReguCuNo
	
   WHERE  CuType = @CuType and CuNo =@CuNo

END






GO
