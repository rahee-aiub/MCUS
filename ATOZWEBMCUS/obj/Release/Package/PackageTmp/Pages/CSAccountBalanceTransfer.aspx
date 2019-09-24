<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSAccountBalanceTransfer.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSAccountBalanceTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save New Sanction Amount?');
        }

        function ValidationBeforeUpdate() {
            var txtCreditUNo = document.getElementById('<%=txtCreditUNo.ClientID%>').value;
            var txtMemNo = document.getElementById('<%=txtMemNo.ClientID%>').value;

            var txtAccNo = document.getElementById('<%=txtAccNo.ClientID%>').value;
            var txtTrnCuNo = document.getElementById('<%=txtTrnCuNo.ClientID%>').value;
            var txtTrnMemNo = document.getElementById('<%=txtTrnMemNo.ClientID%>').value;

            if (txtCreditUNo == '' || txtCreditUNo.length == 0)
                alert('Please Input From Credit Union No.');
            else
                if (txtMemNo == '' || txtMemNo.length == 0)
                    alert('Please Input From Depositor No.');

                else
                    if (txtAccNo == '')
                        alert('Please Input Account No.');
                    else
                        if (txtTrnCuNo == '' || txtTrnCuNo.length == 0)
                            alert('Please Input To Credit Union No.');
                        else
                            if (txtTrnMemNo == '' || txtTrnMemNo.length == 0)
                                alert('Please Input To Depositor No.');
                            else
                                return confirm('Are you sure you want to Transfer Account Balance?');
            return false;

        }

    </script>

    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtTranDate.ClientID %>").datepicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#<%= txtTranDate.ClientID %>").datepicker();

              });

        });


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <br />
    <div align="center">
        <table class="style1">

            <thead>
                <tr>
                    <th colspan="3">Account Balance Transfer From..........
                    </th>
                </tr>
            </thead>

            <tr>
                <td>
                    <asp:Label ID="lblTranDate" runat="server" Text="Transaction Date:" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTranDate" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="1" AutoPostBack="True" OnTextChanged="txtTranDate_TextChanged"></asp:TextBox>

                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  
                             
                 

                </td>

            </tr>


            <tr>
                <td>
                    <asp:Label ID="lblAccNo" runat="server" Text="Account No:" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txtAccNo" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="2" AutoPostBack="True" ToolTip="Enter Code" onkeypress="return functionx(event)" OnTextChanged="txtAccNo_TextChanged"></asp:TextBox>
                    <asp:Label ID="lblAccTitle" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                <asp:Button ID="BtnSearch" runat="server" Text="Help" Font-Size="Medium" ForeColor="Red"
                                    Font-Bold="True" CssClass="button green" OnClick="BtnSearch_Click" />

                </td>
            </tr>



            <tr>
                <td>
                    <asp:Label ID="lblCUNum" runat="server" Text="Credit Union No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCreditUNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="3"></asp:TextBox>

                    <asp:Label ID="lblCuName" runat="server" Width="500px" Height="25px" Text=""></asp:Label>


                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMemNo" runat="server" Text="Depositor No:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMemNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="4" onkeypress="return functionx(event)"></asp:TextBox>

                    <asp:Label ID="lblMemName" runat="server" Width="500px" Height="25px" Text=""></asp:Label>

                </td>
            </tr>
            <tr>
                <td></td>

                <td>

                    <asp:Label ID="lblBal" runat="server" Text="Balance:" Font-Size="Large"
                        ForeColor="Black"></asp:Label>
                    &nbsp;
                    <asp:Label ID="lblAccBalance" runat="server" Text="" Font-Size="Large"
                        ForeColor="Black"></asp:Label>

                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblLimit" runat="server" Text="Limit Balance :" Font-Size="Large"
                        ForeColor="Black"></asp:Label>
                    &nbsp;
                    <asp:Label ID="lblLimitBalance" runat="server" Text="" Font-Size="Large"
                        ForeColor="Black"></asp:Label>


                </td>
            </tr>
        </table>
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Account Balance Transfer To..............
                    </th>
                </tr>
            </thead>

            <tr>
                <td>
                    <asp:Label ID="lblTrnAccNo" runat="server" Text="Account No:" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txtTrnAccNo" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="5" AutoPostBack="True" ToolTip="Enter Code" onkeypress="return functionx(event)"
                        OnTextChanged="txtTrnAccNo_TextChanged"></asp:TextBox>
                    <asp:Label ID="lblTrnAccTitle" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                     <asp:Button ID="BtnTrnSearch" runat="server" Text="Help" Font-Size="Medium" ForeColor="Red"
                         Font-Bold="True" CssClass="button green" OnClick="BtnTrnSearch_Click" />

                </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="lblTrnCuNo" runat="server" Text="Credit Union No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txtTrnCuNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="6"></asp:TextBox>
                    <asp:Label ID="lblTrnCuName" runat="server" Width="500px" Height="25px" Text=""></asp:Label>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTrnmemno" runat="server" Text="Depositor No:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTrnMemNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="7"></asp:TextBox>
                    <asp:Label ID="lblTrnMemName" runat="server" Width="500px" Height="25px" Text=""></asp:Label>


                </td>
            </tr>

            <tr>
                <td></td>
                <td></td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblVchNo" runat="server" Font-Size="Medium" Text="Vch.No. :" ForeColor="Red"></asp:Label>
                    
                </td>
                <td>
                    <asp:TextBox ID="txtVchNo" runat="server" CssClass="cls text" Width="115px" Height="25px" TabIndex="8" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblIntAmt" runat="server" Text="Interest Amount :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIntAmt" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="9"  onkeydown="return (event.keyCode !=13);" onkeypress="return IsDecimalKey(event)" AutoPostBack="True" OnTextChanged="txtIntAmt_TextChanged"></asp:TextBox>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblPrincAmt" runat="server" Text="Principal Amount :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPrincAmt" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="10"  onkeydown="return (event.keyCode !=13);" onkeypress="return IsDecimalKey(event)" AutoPostBack="True" OnTextChanged="txtPrincAmt_TextChanged"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTrnAmount" runat="server" Text="Transaction Amount :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTrnAmount" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="11" AutoPostBack="True" onkeydown="return (event.keyCode !=13);" onkeypress="return IsDecimalKey(event)" OnTextChanged="txtTrnAmount_TextChanged"></asp:TextBox>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblDescription" runat="server" Text="Description :" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="cls text" Width="300px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium" TabIndex="12" onkeydown="return (event.keyCode !=13);"></asp:TextBox>

                </td>
            </tr>

            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnTransfer" runat="server" Text="Transfer" Font-Size="Large"
                        ForeColor="White" Font-Bold="True" CssClass="button green" Height="27px" Width="140px"
                        OnClientClick="return ValidationBeforeUpdate()" OnClick="BtnTransfer_Click" />&nbsp;

                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red" OnClick="BtnExit_Click" />

                    <br />
                </td>
            </tr>

        </table>
        <asp:TextBox ID="txtHidden" runat="server" Visible="false"></asp:TextBox>
        <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblAccTypeClass" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblTrnferCuType" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblTrnferCuNo" runat="server" Visible="false"></asp:Label>
        <asp:TextBox ID="txtTrnHidden" runat="server" Visible="false"></asp:TextBox>


        <asp:Label ID="hdnID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="CtrlProcDate" runat="server" Visible="false"></asp:Label>

        <asp:Label ID="lblId1" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblId2" runat="server" Text="" Visible="false"></asp:Label>

        <asp:Label ID="hdnPrmValue" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="hdnFuncOpt" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="hdnModule" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="hdnUserId" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="hdnCashCode" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="CtrlVoucherNo" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="CtrlProcStat" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblUnPostDataCr" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblUnPostDataDr" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="CtrlBalance" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="CtrlAvailBalance" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="hdnCuNumber" runat="server" Visible="false"></asp:Label>

        <asp:Label ID="CtrlAccStatus" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="CtrlAccType" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="CtrlTrnAccType" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblTrnCode" runat="server" Visible="false"></asp:Label>

        <asp:Label ID="lblcls" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblflag" runat="server" Visible="false"></asp:Label>

        <asp:Label ID="Searchflag" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="CtrlLadgerBalance" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="CtrlLienAmt" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="CtrlLoanSancAmt" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblAtyClass" runat="server" Visible="false"></asp:Label>

        <asp:Label ID="lblTrnAtyClass" runat="server" Visible="false"></asp:Label>

        <asp:Label ID="ValidityFlag" runat="server" Visible="false"></asp:Label>

   
        <asp:Label ID="lblTrnTypeTitle" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblFuncTitle" runat="server" Visible="false"></asp:Label>

        <asp:Label ID="lblBoothNo" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblBoothName" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblIDName" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblProcdate" runat="server" Text="" Visible="false"></asp:Label>

        <asp:Label ID="lblPayType" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblTrnPayType" runat="server" Text="" Visible="false"></asp:Label>


    </div>

</asp:Content>

