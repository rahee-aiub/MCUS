using System;
using System.Web;
using System.Web.UI;
using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;

namespace ATOZWEBMCUS.Pages
{
    public partial class A2ZERPModule :PageBase
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //Put default value from enum
            Enums.ModuleConstant masterPage = Enums.ModuleConstant.HouseKeeping;
            if (SessionStore.ContainsKey(Params.SYS_SELECT_MODULE))//if (Session["LoadSelectedModule"] != null)
            {
                masterPage = (Enums.ModuleConstant)SessionStore.GetValue(Params.SYS_SELECT_MODULE);//Session["LoadSelectedModule"].ToString();
            }
            switch (masterPage)
            {
                case Enums.ModuleConstant.CustomerService:
                    this.Page.MasterPageFile = "~/MasterPages/CustomerServicesMenuMasterPage.Master";
                    break;
                case Enums.ModuleConstant.GeneralLedger:
                    this.Page.MasterPageFile = "~/MasterPages/GLMenuMasterPage.Master";
                    break;
                case Enums.ModuleConstant.HouseKeeping:
                    this.Page.MasterPageFile = "~/MasterPages/HKMenuMasterPage.Master";
                    break;
                case Enums.ModuleConstant.HumanResource:
                    this.Page.MasterPageFile = "~/MasterPages/HRMenuMasterPage.Master";
                    break;
                case Enums.ModuleConstant.Inventory:
                    this.Page.MasterPageFile = "~/MasterPages/INVMenuMasterPage.Master";
                    break;
                case Enums.ModuleConstant.Booth:
                    this.Page.MasterPageFile = "~/MasterPages/BoothMenuMasterPage.Master";
                    break;
                case Enums.ModuleConstant.OffBooth:
                    this.Page.MasterPageFile = "~/MasterPages/OffBoothMenuMasterPage.Master";
                    break;
                default:
                    Response.Redirect("A2ZERP.aspx");
                    break;
            }
        }
    }
}
