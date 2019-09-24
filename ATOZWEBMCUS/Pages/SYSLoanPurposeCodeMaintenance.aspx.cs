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
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.SystemControl;

namespace ATOZWEBMCUS.Pages
{
    public partial class SYSLoanPurposeCodeMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtcode.Focus();
                BtnUpdate.Visible = false;
                dropdown();
            }
        }
        private void dropdown()
        {
            string sqlquery = "SELECT LPurposeCode,LPurposeDescription from A2ZLPURPOSE";
            ddlPurpose = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPurpose, "A2ZCSMCUS");
        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT LPurposeCode,LPurposeDescription FROM A2ZLPURPOSE";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            if (ddlPurpose.SelectedValue == "-Select-")
            {
                txtDescription.Focus();

            }
            try
            {

                if (txtcode.Text != string.Empty)
                {
                    Int16 MainCode = Converter.GetSmallInteger(txtcode.Text);
                    A2ZLPURPOSEDTO getDTO = (A2ZLPURPOSEDTO.GetInformation(MainCode));

                    if (getDTO.LPurposeCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.LPurposeCode);
                        txtDescription.Text = Converter.GetString(getDTO.LPurposeDescription);
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        ddlPurpose.SelectedValue = Converter.GetString(getDTO.LPurposeCode);
                        txtDescription.Focus();
                    }
                    else
                    {
                        txtDescription.Text = string.Empty;
                   //     ddlPurpose.SelectedValue = "-Select-";
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                        txtDescription.Focus();

                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlPurpose_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPurpose.SelectedValue == "-Select-")
            {
                txtcode.Focus();
                txtcode.Text = string.Empty;
                txtDescription.Text = string.Empty;
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
            }

            try
            {


                if (ddlPurpose.SelectedValue != "-Select-")
                {

                    Int16 MainCode = Converter.GetSmallInteger(ddlPurpose.SelectedValue);
                    A2ZLPURPOSEDTO getDTO = (A2ZLPURPOSEDTO.GetInformation(MainCode));
                    if (getDTO.LPurposeCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.LPurposeCode);
                        txtDescription.Text = Converter.GetString(getDTO.LPurposeDescription);
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
                throw ex;
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZLPURPOSEDTO objDTO = new A2ZLPURPOSEDTO();

                objDTO.LPurposeCode = Converter.GetSmallInteger(txtcode.Text);
                objDTO.LPurposeDescription = Converter.GetString(txtDescription.Text);

                int roweffect = A2ZLPURPOSEDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    txtcode.Focus();
                    clearinfo();
                    dropdown();
                    gvDetail();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            A2ZLPURPOSEDTO UpDTO = new A2ZLPURPOSEDTO();
            UpDTO.LPurposeCode = Converter.GetSmallInteger(txtcode.Text);
            UpDTO.LPurposeDescription = Converter.GetString(txtDescription.Text);

            int roweffect = A2ZLPURPOSEDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {

                dropdown();
                clearinfo();
            //    ddlPurpose.SelectedValue = "-Select-";
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                txtcode.Focus();
                gvDetail();

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

        protected void BtnView_Click(object sender, EventArgs e)
        {
            gvDetail();
        }

    }
}
