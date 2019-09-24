using System;
using System.Web.UI.WebControls;

using DataAccessLayer.DTO.HumanResource;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.DTO.SystemControl;

using DataAccessLayer.Utility;
using System.Drawing;

using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.DTO;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;


namespace ATOZWEBMCUS.Pages
{
    public partial class HRAllowanceControlMaint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DivMessage.Visible = false;
            try
            {
                if (!IsPostBack)
                {
                    lblColumn1.Visible = false;
                    ddlColumn1.Visible = false;
                    lblColumn2.Visible = false;
                    ddlColumn2.Visible = false;
                    lblColumn3.Visible = false;
                    ddlColumn3.Visible = false;
                    lblColumn4.Visible = false;
                    ddlColumn4.Visible = false;
                    lblColumn5.Visible = false;
                    ddlColumn5.Visible = false;
                    lblColumn6.Visible = false;
                    txtColumn6.Visible = false;
                    lblStat.Visible = false;
                    ChkStatus.Visible = false;

                    lblRecFlag.Text = "0";

                    ChkLocation.Enabled = false;
                    ChkPercentage.Enabled = false;
                    ChkServiceType.Enabled = false;
                    ChkReligion.Enabled = false;

                    btnSubmit.Visible = false;
                    btnUpdate.Visible = false;
                    AllowanceDropdown();

                    ddlAllowance.Focus();
                    //string sqlquery3 = "Truncate table dbo.WFALLOWANCECONTROL";
                    //int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery3, "A2ZHRMCUS"));
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }

        }

