<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="CSAccountEditMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSAccountEditMaintenance"
    Title="Untitled Page" %>

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
        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }
    </style>

    <%--<script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtOpenDate.ClientID %>").dynDateTime({
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
            $("#<%=txtMatrutiyDate.ClientID %>").dynDateTime({
                showsTime: false,
                ifFormat: "%m/%d/%Y",
                daFormat: "%l;%M %p, %e %m, %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });
    </script>--%>


     <script language="javascript" type="text/javascript">
         $(function () {
             $("#<%= txtOpenDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtOpenDate.ClientID %>").datepicker();

            });

        });
         
         $(function () {
             $("#<%= txtLastRenewalDt.ClientID %>").datepicker();

             var prm = Sys.WebForms.PageRequestManager.getInstance();

             prm.add_endRequest(function () {
                 $("#<%= txtLastRenewalDt.ClientID %>").datepicker();

            });

         });

         $(function () {
             $("#<%= txtAnniDate.ClientID %>").datepicker();

             var prm = Sys.WebForms.PageRequestManager.getInstance();

             prm.add_endRequest(function () {
                 $("#<%= txtAnniDate.ClientID %>").datepicker();

             });

         });

         $(function () {
             $("#<%= txtODIntDate.ClientID %>").datepicker();

             var prm = Sys.WebForms.PageRequestManager.getInstance();

             prm.add_endRequest(function () {
                 $("#<%= txtODIntDate.ClientID %>").datepicker();

             });

         });
         </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3"><asp:Label ID="lblFuncTitle" runat="server" Text=""></asp:Label>
                    </th>
                </tr>
            </thead>
            <%--  <tr>
                <td>
                    <asp:Label ID="lblBranchNo" runat="server" Text="Branch No:" Font-Size="X-Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBranchNo" runat="server"  Width="136px" Height="28px" Font-Size="X-Large" Text="1" ></asp:TextBox>
                   
                </td>
            </tr>--%>
            <tr>
                <td>
                    <asp:Label ID="lblAccountNo" runat="server" Text="Account No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccountNo" runat="server" CssClass="cls text" Width="177px" Height="25px"
                        Font-Size="Large" OnTextChanged="txtAccountNo_TextChanged" AutoPostBack="true"></asp:TextBox>

                     

                    &nbsp;&nbsp;
                    <asp:Button ID="BtnSearch" runat="server" Text="Help" Font-Size="Medium" ForeColor="Red"
                          Font-Bold="True" CssClass="button green" OnClick="BtnSearch_Click" CausesValidation="False" />

                </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="lblAccType" runat="server" Text="Account Type:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccType" runat="server" Width="96px" Height="25px" CssClass="border_color"
                        Font-Size="Large" onkeydown="return (event.keyCode !=13);" TabIndex="1"></asp:TextBox>

                   <asp:Label ID="lblAccTitle" runat="server" Text=""></asp:Label>
                    
                    &nbsp;&nbsp;
       
                </td>

                    <%--<asp:Label ID="lblAccTypeDesc" runat="server" Width="96px" Height="25px" Font-Size="Large"></asp:Label>--%>
                    
               
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCuNumber" runat="server" Text="Credit Union No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                      
                    <asp:TextBox ID="txtCuNo" runat="server" CssClass="cls text" Width="99px" Height="25px"
                        Font-Size="Large" onkeydown="return (event.keyCode !=13);" TabIndex="2" ></asp:TextBox>
                    <asp:Label ID="lblCuName" runat="server" Text=""></asp:Label>
                    
                    <asp:Label ID="lblstat" runat="server" Text="" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
                              
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMemberNo" runat="server" Text="Depositor No:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                      
                    <asp:TextBox ID="txtMemberNo" runat="server" CssClass="cls text" Width="98px" Height="25px"
                        Font-Size="Large" onkeydown="return (event.keyCode !=13);" TabIndex="3"></asp:TextBox>
                    <asp:Label ID="lblMemName" runat="server" Text=""></asp:Label>
                    
                             
                </td>
            </tr>
            
            <tr>
                <td>
                    <asp:Label ID="lblOpendate" runat="server" Text="Open Date:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOpenDate" runat="server" CssClass="cls text" Width="177px" Height="25px"
                        Font-Size="Large" TabIndex="4"
                        OnTextChanged="txtOpenDate_TextChanged" AutoPostBack="True" />

                    &nbsp;<asp:Label ID="lblLastRenewalDt" runat="server" Text="Last Renewal Date :" Visible="false" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtLastRenewalDt" runat="server"  Visible="false"  CssClass="cls text" Width="150px" Height="25px"
                        Font-Size="Large" TabIndex="4"  AutoPostBack="True" OnTextChanged="txtLastRenewalDt_TextChanged"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDepositAmount" runat="server" Text="Deposit Amount:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDepositAmount" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" OnTextChanged="txtDepositAmount_TextChanged"
                        AutoPostBack="True" TabIndex="5" onkeypress="return IsDecimalKey(event)"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="ValidDepositAmount" runat="server" 
        ControlToValidate="txtDepositAmount" ForeColor="Red"   Display="Dynamic"
        InitialValue="" ValidationGroup="Txtgroup" SetFocusOnError="True" ErrorMessage="* Please Input Deposit Amount"></asp:RequiredFieldValidator>


                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFixedDepositAmount" runat="server" Text="Fixed Deposit Amount:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFixedDepositAmount" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" TabIndex="6" onkeypress="return IsDecimalKey(event)"></asp:TextBox>

                                     <asp:RequiredFieldValidator ID="ValidFixedDepositAmount" runat="server" 
        ControlToValidate="txtFixedDepositAmount" ForeColor="Red"   Display="Dynamic"
        InitialValue="" ValidationGroup="Txtgroup" SetFocusOnError="True" ErrorMessage="* Please Input Fixed Deposit Amount"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPeriod" runat="server" Text="Period (Month):" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPeriod" runat="server" CssClass="cls text" Width="177px" Height="25px"
                        Font-Size="Large" OnTextChanged="txtPeriod_TextChanged"
                        AutoPostBack="True" TabIndex="7" onkeydown="return (event.keyCode !=13);" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                    
                    <asp:RequiredFieldValidator ID="ValidPeriod" runat="server" 
        ControlToValidate="txtPeriod" ForeColor="Red"  
        InitialValue="" ValidationGroup="Txtgroup" SetFocusOnError="True" ErrorMessage="* Please Input Period(Month)"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblWithdrawalAC" runat="server" Text="WithdrawalA/C:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlWithdrawalAC" runat="server" Height="25px" Width="180px"
                        AutoPostBack="True" Font-Size="Large" TabIndex="8">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Debit Allow</asp:ListItem>
                        <asp:ListItem Value="2">Debit Not Allow</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblInterestCalculation" runat="server" Text="Interest Calculation:"
                        Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlInterestCalculation" runat="server" Height="25px" Width="180px"
                        AutoPostBack="True" Font-Size="Large" TabIndex="9">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Give Interest</asp:ListItem>
                        <asp:ListItem Value="2">Not Interest</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMatruityDate" runat="server" Text="Maturity Date:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMatrutiyDate" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" img src="../Images/calender.png"
                        TabIndex="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMatruityAmount" runat="server" Text="Maturity Amount:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMatruityAmount" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" TabIndex="11" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblInterestWithdraw" runat="server" Text="Interest Withdraw:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlInterestWithdraw" runat="server" Height="25px" Width="180px"
                        AutoPostBack="True" Font-Size="Large" TabIndex="12">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">At Once</asp:ListItem>
                        <asp:ListItem Value="2">Weekly</asp:ListItem>
                        <asp:ListItem Value="3">Monthly</asp:ListItem>
                        <asp:ListItem Value="4">Quarterly</asp:ListItem>
                        <asp:ListItem Value="5">Half Yearly</asp:ListItem>
                        <asp:ListItem Value="6">Yearly</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFixedMthInt" runat="server" Text="Interest Benefits:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFixedMthInt" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" TabIndex="13"></asp:TextBox>

                    &nbsp;<asp:Label ID="lblBenefitDt" runat="server" Text="Benefits Date :" Visible="false" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtBenefitDt" runat="server"  Visible="false" Enabled="false"   CssClass="cls text" Width="150px" Height="25px"
                        Font-Size="Large" TabIndex="4"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAutoRenewal" runat="server" Text="Auto Renewal:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAutoRenewal" runat="server" Height="25px" Width="180px"
                        AutoPostBack="True" Font-Size="Large" TabIndex="14">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Yes</asp:ListItem>
                        <asp:ListItem Value="2">No Auto Renewal</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLoanAmount" runat="server" Text="Loan Amount:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLoanAmount" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" AutoPostBack="True"
                        OnTextChanged="txtLoanAmount_TextChanged" TabIndex="15" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNoOfInstallment" runat="server" Text="No Of Installment:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNoOfInstallment" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" TabIndex="16" AutoPostBack="True" OnTextChanged="txtNoOfInstallment_TextChanged" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMonthlyInstallment" runat="server" Text="Monthly Installment:"
                        Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMonthlyInstallment" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" TabIndex="17" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLastInstallment" runat="server" Text="Last Installment:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLastInstallment" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" TabIndex="18" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblInterestRate" runat="server" Text="Interest Rate:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtInterestRate" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" TabIndex="19" onkeypress="return IsDecimalKey(event)"></asp:TextBox>

                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ControlToValidate="txtInterestRate" ForeColor="Red"   Display="Dynamic"
        InitialValue="" ValidationGroup="Txtgroup" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblContractInt" runat="server" Text="Contract Int:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtContractInt" runat="server" CssClass="cls text" Width="177px" Visible="false"
                        Height="25px" Font-Size="Large" TabIndex="20"></asp:TextBox>
                    <asp:CheckBox ID="ChkContraInt" runat="server" AutoPostBack="True" OnCheckedChanged="ChkContraInt_CheckedChanged" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblGracePeriod" runat="server" Text="GracePeriod(Month):" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtGracePeriod" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" TabIndex="21"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLoaneeACType" runat="server" Text="Loanee A/C Type:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLoaneeACType" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" TabIndex="22"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLoaneeMemberNo" runat="server" Text="Loanee Member No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLoaneeMemberNo" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" TabIndex="23"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLoanPaymentSchdule" runat="server" Text="Loan Payment Schdule:"
                        Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLoanPaymentSchdule" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" TabIndex="24"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCertificate" runat="server" Text="Certificate Number :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCertificate" runat="server" CssClass="cls text" Width="428px"
                        Height="25px" Font-Size="Large" onkeydown="return (event.keyCode !=13);" TabIndex="25"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSpInstruction" runat="server" Text="Special Instruction:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSpInstruction" runat="server" CssClass="cls text" Width="428px"
                        Height="25px" Font-Size="Large" TabIndex="26"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCorrAccount" runat="server" Text="Corr. Account No.:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCorrAccType" runat="server" CssClass="cls text" Width="35px"
                        Height="25px" Font-Size="Large" TabIndex="27"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtCorrAccNo" runat="server" CssClass="cls text" Width="116px"
                        Height="25px" Font-Size="Large" TabIndex="28"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAutoTransferSaving" runat="server" Text="Auto Transfer from Corr A/C:"
                        Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAutoTransferSaving" runat="server" Height="25px" Width="180px"
                        AutoPostBack="True" Font-Size="Large" TabIndex="29" OnSelectedIndexChanged="ddlAutoTransferSaving_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Yes</asp:ListItem>
                        <asp:ListItem Value="2">No Auto Transfer</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOldAccNo" runat="server" Text="Old Account No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOldAccNo" runat="server" CssClass="cls text" Width="243px" Height="25px"
                        Font-Size="Large" TabIndex="30"></asp:TextBox>
                </td>
            </tr>

             <tr>
                <td>
                    <asp:Label ID="lblTotalDep" runat="server" Text="Total Deposit :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTotalDep" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" TabIndex="32" onkeypress="return IsDecimalKey(event)" AutoPostBack="True" OnTextChanged="txtTotalDep_TextChanged"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblOrgAmt" runat="server" Text="Orginal Amount:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOrgAmt" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" TabIndex="31" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblPrincipalAmt" runat="server" Text="Principal Amount:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPrincipalAmt" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" TabIndex="32" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                </td>
            </tr>

             <tr>
                <td>
                    <asp:Label ID="lblRenewalAmt" runat="server" Text="Renewal Amount:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRenewalAmt" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" TabIndex="32" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblAnniDate" runat="server" Text="Anniversary Date :" Font-Size="Large" Visible="false" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAnniDate" runat="server"  CssClass="cls text" Visible="false" Width="150px" Height="25px"
                        Font-Size="Large" TabIndex="4" AutoPostBack="True" OnTextChanged="txtAnniDate_TextChanged"/>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblDueInt" runat="server" Text="Due Int. Amount :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDueInt" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Large" TabIndex="32" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                </td>
            </tr>

             <tr>
                <td>
                    <asp:Label ID="lblODIntDate" runat="server" Text="OD Last Int. Date :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtODIntDate" runat="server"  CssClass="cls text" Width="150px" Height="25px"
                        Font-Size="Large" TabIndex="4"/>
                </td>
            </tr>



        </table>
    </div>



    <div align="center">
        <asp:GridView ID="gvHidden" runat="server" Width="813px" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ProductCode" HeaderText="Product Code" />
                <asp:BoundField DataField="RecordCode" HeaderText="Record Code" />
                <asp:BoundField DataField="RecordFlag" HeaderText="Flag" />
                <asp:BoundField DataField="FuncFlag" HeaderText="Flag1" />
            </Columns>
        </asp:GridView>
    </div>
    <div align="center">

        <asp:Button ID="BtnNominee" runat="server" Text="Nominee" Width="105px" Font-Size="Large"
            ForeColor="White" Height="31px" Font-Bold="True"
            CssClass="button green" AutoPostBack="True" OnClick="BtnNominee_Click" />&nbsp;
   
        <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Width="91px" Font-Size="Large"
            ForeColor="White" Height="31px" Font-Bold="True" ToolTip="Insert Information"
           ValidationGroup="Txtgroup" CssClass="button green" OnClick="BtnSubmit_Click" />&nbsp;
            
        <asp:Button ID="BtnUpdate" runat="server" Text="Update" Width="91px" Font-Size="Large"
            ForeColor="White" Height="31px" Font-Bold="True" ToolTip="Update Information"
            OnClientClick="return ValidationBeforeUpdate() " CssClass="button green" OnClick="BtnUpdate_Click" />&nbsp;
            
        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Bold="True" Font-Size="Large"
            ForeColor="#FFFFCC" Height="31px" Width="91px" ToolTip="Exit Reference Page"
            CssClass="button red" OnClick="BtnExit_Click" />
    </div>
    <div id="divExtra" hidden="true">
        <asp:Label ID="Label4" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="Label5" runat="server" Text="" Visible="false"></asp:Label><br />
        <asp:TextBox ID="txtMemType" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtAtyClass" runat="server"></asp:TextBox>

    </div>
    <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuNum" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuTypeName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuOpenDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccTypeMode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPrmValue" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlModule" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlFunc" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="ProcDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="GetOpenDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlAccStatus" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlMsgFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnNewAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCuNumber" runat="server" Text="" Visible="false"></asp:Label>
        
    <asp:Label ID="CtrlAccountNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlOrgAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlBenefitDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlRenwlDate" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlNoAnni" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlAnniDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlMatrutiyDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="ChkMatrutiyDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="OrgAccPeriod" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="PrmDepositAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblflag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblcls" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblBenefitMth" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblBenefitDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblPrevAnniDate" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblDepositFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccountBal" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblchk1Hide" runat="server" Text="" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblchk2Hide" runat="server" Text="" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="lblchk3Hide" runat="server" Text="" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>

    <asp:Label ID="lblCorrAccType" runat="server" Text="" Font-Size="Large" ForeColor="Red" Visible="false"></asp:Label>
</asp:Content>
