<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="MenuOptionAccessibilityReport.aspx.cs" Inherits="ATOZWEBMCUS.Pages.MenuOptionAccessibilityReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

      <script language="javascript" type="text/javascript">
          function ValidationBeforeSave() {
              return confirm('Are you sure you want to Proceed???');
          }

          function ValidationBeforeUpdate() {
              return confirm('Are you sure you want to Update information?');
          }

    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
     <br />
    <br />
    <br />
    <br />

    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">Users Menu Option Accessibility Report
                    </th>
                </tr>
            </thead>

            <tr>
                <td style="background-color: #fce7f9" class="auto-style8">
                   <asp:CheckBox ID="chkAllUser" runat="server" ForeColor="Red" Text="All" Font-Size="Large" AutoPostBack="True" OnCheckedChanged="chkAllUser_CheckedChanged" />
               
                    &nbsp;
               
                    <asp:Label ID="Label1" runat="server" Text="User ID No. :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>

                    </td>
                 <td>
                <asp:TextBox ID="txtIdsNo" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="true" ToolTip="Enter Ids" OnTextChanged="txtIdsNo_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlIdsNo" runat="server" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="316px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddlIdsNo_SelectedIndexChanged" > 
                                      
                    </asp:DropDownList>
           
                </td>

            </tr>



            <tr>
                <td>
                    <asp:Label ID="lblModule" runat="server" Text="Module :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
               </td>
                <td>
                    <asp:DropDownList ID="ddlModule" runat="server" Height="25px" Width="316px" CssClass="cls text"
                        Font-Size="Large">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        
                    </asp:DropDownList>
                </td>
            </tr>

             



            




            
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BtnProcess" runat="server" Text="Preview/Print" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" Height="27px" Width="150px" CssClass="button green" OnClick="BtnProcess_Click" />&nbsp;

                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>

        </table>
    </div>






</asp:Content>
