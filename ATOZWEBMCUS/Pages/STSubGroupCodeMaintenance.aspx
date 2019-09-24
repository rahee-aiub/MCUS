<%@ Page Language="C#" MasterPageFile="~/MasterPages/INVMasterPage.Master" AutoEventWireup="true" CodeBehind="STSubGroupCodeMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.STSubGroupCodeMaintenance" Title="Sub Group Maintenance" %>
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
                  Category Maintenance
                    </th>
                </tr>
              
            </thead>
        <tr>
                <td>
                    <asp:Label ID="lblcode" runat="server" Text="Group Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtGrpCode" runat="server" CssClass="cls text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="115px" Height="25px" Font-Size="Large" AutoPostBack="true" 
                        ontextchanged="txtGrpCode_TextChanged" MaxLength="2"></asp:TextBox>
                  
                    <asp:DropDownList ID="ddlGroup" runat="server"  Height="30px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="316px" AutoPostBack="True" 
                        Font-Size="Large" 
                        onselectedindexchanged="ddlGroup_SelectedIndexChanged">
                        
                    </asp:DropDownList>
                </td>
            </tr>
         
                
            <tr>
                <td>
                    <asp:Label ID="lblsubcode" runat="server" Text="Category Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                                      
                    <asp:DropDownList ID="ddlSubGroup" runat="server" Height="30px"
                        Width="316px" AutoPostBack="True" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" 
                        onselectedindexchanged="ddlSubGroup_SelectedIndexChanged">
                     
                    </asp:DropDownList>
                   
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSubGrpDescription" runat="server" Text="Category Name:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSubGrpDescription" runat="server" CssClass="cls text" Width="421px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
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
                        OnClientClick= "return ValidationBeforeSave()" onclick="BtnSubmit_Click" Height="27px" />&nbsp;
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" CssClass="button green" 
                     OnClientClick="return ValidationBeforeUpdate()" onclick="BtnUpdate_Click" Height="27px" />&nbsp;
                    <asp:Button ID="BtnView" runat="server" Text="View" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="View Information" CssClass="button green" 
                         onclick="BtnView_Click" Height="27px" />&nbsp;
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Font-Bold="True" CausesValidation="False" 
                        CssClass="button red" OnClick="BtnExit_Click" Height="27px"  />
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
                  
                <asp:BoundField HeaderText="Group Code" DataField="GrpCode" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField HeaderText="Category Code" DataField="SubGrpCode"  HeaderStyle-Width="180px" ItemStyle-Width="180px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Name" DataField="SubGrpDescription" HeaderStyle-Width="280px" ItemStyle-Width="280px" ItemStyle-HorizontalAlign="Left" />
                
              </Columns>
           
        </asp:GridView>
     </div>

    
    <asp:Label ID="hdnGrpCode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="hdnSubGrpCode" runat="server" Text="" Visible="false"></asp:Label>
   
</asp:Content>
