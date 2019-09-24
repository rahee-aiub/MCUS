<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSViewGetImage.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSViewGetImage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
      <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">
                        Signature/Picture Card Verification
                    </th>
                </tr>

            </thead>
</table>
           <div align="center">
         <table class="style1">
             <tr>
                 <td>
                     <asp:Panel ID="pnlImage" runat="server" Height="260px" Width="350px">
                         <asp:Image ID="ImgPicture" runat="server" Height="256px" ImageUrl="~/Images/index.jpg" Width="413px" />

                     </asp:Panel>
                 </td>
             </tr>
             <tr>
                 <td>
                       <asp:Label ID="lblhideCuNo" runat="server" Text=" " ForeColor="#FF3300" Visible="false"></asp:Label>
                       <asp:Label ID="lblhideCutype" runat="server" Text=" " ForeColor="#FF3300" Visible="false"></asp:Label>
                       <asp:Label ID="lblhideMemNo" runat="server" Text=" " ForeColor="#FF3300" Visible="false"></asp:Label>
                 </td>
             </tr>
         </table>
   </div>
    </div>
    <div align="center">
        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
            Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red"
            OnClientClick="window.close('CSViewDailyTransaction.aspx')" />
        
    </div>


</asp:Content>

