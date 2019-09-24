<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="A2ZFTPUPLOAD.aspx.cs" Inherits="ATOZWEBMCUS.Pages.A2ZFTPUPLOAD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />
      
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtProcessDate.ClientID %>").dynDateTime({
                showsTime: false,
                ifFormat: "%d/%m/%Y",
                daFormat: "%l;%M %p, %e %m, %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });


    </script>
      
    <script type="text/javascript">
        function ProgressBarSim(al) {
            var bar = document.getElementById('progressbar');
            var status = document.getElementById('status');
            status.innerHTML = al + "%";
            bar.value = al;
            al++;
            var sim = setTimeout("progressbar(" + al + ")", 300);
            if (al == 100) {
                status.innerHTML = "100%";
                bar.value = 100;
                clearTimeout(sim);
                document.getElementById('finalmessage').innerHTML = "File Uploading Successfully done!";
            }

        }
        var amountloaded = 100;
        ProgressBarSim(amountloaded);
       </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <div align="center">
        <table class="style1">
            <tr>
                <th>
                    Booth Data Upload
                </th>
            </tr>
            <tr>
                <td >
                    <asp:Label ID="lblServerIp" runat="server" Text="Server IP :" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                </td>
                <td>
                     <asp:TextBox ID="txtserverip" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                            Width="150px" Height="25px" Font-Size="Medium" ReadOnly="True" ></asp:TextBox>
                            
                     
                </td>
            </tr>
            </table><br />
             <div align="center">
                 <table class="style1">
                     <tr>
                <td >
                    <asp:Label ID="lblProcessDate" runat="server" Text="Process Date :" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                </td>
                <td>
                     <asp:TextBox ID="txtProcessDate" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                            Width="150px" Height="25px" Font-Size="Medium" ></asp:TextBox><img src="../Images/calender.png" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="BtnSearch" runat="server" Text="Search File" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" CssClass="button green" 
            Height="22px" OnClick="BtnSearch_Click"/>
                </td>
            </tr>
                 </table>
                 <asp:checkboxlist runat="server" ID="chklist" BackColor="Silver" BorderColor="#CC0000" BorderStyle="Double" ForeColor="Black"  Width="497px" Height="19px" ></asp:checkboxlist>
                 
                  
                 <asp:ListBox ID="listfile" runat="server" Visible="False" Width="264px"></asp:ListBox>
                 
                  
           </div><br />
               
        <div>
            <asp:Image ID="Image1" ImageUrl="~/Images/please_wait.gif" runat="server" />
        </div>
        <%--<div id="divmsg" runat="server" style="border-style: none; color: #0000FF; font-style: italic; font-weight: normal; font-size: x-large;">
        File Upload Processing Status<br />
            <progress id="progressbar" value="100" max="100" style="width: 370px"></progress>
            <span id="status"></span>
            <br />
             <asp:Label ID="Label1" runat="server" Font-Size="X-Large"
                            ForeColor="Red"></asp:Label>
       
    
       
    </div>--%>
            

        <div align="center">
            <br />
        <asp:Button ID="BtnUpload" runat="server" Text="Upload" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" CssClass="button green" 
            Height="22px" OnClick="BtnUpload_Click" />
            &nbsp;
        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
            Height="24px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
            CssClass="button red" OnClick="BtnExit_Click"/>
    </div>
          </div>






</asp:Content>

