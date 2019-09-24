using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.DTO;

namespace ATOZWEBMCUS.Pages
{
    public partial class CSAutoInterest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                if (!IsPostBack)
                {
                    

                    BtnCalculate.Visible = false;
                    BtnPrint.Visible = false;
                    lblVchNo.Visible = false;
                    txtVchNo.Visible = false;
                    BtnPost.Visible = false;
                    BtnReverse.Visible = false;
                    BtnExit.Visible = true;
                    lblTotalProd.Visible = false;
                    lblTotalInt.Visible = false;


                    


                    hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    lblIDName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));


                   
                    hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));
                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt.ToString("dd/MM/yyyy");
                    lblPdate.Text = date;
                    txtToDaysDate.Text = Converter.GetString(String.Format("{0:Y}", dto.ProcessDate));

                    lblBegYear.Text = Converter.GetString(dto.FinancialBegYear);
                    lblEndYear.Text = Converter.GetString(dto.FinancialEndYear);

                    lblCurrMth.Text = Converter.GetString(dto.CurrentMonth);

                    if (lblCurrMth.Text == "12")
                    {
                        lblMonth1.Text = "July" + "'" + lblBegYear.Text;
                        lblMonth2.Text = "August" + "'" + lblBegYear.Text;
                        lblMonth3.Text = "September" + "'" + lblBegYear.Text;
                        lblMonth4.Text = "October" + "'" + lblBegYear.Text;
                        lblMonth5.Text = "November" + "'" + lblBegYear.Text;
                        lblMonth6.Text = "December" + "'" + lblBegYear.Text;
                    }
                    else if (lblCurrMth.Text == "6")
                    {
                        lblMonth1.Text = "January" + "'" + lblEndYear.Text;
                        lblMonth2.Text = "February" + "'" + lblEndYear.Text;
                        lblMonth3.Text = "March" + "'" + lblEndYear.Text;
                        lblMonth4.Text = "April" + "'" + lblEndYear.Text;
                        lblMonth5.Text = "May" + "'" + lblEndYear.Text;
                        lblMonth6.Text = "June" + "'" + lblEndYear.Text;
                    }


                    ddlAccountType();

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }

        protected void ddlAccountType()
        {
            if (lblCurrMth.Text == "12")
            {
                string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE,A2ZCSPARAM Where A2ZACCTYPE.AccTypeClass = 1 and A2ZACCTYPE.AccTypeMode = 1 AND (A2ZACCTYPE.AccTypeCode =12 OR A2ZACCTYPE.AccTypeCode =13 OR A2ZACCTYPE.AccTypeCode =18 OR A2ZACCTYPE.AccTypeCode =21 OR A2ZACCTYPE.AccTypeCode =24) AND A2ZCSPARAM.AccType = A2ZACCTYPE.AccTypeCode AND A2ZCSPARAM.PrmCalPeriod = 5";
                ddlAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccType, "A2ZCSMCUS");
            }
            else if (lblCurrMth.Text == "6")
            {
                string sqlquery = "SELECT AccTypeCode,AccTypeDescription from A2ZACCTYPE,A2ZCSPARAM Where A2ZACCTYPE.AccTypeClass = 1 and A2ZACCTYPE.AccTypeMode = 1 AND (A2ZACCTYPE.AccTypeCode =12 OR A2ZACCTYPE.AccTypeCode =13 OR A2ZACCTYPE.AccTypeCode =18 OR A2ZACCTYPE.AccTypeCode =21 OR A2ZACCTYPE.AccTypeCode =24) AND A2ZCSPARAM.AccType = A2ZACCTYPE.AccTypeCode AND (A2ZCSPARAM.PrmCalPeriod = 5 OR A2ZCSPARAM.PrmCalPeriod = 6)";
                ddlAccType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlAccType, "A2ZCSMCUS");

            }
        }
        protected void ValidationProcess1()
        {
            try
            {
                //gvDetailInfo1.Visible = false;

                lblTotalProd.Visible = false;
                lblTotalInt.Visible = false;

                txtTotalProd.Visible = false;
                txtTotalInt.Visible = false;

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime ProcDt = Converter.GetDateTime(dto.ProcessDate);


                DataTable dt1 = new DataTable();

                 
                string qry1 = "SELECT Id,TrnDate,IntRateJul,IntRateAug,IntRateSep,IntRateOct,IntRateNov,IntRateDec,ProcStat FROM WFCSINTEREST where AccType ='" + ddlAccType.SelectedValue + "' AND CalPeriod = 1";
                dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");

                if (dt1.Rows.Count > 0)
                {
                    DateTime TrnDate = Converter.GetDateTime(dt1.Rows[0]["TrnDate"]);
                    Decimal IRateJul = Converter.GetDecimal(dt1.Rows[0]["IntRateJul"]);
                    Decimal IRateAug = Converter.GetDecimal(dt1.Rows[0]["IntRateAug"]);
                    Decimal IRateSep = Converter.GetDecimal(dt1.Rows[0]["IntRateSep"]);
                    Decimal IRateOct = Converter.GetDecimal(dt1.Rows[0]["IntRateOct"]);
                    Decimal IRateNov = Converter.GetDecimal(dt1.Rows[0]["IntRateNov"]);
                    Decimal IRateDec = Converter.GetDecimal(dt1.Rows[0]["IntRateDec"]);
                    Int16 ProcStat = Converter.GetSmallInteger(dt1.Rows[0]["ProcStat"]);

                    txtMonth1.Text = Converter.GetString(String.Format("{0:0,0.00}", IRateJul));
                    txtMonth2.Text = Converter.GetString(String.Format("{0:0,0.00}", IRateAug));
                    txtMonth3.Text = Converter.GetString(String.Format("{0:0,0.00}", IRateSep));
                    txtMonth4.Text = Converter.GetString(String.Format("{0:0,0.00}", IRateOct));
                    txtMonth5.Text = Converter.GetString(String.Format("{0:0,0.00}", IRateNov));
                    txtMonth6.Text = Converter.GetString(String.Format("{0:0,0.00}", IRateDec));
               
                    if (TrnDate == ProcDt && ProcStat == 3)
                    {
                        BtnCalculate.Visible = false;
                        BtnPrint.Visible = true;
                        lblVchNo.Visible = false;
                        txtVchNo.Visible = false;
                        BtnPost.Visible = false;
                        BtnReverse.Visible = true;
                        BtnExit.Visible = true;

                        txtMonth1.ReadOnly = true;
                        txtMonth2.ReadOnly = true;
                        txtMonth3.ReadOnly = true;
                        txtMonth4.ReadOnly = true;
                        txtMonth5.ReadOnly = true;
                        txtMonth6.ReadOnly = true;
                    }
                    else
                        if (TrnDate == ProcDt && ProcStat == 2)
                        {
                            BtnCalculate.Visible = true;
                            BtnPrint.Visible = true;
                            lblVchNo.Visible = true;
                            txtVchNo.Visible = true;
                            BtnPost.Visible = true;
                            BtnReverse.Visible = false;
                            BtnExit.Visible = true;

                        }
                        else
                            if (TrnDate == ProcDt && ProcStat == 1)
                            {
                                BtnCalculate.Visible = true;
                                BtnPrint.Visible = true;
                                lblVchNo.Visible = false;
                                txtVchNo.Visible = false;
                                BtnPost.Visible = false;
                                BtnReverse.Visible = false;
                                BtnExit.Visible = true;

                            }
                }
                else
                {
                    BtnCalculate.Visible = true;
                    BtnPrint.Visible = true;
                    lblVchNo.Visible = false;
                    txtVchNo.Visible = false;
                    BtnPost.Visible = false;
                    BtnReverse.Visible = false;
                    BtnExit.Visible = true;

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ValidationProcess Problem');</script>");
                //throw ex;
            }
        }

        protected void ValidationProcess2()
        {
            try
            {
                //gvDetailInfo2.Visible = false;

                lblTotalProd.Visible = false;
                lblTotalInt.Visible = false;

                txtTotalProd.Visible = false;
                txtTotalInt.Visible = false;

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime ProcDt = Converter.GetDateTime(dto.ProcessDate);


                DataTable dt1 = new DataTable();


                string qry1 = "SELECT Id,TrnDate,IntRateJan,IntRateFeb,IntRateMar,IntRateApr,IntRateMay,IntRateJun,ProcStat FROM WFCSINTEREST where AccType ='" + ddlAccType.SelectedValue + "' AND CalPeriod = 2";
                dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");

                if (dt1.Rows.Count > 0)
                {
                    DateTime TrnDate = Converter.GetDateTime(dt1.Rows[0]["TrnDate"]);
                    Decimal IRateJan = Converter.GetDecimal(dt1.Rows[0]["IntRateJan"]);
                    Decimal IRateFeb = Converter.GetDecimal(dt1.Rows[0]["IntRateFeb"]);
                    Decimal IRateMar = Converter.GetDecimal(dt1.Rows[0]["IntRateMar"]);
                    Decimal IRateApr = Converter.GetDecimal(dt1.Rows[0]["IntRateApr"]);
                    Decimal IRateMay = Converter.GetDecimal(dt1.Rows[0]["IntRateMay"]);
                    Decimal IRateJun = Converter.GetDecimal(dt1.Rows[0]["IntRateJun"]);
                    Int16 ProcStat = Converter.GetSmallInteger(dt1.Rows[0]["ProcStat"]);

                    txtMonth1.Text = Converter.GetString(String.Format("{0:0,0.00}", IRateJan));
                    txtMonth2.Text = Converter.GetString(String.Format("{0:0,0.00}", IRateFeb));
                    txtMonth3.Text = Converter.GetString(String.Format("{0:0,0.00}", IRateMar));
                    txtMonth4.Text = Converter.GetString(String.Format("{0:0,0.00}", IRateApr));
                    txtMonth5.Text = Converter.GetString(String.Format("{0:0,0.00}", IRateMay));
                    txtMonth6.Text = Converter.GetString(String.Format("{0:0,0.00}", IRateJun));

                    if (TrnDate == ProcDt && ProcStat == 3)
                    {
                        BtnCalculate.Visible = false;
                        BtnPrint.Visible = true;
                        lblVchNo.Visible = false;
                        txtVchNo.Visible = false;
                        BtnPost.Visible = false;
                        BtnReverse.Visible = true;
                        BtnExit.Visible = true;

                        txtMonth1.ReadOnly = true;
                        txtMonth2.ReadOnly = true;
                        txtMonth3.ReadOnly = true;
                        txtMonth4.ReadOnly = true;
                        txtMonth5.ReadOnly = true;
                        txtMonth6.ReadOnly = true;
                    }
                    else
                        if (TrnDate == ProcDt && ProcStat == 2)
                        {
                            BtnCalculate.Visible = true;
                            BtnPrint.Visible = true;
                            lblVchNo.Visible = true;
                            txtVchNo.Visible = true;
                            BtnPost.Visible = true;
                            BtnReverse.Visible = false;
                            BtnExit.Visible = true;

                        }
                        else
                            if (TrnDate == ProcDt && ProcStat == 1)
                            {
                                BtnCalculate.Visible = true;
                                BtnPrint.Visible = true;
                                lblVchNo.Visible = false;
                                txtVchNo.Visible = false;
                                BtnPost.Visible = false;
                                BtnReverse.Visible = false;
                                BtnExit.Visible = true;

                            }
                }
                else
                {
                    BtnCalculate.Visible = true;
                    BtnPrint.Visible = true;
                    lblVchNo.Visible = false;
                    txtVchNo.Visible = false;
                    BtnPost.Visible = false;
                    BtnReverse.Visible = false;
                    BtnExit.Visible = true;

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ValidationProcess Problem');</script>");
                //throw ex;
            }
        }

        protected void ValidationProcess3()
        {
            try
            {
                //gvDetailInfo3.Visible = false;

                lblTotalProd.Visible = false;
                lblTotalInt.Visible = false;

                txtTotalProd.Visible = false;
                txtTotalInt.Visible = false;

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime ProcDt = Converter.GetDateTime(dto.ProcessDate);


                DataTable dt1 = new DataTable();


                string qry1 = "SELECT Id,TrnDate,IntRateJul,ProcStat FROM WFCSINTEREST where AccType ='" + ddlAccType.SelectedValue + "'";
                dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZCSMCUS");

                if (dt1.Rows.Count > 0)
                {
                    DateTime TrnDate = Converter.GetDateTime(dt1.Rows[0]["TrnDate"]);
                    Decimal IRateJul = Converter.GetDecimal(dt1.Rows[0]["IntRateJul"]);
                   
                    Int16 ProcStat = Converter.GetSmallInteger(dt1.Rows[0]["ProcStat"]);

                    txtMonth1.Text = Converter.GetString(String.Format("{0:0,0.00}", IRateJul));
                    

                    if (ProcStat == 3)
                    {
                        BtnCalculate.Visible = false;
                        BtnPrint.Visible = true;
                        lblVchNo.Visible = false;
                        txtVchNo.Visible = false;
                        BtnPost.Visible = false;
                        BtnReverse.Visible = true;
                        BtnExit.Visible = true;

                        txtMonth1.ReadOnly = true;
                        txtMonth2.ReadOnly = true;
                        txtMonth3.ReadOnly = true;
                        txtMonth4.ReadOnly = true;
                        txtMonth5.ReadOnly = true;
                        txtMonth6.ReadOnly = true;
                    }
                    else
                        if (ProcStat == 2)
                        {
                            BtnCalculate.Visible = true;
                            BtnPrint.Visible = true;
                            lblVchNo.Visible = true;
                            txtVchNo.Visible = true;
                            BtnPost.Visible = true;
                            BtnReverse.Visible = false;
                            BtnExit.Visible = true;

                        }
                        else
                            if (ProcStat == 1)
                            {
                                BtnCalculate.Visible = true;
                                BtnPrint.Visible = true;
                                lblVchNo.Visible = false;
                                txtVchNo.Visible = false;
                                BtnPost.Visible = false;
                                BtnReverse.Visible = false;
                                BtnExit.Visible = true;

                            }
                }
                else
                {
                    BtnCalculate.Visible = true;
                    BtnPrint.Visible = true;
                    lblVchNo.Visible = false;
                    txtVchNo.Visible = false;
                    BtnPost.Visible = false;
                    BtnReverse.Visible = false;
                    BtnExit.Visible = true;

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ValidationProcess Problem');</script>");
                //throw ex;
            }
        }

        protected void DisableRecords()
        {
            Label1.Visible = false;
            lblMonth1.Visible = true;
            lblMonth1.Text = "Interest Rate   :";
            txtMonth1.Visible = true;

            lblMonth4.Visible = false;
            txtMonth4.Visible = false;
            lblMonth2.Visible = false;
            txtMonth2.Visible = false;
            lblMonth5.Visible = false;
            txtMonth5.Visible = false;
            lblMonth3.Visible = false;
            txtMonth3.Visible = false;
            lblMonth6.Visible = false;
            txtMonth6.Visible = false;
        }

        protected void EnableRecords()
        {
            Label1.Visible = true;
            lblMonth1.Visible = true;
            txtMonth1.Visible = true;

            lblMonth4.Visible = true;
            txtMonth4.Visible = true;
            lblMonth2.Visible = true;
            txtMonth2.Visible = true;
            lblMonth5.Visible = true;
            txtMonth5.Visible = true;
            lblMonth3.Visible = true;
            txtMonth3.Visible = true;
            lblMonth6.Visible = true;
            txtMonth6.Visible = true;


            if (lblCurrMth.Text == "12")
            {
                lblMonth1.Text = "July" + "'" + lblBegYear.Text;
                lblMonth2.Text = "August" + "'" + lblBegYear.Text;
                lblMonth3.Text = "September" + "'" + lblBegYear.Text;
                lblMonth4.Text = "October" + "'" + lblBegYear.Text;
                lblMonth5.Text = "November" + "'" + lblBegYear.Text;
                lblMonth6.Text = "December" + "'" + lblBegYear.Text;
            }
            else if (lblCurrMth.Text == "6")
            {
                lblMonth1.Text = "January" + "'" + lblEndYear.Text;
                lblMonth2.Text = "February" + "'" + lblEndYear.Text;
                lblMonth3.Text = "March" + "'" + lblEndYear.Text;
                lblMonth4.Text = "April" + "'" + lblEndYear.Text;
                lblMonth5.Text = "May" + "'" + lblEndYear.Text;
                lblMonth6.Text = "June" + "'" + lblEndYear.Text;
            }




        }
        

        protected void ddlAccType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int16 AccType = Converter.GetSmallInteger(ddlAccType.SelectedValue);
            A2ZACCTYPEDTO get1DTO = (A2ZACCTYPEDTO.GetInformation(AccType));
            if (get1DTO.AccTypeCode > 0)
            {
                hdnAccTypeClass.Text = Converter.GetString(get1DTO.AccTypeClass);

                Int16 AType = Converter.GetSmallInteger(ddlAccType.SelectedValue);
                A2ZCSPARAMDTO get2DTO = (A2ZCSPARAMDTO.GetInformation(AType));
                if (get2DTO.AccType > 0)
                {
                    if (ddlAccType.SelectedValue == "12")
                    {
                        EnableRecords();

                        txtMonth1.Text = Converter.GetString(String.Format("{0:0,0.00}", get2DTO.InterestRate));
                        txtMonth2.Text = Converter.GetString(String.Format("{0:0,0.00}", get2DTO.InterestRate));
                        txtMonth3.Text = Converter.GetString(String.Format("{0:0,0.00}", get2DTO.InterestRate));
                        txtMonth4.Text = Converter.GetString(String.Format("{0:0,0.00}", get2DTO.InterestRate));
                        txtMonth5.Text = Converter.GetString(String.Format("{0:0,0.00}", get2DTO.InterestRate));
                        txtMonth6.Text = Converter.GetString(String.Format("{0:0,0.00}", get2DTO.InterestRate));
                    }
                    else 
                    {
                        DisableRecords();
                        txtMonth1.Text = Converter.GetString(String.Format("{0:0,0.00}", get2DTO.InterestRate));
                    }
                }


            }


            if (ddlAccType.SelectedValue != "12")
            {
                ValidationProcess3();
            }
            else if (lblCurrMth.Text == "12")
            {
                ValidationProcess1();
            }
            else if (lblCurrMth.Text == "6")
            {
                ValidationProcess2();
            }

        }
        //protected void gvPreview1()
        //{
        //    try
        //    {
        //        string sqlquery3 = "SELECT CuNumber,MemNo,AccType,AccNo,MemName,AccOpenDate,AmtJul,AmtAug,AmtSep,AmtOct,AmtNov,AmtDec,AmtProduct,AmtInterest FROM WFCSINTEREST Where AccType='" + ddlAccType.SelectedValue + "' ORDER BY CuType,CuNo";
        //        gvDetailInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo1, "A2ZCSMCUS");
        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvPreview Problem');</script>");
        //        //throw ex;
        //    }
        //}    
        //protected void gvPreview2()
        //{
        //    try
        //    {
        //        string sqlquery4 = "SELECT CuNumber,MemNo,AccType,AccNo,MemName,AccOpenDate,AmtJan,AmtFeb,AmtMar,AmtApr,AmtMay,AmtJun,AmtProduct,AmtInterest FROM WFCSINTEREST Where AccType='" + ddlAccType.SelectedValue + "' ORDER BY CuType,CuNo";
        //        gvDetailInfo3 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery4, gvDetailInfo3, "A2ZCSMCUS");
        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvPreview Problem');</script>");
        //        //throw ex;
        //    }
        //}


        //protected void gvPreview3()
        //{
        //    try
        //    {
        //        string sqlquery3 = "SELECT CuNumber,MemNo,AccType,AccNo,MemName,AccOpenDate,AmtJul,AmtAug,AmtSep,AmtOct,AmtNov,AmtDec,AmtJan,AmtFeb,AmtMar,AmtApr,AmtMay,AmtJun,AmtProduct,AmtInterest FROM WFCSINTEREST Where AccType='" + ddlAccType.SelectedValue + "' ORDER BY CuType,CuNo";
        //        gvDetailInfo3 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo3, "A2ZCSMCUS");
        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvPreview Problem');</script>");
        //        //throw ex;
        //    }
        //}

        //protected void gvSumValue1()
        //{
        //    try
        //    {

        //        Decimal sumProdAmt = 0;
        //        Decimal sumIntAmt = 0;


        //        lblTotalProd.Visible = true;
        //        txtTotalProd.Visible = true;

        //        lblTotalInt.Visible = true;
        //        txtTotalInt.Visible = true;


        //        for (int i = 0; i < gvDetailInfo1.Rows.Count; ++i)
        //        {

        //            sumProdAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo1.Rows[i].Cells[12].Text));
        //            sumIntAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo1.Rows[i].Cells[13].Text));
        //        }


        //        txtTotalProd.Text = Convert.ToString(String.Format("{0:0,0.00}", sumProdAmt));
        //        txtTotalInt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumIntAmt));


        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvSumValue Problem');</script>");
        //        //throw ex;
        //    }
        //}

        //protected void gvSumValue2()
        //{
        //    try
        //    {

        //        Decimal sumProdAmt = 0;
        //        Decimal sumIntAmt = 0;


        //        lblTotalProd.Visible = true;
        //        txtTotalProd.Visible = true;

        //        lblTotalInt.Visible = true;
        //        txtTotalInt.Visible = true;


        //        for (int i = 0; i < gvDetailInfo2.Rows.Count; ++i)
        //        {

        //            sumProdAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo2.Rows[i].Cells[12].Text));
        //            sumIntAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo2.Rows[i].Cells[13].Text));
        //        }


        //        txtTotalProd.Text = Convert.ToString(String.Format("{0:0,0.00}", sumProdAmt));
        //        txtTotalInt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumIntAmt));


        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvSumValue Problem');</script>");
        //        //throw ex;
        //    }
        //}

        //protected void gvSumValue3()
        //{
        //    try
        //    {

        //        Decimal sumProdAmt = 0;
        //        Decimal sumIntAmt = 0;


        //        lblTotalProd.Visible = true;
        //        txtTotalProd.Visible = true;

        //        lblTotalInt.Visible = true;
        //        txtTotalInt.Visible = true;


        //        for (int i = 0; i < gvDetailInfo3.Rows.Count; ++i)
        //        {

        //            sumProdAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo3.Rows[i].Cells[16].Text));
        //            sumIntAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfo3.Rows[i].Cells[17].Text));
        //        }


        //        txtTotalProd.Text = Convert.ToString(String.Format("{0:0,0.00}", sumProdAmt));
        //        txtTotalInt.Text = Convert.ToString(String.Format("{0:0,0.00}", sumIntAmt));


        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.gvSumValue Problem');</script>");
        //        //throw ex;
        //    }
        //}
        protected void gvDetailInfo1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void gvDetailInfo2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void gvDetailInfo3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void UpdateMSG()
        {
            string Msg = "";

            string a = "";
            string b = "";
            string c = "";

            a = "Interest Posting Sucessfully Done";


            b = "Generated New Voucher No.";
            c = string.Format(CtrlVchNo.Text);

            Msg += a;
            Msg += "\\n";
            Msg += "\\n";
            Msg += b + c;

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), Guid.NewGuid().ToString(), "window.alert('" + Msg + "')", true);
            return;
            //----------------------------
            //string a = "";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //a = "Provision Posting Sucessfully Done";


            //string b = "Generated New Voucher No.";
            //string c = string.Format(CtrlVchNo.Text);

            //sb.Append("<script type = 'text/javascript'>");
            //sb.Append("window.onload=function(){");
            //sb.Append("alert('");
            //sb.Append(a);
            //sb.Append("\\n");
            //sb.Append("\\n");
            //sb.Append(b);
            //sb.Append(c);
            //sb.Append("')};");
            //sb.Append("</script>");
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        }

        protected void DeleteMSG()
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Interest Posting Deleted');", true);
            return;
        }


        protected void InvalidVchMSG()
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher No. Not Exist');", true);
            return;

        }

        protected void EmptyVchMSG()
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Voucher No.');", true);
            return;

        }


        protected void BtnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlAccType.SelectedValue == "-Select-")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Account Type');", true);
                    return;
                   
                }

                //if (lblCurrMth.Text != "6" && lblCurrMth.Text != "12")
                // {
                //     ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Month for the Interest Process');", true);
                //     return;
                // }


                if (ddlAccType.SelectedValue != "12")
                {
                    var prm = new object[4];

                    prm[0] = ddlAccType.SelectedValue;
                    prm[1] = 0;
                    prm[2] = txtMonth1.Text;
                    prm[3] = 2017;
                    

                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateInterestAmountYearly", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Calculation Done');", true);
                        return;


                        //gvDetailInfo3.Visible = true;
                        //gvDetailInfo1.Visible = false;
                        //gvDetailInfo2.Visible = false;
                        //gvPreview3();
                        //gvSumValue3();
                    }

                }
                else
                {

                    var prm = new object[8];

                    prm[0] = ddlAccType.SelectedValue;
                    prm[1] = 0;
                    prm[2] = txtMonth1.Text;
                    prm[3] = txtMonth2.Text;
                    prm[4] = txtMonth3.Text;
                    prm[5] = txtMonth4.Text;
                    prm[6] = txtMonth5.Text;
                    prm[7] = txtMonth6.Text;


                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateInterestAmount", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {

                        if (lblCurrMth.Text == "12")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Calculation Done');", true);
                            return;

                            //gvDetailInfo1.Visible = true;
                            //gvDetailInfo2.Visible = false;
                            //gvPreview1();
                            //gvSumValue1();
                        }
                        else if (lblCurrMth.Text == "6")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Calculation Done');", true);
                            return;

                            //gvDetailInfo1.Visible = false;
                            //gvDetailInfo2.Visible = true;
                            //gvPreview2();
                            //gvSumValue2();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnCalculate_Click Problem');</script>");
                //throw ex;
            }

        }

        protected void DuplicateVchMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Voucher Already Exist');", true);
            return;

        }

        protected void TrnVchDeplicate()
        {
            try
            {

                hdnMsgFlag.Text = "0";

                DateTime opdate = DateTime.ParseExact(lblPdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                string qry = "SELECT VchNo,TrnDate FROM A2ZTRANSACTION where VchNo ='" + txtVchNo.Text.Trim() + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZCSMCUS");
                if (dt.Rows.Count > 0)
                {
                    hdnMsgFlag.Text = "1";

                    txtVchNo.Text = string.Empty;
                    txtVchNo.Focus();
                    DuplicateVchMSG();
                    return;
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.TrnVchDeplicate Problem');</script>");
                //throw ex;
            }
        }
        protected void BtnPost_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtVchNo.Text == string.Empty)
                {
                    txtVchNo.Focus();

                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Vch.No.');", true);
                    return;
                }


                TrnVchDeplicate();
                if (hdnMsgFlag.Text == "1")
                {
                    return;
                }


                A2ZSYSIDSDTO sysobj = A2ZSYSIDSDTO.GetUserInformation(Converter.GetInteger(hdnID.Text), "A2ZHKMCUS");

                if (sysobj.VPrintflag == false)
                {
                    lblVPrintFlag.Text = "0";
                }
                else
                {
                    lblVPrintFlag.Text = "1";
                }



                //A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                //int GLCode = Converter.GetInteger(dto.CashCode);

                int GLCode = Converter.GetInteger(hdnCashCode.Text);
                Int16 RecType = Converter.GetSmallInteger(1);
                A2ZRECCTRLNODTO getDTO = (A2ZRECCTRLNODTO.GetLastVoucherNo(GLCode, RecType));
                CtrlVchNo.Text = "C" + GLCode + "-" + getDTO.RecLastNo;


                if (ddlAccType.SelectedValue != "12")
                {
                    txtToDaysDate.Text = "July" + "-" + "June" + " " + lblEndYear.Text;
                }
                else if (lblCurrMth.Text == "12")
                {
                    txtToDaysDate.Text = "July" + "-" + "December" + " " + lblBegYear.Text;
                }
                else if (lblCurrMth.Text == "6")
                {
                    txtToDaysDate.Text = "January" + "-" + "June" + " " + lblEndYear.Text;
                }

                var prm = new object[6];

                prm[0] = ddlAccType.SelectedValue;
                prm[1] = CtrlVchNo.Text;
                prm[2] = txtVchNo.Text.Trim();
                prm[3] = hdnCashCode.Text;
                prm[4] = txtToDaysDate.Text;
                prm[5] = hdnID.Text;


                if (ddlAccType.SelectedValue != "12")
                {
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSUpdateInterestAmountYearly", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {
                        if (lblVPrintFlag.Text == "1")
                        {
                            PrintTrnVoucher();
                        }
                        else
                        {
                            UpdateMSG();
                        }



                        BtnCalculate.Visible = false;
                        lblVchNo.Visible = false;
                        txtVchNo.Visible = false;
                        BtnPost.Visible = false;
                        BtnPrint.Visible = true;
                        BtnReverse.Visible = true;
                        BtnExit.Visible = true;
                        UpdateBackUpStat();
                    }
                }
                else
                {
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSUpdateInterestAmount", prm, "A2ZCSMCUS"));
                    if (result == 0)
                    {
                        if (lblVPrintFlag.Text == "1")
                        {
                            PrintTrnVoucher();
                        }
                        else
                        {
                            UpdateMSG();
                        }



                        BtnCalculate.Visible = false;
                        lblVchNo.Visible = false;
                        txtVchNo.Visible = false;
                        BtnPost.Visible = false;
                        BtnPrint.Visible = true;
                        BtnReverse.Visible = true;
                        BtnExit.Visible = true;
                        UpdateBackUpStat();
                    }
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnPost_Click Problem');</script>");
                //throw ex;
            }

        }


        protected void PrintTrnVoucher()
        {
            try
            {
                //SessionStoreValue();

                Session["ProgFlag"] = "2";


                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                int accType = Converter.GetSmallInteger(ddlAccType.SelectedValue);
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, accType);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, lblPdate.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlAccType.SelectedItem.Text);



                DateTime Pdate = DateTime.ParseExact(lblPdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.VOUCHER_NO, txtVchNo.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Pdate);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Pdate);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, "TRANSFER");

                Int32 CSGL = Converter.GetSmallInteger(0);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, CSGL);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO3, 0);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, "");

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO4, hdnID.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, lblIDName.Text);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptCSSumTransactionVch");

                Response.Redirect("ReportServer.aspx", false);



            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.PrintTrnVoucher Problem');</script>");
                //throw ex;
            }

        }
        protected void UpdateBackUpStat()
        {
            A2ZERPSYSPRMDTO dto = A2ZERPSYSPRMDTO.GetParameterValue();
            CtrlBackUpStat.Text = Converter.GetString(dto.PrmBackUpStat);

            if (CtrlBackUpStat.Text == "1")
            {
                Int16 BStat = 0;
                int roweffect = A2ZERPSYSPRMDTO.UpdateBackUpStat(BStat);
                if (roweffect > 0)
                {

                }
            }
        }
        protected void BtnReverse_Click(object sender, EventArgs e)
        {
            try
            {

                var prm = new object[2];

                prm[0] = ddlAccType.SelectedValue;
                prm[1] = "18";


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSDeleteInterestAmount", prm, "A2ZCSMCUS"));
                if (result == 0)
                {
                    DeleteMSG();
                    BtnCalculate.Visible = true;
                    BtnPrint.Visible = true;
                    lblVchNo.Visible = false;
                    txtVchNo.Visible = false;
                    BtnPost.Visible = false;
                    BtnReverse.Visible = false;
                    BtnExit.Visible = true;
                    UpdateBackUpStat();
                }


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnReverse_Click Problem');</script>");
                //throw ex;
            }

        }

        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlAccType.SelectedValue != "12")
                {
                    txtToDaysDate.Text = "July" + "-" + "June" + " " + lblEndYear.Text;
                }
                else if (lblCurrMth.Text == "12")
                {
                    txtToDaysDate.Text = "July" + "-" + "December" + " " + lblBegYear.Text;
                }
                else if (lblCurrMth.Text == "6") 
                {
                    txtToDaysDate.Text = "January" + "-" + "June" + " " + lblEndYear.Text;
                }

                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;
                int accType = Converter.GetSmallInteger(ddlAccType.SelectedValue);
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, accType);

                if (lblCurrMth.Text == "12")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, 1);
                }
                else if (lblCurrMth.Text == "6")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, 2);
                }



                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, txtToDaysDate.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlAccType.SelectedItem.Text);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZCSMCUS");


                if (ddlAccType.SelectedValue == "12")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptAutoInterestAmount");
                }
                else 
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptYearlyInterestAmount");
                }
               

                Response.Redirect("ReportServer.aspx", false);

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnPrint_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }



    }
}