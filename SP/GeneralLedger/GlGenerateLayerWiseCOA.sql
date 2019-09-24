USE [A2ZGLMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GlGenerateLayerWiseCOA]    Script Date: 04/29/2015 15:39:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_GlGenerateLayerWiseCOA](@nFlag INT)
AS

/*

EXECUTE Sp_GlGenerateLayerWiseCOA 0


*/

BEGIN

DELETE FROM A2ZCGLMSTLB;

INSERT INTO A2ZCGLMSTLB (GLCoNo,GLAccType,GLAccNo,GLRecType,GLPrtPos,GLAccDesc,CodeFlag)
SELECT GLCoNo,GLAccType,GLAccNo,GLRecType,GLPrtPos,GLAccDesc,1
FROM A2ZCGLMST WHERE GLPrtPos = 1;

DELETE FROM A2ZCGLMSTLC

INSERT INTO A2ZCGLMSTLC (GLCoNo,GLAccType,GLAccNo,GLRecType,GLPrtPos,GLAccDesc,CodeFlag)
SELECT GLCoNo,GLAccType,GLAccNo,GLRecType,GLPrtPos,GLAccDesc,2
FROM A2ZCGLMST WHERE GLPrtPos = 2;

UPDATE A2ZCGLMST SET GLHeadDesc=NULL,GLMainHeadDesc=NULL,GLSubHeadDesc=NULL;

UPDATE A2ZCGLMST SET GLHeadDesc =
(SELECT A2ZGLACCTYPE.AccountName FROM A2ZGLACCTYPE
WHERE A2ZCGLMST.GLAccType = A2ZGLACCTYPE.AccountType);

UPDATE A2ZCGLMST SET GLMainHeadDesc =
(SELECT A2ZCGLMSTLB.GLACCDESC FROM A2ZCGLMSTLB
WHERE A2ZCGLMST.GLMainHead = A2ZCGLMSTLB.GLACCNO);

UPDATE A2ZCGLMST SET GLSubHeadDesc =
(SELECT A2ZCGLMSTLC.GLACCDESC FROM A2ZCGLMSTLC
WHERE A2ZCGLMST.GLSubHead = A2ZCGLMSTLC.GLACCNO);

DELETE FROM A2ZCGLMSTLD;

INSERT INTO A2ZCGLMSTLD (GLCoNo,GLAccType,GLAccNo,GLRecType,GLPrtPos,GLAccDesc,GLOpBal,GLDrSumC,GLDrSumT,
GLCrSumC,GLCrSumT,GLClBal,GLHead,GLMainHead,GLSubHead,GLHeadDesc,GLMainHeadDesc,GLSubHeadDesc,CodeFlag)
SELECT GLCoNo,GLAccType,GLAccNo,GLRecType,GLPrtPos,GLAccDesc,GLOpBal,GLDrSumC,GLDrSumT,
GLCrSumC,GLCrSumT,GLClBal,GLHead,GLMainHead,GLSubHead,GLHeadDesc,GLMainHeadDesc,GLSubHeadDesc,3
FROM A2ZCGLMST WHERE GLPRTPOS = 6;

DELETE FROM A2ZCGLMSTLB;

INSERT INTO A2ZCGLMSTLB (GLCoNo,GLAccType,GLAccNo,GLRecType,GLPrtPos,GLAccDesc,
GLMainHead,GLSubHead,GLHeadDesc,GLMainHeadDesc,GLSubHeadDesc,CodeFlag)
SELECT GLCoNo,GLAccType,GLAccNo,GLRecType,GLPrtPos,GLAccDesc,
GLMainHead,GLSubHead,GLHeadDesc,GLMainHeadDesc,GLSubHeadDesc,1
FROM A2ZCGLMST WHERE GLPRTPOS = 1;

DELETE FROM A2ZCGLMSTLC;

INSERT INTO A2ZCGLMSTLC (GLCoNo,GLAccType,GLAccNo,GLRecType,GLPrtPos,GLAccDesc,
GLMainHead,GLSubHead,GLHeadDesc,GLMainHeadDesc,GLSubHeadDesc,CodeFlag)
SELECT GLCoNo,GLAccType,GLAccNo,GLRecType,GLPrtPos,GLAccDesc,
GLMainHead,GLSubHead,GLHeadDesc,GLMainHeadDesc,GLSubHeadDesc,2
FROM A2ZCGLMST WHERE GLPRTPOS = 2;

-- ========= UPDATE BALANCE FOR 1ST LAYER ==============
UPDATE A2ZGLACCTYPE SET GLOpBal = (SELECT SUM(GLOpBal) FROM A2ZCGLMST WHERE A2ZCGLMST.GLAccType = AccountType)

UPDATE A2ZGLACCTYPE SET GLDrSumC = (SELECT SUM(GLDrSumC) FROM A2ZCGLMST WHERE A2ZCGLMST.GLAccType = AccountType)

UPDATE A2ZGLACCTYPE SET GLDrSumT = (SELECT SUM(GLDrSumT) FROM A2ZCGLMST WHERE A2ZCGLMST.GLAccType = AccountType)

UPDATE A2ZGLACCTYPE SET GLCrSumC = (SELECT SUM(GLCrSumC) FROM A2ZCGLMST WHERE A2ZCGLMST.GLAccType = AccountType)

UPDATE A2ZGLACCTYPE SET GLCrSumT = (SELECT SUM(GLCrSumT) FROM A2ZCGLMST WHERE A2ZCGLMST.GLAccType = AccountType)

