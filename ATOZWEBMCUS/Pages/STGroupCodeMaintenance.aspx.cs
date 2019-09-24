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
    public partial class STGroupCodeMaintenance : System.Web.UI.Page
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

            string sqlquery = "SELECT GrpCode,GrpDescription from A2ZGROUP";
            ddlGroup = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGroup, "A2ZSTMCUS");
        }
        public void clearInfo()
        {
            txtDescription.Text = string.Empty;

        }
        
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedValue == "-Select-")
            {
                clearInfo();
                ddlGroup.Focus();
               
            }

            try
            {


                if (ddlGroup.SelectedValue != "-Select-")
                {

                    int code = Converter.GetInteger(ddlGroup.SelectedValue);
                    A2ZGROUPDTO getDTO = (A2ZGROUPDTO.GetInformation(code));
                    if (getDTO.GrpCode > 0)
                    {
                        hdnGrpCode.Text = Converter.GetString(getDTO.GrpCode);

                        txtDescription.Text = Converter.GetString(getDTO.GrpDescription);
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
                if (txtDescription.Text != string.Empty)
                {
                    gvDetail();
                    gvDetailInfo.Visible = false;
                    
                    int totrec = gvDetailInfo.Rows.Count;

                    int lastGrpCode = (totrec + 1);


                    if (lastGrpCode < 10)
                    {
                        hdnGrpCode.Text = Converter.GetString("0" + lastGrpCode);
                    }

                    A2ZGROUPDTO objDTO = new A2ZGROUPDTO();

                    objDTO.GrpCode = Converter.GetInteger(hdnGrpCode.Text);

                    objDTO.GrpDescription = Converter.GetString(txtDescription.Text);

                    int roweffect = A2ZGROUPDTO.InsertInformation(objDTO);
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
            if (ddlGroup.SelectedValue != "-Select-")
            {

                A2ZGROUPDTO UpDTO = new A2ZGROUPDTO();
                UpDTO.GrpCode = Converter.GetInteger(hdnGrpCode.Text);
           
                UpDTO.GrpDescription = Converter.GetString(txtDescription.Text);
                int roweffect = A2ZGROUPDTO.UpdateInformation(UpDTO);
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
            string sqlquery3 = "SELECT GrpCode,GrpDescription FROM A2ZGROUP WHERE GrpCode!=0 ";
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
