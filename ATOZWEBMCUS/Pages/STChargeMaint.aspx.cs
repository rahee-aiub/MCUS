using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Inventory;
using DataAccessLayer.Utility;

namespace ATOZWEBMCUS.Pages
{
    public partial class STChargeMaint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }

        }
        private void LoadData()
        {
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT STKVat,STKTax FROM A2ZSTCHRG", "A2ZSTMCUS");

            if (dt.Rows.Count > 0)
            {
                txtVAT.Text = Converter.GetString(dt.Rows[0]["STKVat"]);
                txtTAX.Text = Converter.GetString(dt.Rows[0]["STKTax"]);
            }

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            string InsertExp = "UPDATE A2ZSTCHRG SET STKVat = " + txtVAT.Text + ", STKTax = " + txtTAX.Text + " ";
            int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(InsertExp, "A2ZSTMCUS"));
            if(rowEffect > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Update Successfully');", true);
                return;
            }
            
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

    }
}
