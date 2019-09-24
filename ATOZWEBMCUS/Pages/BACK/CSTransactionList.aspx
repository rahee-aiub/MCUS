<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSTransactionList.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSTransactionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

    </script>

    <script src="../scripts/validation.js" type="text/javascript"></script>
    <%-- <script src="../scripts/jquery-ui.js" type="text/javascript"></script>--%>

    <%--<script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />--%>

     <script language="javascript" type="text/javascript">
         $(function () {
             $("#<%= txtfdate.ClientID %>").datepicker();

             var prm = Sys.WebForms.PageRequestManager.getInstance();

             prm.add_endRequest(function () {
                 $("#<%= txtfdate.ClientID %>").datepicker();

            });

         });
        $(function () {
            $("#<%= txttdate.ClientID %>").datepicker();

             var prm = Sys.WebForms.PageRequestManager.getInstance();

             prm.add_endRequest(function () {
                 $("#<%= txttdate.ClientID %>").datepicker();

            });

         });
            </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <br />
    <br />
  
    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="4">
                        <asp:Label ID="lblTransFunction" runat="server" Text="Label"></asp:Label>
                    </th>
                </tr>

            </thead>


            <tr>
                <td style="background-color: #fce7f9">


                    <asp:CheckBox ID="chkAllAccType" runat="server" AutoPostBack="True" OnCheckedChanged="chkAllAccType_CheckedChanged" Font-Size="Large" ForeColor="Red" Text="   All" />

                </td>

                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblcode" runat="server" Text="Account Type:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>

                <td colspan="2" style="background-color: #fce7f9">
                    <asp:TextBox ID="txtAccType" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" onkeypress="return IsNumberKey(event)" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtAccType_TextChanged"></asp:TextBox>


                    <asp:DropDownList ID="ddlAcType" runat="server" Height="25px" Width="450px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddlAcType_SelectedIndexChanged" Style="margin-left: 11px">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>

                <td style="background-color: #fce7f9">

                    <asp:CheckBox ID="ChkAll99Func" runat="server" AutoPostBack="True" OnCheckedChanged="ChkAll99Func_CheckedChanged" Font-Size="Large" ForeColor="Red" Text="   All" />


                </td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lbl99Func" runat="server" Text=" Misc.Func" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>

                <td colspan="2" style="background-color: #fce7f9">
                    <asp:TextBox ID="txt99Func" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" onkeypress="return IsNumberKey(event)" ToolTip="Enter Code" ></asp:TextBox>
                    <asp:DropDownList ID="ddl99Func" runat="server" Height="25px" Width="450px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddl99Func_SelectedIndexChanged" Style="margin-left: 10px">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>



                </td>

            </tr>


            <tr>

                <td style="background-color: #fce7f9">

                    <asp:CheckBox ID="ChkAllCrUnion" runat="server" AutoPostBack="True" OnCheckedChanged="ChkAllCrUnion_CheckedChanged" Font-Size="Large" ForeColor="Red" Text="   All" />


                </td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblCreditUnion" runat="server" Text=" Credit Union" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>

                <td colspan="2" style="background-color: #fce7f9">
                    <asp:TextBox ID="txtCreditUNo" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" onkeypress="return IsNumberKey(event)" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtCreditUNo_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlCreditUNo" runat="server" Height="25px" Width="450px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddlCreditUNo_SelectedIndexChanged" Style="margin-left: 10px">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>



                </td>

            </tr>

            <tr>

                <td style="background-color: #fce7f9">

                    <asp:CheckBox ID="ChkAllFCashCode" runat="server" AutoPostBack="True" Font-Size="Large" ForeColor="Red" Text="   All" OnCheckedChanged="ChkAllFCashCode_CheckedChanged" />


                </td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblFCashCode" runat="server" Text=" From Cash Code" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>

                <td colspan="2" style="background-color: #fce7f9">
                    <asp:TextBox ID="txtFCashCode" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" onkeypress="return IsNumberKey(event)" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtFCashCode_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlFCashCode" runat="server" Height="25px" Width="450px" AutoPostBack="True"
                        Font-Size="Large" Style="margin-left: 10px" OnSelectedIndexChanged="ddlFCashCode_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>



                </td>

            </tr>


            <tr>

                <td style="background-color: #fce7f9">

                    <asp:CheckBox ID="ChkAllTrnType" runat="server" AutoPostBack="True" Font-Size="Large" ForeColor="Red" Text="   All" OnCheckedChanged="ChkAllTrnType_CheckedChanged" />


                </td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblTrnType" runat="server" Text=" Trn.Type" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>

                <td colspan="2" style="background-color: #fce7f9">
                    <asp:TextBox ID="txtTrnType" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" onkeypress="return IsNumberKey(event)" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtTrnType_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlTrnType" runat="server" Font-Size="Large" Height="25px" Width="120px" AutoPostBack="True" OnSelectedIndexChanged="ddlTrnType_SelectedIndexChanged" Style="margin-left: 11px">
                        <asp:ListItem Value="0" Selected="True"> -Select -  </asp:ListItem>
                        <asp:ListItem Value="1">Cash</asp:ListItem>
                        <asp:ListItem Value="2">Trf.</asp:ListItem>
                        <asp:ListItem Value="3">Bank</asp:ListItem>
                        <asp:ListItem Value="4">CashBank</asp:ListItem>
                        <asp:ListItem Value="5">TrfBank</asp:ListItem>
                    </asp:DropDownList>
                </td>


            </tr>

            <tr>

                <td style="background-color: #fce7f9">

                    <asp:CheckBox ID="ChkAllTrnMode" runat="server" AutoPostBack="True" Font-Size="Large" ForeColor="Red" Text="   All" OnCheckedChanged="ChkAllTrnMode_CheckedChanged" />


                </td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblTrnMode" runat="server" Text=" Trn.Mode" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>

                <td colspan="2" style="background-color: #fce7f9">
                    <asp:TextBox ID="txtTrnMode" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" onkeypress="return IsNumberKey(event)" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtTrnMode_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlTrnMode" runat="server" Font-Size="Large" Height="25px" Width="120px" AutoPostBack="True" Style="margin-left: 11px" OnSelectedIndexChanged="ddlTrnMode_SelectedIndexChanged">
                        <asp:ListItem Value="0" Selected="True"> -Select -  </asp:ListItem>
                        <asp:ListItem Value="1">Debit</asp:ListItem>
                        <asp:ListItem Value="2">Credit</asp:ListItem>
                    </asp:DropDownList>
                </td>


            </tr>

            <tr>

                <td style="background-color: #fce7f9">

                    <asp:CheckBox ID="ChkAllTrnNature" runat="server" AutoPostBack="True" Font-Size="Large" ForeColor="Red" Text="   All" OnCheckedChanged="ChkAllTrnNature_CheckedChanged" />


                </td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblTrnNature" runat="server" Text=" Trn.Nature" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>

                <td colspan="2" style="background-color: #fce7f9">
                    <asp:TextBox ID="txtTrnNature" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" onkeypress="return IsNumberKey(event)" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtTrnNature_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlTrnNature" runat="server" Font-Size="Large" Height="25px" Width="250px" AutoPostBack="True" Style="margin-left: 11px" OnSelectedIndexChanged="ddlTrnNature_SelectedIndexChanged">
                        <asp:ListItem Value="0" Selected="True"> -Select -  </asp:ListItem>
                        <asp:ListItem Value="1">Deposit Amount</asp:ListItem>
                        <asp:ListItem Value="2">Loan Settlement</asp:ListItem>
                        <asp:ListItem Value="3">Withdrawal Amount</asp:ListItem>
                        <asp:ListItem Value="5">Interest/Benefit Withdrawal</asp:ListItem>
                        <asp:ListItem Value="7">Loan Disbursement</asp:ListItem>
                        <asp:ListItem Value="9">Encashment</asp:ListItem>
                        <asp:ListItem Value="11">Adjustment Credit</asp:ListItem>
                        <asp:ListItem Value="12">Adjustment Debit</asp:ListItem>
                        <asp:ListItem Value="19">Devident Amount</asp:ListItem>
                        <asp:ListItem Value="20">Account Balance Transfer</asp:ListItem>
                        <asp:ListItem Value="403">Loan Principal Deposit</asp:ListItem>
                        <asp:ListItem Value="402">Loan Interest Deposit</asp:ListItem>
                        <asp:ListItem Value="107">Interest Adjustment Cr.</asp:ListItem>
                        <asp:ListItem Value="108">Interest Adjustment Dr.</asp:ListItem>
                        <asp:ListItem Value="113">Provision Adjustment Cr.</asp:ListItem>
                        <asp:ListItem Value="114">Provision Adjustment Dr.</asp:ListItem>
                    </asp:DropDownList>
                </td>


            </tr>

            <tr>

                <td style="background-color: #fce7f9">

                    <asp:CheckBox ID="ChkAllTeller" runat="server" AutoPostBack="True" Font-Size="Large" ForeColor="Red" Text="   All" OnCheckedChanged="ChkAllTeller_CheckedChanged" />


                </td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblTeller" runat="server" Text=" User Id." Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>

                <td colspan="2" style="background-color: #fce7f9">
                    <asp:TextBox ID="txtTeller" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" onkeypress="return IsNumberKey(event)" AutoPostBack="True" ToolTip="Enter Code" OnTextChanged="txtTeller_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlTeller" runat="server" Font-Size="Large" Height="25px" Width="300px" AutoPostBack="True" Style="margin-left: 11px" OnSelectedIndexChanged="ddlTeller_SelectedIndexChanged">
                        <asp:ListItem Value="0" Selected="True"> -Select -  </asp:ListItem>

                    </asp:DropDownList>
                </td>


            </tr>

            <tr>

                <td style="background-color: #fce7f9">

                    <asp:CheckBox ID="ChkAllVchNo" runat="server" AutoPostBack="True" Font-Size="Large" ForeColor="Red" Text="   All" OnCheckedChanged="ChkAllVchNo_CheckedChanged" />


                </td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="Label1" runat="server" Text="Vch.No." Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>

                <td colspan="2" style="background-color: #fce7f9">
                    <asp:TextBox ID="txtVchNo" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code"></asp:TextBox>
            </tr>

            <tr>

                <td style="background-color: #fce7f9">

                    <asp:CheckBox ID="ChkAllTrans" runat="server" AutoPostBack="True" Font-Size="Large" ForeColor="Red" Text="   All" OnCheckedChanged="ChkAllTrans_CheckedChanged" />


                </td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="Label2" runat="server" Text="Normal Transaction" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>

               <td colspan="2" style="background-color: #fce7f9">              
                    <asp:DropDownList ID="ddlTrans" runat="server" Font-Size="Large" Height="25px" Width="240px" Style="margin-left: 4px" AutoPostBack="True" OnSelectedIndexChanged="ddlTrans_SelectedIndexChanged">
                        <asp:ListItem Value="0" Selected="True"> -Select -  </asp:ListItem>
                        <asp:ListItem Value="1">Auto System Transaction</asp:ListItem>
                        <asp:ListItem Value="2">All Transaction</asp:ListItem>
                    </asp:DropDownList>
                   &nbsp; &nbsp; 
                   <asp:DropDownList ID="ddlSysTrans" runat="server" Font-Size="Large" Height="25px" Width="250px" Style="margin-left: 4px">
                        <asp:ListItem Value="0" Selected="True"> -Select -  </asp:ListItem>
                        <asp:ListItem Value="1">Auto Provision CPS</asp:ListItem>
                        <asp:ListItem Value="2">Auto Provision FDR</asp:ListItem>
                        <asp:ListItem Value="3">Auto Provision 6YR</asp:ListItem>
                        <asp:ListItem Value="4">Auto Anniversary CPS</asp:ListItem>
                        <asp:ListItem Value="5">Auto Anniversary FDR</asp:ListItem>
                        <asp:ListItem Value="6">Auto Anniversary 6YR</asp:ListItem>
                        <asp:ListItem Value="7">Auto Renewal FDR</asp:ListItem>
                        <asp:ListItem Value="8">Auto Renewal 6YR</asp:ListItem>
                        <asp:ListItem Value="9">Auto Benefit MSplus</asp:ListItem>
                        <asp:ListItem Value="10">Auto Interest Post</asp:ListItem>
                        <asp:ListItem Value="11">Auto Devident Post</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>

                <td style="background-color: #fce7f9"></td>

                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblfdate" runat="server" Text="From date" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>

                <td style="background-color: #fce7f9">
                    <asp:TextBox ID="txtfdate" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png"></asp:TextBox>

                    <asp:Label ID="lbltdate" runat="server" Text="  To date:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    <asp:TextBox ID="txttdate" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png"></asp:TextBox>

                     &nbsp; &nbsp; &nbsp; 
                     


                </td>




            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:CheckBox ID="ChkValueDate" runat="server"  Font-Size="Large" ForeColor="Red" Text=" Only Value Date Transaction" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:CheckBox ID="ChkVchWise" runat="server"  Font-Size="Large" ForeColor="Red" Text=" Voucher Wise Trans.Reports" />
                </td>
            </tr>
            <tr>

                <td colspan="2" style="background-color: #fce7f9"></td>

                <td colspan="2" style="background-color: #fce7f9">


                    <asp:Label ID="lblCuNo" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblCuType" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblModule" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblProcDate" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblBegFinYear" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="hdnCashCode" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="hdnCashCodeDesc" runat="server" Visible="False"></asp:Label>

                     <asp:Label ID="lblTrans" runat="server" Visible="False"></asp:Label>
                     <asp:Label ID="lblAutoVchNo" runat="server" Visible="False"></asp:Label>

                    <asp:Label ID="CtrlProgFlag" runat="server" Visible="False"></asp:Label>

                    <asp:TextBox ID="txtHidden" runat="server" CssClass="cls text" Width="160px" Height="25px"
                        Font-Size="Large" Visible="false"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td colspan="2" style="background-color: #fce7f9"></td>

                <td colspan="2" style="background-color: #fce7f9">
                    <asp:Button ID="BtnView" runat="server" Text="Preview / Print" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" Width="180px" OnClick="BtnView_Click" />&nbsp;
                                      &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>

        </table>
    </div>

    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccClass" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>

