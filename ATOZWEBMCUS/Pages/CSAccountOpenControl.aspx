<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true"
    CodeBehind="CSAccountOpenControl.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSAccountOpenControl"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />
    <div align="center">
        <table class="style1">
            <thead>
                <tr>
                    <th colspan="3">
                        Account Open Control Setup Maintenance
                    </th>
                </tr>
            </thead>
            <tr>
                <td>
                    <asp:DropDownList ID="ddlFlagCheck" runat="server" CssClass="cls text" Height="25px"
                        Width="260px" AutoPostBack="True" Font-Size="Large" Visible="False">
                        <asp:ListItem Value="1">Account Open</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAccType" runat="server" Text="Account Type:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccType" runat="server" Width="154px" CssClass="cls Text" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" ToolTip="Enter Code" AutoPostBack="true" OnTextChanged="txtAccType_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAccount" runat="server" CssClass="cls text" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="260px" AutoPostBack="True" Font-Size="Large" OnSelectedIndexChanged="ddlAccount_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDescription" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" Width="780px" OnRowDataBound="gvDescription_RowDataBound">
             <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="Code" HeaderStyle-Width="195px" ItemStyle-Width="195px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-Width="195px" ItemStyle-Width="195px" />
                <asp:TemplateField HeaderText="Select" HeaderStyle-Width="195px" ItemStyle-Width="195px">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkDescription" runat="server" AutoPostBack="false" OnCheckedChanged="chkDescription_CheckedChanged1" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Read Only" HeaderStyle-Width="180px" ItemStyle-Width="180px">
                    <ItemTemplate>
                       <asp:CheckBox ID="chk1Description" runat="server" AutoPostBack="false" 
            oncheckedchanged="chk1Description_CheckedChanged" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
 

        <asp:GridView ID="gvDetails" runat="server"  HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" Width="780px" OnRowDataBound="gvDetails_RowDataBound">
             <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                <asp:BoundField DataField="RecordCode" HeaderText="Code" HeaderStyle-Width="195px" ItemStyle-Width="195px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-Width="195px" ItemStyle-Width="195px" />
                <asp:TemplateField HeaderText="Select" HeaderStyle-Width="195px" ItemStyle-Width="195px">
                      <itemtemplate>
                        <asp:CheckBox ID="chkUpdate" runat="server" AutoPostBack="false" />
                    </itemtemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Read Only" HeaderStyle-Width="180px" ItemStyle-Width="180px">
                      <itemtemplate>
                        <asp:CheckBox ID="chk1Update" runat="server" AutoPostBack="false" />
                    </itemtemplate>
                    </asp:TemplateField>

            </Columns>
        </asp:GridView>
        <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:Label ID="Label5" runat="server" Text="Label" Visible="false"></asp:Label>
    </div>
    <br />
    <div align="center">
        <asp:Button ID="BtnAccountOpenSubmit" runat="server" Text="Submit" Font-Bold="True"
            Font-Size="Large" ForeColor="White" ToolTip="Update Information" CssClass="button green"
            OnClick="BtnAccountOpenSubmit_Click" OnClientClick="return ValidationBeforeSave()" />&nbsp;
        <asp:Button ID="btnAccountUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Large"
            ForeColor="White" ToolTip="Update Information" CssClass="button green" OnClick="btnAccountUpdate_Click"
            OnClientClick="return ValidationBeforeUpdate()" />
        <asp:Button ID="BtnAccountOpenExit" runat="server" Text="Exit" Font-Size="Large"
            ForeColor="#FFFFCC" Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page"
            CausesValidation="False" CssClass="button red" OnClick="BtnAccountOpenExit_Click"/>

    </div>
</asp:Content>
