<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSApproveStaffLoanApplication.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSApproveStaffLoanApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">

        function ApproveValidation() {
            return confirm('Do you want to Approve Data?');
        }
        function SelectValidation() {
            return confirm('Do you want to Select Data?');
        }
        function RejectValidation() {
            return confirm('Do you want to Reject Data?');
        }
    </script>

    <link href="../Styles/TableStyle1.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/TableStyle2.css" rel="stylesheet" type="text/css" />



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <br />
    <div id="DivMainHeader" runat="server" align="center">

        <table class="style1">
            <tr>
                <th colspan="4">
                    <p align="center" style="color: blue">
                        Approve Staff Loan Application
                    </p>
                </th>
            </tr>
        </table>

    </div>

    <div id="DivButton" runat="server" align="center">
        <table>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                </td>
            </tr>
        </table>
    </div>

    <div id="DivGridViewCancle" runat="server" align="center" style="height: 276px; overflow: auto; width: 100%;">
        <table class="style1">
            <thead>
                <tr>
                    <th>
                        <p align="center" style="color: blue">
                            Approve - Spooler
                        </p>
                        <asp:GridView ID="gvLoanInfo" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Style="margin-top: 4px" Width="757px">
                            <Columns>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="BtnSelect" runat="server" Text="Select" OnClick="BtnSelect_Click" Width="68px" CssClass="button green" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="BtnRejectSelect" runat="server" Text="Reject" OnClick="BtnRejectSelect_Click" Width="68px" CssClass="button red" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:Button ID="BtnPrint" runat="server" Text="Print" OnClick="BtnPrint_Click" Width="60px" CssClass="button black size-100" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Staff Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMemNo" runat="server" Text='<%# Eval("MemNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Loan A/C Type" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccType" runat="server" Text='<%# Eval("AccType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Application No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblappno" runat="server" Text='<%# Eval("LoanApplicationNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="LoanApplicationDate" HeaderText="Application Date" DataFormatString="{0:dd/MM/yyyy}" />


                                <asp:BoundField DataField="LoanApplicationAmt" HeaderText="Application Amt." DataFormatString="{0:0,0.00}" ItemStyle-HorizontalAlign="Right" />

                                
                                <%--<asp:TemplateField HeaderText="Sanction Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtLoanSancAmt" runat="server" AutoPostBack="true" Text='<%# String.Format("{0:0,0.00}",  Convert.ToDouble(Eval("LoanApplicationAmt")))%>' OnTextChanged="txtLoanSancAmt_TextChanged" Font-Bold="true"></asp:TextBox>
                                    </ItemTemplate>

                                </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>


                    </th>
                </tr>
            </thead>
        </table>
    </div>

    <div id="DivReject" runat="server">
        <table class="style1">
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td>
                    <asp:Label ID="lblNote" runat="server" Font-Size="Large" ForeColor="Red"
                        Text=" Reject Note :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRejectNote"
                        runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="1095px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
               
                <td colspan="6" align="center">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnReject" runat="server" Text="Reject" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="30px" Width="120px" Font-Bold="True" CausesValidation="False"
                        CssClass="button red" OnClientClick="return RejectValidation()" OnClick="BtnReject_Click" />
                </td>
            </tr>
        </table>

    </div>

    <div id="DivSelect" runat="server">
        <table class="style1">

            <tr>

                <td>
                    <asp:Label ID="lblSancAmt" runat="server" Font-Size="Large" ForeColor="Red"
                        Text=" Sanction Amount :"></asp:Label>
                </td>
                <td>

                    <asp:TextBox ID="txtSancAmount"
                        runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="170px"
                        AutoPostBack="True" OnTextChanged="txtSancAmount_TextChanged" onFocus="javascript:this.select();"></asp:TextBox>
                </td>

                <td>
                    <asp:Label ID="lblNoInst" runat="server" Font-Size="Large" ForeColor="Red"
                        Text=" No. of Instl. :"></asp:Label>
                </td>
                <td>

                    <asp:TextBox ID="txtNoInstl"
                        runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="80px"></asp:TextBox>
                </td>

                <td>
                    <asp:Label ID="lblInstallmentAmount" runat="server" Font-Size="Large" ForeColor="Red"
                        Text=" Installment Amount :"></asp:Label>
                </td>
                <td>

                    <asp:TextBox ID="txtInstallmentAmount"
                        runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="170px"
                        AutoPostBack="True" OnTextChanged="txtInstallmentAmount_TextChanged" onFocus="javascript:this.select();"></asp:TextBox>
                </td>

                <td>
                    <asp:Label ID="lblLastInstallmentAmount" runat="server" Font-Size="Large" ForeColor="Red"
                        Text=" Last Installment Amount :"></asp:Label>&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtLastInstallmentAmount"
                        runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="170px"></asp:TextBox>
                </td>
            </tr>

        </table>
    </div>

    <div id="Div1" runat="server" align="center">
        <table>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="btnApproved" runat="server" Text="Approved" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="30px" Width="120px" Font-Bold="True" ToolTip="Approved Page" CausesValidation="False"
                        CssClass="button red" OnClientClick="return ApproveValidation()" OnClick="BtnApproved_Click" />
                </td>
            </tr>
        </table>
    </div>




    <div align="center">
        <asp:Label ID="lblmsg1" runat="server" Text="All Record Approve Successfully Completed" Font-Bold="True" Font-Size="XX-Large" ForeColor="#009933"></asp:Label><br />
        <asp:Label ID="lblmsg2" runat="server" Text="No More Record for Approve" Font-Bold="True" Font-Size="XX-Large" ForeColor="#009933"></asp:Label>
    </div>

    <asp:Label ID="lblAccTypeDesc" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAtyClass" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblAType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblIntRate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblExpDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblInstlAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblLastInstlAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblNoInstl" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblFirstInstlDt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblGrace" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="LienAmount" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblApplicationNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblModule" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblNewAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblMemNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>


    <asp:Label ID="hdnApprovedAmount" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnNoInstl" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnAppNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="SingleRec" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblLoanExpiryDt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAppDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblLastSancAmt" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>

