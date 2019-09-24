<%@ Page Language="C#" MasterPageFile="~/MasterPages/CustomerServices.Master" AutoEventWireup="true" CodeBehind="CSAccountStatusCodeMaintenance.aspx.cs" Inherits="ATOZWEBMCUS.Pages.CSAccountStatusCodeMaintenance" Title="Untitled Page" %>
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
            height: 190px;
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
            width: 480px;

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
                    <th colspan="3">Account Status Maintenance
                    </th>
                </tr>

            </thead>

            <tr>
                <td>
                    <asp:Label ID="lblcode" runat="server" Text="Account Status Code:" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcode" runat="server" CssClass="cls text" Width="115px" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Font-Size="Large" AutoPostBack="true" ToolTip="Enter Code" OnTextChanged="txtcode_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlAcStatus" runat="server" Height="25px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Width="316px" AutoPostBack="True"
                        Font-Size="Large" 
                        onselectedindexchanged="ddlAcStatus_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDescription" runat="server" Text="Description:" Font-Size="Large"
                        ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="cls text" Width="316px" BorderColor="#1293D1" BorderStyle="Ridge"
                        Height="25px" Font-Size="Large" ToolTip="Enter Name"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Font-Size="Large" ForeColor="White"
                        Font-Bold="True" ToolTip="Insert Information" CssClass="button green" 
                        OnClientClick="return ValidationBeforeSave()" onclick="BtnSubmit_Click" />&nbsp;
                     <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="Update Information" CssClass="button green" 
                        OnClientClick="return ValidationBeforeUpdate()" onclick="BtnUpdate_Click" />&nbsp;
                      <asp:Button ID="BtnView" runat="server" Text="View" Font-Bold="True" Font-Size="Large"
                        ForeColor="White" ToolTip="View Information" CssClass="button green" 
                         onclick="BtnView_Click" />
                    <asp:Button ID="BtnExit" runat="server" Text="Exit" Font-Size="Large" ForeColor="#FFFFCC"
                        Height="27px" Width="86px" Font-Bold="True" ToolTip="Exit Page" CausesValidation="False"
                        CssClass="button red" OnClick="BtnExit_Click" />
                    <br />
                </td>
            </tr>
        </table>
           <div align="center" class="grid_scroll">
        <asp:GridView ID="gvDetailInfo" runat="server" HeaderStyle-CssClass="FixedHeader" HeaderStyle-BackColor="YellowGreen" 
 AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-Height="10px" OnRowDataBound="gvDetailInfo_RowDataBound">
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <Columns>
                  
                <asp:BoundField HeaderText="Code" DataField="AccStatusCode" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField HeaderText="Description" DataField="AccStatusDescription" HeaderStyle-Width="380px" ItemStyle-Width="380px"/>
                
              </Columns>
          
        </asp:GridView>
     </div>
    </div>







</asp:Content>
