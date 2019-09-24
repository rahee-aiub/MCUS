USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptVerifyReport]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sp_rptVerifyReport](@CommonNo3 int, @CommonNo2 int )
AS
BEGIN
SELECT     dbo.A2ZCUAPPLICATION.CuType, dbo.A2ZCUAPPLICATION.CuNo, dbo.A2ZCUAPPLICATION.CuName, dbo.A2ZCUAPPLICATION.CuOpDt, 
                      dbo.A2ZCUAPPLICATION.CuFlag, dbo.A2ZCUAPPLICATION.CuCertNo, dbo.A2ZCUAPPLICATION.CuAddL1, dbo.A2ZCUAPPLICATION.CuAddL2, 
                      dbo.A2ZCUAPPLICATION.CuAddL3, dbo.A2ZCUAPPLICATION.CuTel, dbo.A2ZCUAPPLICATION.CuMobile, dbo.A2ZCUAPPLICATION.CuFax, 
                      dbo.A2ZCUAPPLICATION.CuEmail, dbo.A2ZCUAPPLICATION.InputBy, dbo.A2ZCUAPPLICATION.VerifyBy, dbo.A2ZCUAPPLICATION.ApprovBy, 
                      dbo.A2ZCUAPPLICATION.InputByDate, dbo.A2ZCUAPPLICATION.VerifyByDate, dbo.A2ZCUAPPLICATION.ApprovByDate, dbo.A2ZCUAPPLICATION.CuDivi, 
                      dbo.A2ZCUAPPLICATION.CuDist, dbo.A2ZCUAPPLICATION.CuThana, dbo.A2ZCUAPPLICATION.CuUpzila, dbo.A2ZCUAPPLICATION.GLCashCode, 
                      A2ZSYSIDS_1.IdsName AS InputByName, A2ZSYSIDS_2.IdsName AS VerifyByName, dbo.A2ZSYSIDS.IdsName AS ApprovedByName, 
                      A2ZGLMCUS.dbo.A2ZCGLMST.GLAccDesc, A2ZHKMCUS.dbo.A2ZDIVISION.DiviDescription, A2ZHKMCUS.dbo.A2ZDISTRICT.DistDescription, 
                      A2ZHKMCUS.dbo.A2ZTHANA.ThanaDescription, A2ZHKMCUS.dbo.A2ZUPZILA.UpzilaDescription
FROM         dbo.A2ZCUAPPLICATION LEFT OUTER JOIN
                      A2ZHKMCUS.dbo.A2ZTHANA ON dbo.A2ZCUAPPLICATION.CuDivi = A2ZHKMCUS.dbo.A2ZTHANA.DiviOrgCode AND 
                      dbo.A2ZCUAPPLICATION.CuDist = A2ZHKMCUS.dbo.A2ZTHANA.DistOrgCode AND dbo.A2ZCUAPPLICATION.CuUpzila = A2ZHKMCUS.dbo.A2ZTHANA.UpzilaOrgCode AND
                       dbo.A2ZCUAPPLICATION.CuThana = A2ZHKMCUS.dbo.A2ZTHANA.ThanaOrgCode LEFT OUTER JOIN
                      A2ZHKMCUS.dbo.A2ZUPZILA ON dbo.A2ZCUAPPLICATION.CuDivi = A2ZHKMCUS.dbo.A2ZUPZILA.DiviOrgCode AND 
                      dbo.A2ZCUAPPLICATION.CuDist = A2ZHKMCUS.dbo.A2ZUPZILA.DistOrgCode AND 
                      dbo.A2ZCUAPPLICATION.CuUpzila = A2ZHKMCUS.dbo.A2ZUPZILA.UpzilaOrgCode LEFT OUTER JOIN
                      A2ZHKMCUS.dbo.A2ZDISTRICT ON dbo.A2ZCUAPPLICATION.CuDivi = A2ZHKMCUS.dbo.A2ZDISTRICT.DiviOrgCode AND 
                      dbo.A2ZCUAPPLICATION.CuDist = A2ZHKMCUS.dbo.A2ZDISTRICT.DistOrgCode LEFT OUTER JOIN
                      A2ZHKMCUS.dbo.A2ZDIVISION ON dbo.A2ZCUAPPLICATION.CuDivi = A2ZHKMCUS.dbo.A2ZDIVISION.DiviOrgCode LEFT OUTER JOIN
                      A2ZGLMCUS.dbo.A2ZCGLMST ON dbo.A2ZCUAPPLICATION.GLCashCode = A2ZGLMCUS.dbo.A2ZCGLMST.GLAccNo LEFT OUTER JOIN
                      dbo.A2ZSYSIDS ON dbo.A2ZCUAPPLICATION.ApprovBy = dbo.A2ZSYSIDS.IdsNo LEFT OUTER JOIN
                      dbo.A2ZSYSIDS AS A2ZSYSIDS_2 ON dbo.A2ZCUAPPLICATION.VerifyBy = A2ZSYSIDS_2.IdsNo LEFT OUTER JOIN
                      dbo.A2ZSYSIDS AS A2ZSYSIDS_1 ON dbo.A2ZCUAPPLICATION.InputBy = A2ZSYSIDS_1.IdsNo
WHERE (CuType=@CommonNo3)AND (CuNo=@CommonNo2) 

END











GO
