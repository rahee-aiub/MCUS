using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.HumanResource;
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.DTO.CustomerServices;

namespace ATOZWEBMCUS.Pages
{
    public partial class HREmpIncrementMaint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    txtEmpNo.Focus();

                    txtNewPayScale.ReadOnly = true;
                    txtNewBasic.ReadOnly = true;
                    txtEmpNo.ReadOnly = false;


                    A2ZHRPARAMETERDTO dto = A2ZHRPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    txtNewIncrementDate.Text = date;

                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }


        protected void gvDetail()
        {
            try
            {
                string sqlquery4 = "SELECT EmpCode,EmpIncrementDate,EmpNewBaseGradeDesc,EmpNewGrade,EmpNewPayScaleDesc,EmpNewPayLabel,EmpNewBasic FROM A2ZHRINCREMENT WHERE EmpCode= '" + txtEmpNo.Text + "'";

                gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery4, gvDetailInfo, "A2ZHRMCUS");
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvDetail Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void InvalidEmpCodeMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Employee's No.');", true);
            return;

        }
        protected void btnSumbit_Click(object sender, EventArgs e)
        {
            try
            {

                int EmpID = Converter.GetInteger(txtEmpNo.Text);
                A2ZEMPLOYEEDTO getDTO = (A2ZEMPLOYEEDTO.GetInformation(EmpID));
                if (getDTO.EmployeeID > 0)
                {
                    txtEmpNo.ReadOnly = true;
                    txtNewIncrementDate.Focus();
                    lblName.Text = Converter.GetString(getDTO.EmployeeName);


                    lblLastBaseGradeCode.Text = Converter.GetString(getDTO.EmpBaseGrade);
                    if (lblLastBaseGradeCode.Text == string.Empty || lblLastBaseGradeCode.Text == "0")
                    {
                        ddlNewBaseGrade.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlNewBaseGrade.SelectedValue = Converter.GetString(getDTO.EmpBaseGrade);
                    }

                    lblLastBaseGrade.Text = Converter.GetString(ddlNewBaseGrade.SelectedItem.Text);

                    lblLastGrade.Text = Converter.GetString(getDTO.EmpGrade);
                    lblLastGradeDesc.Text = Converter.GetString(getDTO.EmpGradeDesc);
                    txtNewGrade.Text = Converter.GetString(getDTO.EmpGrade);
                    lblNewGradeDesc.Text = Converter.GetString(getDTO.EmpGradeDesc);

                    lblBaseGrade.Text = lblLastBaseGradeCode.Text;
                    lblGrade.Text = lblLastGrade.Text;
                    PayScaleDesc();
                    lblLastPayScale.Text = lblPayScale.Text;

                    lblBaseGrade.Text = ddlNewBaseGrade.SelectedValue; ;
                    lblGrade.Text = txtNewGrade.Text;
                    PayScaleDesc();
                    txtNewPayScale.Text = lblPayScale.Text;


                    lblLastPayLabel.Text = Converter.GetString(getDTO.EmpPayLabel);
                    txtNewPayLabel.Text = Converter.GetString(getDTO.EmpPayLabel);

                    lblPayLabel.Text = lblLastPayLabel.Text;
                    PayLabelBasic();
                    lblLastBasic.Text = lblBasic.Text;

                    lblPayLabel.Text = txtNewPayLabel.Text;
                    PayLabelBasic();
                    txtNewBasic.Text = lblBasic.Text;


                    if (getDTO.EmpLastIncrementDate == DateTime.MinValue)
                    {
                        txtLastIncrementDate.Text = string.Empty;
                    }
                    else
                    {
                        DateTime dt = Converter.GetDateTime(getDTO.EmpLastIncrementDate);
                        string date = dt.ToString("dd/MM/yyyy");
                        txtLastIncrementDate.Text = date;
                    }
                }
                else
                {
                    txtEmpNo.Text = string.Empty;
                    txtEmpNo.Focus();
                    InvalidEmpCodeMSG();
                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.btnSumbit_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void clearinfo()
        {
            txtEmpNo.Text = string.Empty;
            lblName.Text = string.Empty;
            txtLastIncrementDate.Text = string.Empty;
            txtNewIncrementDate.Text = string.Empty;

            lblLastBaseGrade.Text = string.Empty;
            ddlNewBaseGrade.SelectedIndex = 0;

            lblLastGrade.Text = string.Empty;
            txtNewGrade.Text = string.Empty;

            lblLastPayScale.Text = string.Empty;
            txtNewPayScale.Text = string.Empty;

            lblLastPayLabel.Text = string.Empty;
            txtNewPayLabel.Text = string.Empty;

            lblLastBasic.Text = string.Empty;
            txtNewBasic.Text = string.Empty;
        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                A2ZINCREMENTDTO UpDTO = new A2ZINCREMENTDTO();

                UpDTO.EmpCode = Converter.GetInteger(txtEmpNo.Text);

                UpDTO.EmpOldBaseGrade = Converter.GetSmallInteger(lblLastBaseGrade.Text);
                UpDTO.EmpOldBaseGradeDesc = Converter.GetString(lblLastBaseGrade.Text);

                UpDTO.EmpOldGrade = Converter.GetInteger(lblLastGrade.Text);
                UpDTO.EmpOldGradeDesc = Converter.GetString(lblLastGradeDesc.Text);
                UpDTO.EmpOldPayLabel = Converter.GetSmallInteger(lblLastPayLabel.Text);
                UpDTO.EmpOldPayScaleDesc = Converter.GetString(lblLastPayScale.Text);
                UpDTO.EmpOldBasic = Converter.GetDecimal(lblLastBasic.Text);

                UpDTO.EmpNewBaseGrade = Converter.GetSmallInteger(ddlNewBaseGrade.SelectedValue);
                UpDTO.EmpNewBaseGradeDesc = Converter.GetString(ddlNewBaseGrade.SelectedItem);

                UpDTO.EmpNewGrade = Converter.GetInteger(txtNewGrade.Text);
                UpDTO.EmpNewGradeDesc = Converter.GetString(lblNewGradeDesc.Text);
                UpDTO.EmpNewPayLabel = Converter.GetSmallInteger(txtNewPayLabel.Text);
                UpDTO.EmpNewPayScaleDesc = Converter.GetString(txtNewPayScale.Text);
                UpDTO.EmpNewBasic = Converter.GetDecimal(txtNewBasic.Text);


                if (txtNewIncrementDate.Text != string.Empty)
                {
                    DateTime PDate = DateTime.ParseExact(txtNewIncrementDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.EmpIncrementDate = PDate;

                }
                else
                {
                    string CheckIncrementDtNull = "";
                    UpDTO.IncrementNullDate = CheckIncrementDtNull;

                }

                if (txtLastIncrementDate.Text != string.Empty)
                {
                    DateTime LPDate = DateTime.ParseExact(txtLastIncrementDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    UpDTO.EmpLastIncrementDate = LPDate;

                }
                else
                {
                    string CheckLIncrementDtNull = "";
                    UpDTO.LIncrementNullDate = CheckLIncrementDtNull;

                }
                int roweffect = A2ZINCREMENTDTO.InsertInformation(UpDTO);
                if (roweffect > 0)
                {
                    txtEmpNo.Focus();
                    clearinfo();
                    gvDetailInfo.Visible = false;
                    txtEmpNo.ReadOnly = false;

                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnUpdate_Click Problem');</script>");
                //throw ex;
            }
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

        protected void BtnView_Click(object sender, EventArgs e)
        {
            if (txtEmpNo.Text != string.Empty)
            {

                gvDetail();
                gvDetailInfo.Visible = true;

            }
            else
            {
                txtEmpNo.Focus();
            }
        }

        protected void txtNewGrade_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int grade = Converter.GetSmallInteger(txtNewGrade.Text);
                A2ZGRADEDTO get2DTO = (A2ZGRADEDTO.GetGradeInformation(grade));
                if (get2DTO.ID > 0)
                {
                    lblNewGradeDesc.Text = Converter.GetString(get2DTO.GradeDesc);
                    lblBaseGrade.Text = ddlNewBaseGrade.SelectedValue; ;
                    lblGrade.Text = txtNewGrade.Text;
                    PayScaleDesc();
                    txtNewPayScale.Text = lblPayScale.Text;

                    if (txtNewPayLabel.Text != string.Empty)
                    {
                        lblPayLabel.Text = txtNewPayLabel.Text;
                        PayLabelBasic();
                        txtNewBasic.Text = lblBasic.Text;
                    }

                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtNewGrade_TextChanged Problem');</script>");
                //throw ex;
            }
        }


        protected void PayScaleDesc()
        {

            string qry1 = "Select Base,Payscale,StartBasic,Bar1,label1,End1Basic,Bar2,label2,End2Basic,Consolidated FROM A2ZPAYSCALE Where Base='" + lblBaseGrade.Text + "' And PayScale='" + lblGrade.Text + "'";
            DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
            if (dt1.Rows.Count > 0)
            {
                lblstbasic.Text = Converter.GetString(string.Format("{0:0,0.00}", dt1.Rows[0]["StartBasic"]));
                string stbasic = Converter.GetString(string.Format("{0:0}", dt1.Rows[0]["StartBasic"]));

                lblbar1.Text = Converter.GetString(string.Format("{0:0,0.00}", dt1.Rows[0]["Bar1"]));
                string bar1 = Converter.GetString(string.Format("{0:0}", dt1.Rows[0]["Bar1"]));

                lbllabel1.Text = Converter.GetString(dt1.Rows[0]["label1"]);
                string label1 = Converter.GetString(dt1.Rows[0]["label1"]);

                lblEndbasic1.Text = Converter.GetString(string.Format("{0:0,0.00}", dt1.Rows[0]["End1Basic"]));
                string Endbasic1 = Converter.GetString(string.Format("{0:0}", dt1.Rows[0]["End1Basic"]));

                lblbar2.Text = Converter.GetString(string.Format("{0:0,0.00}", dt1.Rows[0]["Bar2"]));
                string bar2 = Converter.GetString(string.Format("{0:0}", dt1.Rows[0]["Bar2"]));

                lbllabel2.Text = Converter.GetString(dt1.Rows[0]["label2"]);
                string label2 = Converter.GetString(dt1.Rows[0]["label2"]);

                lblEndbasic2.Text = Converter.GetString(string.Format("{0:0,0.00}", dt1.Rows[0]["End2Basic"]));
                string Endbasic2 = Converter.GetString(string.Format("{0:0}", dt1.Rows[0]["End2Basic"]));

                lblconsulted.Text = Converter.GetString(string.Format("{0:0}", dt1.Rows[0]["Consolidated"]));
                string consulted = Converter.GetString(string.Format("{0:0}", dt1.Rows[0]["Consolidated"]));

                lblPayScale.Text = stbasic + "-" + bar1 + "*" + label1 + "-" + Endbasic1 + "-" + bar2 + "*" + label2 + "-" + Endbasic2;
            }
        }
        protected void txtNewPayLabel_TextChanged(object sender, EventArgs e)
        {
            lblPayLabel.Text = txtNewPayLabel.Text;
            PayLabelBasic();
            txtNewBasic.Text = lblBasic.Text;
        }

        protected void PayLabelBasic()
        {

            double Basic = 0;
            int newlabel = Converter.GetInteger(lblPayLabel.Text);
            int label1 = Converter.GetInteger(lbllabel1.Text);
            double bar1 = Converter.GetDouble(lblbar1.Text);
            double bar2 = Converter.GetDouble(lblbar2.Text);
            double startbasic = Converter.GetDouble(lblstbasic.Text);
            double end1basic = Converter.GetDouble(lblEndbasic1.Text);

            newlabel = newlabel - 1;
            label1 = label1 - 1;


            if (newlabel == 99)
            {
                Basic = Converter.GetDouble(lblconsulted.Text);
            }
            else
                if (newlabel > label1)
                {
                    int restlabel = (newlabel - label1);
                    Basic = (end1basic + (bar2 * restlabel));
                }
                else
                {
                    Basic = (startbasic + (bar1 * newlabel));
                }

            lblBasic.Text = Converter.GetString(String.Format("{0:0,0.00}", Basic));

        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            txtEmpNo.Focus();
            clearinfo();
            gvDetailInfo.Visible = false;
            txtEmpNo.ReadOnly = false;
        }

        protected void ddlNewBaseGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblBaseGrade.Text = ddlNewBaseGrade.SelectedValue;
            lblGrade.Text = txtNewGrade.Text;
            PayScaleDesc();
            txtNewPayScale.Text = lblPayScale.Text;

            lblPayLabel.Text = txtNewPayLabel.Text;
            PayLabelBasic();
            txtNewBasic.Text = lblBasic.Text;
        }


    }
}