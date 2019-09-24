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
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.Utility;

namespace ATOZWEBMCUS.Pages
{
    public partial class SYSDivisionCodeMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DivisionDropdown();
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                
                txtDescription.Text = string.Empty;
                txtDescription.Focus();
            }

        }
        private void DivisionDropdown()
        {

            string sqlquery = "SELECT DiviCode,DiviDescription from A2ZDIVISION";
            ddlDivision = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlDivision, "A2ZHKMCUS");

        }
        public void clearInfo()
        {
            txtDescription.Text = string.Empty;

        }
        
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDivision.SelectedValue == "-Select-")
            {
                clearInfo();
                ddlDivision.Focus();
               
            }

            try
            {


                if (ddlDivision.SelectedValue != "-Select-")
                {

                    int code = Converter.GetInteger(ddlDivision.SelectedValue);
                    A2ZDIVISIONDTO getDTO = (A2ZDIVISIONDTO.GetInformation(code));
                    if (getDTO.DivisionCode > 0)
                    {
                        hdnDiviCode.Text = Converter.GetString(getDTO.DivisionCode);
                        hdnDiviOrgCode.Text = Converter.GetString(getDTO.DivisionOrgCode);
                        txtDescription.Text = Converter.GetString(getDTO.DivisionDescription);
                        txtDescription.Focus();
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
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
                if (ddlDivision.SelectedValue == "-Select-" && txtDescription.Text != string.Empty)
                {
                    gvDetail();
                    gvDetailInfo.Visible = false;
                    
                    int totrec = gvDetailInfo.Rows.Count;

                    int lastDiviCode = (totrec + 1);
                    hdnDiviCode.Text = Converter.GetString(lastDiviCode);
                    string result = hdnDiviCode.Text + "000000";
                    hdnDiviOrgCode.Text = Converter.GetString(result);

                    if (lastDiviCode < 10)
                    {
                        hdnDiviCode.Text = Converter.GetString("0" + lastDiviCode);
                    }

                    A2ZDIVISIONDTO objDTO = new A2ZDIVISIONDTO();

                    objDTO.DivisionCode = Converter.GetInteger(hdnDiviCode.Text);
                    objDTO.DivisionOrgCode = Converter.GetInteger(hdnDiviOrgCode.Text);
                    objDTO.DivisionDescription = Converter.GetString(txtDescription.Text);

                    int roweffect = A2ZDIVISIONDTO.InsertInformation(objDTO);
                    if (roweffect > 0)
                    {
                        DivisionDropdown();
                        clearInfo();
                        BtnUpdate.Visible = false;
                        BtnSubmit.Visible = true;
                        gvDetail();
                        gvDetailInfo.Visible = false;
                        txtDescription.Text = string.Empty;
                        txtDescription.Focus();
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
            if (ddlDivision.SelectedValue != "-Select-")
            {

                A2ZDIVISIONDTO UpDTO = new A2ZDIVISIONDTO();
                UpDTO.DivisionCode = Converter.GetInteger(hdnDiviCode.Text);
                UpDTO.DivisionOrgCode = Converter.GetInteger(hdnDiviOrgCode.Text);
                UpDTO.DivisionDescription = Converter.GetString(txtDescription.Text);
                int roweffect = A2ZDIVISIONDTO.UpdateInformation(UpDTO);
                if (roweffect > 0)
                {
                    DivisionDropdown();
                    clearInfo();
                    BtnUpdate.Visible = false;
                    BtnSubmit.Visible = true;
                    gvDetail();
                    gvDetailInfo.Visible = false;
                    txtDescription.Text = string.Empty;
                    txtDescription.Focus();
                }
            }
        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT DiviCode,DiviDescription FROM A2ZDIVISION WHERE DiviCode!=0 ";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZHKMCUS");
        }
        protected void BtnView_Click(object sender, EventArgs e)
        {
            gvDetailInfo.Visible = true;
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
