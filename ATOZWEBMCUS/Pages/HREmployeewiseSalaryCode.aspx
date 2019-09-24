<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HREmployeewiseSalaryCode.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HREmployeewiseSalaryCode" %>

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
        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="lblEmpNo" runat="server" Text="Employee No :" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtEmpNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSumbit" runat="server" Text="Submit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="24px" Width="86px" Font-Bold="True" ToolTip="Submit" CausesValidation="False"
                        CssClass="button blue" OnClick="btnSumbit_Click" />

                </td>
                <%--<td>
                    <asp:TextBox ID="txtEmpNo" runat="server" CssClass="textbox" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" AutoPostBack="true" OnTextChanged="txtEmpNo_TextChanged"></asp:TextBox>

                </td>--%>
            </tr>
        </table>

        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Name   :" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Label ID="lblName" runat="server" Text="" Font-Size="Large"></asp:Label>
                </td>
                <%--<td>
                    <asp:Label ID="lblName" runat="server" Text="" Font-Size="Large"></asp:Label>
                    <br />

                </td>--%>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Designation   :" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblDesign" runat="server" Text="" Font-Size="Large"></asp:Label>

                </td>
                <%--<td>
                    <asp:Label ID="lblDesign" runat="server" Text="" Font-Size="Large"></asp:Label>
                    <br />

                </td>--%>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Grade   :" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblGrade" runat="server" Text="" Font-Size="Large"></asp:Label>

                </td>
                <%--<td>
                    <asp:Label ID="lblGrade" runat="server" Text="" Font-Size="Large"></asp:Label>

                </td>--%>
            </tr>
        </table>
    </div>
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
    <div align="center">
        <asp:Button ID="BtnAutoPost" runat="server" Text="Auto Update" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" CssClass="button green" Height="22px" OnClick="BtnPost_Click" />&nbsp;
        <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" ToolTip="Update Information" CssClass="button green" OnClientClick="return ValidationBeforeUpdate()"
            Height="22px" OnClick="BtnUpdate_Click" />&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="24px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
    </div>


    <asp:Label ID="CtrlDesignation" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlGrade" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>

