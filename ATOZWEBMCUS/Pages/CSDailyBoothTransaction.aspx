<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSDailyBoothTransaction.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSDailyBoothTransaction" %>

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
            /*width: 520px;*/
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



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div align="left" style="background-color: #FFF8C6">
        <table>
            <tr>
                <td>
                    <table class="style1">

                        <thead>
                            <tr>
                                <th colspan="3">
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
                                    Font-Size="Medium" OnTextChanged="txtTranDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                                <asp:TextBox ID="txtHidden" runat="server" CssClass="cls text" Width="115px" Height="25px" Visible="false"
                                    Font-Size="Large"></asp:TextBox>

                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  
                             
                                
                                
                                <asp:Label ID="lblOldCuNo" runat="server" Font-Size="Medium" Text="Old CU No. :" ForeColor="Red"></asp:Label>
                                <asp:TextBox ID="txtOldCuNo" runat="server" CssClass="cls text" Width="100px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" AutoPostBack="True" OnTextChanged="txtOldCuNo_TextChanged"></asp:TextBox>

                            </td>




                        </tr>

                        <tr>
                            <td>

                               <asp:Label ID="lblVchNo" runat="server" Font-Size="Medium" Text="Vch.No. :" ForeColor="Red"></asp:Label>
                               
                                

                                </td>
                            <td>
                                 <asp:TextBox ID="txtVchNo" runat="server" CssClass="cls text" Width="90px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium"></asp:TextBox> 
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 
                                
                                <asp:Label ID="lblOldMemNo" runat="server" Font-Size="Medium" Text="Old Member No. :" ForeColor="Red"></asp:Label>
                                <asp:TextBox ID="txtOldMemNo" runat="server" CssClass="cls text" Width="100px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" AutoPostBack="True" OnTextChanged="txtOldMemNo_TextChanged" ></asp:TextBox>

                            </td>

                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="lblCUNum" runat="server" Text="Credit Union No:" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCreditUNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtCreditUNo_TextChanged"></asp:TextBox>
                                <asp:Label ID="lblCuName" runat="server" Text=""></asp:Label>
                                

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMemNo" runat="server" Text="Depositor No:" Font-Size="Medium" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMemNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" ToolTip="Enter Code" onkeypress="return functionx(event)" AutoPostBack="True"
                                    OnTextChanged="txtMemNo_TextChanged"></asp:TextBox>
                                <asp:Label ID="lblMemName" runat="server" Text=""></asp:Label>
                                

                                &nbsp;&nbsp;

                                <asp:Button ID="BtnNewMem" runat="server" Text="New" Font-Bold="True" Font-Size="Medium"
                                    ForeColor="White" CssClass="button green" OnClick="BtnNewMem_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="NewMemLabel" runat="server" Text="" ForeColor="green" Font-Size="large" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTrnType" runat="server" Text="Transaction Type :" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList
                                    ID="ddlTrnType" runat="server" Height="25px" Width="120px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" OnSelectedIndexChanged="ddlTrnType_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="1">Cash</asp:ListItem>
                                    <asp:ListItem Value="2">Cheque</asp:ListItem>
                                    <asp:ListItem Value="3">Trf.</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                               
                                    <asp:Label ID="lblChqNo" runat="server" Text="Cheque No. :" Font-Size="Medium" ForeColor="Red"></asp:Label>


                                <asp:TextBox ID="txtChqNo" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium"></asp:TextBox>
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
                                <asp:DropDownList ID="ddlGLCashCode" runat="server" Height="25px" Width="409px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" OnSelectedIndexChanged="ddlGLCashCode_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>

                            </td>

                        </tr>

                        <tr>

                            <td>
                                <asp:Label ID="lblGLContraCode" runat="server" Text="GL Contra Code:" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGLContraCode" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" AutoPostBack="True" OnTextChanged="txtGLContraCode_TextChanged"></asp:TextBox>
                                <asp:DropDownList ID="ddlGLContraCode" runat="server" Height="25px" Width="408px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" AutoPostBack="True" OnSelectedIndexChanged="ddlGLContraCode_SelectedIndexChanged">
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
                                    Font-Size="Medium" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtTrnCode_TextChanged"></asp:TextBox>
                                <asp:Label ID="lblTrnCodeName" runat="server" Text=""></asp:Label>
                                <asp:DropDownList ID="ddlTrnCode" runat="server" Height="25px" Width="409px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" OnSelectedIndexChanged="ddlTrnCode_SelectedIndexChanged">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="lblAccType" runat="server" Text="Account Type:" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAccType" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" AutoPostBack="true" ToolTip="Enter Code" ReadOnly="true" ></asp:TextBox>
                                <asp:Label ID="lblAccNo" runat="server" Text="Account No:" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                                <asp:TextBox ID="txtAccNo" runat="server" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" AutoPostBack="True" ToolTip="Enter Code" onkeypress="return functionx(event)" OnTextChanged="txtAccNo_TextChanged"></asp:TextBox>
                                <asp:DropDownList ID="ddlAccNo" runat="server" Height="25px" Width="196px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Medium" OnSelectedIndexChanged="ddlAccNo_SelectedIndexChanged">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                </asp:DropDownList>

                                &nbsp;&nbsp;
                                <asp:Button ID="BtnNewAcc" runat="server" Text="New" Font-Bold="True" Font-Size="Medium"
                                    ForeColor="White" CssClass="button green" OnClick="BtnNewAcc_Click" />

                                &nbsp;&nbsp;
                                <asp:Label ID="NewAccLabel" runat="server" Text=""  ForeColor="green" Font-Size="large" Font-Bold="true"></asp:Label>

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
                            <td class="auto-style1">
                                <asp:Label ID="lblData1" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>&nbsp;&nbsp;
                            

                                <asp:Label ID="lblBalRec" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblBalData" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>&nbsp;&nbsp;
                            </td>

                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="lblRec2" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:Label ID="lblData2" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>&nbsp;&nbsp;
                              
                             
                                <asp:Label ID="lblUnPostCr" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblUnPostDataCr" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Red"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRec3" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:Label ID="lblData3" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblUnPostDr" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblUnPostDataDr" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Red"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRec4" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:Label ID="lblData4" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblRec11" runat="server" Text="" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>

                            <td>
                                <asp:Label ID="lblData11" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">
                                <asp:Label ID="lblRec5" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style3">
                                <asp:Label ID="lblData5" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblRec9" runat="server" Text="" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>

                            </td>

                            <td>
                                <asp:Label ID="lblData9" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRec6" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:Label ID="lblData6" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblRec10" runat="server" Text="" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>

                            </td>
                            <td>
                                <asp:Label ID="lblData10" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRec7" runat="server" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:Label ID="lblData7" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblRec8" runat="server" Text="" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblData8" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    ForeColor="Black"></asp:Label>

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

                                            <asp:TextBox ID="txtTrnType1" runat="server" CssClass="cls text" AutoPostBack="True" OnTextChanged="txtTrnType1_TextChanged" Width="100px" Height="25px" Font-Size="Medium" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;

                    <asp:TextBox ID="txtPayDesc1" runat="server" Style="text-align: left" Font-Size="Medium" CssClass="cls text" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>
                                           <%-- <asp:Label ID="lblPayDesc1" runat="server" Width="481px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Medium" CssClass="cls text" ForeColor="#FF3300"></asp:Label>--%>
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         
                        <asp:TextBox ID="txtAmount1" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="110px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" onchange="javascript:this.value=Comma(this.value);" OnTextChanged="txtAmount1_TextChanged" AutoPostBack="True"></asp:TextBox>
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
                    <asp:TextBox ID="txtAmount2" runat="server" Style="text-align: right" Font-Size="Medium" CssClass="cls text" Width="110px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" onchange="javascript:this.value=Comma(this.value);" AutoPostBack="True" OnTextChanged="txtAmount2_TextChanged"></asp:TextBox>
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
                        <asp:TextBox ID="txtAmount3" runat="server" Style="text-align: right" Font-Size="Medium" Width="110px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="cls text" onchange="javascript:this.value=Comma(this.value);" AutoPostBack="True" OnTextChanged="txtAmount3_TextChanged"></asp:TextBox>
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
                        <asp:TextBox ID="txtAmount4" runat="server" Style="text-align: right" Font-Size="Medium" Width="110px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" CssClass="cls text" AutoPostBack="True" OnTextChanged="txtAmount4_TextChanged"></asp:TextBox>
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
                                            <asp:Button ID="BtnViewImage" runat="server" Text="View Image" Font-Bold="True" Font-Size="Medium"
                                                ForeColor="White" CssClass="button green" OnClick="BtnViewImage_Click" />
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
        <asp:Label ID="lblTotalAmt" runat="server" Text="TOTAL AMOUNT :" Font-Size="large" Font-Bold="true" ForeColor="#FF6600"></asp:Label>

        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                <asp:Label ID="txtTotalAmt" runat="server" Font-Size="large" Font-Bold="true" ForeColor="#FF6600"></asp:Label>
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








   



    <asp:HiddenField ID="lblAType" runat="server" />
    <asp:HiddenField ID="lblATypeMode" runat="server" />
    <asp:HiddenField ID="lblcls" runat="server" />
    <asp:HiddenField ID="hdnAccDepRoundingBy" runat="server" />
    <asp:HiddenField ID="lblCuType" runat="server" />
    <asp:HiddenField ID="lblCuNo" runat="server" />
    <asp:HiddenField ID="lblCuStatus" runat="server" />

    <asp:HiddenField ID="OrgFuncOpt" runat="server" />
    <asp:HiddenField ID="lblFuncOpt" runat="server" />
    <asp:HiddenField ID="CtrlFuncOpt" runat="server" />
    <asp:HiddenField ID="CtrlPayType" runat="server" />
    <asp:HiddenField ID="CtrlTrnType" runat="server" />
    <asp:HiddenField ID="CtrlTrnMode" runat="server" />
    <asp:HiddenField ID="CtrlTrnRecDesc" runat="server" />
    <asp:HiddenField ID="CtrlRecMode" runat="server" />

    <asp:HiddenField ID="hdnSelectTrnType" runat="server" />


    
    <asp:HiddenField ID="CtrlRecMode1" runat="server" />
    <asp:HiddenField ID="CtrlRecMode2" runat="server" />
    <asp:HiddenField ID="CtrlRecMode3" runat="server" />
    <asp:HiddenField ID="CtrlRecMode4" runat="server" />
    <asp:HiddenField ID="CtrlTrnLogic" runat="server" />
    <asp:HiddenField ID="CtrlRow" runat="server" />
    <asp:HiddenField ID="CtrlTrnType1" runat="server" />

    <asp:HiddenField ID="CtrlTrnType2" runat="server" />
    <asp:HiddenField ID="CtrlTrnType3" runat="server" />
    <asp:HiddenField ID="CtrlTrnType4" runat="server" />
    <asp:HiddenField ID="CtrlPayType1" runat="server" />
    <asp:HiddenField ID="CtrlPayType2" runat="server" />
    <asp:HiddenField ID="CtrlPayType3" runat="server" />
    <asp:HiddenField ID="CtrlPayType4" runat="server" />

    <asp:HiddenField ID="CtrlTrnMode1" runat="server" />
    <asp:HiddenField ID="CtrlTrnMode2" runat="server" />
    <asp:HiddenField ID="CtrlTrnMode3" runat="server" />
    <asp:HiddenField ID="CtrlTrnMode4" runat="server" />
    <asp:HiddenField ID="CtrlTrnContraMode1" runat="server" />
    <asp:HiddenField ID="CtrlTrnContraMode2" runat="server" />
    <asp:HiddenField ID="CtrlTrnContraMode3" runat="server" />

    <asp:HiddenField ID="CtrlTrnContraMode4" runat="server" />
    <asp:HiddenField ID="CtrlLogicAmt" runat="server" />
    <asp:HiddenField ID="CtrlShowInt" runat="server" />
    <asp:HiddenField ID="CtrlGLAccNoDR" runat="server" />
    <asp:HiddenField ID="CtrlGLAccNoCR" runat="server" />
    <asp:HiddenField ID="CtrlTrnCSGL" runat="server" />
    <asp:HiddenField ID="CtrlTrnFlag" runat="server" />
    <asp:HiddenField ID="CtrlGLAccNo" runat="server" />
    <asp:HiddenField ID="CtrlGLAmount" runat="server" />
    <asp:HiddenField ID="CtrlGLDebitAmt" runat="server" />
    <asp:HiddenField ID="CtrlGLCreditAmt" runat="server" />
    <asp:HiddenField ID="txtIdNo" runat="server" />
    <asp:HiddenField ID="CtrlAccAtyClass" runat="server" />
    <asp:HiddenField ID="CtrlMsgFlag" runat="server" />

    
    <asp:HiddenField ID="hdnCuFlag" runat="server" />

    <asp:HiddenField ID="hdnMemFlag" runat="server" />
    <asp:HiddenField ID="hdnAccFlag" runat="server" />
    <asp:HiddenField ID="hdnVchNo" runat="server" />
    <asp:HiddenField ID="hdnCuType" runat="server" />
    <asp:HiddenField ID="hdnCuNo" runat="server" />
    <asp:HiddenField ID="hdnCuName" runat="server" />

    <asp:HiddenField ID="hdnNewMemberNo" runat="server" />
    <asp:HiddenField ID="hdnNewMemberName" runat="server" />
    <asp:HiddenField ID="hdnMemType" runat="server" />
    <asp:HiddenField ID="hdnNewAccNo" runat="server" />


    <asp:HiddenField ID="hdnGLCashCode" runat="server" />

    <asp:HiddenField ID="hdnTrnCode" runat="server" />
    <asp:HiddenField ID="hdnAccType" runat="server" />
    <asp:HiddenField ID="hdnlblcls" runat="server" />

    <asp:HiddenField ID="hdnPeriod" runat="server" />
    <asp:HiddenField ID="hdnIntRate" runat="server" />
    <asp:HiddenField ID="hdnContractInt" runat="server" />

    <asp:HiddenField ID="hdnOpenDate" runat="server" />
    <asp:HiddenField ID="hdnMaturityDate" runat="server" />
    <asp:HiddenField ID="hdnMaturityAmt" runat="server" />


    <asp:HiddenField ID="hdnBenefitDate" runat="server" />
    <asp:HiddenField ID="hdnFixedDepAmt" runat="server" />
    <asp:HiddenField ID="hdnFixedMthInt" runat="server" />
    <asp:HiddenField ID="hdnTranDate" runat="server" />


    <asp:HiddenField ID="hdnRecValue" runat="server" />
    <asp:HiddenField ID="hdnGLSubHead" runat="server" />



    <asp:HiddenField ID="CtrlLadgerBalance" runat="server" />
    <%--<asp:Label ID="CtrlLadgerBalance" runat="server" Text="" Visible="false"></asp:Label>--%>
    <%--<asp:Label ID="CtrlSancAmount" runat="server" Text="" Visible="false"></asp:Label>--%>
    <asp:HiddenField ID="CtrlSancAmount" runat="server" />
    <asp:HiddenField ID="CtrlDisbAmt" runat="server" />
    <asp:HiddenField ID="CtrlPrincipal" runat="server" />
    <asp:HiddenField ID="CtrlOrgAmt" runat="server" />
    <asp:HiddenField ID="CtrlAvailLimit" runat="server" />
    <asp:HiddenField ID="CtrlAvailBal" runat="server" />
    <asp:HiddenField ID="CtrlAvailInterest" runat="server" />
    <asp:HiddenField ID="CtrlAvailBenefit" runat="server" />
    <asp:HiddenField ID="CtrlProvBenefit" runat="server" />

    <asp:HiddenField ID="CtrlCertNo" runat="server" />
    
    <asp:HiddenField ID="CtrlCurrIntAmt" runat="server" />

    <asp:HiddenField ID="CtrlRenwlDate" runat="server" />
    <asp:HiddenField ID="CtrlRenwlAmt" runat="server" />

    <asp:HiddenField ID="CtrlIntRate" runat="server" />
    <asp:HiddenField ID="CtrlValidAmt" runat="server" />
    <asp:HiddenField ID="CtrlValidAmt1" runat="server" />
    <asp:HiddenField ID="CtrlValidAmt2" runat="server" />
    <asp:HiddenField ID="CtrlValidAmt3" runat="server" />
    <asp:HiddenField ID="CtrlValidAmt4" runat="server" />
    <asp:HiddenField ID="CtrlLienAmt" runat="server" />
    <asp:HiddenField ID="CtrlPeriod" runat="server" />
    <asp:HiddenField ID="CtrlMainAmt" runat="server" />

     <asp:HiddenField ID="CtrlTrnPayment" runat="server" />

    <asp:HiddenField ID="CtrlCalPrincAmt" runat="server" />
    <asp:HiddenField ID="CtrlCalIntAmt" runat="server" />
    <asp:HiddenField ID="CtrlCalPenalAmt" runat="server" />
    <asp:HiddenField ID="CtrlPrevDuePrincAmt" runat="server" />
    <asp:HiddenField ID="CtrlPrevDueIntAmt" runat="server" />
    <asp:HiddenField ID="CtrlPaidPrincAmt" runat="server" />
    <asp:HiddenField ID="CtrlPaidIntAmt" runat="server" />
    <asp:HiddenField ID="CtrlPaidPenalAmt" runat="server" />
    <asp:HiddenField ID="CtrlDuePrincAmt" runat="server" />
    <asp:HiddenField ID="CtrlDueIntAmt" runat="server" />

    <asp:HiddenField ID="CtrlCalDepositAmt" runat="server" />  
    <asp:HiddenField ID="CtrlPrevDueDepositAmt" runat="server" />
    <asp:HiddenField ID="CtrlPaidDepositAmt" runat="server" />
    <asp:HiddenField ID="CtrlDueDepositAmt" runat="server" />

    
    <asp:HiddenField ID="CtrlTranAmt" runat="server" />
    <asp:HiddenField ID="CtrlFlag" runat="server" />
    <asp:HiddenField ID="CtrlIntFlag" runat="server" />
    <asp:HiddenField ID="CtrlBenefitFlag" runat="server" />
    <asp:HiddenField ID="CtrlInterestAmt" runat="server" />
    <asp:HiddenField ID="CtrlLastTrnDate" runat="server" />
    <asp:HiddenField ID="CtrlTotalDeposit" runat="server" />
    <asp:HiddenField ID="CtrlMthDeposit" runat="server" />
    <asp:HiddenField ID="CtrlInstlAmt" runat="server" />
    <asp:HiddenField ID="CtrlMaturityDate" runat="server" />
    <asp:HiddenField ID="CtrlOpenDate" runat="server" />
    <asp:HiddenField ID="CtrlAccStatus" runat="server" />

    <asp:HiddenField ID="CtrlVoucherNo" runat="server" />
    <asp:HiddenField ID="CtrlProcStat" runat="server" />
    <asp:HiddenField ID="txtVoucherNo" runat="server" />
    <asp:HiddenField ID="CtrlPrmValue" runat="server" />
    <asp:HiddenField ID="CtrlModule" runat="server" />
    <asp:HiddenField ID="lblModule" runat="server" />
    <asp:HiddenField ID="CtrlGLAType" runat="server" />
    <asp:HiddenField ID="CtrlRecType" runat="server" />
    <asp:HiddenField ID="TotalWithdrawal" runat="server" />
    <asp:HiddenField ID="CtrlNetValidAmt" runat="server" />
    <asp:HiddenField ID="CtrlProvAdjCr" runat="server" />
    <asp:HiddenField ID="CtrlProvAdjDr" runat="server" />
    <asp:HiddenField ID="CtrlProcDate" runat="server" />

    <asp:HiddenField ID="hdnCalIntRate" runat="server" />
    <asp:HiddenField ID="hdnCalFDAmount" runat="server" />
    <asp:HiddenField ID="hdnCalNofDays" runat="server" />
    <asp:HiddenField ID="hdnCalFDate" runat="server" />
    <asp:HiddenField ID="hdnCalOrgInt" runat="server" />
    <asp:HiddenField ID="hdnCalPaidInt" runat="server" />
    <asp:HiddenField ID="hdnCalPeriod" runat="server" />
    <asp:HiddenField ID="hdnAvailBenefit" runat="server" />
    <asp:HiddenField ID="hdnAdjProvBenefit" runat="server" />


    <asp:HiddenField ID="hdnID" runat="server" />
    <asp:HiddenField ID="hdnCashCode" runat="server" />

    <asp:HiddenField ID="hdnAccessT1" runat="server" />
    <asp:HiddenField ID="hdnAccessT2" runat="server" />
    <asp:HiddenField ID="hdnAccessT3" runat="server" />

     <asp:HiddenField ID="hdnCuNumber" runat="server" />








</asp:Content>

