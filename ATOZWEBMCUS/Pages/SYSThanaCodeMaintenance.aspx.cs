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
    public partial class SYSThanaCodeMaintenance : System.Web.UI.Page
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


        protected void ThanaDropdown()
        {
            string sqquery = @"SELECT ThanaCode,ThanaDescription FROM A2ZTHANA WHERE DiviCode='" + ddlDivision.SelectedValue + "' and DistCode='" + ddlDistrict.SelectedValue + "' and UpzilaCode='" + ddlUpzila.SelectedValue + "' or UpzilaCode = '0'";

            ddlThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlThana, "A2ZHKMCUS");


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
                if (ddlDistrict.SelectedItem.Text == "-Select-")
                {
                    txtUpzilacode.Focus();
                 
                }
               
                if (txtDistcode.Text != string.Empty && ddlDivision.SelectedValue != "-Select-")
                {
                    A2ZDISTRICTDTO getDTO = new A2ZDISTRICTDTO();

                    int code = Converter.GetInteger(txtcode.Text);
                    int DistCode = Converter.GetInteger(txtDistcode.Text);
                    getDTO = (A2ZDISTRICTDTO.GetInformation(code, DistCode));

                    if (getDTO.DivisionCode > 0 && getDTO.DistrictCode > 0)
                    {
                    
                        
                        ddlDistrict.SelectedValue = Converter.GetString(getDTO.DistrictCode);
                        hdnDistCode.Text = Converter.GetString(getDTO.DistrictCode);
                        hdnDistOrgCode.Text = Converter.GetString(getDTO.DistrictOrgCode);
                        txtUpzilacode.Focus();
                        UpzilaDropdown();
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        
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
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
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
                        //txtDistDescription.Text = Converter.GetString(getDTO.DistrictDescription);
                        //txtDistDescription.Focus();
                    }
                    else
                    {
                   //     ddlThana.SelectedValue = "-Select-";
                        
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

        protected void txtUpzilacode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlUpzila.SelectedItem.Text == "-Select-")
                {
                    txtUpzilacode.Focus();

                }

                if (txtUpzilacode.Text != string.Empty && ddlDivision.SelectedValue != "-Select-")
                {
                    A2ZUPZILADTO getDTO = new A2ZUPZILADTO();

                    int code = Converter.GetInteger(txtcode.Text);
                    int DistCode = Converter.GetInteger(txtDistcode.Text);
                    int UpzilaCode = Converter.GetInteger(txtUpzilacode.Text);
                    getDTO = (A2ZUPZILADTO.GetInformation(code, DistCode,UpzilaCode));

                    if (getDTO.DivisionCode > 0 && getDTO.DistrictCode > 0 && getDTO.UpzilaCode > 0)
                    {
                        ddlUpzila.SelectedValue = Converter.GetString(getDTO.UpzilaCode);
                        hdnUpzilaCode.Text = Converter.GetString(getDTO.UpzilaCode);
                        hdnUpzilaOrgCode.Text = Converter.GetString(getDTO.UpzilaOrgCode);
                        
                        ThanaDropdown();
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                        txtThanaDescription.Focus();

                    }
                    else
                    {
                        //String csname1 = "PopupScript";
                        //Type cstype = GetType();
                        //ClientScriptManager cs = Page.ClientScript;

                        //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                        //{
                        //    String cstext1 = "alert('Invalid Upzila Code');";
                        //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                            
                        //}

                        txtUpzilacode.Text = string.Empty;
                        txtUpzilacode.Focus();
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

        protected void ddlUpzila_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUpzila.SelectedItem.Text == "-Select-")
            {
                clearInfo();
                txtUpzilacode.Focus();
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                txtUpzilacode.Text = string.Empty;
                return;
            }
            ThanaDropdown();
            try
            {

                if (ddlDistrict.SelectedValue != "-Select-" && ddlDivision.SelectedValue != "-Select-" && ddlUpzila.SelectedValue != "-Select-")
                {
                    A2ZUPZILADTO getDTO = new A2ZUPZILADTO();
                    int code = Converter.GetInteger(ddlDivision.SelectedValue);
                    int distcode = Converter.GetInteger(ddlDistrict.SelectedValue);
                    int Upzila = Converter.GetInteger(ddlUpzila.SelectedValue);
                    getDTO = (A2ZUPZILADTO.GetInformation(code,distcode,Upzila));

                    if (getDTO.DivisionCode > 0 && getDTO.DistrictCode > 0 && getDTO.UpzilaCode > 0)
                    {
                        txtUpzilacode.Text = Converter.GetString(getDTO.UpzilaCode);
                        hdnUpzilaCode.Text = Converter.GetString(getDTO.UpzilaCode);
                        hdnUpzilaOrgCode.Text = Converter.GetString(getDTO.UpzilaOrgCode);
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                        txtThanaDescription.Focus();
                        //txtDistDescription.Text = Converter.GetString(getDTO.DistrictDescription);
                        //txtDistDescription.Focus();
                    }
                    else
                    {
                        //     ddlThana.SelectedValue = "-Select-";
                        
                        //txtThanaDescription.Text = string.Empty;
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

        
        protected void ddlThana_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try
            {

                if (ddlDistrict.SelectedValue != "-Select-" && ddlDivision.SelectedValue != "-Select-" && ddlUpzila.SelectedValue != "-Select-" && ddlThana.SelectedValue != "-Select-")
                {
                    A2ZTHANADTO getDTO = new A2ZTHANADTO();
                    int code = Converter.GetInteger(ddlDivision.SelectedValue);
                    int distcode = Converter.GetInteger(ddlDistrict.SelectedValue);
                    int Upzilacode = Converter.GetInteger(ddlUpzila.SelectedValue);
                    int thanacode = Converter.GetInteger(ddlThana.SelectedValue);
                    getDTO = (A2ZTHANADTO.GetInformation(code, distcode, Upzilacode,thanacode));

                    if (getDTO.DivisionCode > 0 && getDTO.DistrictCode > 0 && getDTO.ThanaCode>0)
                    {
                        hdnThanaCode.Text = Converter.GetString(getDTO.ThanaCode);
                        hdnThanaOrgCode.Text = Converter.GetString(getDTO.ThanaOrgCode);
                        txtThanaDescription.Text = Converter.GetString(getDTO.ThanaDescription);
                        txtThanaDescription.Focus();
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
            if (ddlDistrict.SelectedValue != "-Select-" && ddlDivision.SelectedValue != "-Select-" && ddlUpzila.SelectedValue != "-Select-" && ddlThana.SelectedValue == "-Select-" && txtThanaDescription.Text != string.Empty)
            {

                gvDetail();
                gvDetailInfo.Visible = false;


                int totrec = gvDetailInfo.Rows.Count;

                int lastThanaCode = (totrec + 1);

                int lastDistCode = Converter.GetInteger(hdnDistCode.Text);
                int lastUpzilaCode = Converter.GetInteger(hdnUpzilaCode.Text);

                hdnThanaCode.Text = Converter.GetString(lastThanaCode);
                hdnDistCode.Text = Converter.GetString(lastDistCode);
                hdnUpzilaCode.Text = Converter.GetString(lastUpzilaCode);

                if (lastUpzilaCode < 10)
                {
                    hdnUpzilaCode.Text = Converter.GetString("0" + lastUpzilaCode);
                }

                if (lastDistCode < 10)
                {
                    hdnDistCode.Text = Converter.GetString("0" + lastDistCode);
                }

                if (lastThanaCode < 10)
                {
                    hdnThanaCode.Text = Converter.GetString("0" + lastThanaCode);
                }
                string result = hdnDiviCode.Text + hdnDistCode.Text + hdnUpzilaCode.Text + hdnThanaCode.Text;
                hdnThanaOrgCode.Text = Converter.GetString(result);

                A2ZTHANADTO objDTO = new A2ZTHANADTO();
                objDTO.DivisionCode = Converter.GetInteger(hdnDiviCode.Text);
                objDTO.DistrictCode = Converter.GetInteger(hdnDistCode.Text);
                objDTO.UpzilaCode = Converter.GetInteger(hdnUpzilaCode.Text);
                objDTO.ThanaCode = Converter.GetInteger(hdnThanaCode.Text);
                objDTO.DivisionOrgCode = Converter.GetInteger(hdnDiviOrgCode.Text);
                objDTO.DistrictOrgCode = Converter.GetInteger(hdnDistOrgCode.Text);
                objDTO.UpzilaOrgCode = Converter.GetInteger(hdnUpzilaOrgCode.Text);
                objDTO.ThanaOrgCode = Converter.GetInteger(hdnThanaOrgCode.Text);

                objDTO.ThanaDescription = Converter.GetString(txtThanaDescription.Text);

                int roweffect = A2ZTHANADTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {

                    clearInfo();
                    ThanaDropdown();
                    gvDetail();
                    gvDetailInfo.Visible = false;
                    txtThanaDescription.Focus();
                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;

                }
            }
        }

        private void clearInfo()
        {
            txtThanaDescription.Text = string.Empty;
            
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (ddlDistrict.SelectedValue != "-Select-" && ddlDivision.SelectedValue != "-Select-" && ddlUpzila.SelectedValue != "-Select-" && ddlThana.SelectedValue != "-Select-")
            {

                A2ZTHANADTO UpDTO = new A2ZTHANADTO();

                UpDTO.DivisionCode = Converter.GetInteger(hdnDiviCode.Text);
                UpDTO.DistrictCode = Converter.GetInteger(hdnDistCode.Text);
                UpDTO.UpzilaCode = Converter.GetInteger(hdnUpzilaCode.Text);
                UpDTO.ThanaCode = Converter.GetInteger(hdnThanaCode.Text);
                UpDTO.DivisionOrgCode = Converter.GetInteger(hdnDiviOrgCode.Text);
                UpDTO.DistrictOrgCode = Converter.GetInteger(hdnDistOrgCode.Text);
                UpDTO.UpzilaOrgCode = Converter.GetInteger(hdnUpzilaOrgCode.Text);
                UpDTO.ThanaOrgCode = Converter.GetInteger(hdnThanaOrgCode.Text);

                UpDTO.ThanaDescription = Converter.GetString(txtThanaDescription.Text);


                int roweffect = A2ZTHANADTO.UpdateInformation(UpDTO);
                if (roweffect > 0)
                {
                    ThanaDropdown();

                    clearInfo();
                    gvDetail();
                    gvDetailInfo.Visible = false;
                    txtThanaDescription.Focus();
                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;

                }
            }

        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT DiviCode,DistCode,UpzilaCode,ThanaCode,ThanaDescription FROM A2ZTHANA WHERE DiviCode='" + ddlDivision.SelectedValue + "' and DistCode='" + ddlDistrict.SelectedValue + "' and UpzilaCode='" + ddlUpzila.SelectedValue + "' AND DiviCode!=0 ";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZHKMCUS");
        }
        protected void BtnView_Click(object sender, EventArgs e)
        {
            if (ddlDivision.SelectedValue != "-Select-" && ddlDistrict.SelectedValue != "-Select-" && ddlUpzila.SelectedValue != "-Select-")
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
