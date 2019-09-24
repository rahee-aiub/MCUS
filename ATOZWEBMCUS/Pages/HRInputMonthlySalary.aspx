<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HREditMonthlySalary.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HREditMonthlySalary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/styletext.css" rel="stylesheet" />

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 190px;
            width: 440px;
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
    
    <div align="center">
        <table class="style1">

            <tr>
                
                <td>
                    <asp:Label ID="lnlAccType" runat="server" Text="Process Month :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                   
                    <asp:TextBox ID="txtToDaysDate" runat="server" Enabled="False" BorderColor="#1293D1"
                        Width="302px" BorderStyle="Ridge" Font-Size="X-Large"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    
                </td>
            </tr>




            <tr>
                <td>
                    <asp:Label ID="lblEmpNo" runat="server" Text="Employee No  :" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtEmpNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSumbit" runat="server" Text="Submit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="24px" Width="86px" Font-Bold="True" ToolTip="Submit" CausesValidation="False"
                        CssClass="button green" OnClick="btnSumbit_Click" />


                </td>
                <%--<td>
                    <asp:TextBox ID="txtEmpNo" runat="server" CssClass="textbox" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" AutoPostBack="true" OnTextChanged="txtEmpNo_TextChanged"></asp:TextBox>

                </td>--%>
            </tr>
        </table>

        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Name  :" Font-Size="Large" ForeColor="Red"></asp:Label>
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
                    <asp:Label ID="Label2" runat="server" Text="Designation  :" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblDesign" runat="server" Text="" Font-Size="Large"></asp:Label>

                </td>
                <%--<td>
                    <asp:Label ID="lblDesign" runat="server" Text="" Font-Size="Large"></asp:Label>
                    <br />

                </td>--%>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Grade  :" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblGrade" runat="server" Text="" Font-Size="Large"></asp:Label>

                </td>
                <%-- <td>
                    <asp:Label ID="lblGrade" runat="server" Text="" Font-Size="Large"></asp:Label>

                </td>--%>
            </tr>
        </table>
    </div>
    <br />
    <div>
        <table>
            
            <tr>
                <td>
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label8" runat="server" Text="Basic :" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblBasic" runat="server" Text="" Font-Size="X-Large"></asp:Label>
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label9" runat="server" Text="Consolidated :" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblConsolidated" runat="server" Text="" Font-Size="X-Large"></asp:Label>
                </td>
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
                        <asp:GridView ID="gvAllowanceInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" RowStyle-BackColor="WhiteSmoke"
                            AutoGenerateColumns="False" RowStyle-Height="10px" OnRowDataBound="gvAllowanceInfo_RowDataBound" EnableModelValidation="True" OnRowUpdating="gvAllowanceInfo_RowUpdating" OnRowCancelingEdit="gvAllowanceInfo_RowCancelingEdit" OnRowEditing="gvAllowanceInfo_RowEditing">

                            <HeaderStyle BackColor="YellowGreen" CssClass="FixedHeader"></HeaderStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Allowance Head." HeaderStyle-Width="90px" ItemStyle-Width="90px" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAllowanceHead" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                                    </ItemTemplate>


                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description." HeaderStyle-Width="195px" ItemStyle-Width="250px">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="160px" ItemStyle-Width="90px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAllowanceAmt" runat="server" Font-Bold="true" Enabled="false" Text-align="right"></asp:TextBox>
                                    </ItemTemplate>

                                </asp:TemplateField>


                                <asp:CommandField HeaderText="Modify" ShowEditButton="True" ShowHeader="True" HeaderStyle-Width="75px" ItemStyle-Width="95px"></asp:CommandField>

                            </Columns>

                            <RowStyle Height="10px"></RowStyle>
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
                        <asp:GridView ID="gvDeductionInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" RowStyle-BackColor="WhiteSmoke"
                            AutoGenerateColumns="False" RowStyle-Height="10px" OnRowDataBound="gvDeductionInfo_RowDataBound" EnableModelValidation="True" OnRowCancelingEdit="gvDeductionInfo_RowCancelingEdit" OnRowEditing="gvDeductionInfo_RowEditing" OnRowUpdating="gvDeductionInfo_RowUpdating">

                            <HeaderStyle BackColor="YellowGreen" CssClass="FixedHeader"></HeaderStyle>


                            <Columns>
                                <asp:TemplateField HeaderText="Deduction Head." HeaderStyle-Width="90px" ItemStyle-Width="90px" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeductionHead" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description." HeaderStyle-Width="180px" ItemStyle-Width="250px">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="160px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDeductionAmt" runat="server" Font-Bold="true" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>


                                </asp:TemplateField>

                                <asp:CommandField HeaderText="Modify" ShowEditButton="True" ShowHeader="True" HeaderStyle-Width="75px" ItemStyle-Width="95px"></asp:CommandField>

                            </Columns>

                            <RowStyle Height="10px"></RowStyle>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>

    </div>
    <br />

    <div>

        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label5" runat="server" Text="Gross  :" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblGross" runat="server" Text="" Font-Size="X-Large"></asp:Label>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label6" runat="server" Text="Total Ded.  :" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblTotalDed" runat="server" Text="" Font-Size="X-Large"></asp:Label>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label7" runat="server" Text="Net Pay  :" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblNetPay" runat="server" Text="" Font-Size="X-Large"></asp:Label>



            </td>
        </tr>

    </div>

    <br />
    <br />
    <div align="center">
        <%--<asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" ToolTip="Update Information" CssClass="button green" OnClientClick="return ValidationBeforeUpdate()"
            Height="22px" />&nbsp;--%>
        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
            Height="24px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
            CssClass="button red" OnClick="BtnExit_Click" />
    </div>


    <asp:Label ID="CtrlDesignation" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlGrade" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlStatus" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlBasic" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlConsolidated" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlTAAmt1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTAAmt2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTAAmt3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTAAmt4" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTAAmt5" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTAAmt6" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTAAmt7" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTAAmt8" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTAAmt9" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTAAmt10" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTAAmt11" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTAAmt12" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTAAmt13" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTAAmt14" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTAAmt15" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTAAmt16" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTAAmt17" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTAAmt18" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTAAmt19" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTAAmt20" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlTDAmt1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTDAmt2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTDAmt3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTDAmt4" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTDAmt5" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTDAmt6" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTDAmt7" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTDAmt8" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTDAmt9" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTDAmt10" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTDAmt11" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTDAmt12" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTDAmt13" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTDAmt14" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTDAmt15" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTDAmt16" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTDAmt17" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTDAmt18" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTDAmt19" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTDAmt20" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlGrossTotal" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlDeductTotal" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlNetPayment" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnPeriod" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnMonth" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnYear" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnToDaysDate" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>

