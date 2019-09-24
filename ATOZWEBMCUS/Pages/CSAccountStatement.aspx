<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSAccountStatement.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSAccountStatement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


   <%-- <script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

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



    <%--<style type="text/css">
        .auto-style1 {
            height: 34px;
            width: 17px;
        }
        .auto-style2 {
            width: 111px;
        }
        .auto-style4 {
            width: 108px;
        }
        .auto-style5 {
            width: 17px;
        }
    </style>--%>



    <style type="text/css">
        .auto-style1 {
            height: 37px;
        }
    </style>



</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">


    <br />
    <br />
    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="7">
                        <asp:Label ID="lblStatementFunc" runat="server" Text="Label"></asp:Label>
                        
                    </th>
                </tr>

            </thead>

            <tr>
                <td style="background-color: #fce7f9">

                    <asp:Label ID="lblAccNo" runat="server" Text="Account No." Font-Size="Medium"
                        ForeColor="Black"></asp:Label>
                </td>

                <td style="background-color: #fce7f9"></td>

                <td style="background-color: #fce7f9">

                    <asp:TextBox ID="txtAccNo" runat="server" CssClass="cls text" Width="145px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="1" AutoPostBack="True" OnTextChanged="txtAccNo_TextChanged"></asp:TextBox>&nbsp;
                   <asp:Label ID="lblAccTitle" runat="server" Text=""></asp:Label>

                    &nbsp;&nbsp;
                    <asp:Button ID="BtnSearch" runat="server" Text="Help" Font-Size="Medium" ForeColor="Red"
                          Font-Bold="True" CssClass="button green" OnClick="BtnSearch_Click" />

                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                     <asp:Label ID="lblStat" runat="server" Text="Status :" Font-Size="Large" ForeColor="Red"></asp:Label>
                    <asp:TextBox ID="txtstat" runat="server" Text="" Font-Bold="true" Font-Size="Large" ForeColor="Red" Enabled="false"></asp:TextBox>

                </td>
                <td>
                    <asp:TextBox ID="tClass" runat="server" Visible="false" Enabled="False" Height="22px"></asp:TextBox>
                </td>


            </tr>

            <tr>

                <td style="background-color: #fce7f9">

                    <asp:Label ID="lblCreditUnion" runat="server" Text="Credit Union :" Font-Size="Medium"
                        ForeColor="Black"></asp:Label>


                </td>
                <td style="background-color: #fce7f9"></td>

                <td style="background-color: #fce7f9">
                    <asp:TextBox ID="txtCreditUNo" runat="server" CssClass="cls text" Width="145px" Height="25px"
                        Font-Size="Large" TabIndex="2"></asp:TextBox>

                    <asp:Label ID="lblCuName" runat="server" Width="600px" Height="25px" Text=""></asp:Label>


                </td>
                <td></td>



            </tr>

            <tr>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblCULBMemNo" runat="server" Text="Depositor No." Font-Size="Medium" ForeColor="Black"></asp:Label>
                </td>
                <td style="background-color: #fce7f9"></td>
                <td style="background-color: #fce7f9">
                    <asp:TextBox ID="txtCULBMemNo" runat="server" CssClass="cls text" Width="145px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" TabIndex="3" onkeypress="return functionx(event)"></asp:TextBox>

                    <asp:Label ID="lblMemName" runat="server" Width="600px" Height="25px" Text=""></asp:Label>



                </td>
            </tr>


            <tr>

                <td style="background-color: #fce7f9" class="auto-style1">
                    <asp:Label ID="lblfdate" runat="server" Text="From Date" Font-Size="Medium"
                        ForeColor="Black"></asp:Label>
                </td>
                <td style="background-color: #fce7f9" class="auto-style1">:
                </td>
                <td style="background-color: #fce7f9" class="auto-style1">

                    <asp:TextBox ID="txtfdate" runat="server" CssClass="cls text" Width="145px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" TabIndex="4"></asp:TextBox>

                    <asp:Label ID="lblCuNo" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblCuType" runat="server" Visible="False"></asp:Label>

                </td>



            </tr>
            <tr>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lbltdate" runat="server" Text="To Date" Font-Size="Medium"
                        ForeColor="Black"></asp:Label>

                </td>
                <td style="background-color: #fce7f9"></td>


                <td style="background-color: #fce7f9">
                    <asp:TextBox ID="txttdate" runat="server" CssClass="cls text" Width="145px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png"></asp:TextBox>

                    <asp:TextBox ID="tOpenDt" runat="server" CssClass="cls text" Width="50px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" Visible="False"></asp:TextBox>

                    <asp:TextBox ID="tMaturityDt" runat="server" CssClass="cls text" Width="40px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" Visible="False"></asp:TextBox>

                    <asp:TextBox ID="tRenewalDt" runat="server" CssClass="cls text" Width="40px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" Visible="False"></asp:TextBox>

                    <asp:TextBox ID="tAccPeriod" runat="server" CssClass="cls text" Width="37px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" Visible="False"></asp:TextBox>

                    <asp:TextBox ID="tOrgAmt" runat="server" CssClass="cls text" Width="28px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="tPrincipleAmt" runat="server" CssClass="cls text" Width="36px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="tIntRate" runat="server" CssClass="cls text" Width="51px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" Visible="False"></asp:TextBox>

                    <%--AccLoanSancAmt,AccLoanSancDate,AccDisbAmt,AccDisbDate--%>

                    <asp:TextBox ID="tInterest" runat="server" CssClass="cls text" Width="33px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" Visible="False"></asp:TextBox>

                    <asp:TextBox ID="tAccBalance" runat="server" CssClass="cls text" Width="33px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" Visible="False"></asp:TextBox>

                    <asp:TextBox ID="tOldAccount" runat="server" CssClass="cls text" Width="33px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" Visible="False"></asp:TextBox>


                    <asp:TextBox ID="tAccLoanSancAmt" runat="server" CssClass="cls text" Width="33px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" Visible="False"></asp:TextBox>

                    <asp:TextBox ID="tAccLoanSancDate" runat="server" CssClass="cls text" Width="33px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png" Visible="False"></asp:TextBox>

                    <asp:TextBox ID="tAccDisbAmt" runat="server" CssClass="cls text" Width="33px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code"  Visible="False"></asp:TextBox>

                    <asp:TextBox ID="tAccDisbDate" runat="server" CssClass="cls text" Width="33px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png" Visible="False"></asp:TextBox>

                    <%--AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt--%>

                    <asp:TextBox ID="tAccNoInstl" runat="server" CssClass="cls text" Width="33px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png" Visible="False"></asp:TextBox>

                    <asp:TextBox ID="tAccLoanInstlAmt" runat="server" CssClass="cls text" Width="33px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png" Visible="False"></asp:TextBox>

                    <asp:TextBox ID="tAccLoanLastInstlAmt" runat="server" CssClass="cls text" Width="33px" Height="25px"
                        Font-Size="Large" ToolTip="Enter Code" img src="../Images/calender.png" Visible="False"></asp:TextBox>

                </td>

            </tr>


            <tr>

                <td style="background-color: #fce7f9"></td>
                <td style="background-color: #fce7f9"></td>
                <td style="background-color: #fce7f9">
                    <asp:CheckBox ID="ChkInterest" runat="server" Text="Show Interest" Height="30" Font-Size="Large" ForeColor="#CC6600" TabIndex="5"></asp:CheckBox>
                </td>

            </tr>

            <tr>
                <td colspan=" 4" style="background-color: #fce7f9"></td>

            </tr>
            <tr>

                <td style="background-color: #fce7f9"></td>
                <td style="background-color: #fce7f9"></td>
                <td style="background-color: #fce7f9">


                    <asp:Button ID="BtnView" runat="server" Text="Preview/Print" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" OnClick="BtnView_Click" />&nbsp;
                    
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Font-Bold="True" CssClass="button red" ToolTip="Exit Page" CausesValidation="False"
                        OnClick="BtnExit_Click" />



                </td>
            </tr>

        </table>
    </div>


    <asp:Label ID="CtrlAccType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblModule" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblBegFinYear" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblProcDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblflag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblcls" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTrnCode" runat="server" Text="" Visible="false"></asp:Label>
   
    <asp:Label ID="CtrlProgFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlAccStatus" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="tAccCertNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="tAccFixedMthInt" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="tLastTrnDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="tTotalDeposit" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="tMthDeposit" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="tDueDepositAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="tUptoDate" runat="server" Text="" Visible="false"></asp:Label>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
