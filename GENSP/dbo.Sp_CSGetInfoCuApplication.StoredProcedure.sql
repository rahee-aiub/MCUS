USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoCuApplication]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE  [dbo].[Sp_CSGetInfoCuApplication]
@CUType INT
,@CUNo INT

AS
SELECT 


 Id
 ,CuType
 ,CuTypeName
 ,CuNo
 ,CuName
 ,CuOpDt
 ,CuFlag
 ,CuCertNo
 ,CuAddL1
 ,CuAddL2
 ,CuAddL3
 ,CuTel
 ,CuMobile
 ,CuFax
 ,CuEmail
 ,CuDivi
 ,CuDist
 ,CuUpzila
 ,CuThana
 ,CuProcFlag
 ,CuProcDesc
 ,CuStatus
 ,CuStatusDate
 ,ValueDate
 ,CreateDate
 ,InputBy
 ,VerifyBy
 ,ApprovBy
 ,InputByDate
 ,VerifyByDate
 ,ApprovByDate
 ,GLCashCode

FROM A2ZCUAPPLICATION  WHERE CuType = @CUType and CuNo = @CUNo










GO
