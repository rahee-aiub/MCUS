USE A2ZGLMCUS

TRUNCATE TABLE A2ZGLMCUS..WF_HelpBankTrans

INSERT INTO A2ZGLMCUS..WF_HelpBankTrans (TBFJVDATE,TBFGLCODEOLD,TBFAMOUNT)
SELECT TBFJVDATE,TBFCACCOUNT,ABS(TBFAMOUNTLCY) FROM A2ZCCULB..A2ZCGLTBF 
WHERE MONTH(TBFJVDATE) = 8 AND ((TBFCACCOUNT BETWEEN 100501 AND 100599) or 
(TBFCACCOUNT BETWEEN 100601 AND 100699) or 
(TBFCACCOUNT BETWEEN 100701 AND 100799) or 
(TBFCACCOUNT BETWEEN 100801 AND 100899));


UPDATE A2ZGLMCUS..WF_HelpBankTrans SET  A2ZGLMCUS..WF_HelpBankTrans.TBFCACCOUNTDESC = (SELECT A2ZCCULB..A2ZCGLMST.GLAccDesc 
FROM A2ZCCULB..A2ZCGLMST
WHERE A2ZGLMCUS..WF_HelpBankTrans.TBFGLCODEOLD=A2ZCCULB..A2ZCGLMST.GLAccNo);


UPDATE A2ZGLMCUS..WF_HelpBankTrans SET  A2ZGLMCUS..WF_HelpBankTrans.TBFGLCODENEW = (SELECT A2ZGLMCUS..A2ZCGLMST.GLAccNo 
FROM A2ZGLMCUS..A2ZCGLMST
WHERE A2ZGLMCUS..WF_HelpBankTrans.TBFGLCODEOLD=A2ZGLMCUS..A2ZCGLMST.GLOldAccNo);

