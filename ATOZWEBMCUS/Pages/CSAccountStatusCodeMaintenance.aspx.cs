using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
//using A2Z.Web.Constants;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSAccountStatusCodeMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    txtcode.Focus();
                    BtnUpdate.Visible = false;
                    dropdown();
                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }
        private void dropdown()
        {
            string sqlquery = "SELECT AccStatusCode,AccStatusDescription from A2ZACCSTATUS";
            ddlAcStatus = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAcStatus, "A2ZCSMCUS");
        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlAcStatus.SelectedValue == "-Select-")
                {
                    txtDescription.Focus();

                }

                if (txtcode.Text != string.Empty)
                {
                    Int16 MainCode = Converter.GetSmallInteger(txtcode.Text);
                    A2ZACCSTATUSDTO getDTO = (A2ZACCSTATUSDTO.GetInformation(MainCode));

                    if (getDTO.AccStatusCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.AccStatusCode);
                        txtDescription.Text = Converter.GetString(getDTO.AccStatusDescription);
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        ddlAcStatus.SelectedValue = Converter.GetString(getDTO.AccStatusCode);
                        txtDescription.Focus();
                    }
                    else
                    {
                        txtDescription.Text = string.Empty;
                        ddlAcStatus.SelectedValue = "-Select-";
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                        txtDescription.Focus();

                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtcode_TextChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void ddlAcStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlAcStatus.SelectedValue == "-Select-")
                {
                    txtcode.Focus();
                    txtcode.Text = string.Empty;
                    txtDescription.Text = string.Empty;
                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;
                }


                if (ddlAcStatus.SelectedValue != "-Select-")
                {

                    Int16 MainCode = Converter.GetSmallInteger(ddlAcStatus.SelectedValue);
                    A2ZACCSTATUSDTO getDTO = (A2ZACCSTATUSDTO.GetInformation(MainCode));
                    if (getDTO.AccStatusCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.AccStatusCode);
                        txtDescription.Text = Converter.GetString(getDTO.AccStatusDescription);
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        txtDescription.Focus();


                    }
                    else
                    {
                        txtcode.Focus();
                        txtcode.Text = string.Empty;
                        txtDescription.Text = string.Empty;
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlAcStatus_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZACCSTATUSDTO objDTO = new A2ZACCSTATUSDTO();

                objDTO.AccStatusCode = Converter.GetSmallInteger(txtcode.Text);
                objDTO.AccStatusDescription = Converter.GetString(txtDescription.Text);

                int roweffect = A2ZACCSTATUSDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    gvDetail();
                    txtcode.Focus();
                    clearinfo();
                    dropdown();
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnSubmit_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                A2ZACCSTATUSDTO UpDTO = new A2ZACCSTATUSDTO();
                UpDTO.AccStatusCode = Converter.GetSmallInteger(txtcode.Text);
                UpDTO.AccStatusDescription = Converter.GetString(txtDescription.Text);

                int roweffect = A2ZACCSTATUSDTO.UpdateInformation(UpDTO);
                if (roweffect > 0)
                {
                    gvDetail();
                    dropdown();
                    clearinfo();
                    ddlAcStatus.SelectedValue = "-Select-";
                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;
                    txtcode.Focus();

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnUpdate_Click Problem');</script>");
                //throw ex;
            }
        }
        private void clearinfo()
        {
            txtcode.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT AccStatusCode,AccStatusDescription FROM A2ZACCSTATUS";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
        }
        protected void BtnView_Click(object sender, EventArgs e)
        {
            gvDetail();
        }

        protected void gvDetailInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }
    }
}
