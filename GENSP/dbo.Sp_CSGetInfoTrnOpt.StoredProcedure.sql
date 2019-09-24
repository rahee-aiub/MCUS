USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoTrnOpt]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE  [dbo].[Sp_CSGetInfoTrnOpt]

@Typecode INT
,@functOpt INT
,@PayType INT


AS
SELECT 

AccType
,FuncOpt
,PayType
,TrnType
,TrnMode
,TrnLogic
,TrnValidation
,AccTypeDes


FROM A2ZTRNOPT  WHERE AccType = @Typecode AND 
                       PayType = @PayType AND 
                       FuncOpt = @functOpt 








GO
