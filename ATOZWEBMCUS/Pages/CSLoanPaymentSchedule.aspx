<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master"
    AutoEventWireup="true" CodeBehind="CSLoanPaymentSchedule.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSLoanPaymentSchedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
         function ValidationBeforeSave() {
             return confirm('Are you sure you want to save information?');
         }

  
    </script>

    <style type="text/css">
        .grid_scroll
        {
            overflow: auto;
            height: 340px;
            width: 700px;
            margin: 0 auto;
        }
        .border_color
        {
            border: 1px solid #006;
            background: #D5D5D5;
        }
        .FixedHeader
         {
            position: absolute;
            font-weight: bold;
            width:675px;
        }
        .style1
        {
            width: 487px;
        }
        .style2
        {
            width: 538px;
        }
        .style3
        {
            width: 544px;
        }
        .style4
        {
            width: 547px;
        }
        .style5
        {
            width: 553px;
        }
        .style6
        {
            width: 559px;
        }
        .style7
        {
            width: 674px;
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
                    <th colspan="3">
                  Loan Payment Schedule
                    </th>
                </tr>
              
            </thead>
            <tr>
                <td>
                    <asp:Label ID="lblLoanAmount" runat="server" Text="Loan Amount :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="style7">
                    <asp:TextBox ID="txtLoanAmt" runat="server" CssClass="cls text" Width="143px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="true" ontextchanged="txtLoanAmt_TextChanged" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblInstlAmt" runat="server" Text="Installment Amount :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;
                       
                    <asp:TextBox ID="txtInstlAmt" runat="server" CssClass="cls text" Width="143px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="true" OnTextChanged="txtInstlAmt_TextChanged" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNoOfInstl" runat="server" Text="No of Installment :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="style7">
                    <asp:TextBox ID="txtNoOfInstl" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="true" OnTextChanged="txtNoOfInstl_TextChanged1" onkeypress="return IsDecimalKey(event)" ></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblLastInstlAmt" runat="server" Style="text-align: right" Text="Last Installment Amount :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txtLastInstlAmt" runat="server" Width="162px" 
                        Height="25px" Font-Size="Large" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblIntRate" runat="server" Text="Interest Rate :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="style7">
                    <asp:TextBox ID="txtIntRate" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" onchange="javascript:this.value=Comma(this.value);" onkeypress="return IsDecimalKey(event)"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblTotalIntAmt" runat="server" Text=" Total Interest Amount :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txtTotalIntAmt" runat="server"  Width="115px" 
                        Height="25px" Font-Size="Large" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style7">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblNetPayable" runat="server" Text="Net Payable Amount :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txtNetPayable" runat="server" Width="162px"  
                        Height="25px" Font-Size="Large" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td class="style7">
                    <asp:Button ID="BtnSubmit" runat="server" Text="Calculate" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" OnClick="BtnSubmit_Click" 
                        TabIndex="4" />&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click"  />
                    <asp:Button ID="BtnPrint" runat="server" Text="Print/Preview" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" OnClick="BtnPrint_Click" />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke"  RowStyle-Height="10px"
            OnRowDataBound="gvDetailInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                <asp:BoundField DataField="LoanMth" HeaderText="Month" HeaderStyle-Width="130px" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField  DataField="InstlAmt"  HeaderText="Installment Amt" DataFormatString="{0:N0}" HeaderStyle-Width="130px" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="LoanAmt" HeaderText="Loan Amount" DataFormatString="{0:N0}" HeaderStyle-Width="130px" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Right"/>
                <asp:BoundField DataField="IntAmt" HeaderText="Interest Amount" DataFormatString="{0:N0}"  HeaderStyle-Width="130px" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Right"/>
                <asp:BoundField DataField="LoanPayable" HeaderText="Loan Payable Amt" DataFormatString="{0:N0}" HeaderStyle-Width="130px" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Right" />

            </Columns>
           
        </asp:GridView>
    </div>

   

</asp:Content>
