<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/GLMasterPage.Master" AutoEventWireup="true" CodeBehind="GLDailyBoothTransaction.aspx.cs" Inherits="ATOZWEBMCUS.Pages.GLDailyBoothTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../dateTimeScripts/jquery-1.4.1.min.js" type="text/javascript"></script>

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
    </script>
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
            width:865px;
        }
    </style>

    <script language="javascript" type="text/javascript">
        
        function ValidationBeforeAdd() {

            var txtContra = document.getElementById('<%=txtContra.ClientID%>').value;
            var txtTrnsactionCode = document.getElementById('<%=txtTrnsactionCode.ClientID%>').value;
            var txtDescription = document.getElementById('<%=txtDescription.ClientID%>').value;
            var txtDrAmount = document.getElementById('<%=txtDrAmount.ClientID%>').value;
            var txtCrAmount = document.getElementById('<%=txtCrAmount.ClientID%>').value;

            if (txtContra == '' || txtContra.length == 0)
                alert('Please Input Contra Code');

            else if (txtTrnsactionCode == '' || txtTrnsactionCode.length == 0)
                alert('Please Input Transaction Code');

            else if ((txtDrAmount == '' || txtDrAmount.length == 0) && (txtCrAmount == '' || txtCrAmount.length == 0)) 
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
                <asp:HiddenField ID="hdnID" runat="server" />
            </thead>

            <tr>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblVchDate" runat="server" Text="Voucher Date:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td style="background-color: #fce7f9">
                    <asp:TextBox ID="txtVchDate" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge" Width="140px" Height="25px"
                        Font-Size="Large" OnTextChanged="txtVchDate_TextChanged" AutoPostBack="True"></asp:TextBox><img src="../Images/calender.png" />
                    <asp:Button ID="BtnDelete" runat="server" Text="R" Font-Bold="True" Font-Size="Medium"
                                    ForeColor="White" CssClass="button green" OnClick="BtnDelete_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;

                    <asp:Label ID="lblVoucher2" runat="server" Text="Total Debit:" Font-Size="Large"
                    ForeColor="Red"></asp:Label>&nbsp;
                    <asp:TextBox ID="txtTotDebit" style="text-align:right" runat="server" BorderColor="#1293D1" BorderStyle="Ridge" Width="168px" Height="25px"
                        Font-Size="Large" ReadOnly="True"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblVoucher1" runat="server" Text="Total Credit:" Font-Size="Large"
                    ForeColor="Red"></asp:Label>&nbsp;
                    

                    <asp:TextBox ID="txtTotCredit" style="text-align:right" runat="server" BorderColor="#1293D1" BorderStyle="Ridge" Width="175px" Height="25px"
                        Font-Size="Large" ReadOnly="True"></asp:TextBox>
                     
                </td>

            </tr>                       
            <tr>
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
            </tr>
            <tr>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblContra" runat="server" Text="Contra Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td style="background-color: #fce7f9">
                    <asp:TextBox ID="txtContra" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge" Width="95px"
                        Height="25px" Font-Size="Large" ToolTip="Enter Name" ></asp:TextBox>
                    &nbsp;<asp:DropDownList ID="ddlContra" runat="server" Height="28px" BorderColor="#1293D1" BorderStyle="Ridge" Width="317px" CssClass="cls text"
                        Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlContra_SelectedIndexChanged">
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    
                    &nbsp;&nbsp;</td>
                <td style="background-color: azure"></td>
            </tr>
            
            <tr>
                <td style="background-color: #fce7f9">
                    <asp:Label ID="lblAccount" runat="server" Text="Trnsaction Code:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td style="background-color: #fce7f9">
                    <asp:TextBox ID="txtTrnsactionCode" runat="server" CssClass="abc" BorderColor="#1293D1" BorderStyle="Ridge" Width="94px"
                        Height="25px" Font-Size="Large" ToolTip="Enter Name"></asp:TextBox>

                    &nbsp;<asp:DropDownList ID="ddlTrnsactionCode" runat="server" Height="28px" BorderColor="#1293D1" BorderStyle="Ridge" Width="319px" CssClass="cls text"
                        Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="ddlTrnsactionCode_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp;<asp:Label ID="lblDescription" runat="server" Text="Description:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtDescription" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge" Width="375px"
                        Height="25px" Font-Size="Large" ></asp:TextBox>&nbsp;
                    
                    <asp:Label ID="lblDebit" runat="server" Text="Debit:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtDrAmount" style="text-align:right" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge" Width="115px" Height="25px"
                        Font-Size="Large" onchange="javascript:this.value=Comma(this.value);"  ></asp:TextBox>
                    
                    <asp:Label ID="lblCredit" runat="server" Text="Credit:" Font-Size="Large" ForeColor="Red"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtCrAmount" style="text-align:right" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge" Width="115px" Height="25px"
                        Font-Size="Large" onchange="javascript:this.value=Comma(this.value);" ></asp:TextBox>
                    
                    <asp:Button ID="BtnAdd" runat="server" Text="Add" Font-Bold="True" Font-Size="Medium"
                        ForeColor="White" CssClass="button green" OnClick="BtnAdd_Click" /></td>
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
                   
                    <asp:BoundField HeaderText="Trn Code" DataField="GLAccNo" HeaderStyle-Width="120px" ItemStyle-Width="120px">

                    </asp:BoundField>
                    <asp:BoundField HeaderText="Description" DataField="TrnDesc" HeaderStyle-Width="400px" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="left">
            
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Amount" DataField="GLAmount" DataFormatString="{0:0,0.00}" HeaderStyle-Width="250px" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Right">

                        
                    </asp:BoundField>
                    <asp:ButtonField CommandName="Delete" Text="Delete" HeaderStyle-Width="80px" ItemStyle-Width="80px"/>

                </Columns>

            </asp:GridView>

            

        </div>
        <div>
            <table>
                <tr>
                    <td style="background-color: #fce7f9"></td>
                    <td style="background-color: #fce7f9">
                        <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Size="Large" ForeColor="White"
                            Font-Bold="True" CssClass="button green" OnClick="BtnUpdate_Click" />
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

    <asp:Label ID="CtrlProcDate" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="CtrlMsgFlag" runat="server" Text="" Visible="false"></asp:Label>
    
    <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>
   
</asp:Content>
