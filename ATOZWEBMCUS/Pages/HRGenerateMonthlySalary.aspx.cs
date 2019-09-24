using ATOZWEBMCUS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HumanResource;
using DataAccessLayer.Utility;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.HouseKeeping;

namespace ATOZWEBMCUS.Pages
{
    public partial class HRGenerateMonthlySalary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //lblTotalAmt.Visible = false;

                gvDetailInfoFDR.Visible = false;

                hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                hdnCashCode.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_GLCASHCODE));


                var dt = A2ZHRPARAMETERDTO.GetParameterValue();

                DateTime processDate = dt.ProcessDate;

                txtToDaysDate.Text = Converter.GetString(String.Format("{0:Y}", processDate));

                int lastDay = DateTime.DaysInMonth(processDate.Year, processDate.Month);

                var A = new DateTime(processDate.Year, processDate.Month, lastDay);

                hdnSalDate.Text = Converter.GetString(A);
                hdnNoDays.Text = Converter.GetString(lastDay);

            }

        }

        protected void gvPreview()
        {

            string sqlquery3 = "SELECT EmpCode,EmpName,EmpGradeDesc,EmpDesigDesc,BasicAmount,ConsolidatedAmt,TAAmount1,TAAmount2,TAAmount3,TAAmount4,TAAmount5,TAAmount6,TAAmount7,TAAmount8,TAAmount9,TAAmount10,TAAmount11,TAAmount12,TAAmount13,TAAmount14,TAAmount15,TAAmount16,TAAmount17,TAAmount18,TAAmount19,TAAmount20,GrossTotal,TDAmount1,TDAmount2,TDAmount3,TDAmount4,TDAmount5,TDAmount6,TDAmount7,TDAmount8,TDAmount9,TDAmount10,TDAmount11,TDAmount12,TDAmount13,TDAmount14,TDAmount15,TDAmount16,TDAmount17,TDAmount18,TDAmount19,TDAmount20,DeductTotal,NetPayment FROM A2ZEMPFSALARY";
            gvDetailInfoFDR = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfoFDR, "A2ZHRMCUS");
        }

        //protected void gvSumValue()
        //{
        //    Decimal sumfdAmt = 0;
        //    Decimal sumUptoLastMnth = 0;
        //    Decimal sumThisMnth = 0;
        //    Decimal sumUptoMnth = 0;

        //    lblTotalAmt.Visible = true;
        //    txtTotalFDAmt.Visible = true;
        //    txtTotaluptolastmnth.Visible = true;
        //    txtTotalthisMonth.Visible = true;
        //    txtTotalUptoMonth.Visible = true;


        //    for (int i = 0; i < gvDetailInfoFDR.Rows.Count; ++i)
        //    {

        //        sumfdAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfoFDR.Rows[i].Cells[5].Text));
        //        sumUptoLastMnth += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfoFDR.Rows[i].Cells[7].Text));
        //        sumThisMnth += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfoFDR.Rows[i].Cells[8].Text));
        //        sumUptoMnth += Convert.ToDecimal(String.Format("{0:0,0.00}", gvDetailInfoFDR.Rows[i].Cells[9].Text));
        //    }

        //    txtTotalFDAmt.Text = Convert.ToString(String.Format("{0:0,0.00}",sumfdAmt));
        //    txtTotaluptolastmnth.Text = Convert.ToString(String.Format("{0:0,0.00}", sumUptoLastMnth));
        //    txtTotalthisMonth.Text = Convert.ToString(String.Format("{0:0,0.00}", sumThisMnth));
        //    txtTotalUptoMonth.Text = Convert.ToString(String.Format("{0:0,0.00}", sumUptoMnth));


        //}

        protected void gvDetailInfoFDR_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "100px");
                e.Row.Style.Add("top", "-1500px");
            }
        }

        protected void ProcessMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    //String cstext1 = "confirm('Records Already Added');";
            //    String cstext1 = "alert('Salary Preparaion Completed');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Salary Preparaion Completed');", true);
            return;

        }

        protected void InvalidProcessMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Current Month Salary Already Posted');", true);
            return;

        }

        protected void BtnProcess_Click(object sender, EventArgs e)
        {
            try
            {

                //A2ZERPSYSPRMDTO dto = A2ZERPSYSPRMDTO.GetParameterValue();
                //CtrlSalPostStat.Text = Converter.GetString(dto.PrmSalPostStat);

                //if (CtrlSalPostStat.Text == "1")
                //{
                //    InvalidProcessMSG();
                //    return;
                //}


                var prm = new object[3];

                prm[0] = hdnID.Text;
                prm[1] = hdnSalDate.Text;
                prm[2] = hdnNoDays.Text;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_HRGenerateMonthlySalary", prm, "A2ZHRMCUS"));
                if (result == 0)
                {
                    UpdateSalPostStat();
                    ProcessMSG();



                    //gvDetailInfoFDR.Visible = true;

                    //gvPreview();

                    //string qry1 = @"SELECT TACodeNo1, TACodeNo2, TACodeNo3, TACodeNo4, TACodeNo5,TACodeNo6,TACodeNo7,TACodeNo8,TACodeNo9,TACodeNo10,TACodeNo11,TACodeNo12, TACodeNo13, TACodeNo14, TACodeNo15,TACodeNo16,TACodeNo17,TACodeNo18,TACodeNo19,TACodeNo20, TDCodeNo1, TDCodeNo2, TDCodeNo3, TDCodeNo4, TDCodeNo5,TDCodeNo6,TDCodeNo7,TDCodeNo8,TDCodeNo9,TDCodeNo10,TDCodeNo11, TDCodeNo12, TDCodeNo13, TDCodeNo14, TDCodeNo15,TDCodeNo16,TDCodeNo17,TDCodeNo18,TDCodeNo19,TDCodeNo20 FROM  dbo.A2ZEMPFSALARY ";
                    //DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHRMCUS");
                    //if (dt1.Rows.Count > 0)
                    //{
                    //    gvDetailInfoFDR.HeaderRow.Cells[6].ID = Converter.GetString(dt1.Rows[0]["TACodeNo1"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[7].ID = Converter.GetString(dt1.Rows[0]["TACodeNo2"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[8].ID = Converter.GetString(dt1.Rows[0]["TACodeNo3"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[9].ID = Converter.GetString(dt1.Rows[0]["TACodeNo4"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[10].ID = Converter.GetString(dt1.Rows[0]["TACodeNo5"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[11].ID = Converter.GetString(dt1.Rows[0]["TACodeNo6"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[12].ID = Converter.GetString(dt1.Rows[0]["TACodeNo7"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[13].ID = Converter.GetString(dt1.Rows[0]["TACodeNo8"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[14].ID = Converter.GetString(dt1.Rows[0]["TACodeNo9"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[15].ID = Converter.GetString(dt1.Rows[0]["TACodeNo10"]);

                    //    gvDetailInfoFDR.HeaderRow.Cells[16].ID = Converter.GetString(dt1.Rows[0]["TACodeNo11"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[17].ID = Converter.GetString(dt1.Rows[0]["TACodeNo12"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[18].ID = Converter.GetString(dt1.Rows[0]["TACodeNo13"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[19].ID = Converter.GetString(dt1.Rows[0]["TACodeNo14"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[20].ID = Converter.GetString(dt1.Rows[0]["TACodeNo15"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[21].ID = Converter.GetString(dt1.Rows[0]["TACodeNo16"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[22].ID = Converter.GetString(dt1.Rows[0]["TACodeNo17"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[23].ID = Converter.GetString(dt1.Rows[0]["TACodeNo18"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[24].ID = Converter.GetString(dt1.Rows[0]["TACodeNo19"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[25].ID = Converter.GetString(dt1.Rows[0]["TACodeNo20"]);



                    //    gvDetailInfoFDR.HeaderRow.Cells[27].ID = Converter.GetString(dt1.Rows[0]["TDCodeNo1"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[28].ID = Converter.GetString(dt1.Rows[0]["TDCodeNo2"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[29].ID = Converter.GetString(dt1.Rows[0]["TDCodeNo3"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[30].ID = Converter.GetString(dt1.Rows[0]["TDCodeNo4"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[31].ID = Converter.GetString(dt1.Rows[0]["TDCodeNo5"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[32].ID = Converter.GetString(dt1.Rows[0]["TDCodeNo6"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[33].ID = Converter.GetString(dt1.Rows[0]["TDCodeNo7"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[34].ID = Converter.GetString(dt1.Rows[0]["TDCodeNo8"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[35].ID = Converter.GetString(dt1.Rows[0]["TDCodeNo9"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[36].ID = Converter.GetString(dt1.Rows[0]["TDCodeNo10"]);

                    //    gvDetailInfoFDR.HeaderRow.Cells[37].ID = Converter.GetString(dt1.Rows[0]["TDCodeNo11"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[38].ID = Converter.GetString(dt1.Rows[0]["TDCodeNo12"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[39].ID = Converter.GetString(dt1.Rows[0]["TDCodeNo13"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[40].ID = Converter.GetString(dt1.Rows[0]["TDCodeNo14"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[41].ID = Converter.GetString(dt1.Rows[0]["TDCodeNo15"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[42].ID = Converter.GetString(dt1.Rows[0]["TDCodeNo16"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[43].ID = Converter.GetString(dt1.Rows[0]["TDCodeNo17"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[44].ID = Converter.GetString(dt1.Rows[0]["TDCodeNo18"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[45].ID = Converter.GetString(dt1.Rows[0]["TDCodeNo19"]);
                    //    gvDetailInfoFDR.HeaderRow.Cells[46].ID = Converter.GetString(dt1.Rows[0]["TDCodeNo20"]);



                    //    string cellid1 = gvDetailInfoFDR.HeaderRow.Cells[6].ID;
                    //    string cellid2 = gvDetailInfoFDR.HeaderRow.Cells[7].ID;
                    //    string cellid3 = gvDetailInfoFDR.HeaderRow.Cells[8].ID;
                    //    string cellid4 = gvDetailInfoFDR.HeaderRow.Cells[9].ID;
                    //    string cellid5 = gvDetailInfoFDR.HeaderRow.Cells[10].ID;
                    //    string cellid6 = gvDetailInfoFDR.HeaderRow.Cells[11].ID;
                    //    string cellid7 = gvDetailInfoFDR.HeaderRow.Cells[12].ID;
                    //    string cellid8 = gvDetailInfoFDR.HeaderRow.Cells[13].ID;
                    //    string cellid9 = gvDetailInfoFDR.HeaderRow.Cells[14].ID;
                    //    string cellid10 = gvDetailInfoFDR.HeaderRow.Cells[15].ID;

                    //    string cellid11 = gvDetailInfoFDR.HeaderRow.Cells[16].ID;
                    //    string cellid12 = gvDetailInfoFDR.HeaderRow.Cells[17].ID;
                    //    string cellid13 = gvDetailInfoFDR.HeaderRow.Cells[18].ID;
                    //    string cellid14 = gvDetailInfoFDR.HeaderRow.Cells[19].ID;
                    //    string cellid15 = gvDetailInfoFDR.HeaderRow.Cells[20].ID;
                    //    string cellid16 = gvDetailInfoFDR.HeaderRow.Cells[21].ID;
                    //    string cellid17 = gvDetailInfoFDR.HeaderRow.Cells[22].ID;
                    //    string cellid18 = gvDetailInfoFDR.HeaderRow.Cells[23].ID;
                    //    string cellid19 = gvDetailInfoFDR.HeaderRow.Cells[24].ID;
                    //    string cellid20 = gvDetailInfoFDR.HeaderRow.Cells[25].ID;


                    //    string cellid21 = gvDetailInfoFDR.HeaderRow.Cells[27].ID;
                    //    string cellid22 = gvDetailInfoFDR.HeaderRow.Cells[28].ID;
                    //    string cellid23 = gvDetailInfoFDR.HeaderRow.Cells[29].ID;
                    //    string cellid24 = gvDetailInfoFDR.HeaderRow.Cells[30].ID;
                    //    string cellid25 = gvDetailInfoFDR.HeaderRow.Cells[31].ID;
                    //    string cellid26 = gvDetailInfoFDR.HeaderRow.Cells[32].ID;
                    //    string cellid27 = gvDetailInfoFDR.HeaderRow.Cells[33].ID;
                    //    string cellid28 = gvDetailInfoFDR.HeaderRow.Cells[34].ID;
                    //    string cellid29 = gvDetailInfoFDR.HeaderRow.Cells[35].ID;
                    //    string cellid30 = gvDetailInfoFDR.HeaderRow.Cells[36].ID;

                    //    string cellid31 = gvDetailInfoFDR.HeaderRow.Cells[37].ID;
                    //    string cellid32 = gvDetailInfoFDR.HeaderRow.Cells[38].ID;
                    //    string cellid33 = gvDetailInfoFDR.HeaderRow.Cells[39].ID;
                    //    string cellid34 = gvDetailInfoFDR.HeaderRow.Cells[40].ID;
                    //    string cellid35 = gvDetailInfoFDR.HeaderRow.Cells[41].ID;
                    //    string cellid36 = gvDetailInfoFDR.HeaderRow.Cells[42].ID;
                    //    string cellid37 = gvDetailInfoFDR.HeaderRow.Cells[43].ID;
                    //    string cellid38 = gvDetailInfoFDR.HeaderRow.Cells[44].ID;
                    //    string cellid39 = gvDetailInfoFDR.HeaderRow.Cells[45].ID;
                    //    string cellid40 = gvDetailInfoFDR.HeaderRow.Cells[46].ID;


                    //    string query1 = "SELECT Description FROM  dbo.A2ZALLOWANCE Where Code='" + cellid1 + "' AND Status !=2";
                    //    DataTable data1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query1, "A2ZHRMCUS");
                    //    if (data1.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data1.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[6].Text = celldesc;
                    //        //lblDesc1.Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[6].Visible = false;
                    //    }
                    //    string query2 = "SELECT Description FROM  dbo.A2ZALLOWANCE Where Code='" + cellid2 + "' AND Status !=2";
                    //    DataTable data2 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query2, "A2ZHRMCUS");
                    //    if (data2.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data2.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[7].Text = celldesc;
                    //        // lblDesc2.Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[7].Visible = false;
                    //    }
                    //    string query3 = "SELECT Description FROM  dbo.A2ZALLOWANCE Where Code='" + cellid3 + "' AND Status !=2";
                    //    DataTable data3 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query3, "A2ZHRMCUS");
                    //    if (data3.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data3.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[8].Text = celldesc;
                    //        //lblDesc3.Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[8].Visible = false;
                    //    }
                    //    string query4 = "SELECT Description FROM  dbo.A2ZALLOWANCE Where Code='" + cellid4 + "' AND Status !=2";
                    //    DataTable data4 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query4, "A2ZHRMCUS");
                    //    if (data4.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data4.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[9].Text = celldesc;
                    //        //lblDesc4.Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[9].Visible = false;
                    //    }
                    //    string query5 = "SELECT Description FROM  dbo.A2ZALLOWANCE Where Code='" + cellid5 + "' AND Status !=2";
                    //    DataTable data5 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query5, "A2ZHRMCUS");
                    //    if (data5.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data5.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[10].Text = celldesc;
                    //        //lblDesc5.Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[10].Visible = false;
                    //    }
                    //    string query6 = "SELECT Description FROM  dbo.A2ZALLOWANCE Where Code='" + cellid6 + "' AND Status !=2";
                    //    DataTable data6 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query6, "A2ZHRMCUS");
                    //    if (data6.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data6.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[11].Text = celldesc;
                    //        //lblDesc6.Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[11].Visible = false;
                    //    }
                    //    string query7 = "SELECT Description FROM  dbo.A2ZALLOWANCE Where Code='" + cellid7 + "' AND Status !=2";
                    //    DataTable data7 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query7, "A2ZHRMCUS");
                    //    if (data7.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data7.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[12].Text = celldesc;
                    //        // lblDesc7.Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[12].Visible = false;
                    //    }
                    //    string query8 = "SELECT Description FROM  dbo.A2ZALLOWANCE Where Code='" + cellid8 + "' AND Status !=2";
                    //    DataTable data8 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query8, "A2ZHRMCUS");
                    //    if (data8.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data8.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[13].Text = celldesc;
                    //        // lblDesc8.Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[13].Visible = false;
                    //    }
                    //    string query9 = "SELECT Description FROM  dbo.A2ZALLOWANCE Where Code='" + cellid9 + "' AND Status !=2";
                    //    DataTable data9 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query9, "A2ZHRMCUS");
                    //    if (data9.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data9.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[14].Text = celldesc;
                    //        // lblDesc9.Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[14].Visible = false;
                    //    }
                    //    string query10 = "SELECT Description FROM  dbo.A2ZALLOWANCE Where Code='" + cellid10 + "' AND Status !=2";
                    //    DataTable data10 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query10, "A2ZHRMCUS");
                    //    if (data10.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data10.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[15].Text = celldesc;
                    //        //lblDesc10.Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[15].Visible = false;
                    //    }

                    //    string qry11 = "SELECT Description FROM  dbo.A2ZALLOWANCE Where Code='" + cellid11 + "' AND Status !=2";
                    //    DataTable data11 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry11, "A2ZHRMCUS");
                    //    if (data11.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data11.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[16].Text = celldesc;
                    //        //lblDesc1.Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[16].Visible = false;
                    //    }
                    //    string query12 = "SELECT Description FROM  dbo.A2ZALLOWANCE Where Code='" + cellid12 + "' AND Status !=2";
                    //    DataTable data12 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query12, "A2ZHRMCUS");
                    //    if (data12.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data12.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[17].Text = celldesc;
                    //        // lblDesc2.Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[17].Visible = false;
                    //    }
                    //    string query13 = "SELECT Description FROM  dbo.A2ZALLOWANCE Where Code='" + cellid13 + "' AND Status !=2";
                    //    DataTable data13 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query13, "A2ZHRMCUS");
                    //    if (data13.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data13.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[18].Text = celldesc;
                    //        //lblDesc3.Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[18].Visible = false;
                    //    }
                    //    string query14 = "SELECT Description FROM  dbo.A2ZALLOWANCE Where Code='" + cellid14 + "' AND Status !=2";
                    //    DataTable data14 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query14, "A2ZHRMCUS");
                    //    if (data14.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data14.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[19].Text = celldesc;
                    //        //lblDesc4.Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[19].Visible = false;
                    //    }
                    //    string query15 = "SELECT Description FROM  dbo.A2ZALLOWANCE Where Code='" + cellid15 + "' AND Status !=2";
                    //    DataTable data15 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query15, "A2ZHRMCUS");
                    //    if (data15.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data15.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[20].Text = celldesc;
                    //        //lblDesc5.Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[20].Visible = false;
                    //    }
                    //    string query16 = "SELECT Description FROM  dbo.A2ZALLOWANCE Where Code='" + cellid16 + "' AND Status !=2";
                    //    DataTable data16 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query16, "A2ZHRMCUS");
                    //    if (data16.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data16.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[21].Text = celldesc;
                    //        //lblDesc6.Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[21].Visible = false;
                    //    }
                    //    string query17 = "SELECT Description FROM  dbo.A2ZALLOWANCE Where Code='" + cellid17 + "' AND Status !=2";
                    //    DataTable data17 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query17, "A2ZHRMCUS");
                    //    if (data17.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data17.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[22].Text = celldesc;
                    //        // lblDesc7.Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[22].Visible = false;
                    //    }
                    //    string query18 = "SELECT Description FROM  dbo.A2ZALLOWANCE Where Code='" + cellid18 + "' AND Status !=2";
                    //    DataTable data18 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query18, "A2ZHRMCUS");
                    //    if (data18.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data18.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[23].Text = celldesc;
                    //        // lblDesc8.Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[23].Visible = false;
                    //    }
                    //    string query19 = "SELECT Description FROM  dbo.A2ZALLOWANCE Where Code='" + cellid19 + "' AND Status !=2";
                    //    DataTable data19 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query19, "A2ZHRMCUS");
                    //    if (data19.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data19.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[24].Text = celldesc;
                    //        // lblDesc9.Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[24].Visible = false;
                    //    }
                    //    string query20 = "SELECT Description FROM  dbo.A2ZALLOWANCE Where Code='" + cellid20 + "' AND Status !=2";
                    //    DataTable data20 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query20, "A2ZHRMCUS");
                    //    if (data20.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data20.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[25].Text = celldesc;
                    //        //lblDesc10.Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[25].Visible = false;
                    //    }

                    //    //--------------------------------------------------------------------------------------------------

                    //    string qry21 = "SELECT Description FROM  dbo.A2ZDEDUCTION Where Code='" + cellid21 + "' AND Status !=2";
                    //    DataTable data21 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry21, "A2ZHRMCUS");
                    //    if (data21.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data21.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[27].Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[27].Visible = false;
                    //    }
                    //    string query22 = "SELECT Description FROM  dbo.A2ZDEDUCTION Where Code='" + cellid22 + "' AND Status !=2";
                    //    DataTable data22 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query22, "A2ZHRMCUS");
                    //    if (data22.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data22.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[28].Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[28].Visible = false;
                    //    }
                    //    string query23 = "SELECT Description FROM  dbo.A2ZDEDUCTION Where Code='" + cellid23 + "' AND Status !=2";
                    //    DataTable data23 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query23, "A2ZHRMCUS");
                    //    if (data23.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data23.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[29].Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[29].Visible = false;
                    //    }
                    //    string query24 = "SELECT Description FROM  dbo.A2ZDEDUCTION Where Code='" + cellid24 + "' AND Status !=2";
                    //    DataTable data24 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query24, "A2ZHRMCUS");
                    //    if (data24.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data24.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[30].Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[30].Visible = false;
                    //    }
                    //    string query25 = "SELECT Description FROM  dbo.A2ZDEDUCTION Where Code='" + cellid25 + "' AND Status !=2";
                    //    DataTable data25 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query25, "A2ZHRMCUS");
                    //    if (data25.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data25.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[31].Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[31].Visible = false;
                    //    }
                    //    string query26 = "SELECT Description FROM  dbo.A2ZDEDUCTION Where Code='" + cellid26 + "' AND Status !=2";
                    //    DataTable data26 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query26, "A2ZHRMCUS");
                    //    if (data26.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data26.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[32].Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[32].Visible = false;
                    //    }
                    //    string query27 = "SELECT Description FROM  dbo.A2ZDEDUCTION Where Code='" + cellid27 + "' AND Status !=2";
                    //    DataTable data27 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query27, "A2ZHRMCUS");
                    //    if (data27.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data27.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[33].Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[33].Visible = false;
                    //    }
                    //    string query28 = "SELECT Description FROM  dbo.A2ZDEDUCTION Where Code='" + cellid28 + "' AND Status !=2";
                    //    DataTable data28 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query28, "A2ZHRMCUS");
                    //    if (data28.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data28.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[34].Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[34].Visible = false;
                    //    }
                    //    string query29 = "SELECT Description FROM  dbo.A2ZDEDUCTION Where Code='" + cellid29 + "' AND Status !=2";
                    //    DataTable data29 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query29, "A2ZHRMCUS");
                    //    if (data29.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data29.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[35].Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[35].Visible = false;
                    //    }
                    //    string query30 = "SELECT Description FROM  dbo.A2ZDEDUCTION Where Code='" + cellid30 + "' AND Status !=2";
                    //    DataTable data30 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query30, "A2ZHRMCUS");
                    //    if (data30.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data30.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[36].Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[36].Visible = false;
                    //    }

                    //    string qry31 = "SELECT Description FROM  dbo.A2ZDEDUCTION Where Code='" + cellid31 + "' AND Status !=2";
                    //    DataTable data31 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry31, "A2ZHRMCUS");
                    //    if (data31.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data31.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[37].Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[37].Visible = false;
                    //    }
                    //    string query32 = "SELECT Description FROM  dbo.A2ZDEDUCTION Where Code='" + cellid32 + "' AND Status !=2";
                    //    DataTable data32 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query32, "A2ZHRMCUS");
                    //    if (data32.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data32.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[38].Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[38].Visible = false;
                    //    }
                    //    string query33 = "SELECT Description FROM  dbo.A2ZDEDUCTION Where Code='" + cellid33 + "' AND Status !=2";
                    //    DataTable data33 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query33, "A2ZHRMCUS");
                    //    if (data33.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data33.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[39].Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[39].Visible = false;
                    //    }
                    //    string query34 = "SELECT Description FROM  dbo.A2ZDEDUCTION Where Code='" + cellid34 + "' AND Status !=2";
                    //    DataTable data34 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query34, "A2ZHRMCUS");
                    //    if (data34.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data34.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[40].Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[40].Visible = false;
                    //    }
                    //    string query35 = "SELECT Description FROM  dbo.A2ZDEDUCTION Where Code='" + cellid35 + "' AND Status !=2";
                    //    DataTable data35 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query35, "A2ZHRMCUS");
                    //    if (data35.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data35.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[41].Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[41].Visible = false;
                    //    }
                    //    string query36 = "SELECT Description FROM  dbo.A2ZDEDUCTION Where Code='" + cellid36 + "' AND Status !=2";
                    //    DataTable data36 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query36, "A2ZHRMCUS");
                    //    if (data36.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data36.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[42].Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[42].Visible = false;
                    //    }
                    //    string query37 = "SELECT Description FROM  dbo.A2ZDEDUCTION Where Code='" + cellid37 + "' AND Status !=2";
                    //    DataTable data37 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query37, "A2ZHRMCUS");
                    //    if (data37.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data37.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[43].Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[43].Visible = false;
                    //    }
                    //    string query38 = "SELECT Description FROM  dbo.A2ZDEDUCTION Where Code='" + cellid38 + "' AND Status !=2";
                    //    DataTable data38 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query38, "A2ZHRMCUS");
                    //    if (data38.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data38.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[44].Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[44].Visible = false;
                    //    }
                    //    string query39 = "SELECT Description FROM  dbo.A2ZDEDUCTION Where Code='" + cellid39 + "' AND Status !=2";
                    //    DataTable data39 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query39, "A2ZHRMCUS");
                    //    if (data39.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data39.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[45].Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[45].Visible = false;
                    //    }
                    //    string query40 = "SELECT Description FROM  dbo.A2ZDEDUCTION Where Code='" + cellid40 + "' AND Status !=2";
                    //    DataTable data40 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(query40, "A2ZHRMCUS");
                    //    if (data40.Rows.Count > 0)
                    //    {
                    //        string celldesc = Converter.GetString(data40.Rows[0]["Description"]);
                    //        gvDetailInfoFDR.HeaderRow.Cells[46].Text = celldesc;
                    //    }
                    //    else
                    //    {
                    //        gvDetailInfoFDR.Columns[46].Visible = false;
                    //    }


                    //}

                }


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('BtnProcess_Click Problem');</script>");
                //throw ex;
            }

        }


        protected void UpdateSalPostStat()
        {
            try
            {
                Int16 BStat = 1;

                int roweffect = A2ZERPSYSPRMDTO.UpdateSalPostStat(BStat);
                if (roweffect > 0)
                {

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateSalPostStat Problem');</script>");
                //throw ex;
            }

        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }


    }
}