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
    public partial class HREditMonthlySalary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtEmpNo.Focus();
                DivAllowance.Visible = false;
                divDeduction.Visible = false;
                Label5.Visible = false;
                Label6.Visible = false;
                Label7.Visible = false;
                Label8.Visible = false;
                Label9.Visible = false;

                var dt = A2ZCSPARAMETERDTO.GetParameterValue();

                DateTime processDate = dt.ProcessDate;

                string date = processDate.ToString("dd/MM/yyyy");
                //CtrlProcDate.Text = date;


                txtToDaysDate.Text = Converter.GetString(String.Format("{0:Y}", processDate));


                int Month = processDate.Month;
                int Year = processDate.Year;

                hdnMonth.Text = Converter.GetString(Month);
                hdnYear.Text = Converter.GetString(Year);

            }
        }

        protected void gvAllowanceDetails()
        {
            string sqlquery3 = @"SELECT Code,Description FROM dbo.A2ZALLOWANCE Where Status='True'";
            gvAllowanceInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvAllowanceInfo, "A2ZHRMCUS");
        }
        protected void gvDeductionDetails()
        {
            string sqlquery3 = @"SELECT Code,Description FROM dbo.A2ZDEDUCTION Where status='True'";
            gvDeductionInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDeductionInfo, "A2ZHRMCUS");
        }

        
        protected void MoveAllowAmount()
        {

            for (int i = 0; i < gvAllowanceInfo.Rows.Count; i++)
            {
                Label lblAllowcode = (Label)gvAllowanceInfo.Rows[i].Cells[0].FindControl("lblAllowanceHead");
                Int16 Acode = Converter.GetSmallInteger(lblAllowcode.Text);
                TextBox txtAmount = (TextBox)gvAllowanceInfo.Rows[i].Cells[2].FindControl("txtAllowanceAmt");


                string sqlquery = @"SELECT EmpCode,BasicAmount,ConsolidatedAmt,GrossTotal,DeductTotal,NetPayment FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "' AND YEAR(SalDate)='" + hdnYear.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery, "A2ZHRMCUS");
                if (dt.Rows.Count > 0)
                {
                    lblBasic.Text = Converter.GetString(string.Format("{0:0,0.00}",(dt.Rows[0]["BasicAmount"])));
                    lblConsolidated.Text = Converter.GetString(string.Format("{0:0,0.00}", (dt.Rows[0]["ConsolidatedAmt"])));

                    lblGross.Text = Converter.GetString(string.Format("{0:0,0.00}", (dt.Rows[0]["GrossTotal"])));
                    lblTotalDed.Text = Converter.GetString(string.Format("{0:0,0.00}",(dt.Rows[0]["DeductTotal"])));
                    lblNetPay.Text = Converter.GetString(string.Format("{0:0,0.00}",(dt.Rows[0]["NetPayment"])));

                    string sqlquery1 = "SELECT EmpCode,TAAmount1 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo1='" + Acode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";                                                                         
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery1, "A2ZHRMCUS");
                    if(dt1.Rows.Count>0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}",dt1.Rows[0]["TAAmount1"]));
                    }
                    string sqlquery2 = "SELECT EmpCode,TAAmount2 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo2='" + Acode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery2, "A2ZHRMCUS");
                    if (dt2.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}",dt2.Rows[0]["TAAmount2"]));
                    }
                    string sqlquery3 = "SELECT EmpCode,TAAmount3 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo3='" + Acode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery3, "A2ZHRMCUS");
                    if (dt3.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}",dt3.Rows[0]["TAAmount3"]));
                    }
                    string sqlquery4 = "SELECT EmpCode,TAAmount4 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo4='" + Acode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery4, "A2ZHRMCUS");
                    if (dt4.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}",dt4.Rows[0]["TAAmount4"]));
                    }
                    string sqlquery5 = "SELECT EmpCode,TAAmount5 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo5='" + Acode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt5 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery5, "A2ZHRMCUS");
                    if (dt5.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt5.Rows[0]["TAAmount5"]));
                    }
                    string sqlquery6 = "SELECT EmpCode,TAAmount6 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo6='" + Acode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt6 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery6, "A2ZHRMCUS");
                    if (dt6.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt6.Rows[0]["TAAmount6"]));
                    }
                    string sqlquery7 = "SELECT EmpCode,TAAmount7 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo7='" + Acode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt7 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery7, "A2ZHRMCUS");
                    if (dt7.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt7.Rows[0]["TAAmount7"]));
                    }
                    string sqlquery8 = "SELECT EmpCode,TAAmount8 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo8='" + Acode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt8 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery8, "A2ZHRMCUS");
                    if (dt8.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt8.Rows[0]["TAAmount8"]));
                    }
                    string sqlquery9 = "SELECT EmpCode,TAAmount9 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo9='" + Acode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt9 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery9, "A2ZHRMCUS");
                    if (dt9.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt9.Rows[0]["TAAmount9"]));
                    }
                    string sqlquery10 = "SELECT EmpCode,TAAmount10 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo10='" + Acode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt10 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery10, "A2ZHRMCUS");
                    if (dt10.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt10.Rows[0]["TAAmount10"]));
                    }

                    string sqlquery11 = "SELECT EmpCode,TAAmount11 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo11='" + Acode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt11 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery11, "A2ZHRMCUS");
                    if (dt11.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt11.Rows[0]["TAAmount11"]));
                    }
                    string sqlquery12 = "SELECT EmpCode,TAAmount12 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo12='" + Acode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt12 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery12, "A2ZHRMCUS");
                    if (dt12.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt12.Rows[0]["TAAmount12"]));
                    }
                    string sqlquery13 = "SELECT EmpCode,TAAmount13 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo13='" + Acode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt13 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery13, "A2ZHRMCUS");
                    if (dt13.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt13.Rows[0]["TAAmount13"]));
                    }
                    string sqlquery14 = "SELECT EmpCode,TAAmount14 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo14='" + Acode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt14 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery14, "A2ZHRMCUS");
                    if (dt14.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt14.Rows[0]["TAAmount14"]));
                    }
                    string sqlquery15 = "SELECT EmpCode,TAAmount15 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo15='" + Acode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt15 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery15, "A2ZHRMCUS");
                    if (dt15.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt15.Rows[0]["TAAmount15"]));
                    }
                    string sqlquery16 = "SELECT EmpCode,TAAmount16 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo16='" + Acode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt16 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery16, "A2ZHRMCUS");
                    if (dt16.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt16.Rows[0]["TAAmount16"]));
                    }
                    string sqlquery17 = "SELECT EmpCode,TAAmount17 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo17='" + Acode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt17 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery17, "A2ZHRMCUS");
                    if (dt17.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt17.Rows[0]["TAAmount17"]));
                    }
                    string sqlquery18 = "SELECT EmpCode,TAAmount18 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo18='" + Acode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt18 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery18, "A2ZHRMCUS");
                    if (dt18.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt18.Rows[0]["TAAmount18"]));
                    }
                    string sqlquery19 = "SELECT EmpCode,TAAmount19 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo19='" + Acode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt19 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery19, "A2ZHRMCUS");
                    if (dt19.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt19.Rows[0]["TAAmount19"]));
                    }
                    string sqlquery20 = "SELECT EmpCode,TAAmount20 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo20='" + Acode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt20 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery20, "A2ZHRMCUS");
                    if (dt20.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt20.Rows[0]["TAAmount20"]));
                    }

                }

            }
        }

        protected void MoveDeductAmount()
        {

            for (int i = 0; i < gvDeductionInfo.Rows.Count; i++)
            {
                Label lblDedcode = (Label)gvDeductionInfo.Rows[i].Cells[0].FindControl("lblDeductionHead");
                Int16 Dcode = Converter.GetSmallInteger(lblDedcode.Text);
                TextBox txtAmount = (TextBox)gvDeductionInfo.Rows[i].Cells[2].FindControl("txtDeductionAmt");


                string sqlquery = @"SELECT EmpCode FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery, "A2ZHRMCUS");
                if (dt.Rows.Count > 0)
                {
                    string sqlquery1 = "SELECT EmpCode,TDAmount1 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo1='" + Dcode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery1, "A2ZHRMCUS");
                    if (dt1.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt1.Rows[0]["TDAmount1"]));
                    }
                    string sqlquery2 = "SELECT EmpCode,TDAmount2 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo2='" + Dcode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery2, "A2ZHRMCUS");
                    if (dt2.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt2.Rows[0]["TDAmount2"]));
                    }
                    string sqlquery3 = "SELECT EmpCode,TDAmount3 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo3='" + Dcode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery3, "A2ZHRMCUS");
                    if (dt3.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt3.Rows[0]["TDAmount3"]));
                    }
                    string sqlquery4 = "SELECT EmpCode,TDAmount4 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo4='" + Dcode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery4, "A2ZHRMCUS");
                    if (dt4.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}",dt4.Rows[0]["TDAmount4"]));
                    }
                    string sqlquery5 = "SELECT EmpCode,TDAmount5 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo5='" + Dcode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt5 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery5, "A2ZHRMCUS");
                    if (dt5.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt5.Rows[0]["TDAmount5"]));
                    }
                    string sqlquery6 = "SELECT EmpCode,TDAmount6 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo6='" + Dcode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt6 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery6, "A2ZHRMCUS");
                    if (dt6.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}",dt6.Rows[0]["TDAmount6"]));
                    }
                    string sqlquery7 = "SELECT EmpCode,TDAmount7 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo7='" + Dcode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt7 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery7, "A2ZHRMCUS");
                    if (dt7.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt7.Rows[0]["TDAmount7"]));
                    }
                    string sqlquery8 = "SELECT EmpCode,TDAmount8 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo8='" + Dcode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt8 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery8, "A2ZHRMCUS");
                    if (dt8.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt8.Rows[0]["TDAmount8"]));
                    }
                    string sqlquery9 = "SELECT EmpCode,TDAmount9 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo9='" + Dcode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt9 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery9, "A2ZHRMCUS");
                    if (dt9.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt9.Rows[0]["TDAmount9"]));
                    }
                    string sqlquery10 = "SELECT EmpCode,TDAmount10 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo10='" + Dcode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt10 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery10, "A2ZHRMCUS");
                    if (dt10.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt10.Rows[0]["TDAmount10"]));
                    }

                    string sqlquery11 = "SELECT EmpCode,TDAmount11 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo11='" + Dcode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt11 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery11, "A2ZHRMCUS");
                    if (dt11.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt11.Rows[0]["TDAmount11"]));
                    }
                    string sqlquery12 = "SELECT EmpCode,TDAmount12 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo12='" + Dcode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt12 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery12, "A2ZHRMCUS");
                    if (dt12.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt12.Rows[0]["TDAmount12"]));
                    }
                    string sqlquery13 = "SELECT EmpCode,TDAmount13 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo13='" + Dcode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt13 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery13, "A2ZHRMCUS");
                    if (dt13.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt13.Rows[0]["TDAmount13"]));
                    }
                    string sqlquery14 = "SELECT EmpCode,TDAmount14 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo14='" + Dcode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt14 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery14, "A2ZHRMCUS");
                    if (dt14.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt14.Rows[0]["TDAmount14"]));
                    }
                    string sqlquery15 = "SELECT EmpCode,TDAmount15 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo15='" + Dcode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt15 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery15, "A2ZHRMCUS");
                    if (dt15.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt15.Rows[0]["TDAmount15"]));
                    }
                    string sqlquery16 = "SELECT EmpCode,TDAmount16 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo16='" + Dcode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt16 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery16, "A2ZHRMCUS");
                    if (dt16.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt16.Rows[0]["TDAmount16"]));
                    }
                    string sqlquery17 = "SELECT EmpCode,TDAmount17 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo17='" + Dcode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt17 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery17, "A2ZHRMCUS");
                    if (dt17.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt17.Rows[0]["TDAmount17"]));
                    }
                    string sqlquery18 = "SELECT EmpCode,TDAmount18 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo18='" + Dcode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt18 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery18, "A2ZHRMCUS");
                    if (dt18.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt18.Rows[0]["TDAmount18"]));
                    }
                    string sqlquery19 = "SELECT EmpCode,TDAmount19 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo19='" + Dcode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt19 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery19, "A2ZHRMCUS");
                    if (dt19.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt19.Rows[0]["TDAmount19"]));
                    }
                    string sqlquery20 = "SELECT EmpCode,TDAmount20 FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo20='" + Dcode + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";    
                    DataTable dt20 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery20, "A2ZHRMCUS");
                    if (dt20.Rows.Count > 0)
                    {
                        txtAmount.Text = Converter.GetString(string.Format("{0:0,0.00}", dt20.Rows[0]["TDAmount20"]));
                    }

                }

            }
        }


        protected void gvAllowanceInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void gvDeductionInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "60px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void UpdateNetTotal()
        {
            string sqlquery = @"SELECT EmpCode,BasicAmount,ConsolidatedAmt,TAAmount1,TAAmount2,TAAmount3,TAAmount4,TAAmount5,TAAmount6,
                              TAAmount7,TAAmount8,TAAmount9,TAAmount10,TAAmount11,TAAmount12,TAAmount13,TAAmount14,TAAmount15,
                              TAAmount16,TAAmount17,TAAmount18,TAAmount19,TAAmount20,TDAmount1,TDAmount2,TDAmount3,TDAmount4,TDAmount5,
                              TDAmount6,TDAmount7,TDAmount8,TDAmount9,TDAmount10,TDAmount11,TDAmount12,TDAmount13,TDAmount14,TDAmount15,
                              TDAmount16,TDAmount17,TDAmount18,TDAmount19,TDAmount20,GrossTotal,DeductTotal,NetPayment 
                              FROM  dbo.A2ZEMPFSALARY WHERE EmpCode='" + txtEmpNo.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery, "A2ZHRMCUS");
            if (dt.Rows.Count > 0)
            {
                CtrlBasic.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["BasicAmount"]));
                double Basic = Converter.GetDouble(CtrlBasic.Text);
                CtrlConsolidated.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["ConsolidatedAmt"]));
                double Consolidated = Converter.GetDouble(CtrlConsolidated.Text);

                CtrlTAAmt1.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TAAmount1"]));
                double TAAmt1 = Converter.GetDouble(CtrlTAAmt1.Text);
                CtrlTAAmt2.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TAAmount2"]));
                double TAAmt2 = Converter.GetDouble(CtrlTAAmt2.Text);
                CtrlTAAmt3.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TAAmount3"]));
                double TAAmt3 = Converter.GetDouble(CtrlTAAmt3.Text);
                CtrlTAAmt4.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TAAmount4"]));
                double TAAmt4 = Converter.GetDouble(CtrlTAAmt4.Text);
                CtrlTAAmt5.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TAAmount5"]));
                double TAAmt5 = Converter.GetDouble(CtrlTAAmt5.Text);
                CtrlTAAmt6.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TAAmount6"]));
                double TAAmt6 = Converter.GetDouble(CtrlTAAmt6.Text);
                CtrlTAAmt7.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TAAmount7"]));
                double TAAmt7 = Converter.GetDouble(CtrlTAAmt7.Text);
                CtrlTAAmt8.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TAAmount8"]));
                double TAAmt8 = Converter.GetDouble(CtrlTAAmt8.Text);
                CtrlTAAmt9.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TAAmount9"]));
                double TAAmt9 = Converter.GetDouble(CtrlTAAmt9.Text);
                CtrlTAAmt10.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TAAmount10"]));
                double TAAmt10 = Converter.GetDouble(CtrlTAAmt10.Text);
                CtrlTAAmt11.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TAAmount11"]));
                double TAAmt11 = Converter.GetDouble(CtrlTAAmt11.Text);
                CtrlTAAmt12.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TAAmount12"]));
                double TAAmt12 = Converter.GetDouble(CtrlTAAmt12.Text);
                CtrlTAAmt13.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TAAmount13"]));
                double TAAmt13 = Converter.GetDouble(CtrlTAAmt13.Text);
                CtrlTAAmt14.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TAAmount14"]));
                double TAAmt14 = Converter.GetDouble(CtrlTAAmt14.Text);
                CtrlTAAmt15.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TAAmount15"]));
                double TAAmt15 = Converter.GetDouble(CtrlTAAmt15.Text);
                CtrlTAAmt16.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TAAmount16"]));
                double TAAmt16 = Converter.GetDouble(CtrlTAAmt16.Text);
                CtrlTAAmt17.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TAAmount17"]));
                double TAAmt17 = Converter.GetDouble(CtrlTAAmt17.Text);
                CtrlTAAmt18.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TAAmount18"]));
                double TAAmt18 = Converter.GetDouble(CtrlTAAmt18.Text);
                CtrlTAAmt19.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TAAmount19"]));
                double TAAmt19 = Converter.GetDouble(CtrlTAAmt19.Text);
                CtrlTAAmt20.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TAAmount20"]));
                double TAAmt20 = Converter.GetDouble(CtrlTAAmt20.Text);

                CtrlTDAmt1.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TDAmount1"]));
                double TDAmt1 = Converter.GetDouble(CtrlTDAmt1.Text);
                CtrlTDAmt2.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TDAmount2"]));
                double TDAmt2 = Converter.GetDouble(CtrlTDAmt2.Text);
                CtrlTDAmt3.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TDAmount3"]));
                double TDAmt3 = Converter.GetDouble(CtrlTDAmt3.Text);
                CtrlTDAmt4.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TDAmount4"]));
                double TDAmt4 = Converter.GetDouble(CtrlTDAmt4.Text);
                CtrlTDAmt5.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TDAmount5"]));
                double TDAmt5 = Converter.GetDouble(CtrlTDAmt5.Text);
                CtrlTDAmt6.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TDAmount6"]));
                double TDAmt6 = Converter.GetDouble(CtrlTDAmt6.Text);
                CtrlTDAmt7.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TDAmount7"]));
                double TDAmt7 = Converter.GetDouble(CtrlTDAmt7.Text);
                CtrlTDAmt8.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TDAmount8"]));
                double TDAmt8 = Converter.GetDouble(CtrlTDAmt8.Text);
                CtrlTDAmt9.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TDAmount9"]));
                double TDAmt9 = Converter.GetDouble(CtrlTDAmt9.Text);
                CtrlTDAmt10.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TDAmount10"]));
                double TDAmt10 = Converter.GetDouble(CtrlTDAmt10.Text);
                CtrlTDAmt11.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TDAmount11"]));
                double TDAmt11 = Converter.GetDouble(CtrlTDAmt11.Text);
                CtrlTDAmt12.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TDAmount12"]));
                double TDAmt12 = Converter.GetDouble(CtrlTDAmt12.Text);
                CtrlTDAmt13.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TDAmount13"]));
                double TDAmt13 = Converter.GetDouble(CtrlTDAmt13.Text);
                CtrlTDAmt14.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TDAmount14"]));
                double TDAmt14 = Converter.GetDouble(CtrlTDAmt14.Text);
                CtrlTDAmt15.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TDAmount15"]));
                double TDAmt15 = Converter.GetDouble(CtrlTDAmt15.Text);
                CtrlTDAmt16.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TDAmount16"]));
                double TDAmt16 = Converter.GetDouble(CtrlTDAmt16.Text);
                CtrlTDAmt17.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TDAmount17"]));
                double TDAmt17 = Converter.GetDouble(CtrlTDAmt17.Text);
                CtrlTDAmt18.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TDAmount18"]));
                double TDAmt18 = Converter.GetDouble(CtrlTDAmt18.Text);
                CtrlTDAmt19.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TDAmount19"]));
                double TDAmt19 = Converter.GetDouble(CtrlTDAmt19.Text);
                CtrlTDAmt20.Text = Converter.GetString(string.Format("{0:0,0.00}", dt.Rows[0]["TDAmount20"]));
                double TDAmt20 = Converter.GetDouble(CtrlTDAmt20.Text);

                CtrlGrossTotal.Text = "0";
                CtrlDeductTotal.Text = "0";
                CtrlNetPayment.Text = "0";

                CtrlGrossTotal.Text = Converter.GetString(String.Format("{0:0,0.00}", (Basic + Consolidated + TAAmt1 + TAAmt2 + 
                    TAAmt3 + TAAmt4 + TAAmt5 + TAAmt6 + TAAmt7 + TAAmt8 + TAAmt9 + TAAmt10 + TAAmt11 + TAAmt12 + TAAmt13 + 
                    TAAmt14 + TAAmt15 + TAAmt16 + TAAmt17 + TAAmt18 + TAAmt19 + TAAmt20)));
                double GrossTot = Converter.GetDouble(CtrlGrossTotal.Text);

                CtrlDeductTotal.Text = Converter.GetString(String.Format("{0:0,0.00}", (TDAmt1 + TDAmt2 + TDAmt3 + TDAmt4 + TDAmt5 +
                    TDAmt6 + TDAmt7 + TDAmt8 + TDAmt9 + TDAmt10 + TDAmt11 + TDAmt12 + TDAmt13 + TDAmt14 + TDAmt15 + TDAmt16 +
                    TDAmt17 + TDAmt18 + TDAmt19 + TDAmt20)));
                double DedTot = Converter.GetDouble(CtrlDeductTotal.Text);

                CtrlNetPayment.Text = Converter.GetString(String.Format("{0:0,0.00}", (GrossTot - DedTot)));

                string strqry = "UPDATE  A2ZEMPFSALARY SET GrossTotal='" + CtrlGrossTotal.Text + "',DeductTotal='" + CtrlDeductTotal.Text + "',NetPayment='" + CtrlNetPayment.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
                int rowefect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZHRMCUS"));

                lblGross.Text = Converter.GetString(string.Format("{0:0,0.00}", (CtrlGrossTotal.Text)));
                lblTotalDed.Text = Converter.GetString(string.Format("{0:0,0.00}", (CtrlDeductTotal.Text)));
                lblNetPay.Text = Converter.GetString(string.Format("{0:0,0.00}", (CtrlNetPayment.Text)));

            }
                  
        }
        protected void gvAllowanceInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvAllowanceInfo.Rows[e.RowIndex];
            Label lblAllowanceCode = (Label)row.FindControl("lblAllowanceHead");
            TextBox txtAllowanceAmt = (TextBox)row.FindControl("txtAllowanceAmt");

            string strqry1 = "UPDATE  A2ZEMPFSALARY SET TAAmount1='" + txtAllowanceAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo1='" + lblAllowanceCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry1, "A2ZHRMCUS"));

            string strqry2 = "UPDATE  A2ZEMPFSALARY SET TAAmount2='" + txtAllowanceAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo2='" + lblAllowanceCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry2, "A2ZHRMCUS"));

            string strqry3 = "UPDATE  A2ZEMPFSALARY SET TAAmount3='" + txtAllowanceAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo3='" + lblAllowanceCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect3 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry3, "A2ZHRMCUS"));

            string strqry4 = "UPDATE  A2ZEMPFSALARY SET TAAmount4='" + txtAllowanceAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo4='" + lblAllowanceCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect4 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry4, "A2ZHRMCUS"));

            string strqry5 = "UPDATE  A2ZEMPFSALARY SET TAAmount5='" + txtAllowanceAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo5='" + lblAllowanceCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect5 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry5, "A2ZHRMCUS"));

            string strqry6 = "UPDATE  A2ZEMPFSALARY SET TAAmount6='" + txtAllowanceAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo6='" + lblAllowanceCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect6 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry6, "A2ZHRMCUS"));

            string strqry7 = "UPDATE  A2ZEMPFSALARY SET TAAmount7='" + txtAllowanceAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo7='" + lblAllowanceCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect7 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry7, "A2ZHRMCUS"));

            string strqry8 = "UPDATE  A2ZEMPFSALARY SET TAAmount8='" + txtAllowanceAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo8='" + lblAllowanceCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect8 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry8, "A2ZHRMCUS"));

            string strqry9 = "UPDATE  A2ZEMPFSALARY SET TAAmount9='" + txtAllowanceAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo9='" + lblAllowanceCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect9 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry9, "A2ZHRMCUS"));

            string strqry10 = "UPDATE  A2ZEMPFSALARY SET TAAmount10='" + txtAllowanceAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo10='" + lblAllowanceCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect10 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry10, "A2ZHRMCUS"));

            string strqry11 = "UPDATE  A2ZEMPFSALARY SET TAAmount11='" + txtAllowanceAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo11='" + lblAllowanceCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect11 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry11, "A2ZHRMCUS"));

            string strqry12 = "UPDATE  A2ZEMPFSALARY SET TAAmount12='" + txtAllowanceAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo12='" + lblAllowanceCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect12 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry12, "A2ZHRMCUS"));

            string strqry13 = "UPDATE  A2ZEMPFSALARY SET TAAmount13='" + txtAllowanceAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo13='" + lblAllowanceCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect13 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry13, "A2ZHRMCUS"));

            string strqry14 = "UPDATE  A2ZEMPFSALARY SET TAAmount14='" + txtAllowanceAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo14='" + lblAllowanceCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect14 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry14, "A2ZHRMCUS"));

            string strqry15 = "UPDATE  A2ZEMPFSALARY SET TAAmount15='" + txtAllowanceAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo15='" + lblAllowanceCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect15 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry15, "A2ZHRMCUS"));

            string strqry16 = "UPDATE  A2ZEMPFSALARY SET TAAmount16='" + txtAllowanceAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo16='" + lblAllowanceCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect16 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry16, "A2ZHRMCUS"));

            string strqry17 = "UPDATE  A2ZEMPFSALARY SET TAAmount17='" + txtAllowanceAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo17='" + lblAllowanceCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect17 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry17, "A2ZHRMCUS"));

            string strqry18 = "UPDATE  A2ZEMPFSALARY SET TAAmount18='" + txtAllowanceAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo18='" + lblAllowanceCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect18 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry18, "A2ZHRMCUS"));

            string strqry19 = "UPDATE  A2ZEMPFSALARY SET TAAmount19='" + txtAllowanceAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo19='" + lblAllowanceCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect19 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry19, "A2ZHRMCUS"));

            string strqry20 = "UPDATE  A2ZEMPFSALARY SET TAAmount20='" + txtAllowanceAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TACodeNo20='" + lblAllowanceCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect20 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry20, "A2ZHRMCUS"));


            gvAllowanceInfo.EditIndex = -1;
            gvAllowanceDetails();
            UpdateNetTotal();
            MoveAllowAmount();
            txtAllowanceAmt.Enabled = false;
        }

        protected void gvAllowanceInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAllowanceInfo.EditIndex = e.NewEditIndex;
            gvAllowanceDetails();
            MoveAllowAmount();
            GridViewRow row = gvAllowanceInfo.Rows[gvAllowanceInfo.EditIndex];
            TextBox txtAllowanceAmt = (TextBox)row.FindControl("txtAllowanceAmt");
            gvAllowanceInfo.EditRowStyle.BackColor = System.Drawing.Color.LightSkyBlue;
            txtAllowanceAmt.Enabled = true;

        }

        protected void gvAllowanceInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAllowanceInfo.EditIndex = -1;
            gvAllowanceDetails();
            MoveAllowAmount();
        }

        protected void gvDeductionInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvDeductionInfo.Rows[e.RowIndex];
            Label lblDeductionCode = (Label)row.FindControl("lblDeductionHead");
            TextBox txtDeductionAmt = (TextBox)row.FindControl("txtDeductionAmt");

            string strqry1 = "UPDATE  A2ZEMPFSALARY SET TDAmount1='" + txtDeductionAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo1='" + lblDeductionCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry1, "A2ZHRMCUS"));

            string strqry2 = "UPDATE  A2ZEMPFSALARY SET TDAmount2='" + txtDeductionAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo2='" + lblDeductionCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect2 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry2, "A2ZHRMCUS"));

            string strqry3 = "UPDATE  A2ZEMPFSALARY SET TDAmount3='" + txtDeductionAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo3='" + lblDeductionCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect3 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry3, "A2ZHRMCUS"));

            string strqry4 = "UPDATE  A2ZEMPFSALARY SET TDAmount4='" + txtDeductionAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo4='" + lblDeductionCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect4 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry4, "A2ZHRMCUS"));

            string strqry5 = "UPDATE  A2ZEMPFSALARY SET TDAmount5='" + txtDeductionAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo5='" + lblDeductionCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect5 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry5, "A2ZHRMCUS"));

            string strqry6 = "UPDATE  A2ZEMPFSALARY SET TDAmount6='" + txtDeductionAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo6='" + lblDeductionCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect6 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry6, "A2ZHRMCUS"));

            string strqry7 = "UPDATE  A2ZEMPFSALARY SET TDAmount7='" + txtDeductionAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo7='" + lblDeductionCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect7 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry7, "A2ZHRMCUS"));

            string strqry8 = "UPDATE  A2ZEMPFSALARY SET TDAmount8='" + txtDeductionAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo8='" + lblDeductionCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect8 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry8, "A2ZHRMCUS"));

            string strqry9 = "UPDATE  A2ZEMPFSALARY SET TDAmount9='" + txtDeductionAmt.Text + "' WHERE TDCodeNo9='" + lblDeductionCode.Text + "'";
            int rowefect9 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry9, "A2ZHRMCUS"));

            string strqry10 = "UPDATE  A2ZEMPFSALARY SET TDAmount10='" + txtDeductionAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo10='" + lblDeductionCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect10 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry10, "A2ZHRMCUS"));

            string strqry11 = "UPDATE  A2ZEMPFSALARY SET TDAmount11='" + txtDeductionAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo11='" + lblDeductionCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect11 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry11, "A2ZHRMCUS"));

            string strqry12 = "UPDATE  A2ZEMPFSALARY SET TDAmount12='" + txtDeductionAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo12='" + lblDeductionCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect12 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry12, "A2ZHRMCUS"));

            string strqry13 = "UPDATE  A2ZEMPFSALARY SET TDAmount13='" + txtDeductionAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo13='" + lblDeductionCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect13 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry13, "A2ZHRMCUS"));

            string strqry14 = "UPDATE  A2ZEMPFSALARY SET TDAmount14='" + txtDeductionAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo14='" + lblDeductionCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect14 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry14, "A2ZHRMCUS"));

            string strqry15 = "UPDATE  A2ZEMPFSALARY SET TDAmount15='" + txtDeductionAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo15='" + lblDeductionCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect15 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry15, "A2ZHRMCUS"));

            string strqry16 = "UPDATE  A2ZEMPFSALARY SET TDAmount16='" + txtDeductionAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo16='" + lblDeductionCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect16 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry16, "A2ZHRMCUS"));

            string strqry17 = "UPDATE  A2ZEMPFSALARY SET TDAmount17='" + txtDeductionAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo17='" + lblDeductionCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect17 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry17, "A2ZHRMCUS"));

            string strqry18 = "UPDATE  A2ZEMPFSALARY SET TDAmount18='" + txtDeductionAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo18='" + lblDeductionCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect18 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry18, "A2ZHRMCUS"));

            string strqry19 = "UPDATE  A2ZEMPFSALARY SET TDAmount19='" + txtDeductionAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo19='" + lblDeductionCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect19 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry19, "A2ZHRMCUS"));

            string strqry20 = "UPDATE  A2ZEMPFSALARY SET TDAmount20='" + txtDeductionAmt.Text + "' WHERE EmpCode='" + txtEmpNo.Text + "' AND TDCodeNo20='" + lblDeductionCode.Text + "' AND MONTH(SalDate)='" + hdnMonth.Text + "'";
            int rowefect20 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry20, "A2ZHRMCUS"));

            gvDeductionInfo.EditIndex = -1;
            gvDeductionDetails();
            UpdateNetTotal();
            MoveDeductAmount();
            txtDeductionAmt.Enabled = false;
        }

        protected void gvDeductionInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDeductionInfo.EditIndex = e.NewEditIndex;
            gvDeductionDetails();
            MoveDeductAmount();
            GridViewRow row = gvDeductionInfo.Rows[gvDeductionInfo.EditIndex];
            TextBox txtDeductionAmt = (TextBox)row.FindControl("txtDeductionAmt");
            gvDeductionInfo.EditRowStyle.BackColor = System.Drawing.Color.LightSkyBlue;
            txtDeductionAmt.Enabled = true;


        }

        protected void gvDeductionInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDeductionInfo.EditIndex = -1;
            gvDeductionDetails();
            MoveDeductAmount();
        }

        protected void btnSumbit_Click(object sender, EventArgs e)
        {
            int EmpID = Converter.GetInteger(txtEmpNo.Text);
            A2ZEMPLOYEEDTO getDTO = (A2ZEMPLOYEEDTO.GetInformation(EmpID));
            if (getDTO.EmployeeID > 0)
            {
                lblName.Text = Converter.GetString(getDTO.EmployeeName);
                CtrlDesignation.Text = Converter.GetString(getDTO.EmpDesignation);
                CtrlGrade.Text = Converter.GetString(getDTO.EmpGrade);

                Int16 Desig = Converter.GetSmallInteger(CtrlDesignation.Text);
                A2ZDESIGNATIONDTO get1DTO = (A2ZDESIGNATIONDTO.GetInformation(Desig));
                if (get1DTO.DesignationCode > 0)
                {
                    lblDesign.Text = Converter.GetString(get1DTO.DesignationDescription);
                }

                Int16 Grade = Converter.GetSmallInteger(CtrlGrade.Text);
                A2ZGRADEDTO get2DTO = (A2ZGRADEDTO.GetGradeInformation(Grade));
                if (get2DTO.ID > 0)
                {
                    lblGrade.Text = Converter.GetString(get2DTO.GradeDesc);
                }
                Label5.Visible = true;
                Label6.Visible = true;
                Label7.Visible = true;
                Label8.Visible = true;
                Label9.Visible = true;
                DivAllowance.Visible = true;
                divDeduction.Visible = true;
                gvAllowanceDetails();
                MoveAllowAmount();
                gvDeductionDetails();
                MoveDeductAmount();

            }
        }

           

    }
}