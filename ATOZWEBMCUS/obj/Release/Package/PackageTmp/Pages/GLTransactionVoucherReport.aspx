<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="GLTransactionVoucherReport.aspx.cs" Inherits="ATOZWEBMCUS.Pages.GLTransactionVoucherReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

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


    </script>--%>

      <script language="javascript" type="text/javascript">
          $(function () {
              $("#<%= txtProcessDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtProcessDate.ClientID %>").datepicker();

             });

        });
         

            </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <br />
    <br />
    <div align="center">
        <table class="style1">
            <tr>
                <th>Daily 'GL' Transaction Voucher Report
                </th>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblProcessDate" runat="server" Text="Process Date :" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtProcessDate" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="150px" Height="25px" Font-Size="Medium"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblVchNo" runat="server" Text="Voucher No. :" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVchNo" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="150px" Height="25px" Font-Size="Medium"></asp:TextBox>


                </td>
            </tr>



        </table>
    </div>
    <br />
    <div align="center">


        <asp:Button ID="BtnProcess" runat="server" Text="Print/Preview" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" CssClass="button green"
            Height="24px" OnClick="BtnProcess_Click" />&nbsp;
        
        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
            Height="24px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
            CssClass="button red" OnClick="BtnExit_Click" />
    </div>
   
    
      <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="lblMemNo" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="lblFuncOpt" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="lblTrnType" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="lblMemName" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="lblTrnTypeTitle" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="lblFuncTitle" runat="server" Text="" Visible="false"></asp:Label>

      <asp:Label ID="lblBoothNo" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="lblBoothName" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>

      <asp:Label ID="lblValueDate" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="lblDrCr" runat="server" Text="" Visible="false"></asp:Label>

      <asp:Label ID="lblUserID" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="lblUserIDName" runat="server" Text="" Visible="false"></asp:Label>


</asp:Content>

