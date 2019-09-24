<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/INVMasterPage.Master" AutoEventWireup="true" CodeBehind="STDailyReverseTransaction.aspx.cs" Inherits="ATOZWEBMCUS.Pages.STDailyReverseTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Reverse Transaction?');
        }

    </script>

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 385px;
            margin: 0 auto;
            width: 1380px;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            /*width: 1150px;*/
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
                    <th colspan="3">Daily Reverse Transaction
                    </th>
                </tr>

            </thead>

        </table>

        <table>
            <tr>
                <td></td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblVoucherNo" runat="server" Text="Voucher No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="cls text" Width="192px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"></asp:TextBox>

                    <asp:Button ID="BtnSearch" runat="server" Text="Search" Font-Bold="True" Font-Size="Medium"
                        ForeColor="White" CssClass="button green" OnClick="BtnSearch_Click" />

                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />

    <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvDetailInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>


                <asp:TemplateField HeaderText="Id" Visible="false" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="TrnDate" DataField="TransactionDate" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px" ItemStyle-Width="100px" />

                <asp:TemplateField HeaderText="Function" HeaderStyle-Width="300px" ItemStyle-Width="300px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="Function" runat="server" Text='<%#Eval("FuncOptDesc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:BoundField DataField="TrnAmtDr" DataFormatString="{0:0,0.00}" HeaderText="Debit Amt.">
                            <HeaderStyle Width="150px" />
                            <ItemStyle HorizontalAlign="Right" Width="150px" />
                        </asp:BoundField>

                <asp:BoundField DataField="TrnAmtCr" DataFormatString="{0:0,0.00}" HeaderText="Credit Amt.">
                            <HeaderStyle Width="150px" />
                            <ItemStyle HorizontalAlign="Right" Width="150px" />
                        </asp:BoundField>



               

            </Columns>

        </asp:GridView>
        <%-- </div>--%>

        <table>
            <tr>
                <td></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Font-Bold="True" Font-Size="Medium" Height="27px" Width="120px"
                    ForeColor="White" CssClass="button green" OnClientClick="return ValidationBeforeUpdate()" OnClick="btnDelete_Click" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Font-Bold="True" Font-Size="Medium" Height="27px" Width="120px"
                    ForeColor="White" CssClass="button blue" OnClick="btnCancel_Click" />
                    &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>

    </div>


    <asp:Label ID="CtrlTrnDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="ValidityFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAtyClass" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlBackUpStat" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="HdnFuncOpt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="HdnFOpt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="HdnModule" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnPrmValue" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblGLAccBal" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblVchAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblVchTrnType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTrnRevFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblGLBalanceType" runat="server" Text="" Visible="false"></asp:Label>

     <asp:Label ID="lblTrnID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblChgFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCanFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblRecID" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblFuncOpt" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>

