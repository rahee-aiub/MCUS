using DataAccessLayer.BLL;
using DataAccessLayer.DTO.GeneralLedger;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class GLYearEnd : System.Web.UI.Page
    {

       SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("master"));

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    var dt = A2ZGLPARAMETERDTO.GetParameterValue();
                    txtBegYear.Text = Converter.GetString(dt.FinancialBegYear);
                    txtEndYear.Text = Converter.GetString(dt.FinancialEndYear);
                    A2ZERPSYSPRMDTO dto = A2ZERPSYSPRMDTO.GetParameterValue();
                    hdndatapath.Text = Converter.GetString(dto.PrmDataPath);

                    //btnProcess.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }

        protected void InvalidYEMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Year End Function');", true);
            return;

        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "SELECT PrmYearEndStat FROM A2ZERPSYSPRM";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHKMCUS");
                if (dt1.Rows.Count > 0)
                {
                    lblYearEndFlag.Text = Converter.GetString(dt1.Rows[0]["PrmYearEndStat"]);

                    if (lblYearEndFlag.Text != "1")
                    {
                        InvalidYEMSG();
                        return;
                    }
                }

                
                var prm = new object[1];
            
                prm[0] = txtEndYear.Text;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSGLYearEnd", prm, "A2ZCSMCUS"));

                if (result == 0)
                {
                    Sucessfull();
                }     
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Process End Problem');</script>");
                //throw ex;
            }
           
            

        }


        protected void Sucessfull()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Year End Database Create Sucessfully Done!');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Year End Sucessfully Done');", true);
            return;

        }

       

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");

        }
    }
}