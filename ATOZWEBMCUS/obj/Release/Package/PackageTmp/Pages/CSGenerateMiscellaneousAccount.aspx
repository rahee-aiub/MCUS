<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSGenerateMiscellaneousAccount.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSGenerateMiscellaneousAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <br />
    <div align="center">
        <table class="style1">
            <tr>
                <th>
                    Generate Miscellaneous Account
                </th>
            </tr>       
            </table>
          </div>
    <br />
    <br />
    <div align="center">
        <asp:Button ID="BtnProceed" runat="server" Text="Proceed" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" CssClass="button green" 
            Height="22px" OnClick="BtnProceed_Click"/>&nbsp;
      
        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
            Height="24px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
            CssClass="button red" OnClick="BtnExit_Click"/>
    </div>

     <asp:Label ID="lblCuType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblCuNum" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblptype" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="txtAccNo" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
