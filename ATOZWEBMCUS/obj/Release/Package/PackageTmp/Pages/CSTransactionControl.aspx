<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSTransactionControl.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSTransactionControl" %>

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
        .grid_scroll {
            overflow: auto;
            height: 190px;
            margin: 0 auto;
            width: 900px;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }
    </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />

    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Transaction Control Setup Maintenance
                    </th>
                </tr>

            </thead>
            <tr>
                <td>
                    <asp:Label ID="lblFunctionOpt" runat="server" Text="Function Option:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlFunctionOpt" runat="server" Height="25px" Width="316px" CssClass="cls text"
                        Font-Size="Large" BorderColor="#1293D1" BorderStyle="Ridge" AutoPostBack="true" OnSelectedIndexChanged="ddlFunctionOpt_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <%--<asp:ListItem Value="1">Deposit Amount</asp:ListItem>
                        <asp:ListItem Value="2">Loan Settlement</asp:ListItem>
                        <asp:ListItem Value="3">Cash Withdrawal Amount</asp:ListItem>
                        <asp:ListItem Value="4">Trf. Withdrawal Amount</asp:ListItem>
                        <asp:ListItem Value="5">Cash Interest/Benefit Withdrawal</asp:ListItem>
                        <asp:ListItem Value="6">Trf. Interest/Benefit Withdrawal</asp:ListItem>
                        <asp:ListItem Value="7">Cash Loan Disbursement</asp:ListItem>
                        <asp:ListItem Value="8">Trf. Loan Disbursement</asp:ListItem>
                        <asp:ListItem Value="9">Cash Encashment</asp:ListItem>
                        <asp:ListItem Value="10">Trf. Encashment</asp:ListItem>
                        <asp:ListItem Value="11">Adjustment Credit</asp:ListItem>
                        <asp:ListItem Value="12">Adjustment Debit</asp:ListItem>
                        <asp:ListItem Value="13">Auto Provision Processing</asp:ListItem>
                        <asp:ListItem Value="14">Auto Anniversary Processing</asp:ListItem>
                        <asp:ListItem Value="15">Auto Renewal Processing</asp:ListItem>
                        <asp:ListItem Value="16">Auto Monthly Benefit Processing</asp:ListItem>
                        <asp:ListItem Value="17">Auto Interest Processing</asp:ListItem>
                        <asp:ListItem Value="18">Auto Dividend Processing</asp:ListItem>
                        <asp:ListItem Value="19">Auto Service Charge processing</asp:ListItem>
                        <asp:ListItem Value="20">Account Balance Trf.</asp:ListItem>
                        <asp:ListItem Value="51">Adjustment Provision - Credit</asp:ListItem>
                        <asp:ListItem Value="52">Adjustment Provision - Debit</asp:ListItem>
                        <asp:ListItem Value="75">Payroll Deposit Amt. - Debit</asp:ListItem>--%>
                    </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTrncode" runat="server" Text="Transaction Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTrncode" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtTrncode_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlTranCode" runat="server" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="316px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddlTranCode_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>



            <tr>
                <td>
                    <asp:Label ID="lblPayType" runat="server" Text="Pay Type:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPayType" runat="server" Height="25px" Width="316px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged">
                        <%--<asp:ListItem Value="0">-Select-</asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTrnType" runat="server" Text="Transaction Type:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTrnType" runat="server" Height="25px" Width="316px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
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




                    <asp:CheckBox ID="ChkDebit" runat="server" Text="Debit" OnCheckedChanged="ChkDebit_CheckedChanged" AutoPostBack="True" />

                    <asp:CheckBox ID="ChkCredit" runat="server" Text="Credit" OnCheckedChanged="ChkCredit_CheckedChanged" AutoPostBack="True" />
                    <asp:Label ID="lblTrnMod" runat="server" Font-Size="Large" Visible="False"></asp:Label>


                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblTrnRecDesc" runat="server" Text="Transaction Description:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTrnRecDesc" runat="server" CssClass="cls text" Width="300px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblTrnAmtLogic" runat="server" Text="Transaction Amount Logic:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTranAmtLogic" runat="server" Height="25px" Width="316px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Share Minimum Amount</asp:ListItem>
                        <asp:ListItem Value="2">Pension Monthly Deposit</asp:ListItem>
                        <asp:ListItem Value="3">Fixed Deposit Amount</asp:ListItem>
                        <asp:ListItem Value="4">Loan Disbursement Amount</asp:ListItem>
                        <asp:ListItem Value="5">OD Loan Withdrawal Amount</asp:ListItem>
                        <asp:ListItem Value="6">Interest Received</asp:ListItem>
                        <asp:ListItem Value="7">Loan Amount Received</asp:ListItem>
                        <asp:ListItem Value="8">Interest Withdrawn</asp:ListItem>
                        <asp:ListItem Value="9">Benefit Withdrawn</asp:ListItem>
                        <asp:ListItem Value="10">Interest Amount - FDR</asp:ListItem>
                        <asp:ListItem Value="11">Interest Adjustment - FDR</asp:ListItem>
                        <asp:ListItem Value="12">Encashment Int. - FDR</asp:ListItem>
                        <asp:ListItem Value="13">Encashment Principal- FDR</asp:ListItem>
                        <asp:ListItem Value="14">Interest Amount - 6YR</asp:ListItem>
                        <asp:ListItem Value="15">Interest Adjustment - 6YR</asp:ListItem>
                        <asp:ListItem Value="16">Encashment Int.- 6YR</asp:ListItem>
                        <asp:ListItem Value="17">Encashment Principal - 6YR</asp:ListItem>
                        <asp:ListItem Value="18">Benefit Amount - MS+</asp:ListItem>
                        <asp:ListItem Value="19">Benefit Adjustment - MS+</asp:ListItem>
                        <asp:ListItem Value="20">Encashment Benefit - MS+</asp:ListItem>
                        <asp:ListItem Value="21">Encashment Principal - MS+</asp:ListItem>
                        <asp:ListItem Value="22">Net Interest Received</asp:ListItem>
                        <asp:ListItem Value="23">Net Loan Amt.Received</asp:ListItem>
                        <asp:ListItem Value="25">Penal Interest</asp:ListItem>
                        <asp:ListItem Value="26">Adjustment Int.Cr.Pension</asp:ListItem>
                        <asp:ListItem Value="27">Adjustment Int.Dr.Pension</asp:ListItem>
                        <asp:ListItem Value="28">Closing Fees</asp:ListItem>
                        <asp:ListItem Value="29">Net Encashment - Pension</asp:ListItem>

                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblShowInt" runat="server" Text="Show Trnsaction Amount :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlShowInt" runat="server" Height="25px" Width="316px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large">
                        <asp:ListItem Value="0">Transaction Show in Statement</asp:ListItem>
                        <asp:ListItem Value="1">Transaction Not Show in Statement</asp:ListItem>

                    </asp:DropDownList>
                    <asp:Label ID="lblClass" runat="server" Font-Size="Large" Visible="False"></asp:Label>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblRecMode" runat="server" Text="Record Mode :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlRecMode" runat="server" Height="25px" Width="316px" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large">
                        <asp:ListItem Value="0">Record Input Allow</asp:ListItem>
                        <asp:ListItem Value="1">Record Read Only</asp:ListItem>

                    </asp:DropDownList>
                </td>
            </tr>          
            <tr>
                <td>
                    <asp:Label ID="lblGLAccNoDR" runat="server" Text="GL AccountNo DR:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtGLAccNoDR" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblGLAccNoCR" runat="server" Text="GL AccountNo CR:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtGLAccNoCR" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"></asp:TextBox>
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
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>
    </div>

    <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvDetailInfo_RowDataBound1" RowStyle-Height="10px">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>

                <asp:BoundField HeaderText="Func Opt" DataField="FuncOpt" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="Trn.Code" DataField="TrnCode" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="Type Code" DataField="AccType" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="Pay Code" DataField="PayType" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="Trn Type" DataField="TrnType" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="Trn Mode" DataField="TrnMode" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="Logic" DataField="TrnLogic" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="Show Int." DataField="ShowInt" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="Rec.Mode" DataField="RecMode" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="GLAccNoDR" DataField="GLAccNoDR" HeaderStyle-Width="80px" ItemStyle-Width="92px" />
                <asp:BoundField HeaderText="GLAccNoCR" DataField="GLAccNoCR" HeaderStyle-Width="80px" ItemStyle-Width="92px" />

            </Columns>

        </asp:GridView>
    </div>

    <asp:Label ID="lblAccType" runat="server" Font-Size="Large" Visible="False"></asp:Label>
    <asp:Label ID="lblAccTypeMode" runat="server" Font-Size="Large" Visible="False"></asp:Label>
    <asp:Label ID="lblPayMode" runat="server" Font-Size="Large" Visible="False"></asp:Label>

</asp:Content>

