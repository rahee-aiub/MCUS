SELECT VchNo,VoucherNo,COUNT(AccNo) FROM A2ZTRANSACTION
WHERE PayType = 403 AND TrnFlag = 0 AND 
TrnCSGL = 0 AND ShowInterest <> 2 AND
TrnProcStat = 0
GROUP BY VchNo,VoucherNo
HAVING COUNT(AccNo) > 1