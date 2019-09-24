<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSParameterControl.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSParameterControl" Title="Untitled Page" %>
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
            height: 230px;
            margin: 0 auto;
            width:813px;
            
        }

        .border_color {
            border: 1px solid #006;
            background: #D5D5D5;
        }
       .FixedHeader {
            position: absolute;
            font-weight: bold;
     
        }  
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
   <br />
    <br />
    <br />
    <div align="center">
        <table class="style1">
         <thead>
                <tr>
                    <th colspan="3">
                  Parameter Control Maintenance
                    </th>
                </tr>
              
            </thead>
            <tr>
            <td>
              <asp:DropDownList ID="ddlFlagCheck" runat="server" CssClass="cls text" 
                        Height="25px" Width="260px" AutoPostBack="True"
                        Font-Size="Large" Visible="False">
                  <asp:ListItem Value="2">ParameterInfo</asp:ListItem>
                        </asp:DropDownList>
            </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAccType" runat="server" Text="Account Type:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccType" runat="server" Width="154px" CssClass="cls Text" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" ToolTip="Enter Code" AutoPostBack="true" 
                        ontextchanged="txtAccType_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAccount" runat="server" CssClass="cls text" 
                        Height="25px" Width="260px" AutoPostBack="True"
                        Font-Size="Large"  BorderColor="#1293D1" BorderStyle="Ridge"
                        OnSelectedIndexChanged="ddlAccount_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
         
        </table>
    </div><br />
    <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDescription" runat="server" Width="780px" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvDescription_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="Code" HeaderStyle-Width="260px" ItemStyle-Width="260px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-Width="260px" ItemStyle-Width="260px"/>
                <asp:TemplateField HeaderText="Check/Uncheck" HeaderStyle-Width="245px" ItemStyle-Width="245px">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkDescription" runat="server" AutoPostBack="false" OnCheckedChanged="chkDescription_CheckedChanged1" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:GridView ID="gvDetails" runat="server" Width="780px" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvDetails_RowDataBound">
              <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                <asp:BoundField DataField="RecordCode" HeaderText="Code" HeaderStyle-Width="260px" ItemStyle-Width="260px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-Width="260px" ItemStyle-Width="260px"/>
                <asp:TemplateField HeaderText="Check/Uncheck" HeaderStyle-Width="245px" ItemStyle-Width="245px">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkUpdate" runat="server" AutoPostBack="false" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>
    </div>
    <br />
    <div align="center">
        <asp:Button ID="BtnAccountOpenSubmit" runat="server" Text="Submit" Font-Bold="True"
            Font-Size="Large" ForeColor="White" ToolTip="Update Information" CssClass="button green"
            OnClick="BtnAccountOpenSubmit_Click" OnClientClick="return ValidationBeforeSave()" />&nbsp;
        <asp:Button ID="btnAccountUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Large"
            ForeColor="White" ToolTip="Update Information" CssClass="button green" OnClick="btnAccountUpdate_Click" OnClientClick="return ValidationBeforeUpdate()" />
        <asp:Button ID="BtnAccountOpenExit" runat="server" Text="Exit" Font-Size="Large"
            ForeColor="#FFFFCC" Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page"
            CausesValidation="False" CssClass="button red" OnClick="BtnAccountOpenExit_Click" /></div>



</asp:Content>
