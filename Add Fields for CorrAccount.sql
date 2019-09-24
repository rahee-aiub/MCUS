USE [A2ZCSMCUS]
GO

ALTER TABLE A2ZCSMCUS..A2ZACCTYPE ADD  AccCorrType tinyint


UPDATE A2ZCSMCUS..A2ZACCFIELDS SET Description='Auto Trf. From Corr A/C'
WHERE FieldsFlag= 1 AND Code=23 

UPDATE A2ZCSMCUS..A2ZACCCTRL SET Description='Auto Trf. From Corr A/C'
WHERE ControlCode= 1 AND RecordCode=23 

