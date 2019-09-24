﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSPensionDefaulterDetailReport.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSPensionDefaulterDetailReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function ValidationBeforeSave() {
            return confirm('Are you sure you want to Proceed???');
        }

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

    </script>

    <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 200px;
            margin: 0 auto;
            width: 1500px;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
            Width: 1145px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <br />
    <br />
    <div align="center">
        <div id="DivMainHeader" runat="server" align="center">
         
         <table class="style1">
                             <tr>
                                <th colspan="3">
                                    <p align="center" style="color: blue">
                                   Detail Deposit Defaulter 
                                  </p>
                                </th>
                              </tr>
            <%-- <tr>
                               
                            <th colspan="3">
                                  &nbsp;</th>
                        </tr>--%>
                              
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
                                <asp:Label ID="lblAcctitle" runat="server" Text="Account Title:" Font-Size="Medium"
                                    ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAcctitle" runat="server" CssClass="cls text" Width="64px" Height="25px" BorderColor="#1293D1" BorderStyle="None"
                                    Font-Size="Medium" Enabled="False" Font-Bold="true"></asp:TextBox>
                                <asp:Label ID="lblAccTypeName" runat="server" Text="" Font-Size="Medium"
                                    ForeColor="Black" Font-Bold="true"></asp:Label>
                                
                            </td>



                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="lblAccNo" runat="server" Text="Account No:" Font-Size="Medium"
                                   ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAccNo" runat="server" CssClass="cls text" Width="164px" Height="25px" BorderColor="#1293D1" BorderStyle="None"
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
                              

                                  <asp:Label ID="lblCUName" runat="server" Text="" Font-Size="Large"
                                    ForeColor="Black" Font-Bold="true" Font-Italic="true"></asp:Label>
                              

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMemNo" runat="server" Text="Depositor No:" Font-Size="Medium" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMemNo" runat="server" CssClass="cls text" Width="53px" Height="25px" BorderColor="#1293D1" BorderStyle="None"
                                    Font-Size="Medium" Enabled="False" Font-Bold="true"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblMemName" runat="server" Text="" Font-Size="Medium" ForeColor="Black" Font-Bold="true" Font-Italic="true" Visible="true"></asp:Label>
                            </td>
                        </tr>
                        
                      


                    </table>
                </td>


            </tr>
        </table>
    </div>
    <div align="Center" class="grid_scroll">
       

         <asp:GridView ID="gvPensionDetail" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvPensionDetail_RowDataBound"  >
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>


                <asp:BoundField HeaderText="TrnDate" DataField="TrnDate" HeaderStyle-Width="95px" ItemStyle-Width="95px" DataFormatString="{0:dd/MM/yyyy}"/>
                <asp:BoundField HeaderText="Curr.DepositAmt" DataField="CalDepositAmt" HeaderStyle-Width="150px" ItemStyle-Width="150px" DataFormatString="{0:0,0.00}"  HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                
                <asp:BoundField HeaderText="PrevDueDeposit" DataField="UptoDueDepositAmt" HeaderStyle-Width="150px" ItemStyle-Width="150px"  DataFormatString="{0:0,0.00}"  ItemStyle-ForeColor="Red" HeaderStyle-ForeColor="Red"  ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"/>
                
                <asp:BoundField HeaderText="PayableDeposit" DataField="PayableDepositAmt" HeaderStyle-Width="150px" ItemStyle-Width="150px"  DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" />
                
                <asp:BoundField HeaderText="PayablePenal" DataField="PayablePenalAmt" DataFormatString="{0:0,0.00}" HeaderStyle-Width="120px" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" />
                <asp:BoundField HeaderText="PaidDeposit" DataField="PaidDepositAmt" DataFormatString="{0:0,0.00}" HeaderStyle-Width="150px" ItemStyle-Width="150px"  ItemStyle-ForeColor="Blue" HeaderStyle-ForeColor="Blue" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" />
                
                <asp:BoundField HeaderText="PaidPenal" DataField="PaidPenalAmt" DataFormatString="{0:0,0.00}" HeaderStyle-Width="95px" ItemStyle-Width="95px" ItemStyle-ForeColor="Blue" HeaderStyle-ForeColor="Blue" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"  />
                <asp:BoundField HeaderText="CurrDueDeposit" DataField="CurrDueDepositAmt" DataFormatString="{0:0,0.00}" HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-ForeColor="Red" ItemStyle-ForeColor="Red"  ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" />
                
                <asp:BoundField HeaderText="Due" DataField="NoDueDeposit" DataFormatString="{0:0,0.00}"  HeaderStyle-Width="60px" ItemStyle-Width="60px"  ItemStyle-ForeColor="Red"  HeaderStyle-ForeColor="Red" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" />


       


            </Columns>

        </asp:GridView>

    </div>
    <div align="center">
        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
            Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red"
            OnClick="BtnExit_Click" />
    </div>

    <asp:Label ID="hdnCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnCuNo" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnDate" runat="server" Text="" Visible="false"></asp:Label>
   

</asp:Content>