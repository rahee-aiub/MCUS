USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoTrnLimit]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE  [dbo].[Sp_CSGetInfoTrnLimit]
@idno INT


AS
SELECT 

IdsNo
,LIdsCashCredit
,LIdsCashDebit
,LIdsTrfCredit
,LIdsTrfDebit


FROM A2ZTRNLIMIT  WHERE IdsNo = @idno








GO
