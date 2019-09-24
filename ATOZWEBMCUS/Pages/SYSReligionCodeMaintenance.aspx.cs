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
    public partial class SYSReligionCodeMaintenance : System.Web.UI.Page
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
        protected void dropdown()
        {
            string sqlquery = "SELECT RelegionCode,RelegionDescription from A2ZRELIGION";
            ddlReligion = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlReligion, "A2ZHKMCUS");

        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            if (ddlReligion.SelectedValue == "-Select-")
            {
                txtDescription.Focus();
               
            }
            try
            {

                if (txtcode.Text != string.Empty)
                {
                    Int16 MainCode = Converter.GetSmallInteger(txtcode.Text);
                    A2ZRELIGIONDTO getDTO = (A2ZRELIGIONDTO.GetInformation(MainCode));

                    if (getDTO.RelegionCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.RelegionCode);
                        txtDescription.Text = Converter.GetString(getDTO.RelegionDescription);
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        ddlReligion.SelectedValue = Converter.GetString(getDTO.RelegionCode);
                        txtDescription.Focus();
                    }
                    else
                    {
                        txtDescription.Text = string.Empty;
                   //     ddlReligion.SelectedValue = "-Select-";
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

        protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReligion.SelectedValue == "-Select-")
            {
                txtcode.Focus();
                txtcode.Text = string.Empty;
                txtDescription.Text = string.Empty;
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
            }

            try
            {


                if (ddlReligion.SelectedValue != "-Select-")
                {

                    Int16 MainCode = Converter.GetSmallInteger(ddlReligion.SelectedValue);
                    A2ZRELIGIONDTO getDTO = (A2ZRELIGIONDTO.GetInformation(MainCode));
                    if (getDTO.RelegionCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.RelegionCode);
                        txtDescription.Text = Converter.GetString(getDTO.RelegionDescription);
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
                A2ZRELIGIONDTO objDTO = new A2ZRELIGIONDTO();

                objDTO.RelegionCode = Converter.GetSmallInteger(txtcode.Text);
                objDTO.RelegionDescription = Converter.GetString(txtDescription.Text);

                int roweffect = A2ZRELIGIONDTO.InsertInformation(objDTO);
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
            A2ZRELIGIONDTO UpDTO = new A2ZRELIGIONDTO();
            UpDTO.RelegionCode = Converter.GetSmallInteger(txtcode.Text);
            UpDTO.RelegionDescription = Converter.GetString(txtDescription.Text);

            int roweffect = A2ZRELIGIONDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {

                dropdown();
                clearinfo();
             //   ddlReligion.SelectedValue = "-Select-";
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                txtcode.Focus();
                gvDetail();

            }
        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT RelegionCode,RelegionDescription FROM A2ZRELIGION ";
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
