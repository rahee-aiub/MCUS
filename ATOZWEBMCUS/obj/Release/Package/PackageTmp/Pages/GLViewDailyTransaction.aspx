<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="GLViewDailyTransaction.aspx.cs" Inherits="ATOZWEBMCUS.Pages.GLViewDailyTransaction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
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
           
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        </style>
      <script language="javascript" type="text/javascript">

          function VerifyValidation() {
              return confirm('Do you want to Verify data?');
          }
    </script>
     
    <script type="text/javascript">
        function closechildwindow() {
            window.opener.document.location.href = 'GLVerifyDailyTransaction.aspx';
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
                                <th colspan="3">
                                    <p align="center" style="color: blue">
                                    Verify GL Daily Transaction  
                                  </p>
                                </th>
                              </tr>
             <tr>
                               
                            <th colspan="3">
                                  <asp:Label ID="lblCUName" runat="server" Text="" Font-Size="Large"
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
                            <asp:BoundField HeaderText="GLAccNo" DataField="GLAccNo" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Description" DataField="TrnDesc" HeaderStyle-Width="280px" ItemStyle-Width="280px" ItemStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Amount" DataField="GLAmount" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0,0.00}" />

                        </Columns>

                    </asp:GridView>
                </div>
    <div align="center">
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblTotalAmt" runat="server" Text="TOTAL AMOUNT :" Font-Size="Medium" ForeColor="#FF6600"></asp:Label>

        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                <asp:Label ID="txtTotalAmt" runat="server" Font-Size="Medium" ForeColor="#FF6600"></asp:Label>
    </div>
    <table>
        <tr>
            <td></td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
             
                <asp:Button ID="BtnVerify" runat="server" Text="Verify" Font-Bold="True" Font-Size="Medium"
                    ForeColor="White" ToolTip="Verify Information" CssClass="button green"  OnClick="BtnVerify_Click" OnClientClick=" return VerifyValidation()"/>&nbsp;
            
                 <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                <br />
            </td>
        </tr>
    </table>
   
    </body>

    <asp:Label ID="lblModule" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="hdnID" runat="server" Visible="False"></asp:Label>

  

</asp:Content>

