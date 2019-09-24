<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSChqStatusChange.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSCheckStatusChange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
    <asp:Label ID="lblvalue2" runat="server" Text="Label" Visible="false"></asp:Label>

    <br />
    <br />
    <br />
    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Cheque Status Changes
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
                  <%--  <asp:Label ID="lblMemType" runat="server" Text="Member Type" Font-Size="Large" 
                        ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList 
                        ID="ddlMemType" runat="server" Height="25px" Width="150px" CssClass="cls text"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Premium</asp:ListItem>
                        <asp:ListItem Value="2">Normal</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
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
                        Font-Size="Large" OnTextChanged="txtAccNo_TextChanged" AutoPostBack="true" ReadOnly="true"></asp:TextBox>

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
                    <asp:Label ID="lblCheqLeafNo" runat="server" Text="Cheque Leaf No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCheqLeafNo" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" OnTextChanged="txtCheqLeafNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <asp:Label ID="lblSlNo" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblbPage" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="lblNumPage" runat="server" Text="Number Of Page :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNumPage" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblBeginingNo" runat="server" Text="Beginning No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBeginingNo" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEndNo" runat="server" Text="Ending No :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEndNo" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCheBook" runat="server" Text="Select Page/Book:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCheBook" runat="server" Height="25px" Width="150px" CssClass="cls text"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Single Page</asp:ListItem>
                        <asp:ListItem Value="2">Complete Book</asp:ListItem>

                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblChqOption" runat="server" Text="Status Option:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlChqOption" runat="server" Height="25px" Width="150px" CssClass="cls text"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Uncashed</asp:ListItem>
                        <asp:ListItem Value="2">cashed</asp:ListItem>
                        <asp:ListItem Value="3">Stock</asp:ListItem>
                        <asp:ListItem Value="4">Dishonoured</asp:ListItem>
                        <asp:ListItem Value="5">Lost</asp:ListItem>
                        <asp:ListItem Value="6">Destroy</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" CssClass="button green" OnClientClick="return ValidationBeforeUpdate()" OnClick="BtnUpdate_Click" />
                    &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>
    </div>



</asp:Content>

