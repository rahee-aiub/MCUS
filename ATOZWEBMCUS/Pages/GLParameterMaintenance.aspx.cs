using DataAccessLayer.DTO.GeneralLedger;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class GLParameterMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GetParameter();
            }
        }

        protected void GetParameter()
        {
            A2ZGLPARAMETERDTO dto = A2ZGLPARAMETERDTO.GetParameterValue();
            txtBegMonth.Text = Converter.GetString(dto.FinancialMonth);
            txtGLPLCode.Text = Converter.GetString(dto.PLCode);
            txtLYrPLCode.Text = Converter.GetString(dto.UDPLCode);
        }

        protected void ClearInfo()
        {
            txtBegMonth.Text = string.Empty;
            txtGLPLCode.Text = string.Empty;
            txtLYrPLCode.Text = string.Empty;
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            A2ZGLPARAMETERDTO objDTO = new A2ZGLPARAMETERDTO();
            objDTO.FinancialMonth = Converter.GetSmallInteger(txtBegMonth.Text);
            objDTO.PLCode = Converter.GetInteger(txtGLPLCode.Text);
            objDTO.UDPLCode = Converter.GetInteger(txtLYrPLCode.Text);

            int roweffect = A2ZGLPARAMETERDTO.UpdateInformation(objDTO);
            if(roweffect>0)
            {
                ClearInfo();
                Sucessfull();
            }
        }

        protected void Sucessfull()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('GL Parameter Update Sucessfully Done');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('GL Parameter Update Sucessfully Done');", true);
            return;

        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }



    }
}