<%@ Page Title="SalaryControlMaintenance" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HRSalaryControlMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HRSalaryControlMaintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

    </script>
    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 186px;
            width: 540px;
            margin: 0 auto;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            width: 519px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th>Allowance Information
                    </th>
                </tr>
            </thead>
        </table>
    </div>
    <div id="DivGridViewAllowence" runat="server" bordercolor="#1293D1" borderstyle="Ridge" align="center"
        visible="False" class="grid_scroll">

        <asp:GridView ID="gvInformationAllowence" runat="server" OnRowDataBound="gvInformationAllowence_RowDataBound" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                <asp:TemplateField HeaderText="Select" HeaderStyle-Width="107px" ItemStyle-Width="107px">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText=" Code" DataField="Code" HeaderStyle-Width="107px" ItemStyle-Width="107px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Description" DataField="Description" HeaderStyle-Width="295px" ItemStyle-Width="292px" ItemStyle-HorizontalAlign="left" />

            </Columns>
        </asp:GridView>


    </div>
    <br />
    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th>Deduction Information
                    </th>
                </tr>
            </thead>
        </table>
    </div>

    <div id="DivGridViewDidaction" runat="server" bordercolor="#1293D1" borderstyle="Ridge" align="center"
        visible="False" class="grid_scroll">


        <asp:GridView ID="gvInformationDidaction" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvInformationDidaction_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                <asp:TemplateField HeaderText="Select" HeaderStyle-Width="107px" ItemStyle-Width="107px">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText=" Code" DataField="Code" HeaderStyle-Width="107px" ItemStyle-Width="107px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Description" DataField="Description" HeaderStyle-Width="295px" ItemStyle-Width="292px" ItemStyle-HorizontalAlign="left" />

            </Columns>
        </asp:GridView>


    </div>

    <div id="Div1" runat="server" bordercolor="#1293D1" borderstyle="Ridge" align="center"
        visible="False">
        <table class="style1">
            <thead>
                <tr>
                    <th>Deduction Information
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" BorderColor="#1293D1" BorderStyle="Ridge" Width="483px">
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

    <div id="button" runat="server" align="center">

        <table>
            <tr>
                <td>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button green size-80" OnClientClick="return ValidationBeforeUpdate()" OnClick="btnUpdate_Click" />

                    <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="button red size-80" OnClick="btnExit_Click" />
                </td>

            </tr>
        </table>

    </div>



</asp:Content>
