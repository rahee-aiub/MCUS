USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSCUApplicationDataUpdate]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






create PROCEDURE [dbo].[Sp_CSCUApplicationDataUpdate]
(
		
	
	@CuType	smallint,
	@CuTypeName	nvarchar(25),
	@CuNo	int,
	@CuName	nvarchar(50),
	@CuOpDt	smalldatetime,
	@CuFlag	smallint,
	@CuCertNo	int,
	@CuAddL1	nvarchar(50),
	@CuAddL2	nvarchar(50),
	@CuAddL3	nvarchar(50),
	@CuTel	nvarchar(50),
	@CuMobile	nvarchar(50),
	@CuFax	nvarchar(50),
	@CuEmail	nvarchar(50),
	@CuDivi	int,
	@CuDist	int,
	@CuUpzila	int,
	@CuThana	int,
	@CuProcFlag	smallint,
	@CuProcDesc	nchar(15),
	@CuStatus	smallint,
	@CuStatusDate	smalldatetime,
	@CuPrevCuType	smallint,
	@CuPrevCuTypeName	nvarchar(25),
	@CuPrevCuNo	int,
	@CuNewCuType	smallint,
	@CuNewCuTypeName	nvarchar(50),
	@CuNewCuNo	int,
	@CuOldCuNo	int,
	@GLCashCode	int,
	@InputBy	smallint,
	@VerifyBy	smallint,
	@ApprovBy	smallint,
	@InputByDate	datetime,
	@VerifyByDate	datetime,
	@ApprovByDate	datetime,
	@ValueDate	smalldatetime,
	@UserId	smallint,
	@CreateDate	datetime

)

AS

BEGIN
	UPDATE dbo.A2ZCUAPPLICATION SET
	CuType=@CuType,
	CuTypeName=@CuTypeName,
	CuNo=@CuNo,
	CuName=@CuName,
	CuOpDt=@CuOpDt,
	CuFlag=@CuFlag,
	CuCertNo=@CuCertNo,
	CuAddL1=@CuAddL1,
	CuAddL2=@CuAddL2,
	CuAddL3=@CuAddL3,
	CuTel=@CuTel,
	CuMobile=@CuMobile,
	CuFax=@CuFax,
	CuEmail=@CuEmail,
	CuDivi=@CuDivi,
	CuDist=@CuDist,
	CuUpzila=@CuUpzila,
	CuThana=@CuThana,
	CuProcFlag=@CuProcFlag,
	CuProcDesc=@CuProcDesc,
	CuStatus=@CuStatus,
	CuStatusDate=@CuStatusDate,
	CuPrevCuType=@CuPrevCuType,
	CuPrevCuTypeName=@CuPrevCuTypeName,
	CuPrevCuNo=@CuPrevCuNo,
	CuNewCuType=@CuNewCuType,
	CuNewCuTypeName=@CuNewCuTypeName,
	CuNewCuNo=@CuNewCuNo,
	CuOldCuNo=@CuOldCuNo,
	GLCashCode=@GLCashCode,
	InputBy=@InputBy,
	VerifyBy=@VerifyBy,
	ApprovBy=@ApprovBy,
	InputByDate=@InputByDate,
	VerifyByDate=@VerifyByDate,
	ApprovByDate=@ApprovByDate,
	ValueDate=@ValueDate,
	UserId=@UserId,
	CreateDate=@CreateDate

   WHERE  CuType = @CuType and CuNo =@CuNo

END





GO
