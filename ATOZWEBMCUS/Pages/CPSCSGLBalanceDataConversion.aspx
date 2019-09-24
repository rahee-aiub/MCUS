<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CPSCSGLBalanceDataConversion.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CPSCSGLBalanceDataConversion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />

    
    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Proceed?');
        }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <br />
    <br />
    <br />

    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">'Cps' - 'CS' & 'GL' Balance Data Conversion
                    </th>
                </tr>
            </thead>

          
            <tr>
                <td>
                    
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnProcess" runat="server" Text="Process" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" OnClientClick="return ValidationBeforeSave()" OnClick="BtnProcess_Click"  />&nbsp;

                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>

        </table>
    </div>

    <asp:Label ID="CtrlServiceLoanAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlComputerLoanAmt" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlOldCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuNum" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAcType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="txtAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblMemNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblId" runat="server" Text="" Visible="false"></asp:Label>

     <asp:Label ID="net54balance" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="net61balance" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblOpenDate" runat="server" Text="" Visible="false"></asp:Label>


</asp:Content>

