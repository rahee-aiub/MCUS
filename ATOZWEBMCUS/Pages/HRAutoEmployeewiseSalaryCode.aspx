<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HRAutoEmployeewiseSalaryCode.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HRAutoEmployeewiseSalaryCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/styletext.css" rel="stylesheet" />

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 250px;
            width: 400px;
            margin: 0 auto;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }

        .auto-style3 {
            width: 290px;
        }

        .auto-style4 {
            height: 219px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />
    
    <div align="center">
        <table>
            <tr>
                <td class="auto-style4">
                    <div id="DivAllowance" runat="server">
                        <table>
                            <tr>
                                <th>
                                    <p align="center" style="color: darkorange">
                                        Allowance Information
                                    </p>
                                </th>
                            </tr>
                        </table>
                    </div>
                    <div class="grid_scroll">
                        <asp:GridView ID="gvAllowanceInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="DarkOrange"
                            AutoGenerateColumns="False" RowStyle-Height="10px" OnRowDataBound="gvAllowanceInfo_RowDataBound" EnableModelValidation="True">

                            <HeaderStyle BackColor="YellowGreen" CssClass="FixedHeader"></HeaderStyle>



                            <Columns>
                                <asp:TemplateField HeaderText="Allowance Head." HeaderStyle-Width="90px" ItemStyle-Width="90px" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAllowanceHead" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle Width="90px"></HeaderStyle>

                                    <ItemStyle Width="90px"></ItemStyle>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-Width="300px" ItemStyle-Width="300px" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"/>
                                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk1Status" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                <td class="auto-style4">
                    <div id="divDeduction" runat="server">
                        <table>
                            <tr>
                                <th>
                                    <p align="center" style="color: darkblue; width: 236px;">
                                        Deduction Information
                                    </p>
                                </th>
                            </tr>
                        </table>
                    </div>
                    <div class="grid_scroll">
                        <asp:GridView ID="gvDeductionInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="darkblue"
                            AutoGenerateColumns="False" RowStyle-Height="10px" OnRowDataBound="gvDeductionInfo_RowDataBound" EnableModelValidation="True">

                            <HeaderStyle BackColor="YellowGreen" CssClass="FixedHeader"></HeaderStyle>


                            <Columns>
                                <asp:TemplateField HeaderText="Deduction Head." HeaderStyle-Width="90px" ItemStyle-Width="90px" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeductionHead" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-Width="300px" ItemStyle-Width="300px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk2Status" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>

    </div>
    <br />
    <

    <asp:Label ID="CtrlDesignation" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlGrade" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblEmpCode" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>

