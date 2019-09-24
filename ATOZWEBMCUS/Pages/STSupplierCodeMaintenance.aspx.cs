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
using DataAccessLayer.DTO.Inventory;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.CustomerServices;

namespace ATOZWEBMCUS.Pages
{
    public partial class STSupplierCodeMaintenance : System.Web.UI.Page
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
            string sqlquery = "SELECT SuppCode,SuppName from A2ZSUPPLIER";
            ddlSupplier = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlSupplier, "A2ZSTMCUS");
        }
        public void clearInfo()
        {
            ddlSupplier.SelectedIndex = 0;
            txtDescription.Text = string.Empty;
            txtAddLine1.Text = string.Empty;
            txtAddLine2.Text = string.Empty;
            txtAddLine3.Text = string.Empty;
            txtSuppEmail.Text = string.Empty;
            txtSuppFax.Text = string.Empty;
            txtSuppMobileNo.Text = string.Empty;
            txtSuppTelephone.Text = string.Empty;
        }

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSupplier.SelectedValue == "-Select-")
            {
                clearInfo();
                ddlSupplier.Focus();
                BtnUpdate.Visible = false;
                BtnSubmit.Visible = true;
               
            }

            try
            {

                if (ddlSupplier.SelectedValue != "-Select-")
                {

                    int code = Converter.GetInteger(ddlSupplier.SelectedValue);
                    A2ZSUPPLIERDTO getDTO = (A2ZSUPPLIERDTO.GetInformation(code));
                    if (getDTO.SuppCode > 0)
                    {
                        hdnSuppCode.Text = Converter.GetString(getDTO.SuppCode);
                        txtDescription.Focus();
                        txtDescription.Text = Converter.GetString(getDTO.SuppName);

                        txtAddLine1.Text = Converter.GetString(getDTO.SuppAddL1);
                        txtAddLine2.Text = Converter.GetString(getDTO.SuppAddL2);
                        txtAddLine3.Text = Converter.GetString(getDTO.SuppAddL3);
                        txtSuppEmail.Text = Converter.GetString(getDTO.SuppEmail);
                        txtSuppFax.Text = Converter.GetString(getDTO.SuppFax);
                        txtSuppMobileNo.Text = Converter.GetString(getDTO.SuppMobile);
                        txtSuppTelephone.Text = Converter.GetString(getDTO.SuppTel);
                        
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
                if (txtDescription.Text != string.Empty)
                {
                    gvDetail();
                    gvDetailInfo.Visible = false;
                    
                    int totrec = gvDetailInfo.Rows.Count;

                    int lastWaseCode = (totrec + 1);

                    if (lastWaseCode < 10)
                    {
                        hdnSuppCode.Text = Converter.GetString("0" + lastWaseCode);
                    }

                    A2ZSUPPLIERDTO objDTO = new A2ZSUPPLIERDTO();

                    objDTO.SuppCode = Converter.GetInteger(hdnSuppCode.Text);

                    objDTO.SuppName = Converter.GetString(txtDescription.Text);

                    objDTO.SuppAddL1 = Converter.GetString(txtAddLine1.Text);

                    objDTO.SuppAddL2 = Converter.GetString(txtAddLine2.Text);

                    objDTO.SuppAddL3 = Converter.GetString(txtAddLine3.Text);

                    objDTO.SuppTel = Converter.GetString(txtSuppTelephone.Text);

                    objDTO.SuppMobile = Converter.GetString(txtSuppMobileNo.Text);

                    objDTO.SuppFax = Converter.GetString(txtSuppFax.Text);

                    objDTO.SuppEmail = Converter.GetString(txtSuppEmail.Text);

                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    objDTO.SuppStatDate = Converter.GetDateTime(dto.ProcessDate.ToLongDateString());

                    int roweffect = A2ZSUPPLIERDTO.InsertInformation(objDTO);
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
            if (ddlSupplier.SelectedValue != "-Select-")
            {

                A2ZSUPPLIERDTO UpDTO = new A2ZSUPPLIERDTO();
                UpDTO.SuppCode = Converter.GetInteger(hdnSuppCode.Text);
           
                UpDTO.SuppName = Converter.GetString(txtDescription.Text);

                UpDTO.SuppAddL1 = Converter.GetString(txtAddLine1.Text);

                UpDTO.SuppAddL2 = Converter.GetString(txtAddLine2.Text);

                UpDTO.SuppAddL3 = Converter.GetString(txtAddLine3.Text);

                UpDTO.SuppTel = Converter.GetString(txtSuppTelephone.Text);

                UpDTO.SuppMobile = Converter.GetString(txtSuppMobileNo.Text);

                UpDTO.SuppFax = Converter.GetString(txtSuppFax.Text);

                UpDTO.SuppEmail = Converter.GetString(txtSuppEmail.Text);

                int roweffect = A2ZSUPPLIERDTO.UpdateInformation(UpDTO);
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
            string sqlquery3 = "SELECT SuppCode,SuppName FROM A2ZSUPPLIER WHERE SuppCode!=0 ";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZSTMCUS");
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
