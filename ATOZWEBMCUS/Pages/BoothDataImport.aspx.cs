using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
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
    public partial class BoothDataImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
               
            }
        }

       
        protected void btnimport_Click(object sender, EventArgs e)
        {
            string strqry = string.Empty;
            int result = 0;
            DateTime Fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime Todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            var prm = new object[3];
            prm[0] = Fromdate;
            prm[1] = Todate;
            prm[2] = lblCashCode.Text;

            result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_BoothDataImport", prm, "A2ZCSMCUS"));
            if (result > 0)
            {
                Successful();
            }
        }
            

        private void Successful()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Booth Data imported successfully completed.');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

        }


        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

     
    }
}