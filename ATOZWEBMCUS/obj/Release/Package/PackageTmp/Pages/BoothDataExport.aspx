<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="BoothDataExport.aspx.cs" Inherits="ATOZWEBMCUS.Pages.BoothDataExport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtFromDate.ClientID %>").dynDateTime({
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

        $(document).ready(function () {
            $("#<%=txtToDate.ClientID %>").dynDateTime({
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

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <br />
    <br />
    <div align="center">
        
        <asp:Label ID="Label1" runat="server" Text="Cash Code :" Font-Size="X-Large"
                            ForeColor="Red"></asp:Label>
        <asp:Label ID="lblCashCode" runat="server" Font-Size="X-Large"
                            ForeColor="Red"></asp:Label>
    </div>
    <br />
      <div align="center">
        <table class="style1">
            <tr>
                <th>
                    Booth Data Export
                </th>
            </tr>
            <tr>
                <td >
                    <asp:Label ID="lblFromDate" runat="server" Text="From Date :" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                </td>
                <td>
                     <asp:TextBox ID="txtFromDate" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                            Width="150px" Height="25px" Font-Size="Medium" ></asp:TextBox><img src="../Images/calender.png" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTodate" runat="server" Text="To Date :" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                     <asp:TextBox ID="txtToDate" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                            Width="150px" Height="25px" Font-Size="Medium"  img src="../Images/calender.png"></asp:TextBox>
                </td>
            </tr>
            </table>
          </div>
    <div align="center">
        <asp:Button ID="BtnExport" runat="server" Text="Export" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" CssClass="button green" 
            Height="22px" OnClick="BtnExport_Click"/>&nbsp;
      
        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
            Height="24px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
            CssClass="button red" OnClick="BtnExit_Click"/>
    </div>


</asp:Content>

