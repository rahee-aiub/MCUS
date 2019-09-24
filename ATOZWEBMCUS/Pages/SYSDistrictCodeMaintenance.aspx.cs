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
    public partial class SYSDistrictCodeMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                DivisionDropdown();
                txtDivicode.Text = string.Empty;
                txtDivicode.Focus();           
                
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                               
            }
        }
        private void DivisionDropdown()
        {

            string sqlquery = "SELECT DiviCode,DiviDescription from A2ZDIVISION";
            ddlDivision = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlDivision, "A2ZHKMCUS");

        }
        protected void DistrictDropdown()
        {
            string sqquery = @"SELECT DistCode,DistDescription FROM A2ZDISTRICT WHERE DiviCode='" + ddlDivision.SelectedValue + "' or DiviCode='0' ";

            ddlDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlDistrict, "A2ZHKMCUS");


        }
        public void clearInfo()
        {
          
            txtDistDescription.Text = string.Empty;
                  
        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtDivicode.Text != string.Empty)
                {
                    int code = Converter.GetInteger(txtDivicode.Text);
                    A2ZDIVISIONDTO getDTO = (A2ZDIVISIONDTO.GetInformation(code));
                    if (getDTO.DivisionCode > 0)
                    {
                        ddlDivision.SelectedValue = Converter.GetString(getDTO.DivisionCode);
                        hdnDiviCode.Text = Converter.GetString(getDTO.DivisionCode);
                        hdnDiviOrgCode.Text = Converter.GetString(getDTO.DivisionOrgCode);
                        clearInfo();
                        DistrictDropdown();
                        ddlDistrict.SelectedValue = "-Select-";
                        txtDistDescription.Focus();
                    }
                    else
                    {
                        //String csname1 = "PopupScript";
                        //Type cstype = GetType();
                        //ClientScriptManager cs = Page.ClientScript;

                        //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                        //{
                        //    String cstext1 = "alert('Invalid Division Code');";
                        //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                            
                        //}
                        txtDivicode.Text = string.Empty;
                        txtDivicode.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Division Code');", true);
                        return;                    
                      
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            DistrictDropdown();

            try
            {


                if (ddlDivision.SelectedValue != "-Select-")
                {

                    int code = Converter.GetInteger(ddlDivision.SelectedValue);
                    A2ZDIVISIONDTO getDTO = (A2ZDIVISIONDTO.GetInformation(code));
                    if (getDTO.DivisionCode > 0)
                    {
                        txtDivicode.Text = Converter.GetString(getDTO.DivisionCode);
                        hdnDiviCode.Text = Converter.GetString(getDTO.DivisionCode);
                        hdnDiviOrgCode.Text = Converter.GetString(getDTO.DivisionOrgCode);
                        ddlDistrict.SelectedIndex = 0;
                        txtDistDescription.Focus();
                        clearInfo();
                    }
                    else
                    {
                        //        ddlDivision.SelectedValue = "-Select-";
                        ddlDistrict.SelectedIndex = 0;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try
            {

                if (ddlDistrict.SelectedValue != "-Select-" && ddlDivision.SelectedValue != "-Select-")
                {
                    A2ZDISTRICTDTO getDTO = new A2ZDISTRICTDTO();
                    int code = Converter.GetInteger(ddlDivision.SelectedValue);
                    int distcode = Converter.GetInteger(ddlDistrict.SelectedValue);
                    getDTO = (A2ZDISTRICTDTO.GetInformation(code, distcode));

                    if (getDTO.DivisionCode > 0 && getDTO.DistrictCode > 0)
                    {
                        hdnDistCode.Text = Converter.GetString(getDTO.DistrictCode);
                        hdnDistOrgCode.Text = Converter.GetString(getDTO.DistrictOrgCode);
                        txtDistDescription.Text = Converter.GetString(getDTO.DistrictDescription);
                        txtDistDescription.Focus();
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
            if (ddlDistrict.SelectedValue == "-Select-" && ddlDivision.SelectedValue != "-Select-" && txtDistDescription.Text != string.Empty)
            {
                gvDetail();
                gvDetailInfo.Visible = false;

                int totrec = gvDetailInfo.Rows.Count;

                int lastDistCode = (totrec + 1);
                hdnDistCode.Text = Converter.GetString(lastDistCode);

                if (lastDistCode < 10)
                {
                    hdnDistCode.Text = Converter.GetString("0" + lastDistCode);
                }


                string result = hdnDiviCode.Text + hdnDistCode.Text + "0000";
                hdnDistOrgCode.Text = Converter.GetString(result);


                A2ZDISTRICTDTO objDTO = new A2ZDISTRICTDTO();
                objDTO.DivisionCode = Converter.GetInteger(hdnDiviCode.Text);
                objDTO.DistrictCode = Converter.GetInteger(hdnDistCode.Text);
                objDTO.DivisionOrgCode = Converter.GetInteger(hdnDiviOrgCode.Text);
                objDTO.DistrictOrgCode = Converter.GetInteger(hdnDistOrgCode.Text);
                objDTO.DistrictDescription = Converter.GetString(txtDistDescription.Text);

                int roweffect = A2ZDISTRICTDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    clearInfo();
                    DistrictDropdown();
                    gvDetail();
                    gvDetailInfo.Visible = false;
                    BtnUpdate.Visible = false;
                    BtnSubmit.Visible = true;
                    txtDistDescription.Focus();
                }
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

            if (ddlDistrict.SelectedValue != "-Select-" && ddlDivision.SelectedValue != "-Select-")
            {
                A2ZDISTRICTDTO UpDTO = new A2ZDISTRICTDTO();
                UpDTO.DivisionCode = Converter.GetInteger(hdnDiviCode.Text);
                UpDTO.DistrictCode = Converter.GetInteger(hdnDistCode.Text);
                UpDTO.DivisionOrgCode = Converter.GetInteger(hdnDiviOrgCode.Text);
                UpDTO.DistrictOrgCode = Converter.GetInteger(hdnDistOrgCode.Text);
                UpDTO.DistrictDescription = Converter.GetString(txtDistDescription.Text);


                int roweffect = A2ZDISTRICTDTO.UpdateInformation(UpDTO);
                if (roweffect > 0)
                {
                    clearInfo();
                    DistrictDropdown();
                    gvDetail();
                    gvDetailInfo.Visible = false;
                    BtnUpdate.Visible = false;
                    BtnSubmit.Visible = true;
                    txtDistDescription.Focus();
                }
            }

        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT DiviCode,DistCode,DistDescription FROM A2ZDISTRICT WHERE DiviCode='" + ddlDivision.SelectedValue + "' AND DistCode!=0 ";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZHKMCUS");
        }
        protected void BtnView_Click(object sender, EventArgs e)
        {
            if (ddlDivision.SelectedValue != "-Select-")
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
