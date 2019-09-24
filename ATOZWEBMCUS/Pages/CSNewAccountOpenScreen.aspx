<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSNewAccountOpenScreen.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSNewAccountOpenScreen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Reverse information?');
        }

    </script>

    <script type="text/javascript">
        function closechildwindow() {
            window.opener.document.location.href = 'CSDailyBoothTransaction.aspx';
            window.close();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
   <%-- <body onunload="return closechildwindow()">--%>
 
        <br />
        <br />
        <div align="center">
            <table>
                <tr>
                    <td>
                        <table class="style1">

                            <thead>
                                <tr>
                                    <th colspan="3">New Account Informations

                                    </th>
                                </tr>

                            </thead>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblAccTitle" runat="server" Text="Account Title :" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAccType" runat="server" CssClass="cls text" Width="80px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Medium"></asp:TextBox>
                        &nbsp;&nbsp;
                        <asp:Label ID="lblAccName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblCuTitle" runat="server" Text="Credit Union :" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCuNumber" runat="server" CssClass="cls text" Width="80px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Medium"></asp:TextBox>
                        &nbsp;&nbsp;
                        <asp:Label ID="lblCuName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>


                <tr>
                    <td>
                        <asp:Label ID="lblMemTitile" runat="server" Text="Depositor :" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMemNo" runat="server" CssClass="cls text" Width="80px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Medium"></asp:TextBox>
                        &nbsp;&nbsp;
                        <asp:Label ID="lblMemName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>

                <br />
                <br />

                <tr>
                <td>
                    <asp:Label ID="lblFixedDepositAmount" runat="server" Text="Fixed Deposit Amount:" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFixedDepositAmount" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Medium"
                        AutoPostBack="True" OnTextChanged="txtFixedDepositAmount_TextChanged"
                        TabIndex="3"></asp:TextBox>
                </td>
            </tr>


                <tr>
                    <td>
                        <asp:Label ID="lblPeriod" runat="server" Text="Period(Month) :" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPeriod" runat="server" CssClass="cls text" Width="100px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Medium"  AutoPostBack="true" OnTextChanged="txtPeriod_TextChanged" ></asp:TextBox>
                        <%-- Font-Size="Medium"  AutoPostBack="true" OnTextChanged="txtPeriod_TextChanged" onkeydown="return (event.keyCode=13);"></asp:TextBox>--%>


                    </td>
                </tr>


                <tr>
                    <td>
                        <asp:Label ID="lblIntRate" runat="server" Text="Interest Rate :" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIntRate" runat="server" CssClass="cls text" Width="100px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                            Font-Size="Medium"></asp:TextBox>


                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblContractInt" runat="server" Text="Contract Int:" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtContractInt" runat="server" CssClass="cls text" Width="177px" Visible="false"
                            Height="25px" Font-Size="Medium"></asp:TextBox>
                        <asp:CheckBox ID="ChkContraInt" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblMaturityDate" runat="server" Text="Maturity Date :" Font-Size="Medium"
                            ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMaturityDate" runat="server" CssClass="cls text" Width="177px"
                            Height="25px" Font-Size="Medium"></asp:TextBox>
                        
                    </td>
                </tr>

                 <tr>
                <td>
                    <asp:Label ID="lblFixedMthInt" runat="server" Text="Interest Benefits:" Font-Size="Medium"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFixedMthInt" runat="server" CssClass="cls text" Width="177px"
                        Height="25px" Font-Size="Medium" TabIndex="16"></asp:TextBox>
                </td>
            </tr>
            </table>
        </div>
        <br />
        <br />



        <table>
            <tr>
                <td></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                <asp:Button ID="BtnOkay" runat="server" Text="Okay" Font-Bold="True" Font-Size="Medium"
                    ForeColor="White" CssClass="button green" OnClick="BtnOkay_Click" />&nbsp;
                    <asp:Button ID="BtnNominee" runat="server" Text="Nominee" Font-Bold="True" Font-Size="Medium"
                    ForeColor="White" CssClass="button green" OnClick="BtnNominee_Click" />&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>

        <asp:HiddenField ID="hdnID" runat="server" />
        <asp:HiddenField ID="hdnProcDate" runat="server" />
        <asp:HiddenField ID="hdnFunc" runat="server" />
        <asp:HiddenField ID="hdnModule" runat="server" />
        <asp:HiddenField ID="hdnVchNo" runat="server" />
        <asp:HiddenField ID="hdnCuNo" runat="server" />
        <asp:HiddenField ID="hdnCType" runat="server" />
        <asp:HiddenField ID="hdnCNo" runat="server" />
        <asp:HiddenField ID="hdnCuName" runat="server" />
        <asp:HiddenField ID="hdnNewAccNo" runat="server" />

        <asp:HiddenField ID="hdnNewMemberNo" runat="server" />
        <asp:HiddenField ID="hdnNewMemberName" runat="server" />

        <asp:HiddenField ID="hdnGLCashCode" runat="server" />
        <asp:HiddenField ID="hdnTrnCode" runat="server" />

        <asp:HiddenField ID="hdnAccType" runat="server" />
        <asp:HiddenField ID="hdnlblcls" runat="server" />

        <asp:HiddenField ID="hdnmemType" runat="server" />
        <asp:HiddenField ID="hdnTranDate" runat="server" />

        <asp:Label ID="CtrlTrnDate" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="ValidityFlag" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblAtyClass" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="CtrlBenefitDate" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="CtrlMatureAmt" runat="server" Text="" Visible="false"></asp:Label>
    


</asp:Content>

