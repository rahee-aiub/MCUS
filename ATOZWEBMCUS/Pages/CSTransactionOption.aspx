<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSTransactionOption.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSTransactionOption" %>

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
        .auto-style1 {
            height: 38px;
        }
    </style>

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 200px;
            width: 1000px;
            margin: 0 auto;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }
    </style>

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
                    <th colspan="3">Transaction Control Maintenance
                    </th>
                </tr>

            </thead>

            <tr>
                <td class="auto-style1">
                    <asp:Label ID="lblAccType" runat="server" Text="Account Type Code:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtAccType" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtAccType_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlAcType" runat="server" Height="25px" Width="316px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddlAcType_SelectedIndexChanged">
                    </asp:DropDownList>

                    <asp:Label ID="lblclass" runat="server" Text="" Font-Size="Large"
                        ForeColor="Red" Visible="false"></asp:Label>
                    <asp:Label ID="lblTypeDes" runat="server" Text="" Font-Size="Large"
                        ForeColor="Red" Visible="false"></asp:Label>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFunctionOpt" runat="server" Text="Function Option:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlFunctionOpt" runat="server" Height="25px" Width="316px" CssClass="cls text"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Deposit Amount</asp:ListItem>
                        <asp:ListItem Value="2">Withdrawal Amount</asp:ListItem>
                        <asp:ListItem Value="3">Interest Withdrawal</asp:ListItem>
                        <asp:ListItem Value="4">Loan Disbursement</asp:ListItem>
                        <asp:ListItem Value="5">Encashment/Settlement</asp:ListItem>
                        <asp:ListItem Value="6">Auto Renewal Processing</asp:ListItem>
                        <asp:ListItem Value="7">Auto Anniversary Processing</asp:ListItem>
                        <asp:ListItem Value="8">Auto Provision Processing</asp:ListItem>
                        <asp:ListItem Value="9">Auto Interest Processing</asp:ListItem>
                        <asp:ListItem Value="10">Auto Dividend Processing</asp:ListItem>
                        <asp:ListItem Value="11">Auto Service Charge processing</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPayType" runat="server" Text="Pay Type:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPayType" runat="server" Height="25px" Width="316px" CssClass="cls text"
                        Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblTrnType" runat="server" Text="Trnsaction Type:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTrnType" runat="server" Height="25px" Width="316px" CssClass="cls text"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Cash</asp:ListItem>
                        <asp:ListItem Value="3">Trf.</asp:ListItem>
                        <asp:ListItem Value="47">CaChq</asp:ListItem>
                        <asp:ListItem Value="99">Input</asp:ListItem>

                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblTrnMode" runat="server" Text="Trnsaction Mode:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTrnMode" runat="server" Height="25px" Width="316px" CssClass="cls text"
                        Font-Size="Large">

                        <asp:ListItem Value="0">Debit</asp:ListItem>
                        <asp:ListItem Value="1">Credit</asp:ListItem>

                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblTrnAmtLogic" runat="server" Text="Transaction Amount Logic:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTranAmtLogic" runat="server" Height="25px" Width="316px" CssClass="cls text"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Share Minimum Amount</asp:ListItem>
                        <asp:ListItem Value="2">Pension Monthly Deposit</asp:ListItem>
                        <asp:ListItem Value="3">Loan Disbursement Amount</asp:ListItem>
                        <asp:ListItem Value="4">Loan Return Amount</asp:ListItem>
                        <asp:ListItem Value="5">Interest Return Amount</asp:ListItem>
                        
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTrnValidation" runat="server" Text="Transaction Validation:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTranValidation" runat="server" Height="25px" Width="316px" CssClass="cls text"
                        Font-Size="Large">
                        <asp:ListItem Value="0">No</asp:ListItem>
                        <asp:ListItem Value="1">Yes</asp:ListItem>

                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" ToolTip="Insert Information" CssClass="button green" OnClientClick="return ValidationBeforeSave()"
                        OnClick="BtnSubmit_Click" />&nbsp;
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="Update Information" CssClass="button green" OnClientClick="return ValidationBeforeUpdate()"
                        OnClick="BtnUpdate_Click" />&nbsp;
                    <asp:Button ID="BtnView" runat="server" Text="View" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="View Information" CssClass="button green"
                        OnClick="BtnView_Click" />&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click"  />
                    <br />
                </td>
            </tr>
        </table>
    </div>

    <div align="left" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" Width="1325px" Height="150px"
            AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84"
            BorderStyle="None" BorderWidth="1px" CellPadding="1" CellSpacing="1" EnableModelValidation="True">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>

                <asp:BoundField HeaderText="AccType" DataField="AccType" />
                <asp:BoundField HeaderText="FuncOpt" DataField="FuncOpt" />
                <asp:BoundField HeaderText="PayType" DataField="PayType" />
                <asp:BoundField HeaderText="TranType" DataField="TrnType" />
                <asp:BoundField HeaderText="TranMode" DataField="TrnMode" />
                <asp:BoundField HeaderText="TranLogic" DataField="TrnLogic" />
                <asp:BoundField HeaderText="TranValid" DataField="TrnValidation" />
            </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>


</asp:Content>
