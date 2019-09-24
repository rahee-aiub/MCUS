<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="A2ZERPUserSMSControl.aspx.cs" Inherits="ATOZWEBMCUS.Pages.A2ZERPUserSMSControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">

        function UIFieldValidation() {

          <%--  var ddlUserId = document.getElementById('<%=ddlIdsNo.ClientID%>');--%>

            if (ddlUserId.selectedIndex == 0)
                alert('Please Select User Id.');

            else
                return confirm('Are You Sure Want To Add SMS?');
            return false;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div id="DivMain" runat="server" align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="6" style="color: Black" align="center">
                        <%-- <p align="center">--%>
                            User's SMS Control
                       <%-- </p>--%>
                    </th>
                </tr>
            </thead>
            <tr>

                <td>
                    <asp:Label ID="lblSender" runat="server" CssClass="cls text" Width="190px" Height="25px" Text="Sender ID No. :" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="X-Large"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFromIdsNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="X-Large"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Label ID="lblFromIdsName" runat="server" Width="400px" Height="25px" Text="" Font-Size="X-Large"></asp:Label>
                </td>


            </tr>
            <%--<tr>
                <td>
                    <asp:Label ID="lblSentMode" runat="server" CssClass="cls text" Width="115px" Height="25px" Text="Sent to :" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="X-Large"></asp:Label>
                </td>&nbsp;&nbsp;&nbsp;&nbsp;
                <td>
                    <asp:DropDownList ID="ddlSentMode" runat="server" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="316px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddlSentMode_SelectedIndexChanged">
                        <asp:ListItem Value="1">-Head Office</asp:ListItem>
                        <asp:ListItem Value="2">-Booths</asp:ListItem>

                    </asp:DropDownList>
                </td>

            </tr>--%>
        </table>
    </div>
    <div id="DivGridView" runat="server" align="center" visible="False" style="height: 290px; overflow: auto;">
        <table class="style1">
            <tbody>
                <tr>
                    <td>
                        <asp:GridView ID="gvModule" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>

        </table>
    </div>
    <br />

    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="6" style="color: Red" align="center">
                        <%-- <p align="center">--%>
                            Text Note Messages
                       <%-- </p>--%>
                    </th>
                </tr>
            </thead>
            <tr>

                <td>
                    <asp:TextBox ID="txtsmsMsg" runat="server" Width="490px" Height="100px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div id="DivButton" align="center" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Send"
                        CssClass="button green size-120" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnMark" runat="server" Text="All Mark" CssClass="button blue size-100"
                        OnClick="btnMark_Click" />
                    <asp:Button ID="btnUnMark" runat="server" Text="All Un-Mark" CssClass="button green size-100"
                        OnClick="btnUnMark_Click" />
                    <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="button red size-120" OnClick="btnExit_Click" />
                </td>
            </tr>
        </table>
    </div>

    <asp:Label ID="lblNewSMSNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblProcDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblSMSStatus" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlPrmValue" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlModule" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
