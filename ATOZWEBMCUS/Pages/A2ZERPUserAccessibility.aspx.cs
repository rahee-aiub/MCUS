using System;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System.Drawing;

using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO;
using System.Web.UI;
using System.Data;



namespace ATOZWEBMCUS.Pages
{
    public partial class A2ZERPUserAccessibility : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DivMessage.Visible = false;
            try
            {
                if (IsPostBack)
                {

                }
                else
                {
                    //ddlModule = A2ZERPMODULEDTO.GetDropDownList(ddlModule);

                    txtIdsNo.Focus();
                    IdsDropdown();


                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Err.Load Problem');</script>");
                //throw ex;
            }

        }

        private void IdsDropdown()
        {
            string sqlquery = "SELECT IdsNo,IdsName from A2ZSYSIDS ORDER BY IdsName ASC";
            ddlIdsNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlIdsNo, "A2ZHKMCUS");
        }

        private void IDsNotFoundMsg()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ids Does not exist');", true);
            return;

        }
        protected void txtIdsNo_TextChanged(object sender, EventArgs e)
        {
            int idno = Converter.GetInteger(txtIdsNo.Text);
            A2ZSYSIDSDTO dto = new A2ZSYSIDSDTO();
            dto = A2ZSYSIDSDTO.GetUserInformation(idno, "A2ZHKMCUS");
            if (dto.IdsNo > 0)
            {
                ddlIdsNo.SelectedValue = Converter.GetString(dto.IdsNo);

                string sqlquery = "SELECT ModuleNo,ModuleName from A2ZSYSMODULECTRL WHERE IDSNO = '" + ddlIdsNo.SelectedValue + "' ";
                ddlModule = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlModule, "A2ZHKMCUS");

            }
            else
            {
                IDsNotFoundMsg();
                ddlIdsNo.SelectedValue = "-Select-";
                txtIdsNo.Text = string.Empty;
                txtIdsNo.Focus();
                return;
            }


            ddlIdsNo.SelectedValue = Converter.GetString(txtIdsNo.Text);
        }

        protected void ddlIdsNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIdsNo.Text = Converter.GetString(ddlIdsNo.SelectedValue);

            string sqlquery = "SELECT ModuleNo,ModuleName from A2ZSYSMODULECTRL WHERE IDSNO = '" + ddlIdsNo.SelectedValue + "' ";
            ddlModule = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlModule, "A2ZHKMCUS");

        }


        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlIdsNo.SelectedValue == "-Select-")
                {
                    ddlModule.SelectedValue = "-Select-";
                    ddlIdsNo.Focus();
                    return;
                }


                DivGridView.Visible = true;

                int result = 0;
                string strQuery = string.Empty;


                strQuery = "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = null";
                result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));

                if (ddlModule.SelectedValue == "1")
                {
                    strQuery =
                        "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = (SELECT UserId FROM A2ZCSMCUS.DBO.A2ZERPMENU WHERE A2ZCSMCUS.DBO.A2ZERPMENU.UserId = " +
                          ddlIdsNo.SelectedValue + " AND A2ZHKMCUS.DBO.A2ZMASTERMENU.MenuNo = A2ZCSMCUS.DBO.A2ZERPMENU.MenuNo AND " +
                          "A2ZHKMCUS.DBO.A2ZMASTERMENU.ModuleNo = " + ddlModule.SelectedValue + ")";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));
                }

                if (ddlModule.SelectedValue == "2")
                {
                    strQuery =
                        "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = (SELECT UserId FROM A2ZGLMCUS.DBO.A2ZERPMENU WHERE A2ZGLMCUS.DBO.A2ZERPMENU.UserId = " +
                          ddlIdsNo.SelectedValue + " AND A2ZHKMCUS.DBO.A2ZMASTERMENU.MenuNo = A2ZGLMCUS.DBO.A2ZERPMENU.MenuNo AND " +
                          "A2ZHKMCUS.DBO.A2ZMASTERMENU.ModuleNo = " + ddlModule.SelectedValue + ")";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));
                }


                if (ddlModule.SelectedValue == "3")
                {
                    strQuery =
                        "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = (SELECT UserId FROM A2ZHKMCUS.DBO.A2ZERPMENU WHERE A2ZHKMCUS.DBO.A2ZERPMENU.UserId = " +
                          ddlIdsNo.SelectedValue + " AND A2ZHKMCUS.DBO.A2ZMASTERMENU.MenuNo = A2ZHKMCUS.DBO.A2ZERPMENU.MenuNo AND " +
                          "A2ZHKMCUS.DBO.A2ZMASTERMENU.ModuleNo = " + ddlModule.SelectedValue + ")";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));
                }

                if (ddlModule.SelectedValue == "4")
                {
                    strQuery =
                        "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = (SELECT UserId FROM A2ZHRMCUS.DBO.A2ZERPMENU WHERE A2ZHRMCUS.DBO.A2ZERPMENU.UserId = " +
                          ddlIdsNo.SelectedValue + " AND A2ZHKMCUS.DBO.A2ZMASTERMENU.MenuNo = A2ZHRMCUS.DBO.A2ZERPMENU.MenuNo AND " +
                          "A2ZHKMCUS.DBO.A2ZMASTERMENU.ModuleNo = " + ddlModule.SelectedValue + ")";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));
                }

                if (ddlModule.SelectedValue == "5")
                {
                    strQuery =
                        "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = (SELECT UserId FROM A2ZSTMCUS.DBO.A2ZERPMENU WHERE A2ZSTMCUS.DBO.A2ZERPMENU.UserId = " +
                          ddlIdsNo.SelectedValue + " AND A2ZHKMCUS.DBO.A2ZMASTERMENU.MenuNo = A2ZSTMCUS.DBO.A2ZERPMENU.MenuNo AND " +
                          "A2ZHKMCUS.DBO.A2ZMASTERMENU.ModuleNo = " + ddlModule.SelectedValue + ")";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));
                }

                if (ddlModule.SelectedValue == "6")
                {
                    strQuery =
                        "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = (SELECT UserId FROM A2ZBTMCUS.DBO.A2ZERPMENU WHERE A2ZBTMCUS.DBO.A2ZERPMENU.UserId = " +
                          ddlIdsNo.SelectedValue + " AND A2ZHKMCUS.DBO.A2ZMASTERMENU.MenuNo = A2ZBTMCUS.DBO.A2ZERPMENU.MenuNo AND " +
                          "A2ZHKMCUS.DBO.A2ZMASTERMENU.ModuleNo = " + ddlModule.SelectedValue + ")";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));
                }
                if (ddlModule.SelectedValue == "7")
                {
                    strQuery =
                        "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = (SELECT UserId FROM A2ZOBTMCUS.DBO.A2ZERPMENU WHERE A2ZOBTMCUS.DBO.A2ZERPMENU.UserId = " +
                          ddlIdsNo.SelectedValue + " AND A2ZHKMCUS.DBO.A2ZMASTERMENU.MenuNo = A2ZOBTMCUS.DBO.A2ZERPMENU.MenuNo AND " +
                          "A2ZHKMCUS.DBO.A2ZMASTERMENU.ModuleNo = " + ddlModule.SelectedValue + ")";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));
                }
                strQuery = " SELECT MenuName,MenuNo,UserId FROM A2ZMASTERMENU WHERE ModuleNo = '" + ddlModule.SelectedValue + "' AND MenuParentNo IS NOT NULL AND MenuUrl IS NOT NULL";
                gvModule = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvModule, "A2ZHKMCUS");

                ShowGridViewWithValue();

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Err.Select Problem');</script>");
                throw ex;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string strQuery = "UPDATE A2ZMASTERMENU SET UserId = NULL WHERE ModuleNo = " + ddlModule.SelectedValue;
                int rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));

                int i = 0;

                foreach (GridViewRow gv in gvModule.Rows)
                {
                    Boolean m = ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked;

                    if (m)
                    {
                        strQuery = "UPDATE A2ZMASTERMENU SET UserId = '" + ddlIdsNo.SelectedValue + "' WHERE MenuNo = " + gvModule.Rows[i].Cells[2].Text + " AND ModuleNo = " + ddlModule.SelectedValue;
                        rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));
                    }
                    i++;

                }
                if (ddlModule.SelectedValue == "1") //FOR CUSTOMER SERVICE
                {

                    strQuery = "DELETE FROM A2ZCSMCUS.DBO.A2ZERPMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "' AND ModuleNo = " + ddlModule.SelectedValue;
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                    strQuery = "INSERT INTO A2ZCSMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "'";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                    strQuery = "INSERT INTO A2ZCSMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT " + ddlIdsNo.SelectedValue + ",ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE ModuleNo = " + ddlModule.SelectedValue + " AND (MenuParentNo IS NULL OR MenuUrl IS NULL)";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                    if (rowEffiect > 0)
                    {
                        ShowMessage("Data saved successfully.", Color.Green);
                        ClearInformation();
                    }
                }
                if (ddlModule.SelectedValue == "2") //FOR GENERAL
                {

                    strQuery = "DELETE FROM A2ZGLMCUS.DBO.A2ZERPMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "' AND ModuleNo = " + ddlModule.SelectedValue;
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));

                    strQuery = "INSERT INTO A2ZGLMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "'";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));

                    strQuery = "INSERT INTO A2ZGLMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT " + ddlIdsNo.SelectedValue + ",ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE ModuleNo = " + ddlModule.SelectedValue + " AND (MenuParentNo IS NULL OR MenuUrl IS NULL)";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));

                    if (rowEffiect > 0)
                    {
                        ShowMessage("Data saved successfully.", Color.Green);
                        ClearInformation();
                    }
                }

                if (ddlModule.SelectedValue == "3") //HOUSE KEEPING
                {
                    strQuery = "DELETE FROM A2ZHKMCUS.DBO.A2ZERPMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "' AND ModuleNo = " + ddlModule.SelectedValue;
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));

                    strQuery = "INSERT INTO A2ZHKMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "'";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));

                    strQuery = "INSERT INTO A2ZHKMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT " + ddlIdsNo.SelectedValue + ",ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE ModuleNo = " + ddlModule.SelectedValue + " AND (MenuParentNo IS NULL OR MenuUrl IS NULL)";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));

                    if (rowEffiect > 0)
                    {
                        ShowMessage("Data saved successfully.", Color.Green);
                        ClearInformation();
                    }
                }

                if (ddlModule.SelectedValue == "4") // HR 
                {

                    strQuery = "DELETE FROM A2ZHRMCUS.DBO.A2ZERPMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "' AND ModuleNo = " + ddlModule.SelectedValue;
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

                    strQuery = "INSERT INTO A2ZHRMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "'";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

                    strQuery = "INSERT INTO A2ZHRMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT " + ddlIdsNo.SelectedValue + ",ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE ModuleNo = " + ddlModule.SelectedValue + " AND (MenuParentNo IS NULL OR MenuUrl IS NULL)";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

                    if (rowEffiect > 0)
                    {
                        ShowMessage("Data saved successfully.", Color.Green);
                        ClearInformation();
                    }
                }


                if (ddlModule.SelectedValue == "5") //FOR INVENTORY
                {

                    strQuery = "DELETE FROM A2ZSTMCUS.DBO.A2ZERPMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "' AND ModuleNo = " + ddlModule.SelectedValue;
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                    strQuery = "INSERT INTO A2ZSTMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "'";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                    strQuery = "INSERT INTO A2ZSTMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT " + ddlIdsNo.SelectedValue + ",ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE ModuleNo = " + ddlModule.SelectedValue + " AND (MenuParentNo IS NULL OR MenuUrl IS NULL)";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                    if (rowEffiect > 0)
                    {
                        ShowMessage("Data saved successfully.", Color.Green);
                        ClearInformation();
                    }
                }

                if (ddlModule.SelectedValue == "6") // BT 
                {

                    strQuery = "DELETE FROM A2ZBTMCUS.DBO.A2ZERPMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "' AND ModuleNo = " + ddlModule.SelectedValue;
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZBTMCUS"));

                    strQuery = "INSERT INTO A2ZBTMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "'";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZBTMCUS"));

                    strQuery = "INSERT INTO A2ZBTMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT " + ddlIdsNo.SelectedValue + ",ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE ModuleNo = " + ddlModule.SelectedValue + " AND (MenuParentNo IS NULL OR MenuUrl IS NULL)";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZBTMCUS"));

                    if (rowEffiect > 0)
                    {
                        ShowMessage("Data saved successfully.", Color.Green);
                        ClearInformation();
                    }
                }

                if (ddlModule.SelectedValue == "7") // BT 
                {

                    strQuery = "DELETE FROM A2ZOBTMCUS.DBO.A2ZERPMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "' AND ModuleNo = " + ddlModule.SelectedValue;
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZOBTMCUS"));

                    strQuery = "INSERT INTO A2ZOBTMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "'";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZOBTMCUS"));

                    strQuery = "INSERT INTO A2ZOBTMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT " + ddlIdsNo.SelectedValue + ",ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE ModuleNo = " + ddlModule.SelectedValue + " AND (MenuParentNo IS NULL OR MenuUrl IS NULL)";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZOBTMCUS"));

                    if (rowEffiect > 0)
                    {
                        ShowMessage("Data saved successfully.", Color.Green);
                        ClearInformation();
                        ddlModule.Focus();
                    }
                }


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Add Problem');</script>");
                //throw ex;
            }

        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("A2ZERPModule.aspx", false);
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Exit Problem');</script>");
                //throw ex;
            }
        }

        protected void ShowMessage(string message, Color clr)
        {
            lblMessage.Text = message;
            lblMessage.ForeColor = clr;
            lblMessage.Visible = true;
            DivMessage.Visible = true;
            DivMain.Attributes.CssStyle.Add("opacity", "0.1");
            DivGridView.Attributes.CssStyle.Add("opacity", "0.1");
            DivButton.Attributes.CssStyle.Add("opacity", "0.1");


            DivMessage.Style.Add("Top", "250px");
            DivMessage.Style.Add("Right", "500px");
            DivMessage.Style.Add("position", "absolute");
            DivMessage.Attributes.CssStyle.Add("opacity", "100");
        }

        protected void btnHideMessageDiv_Click(object sender, EventArgs e)
        {
            DivMain.Attributes.CssStyle.Add("opacity", "100");
            DivGridView.Attributes.CssStyle.Add("opacity", "100");
            DivButton.Attributes.CssStyle.Add("opacity", "100");
            DivMessage.Visible = false;
            DivMain.Visible = true;
        }

        //        protected void btnShow_Click(object sender, EventArgs e)
        //        {
        //            DivGridView2.Visible = true;


        //            string strQuery = @"SELECT dbo.A2ZSYSIDS.IdsNo, dbo.A2ZSYSIDS.IdsName,
        //                             dbo.A2ZIDLEVEL.LevelDesc, dbo.A2ZSYSIDS.EmpCode, 
        //                             dbo.A2ZIDPERMISSION.PermissionDesc
        //                             FROM dbo.A2ZSYSIDS INNER JOIN
        //                              dbo.A2ZIDLEVEL ON dbo.A2ZSYSIDS.IdsLevel = dbo.A2ZIDLEVEL.IdsLevel INNER JOIN
        //                             dbo.A2ZIDPERMISSION ON dbo.A2ZSYSIDS.IdsFlag = dbo.A2ZIDPERMISSION.IdsPermission";
        //            gvUserIdInfromation = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvUserIdInfromation, "A2ZHKMCUS");
        //        }

        //protected void btnClose_Click(object sender, EventArgs e)
        //{
        //    DivGridView2.Visible = false;
        //}

        private void ClearInformation()
        {

            try
            {
                //ddlUserId.SelectedIndex = 0;
                ddlModule.SelectedIndex = 0;

                DivGridView.Visible = false;
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Clear Problem');</script>");

                //throw ex;
            }
        }

        private void ShowGridViewWithValue()
        {
            int i = 0;

            foreach (GridViewRow gv in gvModule.Rows)
            {
                int userId = Converter.GetSmallInteger(gvModule.Rows[i].Cells[3].Text);

                if (userId > 0)
                {
                    ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked = true;
                }

                i++;

            }

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

        protected void btnBooth_Click(object sender, EventArgs e)
        {
            string qry = "SELECT IdsNo,ModuleNo FROM A2ZSYSMODULECTRL WHERE ModuleNo=6";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHKMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    var idsno = dr["IdsNo"].ToString();
                    var moduleno = dr["ModuleNo"].ToString();

                    string strQuery = string.Empty;
                    int result = 0;

                    strQuery = "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = null";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));


                    strQuery =
                        "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = (SELECT UserId FROM A2ZBTMCUS.DBO.A2ZERPMENU WHERE A2ZBTMCUS.DBO.A2ZERPMENU.UserId = " +
                          idsno + " AND A2ZHKMCUS.DBO.A2ZMASTERMENU.MenuNo = A2ZBTMCUS.DBO.A2ZERPMENU.MenuNo AND " +
                          "A2ZHKMCUS.DBO.A2ZMASTERMENU.ModuleNo = " + moduleno + ")";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));

                    
                    strQuery = " SELECT MenuName,MenuNo,UserId FROM A2ZMASTERMENU WHERE ModuleNo = '" + moduleno + "' AND MenuParentNo IS NOT NULL AND MenuUrl IS NOT NULL";
                    gvModule = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvModule, "A2ZHKMCUS");

                    int i = 0;

                    foreach (GridViewRow gv in gvModule.Rows)
                    {
                        ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked = true;

                        i++;
                    }

                    string str1Query = "UPDATE A2ZMASTERMENU SET UserId = NULL WHERE ModuleNo = " + moduleno;
                    int rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZHKMCUS"));

                    int ii = 0;

                    foreach (GridViewRow gv in gvModule.Rows)
                    {
                        Boolean m = ((CheckBox)gvModule.Rows[ii].FindControl("chkSelect")).Checked;

                        if (m)
                        {
                            strQuery = "UPDATE A2ZMASTERMENU SET UserId = '" + idsno + "' WHERE MenuNo = " + gvModule.Rows[ii].Cells[2].Text + " AND ModuleNo = " + moduleno;
                            rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));
                        }
                        ii++;
                    }

                    strQuery = "DELETE FROM A2ZBTMCUS.DBO.A2ZERPMENU WHERE UserId = '" + idsno + "' AND ModuleNo = " + moduleno;
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZBTMCUS"));

                    strQuery = "INSERT INTO A2ZBTMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE UserId = '" + idsno + "'";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZBTMCUS"));

                    strQuery = "INSERT INTO A2ZBTMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT " + idsno + ",ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE ModuleNo = " + moduleno + " AND (MenuParentNo IS NULL OR MenuUrl IS NULL)";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZBTMCUS"));


                    //int RowEffected = 0;

                    //A2ZSYSIDSDTO dto = new A2ZSYSIDSDTO();

                    //AtoZUtility a2zUtility = new AtoZUtility();
                    ////string newPass = a2zUtility.GeneratePassword("XXXXXXXX", 0);
                    //string newPass = "XXXXXXXX";

                    //dto.IdsNo = Converter.GetInteger(idsno);

                    //dto.IdsPass = newPass;

                    //RowEffected = A2ZSYSIDSDTO.UpdateResetPassword(dto);



                }

                ShowMessage("Data saved successfully.", Color.Green);
                ClearInformation();
                Response.Redirect("A2ZERPModule.aspx", false);
            }
        }


        protected void btnCS_Click(object sender, EventArgs e)
        {
            string qry = "SELECT IdsNo,ModuleNo FROM A2ZSYSMODULECTRL WHERE ModuleNo=1";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHKMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    var idsno = dr["IdsNo"].ToString();
                    var moduleno = dr["ModuleNo"].ToString();

                    string strQuery = string.Empty;
                    int result = 0;

                    strQuery = "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = null";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));


                    strQuery =
                        "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = (SELECT UserId FROM A2ZCSMCUS.DBO.A2ZERPMENU WHERE A2ZCSMCUS.DBO.A2ZERPMENU.UserId = " +
                          idsno + " AND A2ZHKMCUS.DBO.A2ZMASTERMENU.MenuNo = A2ZCSMCUS.DBO.A2ZERPMENU.MenuNo AND " +
                          "A2ZHKMCUS.DBO.A2ZMASTERMENU.ModuleNo = " + moduleno + ")";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));


                    strQuery = " SELECT MenuName,MenuNo,UserId FROM A2ZMASTERMENU WHERE ModuleNo = '" + moduleno + "' AND MenuParentNo IS NOT NULL AND MenuUrl IS NOT NULL";
                    gvModule = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvModule, "A2ZHKMCUS");

                    int i = 0;

                    foreach (GridViewRow gv in gvModule.Rows)
                    {
                        ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked = true;

                        i++;
                    }

                    string str1Query = "UPDATE A2ZMASTERMENU SET UserId = NULL WHERE ModuleNo = " + moduleno;
                    int rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZHKMCUS"));

                    int ii = 0;

                    foreach (GridViewRow gv in gvModule.Rows)
                    {
                        Boolean m = ((CheckBox)gvModule.Rows[ii].FindControl("chkSelect")).Checked;

                        if (m)
                        {
                            strQuery = "UPDATE A2ZMASTERMENU SET UserId = '" + idsno + "' WHERE MenuNo = " + gvModule.Rows[ii].Cells[2].Text + " AND ModuleNo = " + moduleno;
                            rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));
                        }
                        ii++;
                    }

                    strQuery = "DELETE FROM A2ZCSMCUS.DBO.A2ZERPMENU WHERE UserId = '" + idsno + "' AND ModuleNo = " + moduleno;
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                    strQuery = "INSERT INTO A2ZCSMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE UserId = '" + idsno + "'";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                    strQuery = "INSERT INTO A2ZCSMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT " + idsno + ",ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE ModuleNo = " + moduleno + " AND (MenuParentNo IS NULL OR MenuUrl IS NULL)";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));


                    //int RowEffected = 0;

                    //A2ZSYSIDSDTO dto = new A2ZSYSIDSDTO();

                    //AtoZUtility a2zUtility = new AtoZUtility();
                    ////string newPass = a2zUtility.GeneratePassword("XXXXXXXX", 0);
                    //string newPass = "XXXXXXXX";

                    //dto.IdsNo = Converter.GetInteger(idsno);

                    //dto.IdsPass = newPass;

                    //RowEffected = A2ZSYSIDSDTO.UpdateResetPassword(dto);



                }

                ShowMessage("Data saved successfully.", Color.Green);
                ClearInformation();
                Response.Redirect("A2ZERPModule.aspx", false);
            }
        }


        protected void btnAdmin_Click(object sender, EventArgs e)
        {
            string qry = "SELECT IdsNo,ModuleNo FROM A2ZSYSMODULECTRL WHERE ModuleNo=1";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHKMCUS");
            int totrec = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    var idsno = dr["IdsNo"].ToString();
                    var moduleno = dr["ModuleNo"].ToString();

                    string strQuery = string.Empty;
                    int result = 0;

                    strQuery = "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = null";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));


                    strQuery =
                        "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = (SELECT UserId FROM A2ZCSMCUS.DBO.A2ZERPMENU WHERE A2ZCSMCUS.DBO.A2ZERPMENU.UserId = " +
                          idsno + " AND A2ZHKMCUS.DBO.A2ZMASTERMENU.MenuNo = A2ZCSMCUS.DBO.A2ZERPMENU.MenuNo AND " +
                          "A2ZHKMCUS.DBO.A2ZMASTERMENU.ModuleNo = " + moduleno + ")";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));


                    strQuery = " SELECT MenuName,MenuNo,UserId FROM A2ZMASTERMENU WHERE ModuleNo = '" + moduleno + "' AND MenuParentNo IS NOT NULL AND MenuUrl IS NOT NULL";
                    gvModule = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvModule, "A2ZHKMCUS");

                    int i = 0;

                    foreach (GridViewRow gv in gvModule.Rows)
                    {
                        ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked = true;

                        i++;
                    }

                    string str1Query = "UPDATE A2ZMASTERMENU SET UserId = NULL WHERE ModuleNo = " + moduleno;
                    int rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZHKMCUS"));

                    int ii = 0;

                    foreach (GridViewRow gv in gvModule.Rows)
                    {
                        Boolean m = ((CheckBox)gvModule.Rows[ii].FindControl("chkSelect")).Checked;

                        if (m)
                        {
                            strQuery = "UPDATE A2ZMASTERMENU SET UserId = '" + idsno + "' WHERE MenuNo = " + gvModule.Rows[ii].Cells[2].Text + " AND ModuleNo = " + moduleno;
                            rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));
                        }
                        ii++;
                    }

                    strQuery = "DELETE FROM A2ZCSMCUS.DBO.A2ZERPMENU WHERE UserId = '" + idsno + "' AND ModuleNo = " + moduleno;
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                    strQuery = "INSERT INTO A2ZCSMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE UserId = '" + idsno + "'";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

                    strQuery = "INSERT INTO A2ZCSMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT " + idsno + ",ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE ModuleNo = " + moduleno + " AND (MenuParentNo IS NULL OR MenuUrl IS NULL)";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));


                    //int RowEffected = 0;

                    //A2ZSYSIDSDTO dto = new A2ZSYSIDSDTO();

                    //AtoZUtility a2zUtility = new AtoZUtility();
                    ////string newPass = a2zUtility.GeneratePassword("XXXXXXXX", 0);
                    //string newPass = "XXXXXXXX";

                    //dto.IdsNo = Converter.GetInteger(idsno);

                    //dto.IdsPass = newPass;

                    //RowEffected = A2ZSYSIDSDTO.UpdateResetPassword(dto);



                }
            }

            //-------------------------------------------------------------

            string qry1 = "SELECT IdsNo,ModuleNo FROM A2ZSYSMODULECTRL WHERE ModuleNo=2";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHKMCUS");
            int totrec1 = dt1.Rows.Count;
            if (dt1.Rows.Count > 0)
            {

                foreach (DataRow dr in dt1.Rows)
                {
                    var idsno = dr["IdsNo"].ToString();
                    var moduleno = dr["ModuleNo"].ToString();

                    string strQuery = string.Empty;
                    int result = 0;

                    strQuery = "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = null";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));


                    strQuery =
                        "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = (SELECT UserId FROM A2ZGLMCUS.DBO.A2ZERPMENU WHERE A2ZGLMCUS.DBO.A2ZERPMENU.UserId = " +
                          idsno + " AND A2ZHKMCUS.DBO.A2ZMASTERMENU.MenuNo = A2ZGLMCUS.DBO.A2ZERPMENU.MenuNo AND " +
                          "A2ZHKMCUS.DBO.A2ZMASTERMENU.ModuleNo = " + moduleno + ")";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));


                    strQuery = " SELECT MenuName,MenuNo,UserId FROM A2ZMASTERMENU WHERE ModuleNo = '" + moduleno + "' AND MenuParentNo IS NOT NULL AND MenuUrl IS NOT NULL";
                    gvModule = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvModule, "A2ZHKMCUS");

                    int i = 0;

                    foreach (GridViewRow gv in gvModule.Rows)
                    {
                        ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked = true;

                        i++;
                    }

                    string str1Query = "UPDATE A2ZMASTERMENU SET UserId = NULL WHERE ModuleNo = " + moduleno;
                    int rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZHKMCUS"));

                    int ii = 0;

                    foreach (GridViewRow gv in gvModule.Rows)
                    {
                        Boolean m = ((CheckBox)gvModule.Rows[ii].FindControl("chkSelect")).Checked;

                        if (m)
                        {
                            strQuery = "UPDATE A2ZMASTERMENU SET UserId = '" + idsno + "' WHERE MenuNo = " + gvModule.Rows[ii].Cells[2].Text + " AND ModuleNo = " + moduleno;
                            rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));
                        }
                        ii++;
                    }

                    strQuery = "DELETE FROM A2ZGLMCUS.DBO.A2ZERPMENU WHERE UserId = '" + idsno + "' AND ModuleNo = " + moduleno;
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));

                    strQuery = "INSERT INTO A2ZGLMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE UserId = '" + idsno + "'";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));

                    strQuery = "INSERT INTO A2ZGLMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT " + idsno + ",ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE ModuleNo = " + moduleno + " AND (MenuParentNo IS NULL OR MenuUrl IS NULL)";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));


                    //int RowEffected = 0;

                    //A2ZSYSIDSDTO dto = new A2ZSYSIDSDTO();

                    //AtoZUtility a2zUtility = new AtoZUtility();
                    ////string newPass = a2zUtility.GeneratePassword("XXXXXXXX", 0);
                    //string newPass = "XXXXXXXX";

                    //dto.IdsNo = Converter.GetInteger(idsno);

                    //dto.IdsPass = newPass;

                    //RowEffected = A2ZSYSIDSDTO.UpdateResetPassword(dto);



                }
            }

            //---------------------------------------------------------------------------------------

            string qry2 = "SELECT IdsNo,ModuleNo FROM A2ZSYSMODULECTRL WHERE ModuleNo=3";
            DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZHKMCUS");
            int totrec2 = dt2.Rows.Count;
            if (dt2.Rows.Count > 0)
            {

                foreach (DataRow dr in dt2.Rows)
                {
                    var idsno = dr["IdsNo"].ToString();
                    var moduleno = dr["ModuleNo"].ToString();

                    string strQuery = string.Empty;
                    int result = 0;

                    strQuery = "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = null";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));


                    strQuery =
                        "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = (SELECT UserId FROM A2ZHKMCUS.DBO.A2ZERPMENU WHERE A2ZGLMCUS.DBO.A2ZERPMENU.UserId = " +
                          idsno + " AND A2ZHKMCUS.DBO.A2ZMASTERMENU.MenuNo = A2ZHKMCUS.DBO.A2ZERPMENU.MenuNo AND " +
                          "A2ZHKMCUS.DBO.A2ZMASTERMENU.ModuleNo = " + moduleno + ")";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));


                    strQuery = " SELECT MenuName,MenuNo,UserId FROM A2ZMASTERMENU WHERE ModuleNo = '" + moduleno + "' AND MenuParentNo IS NOT NULL AND MenuUrl IS NOT NULL";
                    gvModule = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvModule, "A2ZHKMCUS");

                    int i = 0;

                    foreach (GridViewRow gv in gvModule.Rows)
                    {
                        ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked = true;

                        i++;
                    }

                    string str1Query = "UPDATE A2ZMASTERMENU SET UserId = NULL WHERE ModuleNo = " + moduleno;
                    int rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZHKMCUS"));

                    int ii = 0;

                    foreach (GridViewRow gv in gvModule.Rows)
                    {
                        Boolean m = ((CheckBox)gvModule.Rows[ii].FindControl("chkSelect")).Checked;

                        if (m)
                        {
                            strQuery = "UPDATE A2ZMASTERMENU SET UserId = '" + idsno + "' WHERE MenuNo = " + gvModule.Rows[ii].Cells[2].Text + " AND ModuleNo = " + moduleno;
                            rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));
                        }
                        ii++;
                    }

                    strQuery = "DELETE FROM A2ZHKMCUS.DBO.A2ZERPMENU WHERE UserId = '" + idsno + "' AND ModuleNo = " + moduleno;
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));

                    strQuery = "INSERT INTO A2ZHKMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE UserId = '" + idsno + "'";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));

                    strQuery = "INSERT INTO A2ZHKMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT " + idsno + ",ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE ModuleNo = " + moduleno + " AND (MenuParentNo IS NULL OR MenuUrl IS NULL)";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));


                    //int RowEffected = 0;

                    //A2ZSYSIDSDTO dto = new A2ZSYSIDSDTO();

                    //AtoZUtility a2zUtility = new AtoZUtility();
                    ////string newPass = a2zUtility.GeneratePassword("XXXXXXXX", 0);
                    //string newPass = "XXXXXXXX";

                    //dto.IdsNo = Converter.GetInteger(idsno);

                    //dto.IdsPass = newPass;

                    //RowEffected = A2ZSYSIDSDTO.UpdateResetPassword(dto);



                }
            }

            //----------------------------------------------------------------------------------

            string qry3 = "SELECT IdsNo,ModuleNo FROM A2ZSYSMODULECTRL WHERE ModuleNo=4";
            DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry3, "A2ZHKMCUS");
            int totrec3 = dt3.Rows.Count;
            if (dt3.Rows.Count > 0)
            {

                foreach (DataRow dr in dt3.Rows)
                {
                    var idsno = dr["IdsNo"].ToString();
                    var moduleno = dr["ModuleNo"].ToString();

                    string strQuery = string.Empty;
                    int result = 0;

                    strQuery = "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = null";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));


                    strQuery =
                        "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = (SELECT UserId FROM A2ZHRMCUS.DBO.A2ZERPMENU WHERE A2ZHRMCUS.DBO.A2ZERPMENU.UserId = " +
                          idsno + " AND A2ZHKMCUS.DBO.A2ZMASTERMENU.MenuNo = A2ZGLMCUS.DBO.A2ZERPMENU.MenuNo AND " +
                          "A2ZHKMCUS.DBO.A2ZMASTERMENU.ModuleNo = " + moduleno + ")";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));


                    strQuery = " SELECT MenuName,MenuNo,UserId FROM A2ZMASTERMENU WHERE ModuleNo = '" + moduleno + "' AND MenuParentNo IS NOT NULL AND MenuUrl IS NOT NULL";
                    gvModule = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvModule, "A2ZHKMCUS");

                    int i = 0;

                    foreach (GridViewRow gv in gvModule.Rows)
                    {
                        ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked = true;

                        i++;
                    }

                    string str1Query = "UPDATE A2ZMASTERMENU SET UserId = NULL WHERE ModuleNo = " + moduleno;
                    int rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZHKMCUS"));

                    int ii = 0;

                    foreach (GridViewRow gv in gvModule.Rows)
                    {
                        Boolean m = ((CheckBox)gvModule.Rows[ii].FindControl("chkSelect")).Checked;

                        if (m)
                        {
                            strQuery = "UPDATE A2ZMASTERMENU SET UserId = '" + idsno + "' WHERE MenuNo = " + gvModule.Rows[ii].Cells[2].Text + " AND ModuleNo = " + moduleno;
                            rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));
                        }
                        ii++;
                    }

                    strQuery = "DELETE FROM A2ZHRMCUS.DBO.A2ZERPMENU WHERE UserId = '" + idsno + "' AND ModuleNo = " + moduleno;
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

                    strQuery = "INSERT INTO A2ZHRMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE UserId = '" + idsno + "'";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

                    strQuery = "INSERT INTO A2ZHRMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT " + idsno + ",ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE ModuleNo = " + moduleno + " AND (MenuParentNo IS NULL OR MenuUrl IS NULL)";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));


                    //int RowEffected = 0;

                    //A2ZSYSIDSDTO dto = new A2ZSYSIDSDTO();

                    //AtoZUtility a2zUtility = new AtoZUtility();
                    ////string newPass = a2zUtility.GeneratePassword("XXXXXXXX", 0);
                    //string newPass = "XXXXXXXX";

                    //dto.IdsNo = Converter.GetInteger(idsno);

                    //dto.IdsPass = newPass;

                    //RowEffected = A2ZSYSIDSDTO.UpdateResetPassword(dto);



                }
            }

            //---------------------------------------------------------------------------------


            ShowMessage("Data saved successfully.", Color.Green);
            ClearInformation();
            Response.Redirect("A2ZERPModule.aspx", false);

        }



    }
}
