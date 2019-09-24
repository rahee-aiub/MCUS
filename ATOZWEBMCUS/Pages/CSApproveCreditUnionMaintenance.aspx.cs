using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.Utility;
using DataAccessLayer.Constants;
using ATOZWEBMCUS.WebSessionStore;
using System.Data;
using DataAccessLayer.BLL;
using System.Data.SqlClient;

using DataAccessLayer.DTO.HouseKeeping;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSApproveCreditUnionMaintenance : System.Web.UI.Page
    {
        protected Int32 userPermission;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    lblmsg1.Visible = false;
                    lblmsg2.Visible = false;
                    userPermission = Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION));

                    //if (userPermission != 30)
                    //{
                    //    string notifyMsg = "?txtOne=" + Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME)) +
                    //                       "&txtTwo=" + "You Don't Have Permission for Approve" +
                    //                       "&txtThree=" + "Contact Your Super User" + "&PreviousMenu=A2ZERPModule.aspx";
                    //    Server.Transfer("Notify.aspx" + notifyMsg);
                    //}
                    //else
                    {

                        string CheckQuery = "SELECT CuType,CuTypeName,CuNo,CuName,CuOpDt,CuProcDesc FROM A2ZCUAPPLICATION Where CuProcFlag = '12' OR CuProcFlag = '22' and CuStatus !='99'";
                        DataTable dt = new DataTable();
                        dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");

                        if (dt.Rows.Count <= 0)
                        {
                            string notifyMsg = "?txtOne=" + Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME)) +
                                               "&txtTwo=" + "No Record Found for Approve" +
                                               "&txtThree=" + "Contact For Record" + "&PreviousMenu=A2ZERPModule.aspx";
                            Server.Transfer("Notify.aspx" + notifyMsg);

                        }
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


        protected void gvVerify()
        {
            string sqlquery3 = "SELECT CuType,CuTypeName,CuNo,CuName,CuOpDt,CuProcDesc from A2ZCUAPPLICATION where CuProcFlag = '12' OR CuProcFlag = '22' and CuStatus !='99'";
            gvCUInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvCUInfo, "A2ZCSMCUS");
        }

        protected void BtnPrint_Click(object sender, EventArgs e)
        {

            try
            {
                Button c = (Button)sender;
                GridViewRow r = (GridViewRow)c.NamingContainer;
                Label lblcutype = (Label)gvCUInfo.Rows[r.RowIndex].Cells[5].FindControl("lblcutype");
                Label CrNo = (Label)gvCUInfo.Rows[r.RowIndex].Cells[6].FindControl("lblcno");
                int cutype = Converter.GetSmallInteger(lblcutype.Text);
                int cu = Converter.GetInteger(CrNo.Text);
                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, cutype);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, cu);
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

        protected void BtnApprove_Click(object sender, EventArgs e)
        {
            try
            {

                A2ZRECCTRLDTO getDTO = (A2ZRECCTRLDTO.GetLastRecords(1));
                lblNewSRL.Text = Converter.GetString(getDTO.CtrlRecLastNo);

                A2ZCUNIONDTO MemDTO = new A2ZCUNIONDTO();
                A2ZMEMBERDTO Mem1DTO = new A2ZMEMBERDTO();
                A2ZACCOUNTDTO Mem2DTO = new A2ZACCOUNTDTO();


                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;
                Label CTyNo = (Label)gvCUInfo.Rows[r.RowIndex].Cells[5].FindControl("lblcutype");
                Label CrNo = (Label)gvCUInfo.Rows[r.RowIndex].Cells[6].FindControl("lblcno");
                int a = Converter.GetSmallInteger(CTyNo.Text);
                int c = Converter.GetSmallInteger(CrNo.Text);

                Int16 CuType = Converter.GetSmallInteger(a);
                int CNo = Converter.GetSmallInteger(c);

                lblCuType.Text = Converter.GetString(CuType);
                lblCuNo.Text = Converter.GetString(CNo);

                A2ZCUAPPLICATIONDTO objDTO = (A2ZCUAPPLICATIONDTO.GetInformation(CuType, CNo));

                if (objDTO.CreditUnionNo > 0)
                {

                    objDTO.CuType = CuType;
                    objDTO.CreditUnionNo = CNo;
                    
                    MemDTO.CuType = Converter.GetSmallInteger(objDTO.CuType);
                    MemDTO.CuTypeName = Converter.GetString(objDTO.CuTypeName);
                    MemDTO.CreditUnionNo = Converter.GetInteger(lblNewSRL.Text);
                    MemDTO.CreditUnionName = Converter.GetString(objDTO.CreditUnionName);
                    MemDTO.opendate = Converter.GetDateTime(objDTO.opendate);
                    MemDTO.MemberFlag = Converter.GetSmallInteger(objDTO.MemberFlag);
                    MemDTO.CertificateNo = Converter.GetInteger(objDTO.CertificateNo);

                    MemDTO.AddressL1 = Converter.GetString(objDTO.AddressL1);
                    MemDTO.AddressL2 = Converter.GetString(objDTO.AddressL2);
                    MemDTO.AddressL3 = Converter.GetString(objDTO.AddressL3);
                    MemDTO.TelephoneNo = Converter.GetString(objDTO.TelephoneNo);
                    MemDTO.MobileNo = Converter.GetString(objDTO.MobileNo);
                    MemDTO.Fax = Converter.GetString(objDTO.Fax);
                    MemDTO.email = Converter.GetString(objDTO.email);
                    MemDTO.Division = Converter.GetInteger(objDTO.Division);
                    MemDTO.District = Converter.GetInteger(objDTO.District);
                    MemDTO.Upzila = Converter.GetInteger(objDTO.Upzila);
                    MemDTO.Thana = Converter.GetInteger(objDTO.Thana);
                    MemDTO.GLCashCode = Converter.GetInteger(objDTO.GLCashCode);


                    MemDTO.CuStatusDate = Converter.GetDateTime(objDTO.CuStatusDate);
                    MemDTO.ValueDate = Converter.GetDateTime(objDTO.ValueDate);
                    MemDTO.CreateDate = Converter.GetDateTime(objDTO.CreateDate);

                    objDTO.CuProcFlag = Converter.GetSmallInteger(13);
                    objDTO.CuProcDesc = "Approved";



                    objDTO.ApprovBy = Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_ID));
                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    objDTO.ApprovByDate = Converter.GetDateTime(dto.ProcessDate);

                    MemDTO.CuProcFlag = Converter.GetSmallInteger(objDTO.CuProcFlag);
                    MemDTO.ApprovBy = Converter.GetSmallInteger(objDTO.ApprovBy);
                    MemDTO.ApprovByDate = Converter.GetDateTime(objDTO.ApprovByDate);

                    MemDTO.InputBy = Converter.GetSmallInteger(objDTO.InputBy);
                    MemDTO.VerifyBy = Converter.GetSmallInteger(objDTO.VerifyBy);
                    MemDTO.InputByDate = Converter.GetDateTime(objDTO.InputByDate);
                    MemDTO.VerifyByDate = Converter.GetDateTime(objDTO.VerifyByDate);

                    
                    objDTO.VerifyBy = Converter.GetSmallInteger(MemDTO.VerifyBy);
                    objDTO.VerifyByDate = Converter.GetDateTime(MemDTO.VerifyByDate);


                    Mem1DTO.CuType = Converter.GetSmallInteger(MemDTO.CuType);
                    Mem1DTO.CreditUnionNo = Converter.GetInteger(MemDTO.CreditUnionNo);
                    Mem1DTO.MemberName = Converter.GetString(MemDTO.CreditUnionName);
                    Mem1DTO.OpenDate = Converter.GetDateTime(MemDTO.opendate);
                    Mem1DTO.MemType = 1;

                    Mem2DTO.CuType = Converter.GetSmallInteger(MemDTO.CuType);
                    Mem2DTO.CuNo = Converter.GetInteger(MemDTO.CreditUnionNo);
                    Mem2DTO.AccType = 99;
                    Mem2DTO.AccNo = 0;
                    Mem2DTO.MemberNo = 0;
                    Mem2DTO.Opendate = Converter.GetDateTime(MemDTO.opendate);
                    Mem2DTO.AccStatus = 1;
                    Mem2DTO.AccAtyClass = 6;

                    int roweffect = A2ZCUAPPLICATIONDTO.UpdateInformation2(objDTO);
                    if (roweffect > 0)
                    {
                        int row = A2ZCUNIONDTO.InsertInformation(MemDTO);
                        int row1 = A2ZMEMBERDTO.Insert(Mem1DTO);
                        InsertMiscRecord();
                        //int row2 = A2ZACCOUNTDTO.Insert(Mem2DTO);

                        DisplayMessage();
                        gvVerify();
                        string CheckQuery = "SELECT CuType,CuTypeName,CuNo,CuName,CuOpDt,CuProcDesc FROM A2ZCUAPPLICATION Where CuProcFlag = '12' OR CuProcFlag = '22' and CuStatus !='99'";
                        DataTable dt = new DataTable();
                        dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");

                        if (dt.Rows.Count <= 0)
                        {
                            DivGridViewCancle.Visible = false;
                            lblmsg1.Visible = true;
                            lblmsg2.Visible = true;

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnApprove_Click Problem');</script>");
                //throw ex;
            }

        }

        private void InsertMiscRecord()
        {
            
                var prm = new object[2];

                prm[0] = lblCuType.Text;
                prm[1] = lblCuNo.Text;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSInsertMiscellaneousAccount", prm, "A2ZCSMCUS"));
                if (result == 0)
                {

                }
            


        }
        protected void BtnReVerify_Click(object sender, EventArgs e)
        {

            try
            {


                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;
                Label CTyNo = (Label)gvCUInfo.Rows[r.RowIndex].Cells[5].FindControl("lblcutype");
                Label CrNo = (Label)gvCUInfo.Rows[r.RowIndex].Cells[6].FindControl("lblcno");
                int a = Converter.GetSmallInteger(CTyNo.Text);
                int c = Converter.GetSmallInteger(CrNo.Text);

                Int16 CuType = Converter.GetSmallInteger(a);
                int CNo = Converter.GetSmallInteger(c);

                string CheckUp = "UPDATE A2ZCUAPPLICATION SET CuProcFlag=31,CuProcDesc='ReVerify' where CuType='" + CuType + "' and CuNo='" + CNo + "'";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(CheckUp, "A2ZCSMCUS"));
                if (rowEffect > 0)
                {
                    gvVerify();
                    string CheckQuery = "SELECT CuType,CuTypeName,CuNo,CuName,CuOpDt,CuProcDesc FROM A2ZCUAPPLICATION Where CuProcFlag = '12' OR CuProcFlag = '22' and CuStatus !='99'";
                    DataTable dt = new DataTable();
                    dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");

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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnReVerify_Click Problem');</script>");
                //throw ex;
            }


        }

        protected void BtnReject_Click(object sender, EventArgs e)
        {
            try
            {

                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;
                Label CTyNo = (Label)gvCUInfo.Rows[r.RowIndex].Cells[5].FindControl("lblcutype");
                Label CrNo = (Label)gvCUInfo.Rows[r.RowIndex].Cells[6].FindControl("lblcno");
                int a = Converter.GetSmallInteger(CTyNo.Text);
                int c = Converter.GetSmallInteger(CrNo.Text);

                Int16 CuType = Converter.GetSmallInteger(a);
                int CNo = Converter.GetSmallInteger(c);

                string CheckUp = "UPDATE A2ZCUAPPLICATION SET CuProcFlag = '0',CuStatus=99 where CuType='" + CuType + "' and CuNo='" + CNo + "'";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(CheckUp, "A2ZCSMCUS"));
                if (rowEffect > 0)
                {
                    gvVerify();
                    string CheckQuery = "SELECT CuType,CuTypeName,CuNo,CuName,CuOpDt,CuProcDesc FROM A2ZCUAPPLICATION Where CuProcFlag = '12' OR CuProcFlag = '22' and CuStatus !='99'";
                    DataTable dt = new DataTable();
                    dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");

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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnReject_Click Problem');</script>");
                //throw ex;
            }

        }
        private void Successful()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Approve successfully completed.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Approve Successfully Completed');", true);
            return;

        }

        protected void DisplayMessage()
        {
            lblCUNoMsg.Text = Converter.GetString(lblNewSRL.Text);
            lblCUTypeMsg.Text = Converter.GetString(1);
            
            string Msg = "";

            string c = "";
            string d = "";
            string e = "";
            string a = "";
            string X = "";
            string b = "";


            c = "Generated New Credit Union No.";
            d = "Credit Union No.:";
            e = "Credit Union Type : Affilate Depositor";
            a = string.Format(lblCUTypeMsg.Text);
            X = "-";
            b = string.Format(lblCUNoMsg.Text);

            Msg += c;
            Msg += "\\n";
            Msg += e;
            Msg += "\\n";
            Msg += d;
            Msg += a;
            Msg += X;
            Msg += b;


            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;
            
            
            
            //-------------
            //lblCUNoMsg.Text = Converter.GetString(lblNewSRL.Text);
            //lblCUTypeMsg.Text = Converter.GetString(1);
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string c = "Generated New Credit Union No";
            //string d = "Credit Union No:";
            //string a = string.Format(lblCUTypeMsg.Text);
            //string X = "-";
            //string b = string.Format(lblCUNoMsg.Text);
            //if (a == "1")
            //{
            //    sb.Append("<script type = 'text/javascript'>");
            //    sb.Append("window.onload=function(){");
            //    sb.Append("alert('");
            //    sb.Append(c);
            //    sb.Append("\\n");
            //    sb.Append("Credit Union Type : Affilate Member");
            //    sb.Append("\\n");
            //    sb.Append(d);
            //    sb.Append(a);
            //    sb.Append(X);
            //    sb.Append(b);
            //    sb.Append("')};");
            //    sb.Append("</script>");
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
            //}
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }


    }
}