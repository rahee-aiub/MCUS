<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HRTransactionVoucherReport.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HRTransactionVoucherReport" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <br />
    <div align="center">
        <table class="style1">
            <tr>
                <th>Daily 'HR' Salary Transaction Voucher Report
                </th>
            </tr>

            <tr>
               
                <td>
                    <asp:Label ID="lblRepMode" runat="server" Text="Report Mode :" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlFuncProceed" runat="server" Height="25px" Width="320px" CssClass="cls text"
                        Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge" AutoPostBack="True" OnSelectedIndexChanged="ddlFuncProceed_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Un-Post Salary Transaction Voucher</asp:ListItem>
                        <asp:ListItem Value="2">Post Salary Transaction Voucher</asp:ListItem>
                    </asp:DropDownList>

                </td>
            </tr>




            <tr>
                <td>
                    <asp:Label ID="lblProcessDate" runat="server" Text="Process Date :" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                <%--</td>
                <td>--%>
                    &nbsp;
                    <asp:TextBox ID="txtProcessDate" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="150px" Height="25px" Font-Size="Medium"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  
                </td>
            </tr>

             <tr>
                <td>
                    <asp:Label ID="lblVchNo" runat="server" Text="Voucher No. :" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                <%--</td>
                <td>--%>
                    &nbsp;&nbsp;
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
            Height="25px" Width="130px" OnClick="BtnProcess_Click" />&nbsp;
        
        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
            Height="25px" Width="130px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
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

      <asp:Label ID="lblUserID" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="lblUserIDName" runat="server" Text="" Visible="false"></asp:Label>

      <asp:Label ID="lblAutoVchNo" runat="server" Text="" Visible="false"></asp:Label>

      <asp:Label ID="CtrlModule" runat="server" Text="" Visible="false"></asp:Label>

      <asp:Label ID="lblFromCashCode" runat="server" Text="" Visible="false"></asp:Label>

     <asp:Label ID="lblVchFlag" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblVchYear" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblProcDate" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="hdnToDaysDate" runat="server" Text="" Visible="false"></asp:Label>
      

</asp:Content>

