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

namespace ATOZWEBMCUS.Pages
{
    public partial class STWareHouseCodeMaintenance : System.Web.UI.Page
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

            string sqlquery = "SELECT WaseCode,WaseDescription from A2ZWAREHOUSE";
            ddlWase = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlWase, "A2ZSTMCUS");
        }
        public void clearInfo()
        {
            txtDescription.Text = string.Empty;

        }
        
        protected void ddlWase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlWase.SelectedValue == "-Select-")
            {
                clearInfo();
                ddlWase.Focus();
               
            }

            try
            {


                if (ddlWase.SelectedValue != "-Select-")
                {

                    int code = Converter.GetInteger(ddlWase.SelectedValue);
                    A2ZWAREHOUSEDTO getDTO = (A2ZWAREHOUSEDTO.GetInformation(code));
                    if (getDTO.WaseCode > 0)
                    {
                        hdnWaseCode.Text = Converter.GetString(getDTO.WaseCode);

                        txtDescription.Text = Converter.GetString(getDTO.WaseDescription);
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
                if (ddlWase.SelectedValue == "-Select-" && txtDescription.Text != string.Empty)
                {
                    gvDetail();
                    gvDetailInfo.Visible = false;
                    
                    int totrec = gvDetailInfo.Rows.Count;

                    int lastWaseCode = (totrec + 1);


                    if (lastWaseCode < 10)
                    {
                        hdnWaseCode.Text = Converter.GetString("0" + lastWaseCode);
                    }

                    A2ZWAREHOUSEDTO objDTO = new A2ZWAREHOUSEDTO();

                    objDTO.WaseCode = Converter.GetInteger(hdnWaseCode.Text);

                    objDTO.WaseDescription = Converter.GetString(txtDescription.Text);

                    int roweffect = A2ZWAREHOUSEDTO.InsertInformation(objDTO);
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
            if (ddlWase.SelectedValue != "-Select-")
            {

                A2ZWAREHOUSEDTO UpDTO = new A2ZWAREHOUSEDTO();
                UpDTO.WaseCode = Converter.GetInteger(hdnWaseCode.Text);
           
                UpDTO.WaseDescription = Converter.GetString(txtDescription.Text);
                int roweffect = A2ZWAREHOUSEDTO.UpdateInformation(UpDTO);
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
            string sqlquery3 = "SELECT WaseCode,WaseDescription FROM A2ZWAREHOUSE WHERE WaseCode!=0 ";
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
