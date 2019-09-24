using System;
using System.Configuration;
using System.Drawing;
using System.Management;
using System.Web.UI.WebControls;
using ATOZWEBMCUS.WebSessionStore;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DataAccessLayer.Utility;

namespace ATOZWEBMCUS.Pages
{
    public partial class ReportServer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!IsPostBack)
                {
                    if (Request.UrlReferrer != null)
                        AspxPreviousPage = Request.UrlReferrer.ToString();

                    PrinterList();

                    //ddlSelectPrinter.DataSource = PrinterSettings.InstalledPrinters;
                    //ddlSelectPrinter.DataBind();

                }
                GenerateReport();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            //this.CrystalReportViewer1.RefreshReport();
            //this.cashToVendorPayableCrystalReportObj.Close();
            //this.cashToVendorPayableCrystalReportObj.Dispose();
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            //this.CrystalReportViewer1.RefreshReport();
            Response.Redirect(AspxPreviousPage, false);
        }

        string aspxPreviousPageKey = "AspxPreviousPage";
        private String AspxPreviousPage
        {
            get
            {
                //session should different key for different users
                //That is why I add SessionID in key, otherwise data become same for all users
                if (Page.Cache[aspxPreviousPageKey + Page.Session.SessionID] != null)
                {
                    return Page.Cache[aspxPreviousPageKey + Page.Session.SessionID] as String;
                }
                return null;
            }
            set
            {
                if (Page.Cache[aspxPreviousPageKey + Page.Session.SessionID] != null)
                {
                    Page.Cache.Remove(aspxPreviousPageKey + Page.Session.SessionID);
                }
                Page.Cache[aspxPreviousPageKey + Page.Session.SessionID] = value;
            }
        }



        protected void GenerateReport()
        {
            try
            {
                ReportDocument theReport = new ReportDocument();
                string reportFile = Converter.GetString(SessionStore.GetValue(Params.REPORT_FILE_NAME_KEY));
                if ((reportFile != string.Empty) & (reportFile != null))
                {
                    TableLogOnInfo myLog;
                    CrystalDecisions.CrystalReports.Engine.Table myTable;
                    reportFile = reportFile + ".rpt";
                    string reportPath = this.MapPath(".") + @"\Reports\" + reportFile;
                    theReport.Load(reportPath);
                    ConnectionInfo conn = new ConnectionInfo();
                    conn.ServerName = ConfigurationManager.AppSettings["DBServer"].Trim();
                    conn.DatabaseName = Converter.GetString(SessionStore.GetValue(Params.REPORT_DATABASE_NAME_KEY));//ConfigurationManager.AppSettings["InitialDB"].Trim();
                    //conn.UserID = ConfigurationManager.AppSettings["DBUserName"].Trim();
                    //conn.Password = ConfigurationManager.AppSettings["DBPassword"].Trim();
                    conn.IntegratedSecurity = true;
                    int tableCount = theReport.Database.Tables.Count - 1;
                    for (int i = 0; i <= tableCount; i++)
                    {
                        myTable = theReport.Database.Tables[i];
                        myLog = myTable.LogOnInfo;
                        myLog.ConnectionInfo = conn;
                        myTable.ApplyLogOnInfo(myLog);
                        myTable.Location = myLog.TableName;

                    }
                    int reportobjectsCount = theReport.ReportDefinition.ReportObjects.Count - 2;
                    for (int i = 0; i <= reportobjectsCount; i++)
                    {
                        ReportObject rpt = theReport.ReportDefinition.ReportObjects[i];
                        if (rpt.Kind == ReportObjectKind.SubreportObject)
                        {
                            SubreportObject subrpt = (SubreportObject)rpt;
                            ReportDocument r = theReport.OpenSubreport(subrpt.SubreportName);
                            int subreportTableCount = r.Database.Tables.Count - 2;
                            for (int j = 0; j <= subreportTableCount; j++)
                            {
                                myTable = r.Database.Tables[j];
                                myLog = myTable.LogOnInfo;
                                myTable.ApplyLogOnInfo(myLog);
                                myTable.Location = myLog.TableName;
                            }
                        }
                    }
                    ParameterValues pList = new ParameterValues();
                    ParameterDiscreteValue pV = new ParameterDiscreteValue();
                    int parameterFieldsCount = theReport.DataDefinition.ParameterFields.Count - 1;
                    for (int i = 0; i <= parameterFieldsCount; i++)
                    {
                        string paramName = theReport.DataDefinition.ParameterFields[i].Name;
                        //Report Parameters Fields 1
                        if (paramName == "@BRNO")
                        {
                            pV.Value = SessionStore.GetValue(Params.BRNO_ID);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 2
                        else if (paramName == "@CONO")
                        {
                            pV.Value = SessionStore.GetValue(Params.CONO_ID);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 3
                        else if (paramName == "@CompanyName")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMPANY_NAME);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        //Report Parameters Fields 4
                        else if (paramName == "@BranchName")
                        {
                            pV.Value = SessionStore.GetValue(Params.BRANCH_NAME);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 5
                        else if (paramName == "@BranchAddress")
                        {
                            pV.Value = SessionStore.GetValue(Params.BRANCH_ADDRESS);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 6
                        else if (paramName == "@CommonName1")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME1);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 7
                        else if (paramName == "@CommonName2")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME2);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                          //Report Parameters Fields 8
                        else if (paramName == "@CommonName3")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME3);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        //Report Parameters Fields 9
                        else if (paramName == "@CommonName4")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME4);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        //Report Parameters Fields 10
                        else if (paramName == "@CommonName5")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NAME5);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 11
                        else if (paramName == "@CommonNo1")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO1);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 12
                        else if (paramName == "@CommonNo2")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO2);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 13
                        else if (paramName == "@CommonNo3")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO3);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 14
                        else if (paramName == "@CommonNo4")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO4);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 15
                        else if (paramName == "@CommonNo5")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_NO5);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 16
                        else if (paramName == "@fDate")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_FDATE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        // Report Parameters Fields 17
                        else if (paramName == "@tDate")
                        {
                            pV.Value = SessionStore.GetValue(Params.COMMON_TDATE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@ProdCode")
                        {
                            pV.Value = SessionStore.GetValue(Params.PROD_CODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        else if (paramName == "@BatchNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.BATCH_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        else if (paramName == "@VchNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.VOUCHER_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        else if (paramName == "@fBatchNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.FIRST_BATCH_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        else if (paramName == "@tBatchNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.TO_BATCH_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        else if (paramName == "@TrnasferType")
                        {
                            pV.Value = SessionStore.GetValue(Params.TRANSFER_TYPE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        else if (paramName == "@TrnSlNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.TRNSL_CODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        //Report Parameters Fields 18
                        else if (paramName == "@LedgerType")
                        {
                            pV.Value = SessionStore.GetValue(Params.LEDGER_TYPE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 19
                        else if (paramName == "@LedgerNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.LEDGER_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                          //Report Parameters Fields 20
                        else if (paramName == "@ReqNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.REQ_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 21
                        else if (paramName == "@ProdCode")
                        {
                            pV.Value = SessionStore.GetValue(Params.PROD_CODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                         //Report Parameters Fields 22
                        else if (paramName == "@BatchNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.BATCH_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 23
                        else if (paramName == "@GRNNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.GRN_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 24
                        else if (paramName == "@nItemType")
                        {
                            pV.Value = SessionStore.GetValue(Params.ITEM_TYPE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 25
                        else if (paramName == "@nItemSource")
                        {
                            pV.Value = SessionStore.GetValue(Params.ITEM_SOURCE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //......................................nimara.................................................
                        //Report Parameters Fields 26
                        else if (paramName == "@SuppCode")
                        {
                            pV.Value = SessionStore.GetValue(Params.SUPP_CODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //.......................................................................................
                        //Report Parameters Fields 27
                        else if (paramName == "@ManuCode")
                        {
                            pV.Value = SessionStore.GetValue(Params.MANU_CODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //............................................................................................
                        //Report Parameters Fields 28
                        else if (paramName == "@OrdNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.ORDER_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 29
                        else if (paramName == "@ItemCode")
                        {
                            pV.Value = SessionStore.GetValue(Params.ITEM_CODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 30
                        else if (paramName == "@fItemCode")
                        {
                            pV.Value = SessionStore.GetValue(Params.F_ITEM_CODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 31
                        else if (paramName == "@tItemCode")
                        {
                            pV.Value = SessionStore.GetValue(Params.T_ITEM_CODE);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 32
                        else if (paramName == "@ChalanNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.CHALLAN_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        //Report Parameters Fields 33
                        else if (paramName == "@EmpNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.EMP_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }
                        //Report Parameters Fields 34
                        else if (paramName == "@PPICNo")
                        {
                            pV.Value = SessionStore.GetValue(Params.PPIC_NO);
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                        //............................................................................................
                        else
                        {
                            //This else for SP parameters 34
                            paramName = theReport.DataDefinition.ParameterFields[i].Name;
                            string paramValue = Converter.GetString(SessionStore.GetValue(Params.WHERE_CLAUSE));
                            pV.Value = paramValue;
                            pList.Add((ParameterValue)pV);
                            theReport.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(pList);
                        }

                    }
                    this.CrystalReportViewer1.ReportSource = theReport;
                    this.CrystalReportViewer1.RefreshReport();
                }
                else
                {
                    //TODO
                }
            }
            catch (Exception ex)
            {
              
                lblMsg.Text = "System Error Ocoured";
                lblMsg.ForeColor = Color.Red;
                throw ex;


            }
        }

        private void PrinterList()
        {
            // USING WMI. (WINDOWS MANAGEMENT INSTRUMENTATION)

            ManagementScope objMS = new ManagementScope(ManagementPath.DefaultPath);
            objMS.Connect();

            SelectQuery objQuery = new SelectQuery("SELECT * FROM Win32_Printer");
            ManagementObjectSearcher objMOS = new ManagementObjectSearcher(objMS, objQuery);
            ManagementObjectCollection objMOC = objMOS.Get();

            ListItemCollection listBoxData = new ListItemCollection();

            foreach (ManagementObject Printers in objMOC)
            {
                listBoxData.Add(new ListItem(Printers["Name"].ToString()));

                //if (Convert.ToBoolean(Printers["Local"]))       // LOCAL PRINTERS.
                //{
                //    listBoxData.Add(new ListItem(Printers["Name"].ToString()));
                //}

                //if (Convert.ToBoolean(Printers["Network"]))     // ALL NETWORK PRINTERS.
                //{
                //    listBoxData.Add(new ListItem(Printers["Name"].ToString()));
                //}
            }


        }

    }
}
