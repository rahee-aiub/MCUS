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
    public partial class SYSNatureCodeMaintenance : System.Web.UI.Page
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
            string sqlquery = "SELECT NatureCode,NatureDescription from A2ZNATURE";
            ddlNature = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlNature, "A2ZCSMCUS");
        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            if (ddlNature.SelectedValue == "-Select-")
            {
                txtDescription.Focus();

            }
            try
            {

                if (txtcode.Text != string.Empty)
                {
                    Int16 MainCode = Converter.GetSmallInteger(txtcode.Text);
                    A2ZNATUREDTO getDTO = (A2ZNATUREDTO.GetInformation(MainCode));

                    if (getDTO.NatureCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.NatureCode);
                        txtDescription.Text = Converter.GetString(getDTO.NatureDescription);
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        ddlNature.SelectedValue = Converter.GetString(getDTO.NatureCode);
                        txtDescription.Focus();
                    }
                    else
                    {
                        txtDescription.Text = string.Empty;
                      //  ddlNature.SelectedValue = "-Select-";
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

        protected void ddlNature_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNature.SelectedValue == "-Select-")
            {
                txtcode.Focus();
                txtcode.Text = string.Empty;
                txtDescription.Text = string.Empty;
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
            }

            try
            {


                if (ddlNature.SelectedValue != "-Select-")
                {

                    Int16 MainCode = Converter.GetSmallInteger(ddlNature.SelectedValue);
                    A2ZNATUREDTO getDTO = (A2ZNATUREDTO.GetInformation(MainCode));
                    if (getDTO.NatureCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.NatureCode);
                        txtDescription.Text = Converter.GetString(getDTO.NatureDescription);
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
        private void clearinfo()
        {
            txtcode.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZNATUREDTO objDTO = new A2ZNATUREDTO();

                objDTO.NatureCode = Converter.GetSmallInteger(txtcode.Text);
                objDTO.NatureDescription = Converter.GetString(txtDescription.Text);

                int roweffect = A2ZNATUREDTO.InsertInformation(objDTO);
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
            A2ZNATUREDTO UpDTO = new A2ZNATUREDTO();
            UpDTO.NatureCode = Converter.GetSmallInteger(txtcode.Text);
            UpDTO.NatureDescription = Converter.GetString(txtDescription.Text);

            int roweffect = A2ZNATUREDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {

                dropdown();
                clearinfo();
           //     ddlNature.SelectedValue = "-Select-";
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                gvDetail();
                txtcode.Focus();

            }
        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT NatureCode,NatureDescription FROM A2ZNATURE";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZCSMCUS");
        }
        protected void BtnView_Click(object sender, EventArgs e)
        {
            gvDetail();
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

    }
}
