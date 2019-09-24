INSERT INTO A2ZGLMCUS..A2ZCGLMST (GLCoNo, GLAccType, GLAccNo, GLRecType, GLPrtPos, GLAccDesc, GLAccMode, GLBgtType, 
			GLOpBal, GLDrSumC, GLDrSumT, GLCrSumC, GLCrSumT, GLClBal,GLHead, GLMainHead, GLSubHead, GLHeadDesc,
			GLMainHeadDesc, GLSubHeadDesc, GLOldAccNo, LastVoucherNo, GLBalanceType, Status)
 VALUES (1,2,20702002,2,6,'Stop Payment Salary Payable',0,'False',0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,20000000,
		 20700000,20702000,'20000000-Liability','20700000-OTHER PAYABLE','20702000-STAFF SALARY PAYABLE',0,0,0,1)

INSERT INTO A2ZGLMCUS..A2ZCGLMST (GLCoNo, GLAccType, GLAccNo, GLRecType, GLPrtPos, GLAccDesc, GLAccMode, GLBgtType, 
			GLOpBal, GLDrSumC, GLDrSumT, GLCrSumC, GLCrSumT, GLClBal,GLHead, GLMainHead, GLSubHead, GLHeadDesc,
			GLMainHeadDesc, GLSubHeadDesc, GLOldAccNo, LastVoucherNo, GLBalanceType, Status)
VALUES (1,2,20702003,2,6,'Others Salary Payable',0,'False',0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,20000000,
		20700000,20702000,'20000000-Liability','20700000-OTHER PAYABLE','20702000-STAFF SALARY PAYABLE',0,0,0,1)

INSERT INTO A2ZGLMCUS..A2ZCGLMST (GLCoNo, GLAccType, GLAccNo, GLRecType, GLPrtPos, GLAccDesc, GLAccMode, GLBgtType, 
			GLOpBal, GLDrSumC, GLDrSumT, GLCrSumC, GLCrSumT, GLClBal,GLHead, GLMainHead, GLSubHead, GLHeadDesc,
			GLMainHeadDesc, GLSubHeadDesc, GLOldAccNo, LastVoucherNo, GLBalanceType, Status)
VALUES (1,1,10606013,2,6,'Receivable Turst Board Found',0,'False',0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,10000000,
		10600000,10606000,'10000000-Asset','10600000-RECEIVABLE ACCOUNT','10606000-OTHER RECEIVABLE',0,0,0,1)

UPDATE A2ZCSMCUS..A2ZTRNCTRL SET A2ZCSMCUS..A2ZTRNCTRL.GLAccNoDR = 10606013,A2ZCSMCUS..A2ZTRNCTRL.GLAccNoCR = 20702001
WHERE (AccType = 63) AND (TrnRecDesc = 'Disbursement Amount') AND TrnType=3

DELETE FROM A2ZCSMCUST2015..A2ZCSOPBALANCE WHERE ACCTYPE = 63

INSERT INTO A2ZCSMCUST2015..A2ZCSOPBALANCE (TrnDate, CuType, CuNo, MemNo, AccType, AccNo, TrnCode, TrnAmount, CuOldNo)
SELECT TrnDate, CuType, CuNo, MemNo, AccType, AccNo, TrnCode, TrnAmount, CuOldNo
FROM TEST..A2ZCSOPBALANCE15

DELETE FROM A2ZCSMCUST2016..A2ZCSOPBALANCE WHERE ACCTYPE = 63

INSERT INTO A2ZCSMCUST2016..A2ZCSOPBALANCE (TrnDate, CuType, CuNo, MemNo, AccType, AccNo, TrnCode, TrnAmount, CuOldNo)
SELECT TrnDate, CuType, CuNo, MemNo, AccType, AccNo, TrnCode, TrnAmount, CuOldNo
FROM TEST..A2ZCSOPBALANCE16

TRUNCATE TABLE A2ZHKMCUS..A2ZDEPARTMENT

