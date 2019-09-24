<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSChqBook.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSCheckBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Add New Cheque Book ?');
        }

        function MsgDuplicate() {
            return confirm('Cheque Book/Leaf Already Exit');
            return confirm('Wish to Issue a New Cheque Book ?');
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
            $("#<%=txtIssueDate.ClientID %>").dynDateTime({
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
    <br />
    <div align="center">
        <table class="style1">

            <thead>
                <tr>
                    <th colspan="3">Cheque Book Issue
                    </th>
                </tr>

            </thead>

            <tr>
                <td>
                    <asp:Label ID="lblMemNo" runat="server" Text="Member No:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMemNo" runat="server" CssClass="cls text" Width="118px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" AutoPostBack="True" OnTextChanged="txtMemNo_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlMemNo" runat="server" Height="25px" Width="250px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddlMemNo_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                 
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAccType" runat="server" Text="Account Type:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccType" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" AutoPostBack="true" OnTextChanged="txtAccType_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlAccType" runat="server" Height="25px" Width="250px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddlAccType_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAccNo" runat="server" Text="Account No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccNo" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" OnTextChanged="txtAccNo_TextChanged" AutoPostBack="true"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblChqprefix" runat="server" Text="Cheque Prefix:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtChqprefix" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblNumPage" runat="server" Text="Number Of Pages :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNumPage" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" OnTextChanged="txtNumPage_TextChanged" AutoPostBack="true"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblBeginingNo" runat="server" Text="Beginning No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBeginingNo" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" AutoPostBack="True" OnTextChanged="txtBeginingNo_TextChanged"></asp:TextBox>
                    <asp:Label ID="lblSlNo" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblbPage" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEndNo" runat="server" Text="Ending No :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEndNo" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" AutoPostBack="True" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblIssueDate" runat="server" Text="Issue Date :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIssueDate" runat="server" CssClass="cls text" Width="115px" Height="25px" DateFormatString="dd/MM/yyyy"
                        Font-Size="Large" img src="../Images/calender.png" AutoPostBack="True" OnTextChanged="txtIssueDate_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" OnClientClick="return ValidationBeforeSave()" OnClick="BtnSubmit_Click" />
                    &nbsp;
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" CssClass="button green" OnClientClick="return ValidationBeforeUpdate()" />
                    &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>
    </div>



</asp:Content>

