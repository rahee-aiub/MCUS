using DataAccessLayer.BLL;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class HOExportData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnimport_Click(object sender, EventArgs e)
        {
            int result;
            result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_HODataExport", "A2ZCSMCUS"));
            if (result == 0)
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
                String cstext1 = "alert('H/O Data imported successfully completed.');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");

        }
    }
}