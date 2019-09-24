<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="GLTransactionReportList.aspx.cs" Inherits="ATOZWEBMCUS.Pages.GLTransactionReportList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtfdate.ClientID %>").dynDateTime({
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
            $("#<%=txttdate.ClientID %>").dynDateTime({
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

    <style type="text/css">
        .auto-style1 {
            height: 35px;
        }

        .auto-style2 {
            height: 34px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <br />

    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="7">GL Transaction Reports
                    </th>
                </tr>

            </thead>


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

                    <asp:CheckBox ID="ChkAllGLCode" runat="server" AutoPostBack="True" Font-Size="Large" ForeColor="Red" Text="   All" OnCheckedChanged="ChkAllGLCode_CheckedChanged" />


                </td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblGLCode" runat="server" Text=" GL Code" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>

                <td colspan="2" style="background-color: #fce7f9">
                    <asp:TextBox ID="txtGLCode" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" onkeypress="return IsNumberKey(event)" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtGLCode_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlGLCode" runat="server" Height="25px" Width="450px" AutoPostBack="True"
                        Font-Size="Large" Style="margin-left: 10px" OnSelectedIndexChanged="ddlGLCode_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>



                </td>

            </tr>

            <tr>

                <td style="background-color: #fce7f9" class="auto-style2">

                    <asp:CheckBox ID="ChkAllTrnType" runat="server" AutoPostBack="True" Font-Size="Large" ForeColor="Red" Text="   All" OnCheckedChanged="ChkAllTrnType_CheckedChanged" />


                </td>
                <td style="background-color: #fce7f9" class="auto-style2">
                    <asp:Label ID="lblTrnType" runat="server" Text=" Trn.Type" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>

                <td colspan="2" style="background-color: #fce7f9" class="auto-style2">
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

                    <asp:CheckBox ID="ChkAllTranMode" runat="server" AutoPostBack="True" Font-Size="Large" ForeColor="Red" Text="   All" OnCheckedChanged="ChkAllTranMode_CheckedChanged" />


                </td>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblTranMode" runat="server" Text="Transaction " Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>

                <td colspan="2" style="background-color: #fce7f9">

                    <asp:DropDownList ID="ddlTranMode" runat="server" Font-Size="Large" Height="25px" Width="300px" Style="margin-left: 11px">
                        <asp:ListItem Value="0" Selected="True"> -Select -  </asp:ListItem>
                        <asp:ListItem Value="1">Customer Service Transction</asp:ListItem>
                        <asp:ListItem Value="2">General Ledger Transaction</asp:ListItem>
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
                        Font-Size="Large" onkeypress="return IsNumberKey(event)" AutoPostBack="True" ToolTip="Enter Code"></asp:TextBox>
                    <asp:DropDownList ID="ddlTeller" runat="server" Font-Size="Large" Height="25px" Width="300px" AutoPostBack="True" Style="margin-left: 11px" OnSelectedIndexChanged="ddlTeller_SelectedIndexChanged">
                        <asp:ListItem Value="0" Selected="True"> -Select -  </asp:ListItem>

                    </asp:DropDownList>
                </td>


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
                    <asp:DropDownList ID="ddlTrans" runat="server" Font-Size="Large" Height="25px" Width="250px" Style="margin-left: 4px">
                        <asp:ListItem Value="0" Selected="True"> -Select -  </asp:ListItem>
                        <asp:ListItem Value="1">Auto System Transaction</asp:ListItem>
                        <asp:ListItem Value="2">All Transaction</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>

                <td style="background-color: #fce7f9">

                    <asp:CheckBox ID="ChkAllAmount" runat="server" AutoPostBack="True" Font-Size="Large" ForeColor="Red" Text="   All" OnCheckedChanged="ChkAllAmount_CheckedChanged" />
                </td>

                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblAmount" runat="server" Text="Transaction Amount" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>

                <td colspan="2" style="background-color: #fce7f9">
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code"></asp:TextBox>
            </tr>

        </table>
    </div>

    <div align="center">
        <table class="style1">


            <tr>

                <td>
                    <asp:Label ID="lblfdate" runat="server" Text="From Date:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    <asp:TextBox ID="txtfdate" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png"></asp:TextBox>

                </td>

                <td>
                    <asp:Label ID="lbltdate" runat="server" Text="To Date:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    <asp:TextBox ID="txttdate" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png"></asp:TextBox>

                </td>


            </tr>

            <tr>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="rbtGLDetail" runat="server" GroupName="GLRptGrp" Text=" GL Detail Transaction Report" AutoPostBack="True" />
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="rbtSummary" runat="server" GroupName="GLRptGrp" Text="GL Summary Transacton Report" AutoPostBack="True" />
                </td>
                <td></td>
                <td></td>
            </tr>

            <tr>
                <td>
                    <asp:RadioButton ID="rbtConsolidated" runat="server" GroupName="GLRptGrp" Text="GL Transacton Consolidated Report" AutoPostBack="True" />
                </td>
                <td></td>
                <td></td>
            </tr>


            <tr>

                <td>
                    <asp:CheckBox ID="ChkValueDate" runat="server" Font-Size="Large" ForeColor="Red" Text=" Only Value Date Transaction" AutoPostBack="True" OnCheckedChanged="ChkValueDate_CheckedChanged" />
                </td>
            </tr>

            <tr>

                <td>
                    <asp:CheckBox ID="ChkVchWise" runat="server" Font-Size="Large" ForeColor="Red" Text=" Voucher Wise Trans.Reports" AutoPostBack="True" OnCheckedChanged="ChkVchWise_CheckedChanged" />
                </td>
            </tr>

            <tr>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td class="auto-style1"></td>

                <td class="auto-style1">
                    <asp:Button ID="BtnView" runat="server" Text="Preview / Print" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" OnClick="BtnView_Click" />&nbsp;
                  &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>
    </div>


    <asp:Label ID="lblModule" runat="server" Visible="False"></asp:Label>

    <asp:Label ID="hdnID" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="hdnCashCode" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="hdnCashCodeDesc" runat="server" Visible="False"></asp:Label>

    <asp:Label ID="CtrlProgFlag" runat="server" Visible="False"></asp:Label>

    <asp:Label ID="lblTrans" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblDrCr" runat="server" Visible="False"></asp:Label>



</asp:Content>
