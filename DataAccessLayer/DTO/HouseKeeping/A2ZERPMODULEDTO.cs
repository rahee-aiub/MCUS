using System;
using System.Web.UI.WebControls;
using DataAccessLayer.BLL;

namespace DataAccessLayer.DTO.HouseKeeping
{
    public class A2ZERPMODULEDTO
    {
        #region Properties

        public Int16 ModuleNo { set; get; }
        public String ModuleName { set; get; }

        #endregion

        public static DropDownList GetDropDownList(DropDownList ddlModule)
        {
            return CommonManager.Instance.FillDropDownList("SELECT ModuleNo,ModuleName FROM A2ZERPMODULE", ddlModule, "A2ZHKMCUS");
        }
    }
}
