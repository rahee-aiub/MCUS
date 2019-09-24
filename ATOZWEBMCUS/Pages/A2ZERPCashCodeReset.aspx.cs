using System;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System.Drawing;

using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO;
using System.Web.UI;
using System.Data;
using DataAccessLayer.DTO.GeneralLedger;
using DataAccessLayer.DTO.CustomerServices;



namespace ATOZWEBMCUS.Pages
{
    public partial class A2ZERPCashCodeReset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {

                }
                else
                {

                    A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
                    string date1 = dt2.ToString("MM/dd/yyyy");
                    txtTranDate.Text = date1;

                    IdsDropdown();
                    txtIdsNo.Focus();

                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Err.Load Problem');</script>");
                //throw ex;
            }

        }

        private void IdsDropdown()
        {
            string sqlquery = "SELECT IdsNo,IdsName from A2ZSYSIDS ORDER BY IdsName ASC";
            ddlIdsNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlIdsNo, "A2ZHKMCUS");
        }

        protected void CashCodeDropdown()
        {
            string sqlquery = @"SELECT FromCashCode,FromCashCodeDesc from A2ZUSERCASHCODE WHERE IdsNo='" + txtIdsNo.Text + "' AND Status !=0";

            ddlCashCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlCashCode, "A2ZCSMCUS");
        }

        private void IDsNotFoundMsg()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ids Does Not Exist');", true);
            return;

        }


        protected void txtIdsNo_TextChanged(object sender, EventArgs e)
        {
            int idno = Converter.GetInteger(txtIdsNo.Text);
            A2ZSYSIDSDTO dto = new A2ZSYSIDSDTO();
            dto = A2ZSYSIDSDTO.GetUserInformation(idno, "A2ZHKMCUS");
            if (dto.IdsNo > 0)
            {
                ddlIdsNo.SelectedValue = Converter.GetString(dto.IdsNo);

                CashCodeDropdown();
            }
            else
            {
                IDsNotFoundMsg();
                ddlIdsNo.SelectedValue = "-Select-";
                txtIdsNo.Text = string.Empty;
                txtIdsNo.Focus();
                return;
            }


            ddlIdsNo.SelectedValue = Converter.GetString(txtIdsNo.Text);
        }

        protected void ddlIdsNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIdsNo.Text = Converter.GetString(ddlIdsNo.SelectedValue);
            CashCodeDropdown();
        }



        protected void txtCashCode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtCashCode.Text != string.Empty)
                {

                    string qry = "SELECT FromCashCodeDesc FROM A2ZUSERCASHCODE where IdsNo='" + txtIdsNo.Text + "' AND FromCashCode='" + txtCashCode.Text + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        ddlCashCode.SelectedValue = Converter.GetString(txtCashCode.Text);
                    }
                    else
                    {
                        txtCashCode.Text = string.Empty;
                        txtCashCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtGLCashCode_TextChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void ddlCashCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlCashCode.SelectedValue != "-Select-")
                {
                    txtCashCode.Text = Converter.GetString(ddlCashCode.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlGLCashCode_SelectedIndexChanged Problem');</script>");
                //throw ex;
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

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Exit Problem');</script>");
                //throw ex;
            }
        }


        private void UpdateMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Reset Booth Day End Confirmation');", true);
            return;
        }


        protected void ClearInformation()
        {
            txtIdsNo.Text = string.Empty;
            ddlIdsNo.SelectedIndex = 0;
            txtCashCode.Text = string.Empty;
            ddlCashCode.SelectedIndex = 0;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                string strQuery = "UPDATE A2ZBTRNCTRL SET  Status = 0,StatusName= null WHERE ProcessDate='" + txtTranDate.Text + "' AND CashCodeNo='" + txtCashCode.Text + "'";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                if (rowEffect > 0)
                {
                    string strQuery1 = "UPDATE A2ZUSERCASHCODE SET  Status = 0 where IdsNo='" + txtIdsNo.Text + "' AND FromCashCode='" + txtCashCode.Text + "'";
                    int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZCSMCUS"));


                    ClearInformation();
                    txtIdsNo.Focus();
                    UpdateMSG();

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Add Problem');</script>");
                //throw ex;
            }
        }


    }
}
