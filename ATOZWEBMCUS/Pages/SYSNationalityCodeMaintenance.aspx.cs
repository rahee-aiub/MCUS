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
    public partial class SYSNationalityCodeMaintenance : System.Web.UI.Page
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
            string sqlquery = "SELECT NationalityCode,NationalityDescription from A2ZNATIONALITY";
            ddlNationality = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlNationality, "A2ZHKMCUS");
        }


        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            if (ddlNationality.SelectedValue == "-Select-")
            {
                txtDescription.Focus();

            }
            try
            {

                if (txtcode.Text != string.Empty)
                {
                    Int16 MainCode = Converter.GetSmallInteger(txtcode.Text);
                    A2ZNATIONALITYDTO getDTO = (A2ZNATIONALITYDTO.GetInformation(MainCode));

                    if (getDTO.NationalityCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.NationalityCode);
                        txtDescription.Text = Converter.GetString(getDTO.NationalityDescription);
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        ddlNationality.SelectedValue = Converter.GetString(getDTO.NationalityCode);
                        txtDescription.Focus();
                    }
                    else
                    {
                        txtDescription.Text = string.Empty;
                     //   ddlNationality.SelectedValue = "-Select-";
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

        protected void ddlNationality_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNationality.SelectedValue == "-Select-")
            {
                txtcode.Focus();
                txtcode.Text = string.Empty;
                txtDescription.Text = string.Empty;
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
            }

            try
            {


                if (ddlNationality.SelectedValue != "-Select-")
                {

                    Int16 MainCode = Converter.GetSmallInteger(ddlNationality.SelectedValue);
                    A2ZNATIONALITYDTO getDTO = (A2ZNATIONALITYDTO.GetInformation(MainCode));
                    if (getDTO.NationalityCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.NationalityCode);
                        txtDescription.Text = Converter.GetString(getDTO.NationalityDescription);
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
                A2ZNATIONALITYDTO objDTO = new A2ZNATIONALITYDTO();

                objDTO.NationalityCode = Converter.GetSmallInteger(txtcode.Text);
                objDTO.NationalityDescription = Converter.GetString(txtDescription.Text);

                int roweffect = A2ZNATIONALITYDTO.InsertInformation(objDTO);
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
            A2ZNATIONALITYDTO UpDTO = new A2ZNATIONALITYDTO();
            UpDTO.NationalityCode = Converter.GetSmallInteger(txtcode.Text);
            UpDTO.NationalityDescription = Converter.GetString(txtDescription.Text);

            int roweffect = A2ZNATIONALITYDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {

                dropdown();
                clearinfo();
         //       ddlNationality.SelectedValue = "-Select-";
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                txtcode.Focus();
                gvDetail();
            }
        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT NationalityCode,NationalityDescription FROM A2ZNATIONALITY";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZHKMCUS");
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
