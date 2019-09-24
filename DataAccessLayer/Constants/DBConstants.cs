using System.Configuration;
using DataAccessLayer.Utility;

namespace DataAccessLayer.Constants
{
    public class DBConstants
    {
        private const string _connectionString = @"Data Source={0};Initial Catalog={1};Integrated Security={2};User ID={3};Password={4}";

        public enum DBName
        {
            A2ZCSMCUS,
            A2ZGLMCUS,
            A2ZHKMCUS,
            A2ZHRMCUS,
            A2ZERPGL
        }

        #region Reports
        public const string SP_REPORT_PRODUCTLIST = "rptProductList";
        #endregion

        #region StoredProcedures

        #region Common
        public const string SP_CURRVAL = "currval";
        public const string SP_NEXTVAL = "nextval";
        public const string SP_USED_CURRVAL = "usedCurrval";
        #endregion

        #region Users
        public const string SP_INSERT_USERS = "InsertUsers";
        public const string SP_EDIT_USERS = "EditUsers";
        public const string SP_DELETE_USERS = "DeleteUsers";
        public const string SP_GET_USERS_ALL = "GetAllUsers";
        public const string SP_GET_USERS_BY_ID = "GetUsersByID";
        public const string SP_GET_USERS_BY_RANGE = "GetUsersByRange";
        #endregion

        #region AUTH_Functionality_T
        //SAAIF:10/13/2012 10:29:38 PM
        public const string SP_INSERT_AUTH_FUNCTIONALITY_T = "InsertAUTH_Functionality_T";
        public const string SP_EDIT_AUTH_FUNCTIONALITY_T = "EditAUTH_Functionality_T";
        public const string SP_DELETE_AUTH_FUNCTIONALITY_T = "DeleteAUTH_Functionality_T";
        public const string SP_GET_AUTH_FUNCTIONALITY_T_ALL = "GetAllAUTH_Functionality_T";
        public const string SP_GET_AUTH_FUNCTIONALITY_T_BY_ID = "GetAUTH_Functionality_TByID";
        public const string SP_GET_AUTH_FUNCTIONALITY_T_BY_RANGE = "GetAUTH_Functionality_TByRange";
        #endregion

        #region AUTH_Group_T
        //SAAIF:10/13/2012 10:29:38 PM
        public const string SP_INSERT_AUTH_GROUP_T = "InsertAUTH_Group_T";
        public const string SP_EDIT_AUTH_GROUP_T = "EditAUTH_Group_T";
        public const string SP_DELETE_AUTH_GROUP_T = "DeleteAUTH_Group_T";
        public const string SP_GET_AUTH_GROUP_T_ALL = "GetAllAUTH_Group_T";
        public const string SP_GET_AUTH_GROUP_T_BY_ID = "GetAUTH_Group_TByID";
        public const string SP_GET_AUTH_GROUP_T_BY_RANGE = "GetAUTH_Group_TByRange";
        #endregion

        #region AUTH_PermissionRole_T
        //SAAIF:10/13/2012 10:29:38 PM
        public const string SP_INSERT_AUTH_PERMISSIONROLE_T = "InsertAUTH_PermissionRole_T";
        public const string SP_EDIT_AUTH_PERMISSIONROLE_T = "EditAUTH_PermissionRole_T";
        public const string SP_DELETE_AUTH_PERMISSIONROLE_T = "DeleteAUTH_PermissionRole_T";
        public const string SP_GET_AUTH_PERMISSIONROLE_T_ALL = "GetAllAUTH_PermissionRole_T";
        public const string SP_GET_AUTH_PERMISSIONROLE_T_BY_ID = "GetAUTH_PermissionRole_TByID";
        public const string SP_GET_AUTH_PERMISSIONROLE_T_BY_RANGE = "GetAUTH_PermissionRole_TByRange";
        #endregion

        #region AUTH_RoleAssign_T
        //SAAIF:10/13/2012 10:29:38 PM
        public const string SP_INSERT_AUTH_ROLEASSIGN_T = "InsertAUTH_RoleAssign_T";
        public const string SP_EDIT_AUTH_ROLEASSIGN_T = "EditAUTH_RoleAssign_T";
        public const string SP_DELETE_AUTH_ROLEASSIGN_T = "DeleteAUTH_RoleAssign_T";
        public const string SP_GET_AUTH_ROLEASSIGN_T_ALL = "GetAllAUTH_RoleAssign_T";
        public const string SP_GET_AUTH_ROLEASSIGN_T_BY_ID = "GetAUTH_RoleAssign_TByID";
        public const string SP_GET_AUTH_ROLEASSIGN_T_BY_RANGE = "GetAUTH_RoleAssign_TByRange";
        #endregion

