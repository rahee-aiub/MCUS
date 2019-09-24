<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HRPostSalaryTransaction.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HRPostSalaryTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to Proceed ?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Print or Preview information ?');
        }

    </script>

     <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 385px;
            margin: 0 auto;
            width:4000px;
            
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }
       .FixedHeader {
            position: absolute;
            font-weight: bold;
            width:2470px;
           
            
     
        }  
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <br />

    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Post Salary Transaction
                    </th>
                </tr>
            </thead>

            <tr>
                <td>
                    <asp:Label ID="lblAccType" runat="server" Text="Process Month :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtToDaysDate" runat="server" Enabled="False" BorderColor="#1293D1"
                        Width="302px" BorderStyle="Ridge" Font-Size="X-Large"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    
                </td>
            </tr>
           
            <tr>
                <td></td>
                <td>
                    <asp:DropDownList ID="ddlFuncProceed" runat="server" Height="25px" Width="316px" CssClass="cls text"
                        Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge" AutoPostBack="True" OnSelectedIndexChanged="ddlFuncProceed_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Post Salary Transaction</asp:ListItem>
                        <asp:ListItem Value="2">Reverse Salary Transaction</asp:ListItem>
                    </asp:DropDownList>

                </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="lblVchNo" runat="server" Text="Voucher No. :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVchNo" runat="server" BorderColor="#1293D1"
                        Width="100px" BorderStyle="Ridge" Font-Size="medium" ></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    
                </td>
            </tr>

            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnProcess" runat="server" Text="Process" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" Height="27px" Width="120px" CssClass="button green" OnClientClick="return ValidationBeforeSave()" OnClick="BtnProcess_Click" />&nbsp;
                       
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>

        </table>
    </div>
   
    <br />

    <%--<div align="right" style="width: 1290px; height: 24px;">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblTotalAmt" runat="server" Text="TOTAL AMOUNT :" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>
       
        &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
              <asp:Label ID="txtTotalFDAmt" runat="server" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
             <asp:Label ID="txtTotaluptolastmnth" runat="server" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Label ID="txtTotalthisMonth" runat="server" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
              <asp:Label ID="txtTotalUptoMonth" runat="server" Font-Size="Medium" ForeColor="Black" Font-Bold="True"></asp:Label>
    </div>--%>

    
    <asp:Label ID="hdnID" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="hdnCashCode" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

    <asp:Label ID="CtrlVchNo" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc1" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc2" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc3" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc4" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc5" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc6" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc7" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc8" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc9" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblDesc10" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

    <asp:Label ID="CtrlProcDate" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="CtrlSalPostStat" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

    <asp:Label ID="lblPerNo" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblPerName" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="VerifyFlag" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    


</asp:Content>