        private void GradeDropdown()
        {
            string sqlquery = "SELECT Grade,GradeDesc from A2ZGRADE";
            ddlColumn2 = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlColumn2, "A2ZHRMCUS");
        }

        private void DesigDropdown()
        {
            string sqlquery = "SELECT DesigCode,DesigDescription from A2ZDESIGNATION";
            ddlColumn2 = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlColumn2, "A2ZHRMCUS");
        }


        private void LocationDropdown()
        {
            string sqlquery = "SELECT DistOrgCode,DistDescription from A2ZDISTRICT";
            ddlColumn3 = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlColumn3, "A2ZHKMCUS");

        }

       

        private void ServiceTypeDropdown()
        {
            string sqlquery = "SELECT ServiceType,ServiceName from A2ZSERVICETYPE";
            ddlColumn4 = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlColumn4, "A2ZHRMCUS");
        }

        private void ReligionDropdown()
        {
            string sqlquery = "SELECT RelegionCode,RelegionDescription from A2ZRELIGION";
            ddlColumn5 = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlColumn5, "A2ZHKMCUS");
        }
        private void AllowanceDropdown()
        {
            string sqlquery = "SELECT Code,DEscription from A2ZALLOWANCE WHERE Status !=2";
            ddlAllowance = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownListWithSelect(sqlquery, ddlAllowance, "A2ZHRMCUS");
        }


        private void gvDetail()
        {
            string sqlquery3 = "SELECT Id,AllowCode,BaseGrade,BaseGradeDesc,GradeCode,GradeDesc,DesignationCode,DesigDesc,LocationCode,LocationDesc,ServTypeCode,ServTypeDesc,ReligionCode,ReligionDesc,Amount,Status from A2ZALLOWCTRLDTL WHERE AllowCode = '" + ddlAllowance.SelectedValue + "'";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZHRMCUS");
        }

        protected void ddlAllowance_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAllowance.SelectedValue != "-Select-")
                {
                    //string sqlquery3 = "Truncate table dbo.WFALLOWANCECONTROL ";
                    //int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery3, "A2ZHRMCUS"));
                    lblRecFlag.Text = "0";

                    gvDetailInfo.Visible = false;


                    int MainCode = Converter.GetInteger(ddlAllowance.SelectedValue);


                    A2ZALLOWCTRLDTO getDTO = (A2ZALLOWCTRLDTO.GetInformation(MainCode));
                    if (getDTO.AllowCode > 0)
                    {
                        ddlAllowance.SelectedValue = Converter.GetString(getDTO.AllowCode);
                        ddlDependsOn.SelectedValue = Converter.GetString(getDTO.DependsOn);

                        ChkLocation.Checked = Converter.GetBoolean(getDTO.Location);
                        ChkPercentage.Checked = Converter.GetBoolean(getDTO.Percentage);
                        ChkServiceType.Checked = Converter.GetBoolean(getDTO.ServType);
                        ChkReligion.Checked = Converter.GetBoolean(getDTO.Religion);

                        ChkLocation.Enabled = false;
                        ChkPercentage.Enabled = false;
                        ChkServiceType.Enabled = false;
                        ChkReligion.Enabled = false;
                        ddlDependsOn.Enabled = false;

                        ChkStatus.Checked = false;

                        btnSubmit.Visible = true;
                        btnUpdate.Visible = false;
                        btnDelete.Visible = false;

                        lblRecFlag.Text = "1";

                        EnableInputSection();
                        VisableColumn();
                        SelectVisableColumn();
                        gvDetailInfo.Visible = true;
                        gvDetail();
                        //GetInformation()
                    }
                    else
                    {
                        ddlDependsOn.SelectedIndex = 0;


                        ChkLocation.Checked = false;
                        ChkPercentage.Checked = false;
                        ChkServiceType.Checked = false;
                        ChkReligion.Checked = false;

                        ChkLocation.Enabled = false;
                        ChkPercentage.Enabled = false;
                        ChkServiceType.Enabled = false;
                        ChkReligion.Enabled = false;

                        ddlDependsOn.Enabled = true;

                        ChkStatus.Checked = false;

                        DisableInputSection();

                        btnSubmit.Visible = true;
                        btnUpdate.Visible = false;
                        btnDelete.Visible = false;

                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlAllowance_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }
        }

        protected void ddlDependsOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                //string sqlquery3 = "Truncate table dbo.WFALLOWANCECONTROL ";
                //int result = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlquery3, "A2ZHRMCUS"));

                ChkLocation.Enabled = true;
                ChkPercentage.Enabled = true;
                ChkServiceType.Enabled = true;
                ChkReligion.Enabled = true;

                EnableInputSection();



                //int MainCode = Converter.GetInteger(ddlAllowance.SelectedValue);
                //int SubCode = Converter.GetInteger(ddlDependsOn.SelectedValue);
                //A2ZALLOWCTRLDTO getDTO = (A2ZALLOWCTRLDTO.GetSubInformation(MainCode, SubCode));
                //if (getDTO.AllowCode > 0)
                //{
                //    ddlAllowance.SelectedValue = Converter.GetString(getDTO.AllowCode);
                //    ddlDependsOn.SelectedValue = Converter.GetString(getDTO.DependsOn);
                //    btnSubmit.Visible = false;
                //    btnUpdate.Visible = true;
                //    GetInformation();
                //}
                //else
                //{
                //    btnSubmit.Visible = true;
                //    btnUpdate.Visible = false;
                //    GetInformation();
                //}

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlDependsOn_SelectedIndexChanged Problem');</script>");
                //throw ex;
            }

        }

        private void VisableColumn()
        {
            gvDetailInfo.Columns[3].Visible = true;
            gvDetailInfo.Columns[5].Visible = true;
            gvDetailInfo.Columns[7].Visible = true;
            gvDetailInfo.Columns[9].Visible = true;
            gvDetailInfo.Columns[11].Visible = true;
            gvDetailInfo.Columns[13].Visible = true;
        }

        private void SelectVisableColumn()
        {
            if (ddlDependsOn.SelectedValue == "1")
            {
                gvDetailInfo.Columns[7].Visible = false;
            }
            if (ddlDependsOn.SelectedValue == "2")
            {
                gvDetailInfo.Columns[3].Visible = false;
                gvDetailInfo.Columns[5].Visible = false;
            }
            if (ddlDependsOn.SelectedValue == "3")
            {
                gvDetailInfo.Columns[3].Visible = false;
                gvDetailInfo.Columns[5].Visible = false;
                gvDetailInfo.Columns[7].Visible = false;
            }
            if (ChkLocation.Checked == false)
            {
                gvDetailInfo.Columns[9].Visible = false;
            }
            if (ChkServiceType.Checked == false)
            {
                gvDetailInfo.Columns[11].Visible = false;
            }
            if (ChkReligion.Checked == false)
            {
                gvDetailInfo.Columns[13].Visible = false;
            }

        }

        private void EnableInputSection()
        {
            if (ddlDependsOn.SelectedValue == "1")
            {
                lblColumn1.Text = "BaseGrade";
                lblColumn1.Visible = true;
                ddlColumn1.Visible = true;

                lblColumn2.Text = "Grade";
                lblColumn2.Visible = true;
                ddlColumn2.Visible = true;

                GradeDropdown();
            }
            
            if (ddlDependsOn.SelectedValue == "2")
            {
                lblColumn2.Text = "Designation";
                lblColumn2.Visible = true;
                ddlColumn2.Visible = true;
                DesigDropdown();
            }

            if (ChkLocation.Checked == true)
            {
                lblColumn3.Text = "Location";
                lblColumn3.Visible = true;
                ddlColumn3.Visible = true;
                LocationDropdown();
            }
            else
            {
                lblColumn3.Visible = false;
                ddlColumn3.Visible = false;
            }

            if (ChkServiceType.Checked == true)
            {
                lblColumn4.Text = "Service Type";
                lblColumn4.Visible = true;
                ddlColumn4.Visible = true;
                ServiceTypeDropdown();
            }
            else
            {
                lblColumn4.Visible = false;
                ddlColumn4.Visible = false;
            }

            if (ChkReligion.Checked == true)
            {
                lblColumn5.Text = "Religion";
                lblColumn5.Visible = true;
                ddlColumn5.Visible = true;
                ReligionDropdown();
            }
            else
            {
                lblColumn5.Visible = false;
                ddlColumn5.Visible = false;
            }

            lblColumn6.Visible = true;
            txtColumn6.Visible = true;
            lblStat.Visible = true;
            ChkStatus.Visible = true;

            if (ChkPercentage.Checked == true)
            {
                lblColumn6.Text = "Percentage";
            }
            else
            {
                lblColumn6.Text = "Amount";

            }

        }

        private void DisableInputSection()
        {
            lblColumn1.Visible = false;
            ddlColumn1.Visible = false;
            lblColumn2.Visible = false;
            ddlColumn2.Visible = false;
            lblColumn3.Visible = false;
            ddlColumn3.Visible = false;
            lblColumn4.Visible = false;
            ddlColumn4.Visible = false;
            lblColumn5.Visible = false;
            ddlColumn5.Visible = false;
            lblColumn6.Visible = false;
            txtColumn6.Visible = false;
            lblStat.Visible = false;
            ChkStatus.Visible = false;
        }

        private void BaseGradeMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Please Input Grade');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input BaseGrade');", true);
            return;
        }
        private void GradeMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Please Input Grade');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Grade');", true);
            return;
        }
        private void DesigMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Please Input Designation');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Designation');", true);
            return;
        }

        private void LocationMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Please Input Location');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Location');", true);
            return;
        }

        private void ServTypeMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Please Input Service Type');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Service Type');", true);
            return;
        }

        private void ReligionMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Please Input Religion');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Religion');", true);
            return;
        }

        private void AmountMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Please Input Amount');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Amount');", true);
            return;
        }

        private void DuplicateMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Already Selected');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Already Selected');", true);
            return;
        }
        private void AddValidity()
        {
            MsgFlag.Text = "0";

            if (ddlDependsOn.SelectedValue == "1")
            {
                if (ddlColumn1.SelectedValue == "-Select-")
                {
                    BaseGradeMSG();
                    MsgFlag.Text = "1";
                }
                if (ddlColumn2.SelectedValue == "-Select-")
                {
                    GradeMSG();
                    MsgFlag.Text = "1";
                }

            }
            if (ddlDependsOn.SelectedValue == "2")
            {
                if (ddlColumn2.SelectedValue == "-Select-")
                {
                    DesigMSG();
                    MsgFlag.Text = "1";
                }
            }

            if (ChkLocation.Checked == true)
            {
                if (ddlColumn3.SelectedValue == "-Select-")
                {
                    LocationMSG();
                    MsgFlag.Text = "1";
                }
            }


            if (ChkServiceType.Checked == true)
            {
                if (ddlColumn4.SelectedValue == "-Select-")
                {
                    ServTypeMSG();
                    MsgFlag.Text = "1";
                }
            }

            if (ChkReligion.Checked == true)
            {
                if (ddlColumn5.SelectedValue == "-Select")
                {
                    ReligionMSG();
                    MsgFlag.Text = "1";
                }
            }

            if (txtColumn6.Text == "00.00" || txtColumn6.Text == "")
            {
                AmountMSG();
                MsgFlag.Text = "1";
            }
        }
        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void gvDetailInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                AddValidity();
                if (MsgFlag.Text == "1")
                {
                    return;
                }


                //string strQuery1 = @"DELETE FROM dbo.A2ZALLOWANCECONTROL WHERE AllowCode='" + ddlAllowance.SelectedValue + "'";
                //int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZHRMCUS"));


                A2ZALLOWCTRLDTO objDTO = new A2ZALLOWCTRLDTO();

                A2ZALLOWCTRLDTLDTO obj1DTO = new A2ZALLOWCTRLDTLDTO();

                objDTO.AllowCode = Converter.GetInteger(ddlAllowance.SelectedValue);
                objDTO.AllowDesc = Converter.GetString(ddlAllowance.SelectedItem);
                obj1DTO.AllowCode = Converter.GetInteger(ddlAllowance.SelectedValue);
                obj1DTO.AllowDesc = Converter.GetString(ddlAllowance.SelectedItem);


                objDTO.DependsOn = Converter.GetInteger(ddlDependsOn.SelectedValue);


                objDTO.Location = Converter.GetBoolean(ChkLocation.Checked);
                objDTO.Percentage = Converter.GetBoolean(ChkPercentage.Checked);
                objDTO.ServType = Converter.GetBoolean(ChkServiceType.Checked);
                objDTO.Religion = Converter.GetBoolean(ChkReligion.Checked);

                if (ddlDependsOn.SelectedValue == "1")
                {
                    obj1DTO.BaseGrade = Converter.GetSmallInteger(ddlColumn1.SelectedValue);
                    obj1DTO.BaseGradeDesc = Converter.GetString(ddlColumn1.SelectedItem);

                    obj1DTO.GradeCode = Converter.GetInteger(ddlColumn2.SelectedValue);
                    obj1DTO.GradeDesc = Converter.GetString(ddlColumn2.SelectedItem);
                }
                if (ddlDependsOn.SelectedValue == "2")
                {
                    obj1DTO.DesignationCode = Converter.GetInteger(ddlColumn2.SelectedValue);
                    obj1DTO.DesigDesc = Converter.GetString(ddlColumn2.SelectedItem);
                }

                if (ChkLocation.Checked == true)
                {
                    obj1DTO.LocationCode = Converter.GetInteger(ddlColumn3.SelectedValue);
                    obj1DTO.LocationDesc = Converter.GetString(ddlColumn3.SelectedItem);
                }

                if (ChkServiceType.Checked == true)
                {
                    obj1DTO.ServTypeCode = Converter.GetInteger(ddlColumn4.SelectedValue);
                    obj1DTO.ServTypeDesc = Converter.GetString(ddlColumn4.SelectedItem);
                }

                if (ChkReligion.Checked == true)
                {
                    obj1DTO.ReligionCode = Converter.GetInteger(ddlColumn5.SelectedValue);
                    obj1DTO.ReligionDesc = Converter.GetString(ddlColumn5.SelectedItem);
                }

                obj1DTO.Amount = Converter.GetDecimal(txtColumn6.Text);

                ChkStatus.Checked = true;

                obj1DTO.Status = Converter.GetBoolean(ChkStatus.Checked);


                //obj1DTO.Status = Converter.GetSmallInteger(txtColumn4.Text);



                if (lblRecFlag.Text == "0")
                {
                    int roweffect = A2ZALLOWCTRLDTO.InsertInformation(objDTO);
                    if (roweffect > 0)
                    {
                        lblRecFlag.Text = "1";
                        ChkLocation.Enabled = false;
                        ChkPercentage.Enabled = false;
                        ChkServiceType.Enabled = false;
                        ChkReligion.Enabled = false;
                        ddlDependsOn.Enabled = false;
                    }
                }
                int roweffect1 = A2ZALLOWCTRLDTLDTO.InsertInformation(obj1DTO);
                if (roweffect1 > 0)
                {
                    EnableInputSection();
                    VisableColumn();
                    SelectVisableColumn();
                    gvDetailInfo.Visible = true;
                    gvDetail();
                    if (ddlDependsOn.SelectedValue != "3")
                    {
                        ddlColumn1.SelectedIndex = 0;
                        ddlColumn2.SelectedIndex = 0;
                    }
                    if (ChkLocation.Checked == true)
                    {
                        ddlColumn3.SelectedIndex = 0;
                    }
                    if (ChkServiceType.Checked == true)
                    {
                        ddlColumn4.SelectedIndex = 0;
                    }
                    if (ChkReligion.Checked == true)
                    {
                        ddlColumn5.SelectedIndex = 0;
                    }
                    txtColumn6.Text = string.Empty;
                    ChkStatus.Checked = false;
                    ddlColumn2.Focus();

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.btnSubmit_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                //string strQuery1 = @"DELETE FROM dbo.A2ZALLOWANCECONTROL WHERE AllowCode='" + ddlAllowance.SelectedValue + "'";
                //int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZHRMCUS"));



                A2ZALLOWCTRLDTLDTO objDTO = new A2ZALLOWCTRLDTLDTO();

                objDTO.ID = Converter.GetInteger(lblID.Text);
                objDTO.AllowCode = Converter.GetInteger(ddlAllowance.SelectedValue);
                objDTO.AllowDesc = Converter.GetString(ddlAllowance.SelectedItem);



                if (ddlDependsOn.SelectedValue == "1")
                {
                    objDTO.BaseGrade = Converter.GetSmallInteger(ddlColumn1.SelectedValue);
                    objDTO.BaseGradeDesc = Converter.GetString(ddlColumn1.SelectedItem);

                    objDTO.GradeCode = Converter.GetInteger(ddlColumn2.SelectedValue);
                    objDTO.GradeDesc = Converter.GetString(ddlColumn2.SelectedItem);
                }
                if (ddlDependsOn.SelectedValue == "2")
                {
                    objDTO.DesignationCode = Converter.GetInteger(ddlColumn2.SelectedValue);
                    objDTO.DesigDesc = Converter.GetString(ddlColumn2.SelectedItem);
                }

                if (ChkLocation.Checked == true)
                {
                    objDTO.LocationCode = Converter.GetInteger(ddlColumn3.SelectedValue);
                    objDTO.LocationDesc = Converter.GetString(ddlColumn3.SelectedItem);
                }

                if (ChkServiceType.Checked == true)
                {
                    objDTO.ServTypeCode = Converter.GetInteger(ddlColumn4.SelectedValue);
                    objDTO.ServTypeDesc = Converter.GetString(ddlColumn4.SelectedItem);
                }

                if (ChkReligion.Checked == true)
                {
                    objDTO.ReligionCode = Converter.GetInteger(ddlColumn5.SelectedValue);
                    objDTO.ReligionDesc = Converter.GetString(ddlColumn5.SelectedItem);
                }

                objDTO.Amount = Converter.GetDecimal(txtColumn6.Text);



                objDTO.Status = Converter.GetBoolean(ChkStatus.Checked);


                //obj1DTO.Status = Converter.GetSmallInteger(txtColumn4.Text);

                int roweffect1 = A2ZALLOWCTRLDTLDTO.UpdateInformation(objDTO);
                if (roweffect1 > 0)
                {
                    EnableInputSection();
                    VisableColumn();
                    SelectVisableColumn();
                    gvDetailInfo.Visible = true;
                    gvDetail();
                    if (ddlDependsOn.SelectedValue != "3")
                    {
                        ddlColumn1.SelectedIndex = 0;
                        ddlColumn2.SelectedIndex = 0;
                    }

                    if (ChkLocation.Checked == true)
                    {
                        ddlColumn3.SelectedIndex = 0;
                    }
                    if (ChkServiceType.Checked == true)
                    {
                        ddlColumn4.SelectedIndex = 0;
                    }
                    if (ChkReligion.Checked == true)
                    {
                        ddlColumn5.SelectedIndex = 0;
                    }
                    lblID.Text = string.Empty;
                    txtColumn6.Text = string.Empty;
                    ChkStatus.Checked = false;
                    ddlColumn1.Focus();

                    btnSubmit.Visible = true;
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.btnUpdate_Click Problem');</script>");
               
                //throw ex;
            }


        }

        protected void ChkLocation_CheckedChanged(object sender, EventArgs e)
        {
            EnableInputSection();

        }

        protected void ChkPercentage_CheckedChanged(object sender, EventArgs e)
        {
            EnableInputSection();

        }

        protected void ChkServiceType_CheckedChanged(object sender, EventArgs e)
        {
            EnableInputSection();

        }

        protected void ChkReligion_CheckedChanged(object sender, EventArgs e)
        {
            EnableInputSection();
        }


        private void ReadValidity()
        {
            ReadFlag.Text = "0";

            if (ddlDependsOn.SelectedValue == "1" &&
                ChkLocation.Checked == true &&
                ChkServiceType.Checked == true &&
                ChkReligion.Checked == true)
            {
                ReadFlag.Text = "1111";
            }
            if (ddlDependsOn.SelectedValue == "1" &&
                ChkLocation.Checked == true &&
                ChkServiceType.Checked == true &&
                ChkReligion.Checked == false)
            {
                ReadFlag.Text = "1110";
            }
            if (ddlDependsOn.SelectedValue == "1" &&
                ChkLocation.Checked == true &&
                ChkServiceType.Checked == false &&
                ChkReligion.Checked == false)
            {
                ReadFlag.Text = "1100";
            }
            if (ddlDependsOn.SelectedValue == "1" &&
                ChkLocation.Checked == true &&
                ChkServiceType.Checked == false &&
                ChkReligion.Checked == true)
            {
                ReadFlag.Text = "1101";
            }
            if (ddlDependsOn.SelectedValue == "1" &&
                ChkLocation.Checked == false &&
                ChkServiceType.Checked == false &&
                ChkReligion.Checked == false)
            {
                ReadFlag.Text = "1000";
            }
            if (ddlDependsOn.SelectedValue == "1" &&
                ChkLocation.Checked == false &&
                ChkServiceType.Checked == false &&
                ChkReligion.Checked == true)
            {
                ReadFlag.Text = "1001";
            }
            if (ddlDependsOn.SelectedValue == "1" &&
                ChkLocation.Checked == false &&
                ChkServiceType.Checked == true &&
                ChkReligion.Checked == true)
            {
                ReadFlag.Text = "1011";
            }

            if (ddlDependsOn.SelectedValue == "1" &&
                ChkLocation.Checked == false &&
                ChkServiceType.Checked == true &&
                ChkReligion.Checked == false)
            {
                ReadFlag.Text = "1010";
            }

            //--------------------------------------------------------------------

            if (ddlDependsOn.SelectedValue == "2" &&
                ChkLocation.Checked == true &&
                ChkServiceType.Checked == true &&
                ChkReligion.Checked == true)
            {
                ReadFlag.Text = "2111";
            }
            if (ddlDependsOn.SelectedValue == "2" &&
                ChkLocation.Checked == true &&
                ChkServiceType.Checked == true &&
                ChkReligion.Checked == false)
            {
                ReadFlag.Text = "2110";
            }
            if (ddlDependsOn.SelectedValue == "2" &&
                ChkLocation.Checked == true &&
                ChkServiceType.Checked == false &&
                ChkReligion.Checked == false)
            {
                ReadFlag.Text = "2100";
            }
            if (ddlDependsOn.SelectedValue == "2" &&
                ChkLocation.Checked == true &&
                ChkServiceType.Checked == false &&
                ChkReligion.Checked == true)
            {
                ReadFlag.Text = "2101";
            }
            if (ddlDependsOn.SelectedValue == "2" &&
                ChkLocation.Checked == false &&
                ChkServiceType.Checked == false &&
                ChkReligion.Checked == false)
            {
                ReadFlag.Text = "2000";
            }
            if (ddlDependsOn.SelectedValue == "2" &&
                ChkLocation.Checked == false &&
                ChkServiceType.Checked == false &&
                ChkReligion.Checked == true)
            {

            }
            if (ddlDependsOn.SelectedValue == "2" &&
                ChkLocation.Checked == false &&
                ChkServiceType.Checked == true &&
                ChkReligion.Checked == true)
            {
                ReadFlag.Text = "2011";
            }
            if (ddlDependsOn.SelectedValue == "2" &&
                ChkLocation.Checked == false &&
                ChkServiceType.Checked == true &&
                ChkReligion.Checked == false)
            {
                ReadFlag.Text = "2010";
            }

        }

        


        protected void ddlColumn2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                ReadValidity();

                if (ReadFlag.Text == "1000")
                {
                    string qry1 = "SELECT Id FROM A2ZALLOWCTRLDTL WHERE AllowCode='" + ddlAllowance.SelectedValue + "' AND BaseGrade='" + ddlColumn1.SelectedValue + "' AND GradeCode='" + ddlColumn2.SelectedValue + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        DuplicateMSG();
                        ddlColumn2.SelectedIndex = 0;
                        ddlColumn2.Focus();
                        return;
                    }
                }

                if (ReadFlag.Text == "2000")
                {
                    string qry1 = "SELECT Id FROM A2ZALLOWCTRLDTL WHERE AllowCode='" + ddlAllowance.SelectedValue + "' AND DesignationCode='" + ddlColumn2.SelectedValue + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        DuplicateMSG();
                        ddlColumn2.SelectedIndex = 0;
                        ddlColumn2.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlColumn1_SelectedIndexChanged Problem');</script>");

                //throw ex;
            }
        }
        protected void ddlColumn3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                ReadValidity();

                if (ddlDependsOn.SelectedValue != "3" && ddlColumn2.SelectedValue == "-Select-")
                {
                    GradeMSG();
                    return;
                }


                if (ReadFlag.Text == "1100")
                {
                    string qry1 = "SELECT Id FROM A2ZALLOWCTRLDTL WHERE AllowCode='" + ddlAllowance.SelectedValue + "' AND BaseGrade='" + ddlColumn1.SelectedValue + "' AND GradeCode='" + ddlColumn2.SelectedValue + "' AND LocationCode='" + ddlColumn3.SelectedValue + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        DuplicateMSG();
                        if (ddlDependsOn.SelectedValue != "3")
                        {
                            ddlColumn2.SelectedIndex = 0;
                        }

                        ddlColumn3.SelectedIndex = 0;
                        ddlColumn3.Focus();
                        return;
                    }
                }


                if (ReadFlag.Text == "2100")
                {
                    string qry1 = "SELECT Id FROM A2ZALLOWCTRLDTL WHERE AllowCode='" + ddlAllowance.SelectedValue + "' AND DesignationCode='" + ddlColumn2.SelectedValue + "' AND LocationCode='" + ddlColumn3.SelectedValue + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        DuplicateMSG();
                        if (ddlDependsOn.SelectedValue != "3")
                        {
                            ddlColumn2.SelectedIndex = 0;
                        }

                        ddlColumn3.SelectedIndex = 0;
                        ddlColumn3.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlColumn2_SelectedIndexChanged Problem');</script>");
                
                //throw ex;
            }
        }

        protected void ddlColumn4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                ReadValidity();

                if (ddlDependsOn.SelectedValue != "3" && ddlColumn2.SelectedValue == "-Select-")
                {
                    GradeMSG();
                    return;
                }

                if (ddlDependsOn.SelectedValue != "3" && ddlColumn3.SelectedValue == "-Select-")
                {
                    LocationMSG();
                    return;
                }


                if (ReadFlag.Text == "1110")
                {
                    string qry1 = "SELECT Id FROM A2ZALLOWCTRLDTL WHERE AllowCode='" + ddlAllowance.SelectedValue + "' AND BaseGrade='" + ddlColumn1.SelectedValue + "' AND GradeCode='" + ddlColumn2.SelectedValue + "' AND LocationCode='" + ddlColumn3.SelectedValue + "' AND ServTypeCode='" + ddlColumn4.SelectedValue + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        DuplicateMSG();
                        if (ddlDependsOn.SelectedValue != "3")
                        {
                            ddlColumn2.SelectedIndex = 0;
                        }

                        ddlColumn3.SelectedIndex = 0;
                        ddlColumn4.SelectedIndex = 0;
                        ddlColumn4.Focus();
                        return;
                    }
                }

                if (ReadFlag.Text == "2110")
                {
                    string qry1 = "SELECT Id FROM A2ZALLOWCTRLDTL WHERE AllowCode='" + ddlAllowance.SelectedValue + "' AND DesignationCode='" + ddlColumn2.SelectedValue + "' AND LocationCode='" + ddlColumn3.SelectedValue + "' AND ServTypeCode='" + ddlColumn4.SelectedValue + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        DuplicateMSG();
                        if (ddlDependsOn.SelectedValue != "3")
                        {
                            ddlColumn2.SelectedIndex = 0;
                        }

                        ddlColumn3.SelectedIndex = 0;
                        ddlColumn4.SelectedIndex = 0;
                        ddlColumn4.Focus();
                        return;
                    }
                }

                if (ReadFlag.Text == "1010")
                {
                    string qry1 = "SELECT Id FROM A2ZALLOWCTRLDTL WHERE AllowCode='" + ddlAllowance.SelectedValue + "' AND BaseGrade='" + ddlColumn1.SelectedValue + "' AND GradeCode='" + ddlColumn2.SelectedValue + "' AND ServTypeCode='" + ddlColumn4.SelectedValue + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        DuplicateMSG();
                        if (ddlDependsOn.SelectedValue != "3")
                        {
                            ddlColumn2.SelectedIndex = 0;
                        }

                        ddlColumn4.SelectedIndex = 0;
                        ddlColumn4.Focus();
                        return;
                    }
                }

                if (ReadFlag.Text == "2010")
                {
                    string qry1 = "SELECT Id FROM A2ZALLOWCTRLDTL WHERE AllowCode='" + ddlAllowance.SelectedValue + "' AND DesignationCode='" + ddlColumn2.SelectedValue + "' AND ServTypeCode='" + ddlColumn4.SelectedValue + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        DuplicateMSG();
                        if (ddlDependsOn.SelectedValue != "3")
                        {
                            ddlColumn2.SelectedIndex = 0;
                        }

                        ddlColumn4.SelectedIndex = 0;
                        ddlColumn4.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlColumn3_SelectedIndexChanged Problem');</script>");
                
                //throw ex;
            }
        }

        protected void ddlColumn5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                ReadValidity();

                if (ddlDependsOn.SelectedValue != "3" && ddlColumn2.SelectedValue == "-Select-")
                {
                    GradeMSG();
                    return;
                }

                if (ddlDependsOn.SelectedValue != "3" && ddlColumn3.SelectedValue == "-Select-")
                {
                    LocationMSG();
                    return;
                }

                if (ddlDependsOn.SelectedValue != "3" && ddlColumn4.SelectedValue == "-Select-")
                {
                    ServTypeMSG();
                    return;
                }


                if (ReadFlag.Text == "1111")
                {
                    string qry1 = "SELECT Id FROM A2ZALLOWCTRLDTL WHERE AllowCode='" + ddlAllowance.SelectedValue + "' AND BaseGrade='" + ddlColumn1.SelectedValue + "' AND GradeCode='" + ddlColumn2.SelectedValue + "' AND LocationCode='" + ddlColumn3.SelectedValue + "' AND ServTypeCode='" + ddlColumn4.SelectedValue + "' AND ReligionCode='" + ddlColumn5.SelectedValue + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        DuplicateMSG();
                        if (ddlDependsOn.SelectedValue != "3")
                        {
                            ddlColumn2.SelectedIndex = 0;
                        }

                        ddlColumn3.SelectedIndex = 0;
                        ddlColumn4.SelectedIndex = 0;
                        ddlColumn5.SelectedIndex = 0;
                        ddlColumn5.Focus();
                        return;
                    }
                }

                if (ReadFlag.Text == "2111")
                {
                    string qry1 = "SELECT Id FROM A2ZALLOWCTRLDTL WHERE AllowCode='" + ddlAllowance.SelectedValue + "' AND DesignationCode='" + ddlColumn2.SelectedValue + "' AND LocationCode='" + ddlColumn3.SelectedValue + "' AND ServTypeCode='" + ddlColumn4.SelectedValue + "' AND ReligionCode='" + ddlColumn5.SelectedValue + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        DuplicateMSG();
                        if (ddlDependsOn.SelectedValue != "3")
                        {
                            ddlColumn2.SelectedIndex = 0;
                        }

                        ddlColumn3.SelectedIndex = 0;
                        ddlColumn4.SelectedIndex = 0;
                        ddlColumn5.SelectedIndex = 0;
                        ddlColumn5.Focus();
                        return;
                    }
                }

                if (ReadFlag.Text == "1101")
                {
                    string qry1 = "SELECT Id FROM A2ZALLOWCTRLDTL WHERE AllowCode='" + ddlAllowance.SelectedValue + "' AND BaseGrade='" + ddlColumn1.SelectedValue + "' AND GradeCode='" + ddlColumn2.SelectedValue + "' AND LocationCode='" + ddlColumn3.SelectedValue + "' AND ReligionCode='" + ddlColumn5.SelectedValue + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        DuplicateMSG();
                        if (ddlDependsOn.SelectedValue != "3")
                        {
                            ddlColumn2.SelectedIndex = 0;
                        }

                        ddlColumn3.SelectedIndex = 0;
                        ddlColumn5.SelectedIndex = 0;
                        ddlColumn5.Focus();
                        return;
                    }
                }

                if (ReadFlag.Text == "2101")
                {
                    string qry1 = "SELECT Id FROM A2ZALLOWCTRLDTL WHERE AllowCode='" + ddlAllowance.SelectedValue + "' AND DesignationCode='" + ddlColumn2.SelectedValue + "' AND LocationCode='" + ddlColumn3.SelectedValue + "' AND ReligionCode='" + ddlColumn5.SelectedValue + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        DuplicateMSG();
                        if (ddlDependsOn.SelectedValue != "3")
                        {
                            ddlColumn2.SelectedIndex = 0;
                        }

                        ddlColumn3.SelectedIndex = 0;
                        ddlColumn5.SelectedIndex = 0;
                        ddlColumn5.Focus();
                        return;
                    }
                }


                if (ReadFlag.Text == "1001")
                {
                    string qry1 = "SELECT Id FROM A2ZALLOWCTRLDTL WHERE AllowCode='" + ddlAllowance.SelectedValue + "' AND BaseGrade='" + ddlColumn1.SelectedValue + "' AND GradeCode='" + ddlColumn2.SelectedValue + "' AND ReligionCode='" + ddlColumn5.SelectedValue + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        DuplicateMSG();
                        if (ddlDependsOn.SelectedValue != "3")
                        {
                            ddlColumn2.SelectedIndex = 0;
                        }

                        ddlColumn5.SelectedIndex = 0;
                        ddlColumn5.Focus();
                        return;
                    }
                }

                if (ReadFlag.Text == "2001")
                {
                    string qry1 = "SELECT Id FROM A2ZALLOWCTRLDTL WHERE AllowCode='" + ddlAllowance.SelectedValue + "' AND DesignationCode='" + ddlColumn2.SelectedValue + "' AND ReligionCode='" + ddlColumn5.SelectedValue + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        DuplicateMSG();
                        if (ddlDependsOn.SelectedValue != "3")
                        {
                            ddlColumn2.SelectedIndex = 0;
                        }

                        ddlColumn5.SelectedIndex = 0;
                        ddlColumn5.Focus();
                        return;
                    }
                }

                if (ReadFlag.Text == "1011")
                {
                    string qry1 = "SELECT Id FROM A2ZALLOWCTRLDTL WHERE AllowCode='" + ddlAllowance.SelectedValue + "' AND BaseGrade='" + ddlColumn1.SelectedValue + "' AND GradeCode='" + ddlColumn2.SelectedValue + "' AND ServTypeCode='" + ddlColumn4.SelectedValue + "' AND ReligionCode='" + ddlColumn5.SelectedValue + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        DuplicateMSG();
                        if (ddlDependsOn.SelectedValue != "3")
                        {
                            ddlColumn2.SelectedIndex = 0;
                        }

                        ddlColumn4.SelectedIndex = 0;
                        ddlColumn5.SelectedIndex = 0;
                        ddlColumn5.Focus();
                        return;
                    }
                }

                if (ReadFlag.Text == "2011")
                {
                    string qry1 = "SELECT Id FROM A2ZALLOWCTRLDTL WHERE AllowCode='" + ddlAllowance.SelectedValue + "' AND DesignationCode='" + ddlColumn2.SelectedValue + "' AND ServTypeCode='" + ddlColumn4.SelectedValue + "' AND ReligionCode='" + ddlColumn5.SelectedValue + "'";
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        DuplicateMSG();
                        if (ddlDependsOn.SelectedValue != "3")
                        {
                            ddlColumn2.SelectedIndex = 0;
                        }

                        ddlColumn4.SelectedIndex = 0;
                        ddlColumn5.SelectedIndex = 0;
                        ddlColumn5.Focus();
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlColumn4_SelectedIndexChanged Problem');</script>");
                
                //throw ex;
            }
        }

        protected void gvDetailInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                GridViewRow r = gvDetailInfo.SelectedRow;
                Label lblId = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[0].FindControl("lblId");
                lblID.Text = lblId.Text;

                if (ddlDependsOn.SelectedValue == "1")
                {
                    Label lblBaseGrade = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[2].FindControl("lblBaseGrade");
                    Label lblBaseGradeDesc = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[3].FindControl("lblBaseGradeDesc");

                    Label lblGradeCode = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[4].FindControl("lblGrade");
                    Label lblGradeDesc = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[5].FindControl("lblGradeDesc");

                    if (lblGradeDesc.Text != "")
                    {
                        ddlColumn1.SelectedValue = lblBaseGrade.Text;
                        ddlColumn1.SelectedItem.Text = lblBaseGradeDesc.Text;

                        ddlColumn2.SelectedValue = lblGradeCode.Text;
                        ddlColumn2.SelectedItem.Text = lblGradeDesc.Text;
                    }
                }

                if (ddlDependsOn.SelectedValue == "2")
                {
                    Label lblDesigCode = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[6].FindControl("lblDesig");
                    Label lblDesigDesc = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[7].FindControl("lblDesigDesc");
                    if (lblDesigDesc.Text != "")
                    {
                        ddlColumn2.SelectedValue = lblDesigCode.Text;
                        ddlColumn2.SelectedItem.Text = lblDesigDesc.Text;
                    }
                }

                Label lblLocationCode = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[8].FindControl("lblLocation");
                Label lblLocationDesc = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[9].FindControl("lblLocationDesc");
                if (lblLocationDesc.Text != "")
                {
                    ddlColumn3.SelectedValue = lblLocationCode.Text;
                    ddlColumn3.SelectedItem.Text = lblLocationDesc.Text;
                }
                Label lblServTypeCode = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[10].FindControl("lblServType");
                Label lblServTypeDesc = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[11].FindControl("lblServTypeDesc");
                if (lblServTypeDesc.Text != "")
                {
                    ddlColumn4.SelectedValue = lblServTypeCode.Text;
                    ddlColumn4.SelectedItem.Text = lblServTypeDesc.Text;
                }
                Label lblReligionCode = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[12].FindControl("lblReligion");
                Label lblReligionDesc = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[13].FindControl("lblReligionDesc");
                if (lblReligionDesc.Text != "")
                {
                    ddlColumn5.SelectedValue = lblReligionCode.Text;
                    ddlColumn5.SelectedItem.Text = lblReligionDesc.Text;
                }
                Label lblAmount = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[14].FindControl("lblAmount");
                txtColumn6.Text = (lblAmount.Text);

                Label lblStat = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[15].FindControl("lblStat");
                if (lblStat.Text == "True")
                {
                    ChkStatus.Checked = true;
                }
                else
                {
                    ChkStatus.Checked = false;
                }


                btnSubmit.Visible = false;
                btnUpdate.Visible = true;
                btnDelete.Visible = true;

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDetailInfo_SelectedIndexChanged Problem');</script>");
                
                //throw ex;
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = Converter.GetInteger(lblID.Text);

                string strQuery1 = "DELETE FROM A2ZALLOWCTRLDTL WHERE Id='" + ID + "'";
                int status1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZHRMCUS"));

                string qry1 = "SELECT Id FROM A2ZALLOWCTRLDTL WHERE AllowCode='" + ddlAllowance.SelectedValue + "'";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
                if (dt1.Rows.Count == 0)
                {
                    string qry2 = "DELETE FROM A2ZALLOWCTRL WHERE AllowCode='" + ddlAllowance.SelectedValue + "'";
                    int status2 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(qry2, "A2ZHRMCUS"));

                    lblRecFlag.Text = "0";
                    ChkLocation.Enabled = true;
                    ChkPercentage.Enabled = true;
                    ChkServiceType.Enabled = true;
                    ChkReligion.Enabled = true;
                    ddlDependsOn.Enabled = true;
                }

                EnableInputSection();
                VisableColumn();
                SelectVisableColumn();
                gvDetailInfo.Visible = true;
                gvDetail();
                ddlColumn2.SelectedIndex = 0;
                if (ChkLocation.Checked == true)
                {
                    ddlColumn3.SelectedIndex = 0;
                }
                if (ChkServiceType.Checked == true)
                {
                    ddlColumn4.SelectedIndex = 0;
                }
                if (ChkReligion.Checked == true)
                {
                    ddlColumn5.SelectedIndex = 0;
                }
                txtColumn6.Text = string.Empty;
                lblID.Text = string.Empty;
                ChkStatus.Checked = false;
                ddlColumn2.Focus();

                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.btnDelete_Click Problem');</script>");
                
                //throw ex;
            }
        }

    }
}
