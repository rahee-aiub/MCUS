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

namespace ATOZWEBMCUS.Pages
{
    public partial class CSAccountCertificateMaint : System.Web.UI.Page
    {
        protected Int32 userPermission;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    dropdown();
                    lblmsg1.Visible = false;
                    lblmsg2.Visible = false;
                    userPermission = Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION));

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }

        }

        private void dropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE WHERE AccCertNo=1";
            ddlAcType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAcType, "A2ZCSMCUS");
        }

        private void gvVerify()
        {
            string sqlquery3 = "SELECT CuType,CuNo,MemNo,AccType,AccNo,AccOldNumber,MemName,AccOpenDate,AccPeriod,AccOrgAmt,REPLACE(LTRIM(RTRIM(AccCertNo)), '  ', ' ') AS AccCertNo from WFCSCERTIFICATENO WHERE Status=0";
            gvCertiDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvCertiDetailInfo, "A2ZCSMCUS");
        }


        protected void BtnPrint_Click(object sender, EventArgs e)
        {

            try
            {
                Button c = (Button)sender;
                GridViewRow r = (GridViewRow)c.NamingContainer;
                Label AppNo = (Label)gvCertiDetailInfo.Rows[r.RowIndex].Cells[8].FindControl("lblappno");
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
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnPrint_Click Problem');</script>");
                //throw ex;
            }
        }


        protected void CertificateNoMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Please Input Certificate No.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Certificate No.');", true);
            return;

        }

        protected void UpdateMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Update Sucessfully Completed');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Update Sucessfully Completed');", true);
            return;

        }

        protected void BtnUpd_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvCertiDetailInfo.Rows.Count; i++)
                {
                    Label CTyNo = (Label)gvCertiDetailInfo.Rows[i].Cells[1].FindControl("lblcutype");
                    Label CrNo = (Label)gvCertiDetailInfo.Rows[i].Cells[2].FindControl("lblcno");
                    Label memNo = (Label)gvCertiDetailInfo.Rows[i].Cells[3].FindControl("lblMemNo");
                    Label AType = (Label)gvCertiDetailInfo.Rows[i].Cells[4].FindControl("lblAccType");
                    Label ANo = (Label)gvCertiDetailInfo.Rows[i].Cells[5].FindControl("lblAccNo");

                    TextBox txtCertiNo = (TextBox)gvCertiDetailInfo.Rows[i].Cells[10].FindControl("txtCertificateNo");

                    int CuType = Converter.GetSmallInteger(CTyNo.Text);
                    int CuNo = Converter.GetInteger(CrNo.Text);
                    int MemNo = Converter.GetInteger(memNo.Text);
                    int AccType = Converter.GetInteger(AType.Text);
                    Int64 AccNo = Converter.GetLong(ANo.Text);

                    string txtCertficateNo = txtCertiNo.Text;

                    if (txtCertficateNo != string.Empty)
                    {
                        string CheckUp = "UPDATE A2ZACCOUNT SET AccCertNo='" + txtCertficateNo + "' WHERE CuType='" + CuType + "' and CuNo='" + CuNo + "' and MemNo='" + MemNo + "' and AccType='" + AccType + "' and AccNo='" + AccNo + "'";
                        int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(CheckUp, "A2ZCSMCUS"));
                        if (rowEffect > 0)
                        {
                            string ChkUp = "UPDATE WFCSCERTIFICATENO SET Status = 1 WHERE CuType='" + CuType + "' and CuNo='" + CuNo + "' and MemNo='" + MemNo + "' and AccType='" + AccType + "' and AccNo='" + AccNo + "'";
                            int rEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(ChkUp, "A2ZCSMCUS"));
                        }
                    }
                }

                UpdateMSG();
                gvVerify();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnApprove_Click Problem');</script>");
                //throw ex;
            }

        }

        protected void gvCertiDetailInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }
        protected void btnSumbit_Click(object sender, EventArgs e)
        {
            if (ddlAcType.SelectedValue != "-Select-")
            {
                if (ChkWithCerti.Checked == true)
                {
                    lblWithFlag.Text = "1";
                }
                else
                {
                    lblWithFlag.Text = "0";
                }



                var prm = new object[2];
                prm[0] = ddlAcType.SelectedValue;
                prm[1] = lblWithFlag.Text;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSGenerateCertificateNo", prm, "A2ZCSMCUS"));
                if (result == 0)
                {
                    gvVerify();
                }

            }
        }



    }
}