<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSPeriodEnd.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSPeriodEnd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        
        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Process of Day End?');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
     <br />
    <br />
    <br />
    <br />
    <div id="DivMain" runat="server" align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3" style="color: Black">
                        Customer Service Module - Day End
                    </th>
                </tr>
            </thead>
            <tr>
                <td>
                    <asp:Label ID="lblToDate" runat="server" Text="Today's Date"></asp:Label>
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtToDaysDate" runat="server" Enabled="False" BorderColor="#1293D1"
                        Width="372px" BorderStyle="Ridge" Font-Size="X-Large"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblEndOfDay" runat="server" Text="Process End of Day" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblEndOfMonth" runat="server" Text="Process Also End of Month" ForeColor="Green"></asp:Label>
                </td>
            </tr>
            <%--<tr>
                <td colspan="3">
                    <asp:Label ID="lblYearEnd" runat="server" Text="Process Also Year End" ForeColor="Blue"></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnProcess" runat="server" Text="Start Process End" CssClass="button green size-180"
                        OnClientClick="return ValidationBeforeUpdate()" OnClick="btnProcess_Click" />
                    <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="button red size-80"
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
    <div id="DivMessage" runat="server" align="center" visible="false">
        <table class="style1">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btnHideMessageDiv" runat="server" Text="OK" CssClass="button blue size-100"
                        OnClick="btnHideMessageDiv_Click" />
                </td>
            </tr>
        </table>
    </div>

    <asp:Label ID="CtrlBackUpStat" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>

