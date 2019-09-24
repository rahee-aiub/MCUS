<%@ Page Language="C#" MasterPageFile="~/MasterPages/HKMasterPage.Master" AutoEventWireup="true"
    CodeBehind="A2ZERPUserInitialize.aspx.cs" Inherits="ATOZWEBMCUS.Pages.A2ZERPUserInitialize"
    Title="UserId Initialize" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script language="javascript" type="text/javascript">
            function ValidationBeforeSave() {

    var ddlUserId = document.getElementById('<%=ddlUserId.ClientID%>');
                var ddlModule = document.getElementById('<%=ddlModule.ClientID%>');

             if (ddlModule.selectedIndex == 0 || ddlModule.length == '')

                alert('Please Select Module.');
             else if (ddlUserId.selectedIndex == 0 || ddlUserId.length == '')
                alert('Please Select User Id.');

   
    else
        return confirm('Are you sure you want update data?');
    return false;
}

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="DivMain" runat="server" align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3" style="color: Black" align="center">
                        <p align="center">
                            User Initialize</p>
                    </th>
                </tr>
            </thead>

            <tr>
                <th>
                    Module
                </th>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlModule" runat="server" BorderColor="#1293D1" 
                        BorderStyle="Ridge" AutoPostBack ="True" onselectedindexchanged="ddlModule_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <th>
                    ID No.
                </th>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtUserId" runat="server" CssClass="cls text" Width="100px" Height="20px" BorderColor="#1293D1" BorderStyle="Ridge"
                        AutoPostBack="true" ToolTip="Enter Ids" OnTextChanged="txtUserId_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlUserId" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"
                        AutoPostBack ="True" onselectedindexchanged="ddlUserId_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            
        </table>
    </div>
    <div id="DivButton" align="center" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnOpenForMultyUser" runat="server" Text="Open For Multi User" OnClientClick="return ValidationBeforeSave(event,this)"
                        CssClass="button green size-160" OnClick="btnOpenForMultyUser_Click" />
                    &nbsp;<asp:Button ID="btnRemovedUser" runat="server" Text="Remove User Lock" CssClass="button blue size-140"
                        OnClick="btnRemovedUser_Click" OnClientClick="return ValidationBeforeSave(event,this)" />
                    &nbsp;<asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="button red size-100"
                        OnClick="btnExit_Click" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div id="DivGridView" runat="server" align="center" visible="True">
        <table class="style1">
            <thead>
                <tr>
                    <th>
                        Following User Id is Using
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <asp:GridView ID="gvUserInfo" runat="server" BorderColor="#1293D1" BorderStyle="Ridge">
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <br />
    <br />
    <div id="DivMessage" runat="server" align="center">
        <table class="style1">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btnHideMessageDiv" runat="server" Text="OK" CssClass="button blue size-100" />
                </td>
            </tr>
        </table>
    </div>

    <asp:HiddenField ID="hdnID" runat="server" />
</asp:Content>
