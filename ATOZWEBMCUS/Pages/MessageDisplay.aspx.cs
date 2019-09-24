using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class MessageDisplay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox1.Text=Request.QueryString["msg"];
            TextBox2.Text = Request.QueryString["PreviousMenu"];            
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect(TextBox2.Text);
        }
    }
}