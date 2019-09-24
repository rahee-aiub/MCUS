<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="CSParameterOpeningMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSParameterOpeningMaintenance"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

    </script>

    <style type="text/css">
        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Parameter Opening Maintenance
                    </th>
                </tr>
            </thead>
            <tr>
                <td>
                    <asp:Label ID="lblAccType" runat="server" Text="Account Type:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccType" runat="server" Width="96px" Height="25px" AutoPostBack="True" CssClass="border_color"
                        Font-Size="Large" OnTextChanged="txtAccType_TextChanged"></asp:TextBox>
                   
                &nbsp;&nbsp;&nbsp;
                 
                    <asp:DropDownList ID="ddlAccType" runat="server" CssClass="cls text" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="260px" AutoPostBack="True" Font-Size="Large" OnSelectedIndexChanged="ddlAccType_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCalPeriod" runat="server" Text="Calculation Period:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCalPeriod" runat="server" Height="25px" Width="200px" Font-Size="Large"
                        CssClass="cls text">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Daily</asp:ListItem>
                        <asp:ListItem Value="2">Weekly</asp:ListItem>
                        <asp:ListItem Value="3">Monthly</asp:ListItem>
                        <asp:ListItem Value="4">Quarterly</asp:ListItem>
                        <asp:ListItem Value="5">Half Yearly</asp:ListItem>
                        <asp:ListItem Value="6">Yearly</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCalMethod" runat="server" Text="Calculation Method:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCalMethod" runat="server" Height="25px" Width="250px" CssClass="cls text"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Monthly Min Balance</asp:ListItem>
                        <asp:ListItem Value="2">Monthly Avg Balance</asp:ListItem>
                        <asp:ListItem Value="3">From Begining Day to Month</asp:ListItem>
                        <asp:ListItem Value="4">From Day To End Of Month</asp:ListItem>
                        <asp:ListItem Value="5">Daily Min Balance</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLoanCalMethod" runat="server" Text="Loan Calculation Method:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlLoanCalMethod" runat="server" Height="25px" Width="200px"
                        CssClass="cls text" Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Reducing</asp:ListItem>
                        <asp:ListItem Value="2">Declining</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblInterestRate" runat="server" Text="Interest Rate:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtInterestRate" runat="server" Width="96px" Height="25px" Font-Size="Large"
                        CssClass="cls text" onkeydown="return (event.keyCode !=13);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFundRate" runat="server" Text="Fund Rate:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFundRate" runat="server" Width="96px" Height="25px" Font-Size="Large"
                        CssClass="cls text" onkeydown="return (event.keyCode !=13);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblProdCondition" runat="server" Text="Product Condition:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlProdCondition" runat="server" Height="25px" Width="200px"
                        CssClass="cls text" Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Allow Withdrawal</asp:ListItem>
                        <asp:ListItem Value="2">Not Withdrawal</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblProdIntType" runat="server" Text="Product Interest Type:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlProdIntType" runat="server" Height="25px" Width="200px"
                        CssClass="cls text" Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Interest Calculation</asp:ListItem>
                        <asp:ListItem Value="2">Dividend</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMinDepositAmt" runat="server" Text="Min. Deposit Amount:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    </td>
         
                <td>
                    <asp:TextBox ID="txtMinDepositAmt" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" 
                        AutoPostBack="True" TabIndex="2" onkeydown="return (event.keyCode !=13);"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    <asp:Label ID="lblRoundFlag" runat="server" Text="Amount Rounding Flag:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlRoundFlag" runat="server" Height="25px" Width="200px"
                        CssClass="cls text" Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Round Up</asp:ListItem>
                        <asp:ListItem Value="2">Round Down</asp:ListItem>
                        <asp:ListItem Value="3">No Rounded</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td>
                    <asp:Label ID="lblIntWithdrDays" runat="server" Text="Int.Withdrawal Days :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    </td>
         
                <td>
                    <asp:TextBox ID="txtIntWithdrDays" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" onkeydown="return (event.keyCode !=13);"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblAccProcFees" runat="server" Text="Loan Processing Fees :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    </td>
         
                <td>
                    <asp:TextBox ID="txtAccProcFees" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" TabIndex="2" onkeydown="return (event.keyCode !=13);"></asp:TextBox>
     
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblAccClosingFees" runat="server" Text="Account Closing Fees :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    </td>
         
                <td>
                    <asp:TextBox ID="txtAccClosingFees" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" TabIndex="2" onkeydown="return (event.keyCode !=13);"></asp:TextBox>
                </td>
            </tr>

             <tr>
                <td>
                    <asp:Label ID="lblPeriod" runat="server" Text="Period :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    </td>
         
                <td>
                    <asp:TextBox ID="txtPeriod" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" TabIndex="2" onkeydown="return (event.keyCode !=13);"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblSlab" runat="server" Text="Period Slab:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>

                    <asp:Button ID="BtnSlab" runat="server" Text="Slab Maintain" Font-Bold="True"
                        Font-Size="Large" Height="27px" Width="155px" OnClick="BtnSlab_Click" />

                </td>
            </tr>
        </table>
    </div>
    <div align="center">
        <asp:GridView ID="gvHidden" runat="server" Width="813px" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ProductCode" HeaderText="Product Code" />
                <asp:BoundField DataField="RecordCode" HeaderText="Record Code" />
                <asp:BoundField DataField="RecordFlag" HeaderText="Flag" />
            </Columns>
        </asp:GridView>
    </div>

    <asp:Label ID="lblTest" runat="server" Text="Label" Visible="false"></asp:Label>
    <asp:Label ID="lblType" runat="server" Text="Label" Visible="false"></asp:Label>
    <asp:Label ID="lbltypecls" runat="server" Text="Label" Visible="false"></asp:Label>
    <asp:Label ID="Label4" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="Label5" runat="server" Text="" Visible="false"></asp:Label><br />
    <asp:Label ID="lblcheck" runat="server" Text="" Visible="false"></asp:Label>
    <div align="center">
        <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Width="91px" Font-Size="Large"
            ForeColor="White" Height="31px" Font-Bold="True" ToolTip="Insert Information"
            OnClientClick="return ValidationBeforeSave() " CssClass="button green" OnClick="BtnSubmit_Click" />&nbsp;
        <asp:Button ID="BtnUpdate" runat="server" Text="Update" Width="91px" Font-Size="Large"
            ForeColor="White" Height="31px" Font-Bold="True" ToolTip="Update Information"
            OnClientClick="return ValidationBeforeUpdate() " CssClass="button green" OnClick="BtnUpdate_Click" />
        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Bold="True" Font-Size="Large"
            ForeColor="#FFFFCC" Height="31px" Width="91px"
            CssClass="button red" OnClick="BtnExit_Click" />
    </div>
</asp:Content>
