using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSOpeningDataConversion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                txtOpBalDate.Focus();
                
            }
        }


        private void Successful()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Data Conversion successfully completed.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Conversion successfully completed');", true);
            return;

        }
        protected void BtnProcess_Click(object sender, EventArgs e)
        {
            if (txtOpBalDate.Text == string.Empty)
            {
                //String csname1 = "PopupScript";
                //Type cstype = GetType();
                //ClientScriptManager cs = Page.ClientScript;

                //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                //{
                //    String cstext1 = "alert('Please input Opening Balance Date.');";
                //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                //}
                txtOpBalDate.Focus();

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please input Opening Balance Date');", true);
                return;
            }
            //string trucate = "TRUNCATE TABLE dbo.A2ZCSOPBALANCE";
            //int re = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(trucate, "A2ZCSMCUST2015"));

            string delqry = "DELETE FROM A2ZCSOPBALANCE WHERE AccType=17";
            int row1Effect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUST2015"));

            DateTime opdate = DateTime.ParseExact(txtOpBalDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string qry = "INSERT INTO A2ZCSMCUST2015.dbo.A2ZCSOPBALANCE(CuType,CuNo,MemNo,AccType,AccNo,TrnAmount,CuOldNo) SELECT CuType, CuNo, MemNo, AccType, AccNo, AccBalance, OldCuNo FROM A2ZCSMCUS.dbo.A2ZACCOUNT WHERE AccType=17";
            int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry, "A2ZCSMCUST2015"));
            if(result>0)
            {
                string strqry = "UPDATE dbo.A2ZCSOPBALANCE SET TrnDate='" + opdate + "'";
                int result2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZCSMCUST2015"));
                if(result2>0)
                {
                    Successful();
                   
                }
                
            }
        }


        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        
    }
}