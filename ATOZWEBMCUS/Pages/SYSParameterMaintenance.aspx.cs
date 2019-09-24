using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class SYSParameterMaintenance : System.Web.UI.Page
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
            A2ZERPSYSPRMDTO dto = A2ZERPSYSPRMDTO.GetParameterValue();
            txtUnitName.Text = Converter.GetString(dto.PrmUnitName);
            txtAddressL1.Text = Converter.GetString(dto.PrmUnitAdd1);
            txtAddressL2.Text = Converter.GetString(dto.PrmUnitAdd2);
            txtAddressL3.Text = Converter.GetString(dto.PrmUnitAdd3);
            txtTelNo.Text = Converter.GetString(dto.PrmUnitPhone);
            txtSysPath.Text = Converter.GetString(dto.PrmSystemPath);
            txtDataPath.Text = Converter.GetString(dto.PrmDataPath);
            txtBackPath.Text = Converter.GetString(dto.PrmBackUpPath);
            txtTimeOut.Text = Converter.GetString(dto.PrmTimeOut);
            txtInDataPath.Text = Converter.GetString(dto.PrmInDataPath);
            txtOutDataPath.Text = Converter.GetString(dto.PrmOutDataPath);
        }

        protected void ClearInfo()
        {
            txtUnitName.Text = string.Empty;
            txtAddressL1.Text = string.Empty;
            txtAddressL2.Text = string.Empty;
            txtAddressL3.Text = string.Empty;
            txtTelNo.Text = string.Empty;
            txtSysPath.Text = string.Empty;
            txtDataPath.Text = string.Empty;
            txtBackPath.Text = string.Empty;
            txtTimeOut.Text = string.Empty;
            txtInDataPath.Text = string.Empty;
            txtOutDataPath.Text = string.Empty;
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            A2ZERPSYSPRMDTO objDTO = new A2ZERPSYSPRMDTO();
            objDTO.PrmUnitAdd1 = Converter.GetString(txtAddressL1.Text);
            objDTO.PrmUnitAdd2 = Converter.GetString(txtAddressL2.Text);
            objDTO.PrmUnitAdd3 = Converter.GetString(txtAddressL3.Text);
            objDTO.PrmUnitPhone = Converter.GetString(txtTelNo.Text);
            objDTO.PrmSystemPath = Converter.GetString(txtSysPath.Text);
            objDTO.PrmDataPath = Converter.GetString(txtDataPath.Text);
            objDTO.PrmBackUpPath = Converter.GetString(txtBackPath.Text);
            objDTO.PrmTimeOut = Converter.GetInteger(txtTimeOut.Text);
            objDTO.PrmInDataPath = Converter.GetString(txtInDataPath.Text);
            objDTO.PrmOutDataPath = Converter.GetString(txtOutDataPath.Text);

            int roweffect = A2ZERPSYSPRMDTO.UpdateInformation(objDTO);
            if(roweffect>0)
            {
                ClearInfo();
                BtnUpdate.Enabled = false;
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
            //    String cstext1 = "alert('System Parameter Update Sucessfully Done');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('System Parameter Update Sucessfully Done');", true);
            return;

        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }
    }
}