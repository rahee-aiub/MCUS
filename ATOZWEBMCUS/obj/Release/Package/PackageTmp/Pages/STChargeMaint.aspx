<%@ Page Language="C#" MasterPageFile="~/MasterPages/INVMasterPage.Master" AutoEventWireup="true" CodeBehind="STChargeMaint.aspx.cs" Inherits="ATOZWEBMCUS.Pages.STChargeMaint" Title="Vat Tax Maintenance" %>
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
                     VAT/TAX Maintenance
                    </th>
                </tr>
              
            </thead>

            <tr>
                <td>
                    <asp:Label ID="lblVat" runat="server" Text="VAT ( % ):" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>     
                    <asp:TextBox ID="txtVAT" runat="server" CssClass="cls text" Width="150px" onfocus="javascript.this.select()" onkeypress="return IsDecimalKey(event)" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" MaxLength="50"></asp:TextBox>
                
                    </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTax" runat="server" Text="TAX ( % ):" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTAX" runat="server" CssClass="cls text" Width="150px" onfocus="javascript.this.select()" onkeypress="return IsDecimalKey(event)" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>

                </td>
                
                <td>
                    &nbsp;
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="Update Information" CssClass="button green" Height="30px"  
                        OnClientClick="return ValidationBeforeUpdate()" onclick="BtnUpdate_Click" />&nbsp;
                   &nbsp;
                     <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="30px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click"/>
                    <br />
                </td>
            </tr>
        </table>
    </div>

    

   

</asp:Content>
