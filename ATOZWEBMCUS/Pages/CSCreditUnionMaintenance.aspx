<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="CSCreditUnionMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSCreditUnionMaintenance"
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

    <script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtCuOpenDate.ClientID %>").dynDateTime({
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
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div align="center">
        <table class="style1">

            <thead>
                <tr>
                    <th colspan="3">Credit Union Maintenance
                    </th>
                </tr>

            </thead>
            <tr>
                <td>
                    <asp:Label ID="lblCUNo" runat="server" Text="Credit Union No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCreditUNo" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="X-Large" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtCreditUNo_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlCreditUNo" runat="server" Height="25px" Width="316px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddlCreditUNo_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCreUName" runat="server" Text="Credit Union Name:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCreUName" runat="server" CssClass="cls text" Width="316px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Name"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuOpenDate" runat="server" Text="Open Date:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCuOpenDate" runat="server" CssClass="cls text" Width="199px"
                        Height="25px" Font-Size="Large" img src="../Images/calender.png"
                        TabIndex="1" AutoPostBack="True" OnTextChanged="txtCuOpenDate_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuMemType" runat="server" Text="Member Type:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCuMemberFlag" runat="server" Height="25px" Width="200px"
                        Font-Size="Large" CssClass="cls text" TabIndex="2">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Single Member</asp:ListItem>
                        <asp:ListItem Value="2">Multiple Member</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuMemStatus" runat="server" Text="Member Status:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCuMemberType" runat="server" Height="25px" Width="200px"
                        Font-Size="Large" CssClass="cls text" TabIndex="3">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Affiliate Member</asp:ListItem>
                        <asp:ListItem Value="2">Associate Member</asp:ListItem>
                        <asp:ListItem Value="3">Regular Member</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuCertNo" runat="server" Text="Certificate No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCuCertificateNo" runat="server" CssClass="cls text" Width="115px"
                        Height="25px" Font-Size="X-Large" TabIndex="4"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuAddL1" runat="server" Text="Address Line1:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCuAddressL1" runat="server" CssClass="cls text" Width="400px"
                        Height="25px" Font-Size="Large" TabIndex="5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuAddL2" runat="server" Text="Address Line2:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCuAddressL2" runat="server" CssClass="cls text" Width="400px"
                        Height="25px" Font-Size="Large" TabIndex="6"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuAddL3" runat="server" Text="Address Line3:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCuAddressL3" runat="server" CssClass="cls text" Width="400px"
                        Height="25px" Font-Size="Large" TabIndex="7"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuDivision" runat="server" Text="Division:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDivision" runat="server" Height="25px" Width="153px" AutoPostBack="True"
                        CssClass="cls text" Font-Size="Large"
                        OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" TabIndex="8">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Label ID="lblCuDistrict" runat="server" Text="District:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="153px" AutoPostBack="True"
                        CssClass="cls text" Font-Size="Large"
                        OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" TabIndex="9">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Label ID="lblCuThana" runat="server" Text="Thana:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    <asp:DropDownList ID="ddlThana" runat="server" Height="25px" Width="153px" AutoPostBack="True"
                        CssClass="cls text" Font-Size="Large"
                        OnSelectedIndexChanged="ddlThana_SelectedIndexChanged" TabIndex="10">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuTelNo" runat="server" Text="Telephone No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCuTelNo" runat="server" CssClass="cls text" Width="316px" Height="25px"
                        Font-Size="Large" TabIndex="11"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuMobNo" runat="server" Text="Mobile No:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCuMobileNo" runat="server" CssClass="cls text" Width="316px"
                        Height="25px" Font-Size="Large" TabIndex="12"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuFax" runat="server" Text="Fax:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCuFax" runat="server" CssClass="cls text" Width="316px" Height="25px"
                        Font-Size="Large" TabIndex="13"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuEmail" runat="server" Text="E-mail:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCuEmail" runat="server" CssClass="cls text" Width="316px" Height="25px"
                        Font-Size="Large" TabIndex="14"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnCreUnionSubmit" runat="server" Text="Submit" Font-Size="Large"
                        ForeColor="White" Font-Bold="True" ToolTip="Insert Information" CssClass="button green"
                        OnClick="BtnCreUnionSubmit_Click" OnClientClick="return ValidationBeforeSave()" />&nbsp;
                    <asp:Button ID="BtnCreUnionUpdate" runat="server" Text="Update" Font-Bold="True"
                        Font-Size="Large" ForeColor="White" OnClientClick="return ValidationBeforeUpdate()"
                        ToolTip="Update Information" CssClass="button green" OnClick="BtnCreUnionUpdate_Click" />&nbsp;
                    <asp:Button ID="BtnCreUniontExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red"
                        PostBackUrl="~/pages/A2ZERPModule.aspx" />
                    <br />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
