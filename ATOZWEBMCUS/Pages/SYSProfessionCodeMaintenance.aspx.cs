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
    public partial class SYSProfessionCodeMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDescription.Focus();
                BtnUpdate.Visible = false;
                dropdown();
            }
        }

        private void dropdown()
        {
            string sqlquery = "SELECT ProfessionCode,ProfessionDescription from A2ZPROFESSION";
            ddlProfession = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlProfession, "A2ZCSMCUS");
        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            if (ddlProfession.SelectedValue == "-Select-")
            {
                txtDescription.Focus();

            }
            try
            {

                if (txtcode.Text != string.Empty)
                {
                    Int16 MainCode = Converter.GetSmallInteger(txtcode.Text);
                    A2ZPROFESSIONDTO getDTO = (A2ZPROFESSIONDTO.GetInformation(MainCode));

                    if (getDTO.ProfessionCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.ProfessionCode);
                        txtDescription.Text = Converter.GetString(getDTO.ProfessionDescription);
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        ddlProfession.SelectedValue = Converter.GetString(getDTO.ProfessionCode);
                        txtDescription.Focus();
                    }
                    else
                    {                       
                        txtcode.Text = string.Empty;
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                        txtDescription.Text = string.Empty;
                        txtcode.Focus();

                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlProfession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProfession.SelectedValue == "-Select-")
            {
                txtcode.Focus();
                txtcode.Text = string.Empty;
                txtDescription.Text = string.Empty;
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
            }

            try
            {


                if (ddlProfession.SelectedValue != "-Select-")
                {

                    Int16 MainCode = Converter.GetSmallInteger(ddlProfession.SelectedValue);
                    A2ZPROFESSIONDTO getDTO = (A2ZPROFESSIONDTO.GetInformation(MainCode));
                    if (getDTO.ProfessionCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.ProfessionCode);
                        txtDescription.Text = Converter.GetString(getDTO.ProfessionDescription);
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
                if (ddlProfession.SelectedValue == "-Select-" && txtDescription.Text != string.Empty)
                {
                    gvDetail();
                    gvDetailInfo.Visible = false;

                    int totrec = gvDetailInfo.Rows.Count;

                    int lastCode = (totrec);
                    txtcode.Text = Converter.GetString(lastCode);

                    A2ZPROFESSIONDTO objDTO = new A2ZPROFESSIONDTO();

                    objDTO.ProfessionCode = Converter.GetSmallInteger(txtcode.Text);
                    objDTO.ProfessionDescription = Converter.GetString(txtDescription.Text);

                    int roweffect = A2ZPROFESSIONDTO.InsertInformation(objDTO);
                    if (roweffect > 0)
                    {
                        txtcode.Focus();
                        clearinfo();
                        dropdown();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (ddlProfession.SelectedValue != "-Select-")
            {

                A2ZPROFESSIONDTO UpDTO = new A2ZPROFESSIONDTO();
                UpDTO.ProfessionCode = Converter.GetSmallInteger(txtcode.Text);
                UpDTO.ProfessionDescription = Converter.GetString(txtDescription.Text);

                int roweffect = A2ZPROFESSIONDTO.UpdateInformation(UpDTO);
                if (roweffect > 0)
                {

                    dropdown();
                    clearinfo();
                    //    ddlProfession.SelectedValue = "-Select-";
                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;
                    txtcode.Focus();

                }
            }
        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT ProfessionCode,ProfessionDescription FROM A2ZPROFESSION";
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
