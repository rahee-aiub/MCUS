USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSCUApplicationDataInsert]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





create PROCEDURE [dbo].[Sp_CSCUApplicationDataInsert]
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

INSERT INTO dbo.A2ZCUAPPLICATION( CuType, CuTypeName, CuNo, CuName, CuOpDt, CuFlag, CuCertNo, CuAddL1, CuAddL2, CuAddL3, CuTel, CuMobile, CuFax, CuEmail, CuDivi, CuDist, 
                      CuUpzila, CuThana, CuProcFlag, CuProcDesc, CuStatus, CuStatusDate, CuPrevCuType, CuPrevCuTypeName, CuPrevCuNo, CuNewCuType, CuNewCuTypeName, 
                      CuNewCuNo, CuOldCuNo, GLCashCode, InputBy, VerifyBy, ApprovBy, InputByDate, VerifyByDate, ApprovByDate, ValueDate, UserId, CreateDate)


VALUES( @CuType, @CuTypeName, @CuNo,@CuName, @CuOpDt, @CuFlag, @CuCertNo, @CuAddL1, @CuAddL2, @CuAddL3, @CuTel, @CuMobile, @CuFax, @CuEmail, @CuDivi, @CuDist, 
                      @CuUpzila, @CuThana, @CuProcFlag, @CuProcDesc, @CuStatus, @CuStatusDate, @CuPrevCuType, @CuPrevCuTypeName, @CuPrevCuNo, @CuNewCuType, @CuNewCuTypeName, 
                      @CuNewCuNo, @CuOldCuNo, @GLCashCode, @InputBy, @VerifyBy, @ApprovBy, @InputByDate, @VerifyByDate, @ApprovByDate, @ValueDate, @UserId, @CreateDate)


END




GO
