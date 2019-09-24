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
    public partial class SYSDesignationCodeMaintenance : System.Web.UI.Page
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
            string sqlquery = "SELECT DesigCode,DesigDescription from A2ZDESIGNATION";
            ddlDesignation = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlDesignation, "A2ZHRMCUS");
        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            if (ddlDesignation.SelectedValue == "-Select-")
            {
                txtDescription.Focus();

            }
            try
            {

                if (txtcode.Text != string.Empty)
                {
                    Int16 MainCode = Converter.GetSmallInteger(txtcode.Text);
                    A2ZDESIGNATIONDTO getDTO = (A2ZDESIGNATIONDTO.GetInformation(MainCode));

                    if (getDTO.DesignationCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.DesignationCode);
                        txtDescription.Text = Converter.GetString(getDTO.DesignationDescription);
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        ddlDesignation.SelectedValue = Converter.GetString(getDTO.DesignationCode);
                        txtDescription.Focus();
                    }
                    else
                    {
                        txtDescription.Text = string.Empty;
                        //    ddlDesignation.SelectedValue = "-Select-";
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

        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDesignation.SelectedValue == "-Select-")
            {
                txtcode.Focus();
                txtcode.Text = string.Empty;
                txtDescription.Text = string.Empty;
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
            }

            try
            {


                if (ddlDesignation.SelectedValue != "-Select-")
                {

                    Int16 MainCode = Converter.GetSmallInteger(ddlDesignation.SelectedValue);
                    A2ZDESIGNATIONDTO getDTO = (A2ZDESIGNATIONDTO.GetInformation(MainCode));
                    if (getDTO.DesignationCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.DesignationCode);
                        txtDescription.Text = Converter.GetString(getDTO.DesignationDescription);
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
                A2ZDESIGNATIONDTO objDTO = new A2ZDESIGNATIONDTO();

                objDTO.DesignationCode = Converter.GetSmallInteger(txtcode.Text);
                objDTO.DesignationDescription = Converter.GetString(txtDescription.Text);

                int roweffect = A2ZDESIGNATIONDTO.InsertInformation(objDTO);
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
            A2ZDESIGNATIONDTO UpDTO = new A2ZDESIGNATIONDTO();
            UpDTO.DesignationCode = Converter.GetSmallInteger(txtcode.Text);
            UpDTO.DesignationDescription = Converter.GetString(txtDescription.Text);

            int roweffect = A2ZDESIGNATIONDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {

                dropdown();
                clearinfo();
                //     ddlDesignation.SelectedValue = "-Select-";
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                txtcode.Focus();
                gvDetail();

            }
        }
        protected void gvDetail()
        {
            string sqlquery3 = "SELECT DesigCode,DesigDescription FROM A2ZDESIGNATION";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZHRMCUS");
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