        #region AUTH_Role_T
        //SAAIF:10/13/2012 10:29:38 PM
        public const string SP_INSERT_AUTH_ROLE_T = "InsertAUTH_Role_T";
        public const string SP_EDIT_AUTH_ROLE_T = "EditAUTH_Role_T";
        public const string SP_DELETE_AUTH_ROLE_T = "DeleteAUTH_Role_T";
        public const string SP_GET_AUTH_ROLE_T_ALL = "GetAllAUTH_Role_T";
        public const string SP_GET_AUTH_ROLE_T_BY_ID = "GetAUTH_Role_TByID";
        public const string SP_GET_AUTH_ROLE_T_BY_RANGE = "GetAUTH_Role_TByRange";
        #endregion

        #region AUTH_UserGroup_T
        //SAAIF:10/13/2012 10:29:38 PM
        public const string SP_INSERT_AUTH_USERGROUP_T = "InsertAUTH_UserGroup_T";
        public const string SP_EDIT_AUTH_USERGROUP_T = "EditAUTH_UserGroup_T";
        public const string SP_DELETE_AUTH_USERGROUP_T = "DeleteAUTH_UserGroup_T";
        public const string SP_GET_AUTH_USERGROUP_T_ALL = "GetAllAUTH_UserGroup_T";
        public const string SP_GET_AUTH_USERGROUP_T_BY_ID = "GetAUTH_UserGroup_TByID";
        public const string SP_GET_AUTH_USERGROUP_T_BY_RANGE = "GetAUTH_UserGroup_TByRange";
        #endregion

        #region CompanyInformation_T
        //SAAIF:10/13/2012 10:29:38 PM
        public const string SP_INSERT_COMPANYINFORMATION_T = "InsertCompanyInformation_T";
        public const string SP_EDIT_COMPANYINFORMATION_T = "EditCompanyInformation_T";
        public const string SP_DELETE_COMPANYINFORMATION_T = "DeleteCompanyInformation_T";
        public const string SP_GET_COMPANYINFORMATION_T_ALL = "GetAllCompanyInformation_T";
        public const string SP_GET_COMPANYINFORMATION_T_BY_ID = "GetCompanyInformation_TByID";
        public const string SP_GET_COMPANYINFORMATION_T_BY_RANGE = "GetCompanyInformation_TByRange";
        #endregion

        #region LoginLog_T
        //SAAIF:10/13/2012 10:29:38 PM
        public const string SP_INSERT_LOGINLOG_T = "InsertLoginLog_T";
        public const string SP_EDIT_LOGINLOG_T = "EditLoginLog_T";
        public const string SP_DELETE_LOGINLOG_T = "DeleteLoginLog_T";
        public const string SP_GET_LOGINLOG_T_ALL = "GetAllLoginLog_T";
        public const string SP_GET_LOGINLOG_T_BY_ID = "GetLoginLog_TByID";
        public const string SP_GET_LOGINLOG_T_BY_RANGE = "GetLoginLog_TByRange";
        #endregion

        #region User_T
        //SAAIF:10/13/2012 10:29:38 PM
        public const string SP_INSERT_USER_T = "InsertUser_T";
        public const string SP_EDIT_USER_T = "EditUser_T";
        public const string SP_DELETE_USER_T = "DeleteUser_T";
        public const string SP_GET_USER_T_ALL = "GetAllUser_T";
        public const string SP_GET_USER_T_BY_ID = "GetUser_TByID";
        public const string SP_GET_USER_T_BY_RANGE = "GetUser_TByRange";
        #endregion

        #region A2ZJWGOLDPRICE
        //SAAIF:11/11/2012 10:38:28 AM
        public const string SP_INSERT_A2ZJWGOLDPRICE = "InsertA2ZJWGOLDPRICE";
        public const string SP_EDIT_A2ZJWGOLDPRICE_BY_COMPANYID_BRANCHID_AND_GOLDCARAT = "EditA2ZJWGOLDPRICEByCompanyIDBranchIDAndGOldCarat";
        public const string SP_DELETE_A2ZJWGOLDPRICE_BY_COMPANYID_BRANCHID_AND_GOLDCARAT = "DeleteA2ZJWGOLDPRICEByCompanyIDBranchIDAndGOldCarat";
        public const string SP_DELETE_A2ZJWGOLDPRICE_BY_ID = "DeleteA2ZJWGOLDPRICE";
        public const string SP_GET_A2ZJWGOLDPRICE_ALL = "GetAllA2ZJWGOLDPRICE";
        public const string SP_GET_A2ZJWGOLDPRICE_BY_COMPANYID_BRANCHID_AND_GOLDCARAT = "GetA2ZJWGOLDPRICEByCompanyIDBranchIDAndGOldCarat";
        public const string SP_GET_A2ZJWGOLDPRICE_BY_RANGE = "GetA2ZJWGOLDPRICEByRange";
        #endregion

