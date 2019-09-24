<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="HOExportData.aspx.cs" Inherits="ATOZWEBMCUS.Pages.HOExportData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <div align="center">
                <h1>Extract CUNION file</h1>
                <h1>Extract Member file</h1>

                <h1>Extract Account file</h1>

    </div>
    <div align="center">
      
             
        <asp:Button ID="btnimport" runat="server" Text="Import" Font-Bold="True" Font-Size="Medium"
            ForeColor="White" CssClass="button green" 
            Height="22px" OnClick="btnimport_Click" />&nbsp;
        
        <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Medium" ForeColor="#FFFFCC"
            Height="24px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
            CssClass="button red" OnClick="BtnExit_Click" />
    </div>


</asp:Content>
