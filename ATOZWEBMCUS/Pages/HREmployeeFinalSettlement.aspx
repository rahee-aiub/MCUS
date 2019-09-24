<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true"
    CodeBehind="HREmployeeFinalSettlement.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HREmployeeFinalSettlement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to Proceed?');
        }
        function ValidationBeforeReverse() {
            return confirm('Are you sure you want to Reverse?');
        }

    </script>

    <link href="../Styles/styletext.css" rel="stylesheet" />

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 380px;
            width: 450px;
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

                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="24px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />

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

                    <div class="grid_scroll">
                        <asp:GridView ID="gvBenefitInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="DarkOrange"
                            AutoGenerateColumns="False" RowStyle-Height="10px" OnRowDataBound="gvBenefitInfo_RowDataBound"
                            EnableModelValidation="True" DataKeyNames="ID">

                            <HeaderStyle BackColor="YellowGreen" CssClass="FixedHeader"></HeaderStyle>

                            <Columns>
                                <asp:TemplateField HeaderText="ID" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="BlblID" runat="server" Text='<%# Eval("ID") %>' Width="70px" Visible="false"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Account Head" HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="BlblAccountHead" runat="server" Text='<%# Eval("AccountHead") %>' Width="200px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="BtxtAmount" runat="server" Text='<%# Eval("Amount","{0:n}") %>' Width="95px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Debit GL Code" HeaderStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="BtxtGLCodeDr" runat="server" Text='<%# Eval("GLCodeDr") %>' Width="115px" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                        <asp:Label ID="lblTotalBenefit" runat="server" Text="Total Benefit:" ForeColor="Blue" Width="200px" Visible="false" />

                        <asp:TextBox ID="txtTotalBenefit" runat="server" ForeColor="Blue" Width="100px" Visible="false" ReadOnly="true"></asp:TextBox>

                    </div>

                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                <td class="auto-style4">

                    <div class="grid_scroll">
                        <asp:GridView ID="gvDeductionInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="darkblue"
                            AutoGenerateColumns="False" RowStyle-Height="10px" OnRowDataBound="gvDeductionInfo_RowDataBound"
                            EnableModelValidation="True" DataKeyNames="ID">

                            <HeaderStyle BackColor="YellowGreen" CssClass="FixedHeader"></HeaderStyle>

                            <Columns>
                                <asp:TemplateField HeaderText="ID" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="DlblID" runat="server" Text='<%# Eval("ID") %>' Width="70px" Visible="false"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Account Head" HeaderStyle-Width="200px" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="DlblAccountHead" runat="server" Text='<%# Eval("AccountHead") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="120px" ItemStyle-Width="120px">

                                    <ItemTemplate>
                                        <asp:TextBox ID="DtxtAmount" runat="server" Text='<%# Eval("Amount","{0:n}") %>' Width="120px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Credit GL Code" HeaderStyle-Width="120px" ItemStyle-Width="120px">

                                    <ItemTemplate>
                                        <asp:TextBox ID="DtxtGLCodeDr" runat="server" Text='<%# Eval("GLCodeCr") %>' Width="115px" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>

                        <asp:Label ID="lblTotalDeduction" runat="server" Text="Total Deduction:" ForeColor="Red" Width="190px" Visible="false" />

                        <asp:TextBox ID="txtTotalDeduction" runat="server" ForeColor="Red" Width="125px" Visible="false" ReadOnly="true"></asp:TextBox>

                    </div>
                </td>
            </tr>
        </table>

    </div>

    <div align="center">

        <table>
            <tr>
                <td>
                    <asp:Label ID="lblVchNo" runat="server" Text="Voucher No. :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVchNo" runat="server" BorderColor="#1293D1"
                        Width="100px" BorderStyle="Ridge" Font-Size="medium" AutoPostBack="true" OnTextChanged="txtVchNo_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    
                </td>
                <td>
                    <asp:Button ID="BtnAutoPost" runat="server" Text="Post Settlement" Font-Bold="True" Font-Size="Medium"
                        ForeColor="White" CssClass="button green" Height="22px" OnClientClick="return ValidationBeforeSave()" OnClick="BtnPost_Click" />

                    <asp:Button ID="BtnAutoReverse" runat="server" Text="Reverse Settlement" Font-Bold="True" Font-Size="Medium"
                        ForeColor="White" CssClass="button red" Height="22px" OnClientClick="return ValidationBeforeReverse()" OnClick="BtnAutoReverse_Click" />
                </td>

            </tr>

        </table>

        <table>

            <tr>

                <td>
                    <asp:Label ID="lblNetPay" runat="server" Text="Net Payable :" ForeColor="Purple" Font-Size="Medium" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtNetPay" runat="server" ForeColor="Purple" Width="100px" Font-Size="Medium" Visible="false" ReadOnly="true"></asp:TextBox>
                </td>

                <td>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update Data" Font-Bold="True" Font-Size="Medium"
                        ForeColor="White" CssClass="button blue" Height="22px" OnClick="btnUpdate_Click" />
                </td>



            </tr>


        </table>

    </div>

    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlDesignation" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlGrade" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblErrMsg" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlProcDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblRevFlag" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>

