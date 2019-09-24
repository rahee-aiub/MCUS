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
    public partial class HREmpLeaveVerify : System.Web.UI.Page
    {
        protected Int32 userPermission;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                  

                    btnApproved.Visible = false;
                    btnCanApproved.Visible = false;


                    DivReject.Visible = false;


                    lblmsg1.Visible = false;
                    lblmsg2.Visible = false;


                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    lblProcDate.Text = date;

                    lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    userPermission = Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION));


                    hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));

                    //if (userPermission != 30)
                    //{
                    //    string notifyMsg = "?txtOne=" + Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME)) +
                    //                       "&txtTwo=" + "You Don't Have Permission for Approve" +
                    //                       "&txtThree=" + "Contact Your Super User" + "&PreviousMenu=A2ZERPModule.aspx";
                    //    Server.Transfer("Notify.aspx" + notifyMsg);
                    //}
                    //else
                    {
                        DataTable dt1 = new DataTable();


                        string CheckQuery = "SELECT EmpCode,A2ZEMPLEAVETYPE.EmpleaveName,LStartDate,LEndDate,LApplyDays,LBalance from A2ZEMPLEAVE inner join A2ZEMPLEAVETYPE on A2ZEMPLEAVETYPE.EmpleaveCode=A2ZEMPLEAVE.EmpleaveCode where LProcStat='2'";
                        dt1 = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZHRMCUS");


                        if (dt1.Rows.Count == 1)
                        {
                            SingleRec.Text = "1";
                        }


                        if (dt1.Rows.Count <= 0)
                        {
                            string notifyMsg = "?txtOne=" + Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME)) +
                                               "&txtTwo=" + "No Record Found for Verify" +
                                               "&txtThree=" + "Contact For Record" + "&PreviousMenu=A2ZERPModule.aspx";
                            Server.Transfer("Notify.aspx" + notifyMsg);

                        }

                        gv1Verify();

                    }

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }

        private void gv1Verify()
        {
            string CheckQuery = "SELECT A2ZEMPLEAVE.Id,A2ZEMPLEAVE.EmpCode,A2ZEMPLEAVETYPE.EmpleaveName,A2ZEMPLEAVE.LStartDate,A2ZEMPLEAVE.LEndDate,A2ZEMPLEAVE.LApplyDays,A2ZEMPLEAVE.LBalance from A2ZEMPLEAVE inner join A2ZEMPLEAVETYPE on A2ZEMPLEAVETYPE.EmpleaveCode=A2ZEMPLEAVE.EmpleaveCode where A2ZEMPLEAVE.LProcStat='2'";
            gvLeaveInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(CheckQuery, gvLeaveInfo, "A2ZHRMCUS");
        }


        protected void BtnPrint_Click(object sender, EventArgs e)
        {

            try
            {
                if (btnApproved.Visible == false && BtnReject.Visible == false)
                {

                    Button c = (Button)sender;
                    GridViewRow r = (GridViewRow)c.NamingContainer;
                    Label AppNo = (Label)gvLeaveInfo.Rows[r.RowIndex].Cells[8].FindControl("lblappno");
                    int ap = Converter.GetInteger(AppNo.Text);

                    var p = A2ZERPSYSPRMDTO.GetParameterValue();
                    string comName = p.PrmUnitName;
                    string comAddress1 = p.PrmUnitAdd1;
                    SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                    SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, ap);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptApproveLoanReport");

                    Response.Redirect("ReportServer.aspx", false);

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnPrint_Click Problem');</script>");
                //throw ex;
            }
        }

       

      

        protected void BtnApproved_Click(object sender, EventArgs e)
        {
            try
            {


                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                Int16 VerifyBy = Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_ID));
                DateTime VerifyByDate = Converter.GetDateTime(dto.ProcessDate);


                string CheckUp = "UPDATE A2ZEMPLEAVE SET LProcStat=3,VerifyBy = '" + VerifyBy + "',VerifyByDate = '" + VerifyByDate + "' where Id='" + CtrlId.Text + "'";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(CheckUp, "A2ZHRMCUS"));
                if (rowEffect > 0)
                {

                  

                    DataTable dt = new DataTable();

                    gv1Verify();
                    string CheckQuery = "SELECT EmpCode from A2ZEMPLEAVE where LProcStat='2'";
                    dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZHRMCUS");




                    btnApproved.Visible = false;
                    btnCanApproved.Visible = false;



                    if (dt.Rows.Count == 1)
                    {
                        SingleRec.Text = "1";
                    }

                    if (dt.Rows.Count <= 0)
                    {
                        DivGridViewCancle.Visible = false;
                        lblmsg1.Visible = true;
                        lblmsg2.Visible = true;
                    }

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnApprove_Click Problem');</script>");
                //throw ex;
            }

        }
        protected void BtnReject_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                Int16 RejectBy = Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_ID));
                DateTime RejectByDate = Converter.GetDateTime(dto.ProcessDate);


                string CheckUp = "UPDATE A2ZEMPLEAVE SET LProcStat=99,LRejectNote = '" + txtRejectNote.Text + "',RejectBy = '" + RejectBy + "',RejectByDate = '" + RejectByDate + "' where Id='" + CtrlId.Text + "'";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(CheckUp, "A2ZHRMCUS"));
                if (rowEffect > 0)
                {

                    DataTable dt = new DataTable();

                    gv1Verify();
                    string CheckQuery = "SELECT EmpCode from A2ZEMPLEAVE where LProcStat='2'";
                    dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZHRMCUS");


                    if (dt.Rows.Count == 1)
                    {
                        SingleRec.Text = "1";
                    }

                    if (dt.Rows.Count <= 0)
                    {
                        DivGridViewCancle.Visible = false;
                        lblmsg1.Visible = true;
                        lblmsg2.Visible = true;
                    }

                    DivReject.Visible = false;
                    BtnReject.Visible = false;



                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnReject_Click Problem');</script>");
                //throw ex;
            }

        }



        protected void BtnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnApproved.Visible == false && BtnReject.Visible == false)
                {


                    Div1.Visible = true;
                    btnApproved.Visible = true;
                    btnCanApproved.Visible = true;


                    Button b = (Button)sender;
                    GridViewRow r = (GridViewRow)b.NamingContainer;
                    Label Id = (Label)gvLeaveInfo.Rows[r.RowIndex].Cells[0].FindControl("lblId");


                    CtrlId.Text = Id.Text;

                    if (SingleRec.Text != "1")
                    {
                        if (Session["PreviousRowIndex"] != null)
                        {
                            var previousRowIndex = (int)Session["PreviousRowIndex"];
                            GridViewRow PreviousRow = gvLeaveInfo.Rows[previousRowIndex];
                            PreviousRow.ForeColor = Color.Black;
                        }

                        GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
                        row.ForeColor = Color.Blue;
                        Session["PreviousRowIndex"] = row.RowIndex;
                    }

                    btnApproved.Visible = true;
                    btnCanApproved.Visible = true;



                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

        }


        protected void BtnRejectSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnApproved.Visible == false && BtnReject.Visible == false)
                {
                    Button b = (Button)sender;
                    GridViewRow r = (GridViewRow)b.NamingContainer;
                    Label Id = (Label)gvLeaveInfo.Rows[r.RowIndex].Cells[0].FindControl("lblId");

                    CtrlId.Text = Id.Text;

                    if (SingleRec.Text != "1")
                    {
                        if (Session["PreviousRowIndex"] != null)
                        {
                            var previousRowIndex = (int)Session["PreviousRowIndex"];
                            GridViewRow PreviousRow = gvLeaveInfo.Rows[previousRowIndex];
                            PreviousRow.ForeColor = Color.Black;
                        }

                        GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
                        row.ForeColor = Color.Blue;
                        Session["PreviousRowIndex"] = row.RowIndex;
                    }

                    DivReject.Visible = true;
                    BtnReject.Visible = true;
                    txtRejectNote.Focus();
                }
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


        protected void BtnCanReject_Click(object sender, EventArgs e)
        {
            DivReject.Visible = false;
        }

        protected void BtnCanApproved_Click(object sender, EventArgs e)
        {

            Div1.Visible = false;
        }

    }

}