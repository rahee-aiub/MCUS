TRUNCATE TABLE A2ZSTMCUS..A2ZSTTRANSACTION

UPDATE A2ZSTMCUS..A2ZSTMST SET STKUnitQty = 0,STKUnitAvgCost = 0,STKUnitAvgCostDate = NULL,STKUnitSalePrice = 0

UPDATE A2ZSTMCUST2018..A2ZSTOPAVGCOST SET STKUnitQty = 0,STKUnitCost = 0

UPDATE A2ZSTMCUST2018..A2ZSTOPBALANCE SET STKUnitQty = 0

TRUNCATE TABLE A2ZSTMCUST2018..A2ZSTTRANSACTION


