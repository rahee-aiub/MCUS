USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CS11GetInfoMember]    Script Date: 1/4/2018 1:40:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE  [dbo].[Sp_CS11GetInfoMember]
@CuType INT
,@CUNo INT
,@MemNo INT
,@RecFlag INT


AS

/*

EXECUTE Sp_CSGetInfoMember 3,5,2,0

*/


DECLARE @strSQL NVARCHAR(MAX);


SET @strSQL = '
SELECT 
CuType
,CuNo
,MemNo
,MemName
,MemFName
,MemMName
,MemSpouseName
,MemOccupation
,MemNationality
,MemGender
,MemReligion
,MemNature
,MemMaritalStatus
,MemOpenDate
,MemDOB
,MemPOB
,MemPreAdd1
,MemPreAdd2
,MemPreAdd3
,MemPreDivi
,MemPreDist
,MemPreUpzila
,MemPreThana
,MemPreTelephone
,MemPreMobile
,MemPreEmail
,MemPerAdd1
,MemPerAdd2
,MemPerAdd3
,MemPerDivi
,MemPerDist
,MemPerUpzila
,MemPerThana
,MemPerTelephone
,MemPerMobile
,MemPerEmail
,MemEmpNm
,MemEmpAdd
,MemIntroMNo1
,MemIntroNm1
,MemIntroMNo2
,MemIntroNm2
,MemNationalId
,MemPPNo
,MemPPIssDt
,MemPPExpDt
,MemPPIssPlace
,MemTin
,MemLastTaxDt
,MemType
,OldMemNo
FROM A2ZMEMBER WHERE '

IF @RecFlag = 1
   BEGIN
       SET @strSQL = @strSQL + ' CuType = ' + @CuType + ' AND CuNo = ' + @CUNo + ' AND OldMemNo = ' + @MemNo
   END
ELSE
   BEGIN
       SET @strSQL = @strSQL + ' CuType = ' + @CuType + ' AND CuNo = ' + @CUNo + ' AND MemNo = ' + @MemNo
   END

EXECUTE (@strSQL);











GO
