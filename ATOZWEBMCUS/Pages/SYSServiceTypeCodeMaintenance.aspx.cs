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
    public partial class SYSServiceTypeCodeMaintenance : System.Web.UI.Page
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
            string sqlquery = "SELECT ServiceType,ServiceName from A2ZSERVICETYPE";
            ddlServiceType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlServiceType, "A2ZHRMCUS");
        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlServiceType.SelectedValue == "-Select-")
                {
                    txtDescription.Focus();

                }

                if (txtcode.Text != string.Empty)
                {
                    Int16 MainCode = Converter.GetSmallInteger(txtcode.Text);
                    A2ZSERVICETYPEDTO getDTO = (A2ZSERVICETYPEDTO.GetInformation(MainCode));

                    if (getDTO.ServiceType > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.ServiceType);
                        txtDescription.Text = Converter.GetString(getDTO.ServiceName);
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        ddlServiceType.SelectedValue = Converter.GetString(getDTO.ServiceType);
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtcode_TextChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void ddlServiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlServiceType.SelectedValue == "-Select-")
                {
                    txtcode.Focus();
                    txtcode.Text = string.Empty;
                    txtDescription.Text = string.Empty;
                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;
                }


                if (ddlServiceType.SelectedValue != "-Select-")
                {

                    Int16 MainCode = Converter.GetSmallInteger(ddlServiceType.SelectedValue);
                    A2ZSERVICETYPEDTO getDTO = (A2ZSERVICETYPEDTO.GetInformation(MainCode));
                    if (getDTO.ServiceType > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.ServiceType);
                        txtDescription.Text = Converter.GetString(getDTO.ServiceName);
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlServiceType_SelectedIndexChanged Problem');</script>");
                //throw ex;
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
                A2ZSERVICETYPEDTO objDTO = new A2ZSERVICETYPEDTO();

                objDTO.ServiceType = Converter.GetSmallInteger(txtcode.Text);
                objDTO.ServiceName = Converter.GetString(txtDescription.Text);

                int roweffect = A2ZSERVICETYPEDTO.InsertInformation(objDTO);
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnSubmit_Click Problem');</script>");
                throw ex;
            }

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                A2ZSERVICETYPEDTO UpDTO = new A2ZSERVICETYPEDTO();
                UpDTO.ServiceType = Converter.GetSmallInteger(txtcode.Text);
                UpDTO.ServiceName = Converter.GetString(txtDescription.Text);

                int roweffect = A2ZSERVICETYPEDTO.UpdateInformation(UpDTO);
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
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnUpdate_Click Problem');</script>");
                throw ex;
            }
        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT ServiceType,ServiceName FROM A2ZSERVICETYPE";
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