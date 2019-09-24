<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="SYSDistrictCodeMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.SYSDistrictCodeMaintenance" Title="District Maintenance" %>
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
            height: 186px;
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
            width: 483px;

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
                  District Code Maintenance
                    </th>
                </tr>
              
            </thead>
        <tr>
                <td>
                    <asp:Label ID="lblcode" runat="server" Text="Division Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDivicode" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="115px" Height="25px" Font-Size="Large" AutoPostBack="true" 
                        ontextchanged="txtcode_TextChanged" MaxLength="2"></asp:TextBox>
                  
                    <asp:DropDownList ID="ddlDivision" runat="server"  Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="316px" AutoPostBack="True" 
                        Font-Size="Large" 
                        onselectedindexchanged="ddlDivision_SelectedIndexChanged">
                        
                    </asp:DropDownList>
                </td>
            </tr>
         
                
            <tr>
                <td>
                    <asp:Label ID="lblDistcode" runat="server" Text="District Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                                      
                    <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px"
                        Width="316px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" 
                        onselectedindexchanged="ddlDistrict_SelectedIndexChanged">
                       <%--<asp:ListItem Value="0">-Select-</asp:ListItem>--%>
                        
                    </asp:DropDownList>
                   
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDistDescription" runat="server" Text="Description:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDistDescription" runat="server" CssClass="cls text" Width="421px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" MaxLength="50"></asp:TextBox>
                </td>
               
            </tr>
            
       
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" 
                        Font-Size="Large" ForeColor="White"
                        Font-Bold="True" CssClass="button green" 
                        OnClientClick= "return ValidationBeforeSave()" onclick="BtnSubmit_Click" />&nbsp;
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" CssClass="button green" 
                     OnClientClick="return ValidationBeforeUpdate()" onclick="BtnUpdate_Click" />&nbsp;
                    <asp:Button ID="BtnView" runat="server" Text="View" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="View Information" CssClass="button green" 
                         onclick="BtnView_Click" />&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" CausesValidation="False" 
                        CssClass="button red" OnClick="BtnExit_Click"  />
                    <br />
                 
                </td>
            </tr>
        </table>
    </div>
    <br />
     <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
 AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvDetailInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                  
                <asp:BoundField HeaderText="Divi Code" DataField="DiviCode" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField HeaderText="Dist Code" DataField="DistCode"  HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Description" DataField="DistDescription" HeaderStyle-Width="280px" ItemStyle-Width="280px" />
                
              </Columns>
           
        </asp:GridView>
     </div>

    
    <asp:Label ID="hdnDiviCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnDistCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnDiviOrgCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnDistOrgCode" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>
