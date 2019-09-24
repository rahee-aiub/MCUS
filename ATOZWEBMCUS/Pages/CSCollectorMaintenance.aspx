<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSCollectorMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSCollectorMaintenance" Title="Untitled Page" %>

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
            $("#<%=txtOpenDate.ClientID %>").dynDateTime({
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
    $(document).ready(function () {
        $("#<%=txtStatDate.ClientID %>").dynDateTime({
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Collector Maintenance
                    </th>
                </tr>

            </thead>
            <tr>
                <td>
                    <asp:Label ID="lblNo" runat="server" Text="Collector No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNo" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="X-Large" AutoPostBack="true" OnTextChanged="txtNo_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlCollectorNo" runat="server" Height="25px"
                        Width="316px" AutoPostBack="True"
                        Font-Size="Large"
                        OnSelectedIndexChanged="ddlCollectorNo_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server" Text="Collector Name:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" CssClass="cls text" Width="316px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Name"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOpenDate" runat="server" Text="Open Date:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOpenDate" runat="server" CssClass="cls text" Width="199px"
                        Height="25px" Font-Size="Large" img src="../Images/calender.png"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblNationalIDNo" runat="server" Text="National ID No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNationalIDNo" runat="server" CssClass="cls text" Width="115px"
                        Height="25px" Font-Size="Large"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddL1" runat="server" Text="Address Line1:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddressL1" runat="server" CssClass="cls text" Width="400px"
                        Height="25px" Font-Size="Large"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddL2" runat="server" Text="Address Line2:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddressL2" runat="server" CssClass="cls text" Width="400px"
                        Height="25px" Font-Size="Large"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddL3" runat="server" Text="Address Line3:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddressL3" runat="server" CssClass="cls text" Width="400px"
                        Height="25px" Font-Size="Large"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDivision" runat="server" Text="Division:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDivision" runat="server" Height="25px" Width="153px" AutoPostBack="True"
                        CssClass="cls text" Font-Size="Large" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Label ID="lblDistrict" runat="server" Text="District:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="153px" AutoPostBack="True"
                        CssClass="cls text" Font-Size="Large" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Label ID="lblThana" runat="server" Text="Thana:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    <asp:DropDownList ID="ddlThana" runat="server" Height="25px" Width="153px" AutoPostBack="True"
                        CssClass="cls text" Font-Size="Large" OnSelectedIndexChanged="ddlThana_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTelNo" runat="server" Text="Telephone No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTelNo" runat="server" CssClass="cls text" Width="316px" Height="25px"
                        Font-Size="Large"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMobNo" runat="server" Text="Mobile No:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMobileNo" runat="server" CssClass="cls text" Width="316px"
                        Height="25px" Font-Size="Large"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFax" runat="server" Text="Fax:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFax" runat="server" CssClass="cls text" Width="316px" Height="25px"
                        Font-Size="Large"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEmail" runat="server" Text="E-mail:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="cls text" Width="316px" Height="25px"
                        Font-Size="Large"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblStatus" runat="server" Text=" Status:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtStatus" runat="server" CssClass="cls text" Width="120px" Height="25px"
                        Font-Size="Large"></asp:TextBox>&nbsp;
                    <asp:Label ID="lblStatusDate" runat="server" Text=" Status Date:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    <asp:TextBox ID="txtStatDate" runat="server" CssClass="cls text" Width="115px"
                        Height="25px" Font-Size="Large" img src="../Images/calender.png"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblStatNote" runat="server" Text="Status Note:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtStatNote" runat="server" CssClass="cls text" Width="300px" Height="25px"
                        Font-Size="Large"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Font-Size="Large"
                        ForeColor="White" Font-Bold="True" ToolTip="Insert Information"
                        CssClass="button green" OnClientClick="return ValidationBeforeSave()"
                        OnClick="BtnSubmit_Click" />&nbsp;
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True"
                        Font-Size="Large" ForeColor="White" OnClientClick="return ValidationBeforeUpdate()"
                        ToolTip="Update Information" CssClass="button green"
                        OnClick="BtnUpdate_Click" />&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>
    </div>





</asp:Content>

