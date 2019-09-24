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
using DataAccessLayer.DTO.GeneralLedger;
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.Utility;
using System.Globalization;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;


namespace ATOZWEBMCUS.Pages
{
    public partial class CSModifyCreditUnionMaintenance : System.Web.UI.Page
    {

        protected Int32 userPermission;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //userPermission = Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION));

                //if (userPermission == 30)
                //{
                //    string notifyMsg = "?txtOne=" + Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME)) +
                //                       "&txtTwo=" + "You Don't Have Permission for Approved" +
                //                       "&txtThree=" + "Contact Your Super User" + "&PreviousMenu=A2ZERPModule.aspx";
                //    Server.Transfer("Notify.aspx" + notifyMsg);
                //}

                //else

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                ProcDate.Text = date;

                CreditUnionDropdown();
                GLCashCodeDropdown();
                txtCreditUNo.Focus();
                BtnCreUnionUpdate.Visible = false;

                DivisionDropdown();
                DistrictInFo();
                UpzilaInfo();
                ThanaInfo();

                ddlDivision.Enabled = false;
                ddlDistrict.Enabled = false;
                ddlThana.Enabled = false;
                //ddlCuMemberFlag.Enabled = false;
                ddlGLCashCode.Enabled = false;

            }

        }

        protected void GLCashCodeDropdown()
        {

            string sqlquery = "SELECT GLAccNo,GLAccDesc from A2ZCGLMST where GLRecType = 2 and GLSubHead = 10101000 ORDER BY GLAccDesc ASC";
            ddlGLCashCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlGLCashCode, "A2ZGLMCUS");

        }

        private void CreditUnionDropdown()
        {

            string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION WHERE CuStatus !='9' ORDER BY CuName ASC";
            ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");

        }
        private void DivisionDropdown()
        {

            string sqlquery = "SELECT DiviOrgCode,DiviDescription from A2ZDIVISION";
            ddlDivision = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlDivision, "A2ZHKMCUS");

        }
        private void DistrictDropdown()
        {

            string sqquery = @"SELECT DistOrgCode,DistDescription FROM A2ZDISTRICT WHERE DiviOrgCode='" + ddlDivision.SelectedValue + "' or DiviCode = '0'";
            ddlDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlDistrict, "A2ZHKMCUS");

        }
        private void UpzilaDropdown()
        {
            string sqquery = @"SELECT UpzilaOrgCode,UpzilaDescription FROM A2ZUPZILA WHERE DiviOrgCode='" + ddlDivision.SelectedValue + "' and DistOrgCode='" + ddlDistrict.SelectedValue + "' or DistCode = '0'";

            ddlUpzila = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlUpzila, "A2ZHKMCUS");

        }

        private void ThanaDropdown()
        {
            string sqquery = @"SELECT ThanaOrgCode,ThanaDescription FROM A2ZTHANA WHERE DiviOrgCode='" + ddlDivision.SelectedValue + "' and DistOrgCode='" + ddlDistrict.SelectedValue + "' and UpzilaOrgCode='" + ddlUpzila.SelectedValue + "' or UpzilaCode = '0'";

            ddlThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlThana, "A2ZHKMCUS");

        }
        public void clearInfo()
        {
            //txtCreditUNo.Text = string.Empty;
            txtCreUName.Text = string.Empty;
            txtCuOpenDate.Text = string.Empty;
            txtCuCertificateNo.Text = string.Empty;
            txtGLCashCode.Text = string.Empty;
            txtCuAddressL1.Text = string.Empty;
            txtCuAddressL2.Text = string.Empty;
            txtCuAddressL3.Text = string.Empty;
            txtCuEmail.Text = string.Empty;
            txtCuFax.Text = string.Empty;
            txtCuTelNo.Text = string.Empty;
            txtCuMobileNo.Text = string.Empty;

        }
        private void DistrictInFo()
        {

            string sqquery = @"SELECT DistOrgCode,DistDescription FROM A2ZDISTRICT";
            ddlDistrict = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlDistrict, "A2ZHKMCUS");

        }

        private void UpzilaInfo()
        {
            string sqquery = @"SELECT UpzilaOrgCode,UpzilaDescription FROM A2ZUPZILA";

            ddlUpzila = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlUpzila, "A2ZHKMCUS");

        }

        private void ThanaInfo()
        {
            string sqquery = @"SELECT ThanaOrgCode,ThanaDescription FROM A2ZTHANA";

            ddlThana = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqquery, ddlThana, "A2ZHKMCUS");

        }


        protected void BtnCreUnionUpdate_Click(object sender, EventArgs e)
        {
            
            Int16 CuType = Converter.GetSmallInteger(lblNewCuType.Text);
            int CNo = Converter.GetSmallInteger(lblNewCuNo.Text);

            A2ZCUNIONDTO UpDTO = new A2ZCUNIONDTO();
            A2ZMEMBERDTO MemDTO = new A2ZMEMBERDTO();

            MemDTO.CreditUnionNo = Converter.GetInteger(CNo);
            MemDTO.MemberName = Converter.GetString(txtCreUName.Text);
            MemDTO.MemType = Converter.GetSmallInteger(CuType);
            UpDTO.CuType = Converter.GetSmallInteger(CuType);
            UpDTO.CreditUnionNo = Converter.GetInteger(CNo);
            UpDTO.CreditUnionName = Converter.GetString(txtCreUName.Text);
            UpDTO.CuProcFlag = 13;
            UpDTO.CuTypeName = Converter.GetString("Affiliate");
            UpDTO.InputBy = Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_ID));
            if (txtCuOpenDate.Text != string.Empty)
            {
                DateTime opdate = DateTime.ParseExact(txtCuOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                UpDTO.opendate = opdate;
            //    MemDTO.OpenDate = opdate;
            }
            else
            {
                UpDTO.opendate = Converter.GetDateTime(System.DateTime.Now.ToLongDateString());
            }



            //UpDTO.MemberFlag = Converter.GetSmallInteger(ddlCuMemberFlag.SelectedValue);

            UpDTO.CertificateNo = Converter.GetInteger(txtCuCertificateNo.Text);
            UpDTO.AddressL1 = Converter.GetString(txtCuAddressL1.Text);
            UpDTO.AddressL2 = Converter.GetString(txtCuAddressL2.Text);
            UpDTO.AddressL3 = Converter.GetString(txtCuAddressL3.Text);
            UpDTO.TelephoneNo = Converter.GetString(txtCuTelNo.Text);
            UpDTO.MobileNo = Converter.GetString(txtCuMobileNo.Text);
            UpDTO.Fax = Converter.GetString(txtCuFax.Text);
            UpDTO.email = Converter.GetString(txtCuEmail.Text);
            UpDTO.Division = Converter.GetInteger(ddlDivision.SelectedValue);
            UpDTO.District = Converter.GetInteger(ddlDistrict.SelectedValue);
            UpDTO.Upzila = Converter.GetInteger(ddlUpzila.SelectedValue);
            UpDTO.Thana = Converter.GetInteger(ddlThana.SelectedValue);
            A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
            UpDTO.InputByDate = Converter.GetDateTime(dto.ProcessDate);
            UpDTO.VerifyByDate = Converter.GetDateTime(dto.ProcessDate);
            UpDTO.ApprovByDate = Converter.GetDateTime(dto.ProcessDate);
            UpDTO.CuStatusDate = Converter.GetDateTime(dto.ProcessDate);
            UpDTO.GLCashCode = Converter.GetInteger(txtGLCashCode.Text);
            //      int row = A2ZMEMBERDTO.Update(MemDTO);
            int roweffect = A2ZCUNIONDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {


                clearInfo();
                txtCreditUNo.Text = string.Empty;
                //ddlCuMemberFlag.SelectedIndex = 0;
                ddlGLCashCode.SelectedIndex = 0;

                ddlCreditUNo.SelectedValue = "-Select-";
                BtnCreUnionUpdate.Visible = false;

                ddlDivision.SelectedIndex = 0;
                ddlDistrict.SelectedIndex = 0;
                ddlUpzila.SelectedIndex = 0;
                ddlThana.SelectedIndex = 0;
                ddlDivision.Enabled = false;
                ddlDistrict.Enabled = false;
                ddlUpzila.Enabled = false;
                ddlThana.Enabled = false;
                //ddlCuMemberFlag.Enabled = false;
                ddlGLCashCode.Enabled = false;
                
                txtCreditUNo.Focus();

                DivisionDropdown();
                DistrictInFo();
                UpzilaInfo();
                ThanaInfo();

            }
        }

        public void ShowRecords()
        {
            Int16 CuType = Converter.GetSmallInteger(0);
            int CNo = Converter.GetInteger(txtCreditUNo.Text);
            A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

            if (getDTO.CreditUnionNo > 0)
            {

                txtCreditUNo.Text = Converter.GetString(getDTO.CreditUnionNo);
                txtCreUName.Text = Converter.GetString(getDTO.CreditUnionName);
                ddlCreditUNo.SelectedValue = Converter.GetString(getDTO.CreditUnionNo);
                DateTime dt = Converter.GetDateTime(getDTO.opendate);
                string date = dt.ToString("dd/MM/yyyy");
                txtCuOpenDate.Text = date;
                GetOpenDate.Text = date;
                //txtCuOpenDate.Text = Converter.GetString(getDTO.opendate);
                //ddlCuMemberFlag.SelectedValue = Converter.GetString(getDTO.MemberFlag);

                txtCuCertificateNo.Text = Converter.GetString(getDTO.CertificateNo);
                txtGLCashCode.Text = Converter.GetString(getDTO.GLCashCode);
                txtCuAddressL1.Text = Converter.GetString(getDTO.AddressL1);
                txtCuAddressL2.Text = Converter.GetString(getDTO.AddressL2);
                txtCuAddressL3.Text = Converter.GetString(getDTO.AddressL3);
                txtCuTelNo.Text = Converter.GetString(getDTO.TelephoneNo);
                txtCuMobileNo.Text = Converter.GetString(getDTO.MobileNo);
                txtCuFax.Text = Converter.GetString(getDTO.Fax);
                txtCuEmail.Text = Converter.GetString(getDTO.email);
                ddlDivision.SelectedValue = Converter.GetString(getDTO.Division);
                ddlDistrict.SelectedValue = Converter.GetString(getDTO.District);
                ddlThana.SelectedValue = Converter.GetString(getDTO.Thana);
                ddlUpzila.SelectedValue = Converter.GetString(getDTO.Upzila);

                BtnCreUnionUpdate.Visible = true;

                txtCreUName.Focus();
            }
            else
            {
                clearInfo();
                //txtCreUName.Text = string.Empty;
                ddlCreditUNo.SelectedValue = "-Select-";
                //ddlCuMemberFlag.SelectedValue = "0";


                BtnCreUnionUpdate.Visible = false;
                txtCreUName.Focus();

            }

        }

        protected void DisplayMessage()
        {
            string Msg = "";

            string a = "";
            string b = "";
            string c = "";
            string d = "";
            string e = "";
            string X = "";
           
            a = "Credit Union No. Already Transfered";
            e = "New Credit Union No.";
            b = string.Format("New Credit Union Type : {0}", lblNewCuTypeName.Text);
            c = string.Format(lblNewCuNo.Text);
            d = string.Format(lblNewCuType.Text);
            X = "-";
            
            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b;
            Msg += "\\n";
            Msg += e;
            Msg += d;
            Msg += X;
            Msg += c;


            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;
            
            
            //--------------------
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string a = "Credit Union No. Already Transfered";
            //string name = "New Credit Union No.";
            //string b = string.Format("New Credit Union Type : {0}", lblNewCuTypeName.Text);
            //string c = string.Format(lblNewCuNo.Text);
            //string d = string.Format(lblNewCuType.Text);
            //sb.Append("<script type = 'text/javascript'>");
            //sb.Append("window.onload=function(){");
            //sb.Append("alert('");
            //sb.Append(a);
            //sb.Append("\\n");
            //sb.Append("\\n");
            //sb.Append(b);
            //sb.Append("\\n");
            //sb.Append(name);
            //sb.Append(d);
            //sb.Append("-");
            //sb.Append(c);
            //sb.Append("')};");
            //sb.Append("</script>");
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }



        protected void txtCreditUNo_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtCreditUNo.Text != string.Empty)
                {
                    int CN = Converter.GetInteger(txtCreditUNo.Text);

                    hdnCuNumber.Text = Converter.GetString(CN);
                    
                    
                    
                    string c = "";
                    int a = txtCreditUNo.Text.Length;

                    string b = txtCreditUNo.Text;
                    c = b.Substring(0, 1);
                    int re = Converter.GetSmallInteger(c);
                    int dd = a - 1;
                    string d = b.Substring(1, dd);
                    int re1 = Converter.GetSmallInteger(d);


                    
                    Int16 CuType = Converter.GetSmallInteger(re);
                    int CNo = Converter.GetSmallInteger(re1);

                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

                    //A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInfo(CN));

                    if (getDTO.NoRecord > 0)
                    {
                        //CreditUnionDropdown();
                        lblNewCuTypeName.Text = Converter.GetString(getDTO.CreditUnionName);
                        lblNewCuNo.Text = Converter.GetString(getDTO.CreditUnionNo);
                        lblNewCuType.Text = Converter.GetString(getDTO.CuType);
                        if (getDTO.CuStatus == 9)
                        {
                            if (getDTO.CuReguCuType == 0)
                            {
                                lblNewCuTypeName.Text = Converter.GetString(getDTO.CuAssoCuTypeName);
                                lblNewCuNo.Text = Converter.GetString(getDTO.CuAssoCuNo);
                                lblNewCuType.Text = Converter.GetString(getDTO.CuAssoCuType);
                            }
                            else 
                            {
                                lblNewCuTypeName.Text = Converter.GetString(getDTO.CuReguCuTypeName);
                                lblNewCuNo.Text = Converter.GetString(getDTO.CuReguCuNo);
                                lblNewCuType.Text = Converter.GetString(getDTO.CuReguCuType);
                            }
                                      
                            DisplayMessage();
                            txtCreditUNo.Text = string.Empty;
                            txtCreditUNo.Focus();
                            return;
                        }

                        //txtCreditUNo.Text = Converter.GetString(getDTO.CreditUnionNo);
                        txtCreUName.Text = Converter.GetString(getDTO.CreditUnionName);
                        string sqlquery = "SELECT lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo,CuName from A2ZCUNION where CuType='" + lblNewCuType.Text + "' ORDER BY CuName ASC";
                        ddlCreditUNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlCreditUNo, "A2ZCSMCUS");
                        ddlCreditUNo.SelectedValue = Converter.GetString(lblNewCuType.Text + lblNewCuNo.Text);

                        txtCreditUNo.Text = (lblNewCuType.Text + "-" + lblNewCuNo.Text);
                        DateTime dt = Converter.GetDateTime(getDTO.opendate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtCuOpenDate.Text = date;
                        //txtCuOpenDate.Text = Converter.GetString(getDTO.opendate);
                        //ddlCuMemberFlag.SelectedValue = Converter.GetString(getDTO.MemberFlag);
                        txtCuCertificateNo.Text = Converter.GetString(getDTO.CertificateNo);
                        txtGLCashCode.Text = Converter.GetString(getDTO.GLCashCode);

                        if (txtGLCashCode.Text != string.Empty && txtGLCashCode.Text != "0")
                        {
                            ddlGLCashCode.SelectedValue = Converter.GetString(getDTO.GLCashCode);
                        }
                        txtCuAddressL1.Text = Converter.GetString(getDTO.AddressL1);
                        txtCuAddressL2.Text = Converter.GetString(getDTO.AddressL2);
                        txtCuAddressL3.Text = Converter.GetString(getDTO.AddressL3);
                        txtCuTelNo.Text = Converter.GetString(getDTO.TelephoneNo);
                        txtCuMobileNo.Text = Converter.GetString(getDTO.MobileNo);
                        txtCuFax.Text = Converter.GetString(getDTO.Fax);
                        txtCuEmail.Text = Converter.GetString(getDTO.email);

                        ddlDivision.Enabled = true;
                        ddlDistrict.Enabled = true;
                        ddlUpzila.Enabled = true;
                        ddlThana.Enabled = true;
                        //ddlCuMemberFlag.Enabled = true;
                        ddlGLCashCode.Enabled = true;

                        ddlDivision.SelectedValue = Converter.GetString(getDTO.Division);
                        ddlDistrict.SelectedValue = Converter.GetString(getDTO.District);
                        ddlThana.SelectedValue = Converter.GetString(getDTO.Thana);
                        ddlUpzila.SelectedValue = Converter.GetString(getDTO.Upzila);


                        BtnCreUnionUpdate.Visible = true;

                        txtCreUName.Focus();
                    }
                    else
                    {

                        InvalidRecords();
                        txtCreditUNo.Focus();

                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlCreditUNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCreditUNo.SelectedValue == "-Select-")
            {
                clearInfo();
                txtCreditUNo.Text = string.Empty;
                //ddlCuMemberFlag.SelectedValue = "0";


                BtnCreUnionUpdate.Visible = false;
                txtCreditUNo.Focus();
            }

            try
            {


                if (ddlCreditUNo.SelectedValue != "-Select-")
                {
                    txtHidden.Text = Converter.GetString(ddlCreditUNo.SelectedValue);

                    int CN = Converter.GetInteger(ddlCreditUNo.SelectedValue);

                    hdnCuNumber.Text = Converter.GetString(CN);
                    


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

                    //A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInfo(CN));

                    A2ZCUNIONDTO getDTO = (A2ZCUNIONDTO.GetInformation(CuType, CNo));

                    if (getDTO.NoRecord > 0)
                    {

                        txtCreditUNo.Text = Converter.GetString(txtHidden.Text);
                       
                        txtCreUName.Text = Converter.GetString(getDTO.CreditUnionName);
                        
                        lblNewCuNo.Text = Converter.GetString(getDTO.CreditUnionNo);
                        lblNewCuType.Text = Converter.GetString(getDTO.CuType);

                        txtCreditUNo.Text = (lblNewCuType.Text + "-" + lblNewCuNo.Text);

                        //ddlCreditUNo.SelectedValue = Converter.GetString(getDTO.CreditUnionNo);
                        DateTime dt = Converter.GetDateTime(getDTO.opendate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtCuOpenDate.Text = date;
                        //txtCuOpenDate.Text = Converter.GetString(getDTO.opendate);
                        //ddlCuMemberFlag.SelectedValue = Converter.GetString(getDTO.MemberFlag);
                        if (getDTO.CuStatus == 9)
                        {
                            DisplayMessage();
                            txtCreditUNo.Focus();
                            return;
                        }
                        txtCuCertificateNo.Text = Converter.GetString(getDTO.CertificateNo);
                        txtGLCashCode.Text = Converter.GetString(getDTO.GLCashCode);

                        if (txtGLCashCode.Text != string.Empty && txtGLCashCode.Text != "0")
                        {
                            ddlGLCashCode.SelectedValue = Converter.GetString(getDTO.GLCashCode);
                        }
                        
                        txtCuAddressL1.Text = Converter.GetString(getDTO.AddressL1);
                        txtCuAddressL2.Text = Converter.GetString(getDTO.AddressL2);
                        txtCuAddressL3.Text = Converter.GetString(getDTO.AddressL3);
                        txtCuTelNo.Text = Converter.GetString(getDTO.TelephoneNo);
                        txtCuMobileNo.Text = Converter.GetString(getDTO.MobileNo);
                        txtCuFax.Text = Converter.GetString(getDTO.Fax);
                        txtCuEmail.Text = Converter.GetString(getDTO.email);

                        ddlDivision.Enabled = true;
                        ddlDistrict.Enabled = true;
                        ddlUpzila.Enabled = true;
                        ddlThana.Enabled = true;
                        //ddlCuMemberFlag.Enabled = true;
                        ddlGLCashCode.Enabled = true;

                        ddlDivision.SelectedValue = Converter.GetString(getDTO.Division);
                        ddlDistrict.SelectedValue = Converter.GetString(getDTO.District);
                        ddlUpzila.SelectedValue = Converter.GetString(getDTO.Upzila);
                        ddlThana.SelectedValue = Converter.GetString(getDTO.Thana);

                        BtnCreUnionUpdate.Visible = true;

                        txtCreUName.Focus();
                    }
                    else
                    {
                        InvalidRecords();
                        txtCreditUNo.Focus();
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
            try
            {
                if (ddlDivision.SelectedValue != "-Select-")
                {

                    int code = Converter.GetInteger(ddlDivision.SelectedValue);
                    A2ZDIVISIONDTO getDTO = (A2ZDIVISIONDTO.GetInformation(code));
                    if (getDTO.DivisionCode > 0)
                    {
                        //txtcode.Text = Converter.GetString(getDTO.DivisionCode);
                        //txtDistcode.Focus();
                        //clearInfo();
                    }
                    ddlDistrict.SelectedIndex = 0;
                    ddlUpzila.SelectedIndex = 0;
                    ddlThana.SelectedIndex = 0;
                    DistrictDropdown();
                    UpzilaDropdown();
                    ThanaDropdown();
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlDivision_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string CheckQuery = "SELECT DiviOrgCode,DistOrgCode FROM A2ZDISTRICT Where DistOrgCode='" + ddlDistrict.SelectedValue + "'";
                DataTable dt = new DataTable();
                dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZHKMCUS");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        var DiviOrg = dr1["DiviOrgCode"].ToString();
                        var DistOrg = dr1["DistOrgCode"].ToString();


                        int a = Converter.GetInteger(DiviOrg);
                        int b = Converter.GetInteger(DistOrg);


                        ddlDivision.SelectedValue = Converter.GetString(a);
                        ddlDistrict.SelectedValue = Converter.GetString(b);
                    }
                }

                ddlUpzila.SelectedIndex = 0;
                ddlThana.SelectedIndex = 0;
                UpzilaDropdown();
                ThanaDropdown();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlDistrict_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void ddlUpzila_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string CheckQuery = "SELECT DiviOrgCode,DistOrgCode,UpzilaOrgCode FROM A2ZUPZILA Where UpzilaOrgCode='" + ddlUpzila.SelectedValue + "'";
                DataTable dt = new DataTable();
                dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZHKMCUS");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        var DiviOrg = dr1["DiviOrgCode"].ToString();
                        var DistOrg = dr1["DistOrgCode"].ToString();
                        var UpzilaOrg = dr1["UpzilaOrgCode"].ToString();

                        int a = Converter.GetInteger(DiviOrg);
                        int b = Converter.GetInteger(DistOrg);
                        int c = Converter.GetInteger(UpzilaOrg);

                        ddlDivision.SelectedValue = Converter.GetString(a);
                        ddlDistrict.SelectedValue = Converter.GetString(b);
                        ddlUpzila.SelectedValue = Converter.GetString(c);

                    }
                }

                ddlThana.SelectedIndex = 0;
                ThanaDropdown();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlDistrict_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }

        }
        protected void ddlThana_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string CheckQuery = "SELECT DiviOrgCode,DistOrgCode,UpzilaOrgCode,ThanaOrgCode FROM A2ZTHANA Where ThanaOrgCode='" + ddlThana.SelectedValue + "'";
                DataTable dt = new DataTable();
                dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZHKMCUS");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        var DiviOrg = dr1["DiviOrgCode"].ToString();
                        var DistOrg = dr1["DistOrgCode"].ToString();
                        var UpzilaOrg = dr1["UpzilaOrgCode"].ToString();
                        var ThanaOrg = dr1["ThanaOrgCode"].ToString();

                        int a = Converter.GetInteger(DiviOrg);
                        int b = Converter.GetInteger(DistOrg);
                        int c = Converter.GetInteger(UpzilaOrg);
                        int d = Converter.GetInteger(ThanaOrg);

                        ddlDivision.SelectedValue = Converter.GetString(a);
                        ddlDistrict.SelectedValue = Converter.GetString(b);
                        ddlUpzila.SelectedValue = Converter.GetString(c);
                        ddlThana.SelectedValue = Converter.GetString(d);
                    }
                }

                DivisionDropdown();
                DistrictDropdown();
                UpzilaDropdown();
                ThanaDropdown();

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlThana_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }

        private void InvalidDateMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Future Date');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Future Date');", true);
            return;


            
        }
        protected void txtCuOpenDate_TextChanged(object sender, EventArgs e)
        {
            if (txtCuOpenDate.Text != "")
            {
                DateTime opdate1 = DateTime.ParseExact(txtCuOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime opdate2 = DateTime.ParseExact(ProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if (opdate1 > opdate2)
                {
                    InvalidDateMSG();
                    txtCuOpenDate.Text = GetOpenDate.Text;
                    txtCuOpenDate.Focus();
                }

                txtCuCertificateNo.Focus();
            }
        }
        private void InvalidRecords()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Invalid Credit Union No.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Credit Union No.');", true);
            return;
        }

        protected void BtnCreUniontExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void txtGLCashCode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtGLCashCode.Text != string.Empty)
                {
                    int GLCode = Converter.GetInteger(txtGLCashCode.Text);
                    A2ZCGLMSTDTO getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

                    if (getDTO.GLAccNo > 0)
                    {
                        txtGLCashCode.Text = Converter.GetString(getDTO.GLAccNo);
                        ddlGLCashCode.SelectedValue = Converter.GetString(getDTO.GLAccNo);
                    }
                    else
                    {
                        txtGLCashCode.Text = string.Empty;
                        txtGLCashCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlGLCashCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlGLCashCode.SelectedValue != "-Select-")
                {

                    int GLCode = Converter.GetInteger(ddlGLCashCode.SelectedValue);
                    A2ZCGLMSTDTO getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

                    if (getDTO.GLAccNo > 0)
                    {
                        txtGLCashCode.Text = Converter.GetString(getDTO.GLAccNo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        


    }
}
