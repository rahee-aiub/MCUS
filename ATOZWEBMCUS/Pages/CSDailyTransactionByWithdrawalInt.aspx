<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSDailyTransactionByWithdrawalInt.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSDailyTransactionByWithdrawalInt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />--%>

    <script language="javascript" type="text/javascript">
        $(function () {
            $("#<%= txtTranDate.ClientID %>").datepicker();

             var prm = Sys.WebForms.PageRequestManager.getInstance();

             prm.add_endRequest(function () {
                 $("#<%= txtTranDate.ClientID %>").datepicker();

            });

         });


    </script>



    <%--<script type="text/javascript">

        $(document).on("keydown", function (event) {
            if (event.keyCode === 8) {
                event.preventDefault();
            }
        });
    </script>

    
    <script type = "text/javascript">
        window.onload = function () {
            document.onkeydown = function (e) {
                return (e.which || e.keyCode) != 116;
            };
        }
</script>--%>





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
            /*width: 520px;*/
            width: 520px;
            margin: 0 auto;
        }

        .grid1_scroll {
            overflow: auto;
            height: 200px;        
            width: 820px;
            margin: 0 auto;
        }
        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            /*width: 490px;*/
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .auto-style1 {
            width: 225px;
        }

        .auto-style2 {
            height: 27px;
        }

        .auto-style3 {
            width: 225px;
            height: 27px;
        }
    </style>




    <script language="javascript" type="text/javascript">
        function ValidationBeforeAdd() {


            var txtCreditUNo = document.getElementById('<%=txtCreditUNo.ClientID%>').value;
            var txtMemNo = document.getElementById('<%=txtMemNo.ClientID%>').value;
            var txtGLCashCode = document.getElementById('<%=txtGLCashCode.ClientID%>').value;
            <%--var txtGLContraCode = document.getElementById('<%=txtGLContraCode.ClientID%>').value;--%>


            if (txtCreditUNo == '' || txtCreditUNo.length == 0)
                alert('Please Input Credit Union No.');
            else
                if (txtMemNo == '' || txtMemNo.length == 0)
                    alert('Please Input Depositor No.');
                else
                    if (txtGLCashCode == '' || txtGLCashCode.length == 0)
                        alert('Please Input GL Cash Code');
                    else
                        return;
            return false;



        }



    </script>


    <%--<script language="javascript" type="text/javascript">

        function DisableBackButton() {
            window.history.forward();
        }
        setTimeout("DisableBackButton()", 0);
        window.onunload = function () { null };

    </script>--%>



