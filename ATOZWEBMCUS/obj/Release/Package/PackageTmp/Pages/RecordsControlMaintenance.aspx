<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="RecordsControlMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.RecordsControlMaintenance" %>

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
            height: 250px;
            width: 500px;
            margin: 0 auto;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            width: 480px;
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
                    <th colspan="3">Record Control Maintenance
                    </th>
                </tr>

            </thead>

            <tr>
                <td>
                    <asp:Label ID="lblGLCashCode" runat="server" Text="GL Cash Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtGLCashCode" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" AutoPostBack="True" OnTextChanged="txtGLCashCode_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlGLCashCode" runat="server" Height="25px" Width="275px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" AutoPostBack="True" OnSelectedIndexChanged="ddlGLCashCode_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblRecordType" runat="server" Text="Record Type:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRecType" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium"  AutoPostBack="True" OnTextChanged="txtRecType_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLastRecordNo" runat="server" Text="Last Record No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLastRecordNo" runat="server" CssClass="cls text" Width="114px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" ToolTip="Enter Name"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>&nbsp;
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="Submit Information" CssClass="button green"
                        OnClientClick="return ValidationBeforeSave()" OnClick="BtnSubmit_Click" />&nbsp;
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="Update Information" CssClass="button green"
                        OnClientClick="return ValidationBeforeUpdate()" OnClick="BtnUpdate_Click" />&nbsp;
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
        <div align="center" class="grid_scroll">
            <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
                AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" Height="250px" OnRowDataBound="gvDetailInfo_RowDataBound">
                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                <Columns>
                    <asp:BoundField HeaderText="Cash Code" DataField="CtrlGLCashCode" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Rec Type" DataField="CtrlRecType" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Last Records No." DataField="CtrlRecLastNo" HeaderStyle-Width="200px" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center" />
                </Columns>

            </asp:GridView>
        </div>

    </div>
</asp:Content>

