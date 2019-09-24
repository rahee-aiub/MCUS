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
    public partial class STSubGroupCodeMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                GroupDropdown();
                txtGrpCode.Text = string.Empty;
                txtGrpCode.Focus();                          
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;                               
            }
        }
        private void GroupDropdown()
        {

            string sqlquery = "SELECT GrpCode,GrpDescription from A2ZGROUP";
            ddlGroup = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGroup, "A2ZSTMCUS");

        }
        protected void SubGroupDropdown()
        {
            string sqquery = @"SELECT SubGrpCode,SubGrpDescription FROM A2ZSUBGROUP WHERE GrpCode='" + hdnGrpCode.Text + "' OR GrpCode='0'";
            ddlSubGroup = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlSubGroup, "A2ZSTMCUS");
        }
        public void clearInfo()
        {          
            txtSubGrpDescription.Text = string.Empty;                  
        }

        protected void txtGrpCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtGrpCode.Text != string.Empty)
                {
                    int code = Converter.GetInteger(txtGrpCode.Text);
                    A2ZGROUPDTO getDTO = (A2ZGROUPDTO.GetInformation(code));
                    if (getDTO.GrpCode > 0)
                    {
                        ddlGroup.SelectedValue = Converter.GetString(getDTO.GrpCode);
                        hdnGrpCode.Text = Converter.GetString(getDTO.GrpCode);
                       
                        clearInfo();
                        SubGroupDropdown();
                      
                        txtSubGrpDescription.Focus();
                    }
                    else
                    {                        
                        txtGrpCode.Text = string.Empty;
                        txtGrpCode.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Group Code');", true);
                        return;                    
                      
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {            
            try
            {
                if (ddlGroup.SelectedIndex != 0)
                {
                   
                    int code = Converter.GetInteger(ddlGroup.SelectedValue);
                    A2ZGROUPDTO getDTO = (A2ZGROUPDTO.GetInformation(code));
                    if (getDTO.GrpCode > 0)
                    {
                        txtGrpCode.Text = Converter.GetString(getDTO.GrpCode);
                        hdnGrpCode.Text = Converter.GetString(getDTO.GrpCode);
                        SubGroupDropdown();
                       
                       
                        txtSubGrpDescription.Focus();
                        clearInfo();
                    }

                            
                  
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlSubGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try
            {

                if (ddlSubGroup.SelectedValue != "-Select-" && ddlGroup.SelectedValue != "-Select-")
                {
                    A2ZSUBGROUPDTO getDTO = new A2ZSUBGROUPDTO();
                    int code = Converter.GetInteger(ddlGroup.SelectedValue);
                    int distcode = Converter.GetInteger(ddlSubGroup.SelectedValue);
                    getDTO = (A2ZSUBGROUPDTO.GetInformation(code, distcode));

                    if (getDTO.GrpCode > 0 && getDTO.SubGrpCode > 0)
                    {
                        hdnSubGrpCode.Text = Converter.GetString(getDTO.SubGrpCode);
                      
                        txtSubGrpDescription.Text = Converter.GetString(getDTO.SubGrpDescription);
                        txtSubGrpDescription.Focus();
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                    }                   

                }
                else
                {
                    BtnUpdate.Visible = false;
                    BtnSubmit.Visible = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (txtSubGrpDescription.Text != string.Empty)
            {
                gvDetail();
                gvDetailInfo.Visible = false;

                int totrec = gvDetailInfo.Rows.Count;

                int lastSubGrpCode = (totrec + 1);
                hdnSubGrpCode.Text = Converter.GetString(lastSubGrpCode);

                if (lastSubGrpCode < 10)
                {
                    hdnSubGrpCode.Text = Converter.GetString("0" + lastSubGrpCode);
                }


                A2ZSUBGROUPDTO objDTO = new A2ZSUBGROUPDTO();
                objDTO.GrpCode = Converter.GetInteger(hdnGrpCode.Text);
                objDTO.SubGrpCode = Converter.GetInteger(hdnSubGrpCode.Text);
                
                objDTO.SubGrpDescription = Converter.GetString(txtSubGrpDescription.Text);

                int roweffect = A2ZSUBGROUPDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    clearInfo();
                    SubGroupDropdown();
                    gvDetail();
                    gvDetailInfo.Visible = false;
                    BtnUpdate.Visible = false;
                    BtnSubmit.Visible = true;
                    txtSubGrpDescription.Focus();
                }
            }
           
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

            if (ddlSubGroup.SelectedValue != "-Select-" && ddlGroup.SelectedValue != "-Select-")
            {
                A2ZSUBGROUPDTO UpDTO = new A2ZSUBGROUPDTO();
                UpDTO.GrpCode = Converter.GetInteger(hdnGrpCode.Text);
                UpDTO.SubGrpCode = Converter.GetInteger(hdnSubGrpCode.Text);
                
                UpDTO.SubGrpDescription = Converter.GetString(txtSubGrpDescription.Text);


                int roweffect = A2ZSUBGROUPDTO.UpdateInformation(UpDTO);
                if (roweffect > 0)
                {
                    clearInfo();
                    SubGroupDropdown();
                    gvDetail();
                    gvDetailInfo.Visible = false;
                    BtnUpdate.Visible = false;
                    BtnSubmit.Visible = true;
                    txtSubGrpDescription.Focus();
                }
            }

        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT GrpCode,SubGrpCode,SubGrpDescription FROM A2ZSUBGROUP WHERE GrpCode='" + ddlGroup.SelectedValue + "'";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZSTMCUS");
        }
        protected void BtnView_Click(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedValue != "-Select-")
            {
                gvDetailInfo.Visible = true;
                gvDetail();
            }
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