        #region A2ZJWGRP
        //SAAIF:11/17/2012 1:14:07 PM
        public const string SP_INSERT_A2ZJWGRP = "InsertA2ZJWGRP";
        public const string SP_EDIT_A2ZJWGRP = "EditA2ZJWGRP";
        public const string SP_EDIT_A2ZJWGRP_BY_COMPANYID_BRANCHID_AND_ITEMGROUP = "EditA2ZJWGRP_ByCompanyIDBranchIDAndItemGroup";
        public const string SP_DELETE_A2ZJWGRP = "DeleteA2ZJWGRP";
        public const string SP_DELETE_A2ZJWGRP_BY_COMPANYID_BRANCHID_AND_ITEMGROUP = "DeleteA2ZJWGRP_ByCompanyIDBranchIDAndItemGroup";
        public const string SP_GET_A2ZJWGRP_ALL = "GetAllA2ZJWGRP";
        public const string SP_GET_A2ZJWGRP_BY_ID = "GetA2ZJWGRPByID";
        public const string SP_GET_A2ZJWGRP_BY_COMPANYID_BRANCHID_AND_GROUPCODE = "GetA2ZJWGRP_ByCompanyIDBranchIDAndGroupCode";
        public const string SP_GET_A2ZJWGRP_BY_RANGE = "GetA2ZJWGRPByRange";
        public const string GET_ALL_A2ZJWGRP_GRPCODE = "GetAllA2ZJWGRP_GRPCODE";
        public const string SP_EDIT_A2ZJWGRP_LastSrlNo = "EditA2ZJWGRPLastSerialNo";
        #endregion

        #region A2ZJWITM
        //SAAIF:11/17/2012 1:14:07 PM
        public const string SP_INSERT_A2ZJWITM = "InsertA2ZJWITM";
        public const string SP_EDIT_A2ZJWITM = "EditA2ZJWITM";
        public const string SP_EDIT_A2ZJWITM_DIAMOND = "UpdateDiamondItemCostPrice";
        public const string SP_EDIT_A2ZJWITM_DIAMOND_DEC = "UpdateDiamondItemCostPriceDesc";
        public const string SP_EDIT_A2ZJWITM_DIAMOND_SELL_DEC = "UpdateDiamondItemSellPriceDesc";
        public const string SP_EDIT_A2ZJWITM_DIAMOND_SELL = "UpdateDiamondItemSellPrice";
        public const string SP_DELETE_A2ZJWITM = "DeleteA2ZJWITM";
        public const string SP_GET_A2ZJWITM_ALL = "GetAllA2ZJWITM";
        public const string SP_GET_A2ZJWITM_BY_ID = "GetA2ZJWITMByID";
        public const string SP_GET_A2ZJWITM_BY_RANGE = "GetA2ZJWITMByRange";
        public const string SP_GET_ALLA2ZJWITM_ITMBARCODE = "GetAllA2ZJWITM_ITMBARCODE";
        public const string SP_GET_ALLA2ZJWITM_ITEMNO = "GETALLA2ZJWITM_ITEMNO";
        public const string SP_GET_ALLA2ZJWITM_ITEMNO_BY_GROUP = "GETALLA2ZJWITM_ITEMNO_BY_GROUP";
        #endregion

        #region A2ZJWMCRATE
        //SAAIF:11/17/2012 1:14:07 PM
        public const string SP_INSERT_A2ZJWMCRATE = "InsertA2ZJWMCRATE";
        public const string SP_EDIT_A2ZJWMCRATE = "EditA2ZJWMCRATE";
        public const string SP_DELETE_A2ZJWMCRATE = "DeleteA2ZJWMCRATE";
        public const string SP_GET_A2ZJWMCRATE_ALL = "GetAllA2ZJWMCRATE";
        public const string SP_GET_A2ZJWMCRATE_BY_ID = "GetA2ZJWMCRATEByID";
        public const string SP_GET_A2ZJWMCRATE_BY_RANGE = "GetA2ZJWMCRATEByRange";
        #endregion

        #region A2ZJWBNK
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWBNK = "InsertA2ZJWBNK";
        public const string SP_EDIT_A2ZJWBNK = "EditA2ZJWBNK";
        public const string SP_DELETE_A2ZJWBNK = "DeleteA2ZJWBNK";
        public const string SP_GET_A2ZJWBNK_ALL = "GetAllA2ZJWBNK";
        public const string SP_GET_A2ZJWBNK_BY_ID = "GetA2ZJWBNKByID";
        public const string SP_GET_A2ZJWBNK_BY_RANGE = "GetA2ZJWBNKByRange";
        #endregion

        #region A2ZJWBRK
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWBRK = "InsertA2ZJWBRK";
        public const string SP_EDIT_A2ZJWBRK = "EditA2ZJWBRK";
        public const string SP_DELETE_A2ZJWBRK = "DeleteA2ZJWBRK";
        public const string SP_GET_A2ZJWBRK_ALL = "GetAllA2ZJWBRK";
        public const string SP_GET_A2ZJWBRK_BY_ID = "GetA2ZJWBRKByID";
        public const string SP_GET_A2ZJWBRK_BY_RANGE = "GetA2ZJWBRKByRange";
        #endregion

