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
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.SystemControl;

namespace ATOZWEBMCUS.Pages
{
    public partial class SYSSectionCodeMaintenance : System.Web.UI.Page
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
            string sqlquery = "SELECT SectionCode,SectionName from A2ZSECTION";
            ddlSection = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlSection, "A2ZHKMCUS");
        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            if (ddlSection.SelectedValue == "-Select-")
            {
                txtDescription.Focus();

            }
            try
            {

                if (txtcode.Text != string.Empty)
                {
                    Int16 MainCode = Converter.GetSmallInteger(txtcode.Text);
                    A2ZSECTIONDTO getDTO = (A2ZSECTIONDTO.GetInformation(MainCode));

                    if (getDTO.SectionCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.SectionCode);
                        txtDescription.Text = Converter.GetString(getDTO.SectionName);
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        ddlSection.SelectedValue = Converter.GetString(getDTO.SectionCode);
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

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSection.SelectedValue == "-Select-")
            {
                txtcode.Focus();
                txtcode.Text = string.Empty;
                txtDescription.Text = string.Empty;
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
            }

            try
            {


                if (ddlSection.SelectedValue != "-Select-")
                {

                    Int16 MainCode = Converter.GetSmallInteger(ddlSection.SelectedValue);
                    A2ZSECTIONDTO getDTO = (A2ZSECTIONDTO.GetInformation(MainCode));
                    if (getDTO.SectionCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.SectionCode);
                        txtDescription.Text = Converter.GetString(getDTO.SectionName);
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
                A2ZSECTIONDTO objDTO = new A2ZSECTIONDTO();

                objDTO.SectionCode = Converter.GetSmallInteger(txtcode.Text);
                objDTO.SectionName = Converter.GetString(txtDescription.Text);

                int roweffect = A2ZSECTIONDTO.InsertInformation(objDTO);
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
            A2ZSECTIONDTO UpDTO = new A2ZSECTIONDTO();
            UpDTO.SectionCode = Converter.GetSmallInteger(txtcode.Text);
            UpDTO.SectionName = Converter.GetString(txtDescription.Text);

            int roweffect = A2ZSECTIONDTO.UpdateInformation(UpDTO);
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
            string sqlquery3 = "SELECT SectionCode,SectionName FROM A2ZSECTION";
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