INSERT INTO A2ZHKMCUS..A2ZDEPARTMENT ( LocationCode, DepartmentCode, DepartmentName, DepartmentFlag, DepartmentType, DepartmentStatus, UserId, CreateDate)
SELECT  LocationCode, DepartmentCode, DepartmentName, DepartmentFlag, DepartmentType, DepartmentStatus, UserId, CreateDate
FROM TEST..A2ZDEPARTMENT

DELETE FROM A2ZCSMCUS..A2ZACCOUNT WHERE ACCTYPE = 63

INSERT INTO A2ZCSMCUS..A2ZACCOUNT (AccType, AccNo, CuType, CuNo, MemNo, AccOpenDate, AccStatus, AccStatusDate, AccStatusNote, AccStatusP, AccStatusDateP, AccMonthlyDeposit, AccPeriod, 
                      AccPrevPeriod, AccDebitFlag, AccIntFlag, AccMatureDate, AccPrevMatureDate, AccMatureAmt, AccIntType, AccAutoRenewFlag, AccIntRate, AccPrevIntRate, 
                      AccContractIntFlag, AccSpecialNote, AccCorrMemNo, AccCorrAccType, AccCorrAccNo, AccAutoTrfFlag, AccFixedAmt, AccFixedMthInt, AccBenefitDate, 
                      AccPrevBenefitDate, AccBalance, AccPrincipal, AccInterest, AccTotalDep, AccTotIntPaid, AccTotPenaltyPaid, AccLienAmt, AccOpeningBal, AccLastTrnTypeU, 
                      AccLastTrnDateU, AccLastTrnAmtU, AccLastTrnTypeS, AccLastTrnDateS, AccLastTrnAmtS, AccPrevTrnTypeU, AccPrevTrnDateU, AccPrevTrnAmtU, AccPrevTrnTypeS, 
                      AccPrevTrnDateS, AccPrevTrnAmtS, AccOrgAmt, AccFirstRenwlDate, AccRenwlDate, AccPrevRenwlDate, AccRenwlAmt, AccPrevRenwlAmt, AccAnniDate, 
                      AccPrevAnniDate, AccAnniAmt, AccPrevAnniAmt, AccIntWdrawn, AccPrevIntWdrawn, AccTotIntWdrawn, AccPrevTotIntWdrawn, AccNoAnni, AccPrevNoAnni, AccNoRenwl, 
                      AccPrevNoRenwl, AccNoBenefit, AccPrevNoBenefit, AccLastIntCr, AccPrevLastIntCr, AccPeriodDays, AccPeriodMonths, AccRaNetInt, AccAdjChgd, AccPrinChgd, 
                      AccPrevRaCurr, AccPrevRaLedger, AccLoanAppNo, AccLoanSancAmt, AccLoanSancDate, AccLoanExpiryDate, AccNoInstl, AccLoanInstlAmt, AccLoanLastInstlAmt, 
                      AccLoanFirstInstlDt, AccLoanGrace, AccDisbAmt, AccDisbDate, AccPrevDisbAmt, AccPrevDisbDate, AccODIntDate, AccPrevODIntDate, AccLoaneeAccType, 
                      AccLoaneeMemNo, AccProvBalance, AccProvCalDate, AccPrevProvBalance, AccPrevProvCalDate, AccAdjProvBalance, AccDuePrincAmt, AccDueIntAmt, 
                      AccDuePenalAmt, AccPrevDuePrincAmt, AccPrevDueIntAmt, AccLastDuePrincAmt, AccLastDueIntAmt, AccHoldIntAmt, AccAtyClass, AccOldNumber, OldCuNo, InputBy, 
                      VerifyBy, ApprovBy, InputByDate, VerifyByDate, ApprovByDate, ValueDate, AccCertNo, UserId, CreateDate, CalFDAmount, CalLastDate, CalFDate, CalTDate, 
                      CalNofDays, CalUptoMthProvision, CalCurrMthProvision, CalIntRate, CalOrgInterest, CalPaidInterest, CalInterest, CalPeriod, CalEncashmentInt, CalEncashment, 
                      CalProvAdjCr, CalProvAdjDr, CalClosingFees, AccOpBal, AccDRBal, AccCRBal, AccCloBal, OldMemNo, AccTrfAccNo)