        #region A2ZJWCARD
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWCARD = "InsertA2ZJWCARD";
        public const string SP_EDIT_A2ZJWCARD = "EditA2ZJWCARD";
        public const string SP_DELETE_A2ZJWCARD = "DeleteA2ZJWCARD";
        public const string SP_GET_A2ZJWCARD_ALL = "GetAllA2ZJWCARD";
        public const string SP_GET_A2ZJWCARD_BY_ID = "GetA2ZJWCARDByID";
        public const string SP_GET_A2ZJWCARD_BY_RANGE = "GetA2ZJWCARDByRange";
        #endregion

        #region A2ZJWCUS
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWCUS = "InsertA2ZJWCUS";
        public const string SP_EDIT_A2ZJWCUS = "EditA2ZJWCUS";
        public const string SP_DELETE_A2ZJWCUS = "DeleteA2ZJWCUS";
        public const string SP_GET_A2ZJWCUS_ALL = "GetAllA2ZJWCUS";
        public const string SP_GET_A2ZJWCUS_BY_ID = "GetA2ZJWCUSByID";
        public const string SP_GET_A2ZJWCUS_BY_RANGE = "GetA2ZJWCUSByRange";
        public const string SP_GET_ALL_ADJ_ADVANCE_AMT_BY_CUSTOMER_CODE = "GetAllAdjAdvanceAmtByCustomerCode";

        #endregion

        #region A2ZJWDIU
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWDIU = "InsertA2ZJWDIU";
        public const string SP_EDIT_A2ZJWDIU = "EditA2ZJWDIU";
        public const string SP_DELETE_A2ZJWDIU = "DeleteA2ZJWDIU";
        public const string SP_GET_A2ZJWDIU_ALL = "GetAllA2ZJWDIU";
        public const string SP_GET_A2ZJWDIU_BY_ID = "GetA2ZJWDIUByID";
        public const string SP_GET_A2ZJWDIU_BY_RANGE = "GetA2ZJWDIUByRange";
        #endregion

        #region A2ZJWEXPENSE
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWEXPENSE = "InsertA2ZJWEXPENSE";
        public const string SP_EDIT_A2ZJWEXPENSE = "EditA2ZJWEXPENSE";
        public const string SP_DELETE_A2ZJWEXPENSE = "DeleteA2ZJWEXPENSE";
        public const string SP_GET_A2ZJWEXPENSE_ALL = "GetAllA2ZJWEXPENSE";
        public const string SP_GET_A2ZJWEXPENSE_BY_ID = "GetA2ZJWEXPENSEByID";
        public const string SP_GET_A2ZJWEXPENSE_BY_RANGE = "GetA2ZJWEXPENSEByRange";
        #endregion

        #region A2ZJWGSM
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWGSM = "InsertA2ZJWGSM";
        public const string SP_EDIT_A2ZJWGSM = "EditA2ZJWGSM";
        public const string SP_DELETE_A2ZJWGSM = "DeleteA2ZJWGSM";
        public const string SP_GET_A2ZJWGSM_ALL = "GetAllA2ZJWGSM";
        public const string SP_GET_A2ZJWGSM_BY_ID = "GetA2ZJWGSMByID";
        public const string SP_GET_A2ZJWGSM_BY_RANGE = "GetA2ZJWGSMByRange";
        public const string SP_GET_A2ZJWGSM_GOLD_CARAT21 = "EditA2ZJWGoldCarat21";
        public const string SP_GET_A2ZJWGSM_GOLD_CARAT22 = "EditA2ZJWGoldCarat22";
        public const string SP_GET_A2ZJWGSM_GOLD_CARAT18 = "EditA2ZJWGoldCarat18";
        #endregion

        #region A2ZJWINCOME
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWINCOME = "InsertA2ZJWINCOME";
        public const string SP_EDIT_A2ZJWINCOME = "EditA2ZJWINCOME";
        public const string SP_DELETE_A2ZJWINCOME = "DeleteA2ZJWINCOME";
        public const string SP_GET_A2ZJWINCOME_ALL = "GetAllA2ZJWINCOME";
        public const string SP_GET_A2ZJWINCOME_BY_ID = "GetA2ZJWINCOMEByID";
        public const string SP_GET_A2ZJWINCOME_BY_RANGE = "GetA2ZJWINCOMEByRange";
        #endregion

        #region A2ZJWLDGTYPE
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWLDGTYPE = "InsertA2ZJWLDGTYPE";
        public const string SP_EDIT_A2ZJWLDGTYPE = "EditA2ZJWLDGTYPE";
        public const string SP_DELETE_A2ZJWLDGTYPE = "DeleteA2ZJWLDGTYPE";
        public const string SP_GET_A2ZJWLDGTYPE_ALL = "GetAllA2ZJWLDGTYPE";
        public const string SP_GET_A2ZJWLDGTYPE_BY_ID = "GetA2ZJWLDGTYPEByID";
        public const string SP_GET_A2ZJWLDGTYPE_BY_RANGE = "GetA2ZJWLDGTYPEByRange";
        #endregion

