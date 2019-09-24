<%@ Page Title="ParameterMaintenance" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="SYSParameterMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.SYSParameterMaintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <br />
    <div align="center">
        <table class="style1">

            <thead>
                <tr>
                    <th colspan="3">System Parameter Maintenance
                    </th>
                </tr>
            </thead>
            <tr>
                <td>
                    <asp:Label ID="lblUnitName" runat="server" Text="Name:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtUnitName" runat="server" CssClass="cls text" Width="578px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" ReadOnly="true"></asp:TextBox>&nbsp;&nbsp;
                        &nbsp;&nbsp;
                </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="lblAddL1" runat="server" Text="Address Line1:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtAddressL1" runat="server" CssClass="cls text" Width="578px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" TabIndex="5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddL2" runat="server" Text="Address Line2:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtAddressL2" runat="server" CssClass="cls text" Width="578px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" TabIndex="6"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddL3" runat="server" Text="Address Line3:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtAddressL3" runat="server" CssClass="cls text" Width="578px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" TabIndex="7"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblTelNo" runat="server" Text="Telephone No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtTelNo" runat="server" CssClass="cls text" Width="316px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="11"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblSysPath" runat="server" Text="System Path:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtSysPath" runat="server" CssClass="cls text" Width="576px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="13"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblBackPath" runat="server" Text="Backup Path:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtBackPath" runat="server" CssClass="cls text" Width="576px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="14"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDataPath" runat="server" Text="Data Path:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtDataPath" runat="server" CssClass="cls text" Width="576px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="14"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblOutDataPath" runat="server" Text="Out Data Path:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtOutDataPath" runat="server" CssClass="cls text" Width="576px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="14"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblInDataPath" runat="server" Text="In Data Path:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtInDataPath" runat="server" CssClass="cls text" Width="576px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="14"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTimeOut" runat="server" Text="System TimeOut :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtTimeOut" runat="server" CssClass="cls text" Width="576px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TabIndex="14"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td class="auto-style1">
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Size="Large"
                        ForeColor="White" Font-Bold="True" CssClass="button green"
                        OnClientClick="return ValidationBeforeUpdate()" OnClick="BtnUpdate_Click" />&nbsp;
                    
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>
    </div>


</asp:Content>

