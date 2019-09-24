<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSProvisionBalanceReport.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSProvisionBalanceReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                    <th colspan="5">
                        CS Provision Balance Report
                    </th>
                </tr>

            </thead>

            <tr>
             <td colspan ="4">
                 <asp:TextBox ID="txtToDaysDate" runat="server" Enabled="False" BorderColor="#1293D1"
                        Width="250px" BorderStyle="Ridge" Font-Size="Large" Visible="False"></asp:TextBox>
             </td>
            </tr>
                        
           <tr>
         

               <td>
                   A/C Type  
               </td>

               <td>
                   :
               </td>
                <td>
                     <asp:TextBox ID="txtAccType" runat="server" CssClass="cls text" Width="115px" Height="25px"
                        Font-Size="Large" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtAccType_TextChanged"></asp:TextBox> 

                    <asp:DropDownList ID="ddlAcType" runat="server" Height="25px" Width="400px" AutoPostBack="True"
                        Font-Size="Large" OnSelectedIndexChanged="ddlAcType_SelectedIndexChanged">
                        
                    </asp:DropDownList>
                </td>


           </tr>

            <tr>
              <td>
                  <td>
                      
                  </td>
                  <td>
                       <asp:CheckBox ID="ChkZeroBalanceShow" runat="server" Text="Show  with Zero Balance"  />
                  </td>
                 
              </td>
           
            <tr>
             
                <td>

                </td>
                  <td>

                </td>

<td>
                    <asp:Button ID="BtnView" runat="server" Text="Preview" Font-Size="Large" ForeColor="White"
                        Font-Bold="True"  CssClass="button green"  OnClick="BtnView_Click" />&nbsp;
                  &nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" PostBackUrl="~/pages/A2ZERPModule.aspx" />
                    <br />
                </td>


            </tr>
            
                

            
             
            </table>
        </div>


</asp:Content>