        #region A2ZJWMAL
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWMAL = "InsertA2ZJWMAL";
        public const string SP_EDIT_A2ZJWMAL = "EditA2ZJWMAL";
        public const string SP_DELETE_A2ZJWMAL = "DeleteA2ZJWMAL";
        public const string SP_GET_A2ZJWMAL_ALL = "GetAllA2ZJWMAL";
        public const string SP_GET_A2ZJWMAL_BY_ID = "GetA2ZJWMALByID";
        public const string SP_GET_A2ZJWMAL_BY_RANGE = "GetA2ZJWMALByRange";
        #endregion

        #region A2ZJWOTH
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWOTH = "InsertA2ZJWOTH";
        public const string SP_EDIT_A2ZJWOTH = "EditA2ZJWOTH";
        public const string SP_DELETE_A2ZJWOTH = "DeleteA2ZJWOTH";
        public const string SP_GET_A2ZJWOTH_ALL = "GetAllA2ZJWOTH";
        public const string SP_GET_A2ZJWOTH_BY_ID = "GetA2ZJWOTHByID";
        public const string SP_GET_A2ZJWOTH_BY_RANGE = "GetA2ZJWOTHByRange";
        #endregion

        #region A2ZJWOWNER
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWOWNER = "InsertA2ZJWOWNER";
        public const string SP_EDIT_A2ZJWOWNER = "EditA2ZJWOWNER";
        public const string SP_DELETE_A2ZJWOWNER = "DeleteA2ZJWOWNER";
        public const string SP_GET_A2ZJWOWNER_ALL = "GetAllA2ZJWOWNER";
        public const string SP_GET_A2ZJWOWNER_BY_ID = "GetA2ZJWOWNERByID";
        public const string SP_GET_A2ZJWOWNER_BY_RANGE = "GetA2ZJWOWNERByRange";
        #endregion

        #region A2ZJWPRAM
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWPARAM = "InsertA2ZJWPARAM";
        public const string SP_EDIT_A2ZJWPARAM = "EditA2ZJWPARAM";
        public const string SP_DELETE_A2ZJWPARAM = "DeleteA2ZJWPARAM";
        public const string SP_GET_A2ZJWPARAM_ALL = "GetAllA2ZJWPARAM";
        public const string SP_GET_A2ZJWPARAM_BY_ID = "GetA2ZJWA2ZJWPARAMByID";
        public const string SP_GET_A2ZJWPARAM_BY_RANGE = "GetA2ZJWA2ZJWPARAMByRange";
        public const string SP_UPDATE_PROCESSDATE = "UpdateA2ZJWPARAMPROCESS";
        #endregion

        #region A2ZJWPGD
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWPGD = "InsertA2ZJWPGD";
        public const string SP_EDIT_A2ZJWPGD = "EditA2ZJWPGD";
        public const string SP_DELETE_A2ZJWPGD = "DeleteA2ZJWPGD";
        public const string SP_GET_A2ZJWPGD_ALL = "GetAllA2ZJWPGD";
        public const string SP_GET_A2ZJWPGD_BY_ID = "GetA2ZJWPGDByID";
        public const string SP_GET_A2ZJWPGD_BY_RANGE = "GetA2ZJWPGDByRange";
        public const string SP_EDIT_A2ZJWPGD_GOLDISSUE = "EditA2ZJWGoldissue";
        #endregion

        #region A2ZJWSLM
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWSLM = "InsertA2ZJWSLM";
        public const string SP_EDIT_A2ZJWSLM = "EditA2ZJWSLM";
        public const string SP_DELETE_A2ZJWSLM = "DeleteA2ZJWSLM";
        public const string SP_GET_A2ZJWSLM_ALL = "GetAllA2ZJWSLM";
        public const string SP_GET_A2ZJWSLM_BY_ID = "GetA2ZJWSLMByID";
        public const string SP_GET_A2ZJWSLM_BY_RANGE = "GetA2ZJWSLMByRange";
        #endregion

        #region A2ZJWSTC
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWSTC = "InsertA2ZJWSTC";
        public const string SP_EDIT_A2ZJWSTC = "EditA2ZJWSTC";
        public const string SP_DELETE_A2ZJWSTC = "DeleteA2ZJWSTC";
        public const string SP_GET_A2ZJWSTC_ALL = "GetAllA2ZJWSTC";
        public const string SP_GET_A2ZJWSTC_BY_ID = "GetA2ZJWSTCByID";
        public const string SP_GET_A2ZJWSTC_BY_RANGE = "GetA2ZJWSTCByRange";
        #endregion

        #region A2ZJWSTN
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWSTN = "InsertA2ZJWSTN";
        public const string SP_EDIT_A2ZJWSTN = "EditA2ZJWSTN";
        public const string SP_DELETE_A2ZJWSTN = "DeleteA2ZJWSTN";
        public const string SP_GET_A2ZJWSTN_ALL = "GetAllA2ZJWSTN";
        public const string SP_GET_A2ZJWSTN_BY_ID = "GetA2ZJWSTNByID";
        public const string SP_GET_A2ZJWSTN_BY_RANGE = "GetA2ZJWSTNByRange";
        #endregion

