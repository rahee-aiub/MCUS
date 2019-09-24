using System;
using System.Web.UI;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO;
using ATOZWEBMCUS.WebSessionStore;


namespace ATOZWEBMCUS.Pages
{
    public partial class A2ZERPUserInitialize : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DivMessage.Visible = false;

            if (!IsPostBack)
            {
                hdnID.Value = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                ModuleDropdown();
            }
        }
    
        private void ModuleDropdown()
        {

            string sqlquery3 = "SELECT ModuleNo,ModuleName from A2ZERPMODULE";
            ddlModule = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery3, ddlModule, "A2ZHKMCUS");

        }

        protected void btnOpenForMultyUser_Click(object sender, EventArgs e)
        {
            int intModule = Converter.GetSmallInteger(ddlModule.SelectedValue);
            string sqlQuery = string.Empty;
            string msg = "";

            switch (intModule)
            {
                case 1:
                    sqlQuery = "UPDATE A2ZHKPARAMETER SET SingleUserFlag = 0";
                    Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZCSMCUS"));
                    msg = "Successfully Done for - Customer Service Module";
                    break;
                case 2:
                    sqlQuery = "UPDATE A2ZINVPARAMETER SET SingleUserFlag = 0";
                    Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZGLMCUS"));
                    msg = "Successfully Done for - General Ledger Module";
                    break;
                case 3:
                    sqlQuery = "UPDATE A2ZHRPARAMETER SET SingleUserFlag = 0";
                    Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHKMCUS"));
                    msg = "Successfully Done for - House Keeping Module";
                    break;
                case 4:
                    sqlQuery = "UPDATE A2ZGLPARAMETER SET SingleUserFlag = 0";
                    Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHRMCUS"));
                    msg = "Successfully Done for - Human Resource Module";
                    break;
                case 5:
                    sqlQuery = "UPDATE A2ZGLPARAMETER SET SingleUserFlag = 0";
                    Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZINVMCUS"));
                    msg = "Successfully Done for - Inventory Module";
                    break;

            }

            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('" + msg + "');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

        }

        protected void btnRemovedUser_Click(object sender, EventArgs e)
        {
            int intModule = Converter.GetSmallInteger(ddlModule.SelectedValue);
            int userId = Converter.GetInteger(ddlUserId.SelectedValue);

            if (intModule == 1)
            {
                A2ZSYSIDSDTO.UpdateUserCSLoginFlag(userId, 0);
            }
            if (intModule == 2)
            {
                A2ZSYSIDSDTO.UpdateUserGLLoginFlag(userId, 0);
            }
            if (intModule == 3)
            {
                A2ZSYSIDSDTO.UpdateUserHKLoginFlag(userId, 0);
            }
            if (intModule == 4)
            {
                A2ZSYSIDSDTO.UpdateUserHRLoginFlag(userId, 0);
            }
            if (intModule == 6)
            {
                A2ZSYSIDSDTO.UpdateUserBTLoginFlag(userId, 0);
            }
            //if (intModule == 7)
            //{
            //    A2ZSYSIDSDTO.UpdateUserOBTLoginFlag(userId, 0);
            //}

            ddlModule_SelectedIndexChanged(null, null);

            txtUserId.Text = string.Empty;

            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('User Intialize Done');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

        }


        protected void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("A2ZERPModule.aspx", false);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlModule.SelectedValue != "-Select-")
            {
                string dbName = "";

                int intModule = Converter.GetSmallInteger(ddlModule.SelectedValue);
                if (intModule == 1)
                {
                    dbName = "A2ZCSMCUS";
                }
                if (intModule == 2)
                {
                    dbName = "A2ZGLMCUS";
                }
                if (intModule == 3)
                {
                    dbName = "A2ZHKMCUS";
                }
                if (intModule == 4)
                {
                    dbName = "A2ZHRMCUS";
                }
                if (intModule == 6)
                {
                    dbName = "A2ZBTMCUS";
                }
                if (intModule == 7)
                {
                    dbName = "A2ZOBTMCUS";
                }
                string strQuery = "SELECT IdsNo, IdsName, EmpCode, IdsFlag, IdsLogInFlag FROM A2ZSYSIDS WHERE IdsLogInFlag <> 0 AND IdsNo != '"+ hdnID.Value +"'";
                gvUserInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvUserInfo, dbName);

                string sqlquery1 = "SELECT IdsNo,IdsName from A2ZSYSIDS WHERE IdsLogInFlag <> 0 AND IdsNo != '" + hdnID.Value + "'";
                ddlUserId = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery1, ddlUserId, dbName);

                txtUserId.Focus();

            }
        }

        protected void ddlUserId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void IDsNotFoundMsg()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ids Does not exist');", true);
            return;

        }
        protected void txtUserId_TextChanged(object sender, EventArgs e)
        {
            int idno = Converter.GetInteger(txtUserId.Text);
            A2ZSYSIDSDTO dto = new A2ZSYSIDSDTO();
            dto = A2ZSYSIDSDTO.GetUserInformation(idno, "A2ZHKMCUS");
            if (dto.IdsNo > 0)
            {
                ddlUserId.SelectedValue = Converter.GetString(dto.IdsNo);
            }
            else
            {
                IDsNotFoundMsg();
                ddlUserId.SelectedValue = "-Select-";
                txtUserId.Text = string.Empty;
                txtUserId.Focus();
                return;
            }
        }


    }
}
