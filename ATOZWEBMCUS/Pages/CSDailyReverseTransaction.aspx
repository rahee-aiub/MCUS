<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSDailyReverseTransaction.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSDailyReverseTransaction" %>

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
            width: 1180px;
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

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
                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("TrnID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="TrnDate" DataField="TrnDate" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="80px" ItemStyle-Width="80px" />

                <asp:TemplateField HeaderText="CuType" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="CuType" runat="server" Text='<%#Eval("CuType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="CuNo" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="CuNo" runat="server" Text='<%#Eval("CuNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Dep.No" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="MemNo" runat="server" Text='<%#Eval("MemNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="AccType" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="AccType" runat="server" Text='<%#Eval("AccType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="AccNo" HeaderStyle-Width="125px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="AccNo" runat="server" Text='<%#Eval("AccNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="Description" DataField="TrnDesc" HeaderStyle-Width="300px" ItemStyle-Width="300px" />

                <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="150px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="Amount" runat="server"  Enabled="false" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("GLAmount"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="TrnType" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="TrnType" runat="server" Text='<%#Eval("TrnType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DrCr" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="TrnDrCr" runat="server" Text='<%#Eval("TrnDrCr") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="sign" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="TrnSign" runat="server" Text='<%#Eval("TrnSign") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="paytype" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="PayType" runat="server" Text='<%#Eval("PayType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="funcopt" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="FuncOpt" runat="server" Text='<%#Eval("FuncOpt") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Action" Visible="false" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="143px">
                    <ItemTemplate>
                        <asp:Button ID="BtnDel" runat="server" Visible="false" Text="Delete" OnClick="BtnDel_Click" CssClass="button green" />
                        <asp:Button ID="BtnChg" runat="server" Visible="false" Text="Change" OnClick="BtnChg_Click" CssClass="button blue" />
                        <asp:Button ID="BtnUpd" runat="server" Visible="false" Text="Update" OnClick="BtnUpd_Click" CssClass="button blue" />
                        <asp:Button ID="BtnCan" runat="server" Visible="false" Text="Cancel" OnClick="BtnCan_Click" CssClass="button green" />
                       
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>

        </asp:GridView>
        <%-- </div>--%>

        <table>
            <tr>
                <td></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDelete" runat="server" Text="All Delete" Font-Bold="True" Font-Size="Medium" Height="27px" Width="120px"
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
    <asp:Label ID="CtrlAccType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlAccNo" runat="server" Text="" Visible="false"></asp:Label>
   
</asp:Content>