        #region A2ZJWTAX
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWTAX = "InsertA2ZJWTAX";
        public const string SP_EDIT_A2ZJWTAX = "EditA2ZJWTAX";
        public const string SP_DELETE_A2ZJWTAX = "DeleteA2ZJWTAX";
        public const string SP_GET_A2ZJWTAX_ALL = "GetAllA2ZJWTAX";
        public const string SP_GET_A2ZJWTAX_BY_ID = "GetA2ZJWTAXByID";
        public const string SP_GET_A2ZJWTAX_BY_RANGE = "GetA2ZJWTAXByRange";
        #endregion

        #region A2ZJWTRN
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWTRN = "InsertA2ZJWTRN";
        public const string SP_EDIT_A2ZJWTRN = "EditA2ZJWTRN";
        public const string SP_DELETE_A2ZJWTRN = "DeleteA2ZJWTRN";
        public const string SP_GET_A2ZJWTRN_ALL = "GetAllA2ZJWTRN";
        public const string SP_GET_A2ZJWTRN_BY_ID = "GetA2ZJWTRNByID";
        public const string SP_GET_A2ZJWTRN_BY_RANGE = "GetA2ZJWTRNByRange";
        public const string SP_EDIT_A2ZJWCUS_CASHBOX = "EditA2ZJWCUSCashBox";
        public const string SP_EDIT_A2ZJWGSM_CASHBOX = "EditA2ZJWGSMCashBox";
        public const string SP_EDIT_A2ZJWBRK_CASHBOX = "EditA2ZJWBRKCashBox";
        public const string SP_EDIT_A2ZJWSLM_CASHBOX = "EditA2ZJWSLMCashBox";
        public const string SP_EDIT_A2ZJWOWN_CASHBOX = "EditA2ZJWOWNCashBox";
        public const string SP_EDIT_A2ZJWINCOME_CASHBOX = "EditA2ZJWINCOMECashBox";
        public const string SP_EDIT_A2ZJWEXPENSE_CASHBOX = "EditA2ZJWEXPENSECashBox";
        public const string SP_EDIT_A2ZJWOTH_CASHBOX = "EditA2ZJWOTHCashBox";
        public const string SP_EDIT_A2ZJWBNK_CASHBOX = "EditA2ZJWBNKCashBox";
        public const string SP_UPDATE_A2ZJWITM = "UpdateA2ZJWITM";
        public const string SP_UPDATE_A2ZJWDIU = "UpdateA2ZJWDIU";
        public const string SP_UPDATE_A2ZJWJIU = "UpdateA2ZJWJIU";
        public const string SP_UPDATE_CUSTOMER = "UpdateA2ZJWCUS";
        public const string SP_UPDATE_A2ZJWTRN = "UpdateA2ZJWTRN";

        #endregion

        #region A2ZJWTRNITM
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWTRNITM = "InsertA2ZJWTRNITM";
        public const string SP_EDIT_A2ZJWTRNITM = "EditA2ZJWTRNITM";
        public const string SP_DELETE_A2ZJWTRNITM = "DeleteA2ZJWTRNITM";
        public const string SP_GET_A2ZJWTRNITM_ALL = "GetAllA2ZJWTRNITM";
        public const string SP_GET_A2ZJWTRNITM_BY_ID = "GetA2ZJWTRNITMByID";
        public const string SP_GET_A2ZJWTRNITM_BY_RANGE = "GetA2ZJWTRNITMByRange";
        public const string SP_UPDATE_A2ZJWTRNITEM_FOR_ITEM_EDITED = "UpdateA2ZJWTRNItemForItemEdited";
        public const string SP_UPDATE_A2ZJWTRNITEM_FOR_BOOKINGORDER = "UpdateA2ZJWTRNItemForCancelBookingOrder";
        #endregion

        #region A2ZJWTRNPAY
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWTRNPAY = "InsertA2ZJWTRNPAY";
        public const string SP_EDIT_A2ZJWTRNPAY = "EditA2ZJWTRNPAY";
        public const string SP_DELETE_A2ZJWTRNPAY = "DeleteA2ZJWTRNPAY";
        public const string SP_GET_A2ZJWTRNPAY_ALL = "GetAllA2ZJWTRNPAY";
        public const string SP_GET_A2ZJWTRNPAY_BY_ID = "GetA2ZJWTRNPAYByID";
        public const string SP_GET_A2ZJWTRNPAY_BY_RANGE = "GetA2ZJWTRNPAYByRange";
        #endregion

