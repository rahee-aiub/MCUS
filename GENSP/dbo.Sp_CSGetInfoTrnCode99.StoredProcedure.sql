USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoTrnCode99]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE  [dbo].[Sp_CSGetInfoTrnCode99]

@PayType INT

AS
SELECT 

TrnCode
,TrnDes
,AccType
,AccTypeMode


FROM A2ZTRNCODE  WHERE PayType = @PayType 








GO
