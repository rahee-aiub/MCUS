USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Updateimage]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE[dbo].[Updateimage](@CuType smallint,@CuNo int, @MemNo int ,@Image varbinary(max)) 
AS

BEGIN 


UPDATE dbo.uploadImg SET Image=@Image WHERE CuType=@CuType and CuNo=@CuNo and MemNo=@MemNo

END
GO
