USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoTrnCtrl]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE  [dbo].[Sp_CSGetInfoTrnCtrl]

@FuncOpt INT
,@TranCode INT
,@paytype INT
,@trntype INT
,@TranMode INT

AS
SELECT 

FuncOpt
,TrnCode
,TrnDesc
,AccType
,AccTypeClass
,AccTypeMode        
,PayType
,TrnType
,TrnMode
,TrnRecDesc
,TrnLogic
,ShowInt
,RecMode
,GLAccNoCR
,GLAccNoDR
,TrnPayment


FROM A2ZTRNCTRL  WHERE FuncOpt = @FuncOpt AND 
                       TrnCode = @TranCode AND 
                       PayType = @paytype AND 
                       TrnType = @trntype AND 
                       TrnMode = @TranMode 








GO
