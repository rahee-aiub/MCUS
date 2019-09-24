<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="GLParameterMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.GLParameterMaintenance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">

        function ValidationBeforeUpdate() {
            return confirm('Are you sure you want to Update information?');
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
       <br />
    <br />
    <div align="center">
    <table class="style1">

            <thead>
                <tr>
                    <th colspan="3">System Parameter Maintenance
                    </th>
                </tr>
            </thead>
            <tr>
                <td>
                    <asp:Label ID="lblBegMonth" runat="server" Text="Financial Begining Month:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtBegMonth" runat="server" CssClass="cls text" Width="224px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" ReadOnly="true"></asp:TextBox>&nbsp;&nbsp;
                        &nbsp;&nbsp;
                        </td>
            </tr>
        
      
            <tr>
                <td>
                    <asp:Label ID="lblGLPLCode" runat="server" Text="GL P/L Code:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtGLPLCode" runat="server" CssClass="cls text" Width="224px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" TabIndex="5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLYrPLCode" runat="server" Text="Undistributed Last Year P/L Code:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtLYrPLCode" runat="server" CssClass="cls text" Width="224px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" TabIndex="6"></asp:TextBox>
                </td>
            </tr>
          
            <tr>
                <td></td>
                <td class="auto-style1">
                       <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Size="Large"
                        ForeColor="White" Font-Bold="True"  CssClass="button green"
                         OnClientClick="return ValidationBeforeUpdate()" OnClick="BtnUpdate_Click" />&nbsp;
                    
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>
        </div>






</asp:Content>

