<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSLoanApplication.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSLoanApplication" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

    </script>
    <%-- <script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />--%>


    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtLoanAppDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtLoanAppDate.ClientID %>").datepicker();

            });

        });
        $(function () {
            $("#<%= txtLoanExpDate.ClientID %>").datepicker();

             var prm = Sys.WebForms.PageRequestManager.getInstance();

             prm.add_endRequest(function () {
                 $("#<%= txtLoanExpDate.ClientID %>").datepicker();

            });

         });
    </script>


    <%--<script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtLoanAppDate.ClientID %>").dynDateTime({
                showsTime: false,
                ifFormat: "%d/%m/%Y",
                daFormat: "%l;%M %p, %e %m, %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });

        $(document).ready(function () {
            $("#<%=txtLoanExpDate.ClientID %>").dynDateTime({
                showsTime: false,
                ifFormat: "%d/%m/%Y",
                daFormat: "%l;%M %p, %e %m, %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });


    </script>--%>
    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 136px;
            margin: 0 auto;
            width: 791px;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            /*width: 750px;*/
            font-weight: bold;
        }

        .FixedHeader2 {
            position: absolute;
            /*//width: 415px;*/
            font-weight: bold;
        }


        .FixedHeader3 {
            position: absolute;
            /*width: 414px;*/
            font-weight: bold;
        }

        .auto-style1 {
            height: 35px;
        }

        .auto-style2 {
            width: 203px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <table>
        <tr>
            <td class="auto-style1">
                <asp:Button ID="BtnLoanApplication" runat="server" Text="Loan Application" Style="background-color: Silver"
                    Width="212px" OnClick="BtnLoanApplication_Click" Height="31px" />
                <asp:Button ID="BtnDeposit" Style="background-color: InactiveCaption" runat="server"
                    Text="Deposit Guarantor" Width="212px" OnClick="BtnDeposit_Click" Height="30px" />
                <asp:Button ID="BtnShare" runat="server" Text="Share Guarantor" Style="background-color: ActiveCaption"
                    Width="212px" OnClick="BtnShare_Click" Height="30px" />
                <asp:Button ID="BtnProperty" runat="server" Text="Property Guarantor" Style="background-color: linen"
                    Width="212px" OnClick="BtnProperty_Click" Height="30px" />
            </td>
        </tr>
    </table>
    <div style="background-color: Silver; border: 1px" align="center">
        <asp:Panel ID="pnlLoanApplication" runat="server" Width="976px">


            <table class="style1">

                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lblLoanAppDate" runat="server" Text=" Application Date:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLoanAppDate" runat="server" CssClass="cls text" Width="115px" Height="25px"
                            Font-Size="Large" ToolTip="Enter Date" AutoPostBack="True" OnTextChanged="txtLoanAppDate_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label3" runat="server" Font-Size="Large" Width="115px" Height="25px" ForeColor="Red" Text="Last Application No. :"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblLastAppNo" runat="server" Font-Size="Large" Width="115px" Height="25px" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lblLoanAppType" runat="server" Text="Loan A/C Type:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAccType" runat="server" Height="25px"
                            Width="199px" AutoPostBack="True" CssClass="cls text"
                            Font-Size="Large" OnSelectedIndexChanged="ddlAccType_SelectedIndexChanged">
                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtHidden" runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Visible="false" Width="115px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lblCUNo" runat="server" Text="Credit Union No:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCreditUNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Large" AutoPostBack="true" TabIndex="1" ToolTip="Enter Code" OnTextChanged="txtCreditUNo_TextChanged"></asp:TextBox>
                        <asp:Label ID="lblCuName" runat="server" Text=""></asp:Label>
                        <asp:Button ID="BtnHelp" runat="server" Text="Help" Font-Size="Medium" ForeColor="Red"
                            Font-Bold="True" CssClass="button green" OnClick="BtnHelp_Click" />
                    </td>
                </tr>


                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lblLoanMemNo" runat="server" Text="Depositor No:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLoanMemNo" runat="server" CssClass="cls text" Width="115px" AutoPostBack="true"
                            Height="25px" Font-Size="Large" TabIndex="2" OnTextChanged="txtLoanMemNo_TextChanged"></asp:TextBox>
                        <asp:Label ID="lblMemName" runat="server" Height="25px" Text=""></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lblLoanAppAmount" runat="server" Text=" Application Amount:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLoanAppAmount" runat="server" CssClass="cls text" Width="199px"
                            Height="25px" Font-Size="Large" TabIndex="3" AutoPostBack="True" OnTextChanged="txtLoanAppAmount_TextChanged" onFocus="javascript:this.select();" onkeypress="return IsDecimalKey(event)"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                   
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lblLoanInterestRate" runat="server" Text=" Interest Rate:" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>

                        <asp:TextBox ID="txtLoanInterestRate" runat="server" CssClass="cls text" Width="113px"
                            Height="25px" TabIndex="4" Font-Size="Large" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                        &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        
                         <asp:Label ID="lblNoInstallment" runat="server" Text="No Of Installment:" Font-Size="Large"
                             ForeColor="Red"></asp:Label>
                        &nbsp;
                        <asp:TextBox ID="txtNoInstallment" runat="server" CssClass="cls text"
                            Width="100px" Height="25px"
                            Font-Size="Large" AutoPostBack="True" TabIndex="5"
                            OnTextChanged="txtNoInstallment_TextChanged" onkeypress="return IsDecimalKey(event)"></asp:TextBox>


                        <%--<asp:Label ID="lblLoanGracePeriod" runat="server" Font-Size="Large" ForeColor="Red" Text=" Grace Period:"></asp:Label>
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                         <asp:TextBox ID="txtLoanGracePeriod" runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="100px"></asp:TextBox>--%>
                  
                   
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lblLoanCategory" runat="server" Font-Size="Large" ForeColor="Red" Text="Loan Category:"></asp:Label>
                    </td>
                    <td>&nbsp;<asp:DropDownList ID="ddlLoanCategory" runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="150px">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">General</asp:ListItem>
                        <asp:ListItem Value="2">Emergency</asp:ListItem>
                    </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
                        <asp:Label ID="lblLoanInstallmentAmount" runat="server" Font-Size="Large" ForeColor="Red" Text=" Installment Amount:"></asp:Label>&nbsp;<asp:TextBox ID="txtLoanInstallmentAmount" runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="170px" AutoPostBack="True" OnTextChanged="txtLoanInstallmentAmount_TextChanged" onFocus="javascript:this.select();" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                    </td>
                </tr>



                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lblSuretyMemNo" runat="server" Font-Size="Large" ForeColor="Red" Text="Loan Surety Member No:"></asp:Label>
                    </td>
                    <td>&nbsp;<asp:TextBox ID="txtSuretyMemNo" runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="100px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblLoanLastInstlAmount" runat="server" Font-Size="Large" ForeColor="Red" Text="Last Installment Amount:"></asp:Label>&nbsp;<asp:TextBox ID="txtLoanLastInstlAmount" runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="170px" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lblLoanPurpose" runat="server" Font-Size="Large" ForeColor="Red" Text="Loan Purpose:"></asp:Label>
                    </td>
                    <td><asp:DropDownList ID="ddlLoanPurpose" runat="server" CssClass="cls text" Font-Size="Large" Height="26px" Width="293px">
                    </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblLoanStatDate" runat="server" Font-Size="Large" ForeColor="Red" Text="Loan Expire Date:"></asp:Label>
                        <asp:TextBox ID="txtLoanExpDate" runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="115px"></asp:TextBox>
                        <%--<asp:Label ID="lblLoanFirstInstlDate" runat="server" Font-Size="Large" ForeColor="Red" Text="First Installment Date:"></asp:Label>
                        <asp:TextBox ID="txtLoanFirstInstlDate" runat="server" CssClass="cls text" Font-Size="Large" Height="25px"  Width="115px" img src="../Images/calender.png" AutoPostBack="True" OnTextChanged="txtLoanFirstInstlDate_TextChanged"></asp:TextBox>--%>
                        
                    </td>
                </tr>

                <tr>
                    <td>

                        <asp:Label ID="lblODPeriod" runat="server" Text="Period (Month) :" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                        </td>
                    <td>
                        
                        <asp:TextBox ID="txtODPeriod" Enabled="false" runat="server" CssClass="cls text"
                            Width="100px" Height="25px"
                            Font-Size="Large" TabIndex="5"
                            onkeypress="return IsDecimalKey(event)" AutoPostBack="true" OnTextChanged="txtODPeriod_TextChanged"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>

                        <asp:Label ID="lblODExpiryDate" runat="server" Text="Loan Expiry Date :" Font-Size="Large"
                            ForeColor="Red"></asp:Label>
                        </td>
                    <td>
                        
                        <asp:TextBox ID="txtODExpDate" runat="server" Enabled="false" CssClass="cls text" Font-Size="Large" Height="25px" Width="115px"></asp:TextBox>
                    </td>
                </tr>

                <tr>

                    <td class="auto-style2"></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <%--<img src="../Images/calender.png" />--%></td>

                </tr>

                <tr>

                    <td class="auto-style2">
                        <asp:Label ID="lblLoanFees" runat="server" Font-Size="Large" ForeColor="Red" Text="Loan Processing Fees :"></asp:Label>
                    </td>
                    <td>&nbsp;<asp:TextBox ID="txtLoanFees" runat="server" CssClass="cls text" MaxLength="5" Font-Size="Large" Height="25px" Width="100px" onkeypress="return IsDecimalKey(event)"></asp:TextBox>

                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblVchNo" runat="server" Font-Size="Large" ForeColor="Red" Text="Voucher No. :"></asp:Label>
                        &nbsp;
                        <asp:TextBox ID="txtVchNo" runat="server" CssClass="cls text" Font-Size="Large" Height="25px" Width="100px"></asp:TextBox>


                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>


                <tr>
                    <td class="auto-style2"></td>
                    <td>
                        <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Font-Size="Large" Height="27px" Width="120px"
                            ForeColor="White" Font-Bold="True" ToolTip="Insert Information" CssClass="button green"
                            OnClientClick="return ValidationBeforeSave()" OnClick="BtnSubmit_Click" />&nbsp;

                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red" OnClick="BtnExit_Click" />

                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div style="border-style: none; border-color: inherit; border-width: 1px; background-color: InactiveCaption" align="center">
        <asp:Panel ID="pnlDeposit" runat="server" Width="976px" Height="395px">
            <table class="style1">
                <tr>
                    <td>

                        <br />
                        <br />
                        <br />
                        <asp:Button ID="BtnSearch" runat="server" Text="Help" Width="96px" Height="28px" Font-Size="Large" ForeColor="Red"
                            Font-Bold="True" CssClass="button green" OnClick="BtnSearch_Click" />
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="lblCrUNo" runat="server" Text="CuNo" ForeColor="Red"></asp:Label></h3>

                        <asp:TextBox ID="txtCrUNo" runat="server" Width="96px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="1" OnTextChanged="txtCrUNo_TextChanged" AutoPostBack="true"></asp:TextBox>

                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="lblMemNo" runat="server" Text="Depositor" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtDepositMemNo" runat="server" Width="96px" Height="25px" Font-Size="Large"
                            CssClass="cls text" AutoPostBack="true" TabIndex="2" OnTextChanged="DepositMemNo_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="lblAccType" runat="server" Text="A/c Type" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtAccType" runat="server" Width="60px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="3" AutoPostBack="true" OnTextChanged="txtAccType_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="lblAccNo" runat="server" Text=" A/c No" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtAccNo" runat="server" Width="170px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="4" OnTextChanged="txtAccNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="lblLionAmt" runat="server" Text="Lien Amount" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtLionAmt" runat="server" Width="140px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="5" AutoPostBack="True" OnTextChanged="txtLionAmt_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="lblTotalLienAmt" runat="server" Text="Total Lien Amount" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtTotalLienAmt" runat="server" Width="140px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="6" AutoPostBack="True"></asp:TextBox>
                    </td>
                    <td></td>
                    <td>
                        <h3>
                            <asp:Label ID="lblLedgerBalance" runat="server" Text="Ledger Balance" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtLedgerBalance" runat="server" Width="140px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="7"></asp:TextBox>
                    </td>
                    <td>
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="BtnAddDeposit" runat="server" Text="Add" Width="91px" Font-Size="Large"
                            ForeColor="White" Height="25px" Font-Bold="True"
                            CssClass="button green" OnClick="BtnAddDeposit_Click" />
                    </td>

                </tr>
            </table>
            <table style="align-content: center">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Cu Name:" Font-Size="Large" ForeColor="Red"></asp:Label>

                    </td>
                    <td>
                        <asp:Label ID="lblCrName" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Depositor Name:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblMemberName" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Account Type:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAccountTypeName" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>

            <div align="center" class="grid_scroll">
                <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader2" HeaderStyle-BackColor="YellowGreen"
                    AutoGenerateColumns="False" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvDetailInfo_RowDataBound" EnableModelValidation="false" OnRowDeleting="gvDetailInfo_RowDeleting">
                    <HeaderStyle BackColor="YellowGreen" CssClass="FixedHeader2" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="CuNo" HeaderText="CU No">
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MemNo" HeaderText="Depositor No">
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AccType" HeaderText="A/C Type">
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AccNo" HeaderText="Acc No">
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AccAmount" DataFormatString="{0:0,0.00}" HeaderText="Amount">
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                        </asp:BoundField>
                        <asp:CommandField ShowDeleteButton="True" HeaderStyle-Width="120px" ItemStyle-Width="120px">
                            <ControlStyle Font-Bold="True" ForeColor="#FF6600" />
                        </asp:CommandField>
                    </Columns>

                </asp:GridView>
            </div>
            <div align="right" style="width: 695px">

                <asp:Label ID="lblTotalAmt" runat="server" Text="Total Lien:" Font-Size="Medium" ForeColor="Red"></asp:Label>

                <asp:Label ID="txtTotalAmt" runat="server" Font-Size="Medium" ForeColor="Red"></asp:Label>
            </div>
            <asp:Label ID="lblDepositCuType" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblDepositCuNo" runat="server" Visible="false"></asp:Label>
        </asp:Panel>
    </div>
    <div style="background-color: ActiveCaption; border: 1px" align="center">
        <asp:Panel ID="pnlShare" runat="server" Height="395px">
            <table class="style1">
                <tr>
                    <td>

                        <br />
                        <br />
                        <br />
                        <asp:Button ID="btnSSearch" runat="server" Text="Help" Width="96px" Height="28px" Font-Size="Large" ForeColor="Red"
                            Font-Bold="True" CssClass="button green" OnClick="btnSSearch_Click" />
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="Label5" runat="server" Text="CuNo" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtShareCuNo" runat="server" Width="96px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="1" AutoPostBack="true" OnTextChanged="txtShareCuNo_TextChanged"></asp:TextBox>

                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="Label6" runat="server" Text="Cu Name " ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtShareCuName" runat="server" Width="504px" Height="25px" Font-Size="Large" Enabled="false"
                            CssClass="cls text" TabIndex="2" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="Label7" runat="server" Text="A/c Type" ForeColor="Red" Visible="false"></asp:Label></h3>
                        <asp:TextBox ID="txtShareAccType" runat="server" Width="140px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="3" Visible="false"></asp:TextBox>
                    </td>


                    <td>
                        <h3>
                            <asp:Label ID="Label9" runat="server" Text="Share Amount" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtShareAmount" runat="server" Width="140px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="4"></asp:TextBox>
                    </td>

                    <td>
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="BtnAddShare" runat="server" Text="Add" Width="91px" Font-Size="Large"
                            ForeColor="White" Height="25px" Font-Bold="True"
                            CssClass="button green" OnClick="BtnAddShare_Click" />
                    </td>

                </tr>
            </table>

            <br />
            <br />
            <div align="center" class="grid_scroll">
                <asp:GridView ID="gvShareDetails" runat="server" HeaderStyle-CssClass="FixedHeader3" HeaderStyle-BackColor="YellowGreen"
                    AutoGenerateColumns="False" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvShareDetails_RowDataBound" EnableModelValidation="True" OnRowDeleting="gvShareDetails_RowDeleting">
                    <HeaderStyle BackColor="YellowGreen" CssClass="FixedHeader3" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CuNo" HeaderText="CU No">
                            <HeaderStyle Width="180px" />
                            <ItemStyle HorizontalAlign="Center" Width="180px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MemNo" HeaderText="Depositor No">
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AccType" HeaderText="A/C Type">
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="AccNo" HeaderText="Acc No">
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="AccAmount" DataFormatString="{0:0,0.00}" HeaderText="Amount">
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                        </asp:BoundField>
                        <asp:CommandField ShowDeleteButton="True" HeaderStyle-Width="180px" ItemStyle-Width="180px">
                            <ControlStyle Font-Bold="True" ForeColor="#FF6600" />
                        </asp:CommandField>
                    </Columns>

                </asp:GridView>
            </div>
            <div align="center" style="width: 600px">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lbltotalshare" runat="server" Text="Total Share :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                <asp:Label ID="lblShareTotalAmt" runat="server" Font-Size="Medium" ForeColor="Red"></asp:Label>
            </div>
            <asp:Label ID="lblShareCType" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblShareCNo" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblStatus" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblApplicationNo" runat="server" Visible="false"></asp:Label>
        </asp:Panel>
    </div>
    <div style="background-color: linen; border: 1px" align="center">
        <asp:Panel ID="pnlProperty" runat="server" Height="395px">
            <table class="style1">
                <tr>
                    <td>
                        <h3>
                            <asp:Label ID="Label8" runat="server" Text="SL No." ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtSerialNo" runat="server" Width="96px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="1" AutoPostBack="True" OnTextChanged="txtSerialNo_TextChanged"></asp:TextBox>

                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="Label10" runat="server" Text="Name of Property " ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtNameProperty" runat="server" Width="196px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="2" AutoPostBack="True" OnTextChanged="txtNameProperty_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="Label11" runat="server" Text="File No" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtFileNo" runat="server" Width="140px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="3" AutoPostBack="True" OnTextChanged="txtFileNo_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="Label12" runat="server" Text="Description" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtDescription" runat="server" Width="227px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="4" AutoPostBack="True" OnTextChanged="txtDescription_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <h3>
                            <asp:Label ID="Label13" runat="server" Text="Amount" ForeColor="Red"></asp:Label></h3>
                        <asp:TextBox ID="txtProprertyAmt" runat="server" Width="140px" Height="25px" Font-Size="Large"
                            CssClass="cls text" TabIndex="5" onchange="javascript:this.value=Comma(this.value);"></asp:TextBox>
                    </td>

                    <td>
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="BtnAddProperty" runat="server" Text="Add" Width="91px" Font-Size="Large"
                            ForeColor="White" Height="25px" Font-Bold="True"
                            CssClass="button green" OnClick="BtnAddProperty_Click" />
                    </td>

                </tr>
            </table>
            <br />
            <br />
            <div align="center" class="grid_scroll">
                <asp:GridView ID="gvPropertyDetails" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
                    AutoGenerateColumns="False" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvPropertyDetails_RowDataBound" EnableModelValidation="True" OnRowDeleting="gvPropertyDetails_RowDeleting">
                    <HeaderStyle BackColor="YellowGreen" CssClass="FixedHeader" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PrSRL" HeaderText="Srl.No">
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Center" Width="125px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PrName" HeaderText="Property Name">
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Center" Width="125px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FileNo" HeaderText="File No">
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Center" Width="125px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PrDesc" HeaderText="Description">
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Center" Width="125px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PrAmount" DataFormatString="{0:0,0.00}" HeaderText="Amount">
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Right" Width="125px" />
                        </asp:BoundField>
                        <asp:CommandField ShowDeleteButton="True" HeaderStyle-Width="120px" ItemStyle-Width="120px">
                            <ControlStyle Font-Bold="True" ForeColor="#FF3300" />
                        </asp:CommandField>
                    </Columns>


                </asp:GridView>
            </div>

            <div align="right" style="width: 685px">

                <asp:Label ID="lblTotalProprty" runat="server" Font-Size="Medium" ForeColor="Red" Text="Total Property :"></asp:Label>
                <asp:Label ID="lblSumProperty" runat="server" Font-Size="Medium" ForeColor="Red"></asp:Label>
            </div>
        </asp:Panel>
    </div>
    <br />

    <div align="center" style="width: 685px">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblTotalGar" runat="server" Font-Size="X-Large" ForeColor="Red" Text="Total Guaranty :"></asp:Label>
        <asp:Label ID="lblTotalResult" runat="server" Font-Size="X-Large" ForeColor="Red"></asp:Label>
    </div>
    <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCu" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuTypeName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblNewAppNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTypeCls" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblModule" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccTypeMode" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnIDName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCuNumber" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblProcDate" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblBoothNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblBoothName" runat="server" Text="" Visible="false"></asp:Label>



    <asp:Label ID="CtrlVoucherNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlBackUpStat" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblTrnTypeTitle" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblFuncTitle" runat="server" Text="" Visible="false"></asp:Label>


    <asp:Label ID="lblAccFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="MsgFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblchk1Hide" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblchk2Hide" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblchk3Hide" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnAccNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblNFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblPCtrl" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccTypeGuaranty" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblIAccTypeGuaranty" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="SPflag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlMsgFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:HiddenField ID="hAccType" runat="server" />
    <asp:HiddenField ID="hInstallmentAmt" runat="server" />

</asp:Content>
