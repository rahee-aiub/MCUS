USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoAccNominee]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE  [dbo].[Sp_CSGetInfoAccNominee]

@NomNo INT
,@acctype INT
,@MemNo INT
,@AccNo BIGINT
,@CuType INT
,@CuNo INT


AS
SELECT 

CuType
,CuNo
,MemNo
,AccType
,AccNo
,NomNo
,NomName
,NomAdd1
,NomAdd2
,NomAdd3
,NomTel
,NomMobile
,NomEmail
,NomDivi
,NomDist
,NomUpzila
,NomThana
,NomRela
,NomSharePer

FROM A2ZACCNOM  WHERE NomNo = @NomNo AND 
                      AccType = @acctype AND 
                      MemNo = @MemNo AND 
                      AccNo = @AccNo AND 
                      CuType = @CuType AND 
                      CuNo = @CuNo 








GO
