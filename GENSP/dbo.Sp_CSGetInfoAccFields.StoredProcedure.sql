USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoAccFields]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE  [dbo].[Sp_CSGetInfoAccFields]

 @MainCode INT
 ,@flag INT
           

AS
SELECT 

FieldsFlag
,Code
,Description


FROM A2ZACCFIELDS  WHERE Code = @MainCode AND 
                       FieldsFlag = @flag 








GO
