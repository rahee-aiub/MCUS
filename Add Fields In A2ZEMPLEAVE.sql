USE [A2ZHRMCUS]
GO

ALTER TABLE A2ZHRMCUS..A2ZEMPLEAVE ADD  LPurpose nvarchar(50)


ALTER TABLE A2ZHRMCUS..A2ZEMPLEAVE ADD  LNote nvarchar(100)

ALTER TABLE A2ZHRMCUS..A2ZEMPLEAVE ADD  LProcStat tinyint

ALTER TABLE A2ZHRMCUS..A2ZEMPLEAVE ADD  LRejectNote nvarchar(100)

ALTER TABLE A2ZHRMCUS..A2ZEMPLEAVE ADD  InputBy int

ALTER TABLE A2ZHRMCUS..A2ZEMPLEAVE ADD  InputByDate datetime

ALTER TABLE A2ZHRMCUS..A2ZEMPLEAVE ADD  CheckBy int

ALTER TABLE A2ZHRMCUS..A2ZEMPLEAVE ADD  CheckByDate datetime

ALTER TABLE A2ZHRMCUS..A2ZEMPLEAVE ADD  VerifyBy int

ALTER TABLE A2ZHRMCUS..A2ZEMPLEAVE ADD  VerifyByDate datetime

ALTER TABLE A2ZHRMCUS..A2ZEMPLEAVE ADD  ApprovBy int

ALTER TABLE A2ZHRMCUS..A2ZEMPLEAVE ADD  ApprovByDate datetime

ALTER TABLE A2ZHRMCUS..A2ZEMPLEAVE ADD  RejectBy int

ALTER TABLE A2ZHRMCUS..A2ZEMPLEAVE ADD  RejectByDate datetime