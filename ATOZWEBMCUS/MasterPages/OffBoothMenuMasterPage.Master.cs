﻿using System;
using System.Data;
using System.Web.UI.WebControls;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.HouseKeeping;


namespace ATOZWEBMCUS.MasterPages
{
    public partial class OffBoothMenuMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
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
                        lblOffBoothNo.Text = hdnCashCode.Value;
                        lblOffBoothName.Text = Converter.GetString(dt.Rows[0]["GLAccDesc"]);
                    }
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

        private void GetMenuData()
        {
            string strQuery = "SELECT MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZERPMENU WHERE UserId = " + DataAccessLayer.Utility.Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_ID));
            DataTable table = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZOBTMCUS");


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
    }
}
