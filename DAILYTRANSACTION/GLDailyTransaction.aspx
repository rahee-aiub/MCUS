<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="GLDailyTransaction.aspx.cs" Inherits="ATOZWEBMCUS.Pages.GLDailyTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../dateTimeScripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../dateTimeScripts/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtVchDate.ClientID %>").dynDateTime({
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
            $("#<%= txtVchDate.ClientID %>").datepicker();

              var prm = Sys.WebForms.PageRequestManager.getInstance();

              prm.add_endRequest(function () {
                  $("#<%= txtVchDate.ClientID %>").datepicker();

            });

          });


    </script>


  <%--  <script type="text/javascript">

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



    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 190px;
            margin: 0 auto;
            width: 930px;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            width: 865px;
        }
    </style>

    <script language="javascript" type="text/javascript">

        function ValidationBeforeAdd() {

            var txtContra = document.getElementById('<%=txtContra.ClientID%>').value;
            var txtTrnsactionCode = document.getElementById('<%=txtTrnsactionCode.ClientID%>').value;
            var txtDescription = document.getElementById('<%=txtDescription.ClientID%>').value;
            var txtAmount = document.getElementById('<%=txtAmount.ClientID%>').value;


            if (txtContra == '' || txtContra.length == 0)
                alert('Please Input Contra Code');

            else if (txtTrnsactionCode == '' || txtTrnsactionCode.length == 0)
                alert('Please Input Transaction Code');

            else if ((txtAmount == '' || txtAmount.length == 0))
                alert('Please Input Voucher Amount');

            else if (txtDescription == '' || txtDescription.length == 0)
                alert('Please Input  Description');

            else
                return confirm('Are you sure you want to Add the Data?');
            return false;
        }


    </script>

    <style type="text/css">
        .text {
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />

    <div align="center">

        <table class="style1">
            <thead>
                <tr>
                    <th colspan="2">
                        <asp:Label ID="lblTransFunction" runat="server" Text="Label"></asp:Label>
                    </th>

                    <th style="background-color: azure"></th>


                </tr>


            </thead>

            <tr>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblVchDate" runat="server" Text="Voucher Date:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td style="background-color: #fce7f9">
                    <asp:TextBox ID="txtVchDate" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge" Width="140px" Height="25px"
                        Font-Size="Large" OnTextChanged="txtVchDate_TextChanged" AutoPostBack="True"></asp:TextBox>


                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblVchNo" runat="server" Font-Size="Large" Text="Vch.No. :" ForeColor="Red"></asp:Label>
                    <asp:TextBox ID="txtVchNo" runat="server" TabIndex="1" CssClass="cls text" Width="140px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge" Font-Size="Large"></asp:TextBox>


                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;

                    <asp:Label ID="lblHoldDebit" runat="server" Text="Hold Debit :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>&nbsp;
                    <asp:TextBox ID="txtHoldDebit" Style="text-align: right" runat="server" BorderColor="#1293D1" BorderStyle="Ridge" Width="168px" Height="25px"
                        Font-Size="Large" ReadOnly="True"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblHoldCredit" runat="server" Text="Hold Credit :" Font-Size="Large"
                    ForeColor="Red"></asp:Label>&nbsp;
                    

                    <asp:TextBox ID="txtHoldCredit" Style="text-align: right" runat="server" BorderColor="#1293D1" BorderStyle="Ridge" Width="175px" Height="25px"
                        Font-Size="Large" ReadOnly="True"></asp:TextBox>

                </td>

            </tr>
            <%--<tr>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblMode" runat="server" Text="Transaction Mode :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td style="background-color: #fce7f9">
                    <asp:DropDownList ID="ddlTrnMode" runat="server" Height="25px" Width="148px" CssClass="cls text"
                        Font-Size="Large" OnSelectedIndexChanged="ddlTrnMode_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Debit Trans. </asp:ListItem>
                        <asp:ListItem Value="2">Credit Trans. </asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>--%>

            <tr>
                <td>
                    <asp:Label ID="lblTrnType" runat="server" Text="Transaction Type :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList
                        ID="ddlTrnType" runat="server" TabIndex="5" Height="25px" Width="90px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" OnSelectedIndexChanged="ddlTrnType_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="1">Cash</asp:ListItem>
                        <asp:ListItem Value="2">Bank</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;

                  
                                <asp:Label ID="lblGLBankCode" runat="server" Text="Bank Code :" Font-Size="Large"
                                    ForeColor="Red"></asp:Label>
                            
                                <asp:TextBox ID="txtGLBankCode" runat="server" TabIndex="7" CssClass="cls text" Width="90px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" AutoPostBack="True" OnTextChanged="txtGLBankCode_TextChanged"></asp:TextBox>
                                <asp:DropDownList ID="ddlGLBankCode" runat="server" Height="28px" Width="300px" BorderColor="#1293D1" BorderStyle="Ridge"
                                    Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlGLBankCode_SelectedIndexChanged">
                                </asp:DropDownList>

                          

                               
                    <asp:Label ID="lblChqNo" runat="server" Text="Cheque No. :" Font-Size="Large" ForeColor="Red"></asp:Label>

                    <asp:TextBox ID="txtChqNo" runat="server" TabIndex="6" CssClass="cls text" MaxLength="15" Width="140px" Height="25px" onkeydown="return (event.keyCode !=13);" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Medium"></asp:TextBox>&nbsp;

                    <asp:Label ID="lblBalance" runat="server" Text=" GL Cash Balance :" Font-Size="Large" ForeColor="Red"></asp:Label>

                    <asp:TextBox ID="txtBalance" runat="server" Style="text-align: right" Font-Bold="true" CssClass="cls text" Width="150px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"></asp:TextBox>

                </td>
            </tr>




            <tr>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblContra" runat="server" Text="Contra Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td style="background-color: #fce7f9">
                    <asp:TextBox ID="txtContra" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge" Width="95px"
                        Height="25px" Font-Size="Large" ToolTip="Enter Name" TabIndex="2" AutoPostBack="True" OnTextChanged="txtContra_TextChanged"></asp:TextBox>
                    &nbsp;<asp:DropDownList ID="ddlContra" runat="server" Height="28px" BorderColor="#1293D1" BorderStyle="Ridge" Width="317px" CssClass="cls text"
                        Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlContra_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnBack1" runat="server" Text="<<" Font-Size="Medium" ForeColor="Red"
                        Font-Bold="True" CssClass="button green" OnClick="btnBack1_Click" />

                </td>
                <td style="background-color: azure"></td>
            </tr>

            <tr>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblAccount" runat="server" Text="Transaction Code:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td style="background-color: #fce7f9">
                    <asp:TextBox ID="txtTrnsactionCode" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge" Width="94px"
                        Height="25px" Font-Size="Large" ToolTip="Enter Name" TabIndex="3" AutoPostBack="True" OnTextChanged="txtTrnsactionCode_TextChanged"></asp:TextBox>
                    &nbsp;<asp:DropDownList ID="ddlTrnsactionCode" runat="server" Height="28px" BorderColor="#1293D1" BorderStyle="Ridge" Width="319px" CssClass="cls text"
                        Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlTrnsactionCode_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnBack2" runat="server" Text="<<" Font-Size="Medium" ForeColor="Red"
                        Font-Bold="True" CssClass="button green" OnClick="btnBack2_Click" />

                    &nbsp;&nbsp;<asp:Label ID="lblDescription" runat="server" Text="Description:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtDescription" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge" Width="300px"
                        Height="25px" Font-Size="Large" TabIndex="4" onkeydown="return (event.keyCode !=13);"></asp:TextBox>&nbsp;
                    
                    <asp:Label ID="lblAmount" runat="server" Text="Amount:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtAmount" Style="text-align: right" runat="server" TabIndex="5" CssClass="cls text" onkeydown="return (event.keyCode !=13);" BorderColor="#1293D1" BorderStyle="Ridge" Width="115px" Height="25px"
                        Font-Size="Large" onkeypress="return IsDecimalKey(event)"></asp:TextBox>

                    <%--<asp:Label ID="lblCredit" runat="server" Text="Credit:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtCrAmount" style="text-align:right" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge" Width="115px" Height="25px"
                        Font-Size="Large" onchange="javascript:this.value=Comma(this.value);" AutoPostBack="True" OnTextChanged="txtCrAmount_TextChanged" ></asp:TextBox>--%>

                    <asp:Button ID="BtnAdd" runat="server" Text="Add" Font-Bold="True" Font-Size="Medium"
                        ForeColor="White" TabIndex="6" CssClass="button green" OnClick="BtnAdd_Click" /></td>
                <%-- <td style="background-color: azure">&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblVoucher2" runat="server" Text="Differrnce:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    <asp:TextBox ID="txtDeffernce" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge" Width="115px" Height="25px"
                        Font-Size="Large" AutoPostBack="True" OnTextChanged="txtCredit_TextChanged" ReadOnly="True"></asp:TextBox>

                </td>--%>
            </tr>






        </table>
        <br />

        <div align="center" class="grid_scroll">
            <asp:GridView ID="gvDebitInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" RowStyle-Height="10px"
                AutoGenerateColumns="False" AlternatingRowStyle-BackColor="WhiteSmoke" EnableModelValidation="True" OnRowDataBound="gvDebitInfo_RowDataBound" OnRowDeleting="gvDebitInfo_RowDeleting">

                <HeaderStyle BackColor="YellowGreen" CssClass="FixedHeader"></HeaderStyle>

                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                <AlternatingRowStyle BackColor="WhiteSmoke"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="Id" Visible="false" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="Trn Code" DataField="GLAccNo" HeaderStyle-Width="120px" ItemStyle-Width="120px"></asp:BoundField>
                    <asp:BoundField HeaderText="Description" DataField="TrnDesc" HeaderStyle-Width="400px" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="left"></asp:BoundField>
                    <asp:BoundField HeaderText="Amount" DataField="GLAmount" DataFormatString="{0:0,0.00}" HeaderStyle-Width="250px" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
            
                    <asp:TemplateField HeaderText="TrnType" Visible="false" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:Label ID="lblTrnType" runat="server" Text='<%# Eval("TrnType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TrnDrCr" Visible="false" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:Label ID="lblTrnDrCr" runat="server" Text='<%# Eval("TrnDrCr") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:ButtonField CommandName="Delete" Text="Delete" HeaderStyle-Width="80px" ItemStyle-Width="80px" />

                </Columns>

            </asp:GridView>



        </div>

        <div align="center">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblTotalAmt" runat="server" Text="TOTAL AMOUNT :" Font-Size="large" Font-Bold="true" ForeColor="#FF6600"></asp:Label>

            &nbsp;&nbsp;

                <asp:Label ID="txtTotalAmt" runat="server" Font-Size="large" Font-Bold="true" ForeColor="#FF6600"></asp:Label>
        </div>

        <div>
            <table>
                <tr>
                    <td style="background-color: #fce7f9"></td>
                    <td style="background-color: #fce7f9">
                        <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Size="Large" ForeColor="White"
                            Height="27px" Width="86px" Font-Bold="True" CssClass="button green" OnClick="BtnUpdate_Click" />
                        &nbsp;
                        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                            Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red"
                            OnClick="BtnExit_Click" />
                    </td>
                    <td style="background-color: azure"></td>

                </tr>
            </table>

        </div>

    </div>

    <asp:Label ID="CtrlPrmValue" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlVchNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnMode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTrnMode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTrnType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlModule" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlProcStat" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlTotAmount" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlAccType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlContraAType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlRecType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lbltotAmt" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlProcDate" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnContraCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnTranCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnContraHead1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnContraHead2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnContraHead3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnContraHead4" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnTranHead1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnTranHead2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnTranHead3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnTranHead4" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblIDName" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlBackUpStat" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTrnTypeTitle" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblBoothNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblBoothName" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblGLAccBal" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="ErrMsg" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblGLCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnGLSubHead" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnGLSubHead1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnGLSubHead2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblGLBalanceType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="MsgFlag" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
