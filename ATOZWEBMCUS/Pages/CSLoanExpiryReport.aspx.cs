using System;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSLoanExpiryReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Accdropdown();
                CreditUnionDropdown();
                DistrictDropdown();

                string PFlag = (string)Session["ProgFlag"];
                CtrlProgFlag.Text = PFlag;

                if (CtrlProgFlag.Text != "1")
                {

                    txtAccType.Focus();
                   
                    txtCrUnion.Enabled = false;
                    ddlCrUnion.Enabled = false;
                    ChkAllCrUnion.Checked = true;

                    ChkAllDistrict.Checked = true;
                    ddlDistrict.Enabled = false;


                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");

                    CtrlTrnDate.Text = date;
                    txtTranDate.Text = date;

                   
                }
                else
                {
                    string RtxtTranDate = (string)Session["StxtTranDate"];

                    string RtxtAccType = (string)Session["StxtAccType"];
                    string RddlAcType = (string)Session["SddlAcType"];
                    string RChkAllCrUnion = (string)Session["SChkAllCrUnion"];
                    string RtxtCreditUNo = (string)Session["StxtCrUnion"];
                    string RddlCreditUNo = (string)Session["SddlCrUnion"];

                    string RlblCuType = (string)Session["SlblCuType"];
                    string RlblCuNo = (string)Session["SlblCuNo"];

                    string RChkAllDistrict = (string)Session["SChkAllDistrict"];
                    string RddlDistrict = (string)Session["SddlDistrict"];


                                        
                    txtAccType.Text = RtxtAccType; 
                    ddlAcType.SelectedValue = RddlAcType;

                    txtTranDate.Text = RtxtTranDate; 
                    
                    if (RChkAllCrUnion == "1")
                    {
                        ChkAllCrUnion.Checked = true;
                        txtCrUnion.Text = string.Empty;
                        ddlCrUnion.SelectedValue = "-Select-";
                        txtCrUnion.Enabled = false;
                        ddlCrUnion.Enabled = false;
                    }
                    else
                    {
                        ChkAllCrUnion.Checked = false;
                        txtCrUnion.Text = RtxtCreditUNo;
                        ddlCrUnion.SelectedValue = RddlCreditUNo;
                        lblCuType.Text = RlblCuType;
                        lblCuNo.Text = RlblCuNo;
                    }


                    if (RChkAllDistrict == "1")
                    {
                        ChkAllDistrict.Checked = true;
                        ddlDistrict.SelectedValue = "-Select-";  
                        ddlDistrict.Enabled = false;
                    }
                    else
                    {
                        ChkAllDistrict.Checked = false;
                        ddlDistrict.SelectedValue = RddlDistrict;
                    }


                }

            }

        }

        private void DistrictDropdown()
        {

            string sqquery = @"SELECT DistOrgCode,DistDescription FROM A2ZDISTRICT";
            ddlDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlDistrict, "A2ZHKMCUS");

        }
        private void Accdropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE WHERE AccTypeClass=5 OR AccTypeClass=6 OR AccTypeClass=8";
            ddlAcType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAcType, "A2ZCSMCUS");
        }

        private void CreditUnionDropdown()
        {

            string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION WHERE CuStatus !='9' ORDER BY CuName ASC";
            ddlCrUnion = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCrUnion, "A2ZCSMCUS");

        }

        protected void RemoveSession()
        {
            Session["ProgFlag"] = string.Empty;


            Session["StxtTranDate"] = string.Empty;

            Session["SChkAllCrUnion"] = string.Empty;
            Session["StxtAccType"] = string.Empty;
            Session["SddlAcType"] = string.Empty;
            Session["StxtCrUnion"] = string.Empty;
            Session["SddlCrUnion"] = string.Empty;

            Session["SlblCuType"] = string.Empty;
            Session["SlblCuNo"] = string.Empty;

            Session["SChkAllDistrict"] = string.Empty;
            Session["SddlDistrict"] = string.Empty;
        }

        protected void txtCrUnion_TextChanged(object sender, EventArgs e)
        {

            try
            {

                if (txtCrUnion.Text != string.Empty)
                {
                    string c = "";
                    int a = txtCrUnion.Text.Length;

                    string b = txtCrUnion.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);


                    Int16 CuType = Converter.GetSmallInteger(re);
                    int CNo = Converter.GetSmallInteger(re1);



                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

                    if (getDTO.CreditUnionNo > 0)
                    {
                        hdnCashCode.Text = Converter.GetString(getDTO.GLCashCode);


                        lblCuNo.Text = Converter.GetString(getDTO.CreditUnionNo);
                        lblCuType.Text = Converter.GetString(getDTO.CuType);
                        string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION where CuType='" + CuType + "' ORDER BY CuName ASC";
                        ddlCrUnion = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCrUnion, "A2ZCSMCUS");
                        ddlCrUnion.SelectedValue = Converter.GetString(txtCrUnion.Text);


                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtCrUnion_TextChanged Problem');</script>");
                //throw ex;
            }
        }



        protected void ddlCrUnion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                if (ddlCrUnion.SelectedValue != "-Select-")
                {
                    txtHidden.Text = Converter.GetString(ddlCrUnion.SelectedValue);

                    string c = "";
                    int a = txtHidden.Text.Length;

                    string b = txtHidden.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);


                    Int16 CuType = Converter.GetSmallInteger(re);
                    int CNo = Converter.GetSmallInteger(re1);


                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

                    if (getDTO.CreditUnionNo > 0)
                    {
                        lblCuNo.Text = Converter.GetString(getDTO.CreditUnionNo);
                        lblCuType.Text = Converter.GetString(getDTO.CuType);
                        txtCrUnion.Text = Converter.GetString(txtHidden.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlCrUnion_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }


        }


        protected void ChkAllCrUnion_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllCrUnion.Checked)
            {

                txtCrUnion.Enabled = false;
                ddlCrUnion.Enabled = false;

            }
            else
            {
                txtCrUnion.Enabled = true;
                ddlCrUnion.Enabled = true;
                txtCrUnion.Focus();
            }
        }


        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {
                Session["ProgFlag"] = "1";

                Session["StxtTranDate"] = txtTranDate.Text;

                Session["StxtAccType"] = txtAccType.Text;
                Session["SddlAcType"] = ddlAcType.SelectedValue;

                if (ChkAllCrUnion.Checked == true)
                {
                    Session["SChkAllCrUnion"] = "1";
                }
                else
                {
                    Session["SChkAllCrUnion"] = "0";
                }

                Session["StxtCrUnion"] = txtCrUnion.Text;
                Session["SddlCrUnion"] = ddlCrUnion.SelectedValue;

                Session["SlblCuType"] = lblCuType.Text;
                Session["SlblCuNo"] = lblCuNo.Text;


                if (ChkAllDistrict.Checked == true)
                {
                    Session["SChkAllDistrict"] = "1";
                }
                else
                {
                    Session["SChkAllDistrict"] = "0";
                }

                Session["SddlDistrict"] = ddlDistrict.SelectedValue;

              

                if (txtAccType.Text == string.Empty)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Type  Not Abailabe');", true);
                    return;
                }


                if (txtCrUnion.Text == string.Empty && ChkAllCrUnion.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Mark Credit Union Check Box / Input Credit Union No.');", true);
                    return;
                }



                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;

                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                DateTime fdate = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);



                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, fdate);


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlAcType.SelectedItem.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, ddlAcType.SelectedValue);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlAcType.SelectedItem.Text);

                if (ChkAllCrUnion.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, 0);
                }
                else
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, lblCuType.Text);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, lblCuNo.Text);
                }

                if (ChkAllDistrict.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, "");
                }
                else
                {
                    int code = Converter.GetInteger(ddlDistrict.SelectedValue);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, code);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, ddlDistrict.SelectedItem.Text);
                }

                
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptCSLoanExpiryDate");


                Response.Redirect("ReportServer.aspx", false);

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void ddlAcType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlAcType.SelectedValue == "-Select-")
            //{
            //    txtAccType.Focus();
            //    txtAccType.Text = string.Empty;
            //}
            try
            {


                if (ddlAcType.SelectedValue != "-Select-")
                {

                    Int16 MainCode = Converter.GetSmallInteger(ddlAcType.SelectedValue);
                    A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));
                    if (getDTO.AccTypeCode > 0)
                    {
                        txtAccType.Text = Converter.GetString(getDTO.AccTypeCode);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void txtAccType_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtAccType.Text != string.Empty)
                {
                    Int16 MainCode = Converter.GetSmallInteger(txtAccType.Text);
                    A2ZACCTYPEDTO getDTO = (A2ZACCTYPEDTO.GetInformation(MainCode));

                    if (getDTO.AccTypeCode > 0)
                    {
                        if (getDTO.AccTypeClass != 5 && getDTO.AccTypeClass != 6 && getDTO.AccTypeClass != 8)
                        {
                            txtAccType.Text = string.Empty;
                            txtAccType.Focus();
                            return;
                        }

                        txtAccType.Text = Converter.GetString(getDTO.AccTypeCode);
                        ddlAcType.SelectedValue = Converter.GetString(getDTO.AccTypeCode);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }

        private void InvalidDateMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Future Date');", true);
            return;

        }
        protected void txtTranDate_TextChanged(object sender, EventArgs e)
        {
            A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime opdate2 = Converter.GetDateTime(dto.ProcessDate);
            int Begyear = Converter.GetInteger(dto.FinancialBegYear);


            DateTime opdate1 = DateTime.ParseExact(txtTranDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            int Iyear = opdate1.Year;
            int Imonth = opdate1.Month;

           
            if (opdate1 > opdate2)
            {
                InvalidDateMSG();
                txtTranDate.Text = string.Empty;
                txtTranDate.Text = CtrlTrnDate.Text;
                txtTranDate.Focus();
            }

            
        }

        protected void ChkAllDistrict_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllDistrict.Checked)
            {
                ddlDistrict.Enabled = false;
                ddlDistrict.SelectedIndex = 0;
            }
            else
            {
                ddlDistrict.Enabled = true;
            }
        }

        

    }
}