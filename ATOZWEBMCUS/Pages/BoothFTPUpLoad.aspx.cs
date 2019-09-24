using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{
    public partial class BoothFTPUpLoad : System.Web.UI.Page
    {

        string ftpServerIP;
        string ftpUserID;
        string ftpPassword;
        FtpWebRequest reqFTP;
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ftpServerIP = "27.147.146.2:21";
                txtserverip.Text = ftpServerIP;
                GetListFile();
                txtProcessDate.Focus();
                BtnUpload.Visible = false;
                //divmsg.Visible = false;
                Image1.Visible = false;
               
            }
        }

      

     

        private void GetListFile( )
        {
           
            string[] filePaths = Directory.GetFiles(@"E:\BOOTH");
            List<ListItem> files = new List<ListItem>();
            foreach (string filePath in filePaths)
            {
                listfile.Items.Add(new ListItem(Path.GetFileName(filePath), filePath));
               
            }
          
           
        }

        
        public string CalculateFileSize(double FileInBytes)
        {
            string strSize = "00";
            if (FileInBytes < 1024)
                strSize = FileInBytes + " B";//Byte
            else if (FileInBytes > 1024 & FileInBytes < 1048576)
                strSize = Math.Round((FileInBytes / 1024), 2) + " KB";//Kilobyte
            else if (FileInBytes > 1048576 & FileInBytes < 107341824)
                strSize = Math.Round((FileInBytes / 1024) / 1024, 2) + " MB";//Megabyte
            else if (FileInBytes > 107341824 & FileInBytes < 1099511627776)
                strSize = Math.Round(((FileInBytes / 1024) / 1024) / 1024, 2) + " GB";//Gigabyte
            else
                strSize = Math.Round((((FileInBytes / 1024) / 1024) / 1024) / 1024, 2) + " TB";//Terabyte
            return strSize;
        }
        private void Upload(string filename)
        {
           
            try
            {

                FileInfo fileInf = new FileInfo(filename);
                string uri = "ftp://" + txtserverip.Text + "/" + fileInf.Name;


                // txtUpload.Text =fileInf.Name;

                // Create FtpWebRequest object from the Uri provided
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + txtserverip.Text + "/" + fileInf.Name));

                // Provide the WebPermission Credintials
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                // By default KeepAlive is true, where the control connection is not closed
                // after a command is executed.
                reqFTP.KeepAlive = false;

                // Specify the command to be executed.
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

                // Specify the data transfer type.
                reqFTP.UseBinary = true;

                // Notify the server about the size of the uploaded file
                reqFTP.ContentLength = fileInf.Length;

                // The buffer size is set to 2kb
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;

                // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
                FileStream fs = fileInf.OpenRead();

                try
                {
                    // Stream to which the file to be upload is written
                    Stream strm = reqFTP.GetRequestStream();

                    // Read from the file stream 2kb at a time
                    contentLen = fs.Read(buff, 0, buffLength);

                    // Till Stream content ends
                    while (contentLen != 0)
                    {
                        // Write Content from the file stream to the FTP Upload Stream
                        strm.Write(buff, 0, contentLen);
                        contentLen = fs.Read(buff, 0, buffLength);
                    }

                    // Close the file stream and the Request Stream
                    strm.Close();
                    fs.Close();
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "click", "alert('Upload error!');");
                }
            }
            catch(Exception x)
            {

            }
        }


        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < chklist.Items.Count; i++)
                {
                    if (chklist.Items[i].Selected)
                    {
                        string name = @"E:\BOOTH\" + chklist.Items[i].ToString();
                       
                        Upload(name);
                        chklist.Items[i].Selected = false;
                        //divmsg.Visible = true;
                        Image1.Visible = true;
                    }

                }

                //Label1.Text = "Booth Data Uploaded---100% ";
                //Image1.Visible = false;
                Sucessfull();

            }
            catch(Exception ex)
            {

            }
        }

        protected void Sucessfull()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Booth Data Uploaded Successfully done');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }
            

            return;

        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            if(txtProcessDate.Text==string.Empty)
            {
                String csname1 = "PopupScript";
                Type cstype = GetType();
                ClientScriptManager cs = Page.ClientScript;

                if (!cs.IsStartupScriptRegistered(cstype, csname1))
                {
                    String cstext1 = "alert('Please insert date!');";
                    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                }
                txtProcessDate.Focus();
                return;
            }
            string monthnum = "";
            string filename = "";
            string cashcode = "";
            string Month = "";
            string year = "";
            string day = "";
            string FullDate = "";
            for (int i = 0; i < listfile.Items.Count; i++)
            {
                filename = listfile.Items[i].Text;
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
              
                FullDate = day + "/" + monthnum + "/" + year;
                string procdate = Converter.GetString(txtProcessDate.Text);
                if(FullDate==procdate)
                {
                    string find = "B" + cashcode + Month + year + day+".DAT";
                    chklist.Items.Add(find);

                    BtnUpload.Visible = true;
                 
                  
                }
            }
        }

    

        
    }
}