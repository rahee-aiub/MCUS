USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[insertimage]    Script Date: 1/4/2018 1:40:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[insertimage](@CuType smallint,@CuNo int, @MemNo int ,@Image varbinary(max)) 
  as insert into uploadImg (CuType,CUNO,MEMNO,Image) values(@CuType,@CuNo,@MemNo,@Image)

GO
