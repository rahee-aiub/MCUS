<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSDailyEditTransaction.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSDailyEditTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to save information?');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Reverse Transaction?');
        }

    </script>

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 385px;
            margin: 0 auto;
            width:890px;
            
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }
       .FixedHeader {
            position: absolute;
            font-weight: bold;
            width:890px;
           
            
     
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
                                <th colspan="3">Daily Edit Transaction
                                </th>
                            </tr>

                        </thead>
                                   
                                    </table>

        <table>
            <tr>
                <td>
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblVoucherNo" runat="server" Text="Voucher No:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="cls text" Width="192px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large"></asp:TextBox>

                    <asp:Button ID="BtnSearch" runat="server" Text="Search" Font-Bold="True" Font-Size="Medium"
                        ForeColor="White" CssClass="button green" OnClick="BtnSearch_Click" />
                    
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />

    <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvDetailInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>

                <asp:BoundField HeaderText="TrnDate" DataField="TrnDate" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="80px" ItemStyle-Width="80px" />

                <asp:TemplateField HeaderText="CuType" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="CuType" runat="server" Text='<%#Eval("CuType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="CuNo" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="CuNo" runat="server" Text='<%#Eval("CuNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="MemNo" HeaderStyle-Width="70px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="MemNo" runat="server" Text='<%#Eval("MemNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="AccType" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="AccType" runat="server" Text='<%#Eval("AccType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="AccNo" HeaderStyle-Width="120px" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="AccNo" runat="server" Text='<%#Eval("AccNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="Description" DataField="TrnDesc" HeaderStyle-Width="300px" ItemStyle-Width="300px" />
                
                <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                        <asp:Label ID="Amount" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("GLAmount"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="TrnType" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="TrnType" runat="server" Text='<%#Eval("TrnType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DrCr" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="TrnDrCr" runat="server" Text='<%#Eval("TrnDrCr") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>
   <%-- </div>--%>

    <table>
        <tr>
            <td></td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Font-Bold="True" Font-Size="Medium" Height="27px" Width="86px"
                        ForeColor="White" CssClass="button green" OnClientClick ="return ValidationBeforeUpdate()" OnClick="btnDelete_Click" />
                &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                <br />
            </td>
        </tr>
    </table>

        </div>

    
    <asp:Label ID="CtrlTrnDate" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="ValidityFlag" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblAtyClass" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CtrlBackUpStat" runat="server" Text="" Visible="false"></asp:Label>

     <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="HdnFuncOpt" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="HdnFOpt" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="HdnModule" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="hdnPrmValue" runat="server" Text="" Visible="false"></asp:Label>

     <asp:Label ID="lblCashCode" runat="server" Text="" Visible="false"></asp:Label>

     <asp:Label ID="lblGLAccBal" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblVchAmt" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblVchTrnType" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblTrnRevFlag" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="lblGLBalanceType" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>