</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <%--   "background-color: #BBBBBB";
    "background-color: #00A5C4";--%>
    <%-- "background-color: #FFF8C6"--%>


    <div id="DivMainHeader" align="left" style="background-color: #FFF8C6">
        <table>
            <tr>
                <td>
                    <table class="style1">

                        <thead>
                            <tr>
                                <th colspan="3">
                                    <asp:Label ID="lblTransFunction" runat="server" Text="Label"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                    <%--<asp:Button ID="BtnHelpBankCode" runat="server" Text="Bank Code ?" Font-Size="Medium" ForeColor="blue"
                                    Font-Bold="True" CssClass="blue" ToolTip="Next" OnClick="BtnHelpBankCode_Click" />--%>


                                </th>
                            </tr>

                        </thead>


                        <tr>
                            <td>
                                <asp:Label ID="lblTranDate" runat="server" Text="Transaction Date:" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTranDate" runat="server" CssClass="cls text" Width="90px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" OnTextChanged="txtTranDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="BtnLV" runat="server" Text="LIVE TRANSACTION" Font-Size="Medium" ForeColor="white"
                                    Font-Bold="True" CssClass="button blue" OnClick="BtnLV_Click" Width="263px" BackColor="Blue" BorderStyle="Outset" />
                                <%--          &nbsp;
                                        <asp:Button ID="BtnBV" runat="server" Text="BV" Font-Size="Medium" ForeColor="Red"
                            Font-Bold="True" CssClass="button green" OnClick="BtnBV_Click" Width="50px" />
                                &nbsp;
                                <asp:Button ID="BtnBL" runat="server" Text="BL" Font-Size="Medium" ForeColor="white"
                            Font-Bold="True" CssClass="button red" OnClick="BtnBL_Click" Width="47px"/>

                                 &nbsp;&nbsp;<asp:TextBox ID="txtTranTitle" runat="server" CssClass="cls text" Width="255px" Height="25px"
                                    Font-Size="Large" BackColor="#3333FF" BorderColor="#66FF33" ForeColor="White"></asp:TextBox>--%>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  
                             
                                
                                
                               <%-- <asp:Label ID="lblOldCuNo" runat="server" Font-Size="Medium" Text="Old CU No. :" ForeColor="Red"></asp:Label>
                                        <asp:TextBox ID="txtOldCuNo" runat="server" CssClass="cls text" Width="100px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" AutoPostBack="True" OnTextChanged="txtOldCuNo_TextChanged"></asp:TextBox>--%>


                            </td>




                        </tr>
                        <tr>
                            <td>

                                <asp:Label ID="lblVchNo" runat="server" Font-Size="Medium" Text="Vch.No. :" ForeColor="Red"></asp:Label>



                            </td>
                            <td>
                                <asp:TextBox ID="txtVchNo" runat="server" TabIndex="1" CssClass="cls text" Width="150px" Height="25px" onkeydown="return (event.keyCode !=13);" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 
                                
                               <%-- <asp:Label ID="lblOldMemNo" runat="server" Font-Size="Medium" Text="Old Member No. :" ForeColor="Red"></asp:Label>
                                        <asp:TextBox ID="txtOldMemNo" runat="server" CssClass="cls text" Width="100px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" AutoPostBack="True" OnTextChanged="txtOldMemNo_TextChanged"></asp:TextBox>--%>

                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAccNo" runat="server" Text="Account No:" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>

                            </td>
                            <td>
                                <asp:TextBox ID="txtAccNo" runat="server" CssClass="cls text" TabIndex="2" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" AutoPostBack="True" ToolTip="Enter Code" onkeypress="return functionx(event)" OnTextChanged="txtAccNo_TextChanged"></asp:TextBox>
                                <asp:Label ID="lblAccTitle" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                <asp:Button ID="BtnSearch" runat="server" Text="Help" Font-Size="Medium" ForeColor="Red"
                                    Font-Bold="True" CssClass="button green" ToolTip="Help Account No." OnClick="BtnSearch_Click" />
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCUNum" runat="server" Text="Credit Union No:" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCreditUNo" runat="server" TabIndex="3" CssClass="cls text" Width="90px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" ToolTip="Enter Code" AutoPostBack="True" OnTextChanged="txtCreditUNo_TextChanged"></asp:TextBox>
                                <asp:CheckBox ID="chkOldSearch" runat="server" Font-Size="Medium" ForeColor="Red" Text="Old No. Search" />
                                <asp:Label ID="lblCuName" runat="server" Width="500px" Height="25px" Text=""></asp:Label>


                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMemNo" runat="server" Text="Depositor No:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMemNo" runat="server" TabIndex="4" CssClass="cls text" Width="90px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" ToolTip="Enter Code" onkeypress="return functionx(event)" AutoPostBack="True" OnTextChanged="txtMemNo_TextChanged"></asp:TextBox>
                               
                                <asp:Label ID="lblMemName" runat="server" Width="500px" Height="25px" Text=""></asp:Label>





                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTrnType" runat="server" Text="Transaction Type :" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList
                                    ID="ddlTrnType" runat="server" TabIndex="5" Height="25px" Width="90px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" OnSelectedIndexChanged="ddlTrnType_SelectedIndexChanged" AutoPostBack="True">
                                    <%--<asp:ListItem Value="1">Cash</asp:ListItem>
                                    <asp:ListItem Value="2">Bank</asp:ListItem>--%>
                                    <%--<asp:ListItem Value="3">Trf.</asp:ListItem>--%>
                                </asp:DropDownList>
                                &nbsp;&nbsp;
                               
                                    <asp:Label ID="lblChqNo" runat="server" Text="Cheque No. :" Font-Size="Medium" ForeColor="Red"></asp:Label>

                                <asp:TextBox ID="txtChqNo" runat="server" TabIndex="6" CssClass="cls text" MaxLength="15" Width="140px" Height="25px" onkeydown="return (event.keyCode !=13);" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium"></asp:TextBox>&nbsp;

                                <asp:Label ID="lblBalance" runat="server" Text="" Font-Size="Medium" ForeColor="Red"></asp:Label>

                                <asp:TextBox ID="txtBalance" runat="server" Style="text-align: right" Font-Bold="true" CssClass="cls text" Width="140px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium"></asp:TextBox>

                            </td>
                        </tr>

                        <%--<tr>
                            <td>
                                <asp:Label ID="lblTrfAcc" runat="server" Text="Trf. Credit A/C :" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTrfAcc" runat="server" TabIndex="7" CssClass="cls text" Width="100px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium"></asp:TextBox>
                              

                            </td>

                        </tr>--%>


                        <tr>
                            <td>
                                <asp:Label ID="lblGLCashCode" runat="server" Text="GL Cash Code:" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGLCashCode" runat="server" TabIndex="7" CssClass="cls text" Width="90px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" OnTextChanged="txtGLCashCode_TextChanged" AutoPostBack="True"></asp:TextBox>
                                <asp:DropDownList ID="ddlGLCashCode" runat="server" Height="25px" Width="409px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" OnSelectedIndexChanged="ddlGLCashCode_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>

                            </td>

                        </tr>

                        


                    </table>
                </td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <td>

                    <table class="style1">
                        <thead>
                            <th>
                                <asp:Button ID="BtnNext" runat="server" Text="->" Font-Size="Medium" ForeColor="blue"
                                    Font-Bold="True" CssClass="blue" ToolTip="Next" OnClick="BtnNext_Click" />

                                <th colspan="3">Account Summary Informations
                              
                                
                                <asp:Button ID="BtnCorrection" runat="server" Text="C" Font-Size="Medium" ForeColor="white"
                                    Font-Bold="True" CssClass="button red" ToolTip="Correction CPS Info." OnClick="BtnCorrection_Click" />
                                    <asp:Button ID="BtnCUpdate" runat="server" Text="U" Font-Size="Medium" ForeColor="Red"
                                        Font-Bold="True" CssClass="button green" ToolTip="Update CPS Info." OnClick="BtnCUpdate_Click" />

                                    <asp:Button ID="BtnCBack" runat="server" Text="<<" Font-Size="Medium" ForeColor="white"
                                        Font-Bold="True" CssClass="button red" ToolTip="Back" OnClick="BtnCBack_Click" />

                                </th>
                        </thead>
                        <tr>
                            <td>
                                <asp:Label ID="lblRmark1" runat="server" Font-Size="Medium" Visible="false"
                                    ForeColor="Black"></asp:Label>
                                <asp:Label ID="lblRec1" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox ID="lblData1" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="125px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:TextBox>&nbsp;&nbsp;
                            

                                <asp:Label ID="lblBalRec" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="lblBalData" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="125px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:TextBox>&nbsp;&nbsp;
                            </td>

                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="lblRmark2" runat="server" Font-Size="Medium" Visible="false"
                                    ForeColor="Black"></asp:Label>
                                <asp:Label ID="lblRec2" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox ID="lblData2" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="125px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:TextBox>&nbsp;&nbsp;
                              
                             
                                <asp:Label ID="lblUnPostCr" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblUnPostDataCr" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="125px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Red"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRec3" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox ID="lblData3" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="125px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:TextBox>&nbsp;&nbsp;
                                <asp:Label ID="lblUnPostDr" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblUnPostDataDr" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="125px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Red"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRmark4" runat="server" Font-Size="Medium" Visible="false"
                                    ForeColor="Black"></asp:Label>
                                <asp:Label ID="lblRec4" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox ID="lblData4" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="125px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:TextBox>&nbsp;&nbsp;
                                <asp:Label ID="lblRmark11" runat="server" Font-Size="Medium" Visible="false"
                                    ForeColor="Black"></asp:Label>
                                <asp:Label ID="lblRec11" runat="server" Text="" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>

                            </td>

                            <td>
                                <asp:TextBox ID="lblData11" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="125px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:TextBox>

                            </td>

                        </tr>
                        <tr>
                            <td class="auto-style2">
                                <asp:Label ID="lblRmark5" runat="server" Font-Size="Medium" Visible="false"
                                    ForeColor="Black"></asp:Label>
                                <asp:Label ID="lblRec5" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style3">
                                <asp:TextBox ID="lblData5" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="125px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:TextBox>&nbsp;&nbsp;
                                <asp:Label ID="lblRec9" runat="server" Text="" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>

                            </td>

                            <td>
                                <asp:TextBox ID="lblData9" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="125px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRmark6" runat="server" Font-Size="Medium" Visible="false"
                                    ForeColor="Black"></asp:Label>
                                <asp:Label ID="lblRec6" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox ID="lblData6" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="125px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:TextBox>&nbsp;&nbsp;
                                <asp:Label ID="lblRec10" runat="server" Text="" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>

                            </td>
                            <td>
                                <asp:TextBox ID="lblData10" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="125px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRmark7" runat="server" Font-Size="Medium" Visible="false"
                                    ForeColor="Black"></asp:Label>
                                <asp:Label ID="lblRec7" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox ID="lblData7" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="125px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:TextBox>&nbsp;&nbsp;
                                <asp:Label ID="lblRec8" runat="server" Text="" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="lblData8" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="125px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:TextBox>

                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
        </table>
    </div>





    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <div id="Dtl1" runat="server" align="left" style="background-color: #E0FFFF">
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

                                            <asp:TextBox ID="txtTrnType1" runat="server" CssClass="cls text" AutoPostBack="True" OnTextChanged="txtTrnType1_TextChanged" Width="100px" Height="25px" Font-Size="Medium" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;

                                            <asp:TextBox ID="txtPayDesc1" runat="server" Style="text-align: left" Font-Size="Medium" CssClass="cls text" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>

                                            <%--<asp:Label ID="lblPayDesc1" runat="server" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" CssClass="cls text" ForeColor="#FF3300"></asp:Label>--%>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         
                        <asp:TextBox ID="txtAmount1" runat="server" Style="text-align: right" Font-Size="Medium" TabIndex="9" CssClass="cls text" Width="110px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" OnTextChanged="txtAmount1_TextChanged" AutoPostBack="True" onFocus="javascript:this.select();" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTrnType2" runat="server" CssClass="cls text" Font-Size="Medium" Width="100px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" AutoPostBack="True" OnTextChanged="txtTrnType2_TextChanged"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtPayDesc2" runat="server" Style="text-align: left" Font-Size="Medium" CssClass="cls text" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>
                                            <%-- <asp:Label ID="lblPayDesc2" runat="server" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" CssClass="cls text" ForeColor="#FF3300"></asp:Label>--%>
                                        </td>

                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtAmount2" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" TabIndex="10" Width="110px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" AutoPostBack="True" OnTextChanged="txtAmount2_TextChanged" onFocus="javascript:this.select();" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTrnType3" runat="server" CssClass="cls text" Font-Size="Medium" Width="100px" Height="25px" AutoPostBack="True" OnTextChanged="txtTrnType3_TextChanged"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:TextBox ID="txtPayDesc3" runat="server" Style="text-align: left" Font-Size="Medium" CssClass="cls text" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>
                                            <%--<asp:Label ID="lblPayDesc3" runat="server" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" CssClass="cls text" ForeColor="#FF3300"></asp:Label>--%>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtAmount3" runat="server" Style="text-align: right" Font-Size="Medium" TabIndex="11" Width="110px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="cls text" AutoPostBack="True" OnTextChanged="txtAmount3_TextChanged" onFocus="javascript:this.select();" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTrnType4" runat="server" CssClass="cls text" Font-Size="Medium" Width="100px" Height="25px" AutoPostBack="True" OnTextChanged="txtTrnType4_TextChanged"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtPayDesc4" runat="server" Style="text-align: left" Font-Size="Medium" CssClass="cls text" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>
                                            <%-- <asp:Label ID="lblPayDesc4" runat="server" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" CssClass="cls text" ForeColor="#FF3300"></asp:Label>--%>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtAmount4" runat="server" Style="text-align: right" Font-Size="Medium" TabIndex="12" Width="110px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="cls text" AutoPostBack="True" OnTextChanged="txtAmount4_TextChanged" onFocus="javascript:this.select();" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BtnAdd" runat="server" TabIndex="13" Text="Add" Font-Bold="True" Font-Size="Medium" Height="27px" Width="83px"
                                                ForeColor="White" CssClass="button green" OnClick="BtnAdd_Click" />&nbsp;
                    <asp:Button ID="BtnCancel" runat="server" Text="Cancel" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="27px" Width="83px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnCancel_Click" />&nbsp;
                                            
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="lbltotAmt" runat="server" Text="Net Amount :" Font-Size="large" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                <asp:Label ID="txttotAmt" runat="server" Font-Size="large" Font-Bold="true" ForeColor="Blue"></asp:Label>


                                        </td>
                                    </tr>

                                </table>
                            </div>

                            <div id="Dtl2" runat="server" align="left" class="grid1_scroll">

                                <asp:GridView ID="gvGroupAccInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
                                    AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvGroupAccInfo_RowDataBound" OnSelectedIndexChanged="gvGroupAccInfo_SelectedIndexChanged">
                                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                    <Columns>


                                        <asp:TemplateField HeaderText="Acc Type" HeaderStyle-Width="70px" ItemStyle-Width="95px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="AccType" runat="server" Text='<%#Eval("AccType") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Acc Title" HeaderStyle-Width="240px" ItemStyle-Width="300px" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="TrCodeDesc" runat="server" Text='<%#Eval("TrnCodeDesc") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField HeaderText="New A/c" DataField="AccNo" HeaderStyle-Width="135px" ItemStyle-Width="140px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />

                                        <asp:TemplateField HeaderText="Old A/c" HeaderStyle-Width="73px" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="AccNo" runat="server" Text='<%#Eval("AccOldNumber") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField HeaderText="Org/Instl Amt." DataField="AccOrgInstlAmt" HeaderStyle-Width="108px" ItemStyle-Width="140px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0,0.00}" />
                                        <%--  <asp:BoundField HeaderText="Ledger Bal." DataField="AccBalance" HeaderStyle-Width="118px" ItemStyle-Width="140px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0,0.00}" />--%>

                                        <asp:BoundField HeaderText="Trf. A/c" DataField="AccTrfAccNo" HeaderStyle-Width="100px" ItemStyle-Width="140px" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="center" />

                                        <asp:TemplateField HeaderStyle-Width="52px" ItemStyle-Width="60px">
                                            <ItemTemplate>
                                                <asp:LinkButton Text="Select" ID="lnkSelect" runat="server" CommandName="Select" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>



                            </div>
                        </td>


                    </tr>
                </table>
            </td>
            <td>
                <div align="left" class="grid_scroll">
                    <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
                        AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvDetailInfo_RowDataBound" OnRowDeleting="gvDetailInfo_RowDeleting">
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <Columns>
                            <asp:TemplateField HeaderText="Id" Visible="false" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="AccType" HeaderStyle-Width="70px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="AccType" runat="server" Text='<%#Eval("AccType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="AccNo" HeaderStyle-Width="60px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="AccNo" runat="server" Text='<%#Eval("AccNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:BoundField HeaderText="AccType" DataField="AccType" HeaderStyle-Width="63px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="AccNo" DataField="AccNo" HeaderStyle-Width="70px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />--%>
                            <asp:BoundField HeaderText="Description" DataField="PayTypeDes" HeaderStyle-Width="210px" ItemStyle-Width="270px" ItemStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Amount" DataField="Amount" HeaderStyle-Width="105px" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0,0.00}" />
                            <asp:TemplateField HeaderText="Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="TrnTypeCode" runat="server" Text='<%#Eval("TrnTypeCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="TrnCode" runat="server" Text='<%#Eval("TrnCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="TrnPayment" runat="server" Text='<%#Eval("TrnPayment") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:ButtonField CommandName="Delete" Text="Delete" HeaderStyle-Width="100px" ItemStyle-Width="50px"/>--%>
                            <asp:ButtonField CommandName="Delete" Text="Delete" HeaderStyle-Width="60px" ItemStyle-Width="60px" />
                        </Columns>

                    </asp:GridView>
                </div>
            </td>
        </tr>



    </table>
    <div align="center">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblTotalAmt" runat="server" Text="TOTAL AMOUNT :" Font-Size="large" Font-Bold="true" BorderColor="Blue" ForeColor="#FF6600"></asp:Label>

        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                <asp:Label ID="txtTotalAmt" runat="server" Font-Size="large" Font-Bold="true" ForeColor="#FF6600"></asp:Label>
    </div>
    <table>
        <tr>
            <%-- <td></td>--%>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Medium" Height="27px" Width="86px"
                    ForeColor="White" ToolTip="Update Information" CssClass="button green" OnClientClick="return ValidationBeforeUpdate()"
                    OnClick="BtnUpdate_Click" />&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                <br />
            </td>
        </tr>
    </table>


    <asp:Label ID="lblATypeMode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblcls" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccDepRoundingBy" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuStatus" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlFuncOpt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPayType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnMode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnRecDesc" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlRecMode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnSelectTrnType" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblAccountType" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblTrnCode" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblFuncOpt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblOrgFuncOpt" runat="server" Text="" Visible="false"></asp:Label>


    <asp:Label ID="CtrlRecMode1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlRecMode2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlRecMode3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlRecMode4" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnLogic" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlRow" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlTrnType1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnType2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnType3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnType4" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPayType1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPayType2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPayType3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPayType4" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlTrnMode1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnMode2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnMode3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnMode4" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnContraMode1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnContraMode2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnContraMode3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnContraMode4" runat="server" Text="" Visible="false"></asp:Label>

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
    <asp:Label ID="CtrlMsgFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnCuFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnMemFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnAccFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnGrpFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnProcDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnVchNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCuName" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnNewMemberNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnNewMemberName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnMemType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnGLCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnTrnCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnAccType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnlblcls" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnPeriod" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnIntRate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnContractInt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnRecValue" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnGLSubHead" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlLadgerBalance" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlSancAmount" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlDisbAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPrincipal" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlOrgAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlAvailLimit" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlAvailBal" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlAvailInterest" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlProvBenefit" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlCalProvDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlFixedMthInt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlCertNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlRenwlDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlRenwlAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlIntRate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlValidAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlValidAmt1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlValidAmt2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlValidAmt3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlValidAmt4" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlLienAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPeriod" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlMainAmt" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlTrnPayment" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnPayment1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnPayment2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnPayment3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnPayment4" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlCalPrincAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlCalIntAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlCalPenalAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPenalAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPrevDuePrincAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPrevDueIntAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPaidPrincAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPaidIntAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPaidPenalAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlDuePrincAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlDueIntAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlDueIntFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlCurrIntAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlCalDepositAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPrevDueDepositAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPaidDepositAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlDueDepositAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlCurrDueDepositAmt" runat="server" Text="" Visible="false"></asp:Label>


    <asp:Label ID="CtrlTranAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlIntFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlPenalFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlHoldIntFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlBenefitFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlInterestAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlLastTrnDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTotalDeposit" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlMthDeposit" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlInstlAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlMaturityDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlOpenDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlAccStatus" runat="server" Text="" Visible="false"></asp:Label>


    <asp:Label ID="CtrlVoucherNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlProcStat" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="txtVoucherNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlGLAType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlRecType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="TotalWithdrawal" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlNetValidAmt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlProvAdjCr" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlProvAdjDr" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlProcDate" runat="server" Text="" Visible="false"></asp:Label>


    <asp:Label ID="hdnCalIntRate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCalFDAmount" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCalNofDays" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCalFDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCalOrgInt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCalPaidInt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCalPeriod" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnAvailBenefit" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnAdjProvBenefit" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlPrmValue" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlModule" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblModule" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnContraCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnContraHead1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnContraHead2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnContraHead3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnContraHead4" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCuNumber" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnAccMulty" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblTotRec" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlContraAType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlBackUpStat" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblflag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblVNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTranDt" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblFuncTitle" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTrnTypeTitle" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblVchMemNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblVchMemName" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblTrancode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblNoMonths" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblCalFromDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCalTillDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTrnDesc" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblAccTypeMode" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblBoothNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblBoothName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblIDName" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblGLAccBal" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblGLBalanceType" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="ErrMsg" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblUptoMonth" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblUptoYear" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblGLCode" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblUptoDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblDepositFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="NoRoundingBy" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="RoundingByFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblMemType" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblMatureAmt" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblTargetDeposit" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTransactionMode" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlAdjBenefitCr" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblBenefitMode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CashBenefitFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="glMainHead" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="ProvAdjFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlBenefitFlag1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlBenefitFlag2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlBenefitFlag3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlBenefitFlag4" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlSelectAccNo" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="InputModule" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblVchType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlProvAmt" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblSkipFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblStatus" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblAccMode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblVPrintFlag" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblFrontHelp" runat="server" Text="" Visible="false"></asp:Label>
    
    <asp:Label ID="lblAccountNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAccTrfAccNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblType" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>