        #region A2ZJWTRNTYPE
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWTRNTYPE = "InsertA2ZJWTRNTYPE";
        public const string SP_EDIT_A2ZJWTRNTYPE = "EditA2ZJWTRNTYPE";
        public const string SP_DELETE_A2ZJWTRNTYPE = "DeleteA2ZJWTRNTYPE";
        public const string SP_GET_A2ZJWTRNTYPE_ALL = "GetAllA2ZJWTRNTYPE";
        public const string SP_GET_A2ZJWTRNTYPE_BY_ID = "GetA2ZJWTRNTYPEByID";
        public const string SP_GET_A2ZJWTRNTYPE_BY_RANGE = "GetA2ZJWTRNTYPEByRange";
        #endregion

        #region A2ZJWVCHTYPE
        //SAAIF:12/10/2012 10:19:47 AM
        public const string SP_INSERT_A2ZJWVCHTYPE = "InsertA2ZJWVCHTYPE";
        public const string SP_EDIT_A2ZJWVCHTYPE = "EditA2ZJWVCHTYPE";
        public const string SP_DELETE_A2ZJWVCHTYPE = "DeleteA2ZJWVCHTYPE";
        public const string SP_GET_A2ZJWVCHTYPE_ALL = "GetAllA2ZJWVCHTYPE";
        public const string SP_GET_A2ZJWVCHTYPE_BY_ID = "GetA2ZJWVCHTYPEByID";
        public const string SP_GET_A2ZJWVCHTYPE_BY_RANGE = "GetA2ZJWVCHTYPEByRange";
        #endregion

        #region A2ZJWINPUTITM
        //SAAIF:1/30/2013 12:10:52 PM
        public const string SP_INSERT_A2ZJWINPUTITM = "InsertA2ZJWINPUTITM";
        public const string SP_EDIT_A2ZJWINPUTITM = "EditA2ZJWINPUTITM";
        public const string SP_DELETE_A2ZJWINPUTITM = "DeleteA2ZJWINPUTITM";
        public const string SP_GET_A2ZJWINPUTITM_ALL = "GetAllA2ZJWINPUTITM";
        public const string SP_GET_A2ZJWINPUTITM_BY_ID = "GetA2ZJWINPUTITMByID";
        public const string SP_GET_A2ZJWINPUTITM_BY_RANGE = "GetA2ZJWINPUTITMByRange";
        #endregion

        #region A2ZJWRCV
        //SAAIF:1/30/2013 12:10:52 PM
        public const string SP_INSERT_A2ZJWRCV = "InsertA2ZJWRCV";
        public const string SP_EDIT_A2ZJWRCV = "EditA2ZJWRCV";
        public const string SP_DELETE_A2ZJWRCV = "DeleteA2ZJWRCV";
        public const string SP_GET_A2ZJWRCV_ALL = "GetAllA2ZJWRCV";
        public const string SP_GET_A2ZJWRCV_BY_ID = "GetA2ZJWRCVByID";
        public const string SP_GET_A2ZJWRCV_BY_RANGE = "GetA2ZJWRCVByRange";
        #endregion

        #region A2ZJWRCVITM
        //SAAIF:1/30/2013 12:10:52 PM
        public const string SP_INSERT_A2ZJWRCVITM = "InsertA2ZJWRCVITM";
        public const string SP_EDIT_A2ZJWRCVITM = "EditA2ZJWRCVITM";
        public const string SP_DELETE_A2ZJWRCVITM = "DeleteA2ZJWRCVITM";
        public const string SP_GET_A2ZJWRCVITM_ALL = "GetAllA2ZJWRCVITM";
        public const string SP_GET_A2ZJWRCVITM_BY_ID = "GetA2ZJWRCVITMByID";
        public const string SP_GET_A2ZJWRCVITM_BY_RANGE = "GetA2ZJWRCVITMByRange";
        #endregion

        #region A2ZJWJIU
        //SAAIF:4/3/13 11:57:49 AM
        public const string SP_INSERT_A2ZJWJIU = "InsertA2ZJWJIU";
        public const string SP_EDIT_A2ZJWJIU = "EditA2ZJWJIU";
        public const string SP_DELETE_A2ZJWJIU = "DeleteA2ZJWJIU";
        public const string SP_GET_A2ZJWJIU_ALL = "GetAllA2ZJWJIU";
        public const string SP_GET_A2ZJWJIU_BY_ID = "GetA2ZJWJIUByID";
        public const string SP_GET_A2ZJWJIU_BY_RANGE = "GetA2ZJWJIUByRange";
        #endregion

        #region A2ZSESSION
        //SAAIF:4/21/13 4:12:45 PM
        public const string SP_INSERT_A2ZSESSION = "InsertA2ZSESSION";
        public const string SP_EDIT_A2ZSESSION = "EditA2ZSESSION";
        public const string SP_DELETE_A2ZSESSION = "DeleteA2ZSESSION";
        public const string SP_GET_A2ZSESSION_ALL = "GetAllA2ZSESSION";
        public const string SP_GET_A2ZSESSION_BY_ID = "GetA2ZSESSIONByID";
        public const string SP_GET_A2ZSESSION_BY_RANGE = "GetA2ZSESSIONByRange";
        #endregion

