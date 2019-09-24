using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.DTO.HouseKeeping;
using System.Drawing;

namespace ATOZWEBMCUS.Pages
{
    public partial class A2ZERPUserSMSSent : System.Web.UI.Page
    {
        protected Int32 userPermission;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {

                    CtrlModule.Text = Request.QueryString["a%b"];
                    
               
                    txtsmsMsg.Visible = false;

                    txtsmsMsg.ReadOnly = true;


                    btnBack.Visible = false;


                    //A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    //DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    //string date = dt.ToString("dd/MM/yyyy");
                    //txtfdate.Text = Converter.GetString(date);
                    //txttdate.Text = Converter.GetString(date);
                    //lblProcDate.Text = Converter.GetString(date);


                    lblUserId.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    lblUserName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));

                    hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                    if (CtrlModule.Text == "1")
                    {
                        string CheckQuery = "SELECT SMSDate,SMSNo,SMSToIdsNo,SMSToIdsName,SMSNote from A2ZUSERSMS WHERE  SMSFromIdsNo='" + lblUserId.Text + "' AND SMSStatus=1 OR SMSStatus=2";
                        DataTable dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZHKMCUS");
                        if (dt.Rows.Count == 1)
                        {
                            SingleRec.Text = "1";
                        }
                    }
                    else 
                    {
                        string CheckQuery = "SELECT SMSDate,SMSNo,SMSToIdsNo,SMSToIdsName,SMSNote from A2ZUSERSMS WHERE  SMSFromIdsNo='" + lblUserId.Text + "' AND SMSStatus=3 OR SMSStatus=4";
                        DataTable dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZHKMCUS");
                        if (dt.Rows.Count == 1)
                        {
                            SingleRec.Text = "1";
                        }
                    
                    }

                    if (CtrlModule.Text == "1")
                    {
                        gvSMS1();
                    }
                    else 
                    {
                        gvSMS6();
                    }


                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }



        private void gvSMS1()
        {
            string sqlquery3 = "SELECT SMSDate,SMSNo,SMSToIdsNo,SMSToIdsName,SMSNote,SMSStatus from A2ZUSERSMS WHERE  SMSFromIdsNo='" + lblUserId.Text + "' AND SMSStatus=1 OR SMSStatus=2";
            gvSMSInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvSMSInfo, "A2ZHKMCUS");
        }

        private void gvSMS6()
        {
            string sqlquery3 = "SELECT SMSDate,SMSNo,SMSToIdsNo,SMSToIdsName,SMSNote,SMSStatus from A2ZUSERSMS WHERE  SMSFromIdsNo='" + lblUserId.Text + "' AND SMSStatus=3 OR SMSStatus=4";
            gvSMSInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvSMSInfo, "A2ZHKMCUS");
        }





        protected void BtnBack_Click(object sender, EventArgs e)
        {
            txtsmsMsg.Visible = false;
            btnBack.Visible = false;
        }



        protected void BtnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;
                Label smsNo = (Label)gvSMSInfo.Rows[r.RowIndex].Cells[2].FindControl("lblSMSNo");
                Label smsNote = (Label)gvSMSInfo.Rows[r.RowIndex].Cells[4].FindControl("lblSMSNote");
                Label smsStatus = (Label)gvSMSInfo.Rows[r.RowIndex].Cells[5].FindControl("lblSMSStatus");

                if (SingleRec.Text != "1")
                {
                    if (Session["PreviousRowIndex"] != null)
                    {
                        var previousRowIndex = (int)Session["PreviousRowIndex"];
                        GridViewRow PreviousRow = gvSMSInfo.Rows[previousRowIndex];
                        PreviousRow.ForeColor = Color.Black;
                    }

                    GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
                    row.ForeColor = Color.Blue;
                    Session["PreviousRowIndex"] = row.RowIndex;
                }


                int a = Converter.GetInteger(smsNo.Text);
                lblsmsNo.Text = Converter.GetString(a);

                
                txtsmsMsg.Text = smsNote.Text;

                txtsmsMsg.Visible = true;
                btnBack.Visible = true;


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }


        //protected void BtnProceed_Click(object sender, EventArgs e)
        //{
        //    if (CtrlModule.Text == "1")
        //    {
        //        string CheckQuery = "SELECT SMSDate,SMSNo,SMSToIdsNo,SMSToIdsName,SMSNote from A2ZUSERSMS WHERE SMSFromIdsNo='" + lblUserId.Text + "' AND SMSStatus=1 OR SMSStatus=2";
        //        DataTable dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZHKMCUS");
        //        if (dt.Rows.Count == 1)
        //        {
        //            SingleRec.Text = "1";
        //        }
        //    }
        //    else
        //    {
        //        string CheckQuery = "SELECT SMSDate,SMSNo,SMSToIdsNo,SMSToIdsName,SMSNote from A2ZUSERSMS WHERE  SMSFromIdsNo='" + lblUserId.Text + "' AND SMSStatus=3 OR SMSStatus=4";
        //        DataTable dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZHKMCUS");
        //        if (dt.Rows.Count == 1)
        //        {
        //            SingleRec.Text = "1";
        //        }

        //    }

        //    if (CtrlModule.Text == "1")
        //    {
        //        gvSMS1();
        //    }
        //    else
        //    {
        //        gvSMS6();
        //    }
        //}
    }

}