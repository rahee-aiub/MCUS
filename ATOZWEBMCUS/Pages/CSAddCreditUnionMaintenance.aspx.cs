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
using System.Text;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.BLL;



namespace ATOZWEBMCUS.Pages
{
    public partial class CSAddCreditUnionMaintenance : System.Web.UI.Page
    {

        protected Int32 userPermission;
        //Button BtnReEdit = new Button();
        //Button BtnInput = new Button();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {

                    userPermission = Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION));

                    //if (userPermission != 10)
                    //{
                    //    string notifyMsg = "?txtOne=" + Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME)) +
                    //                       "&txtTwo=" + "You Don't Have Permission for Input" +
                    //                       "&txtThree=" + "Contact Your Super User" + "&PreviousMenu=A2ZERPModule.aspx";
                    //    Server.Transfer("Notify.aspx" + notifyMsg);
                    //}

                    //else
                    {
                        A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                        DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtCuOpenDate.Text = date;
                        ProcDate.Text = date;

                        A2ZRECCTRLDTO getDTO = (A2ZRECCTRLDTO.ReadLastRecords(5));
                        lblLastSRL.Text = Converter.GetString(getDTO.CtrlRecLastNo);


                        BtnCreUnionSubmit.Visible = true;
                        BtnCreUnionUpdate.Visible = false;

                        DivisionDropdown();
                        DistrictInFo();
                        UpzilaInfo();
                        ThanaInfo();
                        GLCashCodeDropdown();
                        txtCreUName.Focus();
                        gvVerify();


                    }
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }

        }

        //protected void DisableActionMenu()
        //{

        //    string qry1 = "SELECT Id,CuProcFlag FROM A2ZCUNION where CuProcFlag BETWEEN 9 AND 10  AND CuStatus !='99'";
        //    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
        //    if (dt1.Rows.Count > 0)
        //    {
        //        Button BtnReEdit = new Button();
        //        Button BtnInput = new Button();
        //        foreach (GridViewRow r in gvCUInfo.Rows)
        //        {
        //            BtnReEdit = (Button)gvCUInfo.Rows[r.RowIndex].Cells[0].FindControl("BtnReEdit");
        //            BtnInput = (Button)gvCUInfo.Rows[r.RowIndex].Cells[1].FindControl("BtnInput");



        //            //Button BtnReEdit = (Button)gvCUInfo.Rows[r.RowIndex].Cells[0].FindControl("BtnReEdit");
        //            //Button BtnInput = (Button)gvCUInfo.Rows[r.RowIndex].Cells[0].FindControl("BtnInput");
        //            foreach (DataRow dr1 in dt1.Rows)
        //            {

        //                var ParentId = dr1["Id"].ToString();
        //                var ProcFlag = dr1["CuProcFlag"].ToString();

        //                int Id = Converter.GetInteger(ParentId);

        //                int PFlag = Converter.GetInteger(ProcFlag);


        //                if (PFlag == 9)
        //                {
        //                    BtnInput.Visible = true;
        //                    BtnReEdit.Visible = false;
        //                }

        //                if (PFlag == 10)
        //                {
        //                    BtnReEdit.Visible = true;
        //                    BtnInput.Visible = false;
        //                }

        //            }
        //        }

        //    }
        //}

        protected void GLCashCodeDropdown()
        {

            string sqlquery = "SELECT GLAccNo,GLAccDesc from A2ZCGLMST where GLRecType = 2 and GLSubHead = 10101000 ORDER BY GLAccDesc ASC";
            ddlGLCashCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGLCashCode, "A2ZGLMCUS");

        }
        protected void gvVerify()
        {
            string sqlquery3 = "SELECT CuType,CuTypeName,CuNo,CuName,CuOpDt,CuProcDesc from A2ZCUAPPLICATION where CuProcFlag BETWEEN 8 AND 10  AND CuStatus !='99'";
            gvCUInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvCUInfo, "A2ZCSMCUS");
        }
        protected void DisplayMessage()
        {
            lblCUNoMsg.Text = Converter.GetString(lblNewSRL.Text);
            lblCUTypeMsg.Text = Converter.GetString(1);
            
            string Msg = "";

            string c = "";
            string d = "";
            string b = "";
            string e = "";

            
            c = "Generated New CU Application No";
            d = "Application No. :";
            e = "Credit Union Type : Affilate Member";

            b = string.Format(lblCUNoMsg.Text);
                        
            Msg += c;
            Msg += "\\n";
            Msg += e;
            Msg += "\\n";
            Msg += d + b;
            //Msg += b;

            
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;

            
            
            //---------------------------
            //lblCUNoMsg.Text = Converter.GetString(lblNewSRL.Text);
            //lblCUTypeMsg.Text = Converter.GetString(1);
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string c = "Generated New CU Application No";
            //string d = "Application No. :";

            //string b = string.Format(lblCUNoMsg.Text);

            //sb.Append("<script type = 'text/javascript'>");
            //sb.Append("window.onload=function(){");
            //sb.Append("alert('");
            //sb.Append(c);
            //sb.Append("\\n");
            //sb.Append("Credit Union Type : Affilate Member");
            //sb.Append("\\n");
            //sb.Append(d);

            //sb.Append(b);
            //sb.Append("')};");
            //sb.Append("</script>");
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        }


        protected void BtnCreUnionSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZCUAPPLICATIONDTO objDTO = new A2ZCUAPPLICATIONDTO();
                A2ZMEMBERDTO MemDTO = new A2ZMEMBERDTO();
                A2ZRECCTRLDTO getDTO = (A2ZRECCTRLDTO.GetLastRecords(5));
                MemDTO.CreditUnionNo = Converter.GetInteger(getDTO.CtrlRecLastNo);
                objDTO.CreditUnionNo = Converter.GetInteger(getDTO.CtrlRecLastNo);
                lblNewSRL.Text = Converter.GetString(getDTO.CtrlRecLastNo);

                lblLastSRL.Text = Converter.GetString(getDTO.CtrlRecLastNo);

                MemDTO.MemberName = Converter.GetString(txtCreUName.Text);
                MemDTO.MemType = 1;

                objDTO.CreditUnionName = Converter.GetString(txtCreUName.Text);
                //DateTime date = DateTime.ParseExact(txtCuOpenDate.Text, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if (txtCuOpenDate.Text != string.Empty)
                {
                    DateTime opdate = DateTime.ParseExact(txtCuOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objDTO.opendate = opdate;
                    MemDTO.OpenDate = opdate;
                }
                else
                {
                    string CheckNullOpenDate = "";
                    objDTO.NullOpenDate = CheckNullOpenDate;
                }
                //objDTO.opendate = Converter.GetDateTime(System.DateTime.Now.ToLongDateString());


                //objDTO.MemberFlag = Converter.GetSmallInteger(ddlCuMemberFlag.SelectedValue);
                //     objDTO.MemberType = Converter.GetSmallInteger(ddlCuMemberType.SelectedValue);
                objDTO.CuType = 1;
                objDTO.CuTypeName = Converter.GetString("Affiliate");
                objDTO.CuProcFlag = 8;
                objDTO.CuProcDesc = "Input";
                objDTO.InputBy = Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_ID));
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();

                if(dto.ProcessDate==DateTime.MinValue)
                {
                    string checkNullStatusDate = "";
                    objDTO.NullStatusDate = checkNullStatusDate;
                    string checkNullInputByDate = "";
                    objDTO.NullInputByDate = checkNullInputByDate;
                    string checkNullVerifyDate = "";
                    objDTO.NullVerifyByDate = checkNullVerifyDate;
                    string checkNullApproveDate = "";
                    objDTO.NullApprovByDate = checkNullApproveDate;
                    string checkNullValueDate = "";
                    objDTO.NullValueDate = checkNullValueDate;
                    string checkNullCreateDate = "";
                    objDTO.NullCreateDate = checkNullCreateDate;
                   
                }
                else
                {
                    objDTO.CuStatusDate = Converter.GetDateTime(dto.ProcessDate);
                    objDTO.InputByDate = Converter.GetDateTime(dto.ProcessDate);
                    objDTO.VerifyByDate = Converter.GetDateTime(dto.ProcessDate);
                    objDTO.ApprovByDate = Converter.GetDateTime(dto.ProcessDate);
                    objDTO.ValueDate = Converter.GetDateTime(dto.ProcessDate);
                    objDTO.CreateDate = Converter.GetDateTime(dto.ProcessDate);
                    
                }

               
                objDTO.CertificateNo = Converter.GetInteger(txtCuCertificateNo.Text);
                objDTO.AddressL1 = Converter.GetString(txtCuAddressL1.Text);
                objDTO.AddressL2 = Converter.GetString(txtCuAddressL2.Text);
                objDTO.AddressL3 = Converter.GetString(txtCuAddressL3.Text);
                objDTO.TelephoneNo = Converter.GetString(txtCuTelNo.Text);
                objDTO.MobileNo = Converter.GetString(txtCuMobileNo.Text);
                objDTO.Fax = Converter.GetString(txtCuFax.Text);
                objDTO.email = Converter.GetString(txtCuEmail.Text);
                objDTO.Division = Converter.GetInteger(ddlDivision.SelectedValue);
                objDTO.District = Converter.GetInteger(ddlDistrict.SelectedValue);
                objDTO.Upzila = Converter.GetInteger(ddlUpzila.SelectedValue);
                objDTO.Thana = Converter.GetInteger(ddlThana.SelectedValue);
                objDTO.GLCashCode = Converter.GetInteger(txtGLCashCode.Text);
              

                //          int row = A2ZMEMBERDTO.Insert(MemDTO);
                int roweffect = A2ZCUAPPLICATIONDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    //ddlCuMemberFlag.SelectedValue = "0";
                    //     ddlCuMemberType.SelectedValue = "0";
                    clearInfo();
                    txtCreUName.Focus();
                    DisplayMessage();
                    gvVerify();



                }


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnCreUnionSubmit_Click Problem');</script>");
                //throw ex;
            }

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
            //txtCuOpenDate.Text = string.Empty;
            txtCuCertificateNo.Text = string.Empty;
            txtGLCashCode.Text = string.Empty;
            txtCuAddressL1.Text = string.Empty;
            txtCuAddressL2.Text = string.Empty;
            txtCuAddressL3.Text = string.Empty;
            txtCuEmail.Text = string.Empty;
            txtCuFax.Text = string.Empty;
            txtCuTelNo.Text = string.Empty;
            txtCuMobileNo.Text = string.Empty;
            ddlDivision.SelectedIndex = 0;
            ddlDistrict.SelectedIndex = 0;
            ddlUpzila.SelectedIndex = 0;
            ddlThana.SelectedIndex = 0;
            ddlGLCashCode.SelectedIndex = 0;
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


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Not Open an Account');", true);
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
                    txtCuOpenDate.Text = ProcDate.Text;
                    txtCuOpenDate.Focus();
                }
                txtCuCertificateNo.Focus();
            }
        }

        protected void BtnCreUniontExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void BtnInput_Click(object sender, EventArgs e)
        {

        }
        protected void BtnPrint_Click(object sender, EventArgs e)
        {

            try
            {
                Button c = (Button)sender;
                GridViewRow r = (GridViewRow)c.NamingContainer;
                Label lblcutype = (Label)gvCUInfo.Rows[r.RowIndex].Cells[3].FindControl("lblcutype");
                Label CrNo = (Label)gvCUInfo.Rows[r.RowIndex].Cells[4].FindControl("lblcno");
                int cutype = Converter.GetSmallInteger(lblcutype.Text);
                int cuno = Converter.GetInteger(CrNo.Text);
                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, cutype);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, cuno);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptVerifyReport");

                Response.Redirect("ReportServer.aspx", false);

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnPrint_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnInput_Click1(object sender, EventArgs e)
        {
            try
            {

                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;
                Label CTyNo = (Label)gvCUInfo.Rows[r.RowIndex].Cells[3].FindControl("lblcutype");
                Label CrNo = (Label)gvCUInfo.Rows[r.RowIndex].Cells[4].FindControl("lblcno");
                int a = Converter.GetSmallInteger(CTyNo.Text);
                int c = Converter.GetSmallInteger(CrNo.Text);

                Int16 CuType = Converter.GetSmallInteger(a);
                int CNo = Converter.GetSmallInteger(c);
                A2ZCUAPPLICATIONDTO objDTO = (A2ZCUAPPLICATIONDTO.GetInformation(CuType, CNo));


                objDTO.CuType = CuType;
                objDTO.CreditUnionNo = CNo;
                
                if (objDTO.CuProcFlag == 8)
                {
                    objDTO.CuProcFlag = Converter.GetSmallInteger(11);
                    objDTO.CuProcDesc = "Input";
                }

                if (objDTO.CuProcFlag == 10)
                {
                    objDTO.CuProcFlag = Converter.GetSmallInteger(21);
                    objDTO.CuProcDesc = "ReEdited";
                }

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                objDTO.VerifyByDate = Converter.GetDateTime(dto.ProcessDate);
                objDTO.ApprovByDate = Converter.GetDateTime(dto.ProcessDate);      
                
                int roweffect = A2ZCUAPPLICATIONDTO.UpdateInformation2(objDTO);
                if (roweffect > 0)
                {
                    gvVerify();
                    string CheckQuery = "SELECT CuType,CuTypeName,CuNo,CuName,CuOpDt,CuProcDesc FROM A2ZCUAPPLICATION Where CuProcFlag BETWEEN 8 AND 10 and CuStatus !='99'";
                    DataTable dt = new DataTable();
                    dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");

                    if (dt.Rows.Count <= 0)
                    {
                        DivGridViewCancle.Visible = false;


                    }

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnInput_Click1 Problem');</script>");
                //throw ex;
            }
        }


        protected void BtnReEdit_Click(object sender, EventArgs e)
        {
            try
            {

                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;
                Label CTyNo = (Label)gvCUInfo.Rows[r.RowIndex].Cells[3].FindControl("lblcutype");
                Label CrNo = (Label)gvCUInfo.Rows[r.RowIndex].Cells[4].FindControl("lblcno");
                int a = Converter.GetSmallInteger(CTyNo.Text);
                int c = Converter.GetSmallInteger(CrNo.Text);

                Int16 CuType = Converter.GetSmallInteger(a);
                lblCuType.Text = Converter.GetString(a);
                int CNo = Converter.GetSmallInteger(c);
                lblCuNo.Text = Converter.GetString(c);

                A2ZCUAPPLICATIONDTO getDTO = (A2ZCUAPPLICATIONDTO.GetInformation(CuType, CNo));

                if (getDTO.CreditUnionNo > 0)
                {

                    //txtCreditUNo.Text = Converter.GetString(getDTO.CreditUnionNo);
                    txtCreUName.Text = Converter.GetString(getDTO.CreditUnionName);
                    //ddlCreditUNo.SelectedValue = Converter.GetString(getDTO.CreditUnionNo);
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
                    ddlDivision.SelectedValue = Converter.GetString(getDTO.Division);
                    ddlDistrict.SelectedValue = Converter.GetString(getDTO.District);
                    ddlUpzila.SelectedValue = Converter.GetString(getDTO.Upzila);
                    ddlThana.SelectedValue = Converter.GetString(getDTO.Thana);
                    lblProcFlag.Text = Converter.GetString(getDTO.CuProcFlag);



                    txtCreUName.Focus();
                    BtnCreUnionSubmit.Visible = false;
                    BtnCreUnionUpdate.Visible = true;
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnReEdit_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnCreUnionUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                A2ZCUAPPLICATIONDTO UpDTO = new A2ZCUAPPLICATIONDTO();


                UpDTO.CuType = Converter.GetSmallInteger(lblCuType.Text);
                UpDTO.CreditUnionNo = Converter.GetInteger(lblCuNo.Text);
                UpDTO.CreditUnionName = Converter.GetString(txtCreUName.Text);


                if (lblProcFlag.Text == "9")
                {
                    UpDTO.CuProcFlag = Converter.GetSmallInteger(10);
                    UpDTO.CuProcDesc = "Edited";
                }
                else
                {
                    UpDTO.CuProcFlag = Converter.GetSmallInteger(8);
                    UpDTO.CuProcDesc = "Input";
                }
                UpDTO.CuTypeName = Converter.GetString("Affiliate");
                UpDTO.InputBy = Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_ID));


                if (txtCuOpenDate.Text != string.Empty)
                {
                    DateTime opdate = DateTime.ParseExact(txtCuOpenDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.opendate = opdate;
                }
                else
                {
                    string CheckNullOpenDate = "";
                    UpDTO.NullOpenDate = CheckNullOpenDate;
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

                if (dto.ProcessDate == DateTime.MinValue)
                {
                    string checkNullStatusDate = "";
                    UpDTO.NullStatusDate = checkNullStatusDate;
                    string checkNullInputByDate = "";
                    UpDTO.NullInputByDate = checkNullInputByDate;
                    string checkNullVerifyDate = "";
                    UpDTO.NullVerifyByDate = checkNullVerifyDate;
                    string checkNullApproveDate = "";
                    UpDTO.NullApprovByDate = checkNullApproveDate;
                    string checkNullValueDate = "";
                    UpDTO.NullValueDate = checkNullValueDate;
                    string checkNullCreateDate = "";
                    UpDTO.NullCreateDate = checkNullCreateDate;

                }
                else
                {
                    UpDTO.CuStatusDate = Converter.GetDateTime(dto.ProcessDate);
                    UpDTO.InputByDate = Converter.GetDateTime(dto.ProcessDate);
                    UpDTO.VerifyByDate = Converter.GetDateTime(dto.ProcessDate);
                    UpDTO.ApprovByDate = Converter.GetDateTime(dto.ProcessDate);
                    UpDTO.ValueDate = Converter.GetDateTime(dto.ProcessDate);
                    UpDTO.CreateDate = Converter.GetDateTime(dto.ProcessDate);
                }

                UpDTO.GLCashCode = Converter.GetInteger(txtGLCashCode.Text);
                //      int row = A2ZMEMBERDTO.Update(MemDTO);
                int roweffect = A2ZCUAPPLICATIONDTO.UpdateInformation(UpDTO);
                if (roweffect > 0)
                {

                    BtnCreUnionSubmit.Visible = true;
                    BtnCreUnionUpdate.Visible = false;
                    //ddlCuMemberFlag.SelectedValue = "0";
                    clearInfo();
                    gvVerify();
                    string CheckQuery = "SELECT CuType,CuTypeName,CuNo,CuName,CuOpDt,CuProcDesc FROM A2ZCUAPPLICATION Where CuProcFlag BETWEEN 8 AND 10 and CuStatus !='99'";
                    DataTable dt = new DataTable();
                    dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");

                    if (dt.Rows.Count <= 0)
                    {
                        DivGridViewCancle.Visible = false;


                    }



                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnCreUnionUpdate_Click Problem');</script>");
                //throw ex;
            }
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtGLCashCode_TextChanged Problem');</script>");
                //throw ex;
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlGLCashCode_SelectedIndexChanged Problem');</script>");
                throw ex;
            }
        }


    }




}

