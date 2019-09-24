<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="GLVerifyDailyTransaction.aspx.cs" Inherits="ATOZWEBMCUS.Pages.GLVerifyDailyTransaction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
     <script language="javascript" type="text/javascript">

         function VerifyValidation() {
             return confirm('Do you want to Verify data?');
         }
    </script>

     <link href="../Styles/TableStyle1.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/TableStyle2.css" rel="stylesheet" type="text/css" />

  

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
      <br />
    <br />
    
    <div id="DivMainHeader" runat="server" align="center">
            
            <table class="style1">
                <tr>
                    <th colspan="4">
                        <p align="center" style="color: blue">
                            Verify GL Daily Transaction
                        </p>
                    </th>
                </tr>
            </table>
         
        </div>
        <div id="DivButton" runat="server" align="center">
            <table>
                <tr>
                    <td colspan="6" align="center">
                      <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    </td>
                </tr>
            </table>
        </div>
     <div id="DivGridViewCancle" runat="server" align="center" style="height: 245px; overflow: auto;
            width: 100%;">
            <table class="style1">
                <thead>
                    <tr>
                        <th>
                            <p align="center" style="color: blue">
                               Verify - Spooler
                            </p>
        <asp:GridView ID="gvCUInfo" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" style="margin-top: 4px" Width="757px" >
            <Columns>
                     
            <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
         <asp:Button ID="BtnSelect" runat="server" Text="Select"  Width="68px" CssClass="button green" OnClick="BtnSelect_Click" OnClientClick="ShowPopup('CSViewDailyTransaction.aspx')" />
            </ItemTemplate>
        </asp:TemplateField>
          
                <asp:TemplateField  HeaderText="Voucher No.">
               <ItemTemplate>
                    <asp:Label ID="lblVoucherNo" runat="server" Text='<%# Eval("VchNo") %>'></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
            
               
                
                <asp:TemplateField  HeaderText="Description">
               <ItemTemplate>
                    <asp:Label ID="lblTrnDesc" runat="server" Text='<%# Eval("TrnDesc") %>'></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>

                <asp:TemplateField  HeaderText="Amount">
               <ItemTemplate>
                    <asp:Label ID="Amount" runat="server" Text='<%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Amount"))) %>'></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
                      
            </Columns>
        </asp:GridView>
                           
                       </th>        
                    </tr>
                </thead>
            </table>
        </div>
    <div align="center">
          <asp:Label ID="lblmsg1" runat="server" Text="All Record Verify Successfully Completed" Font-Bold="True" Font-Size="XX-Large" ForeColor="#009933"></asp:Label><br />
            <asp:Label ID="lblmsg2" runat="server" Text="No More Record for Verify" Font-Bold="True" Font-Size="XX-Large" ForeColor="#009933"></asp:Label>  </div>


      <asp:Label ID="CtrlPrmValue" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="CtrlModule" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="lblTrnAmount" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="lblFuncOpt" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="hdnCashCode" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="lblVch" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="CtrlFlag" runat="server" Text="" Visible="false"></asp:Label>

    


</asp:Content>

