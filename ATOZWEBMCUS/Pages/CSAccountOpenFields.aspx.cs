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
    public partial class CSAccountOpenFields : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtcode.Focus();
                BtnDesUpdate.Visible = false;
                //dropdown();
            }

        }
        protected void dropdown()
        {
            string sqlquery = "SELECT Code,Description from A2ZACCFIELDS WHERE FieldsFlag='" + ddlFlag.SelectedValue + "'";
            ddlDesc = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlDesc, "A2ZCSMCUS");

        }

        protected void BtnDesSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                A2ZACCFIELDSDTO objDTO = new A2ZACCFIELDSDTO();
                objDTO.Flag = Converter.GetSmallInteger(ddlFlag.SelectedValue);
                objDTO.Code = Converter.GetSmallInteger(txtcode.Text);
                objDTO.Description = Converter.GetString(txtDescription.Text);

                int roweffect = A2ZACCFIELDSDTO.InsertInformation(objDTO);
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnDesSubmit_Click Problem');</script>");
                //throw ex;
            }

        }

        private void clearinfo()
        {
            txtcode.Text = string.Empty;
            txtDescription.Text = string.Empty;

        }

        protected void BtnDesExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void ddlDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlDesc.SelectedValue == "-Select-")
                {
                    txtcode.Focus();
                    txtcode.Text = string.Empty;
                    txtDescription.Text = string.Empty;
                    BtnDesSubmit.Visible = true;
                    BtnDesUpdate.Visible = false;
                }

                if (ddlDesc.SelectedValue != "-Select-")
                {
                    Int16 flag = Converter.GetSmallInteger(ddlFlag.SelectedValue);
                    Int16 MainCode = Converter.GetSmallInteger(ddlDesc.SelectedValue);
                    A2ZACCFIELDSDTO getDTO = (A2ZACCFIELDSDTO.GetInformation(MainCode, flag));
                    if (getDTO.Code > 0 && getDTO.Flag > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.Code);
                        txtDescription.Text = Converter.GetString(getDTO.Description);
                        BtnDesSubmit.Visible = false;
                        BtnDesUpdate.Visible = true;
                        txtDescription.Focus();


                    }
                    else
                    {
                        txtcode.Focus();
                        txtcode.Text = string.Empty;
                        txtDescription.Text = string.Empty;
                        BtnDesSubmit.Visible = true;
                        BtnDesUpdate.Visible = false;
                    }

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlDesc_SelectedIndexChanged Problem');</script>");
                
                //throw ex;
            }
        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlDesc.SelectedValue == "-Select-")
                {
                    return;
                }

                if (txtcode.Text != string.Empty)
                {
                    Int16 flag = Converter.GetSmallInteger(ddlFlag.SelectedValue);
                    Int16 MainCode = Converter.GetSmallInteger(txtcode.Text);
                    A2ZACCFIELDSDTO getDTO = (A2ZACCFIELDSDTO.GetInformation(MainCode, flag));

                    if (getDTO.Code > 0 && getDTO.Flag > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.Code);
                        txtDescription.Text = Converter.GetString(getDTO.Description);
                        BtnDesSubmit.Visible = false;
                        BtnDesUpdate.Visible = true;
                        ddlDesc.SelectedValue = Converter.GetString(getDTO.Code);
                        txtDescription.Focus();
                    }
                    else
                    {
                        txtDescription.Text = string.Empty;
                        //    ddlDesc.SelectedValue = "-Select-";
                        BtnDesSubmit.Visible = true;
                        BtnDesUpdate.Visible = false;
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

        protected void BtnDesUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZACCFIELDSDTO UpDTO = new A2ZACCFIELDSDTO();
                UpDTO.Flag = Converter.GetSmallInteger(ddlFlag.SelectedValue);
                UpDTO.Code = Converter.GetSmallInteger(txtcode.Text);
                UpDTO.Description = Converter.GetString(txtDescription.Text);

                int roweffect = A2ZACCFIELDSDTO.UpdateInformation(UpDTO);
                if (roweffect > 0)
                {
                    gvDetail();
                    dropdown();
                    clearinfo();
                    ddlDesc.SelectedValue = "-Select-";
                    BtnDesSubmit.Visible = true;
                    BtnDesUpdate.Visible = false;
                    txtcode.Focus();

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnDesUpdate_Click Problem');</script>");
                
                //throw ex;
            }
        }

        protected void ddlFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFlag.SelectedValue == "0")
            {
                clearinfo();
                txtcode.Text = string.Empty;
                txtcode.Focus();
                ddlDesc.SelectedIndex = 0;
                return;
            }


            dropdown();


        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT FieldsFlag,Code,Description FROM A2ZACCFIELDS";
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
