USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoPayType]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE  [dbo].[Sp_CSGetInfoPayType]

@ClassCode INT
,@Typecode INT


AS
SELECT 

PayType
,PayTypeDes
,AtyClass
,PayMode


FROM A2ZPAYTYPE  WHERE AtyClass = @ClassCode AND 
                       PayType = @Typecode 








GO
