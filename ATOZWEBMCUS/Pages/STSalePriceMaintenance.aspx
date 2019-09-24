<%@ Page Language="C#" MasterPageFile="~/MasterPages/INVMasterPage.Master" AutoEventWireup="true" CodeBehind="STSalePriceMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.STSalePriceMaintenance" Title="Sale Price Maintenance" %>
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

	    function ValidationBeforeDelete() {
	        return confirm('Are you sure you want to Delete information?');
	    }
	   	    
			
    </script>

           <style type="text/css">
        .grid_scroll {
            overflow: auto;
            height: 350px;
            width: 650px;
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
                  Stock Sale Price Maintenance
                    </th>
                </tr>
              
            </thead>

            <tr>
                <td>
                    <asp:Label ID="lblcode" runat="server" Text="Group:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlGroup" runat="server" Height="30px" Width="316px" 
                        Font-Size="Large"  BorderColor="#1293D1" BorderStyle="Ridge">
                        <%--<asp:ListItem Value="0">-Select-</asp:ListItem>--%>
                    </asp:DropDownList>
                    
                </td>
            </tr>
         
            <tr>
               
                <td colspan="2">
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" ToolTip="Insert Information" CssClass="button green"  
                        OnClientClick="return ValidationBeforeSave()" onclick="BtnSubmit_Click" Height="27px" />&nbsp;
                    <asp:Button ID="BtnSearch" runat="server" Text="Search" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="Search Information" CssClass="button green"  
                         onclick="BtnSearch_Click" Height="27px" />&nbsp;

                   
                     <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click"/>
                    <br />
                </td>
            </tr>
        </table>
    </div>
<br />
  <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetails" runat="server" HeaderStyle-BackColor="YellowGreen"
            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />

            <Columns>
                <asp:TemplateField HeaderText="Item Code" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtItemCode"  runat="server" EnableViewState="true" Enabled="false" Width="100px" Height="20" Style="text-align: center" ForeColor="Blue" Text='<%#Bind("STKItemCode")%>'>
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="250px" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtItemName" runat="server" EnableViewState="true" Width="250px" Height="20" Enabled="false" Style="text-align: left" ForeColor="Blue" Text='<%#Bind("STKItemName")%>'>
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Avg Cost" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtAvgCost"  runat="server" EnableViewState="true" Enabled="false" Width="100px" Height="20" Style="text-align: right" ForeColor="Blue" Text='<%#Bind("STKUnitAvgCost")%>'>
                        
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
              

                <asp:TemplateField HeaderText="Sale Cost" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtSaleCost" runat="server" Font-Bold="true" Enabled="true" EnableViewState="true" Width="100px" Height="20" Style="text-align: right" ForeColor="Blue" onFocus="javascript:this.select();" Text='<%#Bind("STKUnitSalePrice")%>'>
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>

   
    <asp:Label ID="hdnNewUnitNo" runat="server" Text="" Visible="false"></asp:Label>
  

</asp:Content>
