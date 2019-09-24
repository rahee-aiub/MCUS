﻿<%@ Page Language="C#" MasterPageFile="~/MasterPages/INVMasterPage.Master" AutoEventWireup="true" CodeBehind="STSupplierCodeMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.STSupplierCodeMaintenance" Title="Supplier Maintenance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
	    function ValidationBeforeSave()
	    {    
	     return confirm('Are you sure you want to save information?');	  
	    }
	    
	    function ValidationBeforeUpdate()
	    {    
	     return confirm('Are you sure you want to Update information?');	  
	    }
	   	    

    </script>

           <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 350px;
            width: 500px;
            margin: 0 auto;
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }
        .FixedHeader {
            position: absolute;
            font-weight: bold;
            width: 485px;

        }  
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <br />
    <br />
    <br />
    <br />
    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">
                  Supplier Code Maintenance
                    </th>
                </tr>
              
            </thead>

            <tr>
                <td>
                    <asp:Label ID="lblcode" runat="server" Text=" Supplier:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSupplier" runat="server" Height="30px" Width="316px" AutoPostBack="True"
                        Font-Size="Large"  BorderColor="#1293D1" BorderStyle="Ridge"
                        onselectedindexchanged="ddlSupplier_SelectedIndexChanged">
                        <%--<asp:ListItem Value="0">-Select-</asp:ListItem>--%>
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDescription" runat="server" Text="Supplier Name :" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="cls text" Width="316px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" MaxLength="50"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblAddressLine1" runat="server" Text="Address Line1:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddLine1" runat="server" CssClass="cls text" Width="316px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" MaxLength="50"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblAddressLine2" runat="server" Text="Address Line2:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddLine2" runat="server" CssClass="cls text" Width="316px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" MaxLength="50"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblAddressLine3" runat="server" Text="Address Line3:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddLine3" runat="server" CssClass="cls text" Width="316px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" MaxLength="50"></asp:TextBox>
                </td>
            </tr>

              <tr>
                <td>
                    <asp:Label ID="lblSuppTelephone" runat="server" Text="Telephone:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSuppTelephone" runat="server" CssClass="cls text" Width="316px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" MaxLength="50"></asp:TextBox>
                </td>
            </tr>

              <tr>
                <td>
                    <asp:Label ID="lblSuppMobile" runat="server" Text="Mobile No.:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSuppMobileNo" runat="server" CssClass="cls text" Width="316px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" MaxLength="50"></asp:TextBox>
                </td>
            </tr>

             <tr>
                <td>
                    <asp:Label ID="lblSuppFax" runat="server" Text="Fax:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSuppFax" runat="server" CssClass="cls text" Width="316px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" MaxLength="50"></asp:TextBox>
                </td>
            </tr>

             <tr>
                <td>
                    <asp:Label ID="lblSuppEmail" runat="server" Text="Email:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSuppEmail" runat="server" CssClass="cls text" Width="316px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" MaxLength="50"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" ToolTip="Insert Information" CssClass="button green"  
                        OnClientClick="return ValidationBeforeSave()" onclick="BtnSubmit_Click" Height="27px" />&nbsp;
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="Update Information" CssClass="button green"  
                        OnClientClick="return ValidationBeforeUpdate()" onclick="BtnUpdate_Click" Height="27px" />&nbsp;
                   <asp:Button ID="BtnView" runat="server" Text="View" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="View Information" CssClass="button green" 
                         onclick="BtnView_Click" Height="27px" />&nbsp;
                     <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click"/>
                    <br />
                </td>
            </tr>
        </table>
    </div>

     <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
 AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvDetailInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                  
                <asp:BoundField HeaderText="Supplier" DataField="SuppCode" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Name" DataField="SuppName" HeaderStyle-Width="380px" ItemStyle-Width="380px" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center"/>
                
              </Columns>
          
        </asp:GridView>
     </div>

   
    <asp:Label ID="hdnSuppCode" runat="server" Text="" Visible="false"></asp:Label>
  

</asp:Content>
