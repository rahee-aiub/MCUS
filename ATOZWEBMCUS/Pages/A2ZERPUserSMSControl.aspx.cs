using System;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System.Drawing;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;
using System.Data;
using System.Web.UI;

namespace ATOZWEBMCUS.Pages
{
    public partial class A2ZERPUserSMSControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {

                }
                else
                {
                    CtrlModule.Text = Request.QueryString["a%b"];

                    DivGridView.Visible = true;

                    txtsmsMsg.Visible = true;
                    txtsmsMsg.Focus();


                    lblFromIdsNo.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    lblFromIdsName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));

                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    lblProcDate.Text = date;

                    //if (CtrlModule.Text == "1")
                    //{
                    //    lblSentMode.Visible = false;
                    //    ddlSentMode.Visible = false;
                    //}
                    //else 
                    //{
                    //    lblSentMode.Visible = true;
                    //    ddlSentMode.Visible = true;
                    //}


                    //ddlUserId = A2ZSYSIDSDTO.GetDropDownList(ddlUserId, "IdsNo <> " + Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID)));

                    Iniatialized();



                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Iniatialized()
        {
            string strQuery = "SELECT IdsNo,IdsName,GLCashCode FROM A2ZSYSIDS WHERE IdsNo!='" + lblFromIdsNo.Text + "'";
            gvModule = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvModule, "A2ZHKMCUS");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZRECCTRLDTO getDTO = (A2ZRECCTRLDTO.GetLastRecords(7));
                lblNewSMSNo.Text = Converter.GetString(getDTO.CtrlRecLastNo);

                DateTime procdate = DateTime.ParseExact(lblProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                if (CtrlModule.Text == "1")
                {
                    lblSMSStatus.Text = "1";
                }
                else
                {
                    lblSMSStatus.Text = "3";
                }

                string sqlQuery;
                int rowEffiect;
                int i = 0;
                //int nCount = 0;


                //i = 0;
                //foreach (GridViewRow gv in gvModule.Rows)
                //{
                //    Boolean m = ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked;

                //    if (m)
                //    {
                //        nCount++;
                //    }
                //    i++;
                //}

                //if (nCount == 0)
                //{
                //    //String csname1 = "PopupScript";
                //    //Type cstype = GetType();
                //    //ClientScriptManager cs = Page.ClientScript;

                //    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //    //{
                //    //    String cstext1 = "alert('Please Select any one module');";
                //    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //    //}

                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select any one Module');", true);
                //    return;
                //}


                i = 0;
                foreach (GridViewRow gv in gvModule.Rows)
                {
                    Boolean m = ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked;

                    if (m)
                    {
                        lblFromIdsName.Text = (lblFromIdsName.Text != null) ? lblFromIdsName.Text.Trim().Replace("'", "''") : "";
                        //gvModule.Rows[i].Cells[2].Text = (gvModule.Rows[i].Cells[2].Text != null) ? gvModule.Rows[i].Cells[2].Text.Trim().Replace("'", "''") : "";

                        sqlQuery = "INSERT INTO A2ZUSERSMS (SMSNo,SMSDate,SMSFromIdsNo,SMSFromIdsName,SMSToIdsNo,SMSToIdsName,SMSNote,SMSStatus) VALUES ('" + lblNewSMSNo.Text + "','" + procdate + "','" + lblFromIdsNo.Text + "','" + lblFromIdsName.Text + "','" + gvModule.Rows[i].Cells[1].Text + "','" + gvModule.Rows[i].Cells[2].Text.Replace(@"&#160;", "") +"','" + txtsmsMsg.Text + "','" + lblSMSStatus.Text + "')";
                        rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHKMCUS"));
                    }
                    i++;

                }

                Iniatialized();

                txtsmsMsg.Text = string.Empty;
                //lblsmsMsg.Visible = false;
                //txtsmsMsg.Visible = false;

                //DivGridView.Visible = false;
                //lblsmsMsg.Visible = false;
                //txtsmsMsg.Visible = false;


                SentMsg();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx", false);
        }


        private void ShowGridViewWithValue()
        {
            int i = 0;

            foreach (GridViewRow gv in gvModule.Rows)
            {
                int userId = Converter.GetSmallInteger(gvModule.Rows[i].Cells[1].Text);

                if (userId > 0)
                {
                    ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked = true;
                }

                i++;

            }

        }

        private void SentMsg()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Your Message has been Sent');", true);
            return;
        }

        private void IDsNotFoundMsg()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ids Does not exist');", true);
            return;

        }


        protected void btnMark_Click(object sender, EventArgs e)
        {
            int i = 0;

            foreach (GridViewRow gv in gvModule.Rows)
            {
                ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked = true;

                i++;
            }
        }

        protected void btnUnMark_Click(object sender, EventArgs e)
        {
            int i = 0;

            foreach (GridViewRow gv in gvModule.Rows)
            {
                ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked = false;

                i++;
            }
        }

    }
}