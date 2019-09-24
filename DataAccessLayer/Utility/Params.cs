namespace DataAccessLayer.Utility
{
    /// <summary>
    /// Saaif: 20130924
    /// </summary>
    public class Params
    {
        #region ERPMODULE

        public const string SYS_SELECT_MODULE = "LoadSelectedModule";
        public const string SYS_SELECT_DBNAME = "ErpDatabase";

        #region USERID_CONSTANT

        public const string SYS_USER_ID = "UserId";
        public const string SYS_USER_NAME = "UserName";
        public const string SYS_USER_LEVEL = "UserLevel";
        public const string SYS_USER_LOGIN_FLAG = "UserLogInFlag";
        public const string SYS_USER_LOCK_FLAG = "UserLockFlag";
        public const string SYS_USER_PERMISSION = "UserIdsFlag";
        public const string SYS_USER_TYPE = "UserType";
        public const string SYS_USER_STATUS = "UserStatus";
        public const string SYS_USER_EMP_CODE = "UserEmpCode";
        public const string SYS_USER_GLCASHCODE = "GLCashCode";
        public const string SYS_USER_IP = "UserIP";
        public const string SYS_USER_SERVER_IP = "UserServerIP";
        public const string SYS_USER_SERVER_NAME = "UserServerName";

        #endregion END_USERID_CONSTANT

        #region UNIT_CONSTANT

        public const string SYS_UNIT_NO = "PrmUnitNo";
        public const string SYS_UNIT_NAME = "PrmUnitName";

        #endregion END_UNIT_CONSTANT

        #endregion END_ERPMODULE

        #region All Report Parameter

        public const string REPORT_FILE_NAME_KEY = "ReportFileName";
        public const string REPORT_DATABASE_NAME_KEY = "DatabaseName";

        public const string WHERE_CLAUSE = "@WhereClause";
        public const string LEDGER_TYPE = "@LedgerType";
        public const string LEDGER_NO = "@LedgerNo";
        public const string NO_DATA_FOUND_COMPANY_ID = "@CompanyID";
        public const string BRNO_ID = "@BRNO";
        public const string CONO_ID = "@CONO";
        public const string COMPANY_NAME = "@CompanyName";
        public const string BRANCH_NAME = "@BranchName";
        public const string BRANCH_ADDRESS = "@BranchAddress";
       
        public const string CUNO = "@CuNo";
        public const string CUPTYE = "@CuType";
        public const string ACCTYPE = "@AccType";
        public const string ACCNO = "@AccNo";
        public const string MEMNO = "@MemNo";
        public const string ACCOUNTCODE = "@AccountCode";
        public const string NFLAG = "@nFlag";

        public const string COMMON_NAME = "@CommonName";
        public const string COMMON_NAME1 = "@CommonName1";
        public const string COMMON_NAME2 = "@CommonName2";
        public const string COMMON_NAME3 = "@CommonName3";
        public const string COMMON_NAME4 = "@CommonName4";
        public const string COMMON_NAME5 = "@CommonName5";
        public const string COMMON_NAME6 = "@CommonName6";
        public const string COMMON_NAME7 = "@CommonName7";
        public const string COMMON_NAME8 = "@CommonName8";
        public const string COMMON_NAME9 = "@CommonName9";
        public const string COMMON_NAME10 = "@CommonName10";
        public const string COMMON_NAME11 = "@CommonName11";
        public const string COMMON_NAME12 = "@CommonName12";
        public const string COMMON_NAME13 = "@CommonName13";
        public const string COMMON_NAME14 = "@CommonName14";

        public const string COMMON_FDATE = "@fDate";
        public const string COMMON_TDATE = "@tDate";

        //......................................Multiuser Store Procedure...........................
        public const string MU_USER_ID = "@UserId";
        public const string MU_ACC_CLASS = "@AccClass";
        public const string MU_FUNC_99 = "@Func99";
        public const string MU_F_CASH_CODE = "@FcashCode";
        public const string MU_TRN_TYPE = "@TrnType";
        public const string MU_TELLER = "@teller";
        public const string MU_TRN_MODE = "@TrnMode";
        public const string MU_TRN_NATURE = "@TrnNature";
        public const string MU_TRN_MODULE = "@TrnModule";
        public const string MU_TRAN_SW = "@TranSw";

        public const string MU_ACC_STAT_FLAG = "@AccStatFlag";
        public const string MU_BALANCE_FLAG = "@BalanceFlag";
        public const string MU_F_BALANCE = "@fBalance";
        public const string MU_T_BALANCE = "@tBalance";
        public const string MU_CODE_TYPE = "@CodeType";
        public const string MU_TRN_CODE = "@TrnCode";
        public const string MU_CONTRACT_INT = "@ContractInt";

                    //==========  For GL ===========
        public const string MU_CASH_CODE = "@CashCode";
        public const string MU_CS_GL_TRN = "@CSGLTransaction";
        public const string MU_AUTO_TRN = "@AutoTransaction";
        public const string MU_TRAN_AMT = "@TransactionAmount";
                    //==========  For GL ===========

        //......................................End of Multiuser Store Procedure....................

        //......................................ONI.................................................
        public const string TRANSFER_TYPE = "@TrnasferType";
        public const string TRNSL_CODE = "@TrnSlNo";
        //......................................ONI.................................................
        public const string COMMON_NO1 = "@CommonNo1";
        public const string COMMON_NO2 = "@CommonNo2";
        public const string COMMON_NO3 = "@CommonNo3";
        public const string COMMON_NO4 = "@CommonNo4";
        public const string COMMON_NO5 = "@CommonNo5";
        public const string COMMON_NO6 = "@CommonNo6";
        public const string COMMON_NO7 = "@CommonNo7";
        public const string COMMON_NO8 = "@CommonNo8";
        public const string COMMON_NO9 = "@CommonNo9";
        public const string COMMON_NO10 = "@CommonNo10";
        public const string COMMON_NO11 = "@CommonNo11";
        public const string COMMON_NO12 = "@CommonNo12";
        public const string COMMON_NO13 = "@CommonNo13";
        public const string COMMON_NO14 = "@CommonNo14";
        public const string COMMON_NO15 = "@CommonNo15";
        public const string COMMON_NO16 = "@CommonNo16";
        public const string COMMON_NO17 = "@CommonNo17";
       
        public const string EMP_NO = "@EmpNo";
        public const string REQ_NO = "@ReqNo";
        public const string GRN_NO = "@GRNNo";
        public const string ORDER_NO = "@OrdNo";
        public const string ITEM_TYPE = "@nItemType";
        public const string ITEM_SOURCE = "@nItemSource";
        public const string PROD_CODE = "@ProdCode";
        public const string BATCH_NO = "@BatchNo";
        public const string VOUCHER_NO = "@VchNo";
        public const string FIRST_BATCH_NO = "@fBatchNo";
        public const string TO_BATCH_NO = "@tBatchNo";
        public const string CHALLAN_NO = "@ChalanNo";
        public const string MANU_CODE = "@ManuCode";
        public const string ITEM_CODE = "@ItemCode";
        public const string F_ITEM_CODE = "@fItemCode";
        public const string T_ITEM_CODE = "@tItemCode";
        public const string PPIC_NO = "@PPICNo";
        //......................................nimara.................................................
        public const string SUPP_CODE = "@SuppCode";
        //......................................nimara.................................................
        public const string HR_THANA = "rptHRThana";
        public const string GL_CHART_OF_ACC = "rptGLChartOfAcc";
        public const string GL_BATCH_VIEW_DETAIL = "rptGLBatchViewDetail";
        public const string GL_BATCH_VIEW_SINGLE_VCH = "rptGLBatchViewSingleVch";
        public const string GL_BATCH_VIEW_HEADER = "rptGLBatchViewHeader";
        public const string GL_SRC_CODE_MAINTENC = "rptGLSrcCodeMaintenc";
        public const string GL_COST_ANALYSIS = "rptGLCostAnalysis";
        public const string HR_AREA = "rptHRAreaList";
        public const string HR_DISTRICT = "rptHRDistrict";
        public const string HR_DEPARTMENT = "rptHRDepartmentList";
        public const string HR_DIVISION = "rptHRDivision";
        public const string HR_SECTION = "rptHRSectionList";
        public const string INV_BANK = "rptINVBank";
        public const string INV_SUPPLIER = "rptINVSupplier";
        public const string INV_PRODUCT_LIST = "rptINVProductList";
        public const string INV_PRODUCT_INGREDIENT = "rptINVProductIngredient";
        public const string INV_PRODUCT_BATCH = "rptINVProductBatch";
        public const string INV_QUOTATION_RECEIVED = "rptINVSupplierCS";
        public const string INV_PPIC_INFORMATION = "rptINVPPICInformation";
        public const string INV_FACTORY_REQUIREMENT = "rptINVRequirement";
        
        public const string INV_FACTORY_REQUIREMENT_VERIFY = "rptINVRequirementVerify";
        public const string INV_MATERIAL_LIST = "rptINVMaterialList";
        public const string INV_MATERIAL_REQ = "rptINVMaterialReq";
        public const string INV_ORDER_FINALIZE = "rptINVOrderFinalize";

        public const string INV_BATCH_DOS = "rptINVBatchDos";
        public const string INV_BATCH_POS = "rptINVBatchPos";
        public const string INV_MANUFACTURER = "rptINVManufacturer";
        public const string INV_PROCUREMENT_BY_MATERIAL = "rptINVProcurementByMaterial";
        public const string INV_PROCUREMENT_BY_SUPPLIER = "rptINVProcurementBySupplier";        
        public const string INV_GRN_BY_DATE = "rptINVGoodsReceivedNoteByDate";
        public const string INV_MONTHLY_RAW_MATERIAL_STOCK = "rptINVMonthlyRawMaterialStock";

        public const string INV_PRODUCTION_TRANSFER_TO_WAREHOUSE = "rptINVProductTransferWarehouse";

        #endregion Report Parameter
    }
}
