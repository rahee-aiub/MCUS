using System;
using System.Web;
using System.Web.UI;
using DataAccessLayer.DTO;
using System.Drawing;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.HouseKeeping;
using System.Data;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using System.Web.UI.WebControls;
using System.Threading;

namespace ATOZWEBMCUS.Pages
{
    /// <summary>
    /// ONI 13/02/2016
    /// </summary>
    public partial class A2ZERP : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                DivFromCashCode.Visible = false;

                linkSMSReload.Text = "SMS Reload";

                lblLastProcDate.ForeColor = Color.Black;
                lblNewProcDt.ForeColor = Color.Black;
                lblSOD.ForeColor = Color.Black;

                DivChangePassword.Visible = false;
                txtIdNo.Focus();
                btChangePassword.Visible = false;
                DivLogin.Visible = false;

                DivDetails.Visible = false;

                if (IsPostBack)
                {


                    lblPassword.Text = string.Empty;
                    lblold.Text = string.Empty;
                }
                else
                {
                   

                

                    lblChgProcDt.Visible = false;
                    txtChgProcDt.Visible = false;

                    lblUserId.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));


                    txtsmsMsg.ReadOnly = true;

                    DivGridViewSMS.Visible = false;
                    DivTextMsg.Visible = false;

                    linkSMSMessage.Visible = false;
                    lblNo.Visible = false;

                    GetSMSMsg();


                    DivLogin.Visible = false;
                    DivChangePassword.Visible = false;
                    btnLogin.Enabled = true;
                    DivLogin.Visible = false;
                    Session["dummy"] = "dummy";

                    AtoZUtility a2zUtility = new AtoZUtility();
                    string notifyMsg = string.Empty;
                    //OPEN

                    if (a2zUtility.CheckAtoZLicense() == 1)
                    {
                        notifyMsg = "?txtOne=" + "System Parameter Not Found" + "&txtTwo=" + "Contact AtoZ Computer Services or" +
                                           "&txtThree=" + "Contact Your Super User" + "&PreviousMenu=A2ZERP.aspx";
                        Server.Transfer("Notify.aspx" + notifyMsg);
                    }
                    if (a2zUtility.CheckAtoZLicense() == 2)
                    {
                        notifyMsg = "?txtOne=" + "AtoZ License is Mismatch" + "&txtTwo=" + "Contact rptINVPPICRequirementMaterial Computer Services or" +
                                           "&txtThree=" + "Contact Your Super User" + "&PreviousMenu=A2ZERP.aspx";
                        Server.Transfer("Notify.aspx" + notifyMsg);
                    }





                    int cr = a2zUtility.CheckClientRegistry();
                    //if (cr == 9)
                    //{
                    //    notifyMsg = "?txtOne=" + "Client Registry Missing" + "&txtTwo=" + "Contact AtoZ Computer Services or" +
                    //                       "&txtThree=" + "Contact Your Super User" + "&PreviousMenu=ClientRegistry.aspx";
                    //    Server.Transfer("Notify.aspx" + notifyMsg);
                    //}
                    if (cr == 1)
                    {
                        notifyMsg = "?txtOne=" + "Mismatch Client Registry" + "&txtTwo=" + "Contact AtoZ Computer Services or" +
                                           "&txtThree=" + "Contact Your Super User" + "&PreviousMenu=Notify.aspx";
                        Server.Transfer("Notify.aspx" + notifyMsg);
                    }

                    btnCustomerService.Visible = false;

                    btnGl.Visible = false;

                    btnHk.Visible = false;

                    btnHr.Visible = false;

                    btnInv.Visible = false;

                    btnBooth.Visible = false;

                    btnOffBooth.Visible = false;



                    A2ZERPSYSPRMDTO dto = A2ZERPSYSPRMDTO.GetParameterValue();
                    //lblCompany.Text = dto.PrmUnitName;

                    string a = (string)Session["IdsNo"];

                    txtIdNo.Text = a;
                    CheckUserId();

                    string sqlQuery = @"SELECT * FROM dbo.A2ZSYSMODULECTRL  WHERE IDSNO='" + txtIdNo.Text + "'";

                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlQuery, "A2ZHKMCUS");

                    if (dt.Rows.Count > 0)
                    {

                        lblNoRec.Text = Converter.GetString(dt.Rows.Count);


                        DataView view = new DataView(dt);

                        foreach (DataRowView row in view)
                        {
                            int moduleNo = Converter.GetInteger(row["ModuleNo"].ToString());

                            if (dt.Rows.Count == 1)
                            {
                                string LogOut = (string)Session["LogOutFlag"];
                                if (LogOut == "1")
                                {
                                    Session["LogOutFlag"] = "";
                                    Response.Redirect("A2ZStartUp.aspx", false);
                                    return;
                                }
                                else
                                {
                                    hdnModule.Value = Converter.GetString(moduleNo);
                                    SODProcessFunction();
                                    if (lblMsgFlag.Text == "1")
                                    {
                                        ShowMessage();
                                    }
                                    else if (lblMsgFlag.Text == "2")
                                    {
                                        return;
                                    }

                                    if (moduleNo == 1)
                                    {
                                        lblSwFlag.Text = "0";

                                        if (lblSwFlag.Text == "0")
                                        {
                                            SelectFromCashCodeSw();
                                        }
                                        else
                                        {
                                            CSFunc();
                                            return;
                                        }
                                    }
                                    else
                                        if (moduleNo == 2)
                                        {
                                            lblSwFlag.Text = "0";

                                            if (lblSwFlag.Text == "0")
                                            {
                                                SelectFromCashCodeSw();
                                            }
                                            else
                                            {
                                                GLFunc();
                                                return;
                                            }
                                        }
                                        else
                                            if (moduleNo == 3)
                                            {
                                                HKFunc();
                                                return;
                                            }
                                            else
                                                if (moduleNo == 4)
                                                {
                                                    HRFunc();
                                                    return;
                                                }
                                            else
                                                if (moduleNo == 5)
                                                {
                                                    lblSwFlag.Text = "0";

                                                    if (lblSwFlag.Text == "0")
                                                    {
                                                        SelectFromCashCodeSw();
                                                    }
                                                    else
                                                    {
                                                        INVFunc();
                                                        return;
                                                    }
                                                }
                                            
                                                else
                                                    if (moduleNo == 6)
                                                    {
                                                        lblSwFlag.Text = "0";

                                                        if (lblSwFlag.Text == "0")
                                                        {
                                                            SelectFromCashCodeSw();
                                                        }
                                                        else
                                                        {
                                                            BoothFunc();
                                                            return;
                                                        }

                                                    }
                                    //else
                                    //    if (moduleNo == 7)
                                    //    {
                                    //        OffBoothFunc();
                                    //        return;
                                    //    }
                                }
                            }

                            switch (moduleNo)
                            {
                                case 1:
                                    btnCustomerService.Visible = true;
                                    break;
                                case 2:
                                    btnGl.Visible = true;
                                    break;
                                case 3:
                                    btnHk.Visible = true;
                                    break;

                                case 4:
                                    btnHr.Visible = true;
                                    break;

                                case 5:
                                    btnInv.Visible = true;
                                    break;

                                case 6:
                                    btnBooth.Visible = true;
                                    break;
                                case 7:
                                    btnOffBooth.Visible = true;
                                    break;
                                default:

                                    break;
                            }

                        }


                    }
                    else
                    {

                        String csname1 = "PopupScript";
                        Type cstype = GetType();
                        ClientScriptManager cs = Page.ClientScript;

                        if (!cs.IsStartupScriptRegistered(cstype, csname1))
                        {
                            String cstext1 = "alert('This User Have No Permission.');";
                            cs.RegisterStartupScript(cstype, csname1, cstext1, true);

                            txtIdNo.Text = string.Empty;
                            txtPassword.Text = string.Empty;
                        }
                        return;

                    }


                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private void GetSMSMsg()
        {
            string qry4 = "SELECT SMSNo,SMSDate FROM A2ZUSERSMS WHERE  SMSToIdsNo='" + lblUserId.Text + "' AND (SMSStatus=3)";
            DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZHKMCUS");
            int totrec = dt4.Rows.Count;
            if (dt4.Rows.Count > 0)
            {
                foreach (DataRow dr2 in dt4.Rows)
                {
                    linkSMSMessage.Text = "New SMS Messages";
                    lblNo.Text = Converter.GetString(totrec);
                    DivSMS.Visible = true;
                    linkSMSMessage.Visible = true;
                    lblNo.Visible = true;

                }

            }

        }


        private void gvSMS()
        {
            string sqlquery3 = "SELECT SMSDate,SMSNo,SMSFromIdsNo,SMSNote,SMSStatus from A2ZUSERSMS WHERE  SMSToIdsNo='" + lblUserId.Text + "' AND (SMSStatus=3)";
            gvSMSInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvSMSInfo, "A2ZHKMCUS");
        }


        protected void linkSMSMessage_Click(object sender, EventArgs e)
        {
            if (DivGridViewSMS.Visible == false)
            {

                gvSMS();
                gvSMSInfo.Visible = true;

                DivGridViewSMS.Visible = true;
            }

        }


        protected void BtnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (DivTextMsg.Visible == false)
                {
                    Button b = (Button)sender;
                    GridViewRow r = (GridViewRow)b.NamingContainer;
                    Label smsNo = (Label)gvSMSInfo.Rows[r.RowIndex].Cells[2].FindControl("lblSMSNo");
                    Label smsNote = (Label)gvSMSInfo.Rows[r.RowIndex].Cells[4].FindControl("lblSMSNote");
                    Label smsStatus = (Label)gvSMSInfo.Rows[r.RowIndex].Cells[5].FindControl("lblSMSStatus");

                    int a = Converter.GetInteger(smsNo.Text);
                    lblsmsNo.Text = Converter.GetString(a);

                    DivTextMsg.Visible = true;
                    txtsmsMsg.Text = smsNote.Text;
                    txtsmsMsg.Visible = true;


                    lblStatus.Text = "4";



                    string CheckUp = "UPDATE A2ZUSERSMS SET SMSStatus='" + lblStatus.Text + "' WHERE SMSNo='" + lblsmsNo.Text + "'";
                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(CheckUp, "A2ZHKMCUS"));
                    if (rowEffect > 0)
                    {
                        string qry4 = "SELECT SMSNo,SMSDate FROM A2ZUSERSMS WHERE  SMSToIdsNo='" + lblUserId.Text + "' AND (SMSStatus=3)";
                        DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZHKMCUS");
                        int totrec = dt4.Rows.Count;
                        lblNo.Text = Converter.GetString(totrec);
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            string qry4 = "SELECT SMSNo,SMSDate FROM A2ZUSERSMS WHERE  SMSToIdsNo='" + lblUserId.Text + "' AND (SMSStatus=3)";
            DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZHKMCUS");
            if (dt4.Rows.Count <= 0)
            {
                DivGridViewSMS.Visible = false;

                DivGridViewSMS.Visible = false;
                DivTextMsg.Visible = false;

                DivSMS.Visible = false;
                linkSMSMessage.Visible = false;
                lblNo.Visible = false;

            }
            else
            {
                DivTextMsg.Visible = false;
                txtsmsMsg.Text = string.Empty;

                gvSMS();
                gvSMSInfo.Visible = true;
            }
        }





        protected void SODProcessFunction()
        {
            lblMsgFlag.Text = "0";

            A2ZERPSYSPRMDTO prmobj = A2ZERPSYSPRMDTO.GetParameterValue();

            if (prmobj.PrmEODStat == 0)
            {
                return;
            }
            else
            {
                A2ZSYSIDSDTO sysobj = A2ZSYSIDSDTO.GetUserInformation(Converter.GetInteger(txtIdNo.Text), "A2ZHKMCUS");

                if (sysobj.SODflag == false)
                {
                    lblMsgFlag.Text = "2";
                    String csname1 = "PopupScript";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;

                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('Access Denied START OF DAY NOT DONE');";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    }
                    return;
                }
                else
                {
                    lblMsgFlag.Text = "1";
                }
            }
        }

        protected void ShowMessage()
        {

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_DUMMYProcessDt", "A2ZGLMCUS"));

            if (result == 0)
            {
                var dt = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime processDate = dt.ProcessDate;
                DateTime NewprocessDate = dt.DummyProcessDate;

                txtLastProcDt.Text = Converter.GetString(String.Format("{0:D}", processDate));
                txtNewProcDt.Text = Converter.GetString(String.Format("{0:D}", NewprocessDate));

                string date = NewprocessDate.ToString("dd/MM/yyyy");
                txtChgProcDt.Text = date;


                txtStDay.Focus();
            }


            DivDetails.Visible = true;

            DivMain.Attributes.CssStyle.Add("opacity", "0.1");
            DivDetails.Style.Add("Top", "220px");
            DivDetails.Style.Add("Right", "300px");
            DivDetails.Style.Add("position", "fixed");
            DivDetails.Attributes.CssStyle.Add("opacity", "200");
            DivDetails.Attributes.CssStyle.Add("z-index", "200");

            lockbutton();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
               

                if (txtStDay.Text == string.Empty)
                {
                    String csname1 = "PopupScript";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;

                 
                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('Please Input START OF DAY');";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    }

                    DivMain.Attributes.CssStyle.Add("opacity", "100");

                    DivDetails.Visible = false;
                    DivMain.Visible = true;

                    unlockbutton();
                    txtStDay.Text = string.Empty;

                    return;

                }

                if (txtStDay.Text == "START OF DAY")
                {
                   

                    BtnChg.Visible = false;
                    Button2.Visible = false;

                  

                    //BtnChg.Style.Add("visibility", "hidden");
                    //Button2.Style.Add("visibility", "hidden");



                    lblProessing.Text = "Please Wait... Auto SOD Processing";

                    ProcessSOD();

                    //DivMain.Attributes.CssStyle.Add("opacity", "100");

                    //DivDetails.Visible = false;
                    //DivMain.Visible = true;

                    //ModuleFunction();
                    return;

                }
                else
                {

                    DivMain.Attributes.CssStyle.Add("opacity", "100");

                    DivDetails.Visible = false;
                    DivMain.Visible = true;

                    unlockbutton();
                    txtStDay.Text = string.Empty;
                    txtStDay.Focus();

                    String csname1 = "PopupScript";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;

                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('Please Input START OF DAY');";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


       
        protected void ProcessSOD()
        {
          


            var prm = new object[2];
            prm[0] = txtIdNo.Text;


            DateTime fdate = DateTime.ParseExact(txtChgProcDt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            prm[1] = fdate;


            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLSODProcess", prm, "A2ZGLMCUS"));

            if (result == 0)
            {
                UpdateEODStat();
                DivDetails.Visible = false;
                DivMain.Visible = true;

                SelectFromCashCodeSw();

                ModuleFunction();
                return;
            }

        }

        protected void UpdateEODStat()
        {
            try
            {
                Int16 BStat = 0;

                int roweffect = A2ZERPSYSPRMDTO.UpdateEODStat(BStat);
                if (roweffect > 0)
                {

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateEODStat Problem');</script>");
                //throw ex;
            }

        }

        protected void btnHideMessageDiv_Click(object sender, EventArgs e)
        {
            //DivMain.Attributes.CssStyle.Add("opacity", "100");

            //DivDetails.Visible = false;
            // DivMain.Visible = true;

            //unlockbutton();
        }

        private void unlockbutton()
        {
            btnInv.Enabled = true;
            btnHr.Enabled = true;
            btnHk.Enabled = true;
            btnBooth.Enabled = true;
            btnGl.Enabled = true;
            btnHome.Enabled = true;
            btnCustomerService.Enabled = true;
        }

        private void lockbutton()
        {
            btnInv.Enabled = false;
            btnHr.Enabled = false;
            btnHk.Enabled = false;
            btnBooth.Enabled = false;
            btnGl.Enabled = false;
            btnHome.Enabled = false;
            btnCustomerService.Enabled = false;
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    AtoZUtility a2zUtility = new AtoZUtility();

            //    string orgPass = a2zUtility.GeneratePassword(txtOrgPass.Text, 1);

            //    if (orgPass != txtPassword.Text)
            //    {
            //        lblPassword.Text = "Wrong Password";
            //        lblPassword.ForeColor = Color.Red;
            //        return;
            //    }

            //    string dbName = Converter.GetString(SessionStore.GetValue(Params.SYS_SELECT_DBNAME));
            //    A2ZSYSIDSDTO.UpdateUserLoginFlag(Converter.GetInteger(txtIdNo.Text), 1);

            //    SessionStore.SaveToCustomStore(Params.SYS_USER_ID, txtIdNo.Text);
            //    SessionStore.SaveToCustomStore(Params.SYS_USER_NAME, lblWelcome.Text);
            //    SessionStore.SaveToCustomStore(Params.SYS_USER_LEVEL, txtIdsLevel.Text);
            //    SessionStore.SaveToCustomStore(Params.SYS_USER_PERMISSION, txtIdsFlag.Text);
            //    SessionStore.SaveToCustomStore(Params.SYS_USER_EMP_CODE, txtUserEmpCode.Text);
            //    SessionStore.SaveToCustomStore(Params.SYS_USER_SERVER_IP, txtServerIP.Text);
            //    SessionStore.SaveToCustomStore(Params.SYS_USER_SERVER_NAME, txtServerName.Text);
            //    SessionStore.SaveToCustomStore(Params.SYS_USER_IP, txtUserIP.Text);
            //    SessionStore.SaveToCustomStore(Params.SYS_USER_GLCASHCODE, txtGLCashCode.Text);

            //    AddAuditInformation();

            //    Response.Redirect("A2ZERPModule.aspx", false);

            //}

            //catch (Exception ex)
            //{

            //    throw ex;
            //}

        }

        protected void btChangePassword_Click(object sender, EventArgs e)
        {
            DivChangePassword.Visible = true;
            DivLogin.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            DivChangePassword.Visible = false;
            DivLogin.Visible = true;

            txtIdNo.Text = string.Empty;
            lblWelcome.Text = string.Empty;
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    AtoZUtility a2zUtility = new AtoZUtility();
            //    string orgPass = a2zUtility.GeneratePassword(txtOrgPass.Text, 1);

            //    if (orgPass != txtOldPassword.Text)
            //    {
            //        ShowMessage("Old Password did not match.", Color.Red);
            //        DivLogin.Visible = false;
            //    }
            //    else
            //    {
            //        if (txtNewPassword.Text != this.txtConfirmPassword.Text)
            //        {
            //            ShowMessage("New Password did not match.", Color.Red);
            //            DivLogin.Visible = false;
            //        }
            //        else
            //        {
            //            string newPass = a2zUtility.GeneratePassword(txtNewPassword.Text, 0);

            //            A2ZSYSIDSDTO idsDto = new A2ZSYSIDSDTO();

            //            idsDto.IdsNo = Converter.GetSmallInteger(txtIdNo.Text);
            //            idsDto.IdsPass = newPass;

            //            int rowEffiect = A2ZSYSIDSDTO.UpdateNewPassword(idsDto);

            //            if (rowEffiect > 0)
            //            {
            //                ShowMessage("Data saved successfully.", Color.Green);
            //            }
            //        }
            //    }

            //    DivChangePassword.Visible = false;

            //    txtIdNo.Text = string.Empty;
            //    lblWelcome.Text = string.Empty;
            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}
        }

        protected void txtIdNo_TextChanged(object sender, EventArgs e)
        {

            try
            {
                int checkUser = CheckUserId();

                if (checkUser == 0)
                {
                    txtPassword.Focus();
                    btnLogin.Enabled = true;
                }
                else
                {
                    btnLogin.Enabled = false;

                    string msg = string.Empty;

                    switch (checkUser)
                    {
                        case 1:
                            msg = "User Id Not Available";
                            break;
                        case 2:
                            msg = "Change Password - New Id was Created";
                            break;
                        //case 3:
                        //    msg = "User Id is using by other client or Abnormal Logout";

                        //    break;
                        default:
                            msg = "Check User Id Information";
                            break;
                    }

                    String csname1 = "PopupScript";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;

                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('" + msg + "');";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //protected void btnHideMessageDiv_Click(object sender, EventArgs e)
        //{
        //    //    DivMain.Attributes.CssStyle.Add("opacity", "100"); ShowMessage("System error occured.", Color.Red);
        //    //    DivMain.Visible = true;
        //    //    DivMessage.Visible = false;

        //    //    ShowLogIn();
        //}


        private void ModuleFunction()
        {
            if (hdnModule.Value == "1")
            {
                CSFunc();
            }
            else
                if (hdnModule.Value == "2")
                {
                    GLFunc();
                }
                else
                    if (hdnModule.Value == "3")
                    {
                        HKFunc();
                    }
                    else
                        if (hdnModule.Value == "5")
                        {
                            INVFunc();
                        }
                        else
                        if (hdnModule.Value == "4")
                        {
                            HRFunc();
                        }
                        else
                            if (hdnModule.Value == "6")
                            {
                                BoothFunc();
                            }

        }

        protected void btnCustomerService_Click(object sender, EventArgs e)
        {
            hdnModule.Value = "1";
            SODProcessFunction();
            if (lblMsgFlag.Text == "1")
            {
                ShowMessage();
            }
            else if (lblMsgFlag.Text == "2")
            {
                return;
            }
            else
            {
                lblSwFlag.Text = "0";

                if (lblSwFlag.Text == "0")
                {
                    SelectFromCashCodeSw();
                }
                else
                {
                    CSFunc();
                }


            }
        }

        protected void CSFunc()
        {
            try
            {
                SessionStore.SaveToCustomStore(Params.SYS_SELECT_DBNAME, "A2ZCSMCUS");
                SessionStore.SaveToCustomStore(Params.SYS_SELECT_MODULE, Enums.ModuleConstant.CustomerService);//Session["LoadSelectedModule"] = "Inventory";

                hdnModule.Value = "1";
                //CheckUserId();

                int checkUser = CheckUserId();
                switch (checkUser)
                {
                    case 3:
                        AbnormalMSG();
                        return;

                }

                A2ZSYSIDSDTO.UpdateUserCSLoginFlag(Converter.GetInteger(txtIdNo.Text), 1);

                //hdnModule.Value = "1";
                //CheckUserId();

                Login();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnGl_Click(object sender, EventArgs e)
        {
            hdnModule.Value = "2";
            SODProcessFunction();
            if (lblMsgFlag.Text == "1")
            {
                ShowMessage();
            }
            else if (lblMsgFlag.Text == "2")
            {
                return;
            }
            else
            {
                lblSwFlag.Text = "0";

                if (lblSwFlag.Text == "0")
                {
                    SelectFromCashCodeSw();
                }
                else
                {
                    GLFunc();
                }


            }

        }

        protected void GLFunc()
        {
            try
            {
                SessionStore.SaveToCustomStore(Params.SYS_SELECT_DBNAME, "A2ZGLMCUS");
                SessionStore.SaveToCustomStore(Params.SYS_SELECT_MODULE, Enums.ModuleConstant.GeneralLedger);//Session["LoadSelectedModule"] = "Inventory";

                hdnModule.Value = "2";
                //CheckUserId();

                int checkUser = CheckUserId();
                switch (checkUser)
                {
                    case 3:
                        AbnormalMSG();
                        return;

                }


                A2ZSYSIDSDTO.UpdateUserGLLoginFlag(Converter.GetInteger(txtIdNo.Text), 1);

                //hdnModule.Value = "2";
                //CheckUserId();


                Login();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnHk_Click(object sender, EventArgs e)
        {
            HKFunc();
        }

        protected void HKFunc()
        {
            try
            {
                SessionStore.SaveToCustomStore(Params.SYS_SELECT_DBNAME, "A2ZHKMCUS");
                SessionStore.SaveToCustomStore(Params.SYS_SELECT_MODULE, Enums.ModuleConstant.HouseKeeping);//Session["LoadSelectedModule"] = "Inventory";

                hdnModule.Value = "3";
                //CheckUserId();

                int checkUser = CheckUserId();
                switch (checkUser)
                {
                    case 3:
                        AbnormalMSG();
                        return;

                }

                A2ZSYSIDSDTO.UpdateUserHKLoginFlag(Converter.GetInteger(txtIdNo.Text), 1);

                //hdnModule.Value = "3";
                //CheckUserId();

                CashCodeDropdown();
                ddlFCashCode.SelectedValue = txtGLCashCode.Text;

                Login();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnInv_Click(object sender, EventArgs e)
        {
            hdnModule.Value = "5";
            SODProcessFunction();
            if (lblMsgFlag.Text == "1")
            {
                ShowMessage();
            }
            else if (lblMsgFlag.Text == "2")
            {
                return;
            }
            else
            {
                lblSwFlag.Text = "0";

                if (lblSwFlag.Text == "0")
                {
                    SelectFromCashCodeSw();
                }
                else
                {
                    INVFunc();
                }
            }    
        }


        protected void INVFunc()
        {
            try
            {
                SessionStore.SaveToCustomStore(Params.SYS_SELECT_DBNAME, "A2ZSTMCUS");
                SessionStore.SaveToCustomStore(Params.SYS_SELECT_MODULE, Enums.ModuleConstant.Inventory);//Session["LoadSelectedModule"] = "Inventory";

                hdnModule.Value = "5";
                //CheckUserId();

                int checkUser = CheckUserId();
                switch (checkUser)
                {
                    //case 3:
                    //    AbnormalMSG();
                    //    return;

                }

                A2ZSYSIDSDTO.UpdateUserCSLoginFlag(Converter.GetInteger(txtIdNo.Text), 1);

                //hdnModule.Value = "1";
                //CheckUserId();

                Login();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        protected void btnHr_Click(object sender, EventArgs e)
        {
            hdnModule.Value = "4";
            SODProcessFunction();
            if (lblMsgFlag.Text == "1")
            {
                ShowMessage();
            }
            else if (lblMsgFlag.Text == "2")
            {
                return;
            }
            else
            {
                HRFunc();
            }

        }

        protected void HRFunc()
        {
            try
            {
                SessionStore.SaveToCustomStore(Params.SYS_SELECT_DBNAME, "A2ZHRMCUS");
                SessionStore.SaveToCustomStore(Params.SYS_SELECT_MODULE, Enums.ModuleConstant.HumanResource);//Session["LoadSelectedModule"] = "Inventory";

                hdnModule.Value = "4";
                //CheckUserId();

                int checkUser = CheckUserId();
                switch (checkUser)
                {
                    case 3:
                        AbnormalMSG();
                        return;

                }


                A2ZSYSIDSDTO.UpdateUserHRLoginFlag(Converter.GetInteger(txtIdNo.Text), 1);

                //hdnModule.Value = "2";
                //CheckUserId();

                CashCodeDropdown();
                ddlFCashCode.SelectedValue = txtGLCashCode.Text;


                Login();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnBooth_Click(object sender, EventArgs e)
        {
            hdnModule.Value = "6";
            SODProcessFunction();
            if (lblMsgFlag.Text == "1")
            {
                ShowMessage();
            }
            else if (lblMsgFlag.Text == "2")
            {
                return;
            }
            else
            {
                lblSwFlag.Text = "0";

                if (lblSwFlag.Text == "0")
                {
                    SelectFromCashCodeSw();
                }
                else
                {
                    BoothFunc();
                }
            }

        }


        protected void BoothFunc()
        {
            try
            {
                SessionStore.SaveToCustomStore(Params.SYS_SELECT_DBNAME, "A2ZBTMCUS");
                SessionStore.SaveToCustomStore(Params.SYS_SELECT_MODULE, Enums.ModuleConstant.Booth);//Session["LoadSelectedModule"] = "Inventory";

                hdnModule.Value = "6";
                //CheckUserId();

                int checkUser = CheckUserId();
                switch (checkUser)
                {
                    case 3:
                        AbnormalMSG();
                        return;

                }


                lblCashCode.Text = txtGLCashCode.Text; //DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));


                A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
                //string date1 = dt2.ToString("dd/MM/yyyy");
                txtTranDate.Text = Converter.GetString(dt2);

                string sqlQuery = "SELECT FromCashCode,Status FROM dbo.A2ZUSERCASHCODE  WHERE IDSNO='" + txtIdNo.Text + "' AND FromCashCode='" + lblCashCode.Text + "' AND Status !=0";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlQuery, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)

                //string qry1 = "SELECT ProcessDate FROM A2ZBTRNCTRL WHERE Status!=0 AND ProcessDate='" + txtTranDate.Text + "' AND CashCodeNo='" + lblCashCode.Text + "'";
                //DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");
                //if (dt1.Rows.Count > 0)
                {
                    ProcessDoneMSG();
                    return;
                }


                A2ZSYSIDSDTO.UpdateUserBTLoginFlag(Converter.GetInteger(txtIdNo.Text), 1);

                //hdnModule.Value = "3";
                //CheckUserId();

                Login();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnOffBooth_Click(object sender, EventArgs e)
        {
            //OffBoothFunc();
        }

        //protected void OffBoothFunc()
        //{
        //    try
        //    {
        //        SessionStore.SaveToCustomStore(Params.SYS_SELECT_DBNAME, "A2ZOBTMCUS");
        //        SessionStore.SaveToCustomStore(Params.SYS_SELECT_MODULE, Enums.ModuleConstant.OffBooth);//Session["LoadSelectedModule"] = "Inventory";

        //        hdnModule.Value = "7";
        //        //CheckUserId();

        //        int checkUser = CheckUserId();
        //        switch (checkUser)
        //        {
        //            case 3:
        //                AbnormalMSG();
        //                return;

        //        }

        //        //A2ZSYSIDSDTO.UpdateUserOBTLoginFlag(Converter.GetInteger(txtIdNo.Text), 1);

        //        //hdnModule.Value = "3";
        //        //CheckUserId();

        //        Login();


        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        protected void btnExit_Click(object sender, EventArgs e)
        {
            DivMain.Attributes.CssStyle.Add("opacity", "100");
            DivLogin.Visible = false;
            txtIdNo.Text = string.Empty;
            lblWelcome.Text = string.Empty;
            Response.Redirect("A2ZERP.aspx", false);
        }

        //protected void ShowLogIn()
        //{


        //    DivLogin.Visible = true;

        //    DivMain.Attributes.CssStyle.Add("opacity", "0.0");
        //    DivMain.Attributes.CssStyle.Add("opacity", "0.0");

        //    DivLogin.Style.Add("Top", "280px");
        //    DivLogin.Style.Add("Right", "500px");
        //    DivLogin.Style.Add("position", "absolute");
        //    DivLogin.Attributes.CssStyle.Add("opacity", "100");

        //    DivLogin.Visible = true;

        //    DivChangePassword.Style.Add("Top", "280px");
        //    DivChangePassword.Style.Add("Right", "500px");
        //    DivChangePassword.Style.Add("position", "absolute");
        //    DivChangePassword.Attributes.CssStyle.Add("opacity", "100");

        //}

        //protected void ShowMessage(string message, Color clr)
        //{
        //    lblMessage.Text = message;
        //    lblMessage.ForeColor = clr;
        //    lblMessage.Visible = true;
        //    DivMessage.Visible = true;
        //    DivMain.Attributes.CssStyle.Add("opacity", "0.0");

        //    DivMessage.Style.Add("Top", "320px");
        //    DivMessage.Style.Add("Right", "500px");
        //    DivMessage.Style.Add("position", "absolute");
        //    DivMessage.Attributes.CssStyle.Add("opacity", "100");
        //}

        protected int CheckUserId()
        {
            // For Return Value of CheckUserId()
            //---------------------------------
            // 0 = Id is Available 
            // 1 = ID not in Table
            // 2 = Please Change Password - New Id was created
            // 3 = Id was not Initialize - Abnormal Logout
            //---------------------------------
            // End of For Return Value of CheckUserId()

            try
            {
                lblWelcome.Text = null;
                txtIdsLevel.Text = null;
                txtIdsFlag.Text = null; // Using for Permission
                txtOrgPass.Text = null;
                A2ZSYSIDSDTO idsDto = new A2ZSYSIDSDTO();

                if (hdnModule.Value == "1")
                {
                    string dbName = Converter.GetString("A2ZCSMCUS");
                    idsDto = A2ZSYSIDSDTO.GetUserInformation(Converter.GetInteger(txtIdNo.Text), dbName);
                }

                if (hdnModule.Value == "2")
                {
                    string dbName = Converter.GetString("A2ZGLMCUS");
                    idsDto = A2ZSYSIDSDTO.GetUserInformation(Converter.GetInteger(txtIdNo.Text), dbName);
                }
                if (hdnModule.Value == "3")
                {
                    string dbName = Converter.GetString("A2ZHKMCUS");
                    idsDto = A2ZSYSIDSDTO.GetUserInformation(Converter.GetInteger(txtIdNo.Text), dbName);
                }
                if (hdnModule.Value == "4")
                {
                    string dbName = Converter.GetString("A2ZHRMCUS");
                    idsDto = A2ZSYSIDSDTO.GetUserInformation(Converter.GetInteger(txtIdNo.Text), dbName);
                }
                if (hdnModule.Value == "5")
                {
                    string dbName = Converter.GetString("A2ZSTMCUS");
                    idsDto = A2ZSYSIDSDTO.GetUserInformation(Converter.GetInteger(txtIdNo.Text), dbName);
                }
                if (hdnModule.Value == "6")
                {
                    string dbName = Converter.GetString("A2ZBTMCUS");
                    idsDto = A2ZSYSIDSDTO.GetUserInformation(Converter.GetInteger(txtIdNo.Text), dbName);
                }
                if (hdnModule.Value == "7")
                {
                    string dbName = Converter.GetString("A2ZOBTMCUS");
                    idsDto = A2ZSYSIDSDTO.GetUserInformation(Converter.GetInteger(txtIdNo.Text), dbName);
                }
                if (idsDto.IdsNo == 0)
                {
                    lblWelcome.Text = "ID Not Found";
                    lblWelcome.ForeColor = Color.Red;
                    DivLogin.Visible = true;
                    btChangePassword.Visible = false;
                    return 1;
                }


                //IdsLogInFlag 
                lblWelcome.Text = idsDto.IdsName;

                Session["UserName"] = lblWelcome.Text;

                txtIdsLevel.Text = Converter.GetString(idsDto.IdsLevel);

                Session["USERLAVEL"] = txtIdsLevel.Text;

                txtIdsFlag.Text = Converter.GetString(idsDto.IdsFlag); // Using for Permission
                txtOrgPass.Text = idsDto.IdsPass;

                if (hdnModule.Value != "6")
                {
                    txtGLCashCode.Text = Converter.GetString(idsDto.GLCashCode);
                }

                txtUserEmpCode.Text = Converter.GetString(idsDto.EmpCode);
                txtServerIP.Text = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
                txtServerName.Text = System.Net.Dns.GetHostName();
                txtUserIP.Text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                lblWelcome.ForeColor = Color.Green;
                DivLogin.Visible = true;
                btChangePassword.Visible = true;
                btChangePassword.Visible = true;


                if (idsDto.IdsPass == "XXXXXXXX")
                {
                    lblWelcome.ForeColor = Color.Red;
                    DivLogin.Visible = true;
                    return 2;
                }

                if (idsDto.IdsLogInFlag == 1)
                {
                    return 3;
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void AbnormalMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('User Id is using by other client or Abnormal Logout');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }

        protected void ProcessDoneMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Current Date Process Done');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserRegistry.aspx", true);
        }

        protected void AddAuditInformation()
        {
            Int16 moduleNo = 0;

            Enums.ModuleConstant masterPage = Enums.ModuleConstant.HouseKeeping;
            if (SessionStore.ContainsKey(Params.SYS_SELECT_MODULE))//if (Session["LoadSelectedModule"] != null)
            {
                masterPage = (Enums.ModuleConstant)SessionStore.GetValue(Params.SYS_SELECT_MODULE);//Session["LoadSelectedModule"].ToString();
            }


            switch (masterPage)
            {
                case Enums.ModuleConstant.CustomerService:
                    moduleNo = 1;
                    break;

                case Enums.ModuleConstant.GeneralLedger:
                    moduleNo = 2;
                    break;
                case Enums.ModuleConstant.HouseKeeping:
                    moduleNo = 3;
                    break;
                case Enums.ModuleConstant.HumanResource:
                    moduleNo = 4;
                    break;
                case Enums.ModuleConstant.Inventory:
                    moduleNo = 5;
                    break;
                case Enums.ModuleConstant.Booth:
                    moduleNo = 6;
                    break;
                //case Enums.ModuleConstant.OffBooth:
                //    moduleNo = 7;
                //    break;
                default:
                    moduleNo = 1;
                    break;
            }

            var dto = new A2ZAUDITDTO();

            dto.UserId = Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_ID));
            dto.EmpCode = Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_EMP_CODE));
            dto.UserIP = Converter.GetString(SessionStore.GetValue(Params.SYS_USER_IP));
            dto.UserServerIP = Converter.GetString(SessionStore.GetValue(Params.SYS_USER_SERVER_IP));
            dto.UserServerName = Converter.GetString(SessionStore.GetValue(Params.SYS_USER_SERVER_NAME));


            dto.AudRecordNo = 1;
            dto.ModuleNo = moduleNo;
            dto.AudRemarks = "Log In";
            dto.AudProcessDate = DateTime.Now.Date;
            dto.AudOldDate = DateTime.Now.Date;
            dto.AudNewDate = DateTime.Now.Date;

            A2ZAUDITDTO.InsertAuditInformation(dto);
        }



        protected void Login()
        {
            try
            {
                string dbName = Converter.GetString(SessionStore.GetValue(Params.SYS_SELECT_DBNAME));
                if (hdnModule.Value == "1")
                {
                    A2ZSYSIDSDTO.UpdateUserCSLoginFlag(Converter.GetInteger(txtIdNo.Text), 1);
                }

                if (hdnModule.Value == "2")
                {
                    A2ZSYSIDSDTO.UpdateUserGLLoginFlag(Converter.GetInteger(txtIdNo.Text), 1);
                }
                if (hdnModule.Value == "3")
                {
                    A2ZSYSIDSDTO.UpdateUserHKLoginFlag(Converter.GetInteger(txtIdNo.Text), 1);
                }
                if (hdnModule.Value == "5")
                {
                    A2ZSYSIDSDTO.UpdateUserSTLoginFlag(Converter.GetInteger(txtIdNo.Text), 1);
                }
                if (hdnModule.Value == "6")
                {
                    A2ZSYSIDSDTO.UpdateUserBTLoginFlag(Converter.GetInteger(txtIdNo.Text), 1);
                }
                //if (hdnModule.Value == "7")
                //{
                //    A2ZSYSIDSDTO.UpdateUserOBTLoginFlag(Converter.GetInteger(txtIdNo.Text), 1);
                //}
                SessionStore.SaveToCustomStore(Params.SYS_USER_ID, txtIdNo.Text);
                SessionStore.SaveToCustomStore(Params.SYS_USER_NAME, lblWelcome.Text);
                SessionStore.SaveToCustomStore(Params.SYS_USER_LEVEL, txtIdsLevel.Text);
                SessionStore.SaveToCustomStore(Params.SYS_USER_PERMISSION, txtIdsFlag.Text);
                SessionStore.SaveToCustomStore(Params.SYS_USER_EMP_CODE, txtUserEmpCode.Text);
                SessionStore.SaveToCustomStore(Params.SYS_USER_SERVER_IP, txtServerIP.Text);
                SessionStore.SaveToCustomStore(Params.SYS_USER_SERVER_NAME, txtServerName.Text);
                SessionStore.SaveToCustomStore(Params.SYS_USER_IP, txtUserIP.Text);
                SessionStore.SaveToCustomStore(Params.SYS_USER_GLCASHCODE, ddlFCashCode.SelectedValue);

                AddAuditInformation();

                Response.Redirect("A2ZERPModule.aspx", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZStartUp.aspx", false);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DivMain.Attributes.CssStyle.Add("opacity", "100");

            DivDetails.Visible = false;
            DivMain.Visible = true;

            unlockbutton();
        }

        protected void linkSMSReload_Click(object sender, EventArgs e)
        {
            GetSMSMsg();
        }

        protected void BtnProceed_Click(object sender, EventArgs e)
        {
            //SessionStore.SaveToCustomStore(Params.SYS_USER_CURRENCY_CODE, ddlFCurrency.SelectedValue);
            //SessionStore.SaveToCustomStore(Params.SYS_USER_CURRENCY_NAME, ddlFCurrency.SelectedItem.Text);

            if (ddlFCashCode.SelectedValue != "-Select-")
            {
                SessionStore.SaveToCustomStore(Params.SYS_USER_GLCASHCODE, ddlFCashCode.SelectedValue);

                txtGLCashCode.Text = ddlFCashCode.SelectedValue;

                ModuleFunction();
                return;
            }
            else
            {
                DivFromCashCode.Visible = true;
                ddlFCashCode.Focus();

            }
        }

        protected void BtnBack1_Click(object sender, EventArgs e)
        {
            if (lblNoRec.Text == "1")
            {
                Response.Redirect("A2ZStartUp.aspx", false);
            }
            else
            {
                DivMain.Attributes.CssStyle.Add("opacity", "100");

                DivFromCashCode.Visible = false;
                DivMain.Visible = true;

                unlockbutton();
            }

        }

        protected void SelectFromCashCodeSw()
        {
            if (hdnModule.Value == "1")
            {
                lblModuleFunc.Text = "Customer Service Module";
            }
            else
                if (hdnModule.Value == "2")
                {
                    lblModuleFunc.Text = "General Ledger Module";
                }
                else
                    if (hdnModule.Value == "5")
                    {
                        lblModuleFunc.Text = "Inventory Module";
                    }
                    else
                        if (hdnModule.Value == "6")
                        {
                            lblModuleFunc.Text = "Booth Module";
                        }

            DivFromCashCode.Visible = true;
            CashCodeDropdown();

            string sqlQuery = "SELECT FromCashCode,Status FROM dbo.A2ZUSERCASHCODE  WHERE IDSNO='" + txtIdNo.Text + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlQuery, "A2ZCSMCUS");
            if (dt.Rows.Count > 1)
            {
                DivMain.Attributes.CssStyle.Add("opacity", "0.0");
                DivFromCashCode.Style.Add("Top", "200px");
                DivFromCashCode.Style.Add("Right", "300px");
                DivFromCashCode.Style.Add("position", "fixed");
                DivFromCashCode.Attributes.CssStyle.Add("opacity", "200");
                DivFromCashCode.Attributes.CssStyle.Add("z-index", "200");

                lockbutton();
            }
            else
            {

                lblAccessStatus.Text = Converter.GetString(dt.Rows[0]["Status"]);

                ddlFCashCode.SelectedValue = Converter.GetString(dt.Rows[0]["FromCashCode"]);
                SessionStore.SaveToCustomStore(Params.SYS_USER_GLCASHCODE, ddlFCashCode.SelectedValue);

                txtGLCashCode.Text = ddlFCashCode.SelectedValue;

                ModuleFunction();
                return;
            }

        }

        protected void CashCodeDropdown()
        {
            string sqlquery = @"SELECT FromCashCode,+ CAST (FromCashCode AS VARCHAR(100))+ '-' + LTRIM(RTRIM(FromCashCodeDesc)) from A2ZUSERCASHCODE WHERE IdsNo = '" + txtIdNo.Text + "'";
            //string sqlquery = "SELECT FromCashCode,FromCashCodeDesc from A2ZUSERCASHCODE WHERE IdsNo = '" + txtIdNo.Text + "'";
            ddlFCashCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlFCashCode, "A2ZCSMCUS");

        }

        protected void BtnChg_Click(object sender, EventArgs e)
        {
            ShowMessage();

            if (lblChgProcDt.Visible == false)
            {
                lblChgProcDt.Visible = true;
                txtChgProcDt.Visible = true;
                txtStDay.Focus();
            }
            else
            {
                lblChgProcDt.Visible = false;
                txtChgProcDt.Visible = false;
            }
        }

    }
}
