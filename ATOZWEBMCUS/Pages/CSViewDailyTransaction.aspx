<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSViewDailyTransaction.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSViewDailyTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 200px;
            width: 555px;
            margin: 0 auto;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            width: 545px;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .auto-style2 {
            height: 32px;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function VerifyValidation() {
            return confirm('Do you want to Verify data?');
        }
    </script>
    <script type="text/javascript">
        function basicPopup() {
            popupWindow = window.open("CSViewGetImage.aspx", 'popUpWindow', 'height=500,width=600,left=100,top=30,resizable=No,scrollbars=No,toolbar=no,menubar=no,location=no,directories=no, status=No');
        }
    </script>
    <script type="text/javascript">
        function closechildwindow() {
            window.opener.document.location.href = 'CSVerifyDailyTransaction.aspx';
            window.close();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
     <body>
   


        <br />
        <br />
        <div align="center">
            <div id="DivMainHeader" runat="server" align="center">

                <table class="style1">
                    <tr>
                        <th colspan="3" class="auto-style2">
                            <p align="center" style="color: blue">
                                Verify CS Daily Transaction  
                            </p>
                        </th>
                    </tr>
                    <tr>

                        <th colspan="3">
                            <asp:Label ID="lblFuncDesc" runat="server" Text="" Font-Size="Large"
                                ForeColor="red" Font-Bold="true" Font-Italic="true"></asp:Label>
                        </th>
                    </tr>


                </table>
            </div>
            <table>
                <tr>
                    <td>
                        <table class="style1">

                            <thead>
                            </thead>

                            

                            <tr>
                                <td>
                                    <asp:Label ID="lblVoucherNo" runat="server" Text="Voucher No:" Font-Size="Medium"
                                        ForeColor="Black" Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="None"
                                        Font-Size="Medium" Enabled="False" Font-Bold="true"></asp:TextBox>


                                </td>



                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lblTranDate" runat="server" Text="Transaction Date:" Font-Size="Medium"
                                        ForeColor="Black" Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTranDate" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="None"
                                        Font-Size="Medium" Enabled="False" Font-Bold="true"></asp:TextBox>



                                </td>




                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCUNum" runat="server" Text="Credit Union No:" Font-Size="Medium"
                                        ForeColor="Black" Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCreditUNo" runat="server" CssClass="cls text" Width="73px" Height="25px" BorderColor="#1293D1" BorderStyle="None"
                                        Font-Size="Medium" BackColor="White" Enabled="False" Font-Bold="true"></asp:TextBox>
                                    <asp:Label ID="lblCUName" runat="server" Text="" Font-Size="Medium" ForeColor="Black" Font-Bold="true" Font-Italic="true"></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMemNo" runat="server" Text="Depositor No:" Font-Size="Medium" ForeColor="Black" Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMemNo" runat="server" CssClass="cls text" Width="53px" Height="25px" BorderColor="#1293D1" BorderStyle="None"
                                        Font-Size="Medium" Enabled="False" Font-Bold="true"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblMemName" runat="server" Text="" Font-Size="Medium" ForeColor="Black" Font-Bold="true" Font-Italic="true"></asp:Label>
                                </td>
                            </tr>




                        </table>
                    </td>


                </tr>
            </table>
        </div>

        <div align="center" class="grid_scroll">
            <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
                AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvDetailInfo_RowDataBound">
                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                <Columns>

                    <asp:BoundField HeaderText="AccType" DataField="AccType" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="AccNo" DataField="AccNo" HeaderStyle-Width="120px" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Description" DataField="TrnDesc" HeaderStyle-Width="210px" ItemStyle-Width="210px" ItemStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Amount" DataField="GLAmount" HeaderStyle-Width="120px" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0,0.00}" />
                    <asp:TemplateField HeaderText="Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="TrnType" runat="server" Text='<%#Eval("TrnType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="TrnPayment" runat="server" Text='<%#Eval("TrnPayment") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
        </div>
        <div align="center">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblTotalAmt" runat="server" Text="TOTAL AMOUNT :" Font-Size="Large" Font-Bold="true" ForeColor="#FF6600"></asp:Label>

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                <asp:Label ID="txtTotalAmt" runat="server" Font-Size="Large" Font-Bold="true" ForeColor="#FF6600"></asp:Label>
        </div>
        <table>
            <tr>
                <td></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
             
                <asp:Button ID="BtnVerify" runat="server" Text="Verify" Font-Bold="True" Font-Size="Medium"
                    ForeColor="White" ToolTip="Verify Information" CssClass="button green" OnClick="BtnVerify_Click" OnClientClick=" return VerifyValidation()" />&nbsp;
                   
                <asp:Button ID="BtnViewImage" runat="server" Text="View Image" Font-Bold="True" Font-Size="Medium"
                    ForeColor="White" CssClass="button green" OnClick="BtnViewImage_Click" OnClientClick="basicPopup();return false;" />
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblCuNo" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblFuncOpt" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblFOptDesc" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblModule" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblAccTitle" runat="server" Text="" Visible="false"></asp:Label>

        <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="hdnProcDate" runat="server" Text="" Visible="false"></asp:Label>

     </body>
</asp:Content>