SELECT AccType, AccNo, CuType, CuNo, MemNo, AccOpenDate, AccStatus, AccStatusDate, AccStatusNote, AccStatusP, AccStatusDateP, AccMonthlyDeposit, AccPeriod, 
                      AccPrevPeriod, AccDebitFlag, AccIntFlag, AccMatureDate, AccPrevMatureDate, AccMatureAmt, AccIntType, AccAutoRenewFlag, AccIntRate, AccPrevIntRate, 
                      AccContractIntFlag, AccSpecialNote, AccCorrMemNo, AccCorrAccType, AccCorrAccNo, AccAutoTrfFlag, AccFixedAmt, AccFixedMthInt, AccBenefitDate, 
                      AccPrevBenefitDate, AccBalance, AccPrincipal, AccInterest, AccTotalDep, AccTotIntPaid, AccTotPenaltyPaid, AccLienAmt, AccOpeningBal, AccLastTrnTypeU, 
                      AccLastTrnDateU, AccLastTrnAmtU, AccLastTrnTypeS, AccLastTrnDateS, AccLastTrnAmtS, AccPrevTrnTypeU, AccPrevTrnDateU, AccPrevTrnAmtU, AccPrevTrnTypeS, 
                      AccPrevTrnDateS, AccPrevTrnAmtS, AccOrgAmt, AccFirstRenwlDate, AccRenwlDate, AccPrevRenwlDate, AccRenwlAmt, AccPrevRenwlAmt, AccAnniDate, 
                      AccPrevAnniDate, AccAnniAmt, AccPrevAnniAmt, AccIntWdrawn, AccPrevIntWdrawn, AccTotIntWdrawn, AccPrevTotIntWdrawn, AccNoAnni, AccPrevNoAnni, AccNoRenwl, 
                      AccPrevNoRenwl, AccNoBenefit, AccPrevNoBenefit, AccLastIntCr, AccPrevLastIntCr, AccPeriodDays, AccPeriodMonths, AccRaNetInt, AccAdjChgd, AccPrinChgd, 
                      AccPrevRaCurr, AccPrevRaLedger, AccLoanAppNo, AccLoanSancAmt, AccLoanSancDate, AccLoanExpiryDate, AccNoInstl, AccLoanInstlAmt, AccLoanLastInstlAmt, 
                      AccLoanFirstInstlDt, AccLoanGrace, AccDisbAmt, AccDisbDate, AccPrevDisbAmt, AccPrevDisbDate, AccODIntDate, AccPrevODIntDate, AccLoaneeAccType, 
                      AccLoaneeMemNo, AccProvBalance, AccProvCalDate, AccPrevProvBalance, AccPrevProvCalDate, AccAdjProvBalance, AccDuePrincAmt, AccDueIntAmt, 
                      AccDuePenalAmt, AccPrevDuePrincAmt, AccPrevDueIntAmt, AccLastDuePrincAmt, AccLastDueIntAmt, AccHoldIntAmt, AccAtyClass, AccOldNumber, OldCuNo, InputBy, 
                      VerifyBy, ApprovBy, InputByDate, VerifyByDate, ApprovByDate, ValueDate, AccCertNo, UserId, CreateDate, CalFDAmount, CalLastDate, CalFDate, CalTDate, 
                      CalNofDays, CalUptoMthProvision, CalCurrMthProvision, CalIntRate, CalOrgInterest, CalPaidInterest, CalInterest, CalPeriod, CalEncashmentInt, CalEncashment, 
                      CalProvAdjCr, CalProvAdjDr, CalClosingFees, AccOpBal, AccDRBal, AccCRBal, AccCloBal, OldMemNo, AccTrfAccNo

FROM TEST..A2ZACCOUNT











