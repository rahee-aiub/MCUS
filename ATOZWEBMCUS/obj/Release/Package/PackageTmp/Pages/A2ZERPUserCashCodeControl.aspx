<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="A2ZERPUserCashCodeControl.aspx.cs" Inherits="ATOZWEBMCUS.Pages.A2ZERPUserCashCodeControl" %>

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
                            User's Cash Code Control 
                       <%-- </p>--%>
                    </th>
                </tr>
            </thead>
            <tr>

                <td>
                    <asp:Label ID="lblSender" runat="server" CssClass="cls text" Width="190px" Height="25px" Text="User ID No. :" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="X-Large"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                    <asp:TextBox ID="txtUserId" runat="server" CssClass="cls text" Width="100px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="X-Large" AutoPostBack="true" ToolTip="Enter Ids" OnTextChanged="txtUserId_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlUserId" runat="server" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="300px" Height="30px" Font-Size="X-Large" AutoPostBack ="True" onselectedindexchanged="ddlUserId_SelectedIndexChanged">
                    </asp:DropDownList>
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
    <div id="DivGridView" runat="server" align="center" visible="False" style="height: 320px; overflow: auto;">
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
    </br>
    <div id="Div1" runat="server" align="center" visible="False" style="height: 100px; overflow: auto;">
        <table class="style1">
            <tbody>
                <tr>
                    <td>
                        <asp:GridView ID="gvModule1" runat="server">
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

       <div id="DivButton" align="center" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Add"
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
