<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HRSalaryCertificateControl.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HRSalaryCertificateControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            width: 3px;
        }
    </style>


    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 400px;
            margin: 0 auto;
            width: 600px;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }
    </style>

    <script language="javascript" type="text/javascript">

        function UIFieldValidation() {

            var ddlUserId = document.getElementById('<%=ddlIdsNo.ClientID%>');

            if (ddlUserId.selectedIndex == 0)
                alert('Please Select User Id.');

            else
                return confirm('Are You Sure Want To Add Module?');
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
                    <th colspan="3" style="color: Black" aligne="center">
                        <p align="center">
                            Salary Certificate Control
                        </p>
                    </th>
                </tr>
            </thead>
            <tr>
                <th>ID No.
                </th>
                <td class="auto-style2">:
                </td>

                <td>
                    <asp:TextBox ID="txtIdsNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="X-Large" AutoPostBack="true" ToolTip="Enter Ids" OnTextChanged="txtIdsNo_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlIdsNo" runat="server" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="316px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddlIdsNo_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>


            </tr>
        </table>
    </div>

    <div align="center" class="grid_scroll">
        <asp:GridView ID="gvCertiDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvCertiDetailInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>

                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Emp. No." HeaderStyle-Width="90px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblEmpCode" runat="server" Text='<%# Eval("EmpCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Emp. Name" HeaderStyle-Width="300px" ItemStyle-Width="300px" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("EmpName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>

        </asp:GridView>


    </div>
    <div id="DivButton" align="center" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Save" OnClientClick="return UIFieldValidation()"
                        CssClass="button green size-120" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="button red size-120" OnClick="btnExit_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="lblEmpNo" runat="server" Visible="false"></asp:Label>
</asp:Content>