        #region Direct Sales Queue
        public const string SP_INSERT_DIRECTSALESQUEUE = "InsertDirectSalesQueue";
        public const string SP_DELETE_DIRECTSALESQUEUE = "DeleteDirectSalesQueue";
        public const string SP_GET_DIRECTSALESQUEUE_ALL = "GetAllDirectSalesQueue";

        public const string SP_GET_PAYMENTSQUEUE = "GetAllPaymentsQueue";
        public const string SP_INSERT_PAYMENTSQUEUE = "InsertPaymentsQueue";
        public const string SP_DELETE_PAYMENTSQUEUE = "DeletePaymentsQueue";

        public const string SP_GET_SALESITEMSQUEUE_ALL = "GetAllSalesItemsQueue";
        public const string SP_INSERT_SALESITEMSQUEUE = "InsertSalesItemsQueue";
        public const string SP_DELETE_SALESITEMSQUEUE = "DeleteSalesItemsQueue";

        public const string SP_GET_PAYCHECKQUEUE_ALL = "GetAllPayCheckQueue";
        public const string SP_INSERT_PAYCHECKQUEUE = "InsertPayCheckQueue";
        public const string SP_DELETE_PAYCHECKQUEUE = "DeletePayCheckQueue";

        public const string SP_GET_CARDPAYMENTQUEUE_ALL = "GetAllCardPaymentQueue";
        public const string SP_INSERT_CARDPAYMENTQUEUE = "InsertCardPaymentQueue";
        public const string SP_DELETE_CARDPAYMENTQUEUE = "DeleteCardPaymentQueue";

        public const string SP_GET_OLDJEWELLRYQUEUE_ALL = "GetAllOldJewellryQueue";
        public const string SP_INSERT_OLDJEWELLRYQUEUE = "InsertOldJewellryQueue";
        public const string SP_DELETE_OLDJEWELLRYQUEUE = "DeleteOldJewellryQueue";

        public const string SP_GET_ADJADVANCEAMTQUEUE_ALL = "GetAllAdjAdvanceAmtQueue";
        public const string SP_INSERT_ADJADVANCEAMTQUEUE = "InsertAdjAdvanceAmtQueue";
        public const string SP_DELETE_ADJADVANCEAMTQUEUE = "DeleteAdjAdvanceAmtQueue";
        #endregion End Direct Sales Queue

        #endregion StoredProcedures

        private static string DBServer
        {
            get
            {
                return ConfigurationManager.AppSettings["DBServer"];
            }
        }

        private static string DBUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["DBUserName"];
            }
        }

        private static string DBPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["DBPassword"];
            }
        }

        public static string GetConnectionString()
        {
            string strConnectionString;
            if (string.IsNullOrEmpty(DBUserName) || string.IsNullOrEmpty(DBPassword))
            {
                strConnectionString = string.Format(_connectionString, DBServer, string.Empty, true, string.Empty, string.Empty);
            }
            else
            {
                strConnectionString = string.Format(_connectionString, DBServer, string.Empty, false, DBUserName, DBPassword);
            }
            return strConnectionString;
        }

        public static string GetConnectionString(string DBName)
        {
            string strConnectionString;
            if (!string.IsNullOrEmpty(DBName))
            {
                if (string.IsNullOrEmpty(DBUserName) || string.IsNullOrEmpty(DBPassword))
                {
                    strConnectionString = string.Format(_connectionString, DBServer, DBName, true, string.Empty, string.Empty);
                }
                else
                {
                    strConnectionString = string.Format(_connectionString, DBServer, DBName, false, DBUserName, DBPassword);
                }
            }
            else
            {
                strConnectionString = GetConnectionString();
            }
            return strConnectionString;
        }

        public static string GetConnectionString(string DBName, string UserName, string Password)
        {
            string strConnectionString;
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                strConnectionString = GetConnectionString(DBName);
            }
            else
            {
                strConnectionString = string.Format(_connectionString, DBServer, DBName, false, UserName, Password);
            }
            return strConnectionString;
        }

        public static string GetConnectionString(string ServerName, string DBName, string UserName, string Password)
        {
            string strConnectionString = string.Empty;
            if (string.IsNullOrEmpty(ServerName))
            {
                GetConnectionString(DBName, UserName, Password);
            }
            else
            {
                strConnectionString = string.Format(_connectionString, ServerName, DBName, false, UserName, Password);
            }
            return strConnectionString;
        }

        public static string GetConnectionString(DBName dbName)
        {
            return GetConnectionString(Converter.GetString(dbName));
        }

        public static string GetConnectionString(DBName dbName, string UserName, string Password)
        {
            return GetConnectionString(Converter.GetString(dbName), UserName, Password);
        }

        public static string GetConnectionString(string ServerName, DBName dbName, string UserName, string Password)
        {
            return GetConnectionString(ServerName, Converter.GetString(dbName), UserName, Password);
        }
    }
}
