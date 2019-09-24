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
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
namespace ATOZWEBMCUS.Pages
{
    public partial class CSParameterOpeningMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                AccountTypeDropdown();
                txtAccType.Focus();
                Visible();
                string sqlquery = "SELECT ProductCode, RecordCode,RecordFlag from A2ZACCCTRL";
                gvHidden = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvHidden, "A2ZCSMCUS");
                gvHidden.Visible = false;
                BtnUpdate.Visible = false;


            }
        }

        private void Visible()
        {
            lblCalPeriod.Visible = false;
            ddlCalPeriod.Visible = false;
            lblCalMethod.Visible = false;
            ddlCalMethod.Visible = false;
            lblLoanCalMethod.Visible = false;
            ddlLoanCalMethod.Visible = false;
            lblInterestRate.Visible = false;
            txtInterestRate.Visible = false;
            lblFundRate.Visible = false;
            txtFundRate.Visible = false;
            lblProdCondition.Visible = false;
            ddlProdCondition.Visible = false;
            lblProdIntType.Visible = false;
            ddlProdIntType.Visible = false;
            lblMinDepositAmt.Visible = false;
            txtMinDepositAmt.Visible = false;
            lblIntWithdrDays.Visible = false;
            txtIntWithdrDays.Visible = false;
            lblAccProcFees.Visible = false;
            txtAccProcFees.Visible = false;
            lblAccClosingFees.Visible = false;
            txtAccClosingFees.Visible = false;
            lblRoundFlag.Visible = false;
            ddlRoundFlag.Visible = false;

            lblPeriod.Visible = false;
            txtPeriod.Visible = false;

            lblSlab.Visible = false;
            BtnSlab.Visible = false;
        }

        protected void AccountTypeDropdown()
        {
            string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE";
            ddlAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccType, "A2ZCSMCUS");

        }


        protected void GetInfo()
        {
            Int16 Acctype = Converter.GetSmallInteger(txtAccType.Text);

            A2ZCSPARAMDTO getDTO = (A2ZCSPARAMDTO.GetInformation(Acctype));

            if (getDTO.AccType > 0)
            {

                ddlCalPeriod.SelectedValue = Converter.GetString(getDTO.CalculationPeriod);
                ddlCalMethod.SelectedValue = Converter.GetString(getDTO.CalculationMethod);
                ddlLoanCalMethod.SelectedValue = Converter.GetString(getDTO.LoanCalculationMethod);
                txtInterestRate.Text = Converter.GetString(String.Format("{0:0,0.00}",getDTO.InterestRate));
                txtFundRate.Text = Converter.GetString(String.Format("{0:0,0.00}",getDTO.FundRate));
                ddlProdCondition.SelectedValue = Converter.GetString(getDTO.ProductCondition);
                ddlProdIntType.SelectedValue = Converter.GetString(getDTO.ProductInterestType);
                txtMinDepositAmt.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.MinDepositAmt));
                txtIntWithdrDays.Text = Converter.GetString(getDTO.IntWithdrDays);
                txtAccProcFees.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccProcFees));
                txtAccClosingFees.Text = Converter.GetString(String.Format("{0:0,0.00}", getDTO.AccClosingFees));
                txtPeriod.Text = Converter.GetString(getDTO.Period);

                ddlRoundFlag.SelectedValue = Converter.GetString(getDTO.RoundFlag);
                BtnSubmit.Visible = false;
                BtnUpdate.Visible = true;


            }
            else
            {
                clearInfo();
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;



            }
        }

        protected void ddlAccType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAccType.SelectedValue != "-Select-")
            {
                txtAccType.Text = ddlAccType.SelectedValue;
                txtAccType_TextChanged(this, EventArgs.Empty);
            }
        }

        protected void txtAccType_TextChanged(object sender, EventArgs e)
        {

            if (txtAccType.Text != string.Empty)
            {
                A2ZACCTYPEDTO objDTO = new A2ZACCTYPEDTO();
                Int16 typecls = Converter.GetSmallInteger(txtAccType.Text);
                objDTO = (A2ZACCTYPEDTO.GetInformation(typecls));
                if (objDTO.AccTypeCode > 0)
                {
                    Visible();
                    lbltypecls.Text = Converter.GetString(objDTO.AccTypeClass);
                    lblType.Text = Converter.GetString(objDTO.AccTypeCode);
                    ddlAccType.SelectedValue = Converter.GetString(objDTO.AccTypeCode);
                }

                //Response.Write("<script>alert('dsadasd');</script>");

            }

            GetInfo();
            try
            {
                Int16 code = Converter.GetSmallInteger(txtAccType.Text);
                //string sqlquery = "SELECT ProductCode, RecordCode,RecordFlag from A2ZACCCTRL where ProductCode='" + code + "'and ControlCode='2'";
                //gvHidden = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvHidden, "A2ZCSMCUS");
                A2ZACCCTRLDTO getDTO = new A2ZACCCTRLDTO();
                for (int i = 0; i < gvHidden.Rows.Count; i++)
                {

                    Int16 rcode = Converter.GetSmallInteger(gvHidden.Rows[i].Cells[1].Text);
                    Int16 ccode = 2;
                    getDTO = (A2ZACCCTRLDTO.GetInformation(code, ccode, rcode));
                    if (getDTO.ProductCode > 0 && getDTO.RecordCode > 0)
                    {
                        Label4.Text = Converter.GetString(getDTO.RecordFlag);

                    }

                    if (Label4.Text == "1")
                    {
                        switch (rcode)
                        {
                            case 1:
                                ddlCalPeriod.Visible = true;
                                lblCalPeriod.Visible = true;
                                break;
                            case 2:
                                ddlCalMethod.Visible = true;
                                lblCalMethod.Visible = true;
                                break;
                            case 3:
                                ddlLoanCalMethod.Visible = true;
                                lblLoanCalMethod.Visible = true;
                                break;
                            case 4:
                                txtInterestRate.Visible = true;
                                lblInterestRate.Visible = true;
                                break;
                            case 5:
                                txtFundRate.Visible = true;
                                lblFundRate.Visible = true;
                                break;
                            case 6:
                                ddlProdCondition.Visible = true;
                                lblProdCondition.Visible = true;
                                break;
                            case 7:
                                ddlProdIntType.Visible = true;
                                lblProdIntType.Visible = true;
                                break;
                            case 8:
                                txtMinDepositAmt.Visible = true;
                                lblMinDepositAmt.Visible = true;
                                break;
                            case 9:
                                ddlRoundFlag.Visible = true;
                                lblRoundFlag.Visible = true;
                                break;
                            case 10:
                                txtIntWithdrDays.Visible = true;
                                lblIntWithdrDays.Visible = true;
                                break;
                            case 11:
                                txtAccProcFees.Visible = true;
                                lblAccProcFees.Visible = true;
                                break;
                            case 12:
                                txtAccClosingFees.Visible = true;
                                lblAccClosingFees.Visible = true;
                                break;
                            case 13:
                                txtPeriod.Visible = true;
                                lblPeriod.Visible = true;
                                break;
                            case 99:
                                BtnSlab.Visible = true;
                                lblSlab.Visible = true;
                                break;
                        }
                    }


                }
                if (BtnUpdate.Visible == true)
                {
                    lblTest.Text = "1";
                    Session["Test"] = lblTest.Text;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            A2ZCSPARAMDTO objDTO = new A2ZCSPARAMDTO();
            objDTO.AccType = Converter.GetSmallInteger(txtAccType.Text);
            objDTO.CalculationPeriod = Converter.GetSmallInteger(ddlCalPeriod.SelectedValue);
            objDTO.CalculationMethod = Converter.GetSmallInteger(ddlCalMethod.SelectedValue);
            objDTO.LoanCalculationMethod = Converter.GetSmallInteger(ddlLoanCalMethod.SelectedValue);
            objDTO.InterestRate = Converter.GetDecimal(txtInterestRate.Text);
            objDTO.FundRate = Converter.GetSmallInteger(txtFundRate.Text);
            objDTO.ProductCondition = Converter.GetSmallInteger(ddlProdCondition.SelectedValue);
            objDTO.ProductInterestType = Converter.GetSmallInteger(ddlProdIntType.SelectedValue);
            objDTO.MinDepositAmt = Converter.GetDecimal(txtMinDepositAmt.Text);
            objDTO.IntWithdrDays = Converter.GetSmallInteger(txtIntWithdrDays.Text);
            objDTO.RoundFlag = Converter.GetSmallInteger(ddlRoundFlag.SelectedValue);
            objDTO.AccProcFees = Converter.GetDecimal(txtAccProcFees.Text);
            objDTO.AccClosingFees = Converter.GetDecimal(txtAccClosingFees.Text);
            objDTO.Period = Converter.GetInteger(txtPeriod.Text);
            objDTO.PeriodSlab = Converter.GetSmallInteger(99);

            int roweffect = A2ZCSPARAMDTO.InsertInformation(objDTO);
            if (roweffect > 0)
            {

                clearInfo();
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                txtAccType.Text = string.Empty;
                ddlAccType.SelectedValue = "-Select-";
                txtAccType.Focus();

            }

        }

        private void clearInfo()
        {
            ddlCalPeriod.SelectedValue = "0";
            ddlCalMethod.SelectedValue = "0";
            ddlLoanCalMethod.SelectedValue = "0";
            ddlProdCondition.SelectedValue = "0";
            ddlProdIntType.SelectedValue = "0";
            ddlRoundFlag.SelectedValue = "0";
            txtInterestRate.Text = string.Empty;
            txtFundRate.Text = string.Empty;
            txtIntWithdrDays.Text = string.Empty;
            txtMinDepositAmt.Text = string.Empty;
            txtAccProcFees.Text = string.Empty;
            txtAccClosingFees.Text = string.Empty;
            txtPeriod.Text = string.Empty;
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            A2ZCSPARAMDTO UpDTO = new A2ZCSPARAMDTO();

            UpDTO.AccType = Converter.GetSmallInteger(txtAccType.Text);
            UpDTO.CalculationPeriod = Converter.GetSmallInteger(ddlCalPeriod.SelectedValue);
            UpDTO.CalculationMethod = Converter.GetSmallInteger(ddlCalMethod.SelectedValue);
            UpDTO.LoanCalculationMethod = Converter.GetSmallInteger(ddlLoanCalMethod.SelectedValue);
            UpDTO.InterestRate = Converter.GetDecimal(txtInterestRate.Text);
            UpDTO.FundRate = Converter.GetDecimal(txtFundRate.Text);
            UpDTO.ProductCondition = Converter.GetSmallInteger(ddlProdCondition.SelectedValue);
            UpDTO.ProductInterestType = Converter.GetSmallInteger(ddlProdIntType.SelectedValue);
            UpDTO.RoundFlag = Converter.GetSmallInteger(ddlRoundFlag.SelectedValue);
            UpDTO.IntWithdrDays = Converter.GetSmallInteger(txtIntWithdrDays.Text);
            UpDTO.MinDepositAmt = Converter.GetDecimal(txtMinDepositAmt.Text);
            UpDTO.AccProcFees = Converter.GetDecimal(txtAccProcFees.Text);
            UpDTO.AccClosingFees = Converter.GetDecimal(txtAccClosingFees.Text);
            UpDTO.Period = Converter.GetInteger(txtPeriod.Text);
            //UpDTO.PeriodSlab = Converter.GetSmallInteger(99);

            int roweffect = A2ZCSPARAMDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {
                txtAccType.Text = string.Empty;
                ddlAccType.SelectedValue = "-Select-";
                
                clearInfo();
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                txtAccType.Focus();

            }

        }

        protected void BtnSlab_Click(object sender, EventArgs e)
        {

            Session["TypeClass"] = lbltypecls.Text;
            Session["TypeCode"] = lblType.Text;

            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
          "click", @"<script>window.open('CSParameterSlabMaintenance.aspx','_blank');</script>", false);



            ////ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'CSParameterSlabMaintenance.aspx', null, 'height=800,width=1200,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
            //Page.ClientScript.RegisterStartupScript(
            //  this.GetType(), "OpenWindow", "window.open('CSParameterSlabMaintenance.aspx','_newtab');", true);
        }


        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }


    }
}
