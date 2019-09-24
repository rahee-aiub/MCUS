﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.Utility;
using DataAccessLayer.Constants;
using ATOZWEBMCUS.WebSessionStore;
using System.Data;
using DataAccessLayer.BLL;
using System.Data.SqlClient;

using DataAccessLayer.DTO.HouseKeeping;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSReEditCreditUnionMaintenance : System.Web.UI.Page
    {
        protected Int32 userPermission;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmsg1.Visible = false;
                lblmsg2.Visible = false;
                userPermission = Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION));

                if (userPermission != 10)
                {
                    string notifyMsg = "?txtOne=" + Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME)) +
                                       "&txtTwo=" + "You Don't Have Permission for ReEdit" +
                                       "&txtThree=" + "Contact Your Super User" + "&PreviousMenu=A2ZERPModule.aspx";
                    Server.Transfer("Notify.aspx" + notifyMsg);
                }
                else
                {

                    string CheckQuery = "SELECT CuType,CuTypeName,CuNo,CuName,CuOpDt,CuProcDesc FROM A2ZCUAPPLICATION Where CuProcFlag = '10' and CuStatus !='99'" ;
                    DataTable dt = new DataTable();
                    dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");

                    if (dt.Rows.Count <= 0)
                    {
                        string notifyMsg = "?txtOne=" + Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME)) +
                                           "&txtTwo=" + "No Record Found for ReEdit" +
                                           "&txtThree=" + "Contact For Record" + "&PreviousMenu=A2ZERPModule.aspx";
                        Server.Transfer("Notify.aspx" + notifyMsg);

                    }
                    gvVerify();
                }

            }
        }


        protected void gvVerify()
        {
            string sqlquery3 = "SELECT CuType,CuTypeName,CuNo,CuName,CuOpDt,CuProcDesc from A2ZCUAPPLICATION where CuProcFlag='10' and CuStatus !='99'";
            gvCUInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvCUInfo, "A2ZCSMCUS");
        }

        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            
            try
            {
                Button c = (Button)sender;
                GridViewRow r = (GridViewRow)c.NamingContainer;
                Label CrNo = (Label)gvCUInfo.Rows[r.RowIndex].Cells[4].FindControl("lblcno");
                int cu = Converter.GetSmallInteger(CrNo.Text);
                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, cu);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptReEditReport");

                Response.Redirect("ReportServer.aspx", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnReEdit_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;
            Label CTyNo = (Label)gvCUInfo.Rows[r.RowIndex].Cells[3].FindControl("lblcutype");
            Label CrNo = (Label)gvCUInfo.Rows[r.RowIndex].Cells[4].FindControl("lblcno");
            int a = Converter.GetSmallInteger(CTyNo.Text);
            int c = Converter.GetSmallInteger(CrNo.Text);

            Int16 CuType = Converter.GetSmallInteger(a);
            int CNo = Converter.GetSmallInteger(c);
            A2ZCUAPPLICATIONDTO objDTO = (A2ZCUAPPLICATIONDTO.GetInformation(CuType, CNo));

            objDTO.CuProcFlag = Converter.GetSmallInteger(21);
            objDTO.CuProcDesc = "ReEdited";
            objDTO.VerifyBy = Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_ID));
            A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
            objDTO.VerifyByDate = Converter.GetDateTime(dto.ProcessDate);
            int roweffect = A2ZCUAPPLICATIONDTO.UpdateInformation(objDTO);
            if (roweffect > 0)
            {
                gvVerify();
                string CheckQuery = "SELECT CuType,CuTypeName,CuNo,CuName,CuOpDt,CuProcDesc FROM A2ZCUAPPLICATION Where CuProcFlag = '10' and CuStatus !='99'";
                DataTable dt = new DataTable();
                dt = CommonManager.Instance.GetDataTableByQuery(CheckQuery, "A2ZCSMCUS");

                if (dt.Rows.Count <= 0)
                {
                    DivGridViewCancle.Visible = false;
                    lblmsg1.Visible = true;
                    lblmsg2.Visible = true;

                }

            }


        }

            


        private void Successful()
        {         
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('ReEdit successfully completed.');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('ReEdit successfully completed');", true);
            return;

        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }
       

    }
}