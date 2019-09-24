using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class MenuOptionAccessibilityReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IdsDropdown();

                AllModule();

                chkAllUser.Checked = true;


                txtIdsNo.Enabled = false;
                ddlIdsNo.Enabled = false;

            }
        }


        private void IdsDropdown()
        {
            string sqlquery = "SELECT IdsNo,IdsName from A2ZSYSIDS ORDER BY IdsName ASC";
            ddlIdsNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlIdsNo, "A2ZHKMCUS");
        }


        protected void BtnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlModule.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Module');", true);
                    return;
                }




                if (chkAllUser.Checked == false && ddlIdsNo.SelectedIndex == 0)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select User Id.');", true);
                    return;
                }



                var prm = new object[2];


                prm[0] = ddlModule.SelectedValue;

                if (chkAllUser.Checked == true)
                {
                    prm[1] = "0";
                }
                else
                {
                    prm[1] = ddlIdsNo.SelectedValue;
                }


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GenerateUserMenuOptionAccessibility", prm, "A2ZCSMCUS"));



                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);



                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptUserAccessibilityReport");


                Response.Redirect("ReportServer.aspx", false);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        protected void chkAllUser_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllUser.Checked == false)
            {
                txtIdsNo.Enabled = true;
                ddlIdsNo.Enabled = true;

                InitialedModule();

                
                
            }
            else
            {
                txtIdsNo.Enabled = false;
                ddlIdsNo.Enabled = false;

                AllModule();
            }
        }




        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void txtIdsNo_TextChanged(object sender, EventArgs e)
        {
            int idno = Converter.GetInteger(txtIdsNo.Text);
            A2ZSYSIDSDTO dto = new A2ZSYSIDSDTO();
            dto = A2ZSYSIDSDTO.GetUserInformation(idno, "A2ZHKMCUS");
            if (dto.IdsNo > 0)
            {
                ddlIdsNo.SelectedValue = Converter.GetString(dto.IdsNo);

                SelectModule();
            }
            else
            {
                ddlIdsNo.SelectedValue = "-Select-";
                txtIdsNo.Text = string.Empty;
                txtIdsNo.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ids Does not exist');", true);
                return;

            }


            ddlIdsNo.SelectedValue = Converter.GetString(txtIdsNo.Text);
        }

        protected void ddlIdsNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIdsNo.Text = Converter.GetString(ddlIdsNo.SelectedValue);

            SelectModule();

        }

        private void InitialedModule()
        {
            string sqlquery;
            string statment;
            int rowEffect;


            sqlquery = @"DELETE dbo.WFA2ZERPMODULE";
            int result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery, "A2ZCSMCUS"));


            //statment = "INSERT INTO  WFA2ZERPMODULE (ModuleNo,ModuleName) VALUES(0,'-Select-')";
            //rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));

            
            string sqlquery1 = "SELECT ModuleNo,ModuleName from WFA2ZERPMODULE";
            ddlModule = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery1, ddlModule, "A2ZCSMCUS");

        }
        private void AllModule()
        {
            string sqlquery;
            string statment;
            int rowEffect;
           

            sqlquery = @"DELETE dbo.WFA2ZERPMODULE";
            int result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery, "A2ZCSMCUS"));


            statment = "INSERT INTO  WFA2ZERPMODULE (ModuleNo,ModuleName) VALUES(1,'Customer Service')";
            rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));

            statment = "INSERT INTO  WFA2ZERPMODULE (ModuleNo,ModuleName) VALUES(2,'General Ledger')";
            rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));

            statment = "INSERT INTO  WFA2ZERPMODULE (ModuleNo,ModuleName) VALUES(6,'Booth')";
            rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));

            string sqlquery1 = "SELECT ModuleNo,ModuleName from WFA2ZERPMODULE";
            ddlModule = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery1, ddlModule, "A2ZCSMCUS");

        }

        private void SelectModule()
        {

            string sqlquery;
            sqlquery = @"DELETE dbo.WFA2ZERPMODULE";
            int result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery, "A2ZCSMCUS"));

            string qry1 = "SELECT IdsNo FROM A2ZSYSMODULECTRL WHERE IdsNo = '" + txtIdsNo.Text + "' AND ModuleNo = 1";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHKMCUS");
            if (dt1.Rows.Count > 0)
            {
                string statment = "INSERT INTO  WFA2ZERPMODULE (ModuleNo,ModuleName) VALUES(1,'Customer Service')";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));
            }

            string qry2 = "SELECT IdsNo FROM A2ZSYSMODULECTRL WHERE IdsNo = '" + txtIdsNo.Text + "' AND ModuleNo = 2";
            DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry2, "A2ZHKMCUS");
            if (dt2.Rows.Count > 0)
            {
                string statment = "INSERT INTO  WFA2ZERPMODULE (ModuleNo,ModuleName) VALUES(2,'General Ledger')";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));
            }


            string qry6 = "SELECT IdsNo FROM A2ZSYSMODULECTRL WHERE IdsNo = '" + txtIdsNo.Text + "' AND ModuleNo = 6";
            DataTable dt6 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry6, "A2ZHKMCUS");
            if (dt6.Rows.Count > 0)
            {
                string statment = "INSERT INTO  WFA2ZERPMODULE (ModuleNo,ModuleName) VALUES(6,'Booth')";
                int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(statment, "A2ZCSMCUS"));
            }


            string sqlquery1 = "SELECT ModuleNo,ModuleName from WFA2ZERPMODULE";
            ddlModule = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery1, ddlModule, "A2ZCSMCUS");

        }

    }
}