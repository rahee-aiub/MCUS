<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSLoanDistbursment.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSLoanDistbursment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />

    <%--<script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtTranDate.ClientID %>").dynDateTime({
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
            height: 200px;
            width: 520px;
            margin: 0 auto;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            width: 490px;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

    </script>





</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div align="left" style="background-color: #FFF8C6">
        <table>
            <tr>
                <td>
                    <table class="style1">

                        <thead>
                            <tr>
                                <th colspan="3">Daily Transaction - 
                                    <asp:Label ID="lblTransFunction" runat="server" Text="Label"></asp:Label>
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
                                    Font-Size="Medium"></asp:TextBox>
                                <asp:TextBox ID="txtHidden" runat="server" CssClass="cls text" Width="115px" Height="25px" Visible="false"
                                    Font-Size="Large"></asp:TextBox>

                                <asp:Button ID="BtnDelete" runat="server" Text="R" Font-Bold="True" Font-Size="Medium"
                                    ForeColor="White" CssClass="button green" OnClick="BtnDelete_Click" />&nbsp;
                  <asp:Label ID="lblLoanAppNo" runat="server" Text="Loan Application No:" Font-Size="Large"
                      ForeColor="Red"></asp:Label>
                                <asp:TextBox ID="txtLoanAppNo" runat="server" CssClass="cls text" Width="115px" Height="25px"
                                    Font-Size="Large" AutoPostBack="true" ToolTip="Enter No"
                                    OnTextChanged="txtLoanAppNo_TextChanged"></asp:TextBox>

                            </td>




                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCUNum" runat="server" Text="Credit Union No:" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCreditUNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium"></asp:TextBox>
                                <asp:Label ID="lblCUName" runat="server" Text="" Font-Size="Medium"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMemNo" runat="server" Text="Depositor No:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMemNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium"></asp:TextBox>
                                <asp:Label ID="lblMemName" runat="server" Text="" Font-Size="Medium"></asp:Label>
                            </td>
                        </tr>
                        <tr>

                            <td>
                                <asp:Label ID="lblGLCashCode" runat="server" Text="GL Cash Code:" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>

                            <td>
                                <asp:TextBox ID="txtGLCashCode" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" OnTextChanged="txtGLCashCode_TextChanged" AutoPostBack="True"></asp:TextBox>
                                <asp:DropDownList ID="ddlGLCashCode" runat="server" Height="25px" Width="275px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" OnSelectedIndexChanged="ddlGLCashCode_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>

                            </td>



                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTrnCode" runat="server" Text="Transaction Code:" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTrnCode" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium"></asp:TextBox>

                                <asp:Label ID="lblTrnCodeName" runat="server" Text="" Font-Size="Medium"></asp:Label>

                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="lblAccType" runat="server" Text="Account Type:" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAccType" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium"></asp:TextBox>
                                <asp:Label ID="lblAccNo" runat="server" Text="Account No:" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                                <asp:TextBox ID="txtAccNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium"></asp:TextBox>


                                <asp:Label ID="lblAccNoName" runat="server" Text="" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>


                            </td>
                        </tr>


                    </table>
                </td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <td>

                    <table class="style1">
                        <thead>
                            <tr>
                                <th colspan="3">Account Summary Informations
                                </th>
                            </tr>

                        </thead>
                        <tr>
                            <td>
                                <asp:Label ID="lblRec1" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblData1" runat="server" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRec2" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblData2" runat="server" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRec3" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblData3" runat="server" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRec4" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblData4" runat="server" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRec5" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblData5" runat="server" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="lblAType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblcls" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblFuncOpt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlFuncOpt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPayType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnMode" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlTrnLogic" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlRow" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnType1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnType2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnType3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPayType1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPayType2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPayType3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnMode1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnMode2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnMode3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnContraMode1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnContraMode2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnContraMode3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlLogicAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlShowInt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlGLAccNoDR" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlGLAccNoCR" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnCSGL" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlGLAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlGLAmount" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlGLDebitAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlGLCreditAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="txtIdNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlAccAtyClass" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlLadgerBalance" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPrincipal" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlOrgAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlAvailInterest" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlLastTrnDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTotalDeposit" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlMthDeposit" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlMaturityDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlAccStatus" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="txtVoucherNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>



    <table>
        <tr>
            <td>
                <table class="style1">
                    <tr>
                        <td>
                            <div align="left" style="background-color: #E0FFFF">
                                <table>
                                    <tr>
                                        <td>
                                            <h3>Trans.Type</h3>
                                        </td>
                                        <td>
                                            <h3>&nbsp;&nbsp;&nbsp;&nbsp;Description</h3>
                                        </td>
                                        <td>
                                            <h3>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Amount</h3>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>

                                            <asp:TextBox ID="txtTrnType1" runat="server" CssClass="cls text" AutoPostBack="True" Width="100px" Height="25px" Font-Size="Medium" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;

                    <asp:Label ID="lblPayDesc1" runat="server" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" CssClass="cls text" ForeColor="#FF3300"></asp:Label>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         
                        <asp:TextBox ID="txtAmount1" runat="server" Font-Size="Medium" CssClass="cls text" Width="110px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" OnTextChanged="txtAmount1_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTrnType2" runat="server" CssClass="cls text" Font-Size="Medium" Width="100px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" AutoPostBack="True"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblPayDesc2" runat="server" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" CssClass="cls text" ForeColor="#FF3300"></asp:Label>
                                        </td>

                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtAmount2" runat="server" Font-Size="Medium" CssClass="cls text" Width="110px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" AutoPostBack="True" OnTextChanged="txtAmount2_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTrnType3" runat="server" CssClass="cls text" Font-Size="Medium" Width="100px" Height="25px" AutoPostBack="True"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblPayDesc3" runat="server" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" CssClass="cls text" ForeColor="#FF3300"></asp:Label>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtAmount3" runat="server" Font-Size="Medium" Width="110px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="cls text" AutoPostBack="True" OnTextChanged="txtAmount3_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BtnAdd" runat="server" Text="Add" Font-Bold="True" Font-Size="Medium"
                                                ForeColor="White" CssClass="button green" OnClick="BtnAdd_Click" />&nbsp;
                    <asp:Button ID="BtnCancel" runat="server" Text="Cancel" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="27px" Width="83px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnCancel_Click" />&nbsp;
                                        </td>
                                    </tr>

                                </table>
                            </div>
                        </td>


                    </tr>
                </table>
            </td>
            <td>
                <div align="left" class="grid_scroll">
                    <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
                        AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvDetailInfo_RowDataBound">
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <Columns>

                            <asp:BoundField HeaderText="AccType" DataField="AccType" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="AccNo" DataField="AccNo" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Description" DataField="PayTypeDes" HeaderStyle-Width="200px" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Amount" DataField="GLAmount" HeaderStyle-Width="120px" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0,0.00}" />

                        </Columns>

                    </asp:GridView>
                </div>
            </td>
        </tr>



    </table>
    <div align="center">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblTotalAmt" runat="server" Text="TOTAL AMOUNT :" Font-Size="Medium" ForeColor="#FF6600"></asp:Label>

        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                <asp:Label ID="txtTotalAmt" runat="server" Font-Size="Medium" ForeColor="#FF6600"></asp:Label>
    </div>
    <table>
        <tr>
            <td></td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Medium"
                    ForeColor="White" ToolTip="Update Information" CssClass="button green" OnClientClick="return ValidationBeforeUpdate()"
                    OnClick="BtnUpdate_Click" />&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                <br />
            </td>
        </tr>
    </table>





</asp:Content>
