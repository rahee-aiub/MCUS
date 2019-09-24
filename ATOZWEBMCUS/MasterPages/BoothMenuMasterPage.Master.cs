using System;
using System.Data;
using System.Web.UI.WebControls;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.HouseKeeping;


namespace ATOZWEBMCUS.MasterPages
{
    public partial class BoothMenuMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    linkSMSReload.Text = "SMS Reload";

                    txtsmsMsg.ReadOnly = true;

                    DivGridViewSMS.Visible = false;
                    DivTextMsg.Visible = false;

                    linkSMSMessage.Visible = false;
                    lblNo.Visible = false;

                    lblUserId.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    lblUserName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));

                    var p = A2ZERPSYSPRMDTO.GetParameterValue();
                    lblCompanyName.Text = p.PrmUnitName;

                    GetMenuData();

                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    lblProcessDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());

                    //if (DataAccessLayer.Utility.Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION)) == 10)
                    //{
                    //    lblUserPermission.Text = "Permission :" + "Input";
                    //}
                    //if (DataAccessLayer.Utility.Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION)) == 20)
                    //{
                    //    lblUserPermission.Text = "Permission :" + "Checked and Verify";
                    //}
                    //if (DataAccessLayer.Utility.Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION)) == 30)
                    //{
                    //    lblUserPermission.Text = "Permission :" + "Approved";
                    //}

                    hdnCashCode.Value = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                    string qry = "SELECT GLAccDesc FROM A2ZCGLMST where GLAccNo='" + hdnCashCode.Value + "'";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZGLMCUS");
                    if (dt.Rows.Count > 0)
                    {
                        lblBoothNo.Text = hdnCashCode.Value;
                        lblBoothName.Text = Converter.GetString(dt.Rows[0]["GLAccDesc"]);
                    }

                    GetSMSMsg();
                }
                else
                {
                    this.Page.MaintainScrollPositionOnPostBack = true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void GetSMSMsg()
        {
            string qry4 = "SELECT SMSNo,SMSDate FROM A2ZUSERSMS WHERE  SMSToIdsNo='" + lblUserId.Text + "' AND (SMSStatus=1 OR SMSStatus=3)";
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
            string sqlquery3 = "SELECT SMSDate,SMSNo,SMSFromIdsNo,SMSNote,SMSStatus from A2ZUSERSMS WHERE  SMSToIdsNo='" + lblUserId.Text + "' AND (SMSStatus=1 OR SMSStatus=3)";
            gvSMSInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvSMSInfo, "A2ZHKMCUS");
        }

        private void GetMenuData()
        {
            string strQuery = "SELECT MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZERPMENU WHERE UserId = " + DataAccessLayer.Utility.Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_ID));
            DataTable table = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZBTMCUS");


            if (table.Rows.Count == 0)
            {
                string notifyMsg = "?txtOne=" + lblUserName.Text + "&txtTwo=" + "You Don't Have Permission" +
                                                           "&txtThree=" + "Contact Your Super User" + "&PreviousMenu=A2ZERP.aspx";
                Server.Transfer("Notify.aspx" + notifyMsg);
            }


            DataView view = new DataView(table);
            view.RowFilter = "MenuParentNo IS NULL";
            foreach (DataRowView row in view)
            {
                MenuItem menuItem = new MenuItem(row["MenuName"].ToString(), row["MenuNo"].ToString());
                menuItem.NavigateUrl = row["MenuUrl"].ToString();
                menuBar.Items.Add(menuItem);
                AddChildItems(table, menuItem);
            }
        }
        private void AddChildItems(DataTable table, MenuItem menuItem)
        {
            DataView viewItem = new DataView(table);
            viewItem.RowFilter = "MenuParentNo = " + menuItem.Value;
            foreach (DataRowView childView in viewItem)
            {
                MenuItem childItem = new MenuItem(childView["MenuName"].ToString(), childView["MenuNo"].ToString());
                childItem.NavigateUrl = childView["MenuUrl"].ToString();
                menuItem.ChildItems.Add(childItem);
                AddChildItems(table, childItem);
            }
        }

        protected void linkSMSMessage_Click(object sender, EventArgs e)
        {
            if (DivGridViewSMS.Visible == false)
            {
                DivBooth.Visible = false;
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

                    if(smsStatus.Text == "1")
                    {
                        lblStatus.Text = "2";
                    }
                    else 
                    {
                        lblStatus.Text = "4";
                    }


                    string CheckUp = "UPDATE A2ZUSERSMS SET SMSStatus='" + lblStatus.Text + "' WHERE SMSToIdsNo='" + lblUserId.Text + "' AND SMSNo='" + lblsmsNo.Text + "'";
                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(CheckUp, "A2ZHKMCUS"));
                    if (rowEffect > 0)
                    {
                        string qry4 = "SELECT SMSNo,SMSDate FROM A2ZUSERSMS WHERE  SMSToIdsNo='" + lblUserId.Text + "' AND (SMSStatus=1 OR SMSStatus=3)";
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
            string qry4 = "SELECT SMSNo,SMSDate FROM A2ZUSERSMS WHERE  SMSToIdsNo='" + lblUserId.Text + "' AND (SMSStatus=1 OR SMSStatus=3)";
            DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry4, "A2ZHKMCUS");
            if (dt4.Rows.Count <= 0)
            {
                DivGridViewSMS.Visible = false;

                DivGridViewSMS.Visible = false;
                DivTextMsg.Visible = false;

                DivSMS.Visible = false;
                linkSMSMessage.Visible = false;
                lblNo.Visible = false;
                DivBooth.Visible = true;
            }
            else
            {
                DivTextMsg.Visible = false;
                txtsmsMsg.Text = string.Empty;
               
                gvSMS();
                gvSMSInfo.Visible = true;
            }
        }

        protected void linkSMSReload_Click(object sender, EventArgs e)
        {
            GetSMSMsg();
        }



    }
}
