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
    public partial class SYSUpzilaCodeMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DivisionDropdown();
                txtcode.Focus();              
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
            string sqquery = @"SELECT DistCode,DistDescription FROM A2ZDISTRICT WHERE DiviCode='" + ddlDivision.SelectedValue + "' or DiviCode = '0'";

            ddlDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlDistrict, "A2ZHKMCUS");


        }
        protected void UpzilaDropdown()
        {
            string sqquery = @"SELECT UpzilaCode,UpzilaDescription FROM A2ZUPZILA WHERE DiviCode='" + ddlDivision.SelectedValue + "' and DistCode='" + ddlDistrict.SelectedValue + "' or DistCode = '0'";

            ddlUpzila = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlUpzila, "A2ZHKMCUS");


        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtcode.Text != string.Empty)
                {

                    int code = Converter.GetInteger(txtcode.Text);
                    A2ZDIVISIONDTO getDTO = (A2ZDIVISIONDTO.GetInformation(code));
                    if (getDTO.DivisionCode > 0)
                    {
                        ddlDivision.SelectedValue = Converter.GetString(getDTO.DivisionCode);
                        hdnDiviCode.Text = Converter.GetString(getDTO.DivisionCode);
                        hdnDiviOrgCode.Text = Converter.GetString(getDTO.DivisionOrgCode);
                        txtDistcode.Text = string.Empty;
                        txtDistcode.Focus();
                        clearInfo();
                        DistrictDropdown();
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
                        txtcode.Text = string.Empty;
                        txtcode.Focus();
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
            if (ddlDivision.SelectedValue == "-Select-")
            {
                clearInfo();
                txtcode.Text = string.Empty;
                txtDistcode.Focus();
                ddlDistrict.SelectedIndex = 0;
                return;
            }


            DistrictDropdown();

            try
            {


                if (ddlDivision.SelectedValue != "-Select-")
                {

                    int code = Converter.GetInteger(ddlDivision.SelectedValue);
                    A2ZDIVISIONDTO getDTO = (A2ZDIVISIONDTO.GetInformation(code));
                    if (getDTO.DivisionCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.DivisionCode);
                        hdnDiviCode.Text = Converter.GetString(getDTO.DivisionCode);
                        hdnDiviOrgCode.Text = Converter.GetString(getDTO.DivisionOrgCode);
                        txtDistcode.Text = string.Empty;
                        txtDistcode.Focus();
                        clearInfo();
                    }
                    else
                    {
                        //       ddlDivision.SelectedValue = "-Select-";
                        ddlDistrict.SelectedIndex = 0;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtDistcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (txtDistcode.Text != string.Empty && ddlDivision.SelectedValue != "-Select-")
                {
                    A2ZDISTRICTDTO getDTO = new A2ZDISTRICTDTO();

                    int code = Converter.GetInteger(hdnDiviCode.Text);
                    int DistCode = Converter.GetInteger(txtDistcode.Text);
                    getDTO = (A2ZDISTRICTDTO.GetInformation(code, DistCode));

                    if (getDTO.DivisionCode > 0 && getDTO.DistrictCode > 0)
                    {
                        ddlDistrict.SelectedValue = Converter.GetString(getDTO.DistrictCode);
                        hdnDistCode.Text = Converter.GetString(getDTO.DistrictCode);
                        hdnDistOrgCode.Text = Converter.GetString(getDTO.DistrictOrgCode);
                        
                        UpzilaDropdown();
                        txtUpzilaDescription.Focus();
                        
                    }
                    else
                    {
                        //String csname1 = "PopupScript";
                        //Type cstype = GetType();
                        //ClientScriptManager cs = Page.ClientScript;

                        //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                        //{
                        //    String cstext1 = "alert('Invalid District Code');";
                        //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                            
                        //}
                        txtDistcode.Text = string.Empty;
                        txtDistcode.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid District Code');", true);
                        return;
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
            if (ddlDistrict.SelectedItem.Text == "-Select-")
            {
                clearInfo();
                txtDistcode.Focus();
                txtDistcode.Text = string.Empty;
                return;
            }
            UpzilaDropdown();
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
                        txtDistcode.Text = Converter.GetString(getDTO.DistrictCode);
                        hdnDistCode.Text = Converter.GetString(getDTO.DistrictCode);
                        hdnDistOrgCode.Text = Converter.GetString(getDTO.DistrictOrgCode);
                        txtUpzilaDescription.Focus();
                        
                    }
                    else
                    {
                        //     ddlThana.SelectedValue = "-Select-";
                       
                        txtUpzilaDescription.Text = string.Empty;
                    }
                    //BtnSubmit.Visible = false;
                    //BtnUpdate.Visible = true;


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        protected void ddlUpzila_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try
            {

                if (ddlDistrict.SelectedValue != "-Select-" && ddlDivision.SelectedValue != "-Select-" && ddlUpzila.SelectedValue != "-Select-")
                {
                    A2ZUPZILADTO getDTO = new A2ZUPZILADTO();
                    int code = Converter.GetInteger(ddlDivision.SelectedValue);
                    int distcode = Converter.GetInteger(ddlDistrict.SelectedValue);
                    int Upzilacode = Converter.GetInteger(ddlUpzila.SelectedValue);
                    getDTO = (A2ZUPZILADTO.GetInformation(code, distcode, Upzilacode));

                    if (getDTO.DivisionCode > 0 && getDTO.DistrictCode > 0 && getDTO.UpzilaCode > 0)
                    {
                        hdnUpzilaCode.Text = Converter.GetString(getDTO.UpzilaCode);
                        hdnUpzilaOrgCode.Text = Converter.GetString(getDTO.UpzilaOrgCode);
                        txtUpzilaDescription.Text = Converter.GetString(getDTO.UpzilaDescription);
                        txtUpzilaDescription.Focus();
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
            if (ddlDistrict.SelectedValue != "-Select-" && ddlDivision.SelectedValue != "-Select-" && ddlUpzila.SelectedValue == "-Select-" && txtUpzilaDescription.Text != string.Empty)
            {

                gvDetail();
                gvDetailInfo.Visible = false;

                int totrec = gvDetailInfo.Rows.Count;

                int lastUpzilaCode = (totrec + 1);

                int lastDistCode = Converter.GetInteger(hdnDistCode.Text);

                hdnUpzilaCode.Text = Converter.GetString(lastUpzilaCode);

                if (lastUpzilaCode < 10)
                {
                    hdnUpzilaCode.Text = Converter.GetString("0" + lastUpzilaCode);
                }

                if (lastDistCode < 10)
                {
                    hdnDistCode.Text = Converter.GetString("0" + lastDistCode);
                }

                string result = hdnDiviCode.Text + hdnDistCode.Text + hdnUpzilaCode.Text + "00";
                hdnUpzilaOrgCode.Text = Converter.GetString(result);

                A2ZUPZILADTO objDTO = new A2ZUPZILADTO();
                objDTO.DivisionCode = Converter.GetInteger(hdnDiviCode.Text);
                objDTO.DistrictCode = Converter.GetInteger(hdnDistCode.Text);
                objDTO.UpzilaCode = Converter.GetInteger(hdnUpzilaCode.Text);
                objDTO.DivisionOrgCode = Converter.GetInteger(hdnDiviOrgCode.Text);
                objDTO.DistrictOrgCode = Converter.GetInteger(hdnDistOrgCode.Text);
                objDTO.UpzilaOrgCode = Converter.GetInteger(hdnUpzilaOrgCode.Text);
                objDTO.UpzilaDescription = Converter.GetString(txtUpzilaDescription.Text);

                int roweffect = A2ZUPZILADTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {

                    clearInfo();

                    UpzilaDropdown();
                    gvDetail();
                    gvDetailInfo.Visible = false;
                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;
                    txtUpzilaDescription.Focus();

                }
            }
        }

        private void clearInfo()
        {
            txtUpzilaDescription.Text = string.Empty;
            
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (ddlDistrict.SelectedValue != "-Select-" && ddlDivision.SelectedValue != "-Select-" && ddlUpzila.SelectedValue != "-Select-")
            {
                A2ZUPZILADTO UpDTO = new A2ZUPZILADTO();
                UpDTO.DivisionCode = Converter.GetInteger(hdnDiviCode.Text);
                UpDTO.DistrictCode = Converter.GetInteger(hdnDistCode.Text);
                UpDTO.UpzilaCode = Converter.GetInteger(hdnUpzilaCode.Text);
                UpDTO.DivisionOrgCode = Converter.GetInteger(hdnDiviOrgCode.Text);
                UpDTO.DistrictOrgCode = Converter.GetInteger(hdnDistOrgCode.Text);
                UpDTO.UpzilaOrgCode = Converter.GetInteger(hdnUpzilaOrgCode.Text);
                UpDTO.UpzilaDescription = Converter.GetString(txtUpzilaDescription.Text);


                int roweffect = A2ZUPZILADTO.UpdateInformation(UpDTO);
                if (roweffect > 0)
                {
                    UpzilaDropdown();

                    clearInfo();
                    gvDetail();
                    gvDetailInfo.Visible = false;

                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;
                    txtUpzilaDescription.Focus();
                }
            }

        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT DiviCode,DistCode,UpzilaCode,UpzilaDescription FROM A2ZUPZILA WHERE DiviCode='" + ddlDivision.SelectedValue + "' and DistCode='" + ddlDistrict.SelectedValue + "' AND DiviCode!=0 ";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZHKMCUS");
        }
        protected void BtnView_Click(object sender, EventArgs e)
        {
            if (ddlDivision.SelectedValue != "-Select-" && ddlDistrict.SelectedValue != "-Select-")
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