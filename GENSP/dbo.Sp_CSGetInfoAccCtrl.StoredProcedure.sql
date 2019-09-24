USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoAccCtrl]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE  [dbo].[Sp_CSGetInfoAccCtrl]

@MainCode INT
,@CtrlCode INT
,@Rcode INT


AS
SELECT 

ControlCode
,ProductCode
,RecordCode
,RecordFlag
,FuncFlag
,Description


FROM A2ZACCCTRL  WHERE ProductCode = @MainCode AND 
                       ControlCode = @CtrlCode AND 
                       RecordCode = @Rcode 








GO
