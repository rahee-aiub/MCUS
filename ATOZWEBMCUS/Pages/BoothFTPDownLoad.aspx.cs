using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBMCUS.Pages
{

    public partial class BoothFTPDownLoad : System.Web.UI.Page
    {
        string ftpServerIP;
        //string ftpUserID;
        //string ftpPassword;
        FtpWebRequest reqFTP;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ftpServerIP = "27.147.146.2:21";
                txtserverip.Text = ftpServerIP;
                string[] filenames = GetFileList();
                listfile.Items.Clear();
                foreach (string filename in filenames)
                {
                    listfile.Items.Add(filename);
                }
                txtProcessDate.Focus();
                Btndownload.Visible = false;
                divmsg.Visible = false;

                //GetFileList();
            }
        }


        public string[] GetFileList()
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + txtserverip.Text + "/"));
                reqFTP.UseBinary = true;
                //reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                //MessageBox.Show(reader.ReadToEnd());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                //MessageBox.Show(response.StatusDescription);
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                downloadFiles = null;
                return downloadFiles;
            }
        }

        private void Download(string filePath, string fileName)
        {
            //FtpWebRequest reqFTP;
            try
            {
                //filePath = <<The full path where the file is to be created.>>, 
                //fileName = <<Name of the file to be created(Need not be the name of the file on FTP server).>>
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + txtserverip.Text + "/" + fileName));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                //reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void Sucessfull()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Booth Data Download Successfully done');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }
            
            return;

        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void Btndownload_Click(object sender, EventArgs e)
        {
            try
            {
                string path = "E://BOOTHDATA";
                for (int i = 0; i < chklist.Items.Count; i++)
                {
                    if (chklist.Items[i].Selected)
                    {
                        string name = chklist.Items[i].ToString();
                        Download(path, name);
                        chklist.Items[i].Selected = false;
                        divmsg.Visible = true;

                    }

                }

                Label1.Text = "Booth Data Downloaded---100% ";
            }
           catch(Exception ex)
            {

            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            if (txtProcessDate.Text == string.Empty)
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

                    Btndownload.Visible = true;
                 
                  
                }
            }
        
        }

    }
}