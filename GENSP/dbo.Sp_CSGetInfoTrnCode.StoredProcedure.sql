USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoTrnCode]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE  [dbo].[Sp_CSGetInfoTrnCode]

@TrnCode INT

AS
SELECT 

TrnCode
,TrnDes
,AccType
,AccTypeMode
,PayType


FROM A2ZTRNCODE  WHERE TrnCode = @TrnCode 











GO
