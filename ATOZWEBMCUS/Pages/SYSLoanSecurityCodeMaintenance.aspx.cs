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
    public partial class SYSLoanSecurityCodeMaintenance : System.Web.UI.Page
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
            string sqlquery = "SELECT LSecurityCode,LSecurityDescription from A2ZLSECURITY";
            ddlSecurity = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlSecurity, "A2ZCSMCUS");
        }
        protected void gvDetail()
        {
            string sqlquery3 = "SELECT LSecurityCode,LSecurityDescription FROM A2ZLSECURITY";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
        }
        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            if (ddlSecurity.SelectedValue == "-Select-")
            {
                txtDescription.Focus();

            }
            try
            {

                if (txtcode.Text != string.Empty)
                {
                    Int16 MainCode = Converter.GetSmallInteger(txtcode.Text);
                    A2ZLSECURITYDTO getDTO = (A2ZLSECURITYDTO.GetInformation(MainCode));

                    if (getDTO.LSecurityCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.LSecurityCode);
                        txtDescription.Text = Converter.GetString(getDTO.LSecurityDescription);
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        ddlSecurity.SelectedValue = Converter.GetString(getDTO.LSecurityCode);
                        txtDescription.Focus();
                    }
                    else
                    {
                        txtDescription.Text = string.Empty;
                  //     ddlSecurity.SelectedValue = "-Select-";
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

        protected void ddlSecurity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSecurity.SelectedValue == "-Select-")
            {
                txtcode.Focus();
                txtcode.Text = string.Empty;
                txtDescription.Text = string.Empty;
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
            }

            try
            {


                if (ddlSecurity.SelectedValue != "-Select-")
                {

                    Int16 MainCode = Converter.GetSmallInteger(ddlSecurity.SelectedValue);
                    A2ZLSECURITYDTO getDTO = (A2ZLSECURITYDTO.GetInformation(MainCode));
                    if (getDTO.LSecurityCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.LSecurityCode);
                        txtDescription.Text = Converter.GetString(getDTO.LSecurityDescription);
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
                A2ZLSECURITYDTO objDTO = new A2ZLSECURITYDTO();

                objDTO.LSecurityCode = Converter.GetSmallInteger(txtcode.Text);
                objDTO.LSecurityDescription = Converter.GetString(txtDescription.Text);

                int roweffect = A2ZLSECURITYDTO.InsertInformation(objDTO);
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

        private void clearinfo()
        {
            txtcode.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            A2ZLSECURITYDTO UpDTO = new A2ZLSECURITYDTO();
            UpDTO.LSecurityCode = Converter.GetSmallInteger(txtcode.Text);
            UpDTO.LSecurityDescription = Converter.GetString(txtDescription.Text);

            int roweffect = A2ZLSECURITYDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {

                dropdown();
                clearinfo();
            //    ddlSecurity.SelectedValue = "-Select-";
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                txtcode.Focus();
                gvDetail();

            }
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
