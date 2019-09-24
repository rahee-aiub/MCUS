using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class UploadBoothDataControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
               
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                txtProcessDate.Text = date;
                txtProcessDate.ReadOnly = true;
                BtnMerge.Visible = false;
            }
        }

        //protected void BtnSearch_Click(object sender, EventArgs e)
        //{
        //    if (txtProcessDate.Text == string.Empty)
        //    {
        //        String csname1 = "PopupScript";
        //        Type cstype = GetType();
        //        ClientScriptManager cs = Page.ClientScript;

        //        if (!cs.IsStartupScriptRegistered(cstype, csname1))
        //        {
        //            String cstext1 = "alert('Please insert date!');";
        //            cs.RegisterStartupScript(cstype, csname1, cstext1, true);
        //        }
        //        txtProcessDate.Focus();
        //        return;
        //    }
        //    string strqry = null;
        //    int rowEffect = 0;
        //    string monthnum="";
        //    string filename = "";
        //    string cashcode = "";
        //    string Month = "";
        //    string year = "";
        //    string day = "";
        //    string FullDate = "";

        //    DateTime prodate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

        //    string delqry = "DELETE FROM  A2ZUBDCTRL  WHERE ProcessDate='" + prodate + "'";
        //    rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
        //    if (rowEffect > 0)
        //    {

        //    }


        //    //strqry = "TRUNCATE TABLE dbo.A2ZUBDCTRL";
        //    //rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZCSMCUS"));

        //    strqry = "INSERT INTO A2ZCSMCUS.dbo.A2ZUBDCTRL(CashCodeNo,CashCodeName) SELECT GLAccNo,GLAccDesc FROM A2ZGLMCUS.dbo.A2ZCGLMST WHERE GLRecType = 2 and GLSubHead = 10101000";
        //    rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZCSMCUS"));
        //    if (rowEffect > 0)
        //    {
        //        strqry = "UPDATE A2ZUBDCTRL SET ProcessDate='" + prodate + "', ProcessStatus=0, ProcessStatusDesc='Not Receive'";
        //        rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZCSMCUS"));
        //        BindFileNamesToList();

        //        for(int i=0;i<filelist.Items.Count;i++)
        //        {
        //            filename = filelist.Items[i].Text;
        //            cashcode = filename.Substring(1, 8);
        //            Month = filename.Substring(9, 3);
        //            #region MonthNumber
        //            if (Month=="JAN")
        //            {
        //                monthnum = "01";
        //            }
        //            if (Month == "FEB")
        //            {
        //                monthnum = "02";
        //            }
        //            if (Month == "MAR")
        //            {
        //                monthnum = "03";
        //            }
        //            if (Month == "APR")
        //            {
        //                monthnum = "04";
        //            }
        //            if (Month == "MAY")
        //            {
        //                monthnum = "05";
        //            }
        //            if (Month == "JUN")
        //            {
        //                monthnum = "06";
        //            }
        //            if (Month == "JUL")
        //            {
        //                monthnum = "07";
        //            }
        //            if (Month == "AUG")
        //            {
        //                monthnum = "08";
        //            }
        //            if (Month == "SEP")
        //            {
        //                monthnum = "09";
        //            }
        //            if (Month == "OCT")
        //            {
        //                monthnum = "10";
        //            }
        //            if (Month == "NOV")
        //            {
        //                monthnum = "11";
        //            }
        //            if (Month == "DEC")
        //            {
        //                monthnum = "12";
        //            }
        //            #endregion
        //            year = filename.Substring(12, 4);
        //            day = filename.Substring(16, 2);
        //            FullDate = monthnum + "/" + day + "/" + year;

        //            DateTime filedate = Converter.GetDateTime(FullDate);

        //            strqry = "SELECT CashCodeNo,ProcessDate,ProcessStatus FROM A2ZUBDCTRL WHERE ProcessDate='" + filedate + "' AND CashCodeNo='" + cashcode + "' AND ProcessStatus !=2";
        //            DataTable dt = CommonManager.Instance.GetDataTableByQuery(strqry, "A2ZCSMCUS");
        //            if(dt.Rows.Count>0)
        //            {
        //                foreach (DataRow dr in dt.Rows)
        //                {
        //                    var CaCode = dr["CashCodeNo"].ToString();
        //                    var PDate = dr["ProcessDate"].ToString();
        //                    DateTime processdate = Converter.GetDateTime(PDate);
        //                    strqry = "UPDATE A2ZUBDCTRL SET  ProcessStatus=1, ProcessStatusDesc='Receive' WHERE ProcessDate='" + filedate + "' AND CashCodeNo='" + CaCode + "'";
        //                    rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZCSMCUS"));
        //                }
        //            }
                   
        //        }

               
        //   }
        //    gvDetail();
        //    ColorStatus();
        //    BtnProcess.Visible = false;
        //    BtnMerge.Visible = true;

        //}


        protected void ColorStatus()
        {
             for (int i = 0; i < gvUPControlInfo.Rows.Count; ++i)
             {
                 Label lblstatus = (Label)gvUPControlInfo.Rows[i].Cells[3].FindControl("lblStatus");

                int status = Converter.GetInteger(lblstatus.Text);
                if(status==0)
                 {
                    gvUPControlInfo.Rows[i].Cells[4].ForeColor=System.Drawing.Color.Red;
                 }
                if (status == 1)
                {
                    gvUPControlInfo.Rows[i].Cells[4].ForeColor = System.Drawing.Color.Blue;
                }
                if (status == 2)
                {
                    gvUPControlInfo.Rows[i].Cells[4].ForeColor = System.Drawing.Color.Green;
                }
             }
        }

        protected void gvDetail()
        {
            DateTime prodate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string sqlquery3 = "SELECT ProcessDate,CashCodeNo,CashCodeName,ProcessStatus,ProcessStatusDesc FROM A2ZUBDCTRL WHERE ProcessStatus=1";
            gvUPControlInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvUPControlInfo, "A2ZCSMCUS");
        }


        private void BindFileNamesToList()
         {
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZERPSYSPRM ", "A2ZHKMCUS");
            if(dt.Rows.Count>0)
            {
                string filepath = Converter.GetString(dt.Rows[0]["PrmOutDataPath"]);

                string extension = ".DAT";
                FileInfo[] fileInfo = GetFilesFromFolder(filepath, (extension == ".DAT") ? "DAT" : extension);
                //Clear the listbox items before loading.
                filelist.Items.Clear();
                //Append the filename as a value and text of a listbox
                foreach (FileInfo fileInfoTemp in fileInfo)
                {

                    ListItem listItem = new ListItem(fileInfoTemp.Name, fileInfoTemp.Name);

                    filelist.Items.Add(listItem);

                }
            }

       

    }
            
       FileInfo[] GetFilesFromFolder(string folderName, string extension)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(folderName);
        string internalExtension = string.Concat("*.", extension);
        FileInfo[] fileInfo = directoryInfo.GetFiles(internalExtension, SearchOption.AllDirectories);
        return fileInfo;

    }

       protected void BtnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("A2ZERPModule.aspx");
    }

       //protected void BtnProcess_Click(object sender, EventArgs e)
       //{
       //    DateTime prodate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
           
       //    string strqry = null;
       //    int rowEffect = 0;
       //    string monthnum = "";
       //    string filename = "";
       //    string cashcode = "";
       //    string Month = "";
       //    string year = "";
       //    string day = "";
       //    string FullDate = "";
       //    BindFileNamesToList();

       //    for (int i = 0; i < filelist.Items.Count; i++)
       //    {
       //        filename = filelist.Items[i].Text;
       //        cashcode = filename.Substring(1, 8);
       //        Month = filename.Substring(9, 3);
       //        #region MonthNumber
       //        if (Month == "JAN")
       //        {
       //            monthnum = "01";
       //        }
       //        if (Month == "FEB")
       //        {
       //            monthnum = "02";
       //        }
       //        if (Month == "MAR")
       //        {
       //            monthnum = "03";
       //        }
       //        if (Month == "APR")
       //        {
       //            monthnum = "04";
       //        }
       //        if (Month == "MAY")
       //        {
       //            monthnum = "05";
       //        }
       //        if (Month == "JUN")
       //        {
       //            monthnum = "06";
       //        }
       //        if (Month == "JUL")
       //        {
       //            monthnum = "07";
       //        }
       //        if (Month == "AUG")
       //        {
       //            monthnum = "08";
       //        }
       //        if (Month == "SEP")
       //        {
       //            monthnum = "09";
       //        }
       //        if (Month == "OCT")
       //        {
       //            monthnum = "10";
       //        }
       //        if (Month == "NOV")
       //        {
       //            monthnum = "11";
       //        }
       //        if (Month == "DEC")
       //        {
       //            monthnum = "12";
       //        }
       //        #endregion
       //        year = filename.Substring(12, 4);
       //        day = filename.Substring(16, 2);
       //        FullDate = monthnum + "/" + day + "/" + year;

       //        DateTime filedate = Converter.GetDateTime(FullDate);

       //       string strqry1 = "SELECT CashCodeNo,ProcessDate,ProcessStatus FROM A2ZUBDCTRL WHERE ProcessDate='" + filedate + "' AND CashCodeNo='" + cashcode + "' AND ProcessStatus=1";
       //       DataTable dt = CommonManager.Instance.GetDataTableByQuery(strqry1, "A2ZCSMCUS");
       //        if (dt.Rows.Count > 0)
       //        {
       //            foreach (DataRow dr in dt.Rows)
       //            {
       //                var CaCode = dr["CashCodeNo"].ToString();
       //                var PDate = dr["ProcessDate"].ToString();
       //                var Pstatus = dr["ProcessStatus"].ToString();
       //                if(Pstatus=="1")
       //                {
       //                    var prm = new object[2];
       //                    prm[0] = filename;
       //                    prm[1] = prodate;
       //                    rowEffect = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_BoothDataMerge", prm, "A2ZCSMCUS"));

       //                    if(rowEffect==0)
       //                    {
       //                        DateTime processdate = Converter.GetDateTime(PDate);
       //                        strqry = "UPDATE A2ZUBDCTRL SET  ProcessStatus=2, ProcessStatusDesc='Merge' WHERE ProcessDate='" + processdate + "' AND CashCodeNo='" + CaCode + "'";
       //                        rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZCSMCUS"));
       //                    }
       //                }
                      
       //            }
       //        }

       //    }

       //    gvDetail();
       //    ColorStatus();
       //    Successful();
       //}

       private void Successful()
       {
           String csname1 = "PopupScript";
           Type cstype = GetType();
           ClientScriptManager cs = Page.ClientScript;

           if (!cs.IsStartupScriptRegistered(cstype, csname1))
           {
               String cstext1 = "alert(' Data Merging successfully Done.');";
               cs.RegisterStartupScript(cstype, csname1, cstext1, true);
           }
       }

       protected void BtnProcess_Click(object sender, EventArgs e)
       {
           string strqry = null;
           int rowEffect = 0;
           string monthnum = "";
           string filename = "";
           string cashcode = "";
           string Month = "";
           string year = "";
           string day = "";
           string FullDate = "";

           DateTime prodate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

           //string delqry = "DELETE FROM  A2ZUBDCTRL  WHERE ProcessDate='" + prodate + "'";
           //rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(delqry, "A2ZCSMCUS"));
           //if (rowEffect > 0)
           //{

           //}


           //strqry = "TRUNCATE TABLE dbo.A2ZUBDCTRL";
           //rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZCSMCUS"));

           strqry = "INSERT INTO A2ZCSMCUS.dbo.A2ZUBDCTRL(CashCodeNo,CashCodeName) SELECT GLAccNo,GLAccDesc FROM A2ZGLMCUS.dbo.A2ZCGLMST WHERE GLRecType = 2 and GLSubHead = 10101000";
           rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZCSMCUS"));
           if (rowEffect > 0)
           {
               strqry = "UPDATE A2ZUBDCTRL SET ProcessDate='" + prodate + "', ProcessStatus=0, ProcessStatusDesc='Not Receive'";
               rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZCSMCUS"));
               BindFileNamesToList();

               for (int i = 0; i < filelist.Items.Count; i++)
               {
                   filename = filelist.Items[i].Text;
                   cashcode = filename.Substring(1, 8);
                   Month = filename.Substring(9, 3);
                   #region MonthNumber
                   if (Month == "JAN")
                   {
                       monthnum = "01";
                   }
                   if (Month == "FEB")
                   {
                       monthnum = "02";
                   }
                   if (Month == "MAR")
                   {
                       monthnum = "03";
                   }
                   if (Month == "APR")
                   {
                       monthnum = "04";
                   }
                   if (Month == "MAY")
                   {
                       monthnum = "05";
                   }
                   if (Month == "JUN")
                   {
                       monthnum = "06";
                   }
                   if (Month == "JUL")
                   {
                       monthnum = "07";
                   }
                   if (Month == "AUG")
                   {
                       monthnum = "08";
                   }
                   if (Month == "SEP")
                   {
                       monthnum = "09";
                   }
                   if (Month == "OCT")
                   {
                       monthnum = "10";
                   }
                   if (Month == "NOV")
                   {
                       monthnum = "11";
                   }
                   if (Month == "DEC")
                   {
                       monthnum = "12";
                   }
                   #endregion
                   year = filename.Substring(12, 4);
                   day = filename.Substring(16, 2);
                   FullDate = monthnum + "/" + day + "/" + year;

                   DateTime filedate = Converter.GetDateTime(FullDate);

                   strqry = "SELECT CashCodeNo,ProcessDate,ProcessStatus FROM A2ZUBDCTRL WHERE ProcessDate='" + filedate + "' AND CashCodeNo='" + cashcode + "' AND ProcessStatus !=2";
                   DataTable dt = CommonManager.Instance.GetDataTableByQuery(strqry, "A2ZCSMCUS");
                   if (dt.Rows.Count > 0)
                   {
                       foreach (DataRow dr in dt.Rows)
                       {
                           var CaCode = dr["CashCodeNo"].ToString();
                           var PDate = dr["ProcessDate"].ToString();
                           DateTime processdate = Converter.GetDateTime(PDate);
                           strqry = "UPDATE A2ZUBDCTRL SET  ProcessStatus=1, ProcessStatusDesc='Receive' WHERE ProcessDate='" + filedate + "' AND CashCodeNo='" + CaCode + "'";
                           rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZCSMCUS"));
                       }
                   }

               }


           }
           gvDetail();
           ColorStatus();
           BtnProcess.Visible = false;
           BtnMerge.Visible = true;
       }

       protected void BtnMerge_Click(object sender, EventArgs e)
       {
           DateTime prodate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

           string strqry = null;
           int rowEffect = 0;
           string monthnum = "";
           string filename = "";
           string cashcode = "";
           string Month = "";
           string year = "";
           string day = "";
           string FullDate = "";
           BindFileNamesToList();

           for (int i = 0; i < filelist.Items.Count; i++)
           {
               filename = filelist.Items[i].Text;
               cashcode = filename.Substring(1, 8);
               Month = filename.Substring(9, 3);
               #region MonthNumber
               if (Month == "JAN")
               {
                   monthnum = "01";
               }
               if (Month == "FEB")
               {
                   monthnum = "02";
               }
               if (Month == "MAR")
               {
                   monthnum = "03";
               }
               if (Month == "APR")
               {
                   monthnum = "04";
               }
               if (Month == "MAY")
               {
                   monthnum = "05";
               }
               if (Month == "JUN")
               {
                   monthnum = "06";
               }
               if (Month == "JUL")
               {
                   monthnum = "07";
               }
               if (Month == "AUG")
               {
                   monthnum = "08";
               }
               if (Month == "SEP")
               {
                   monthnum = "09";
               }
               if (Month == "OCT")
               {
                   monthnum = "10";
               }
               if (Month == "NOV")
               {
                   monthnum = "11";
               }
               if (Month == "DEC")
               {
                   monthnum = "12";
               }
               #endregion
               year = filename.Substring(12, 4);
               day = filename.Substring(16, 2);
               FullDate = monthnum + "/" + day + "/" + year;

               DateTime filedate = Converter.GetDateTime(FullDate);

               string strqry1 = "SELECT CashCodeNo,ProcessDate,ProcessStatus FROM A2ZUBDCTRL WHERE ProcessDate='" + filedate + "' AND CashCodeNo='" + cashcode + "' AND ProcessStatus=1";
               DataTable dt = CommonManager.Instance.GetDataTableByQuery(strqry1, "A2ZCSMCUS");
               if (dt.Rows.Count > 0)
               {
                   foreach (DataRow dr in dt.Rows)
                   {
                       var CaCode = dr["CashCodeNo"].ToString();
                       var PDate = dr["ProcessDate"].ToString();
                       var Pstatus = dr["ProcessStatus"].ToString();
                       if (Pstatus == "1")
                       {
                           var prm = new object[2];
                           prm[0] = filename;
                           prm[1] = prodate;
                           rowEffect = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_BoothDataMerge", prm, "A2ZCSMCUS"));

                           if (rowEffect == 0)
                           {
                               DateTime processdate = Converter.GetDateTime(PDate);
                               strqry = "UPDATE A2ZUBDCTRL SET  ProcessStatus=2, ProcessStatusDesc='Merge' WHERE ProcessDate='" + processdate + "' AND CashCodeNo='" + CaCode + "'";
                               rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strqry, "A2ZCSMCUS"));
                           }
                       }

                   }
               }

           }

           gvDetail();
           ColorStatus();
           Successful();
       }

    }
}