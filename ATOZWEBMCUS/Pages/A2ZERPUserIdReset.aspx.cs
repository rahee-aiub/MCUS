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
    public partial class A2ZERPUserIdReset : System.Web.UI.Page
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
                    //ddlModule = A2ZERPMODULEDTO.GetDropDownList(ddlModule);

                    IdsDropdown();
                    txtIdsNo.Focus();


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


        protected void UpdateMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Reset Passward with XXXXXXXX');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }

        protected void ClearInformation()
        {
            txtIdsNo.Text = string.Empty;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                int RowEffected = 0;

                A2ZSYSIDSDTO dto = new A2ZSYSIDSDTO();

                AtoZUtility a2zUtility = new AtoZUtility();
                //string newPass = a2zUtility.GeneratePassword("XXXXXXXX", 0);
                string newPass = "XXXXXXXX";

                dto.IdsNo = Converter.GetInteger(txtIdsNo.Text);

                dto.IdsPass = newPass;

                RowEffected = A2ZSYSIDSDTO.UpdateResetPassword(dto);

                if (RowEffected > 0)
                {

                    UpdateMSG();
                    //ShowMessage("Data has been saved successfully.", Color.Green);
                    ClearInformation();
                    txtIdsNo.Focus();
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Add Problem');</script>");
                //throw ex;
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

                    
                    int RowEffected = 0;

                    A2ZSYSIDSDTO dto = new A2ZSYSIDSDTO();

                    AtoZUtility a2zUtility = new AtoZUtility();
                    //string newPass = a2zUtility.GeneratePassword("XXXXXXXX", 0);
                    string newPass = "XXXXXXXX";

                    dto.IdsNo = Converter.GetInteger(idsno);

                    dto.IdsPass = newPass;

                    RowEffected = A2ZSYSIDSDTO.UpdateResetPassword(dto);
                }
                ClearInformation();
                Response.Redirect("A2ZERPModule.aspx", false);
            }
        }
    }
}