UPDATE A2ZGLACCTYPE SET GLClBal = (SELECT SUM(GLClBal) FROM A2ZCGLMST WHERE A2ZCGLMST.GLAccType = AccountType)
--- ========= END OF UPDATE BALANCE FOR 1ST LAYER ==============

--- ========= UPDATE BALANCE FOR 2ND LAYER ==============
UPDATE A2ZCGLMSTLB SET GLOpBal = (SELECT SUM(GLOpBal) FROM A2ZCGLMST WHERE A2ZCGLMST.GLMainHead = A2ZCGLMSTLB.GLMainHead);

UPDATE A2ZCGLMSTLB SET GLDrSumC = (SELECT SUM(GLDrSumC) FROM A2ZCGLMST WHERE A2ZCGLMST.GLMainHead = A2ZCGLMSTLB.GLMainHead);

UPDATE A2ZCGLMSTLB SET GLDrSumT = (SELECT SUM(GLDrSumT) FROM A2ZCGLMST WHERE A2ZCGLMST.GLMainHead = A2ZCGLMSTLB.GLMainHead);

UPDATE A2ZCGLMSTLB SET GLCrSumC = (SELECT SUM(GLCrSumC) FROM A2ZCGLMST WHERE A2ZCGLMST.GLMainHead = A2ZCGLMSTLB.GLMainHead);

UPDATE A2ZCGLMSTLB SET GLCrSumT = (SELECT SUM(GLCrSumT) FROM A2ZCGLMST WHERE A2ZCGLMST.GLMainHead = A2ZCGLMSTLB.GLMainHead);

UPDATE A2ZCGLMSTLB SET GLClBal = (SELECT SUM(GLClBal) FROM A2ZCGLMST WHERE A2ZCGLMST.GLMainHead = A2ZCGLMSTLB.GLMainHead);
---- ========= END OF UPDATE BALANCE FOR 2ND LAYER ==============

---- ========= UPDATE BALANCE FOR 3RD LAYER ==============
UPDATE A2ZCGLMSTLC SET GLOpBal = (SELECT SUM(GLOpBal) FROM A2ZCGLMST WHERE A2ZCGLMST.GLSubHead = A2ZCGLMSTLC.GLSubHead);

UPDATE A2ZCGLMSTLC SET GLDrSumC = (SELECT SUM(GLDrSumC) FROM A2ZCGLMST WHERE A2ZCGLMST.GLSubHead = A2ZCGLMSTLC.GLSubHead);

UPDATE A2ZCGLMSTLC SET GLDrSumT = (SELECT SUM(GLDrSumT) FROM A2ZCGLMST WHERE A2ZCGLMST.GLSubHead = A2ZCGLMSTLC.GLSubHead);

UPDATE A2ZCGLMSTLC SET GLCrSumC = (SELECT SUM(GLCrSumC) FROM A2ZCGLMST WHERE A2ZCGLMST.GLSubHead = A2ZCGLMSTLC.GLSubHead);

UPDATE A2ZCGLMSTLC SET GLCrSumT = (SELECT SUM(GLCrSumT) FROM A2ZCGLMST WHERE A2ZCGLMST.GLSubHead = A2ZCGLMSTLC.GLSubHead);

UPDATE A2ZCGLMSTLC SET GLClBal = (SELECT SUM(GLClBal) FROM A2ZCGLMST WHERE A2ZCGLMST.GLSubHead = A2ZCGLMSTLC.GLSubHead);
--- ========= END OF UPDATE BALANCE FOR 3RD LAYER ==============

--- ========= GENERATE WORKFILE ================================

DELETE FROM WFINCEXPREP

INSERT INTO WFINCEXPREP (GLAccType,GLAccNo,GLAccDesc,GLOpBal,GLDrSumC,GLDrSumT,GLCrSumC,GLCrSumT,GLClBal,CodeFlag)
SELECT GLAccType,GLAccNo,GLAccDesc,GLOpBal,GLDrSumC,GLDrSumT,GLCrSumC,GLCrSumT,GLClBal,CodeFlag
FROM A2ZCGLMSTLB

INSERT INTO WFINCEXPREP (GLAccType,GLAccNo,GLAccDesc,GLOpBal,GLDrSumC,GLDrSumT,GLCrSumC,GLCrSumT,GLClBal,CodeFlag)
SELECT GLAccType,GLAccNo,GLAccDesc,GLOpBal,GLDrSumC,GLDrSumT,GLCrSumC,GLCrSumT,GLClBal,CodeFlag
FROM A2ZCGLMSTLC

INSERT INTO WFINCEXPREP (GLAccType,GLAccNo,GLAccDesc,GLOpBal,GLDrSumC,GLDrSumT,GLCrSumC,GLCrSumT,GLClBal,CodeFlag)
SELECT GLAccType,GLAccNo,GLAccDesc,GLOpBal,GLDrSumC,GLDrSumT,GLCrSumC,GLCrSumT,GLClBal,CodeFlag
FROM A2ZCGLMSTLD

INSERT INTO WFINCEXPREP (GLAccType,GLAccDesc,GLClBal,GLAccNo,CodeFlag)
SELECT GLAccType,GLHeadDesc,SUM(GLClBal),0,0
FROM A2ZCGLMSTLD
GROUP BY GLAccType,GLHeadDesc
--- ========= END OF GENERATE WORKFILE ================================

END




